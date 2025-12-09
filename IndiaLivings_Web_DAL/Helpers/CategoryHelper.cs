using IndiaLivings_Web_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Helpers
{
    public class CategoryHelper
    {
        public string AddCategory(string name, string image, string createdBy)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Category/addCategory?strCategoryName={name}&strCategoryImage={image}&strCreatedBy={createdBy}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdateCategory(int categoryId, string name, string image, string updatedBy)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Category/updateCategory?intCategoryID={categoryId}&strCategoryName={name}&strCategoryImage={image}&strUpdatedBy={updatedBy}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteCategory(int categoryId, string updatedBy)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Category/deleteCategory?intCategoryID={categoryId}&strUpdatedBy={updatedBy}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string AddSubcategory(string subCategoryName, int categoryId, string createdBy)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Category/addSubCategory?subCatergoryName={subCategoryName}&intCategoryID={categoryId}&strCreatedBy={createdBy}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string UpdateSubcategory(int subCategoryId, string subCategoryName, int categoryId, string updatedBy)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Category/updateSubCategory?subCategoryID={subCategoryId}&subCatergoryName={subCategoryName}&intCategoryID={categoryId}&strUpdatedBy={updatedBy}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string DeleteSubcategory(int subCategoryId, string updatedBy)
        {
            string response = String.Empty;
            try
            {
                response = ServiceAPI.Post_Api($"https://api.indialivings.com/api/Category/deleteSubCategory?subCategoryID={subCategoryId}&strUpdatedBy={updatedBy}");
            }
            catch (Exception ex)
            {
                response = ex.Message;
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
    }
}
