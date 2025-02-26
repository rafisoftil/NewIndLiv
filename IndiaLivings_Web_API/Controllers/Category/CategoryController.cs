using IndiaLivingsAPI.Model.Categories;
using IndiaLivingsAPI.Model.ErrorLogs;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivingsAPI.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Categories

        [HttpPost("addCategory")]
        public ActionResult insertCategory(string strCategoryName, string strCategoryImage, string strCreatedBy)
        {
            clsCategoryDetails _categoryDetail = new clsCategoryDetails();
            bool blnUserFlag = false;
            string strStatus = "Add Category Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _categoryDetail.insertCategory(strCategoryName, strCategoryImage, strCreatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "Category Added.";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpPost("updateCategory")]
        public ActionResult updateCategory(int intCategoryID, string strCategoryName, string strCategoryImage, string strUpdatedBy)
        {
            clsCategoryDetails _CategoryDetail = new clsCategoryDetails();
            bool blnUserFlag = false;
            string strStatus = "Category Update Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _CategoryDetail.updateCategory(intCategoryID, strCategoryName, strCategoryImage, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "Category Update Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpDelete("deleteCategory")]
        public ActionResult DeleteCategory(int intCategoryID, string strUpdatedBy)
        {
            clsCategoryDetails _categoryDetail = new clsCategoryDetails();
            bool blnUserFlag = false;
            string strStatus = "Category Delete Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _categoryDetail.deleteCategory(intCategoryID, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "Category Deleted Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }
        [HttpGet("GetActiveListofCategory")]
        public List<Category> GetActiveCategoryList(int intCategoryID)
        {
            List<Category> _lstCategory = new List<Category>();
            clsCategoryDetails _categoryDetail = new clsCategoryDetails();
            try
            {
                _lstCategory = _categoryDetail.viewActiveCategory(intCategoryID);
            }
            catch (Exception ex)
            {

                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return _lstCategory;

        }

        [HttpGet("GetCategoryCounts")]
        public IActionResult Get()
        {
            List<Category> _lstCategory = new List<Category>();
            clsCategoryDetails _categoryCategoryDetails = new clsCategoryDetails();

            string strJsonResponse = null;
            try
            {
                _lstCategory = _categoryCategoryDetails.viewCategoryListCount();
                strJsonResponse = clsHelper.RemoveEmptyProps(_lstCategory);
            }
            catch (Exception ex)
            {

                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return Ok(strJsonResponse);
        }
        #endregion

        #region subCategories

        // Sub Categories
        [HttpPost("addSubCategory")]
        public ActionResult insertSubCategory(string subCatergoryName, int intCategoryID, string strCreatedBy)
        {
            clsSubCategoryDetails _categorySubCategoryDetails = new clsSubCategoryDetails();
            bool blnUserFlag = false;
            string strStatus = "Add SubCategory Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _categorySubCategoryDetails.insertSubCategory(subCatergoryName, intCategoryID, strCreatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "SubCategory Added.";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpPost("updateSubCategory")]
        public ActionResult updateSubCategory(int subCategoryID, string subCatergoryName, int intCategoryID, string strUpdatedBy)
        {
            clsSubCategoryDetails _categorySubCategoryDetails = new clsSubCategoryDetails();
            bool blnUserFlag = false;
            string strStatus = "SubCategory Update Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _categorySubCategoryDetails.updateSubCategory(subCategoryID, subCatergoryName, intCategoryID, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "SubCategory Update Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpDelete("deleteSubCategory")]
        public ActionResult DeleteSubCategory(int subCategoryID, string strUpdatedBy)
        {
            clsSubCategoryDetails _categorySubCategoryDetails = new clsSubCategoryDetails();
            bool blnUserFlag = false;
            string strStatus = "SubCategory Delete Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _categorySubCategoryDetails.deleteSubCategory(subCategoryID, strUpdatedBy);
                if (blnUserFlag == true)
                {
                    strStatus = "SubCategory Deleted Success.";
                }

            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }

        [HttpGet("GetActiveListofSubCategory")]
        public List<subCategory> GetActiveSubCategory(int intCategoryID)
        {
            List<subCategory> _lstSubCategory = new List<subCategory>();
            clsSubCategoryDetails _categorySubCategoryDetails = new clsSubCategoryDetails();
            try
            {
                _lstSubCategory = _categorySubCategoryDetails.viewActiveSubCategory(intCategoryID);
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return _lstSubCategory;

        }
        #endregion
    }
}
