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
