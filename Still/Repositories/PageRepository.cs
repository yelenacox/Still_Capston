using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Still.Models;
using Still.Utils;
using System;
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
        public Page GetPageById(int id)
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
                        JOIN PagePicture pp on pp.PageId = x.id
                        JOIN Picture pi on pi.Id = pp.PictureId
                        WHERE @id = x.id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Page page = null;
                        while (reader.Read())
                        {
                            if (page == null)
                            {
                                page = new Page()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    UserProfileId = reader.GetInt32(reader.GetOrdinal("PageUserId")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    Description = DbUtils.GetString(reader, "PageDescription"),
                                    DateCreated = reader.GetDateTime(reader.GetOrdinal("PageDateCreated")),
                                    PagePictures = new List<PagePicture>()
                                };
                            }
                            if (DbUtils.IsNotDbNull(reader, "PagePictureId"))
                            {
                                page.PagePictures.Add(new PagePicture()
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
                        return page;
                    }
                    

                }
            }
        }

        public int Add(Page page)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Page ( 
                            UserProfileId , Title, Description, DateCreated  )                            
                        OUTPUT INSERTED.ID
                        VALUES ( 
                            @UserProfileId, @Title, @Description, @DateCreated )";
                    page.DateCreated = DateTime.Now;

                    DbUtils.AddParameter(cmd, "@UserProfileId", page.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Title", page.Title);
                    DbUtils.AddParameter(cmd, "@Description", page.Description);
                    DbUtils.AddParameter(cmd, "@DateCreated", page.DateCreated);

                    page.Id = (int)cmd.ExecuteScalar();
                    return page.Id;

                }
            }
        }

        public void AddPagePicture(PagePicture pagePicture)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO PagePicture ( 
                            PageId, PictureId, Description )
                        OUTPUT INSERTED.ID
                        VALUES (
                            @PageId, @PictureId, @Description )";

                    DbUtils.AddParameter(cmd, "@PageId", pagePicture.PageId);
                    DbUtils.AddParameter(cmd, "@PictureId", pagePicture.PictureId);
                    DbUtils.AddParameter(cmd, "@Description", pagePicture.Description);

                    pagePicture.Id = (int)cmd.ExecuteScalar();
                   
                }
            }
        }

        public void Update(Page page)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Page
                            SET Title = @Title, 
                                Description = @Description 
                                WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", page.Id);
                    DbUtils.AddParameter(cmd, "@UserProfileId", page.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Title", page.Title);
                    DbUtils.AddParameter(cmd, "@Description", page.Description);
                
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePagePicture(PagePicture pagePicture)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE PagePicture
                    SET PageId = @PageId, 
                        PictureId = @PictureId, 
                        Description = @Description
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", pagePicture.Id);
                    DbUtils.AddParameter(cmd, "@PageId", pagePicture.PageId);
                    DbUtils.AddParameter(cmd, "@PictureId", pagePicture.PictureId);
                    DbUtils.AddParameter(cmd, "@Description", pagePicture.Description);

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public List<PagePicture> GetPagePicturesByPageId(int pageId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, PageId, PictureId
                        FROM PagePicture 
                        WHERE @pageId = PageId";
                    //cmd.Parameters.AddWithValue("@pageId", pageId);
                    DbUtils.AddParameter(cmd, "@PageId", pageId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var pagePictures = new List<PagePicture>();

                        while (reader.Read())
                        {
                            pagePictures.Add(new PagePicture()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                PageId = reader.GetInt32(reader.GetOrdinal("PageId")),
                                PictureId = reader.GetInt32(reader.GetOrdinal("PictureId"))
                            });
                        }
                        return pagePictures;
                    }
                }
            }
        }

        public void Delete (int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Page
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }public void DeletePagePicture (int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM PagePicture
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}

