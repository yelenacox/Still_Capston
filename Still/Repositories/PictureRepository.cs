using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using Still.Models;
using Still.Utils;
using System;
using Microsoft.Extensions.Hosting;

namespace Still.Repositories
{
    public class PictureRepository : BaseRepository, IPictureRepository
    {
        public PictureRepository(IConfiguration config) : base(config) { }

        public List<Picture> GetAllPictures()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, UserProfileId, Description, DateCreated, PictureLocation
                        FROM Picture
                        ORDER BY DateCreated DESC";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var pictures = new List<Picture>();

                        while (reader.Read())
                        {
                            pictures.Add(NewPictureFromReader(reader));
                        }
                        return pictures;
                    }
                }
            }
        } 
        
        public List<Picture> GetUserPictures(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT p.Id, UserProfileId, Description, DateCreated, PictureLocation, u.Id, FirebaseUserId, Email, Name
                        FROM Picture p
                        JOIN UserProfile u ON u.Id = p.UserProfileId
                        WHERE u.FirebaseUserId = @firebaseUserId
                        ORDER BY DateCreated DESC";

                    cmd.Parameters.AddWithValue("@FirebaseUserId", firebaseUserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var pictures = new List<Picture>();

                        while (reader.Read())
                        {
                            pictures.Add(NewPictureFromReader(reader));
                        }
                        return pictures;
                    }
                }
            }
        }

        public Picture GetPictureById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, UserProfileId, Description, DateCreated, PictureLocation
                        FROM Picture
                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    Picture picture = null;
                    if (reader.Read())
                    {
                        picture = NewPictureFromReader(reader);
                    }
                 
                    return picture;
                }
            }
        }

        public void Add(Picture picture)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Picture ( 
                            UserProfileId, Description, DateCreated, PictureLocation )
                        OUTPUT INSERTED.ID
                        VALUES (
                            @UserProfileId, @Description, @DateCreated, @PictureLocation )";
                        
                        picture.DateCreated= DateTime.Now;

                    DbUtils.AddParameter(cmd, "@UserProfileId", picture.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Description", picture.Description);
                    DbUtils.AddParameter(cmd, "@DateCreated", picture.DateCreated);
                    DbUtils.AddParameter(cmd, "@PictureLocation", picture.PictureLocation);

                    picture.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Picture picture)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Picture
                            SET UserProfileId = @UserProfileId,
                                Description = @Description,
                                DateCreated = @DateCreated,
                                PictureLocation = @PictureLocation
                                WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", picture.Id);
                    DbUtils.AddParameter(cmd, "@UserProfileId", picture.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Description", picture.Description);
                    DbUtils.AddParameter(cmd, "@DateCreated", picture.DateCreated);
                    DbUtils.AddParameter(cmd, "@PictureLocation", picture.PictureLocation);
               
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        { 
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Picture
                            WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Picture NewPictureFromReader(SqlDataReader reader)
        {
            return new Picture()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                Description = DbUtils.GetString(reader, "Description"),
                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                PictureLocation = reader.GetString(reader.GetOrdinal("PictureLocation"))
            };
        }
    }
}
