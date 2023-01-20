using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;

namespace Streamish.Repositories
{

    public class UserProfileRepository : BaseRepository//, IUserProfileRepository
, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT Id, [Name], Email, DateCreated, ImageUrl
                            FROM UserProfile
                        
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var profiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            profiles.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            });
                        }

                        return profiles;
                    }
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                           SELECT Id, [Name], Email, DateCreated, ImageUrl
                            FROM UserProfile
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        UserProfile profile = null;
                        if (reader.Read())
                        {
                            profile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            };
                        }

                        return profile;
                    }
                }
            }
        }

        public UserProfile GetUserProfileByIdWithVideos(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT up.Id, up.Name, up.Email, up.ImageUrl,
                    up.DateCreated AS UserProfileDateCreated,


                     v.Id AS VideoId, v.Title, v.Description, v.Url, 
                     v.DateCreated AS VideoDateCreated, v.UserProfileId 

                    FROM UserProfile up                         
                     JOIN Video v ON up.Id = v.UserProfileId

                     
                     WHERE up.Id = @Id
                    ORDER BY  v.DateCreated
            ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        UserProfile profile = null;
                        while (reader.Read())
                        {

                            if (profile == null)
                            {
                                profile = new UserProfile()
                                {
                                    Id = id,
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                    DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
                                    Videos = new List<Video>()

                                };
                            }
                            if (DbUtils.IsNotDbNull(reader, "VideoId"))
                            {
                                profile.Videos.Add(new Video()
                                {
                                    Id = DbUtils.GetInt(reader, "VideoId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    DateCreated = DbUtils.GetDateTime(reader, "VideoDateCreated"),
                                    Url = DbUtils.GetString(reader, "Url"),
                                    UserProfileId = id,
                                });
                            }

                        }

                        return profile;
                    }
                }
            }
        }

        public void Add(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile ([Name], Email, DateCreated, ImageUrl)
                        OUTPUT INSERTED.ID
                        VALUES (@name, @email, @url, @dateCreated, @imageUrl)";

                    DbUtils.AddParameter(cmd, "@name", profile.Name);
                    DbUtils.AddParameter(cmd, "@email", profile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", profile.DateCreated);
                    DbUtils.AddParameter(cmd, "@imageUrl", profile.ImageUrl);

                    profile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @name,
                               Email = @email,
                               DateCreated = @DateCreated,
                               ImageUrl = @imageUrl,
                               
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@name", profile.Name);
                    DbUtils.AddParameter(cmd, "@email", profile.Email);
                    DbUtils.AddParameter(cmd, "@dateCreated", profile.DateCreated);
                    DbUtils.AddParameter(cmd, "@imageUrl", profile.ImageUrl);

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
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}