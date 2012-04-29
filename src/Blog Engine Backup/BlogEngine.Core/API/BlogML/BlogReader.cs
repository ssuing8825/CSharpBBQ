namespace BlogEngine.Core.API.BlogML
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Linq;
    using System.Globalization;

    using global::BlogML;
    using global::BlogML.Xml;

    /// <summary>
    /// Class to validate BlogML data and import it into Blog
    /// </summary>
    public class BlogReader : BaseReader
    {
        #region Constants and Fields

        /// <summary>
        ///     The xml data.
        /// </summary>
        private string xmlData = string.Empty;

        /// <summary>
        ///     The category lookup.
        /// </summary>
        private List<Category> categoryLookup = new List<Category>();

        private List<BlogMlExtendedPost> blogsExtended = new List<BlogMlExtendedPost>();

        #endregion

        #region Properties

        /// <summary>
        /// imported posts counter
        /// </summary>
        public int PostCount { get; set; }

        /// <summary>
        ///     Sets BlogML data uploaded and saved as string
        /// </summary>
        public string XmlData
        {
            set
            {
                xmlData = value;
            }
        }

        /// <summary>
        ///     Gets an XmlReader that converts BlogML data saved as string into XML stream
        /// </summary>
        private XmlTextReader XmlReader
        {
            get
            {
                var byteArray = Encoding.UTF8.GetBytes(this.xmlData);
                var stream = new MemoryStream(byteArray);
                return new XmlTextReader(stream);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Imports BlogML file into blog
        /// </summary>
        /// <returns>
        /// True if successful
        /// </returns>
        public override bool Import()
        {
            Message = string.Empty;
            var blog = new BlogMLBlog();
            try
            {
                blog = BlogMLSerializer.Deserialize(XmlReader);
            }
            catch (Exception ex)
            {
                Message = string.Format("BlogReader.Import: BlogML could not load with 2.0 specs. {0}", ex.Message);
                Utils.Log(Message);
                return false;
            }

            try
            {
                LoadFromXmlDocument();

                LoadBlogCategories(blog);

                LoadBlogExtendedPosts(blog);

                LoadBlogPosts();

                Message = string.Format("Imported {0} new posts", PostCount);
            }
            catch (Exception ex)
            {
                Message = string.Format("BlogReader.Import: {0}", ex.Message);
                Utils.Log(Message);
                return false;
            }

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// BlogML does not support tags - load directly fro XML
        /// </summary>
        private void LoadFromXmlDocument()
        {
            var doc = new XmlDocument();
            doc.Load(XmlReader);
            var posts = doc.GetElementsByTagName("post");

            foreach (XmlNode post in posts)
            {
                var blogX = new BlogMlExtendedPost();

                if(post.Attributes != null)
                    blogX.PostUrl = post.Attributes["post-url"].Value;

                if (post.ChildNodes.Count <= 0)
                {
                    blogsExtended.Add(blogX);
                    continue;
                }

                foreach (XmlNode child in post.ChildNodes)
                {
                    if (child.Name == "tags")
                    {
                        foreach (XmlNode tag in child.ChildNodes)
                        {
                            if (tag.Attributes != null)
                            {
                                if (blogX.Tags == null) blogX.Tags = new StateList<string>();
                                blogX.Tags.Add(tag.Attributes["ref"].Value);
                            }
                        }
                    }

                    if(child.Name == "comments")
                        LoadBlogComments(blogX, child);

                    if (child.Name == "trackbacks")
                        LoadBlogTrackbacks(blogX, child);
                }
                blogsExtended.Add(blogX);
            }
        }

        /// <summary>
        /// Lost post comments from xml file
        /// </summary>
        /// <param name="blogX">extended blog</param>
        /// <param name="child">comments xml node</param>
        private static void LoadBlogComments(BlogMlExtendedPost blogX, XmlNode child)
        {
            foreach (XmlNode com in child.ChildNodes)
            {
                if(com.Attributes != null)
                {
                    var c = new Comment
                                {
                                    Id = new Guid(com.Attributes["id"].Value),
                                    Author = com.Attributes["user-name"].Value,
                                    Email = com.Attributes["user-email"].Value,
                                    ParentId = new Guid(com.Attributes["parentid"].Value),
                                    IP = com.Attributes["user-ip"].Value,
                                    DateCreated = DateTime.ParseExact(com.Attributes["date-created"].Value,
                                                                      "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
                                };

                    if(!string.IsNullOrEmpty(com.Attributes["user-url"].Value))
                        c.Website = new Uri(com.Attributes["user-url"].Value);

                    c.IsApproved = bool.Parse(com.Attributes["approved"].Value);

                    foreach (XmlNode comNode in com.ChildNodes)
                    {
                        if(comNode.Name == "content")
                        {
                            c.Content = comNode.InnerText;
                        }
                    }
                    if(blogX.Comments == null) blogX.Comments = new List<Comment>();
                    blogX.Comments.Add(c);
                }
            }
        }

        /// <summary>
        /// Lost post trackbacks and pingbacks from xml file
        /// </summary>
        /// <param name="blogX">extended blog</param>
        /// <param name="child">comments xml node</param>
        private static void LoadBlogTrackbacks(BlogMlExtendedPost blogX, XmlNode child)
        {
            foreach (XmlNode com in child.ChildNodes)
            {
                if (com.Attributes != null)
                {
                    var c = new Comment
                    {
                        Id = new Guid(com.Attributes["id"].Value), 
                        IP = "127.0.0.1",
                        IsApproved = bool.Parse(com.Attributes["approved"].Value),
                        DateCreated = DateTime.ParseExact(com.Attributes["date-created"].Value,
                                                          "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
                    };

                    if (!string.IsNullOrEmpty(com.Attributes["url"].Value))
                        c.Website = new Uri(com.Attributes["url"].Value);

                    foreach (XmlNode comNode in com.ChildNodes)
                    {
                        if (comNode.Name == "title")
                        {
                            c.Content = comNode.InnerText;
                        }
                    }

                    c.Email = c.Content.ToLowerInvariant().Contains("pingback") ? "pingback" : "trackback";
                    c.Author = c.Email;

                    if (blogX.Comments == null) blogX.Comments = new List<Comment>();
                    blogX.Comments.Add(c);
                }
            }
        }

        /// <summary>
        /// Load blog categories
        /// </summary>
        /// <param name="blog">BlogML blog</param>
        private void LoadBlogCategories(BlogMLBlog blog)
        {
            foreach (var cat in blog.Categories)
            {
                var c = new Category
                {
                    Id = new Guid(cat.ID),
                    Title = cat.Title,
                    Description = string.IsNullOrEmpty(cat.Description) ? "" : cat.Description,
                    DateCreated = cat.DateCreated,
                    DateModified = cat.DateModified
                };

                if (!string.IsNullOrEmpty(cat.ParentRef) && cat.ParentRef != "0")
                    c.Parent = new Guid(cat.ParentRef);

                categoryLookup.Add(c);

                if (Category.GetCategory(c.Id) == null)
                {
                    c.Save();
                }
            }
        }

        /// <summary>
        /// extended post has all BlogML plus fields not supported
        /// by BlogML like tags. here we assign BlogML post
        /// to extended matching on post URL 
        /// </summary>
        /// <param name="blog">BlogML blog</param>
        private void LoadBlogExtendedPosts(BlogMLBlog blog)
        {
            foreach (var post in blog.Posts)
            {
                if (post.PostType == BlogPostTypes.Normal)
                {
                    BlogMLPost p = post;
                    blogsExtended.Where(b => b.PostUrl == p.PostUrl).FirstOrDefault().BlogPost = post;
                }
            }
        }

        /// <summary>
        /// Loads the blog posts.
        /// </summary>
        private void LoadBlogPosts()
        {
            var bi = new BlogImporter();
            Utils.Log("BlogReader.LoadBlogPosts: Start importing posts");

            foreach (BlogMlExtendedPost extPost in blogsExtended)
            {
                try
                {
                    BlogMlExtendedPost post = extPost;

                    if (extPost.BlogPost.Categories.Count > 0)
                    {
                        for (var i = 0; i < extPost.BlogPost.Categories.Count; i++)
                        {
                            int i2 = i;
                            var cId = new Guid(post.BlogPost.Categories[i2].Ref);

                            foreach (var category in categoryLookup)
                            {
                                if (category.Id == cId)
                                {
                                    if (extPost.Categories == null)
                                        extPost.Categories = new StateList<Category>();

                                    extPost.Categories.Add(category);
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(bi.AddPost(extPost)))
                    {
                        PostCount++;
                    }
                    else
                    {
                        Utils.Log("Post '{0}' has been skipped" + extPost.BlogPost.Title);
                    }
                }
                catch (Exception ex)
                {
                    Utils.Log("BlogReader.LoadBlogPosts: " + ex.Message);
                }
            }
            bi.ForceReload();
            Utils.Log(string.Format("BlogReader.LoadBlogPosts: Completed importing {0} posts", PostCount));
        }

        #endregion
    }
}