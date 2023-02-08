﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Still.Models;
using Still.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Still.Repositories
{
    public class PageRepository : BaseRepository, IPageRepository
    {
        public PageRepository(IConfiguration config) : base(config) { }

        public List<Page> GetAllPagesByUser(int userProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT x.Id, x.UserProfileId AS PageUserId, x.Title, x.Description AS PageDescription, x.DateCreated AS PageDateCreated, 
                               pp.Id AS PagePictureId, pp.PageId, pp.PictureId, 
                               pi.Id AS PictureId, pi.UserProfileId AS PictureUserId, pi.Description AS PictureDescription, pi.DateCreated AS PictureDateCreated, pi.PictureLocation
                        FROM Page x
                        JOIN UserProfile u on u.Id = @userProfileId
                        JOIN PagePicture pp on pp.PageId = x.id
                        JOIN Picture pi on pi.Id = pp.PictureId
                        WHERE @userProfileId = x.UserProfileId
                        ORDER BY x.DateCreated DESC";
                    cmd.Parameters.AddWithValue("@userProfileId", userProfileId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var pages = new List<Page>();

                        while (reader.Read())
                        {
                            var Id = DbUtils.GetInt(reader, "id");

                            var existingPage = pages.FirstOrDefault(x => x.Id == Id);
                            if (existingPage == null)
                            {
                                existingPage = new Page()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    UserProfileId = reader.GetInt32(reader.GetOrdinal("PageUserId")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    Description = DbUtils.GetString(reader, "PageDescription"),
                                    DateCreated = reader.GetDateTime(reader.GetOrdinal("PageDateCreated")),
                                    PagePictures = new List<PagePicture>()
                                };
                                pages.Add(existingPage);
                            }
                            if (DbUtils.IsNotDbNull(reader, "Id"))
                            {
                                existingPage.PagePictures.Add(new PagePicture()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("PagePictureId")),
                                    PageId = reader.GetInt32(reader.GetOrdinal("PageId")),
                                    PictureId = reader.GetInt32(reader.GetOrdinal("PictureId")),
                                    Picture = new Picture()
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("PictureId")),
                                        UserProfileId = reader.GetInt32(reader.GetOrdinal("PictureUserId")),
                                        Description = DbUtils.GetString(reader, "PictureDescription"),
                                        DateCreated = reader.GetDateTime(reader.GetOrdinal("PictureDateCreated")),
                                        PictureLocation = reader.GetString(reader.GetOrdinal("PictureLocation")),
                                    }
                                });
                            }
                        }
                        return pages;
                    }

                }
            }
        }
    }

    /*private Page NewPageFromReader(SqlDataReader reader)
    {
        return new Page()
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
            Title = reader.GetString(reader.GetOrdinal("Title")),
            Description = DbUtils.GetString(reader, "Description"),
            DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
            PagePictures = new List<PagePicture>()
            {
                PagePictures.Add()
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                PageId = reader.GetInt32(reader.GetOrdinal("PageId")),
                PictureId = reader.GetInt32(reader.GetOrdinal("PictureId")),
            },

    }*/
}

