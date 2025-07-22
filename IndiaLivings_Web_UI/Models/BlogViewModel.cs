using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class BlogViewModel
    {
        public int blogId { get; set; }
        public string title { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public string summary { get; set; } = string.Empty;
        public string author { get; set; } = string.Empty;
        public byte[] featuredImage { get; set; } = [];
        public string tags { get; set; } = string.Empty;
        public int categoryID { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public int viewCount { get; set; }
        public bool isFeatured { get; set; } = false;
        public bool isPublished { get; set; } = false;
        public DateTime publishedDate { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; } = string.Empty;
        public DateTime updatedDate { get; set; }
        public string updatedBy { get; set; } = string.Empty;

        public static List<BlogViewModel> GetAllBlogs(int pageNumber, int pageSize, int categoryId, bool publishedOnly)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<BlogModel> blogList = AH.GetAllBlogs(pageNumber, pageSize, categoryId, publishedOnly);
            List<BlogViewModel> blogs = new List<BlogViewModel>();
            try
            {
                if (blogList != null && blogList.Count > 0)
                {
                    foreach (var blog in blogList)
                    {
                        BlogViewModel blogVM = new BlogViewModel
                        {
                            blogId = blog.blogId,
                            title = blog.title,
                            content = blog.content,
                            summary = blog.summary,
                            author = blog.author,
                            featuredImage = blog.featuredImage,
                            tags = blog.tags,
                            categoryID = blog.categoryID,
                            categoryName = blog.categoryName,
                            viewCount = blog.viewCount,
                            isFeatured = blog.isFeatured,
                            isPublished = blog.isPublished,
                            publishedDate = blog.publishedDate,
                            isActive = blog.isActive,
                            createdDate = blog.createdDate,
                            createdBy = blog.createdBy,
                            updatedDate = blog.updatedDate,
                            updatedBy = blog.updatedBy
                        };
                        blogs.Add(blogVM);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return blogs;
        }
        public static BlogViewModel GetBlogById(int blogId)
        {
            BlogViewModel blogVM = new BlogViewModel();
            AuthenticationHelper AH = new AuthenticationHelper();
            BlogModel blog = AH.GetBlogById(blogId);
            try
            {
                if (blog != null)
                {
                    blogVM.blogId = blog.blogId;
                    blogVM.title = blog.title;
                    blogVM.content = blog.content;
                    blogVM.summary = blog.summary;
                    blogVM.author = blog.author;
                    blogVM.featuredImage = blog.featuredImage;
                    blogVM.tags = blog.tags;
                    blogVM.categoryID = blog.categoryID;
                    blogVM.categoryName = blog.categoryName;
                    blogVM.viewCount = blog.viewCount;
                    blogVM.isFeatured = blog.isFeatured;
                    blogVM.isPublished = blog.isPublished;
                    blogVM.publishedDate = blog.publishedDate;
                    blogVM.isActive = blog.isActive;
                    blogVM.createdDate = blog.createdDate;
                    blogVM.createdBy = blog.createdBy;
                    blogVM.updatedDate = blog.updatedDate;
                    blogVM.updatedBy = blog.updatedBy;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return blogVM;
        }
        public string PostBlog(BlogViewModel blog)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string result = "An error occured";
            try
            {
                BlogModel blogModel = new BlogModel
                {
                    blogId = blog.blogId,
                    title = blog.title,
                    content = blog.content,
                    summary = blog.summary,
                    author = blog.author,
                    featuredImage = blog.featuredImage,
                    tags = blog.tags,
                    categoryID = blog.categoryID,
                    categoryName = blog.categoryName,
                    viewCount = blog.viewCount,
                    isFeatured = blog.isFeatured,
                    isPublished = blog.isPublished,
                    publishedDate = blog.publishedDate,
                    isActive = blog.isActive,
                    createdDate = blog.createdDate,
                    createdBy = blog.createdBy,
                    updatedDate = blog.updatedDate,
                    updatedBy = blog.updatedBy
                };
                result = AH.BlogPost(blogModel);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public string UpdateBlog(BlogViewModel blog)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string result = "An error occured";
            try
            {
                BlogModel blogModel = new BlogModel
                {
                    blogId = blog.blogId,
                    title = blog.title,
                    content = blog.content,
                    summary = blog.summary,
                    author = blog.author,
                    featuredImage = blog.featuredImage,
                    tags = blog.tags,
                    categoryID = blog.categoryID,
                    categoryName = blog.categoryName,
                    viewCount = blog.viewCount,
                    isFeatured = blog.isFeatured,
                    isPublished = blog.isPublished,
                    publishedDate = blog.publishedDate,
                    isActive = blog.isActive,
                    createdDate = blog.createdDate,
                    createdBy = blog.createdBy,
                    updatedDate = DateTime.Now, // Assuming updated date is now
                    updatedBy = blog.updatedBy
                };
                result = AH.UpdateBlog(blogModel);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public string DeleteBlog(int blogId, string updatedBy)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string result = "An error occured";
            try
            {
                result = AH.DeleteBlog(blogId, updatedBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
        public string PublishBlog(int blogId, string updatedBy)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string result = "An error occured";
            try
            {
                result = AH.PublishBlog(blogId, updatedBy);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
    }
    public class BlogCategoriesViewModel
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int blogCount { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; } = string.Empty;
        public string updatedBy { get; set; } = string.Empty;

        public static List<BlogCategoriesViewModel> GetAllBlogCategories()
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<BlogCategoriesModel> categoryList = AH.GetBlogCategories();
            List<BlogCategoriesViewModel> blogCategoriesList = [];
            try
            {
                if (categoryList != null && categoryList.Count > 0)
                {
                    foreach (var category in categoryList)
                    {
                        BlogCategoriesViewModel categoryVM = new BlogCategoriesViewModel
                        {
                            categoryID = category.categoryID,
                            categoryName = category.categoryName,
                            description = category.description,
                            blogCount = category.blogCount,
                            isActive = category.isActive,
                            createdDate = category.createdDate,
                            createdBy = category.createdBy,
                            updatedBy = category.updatedBy
                        };
                        blogCategoriesList.Add(categoryVM);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return blogCategoriesList;
        }
    }
}
