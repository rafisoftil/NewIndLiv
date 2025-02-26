using IndiaLivingsAPI.Model.ErrorLogs;
using IndiaLivingsAPI.Model.Products;
using Microsoft.AspNetCore.Mvc;

namespace IndiaLivingsAPI.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost("addProduct")]
        public ActionResult insertProduct(clsProduct _clsProduct)
        {
            clsProductDetails _productDetails = new clsProductDetails();
            bool blnUserFlag = false;
            string strStatus = "Add Product Failed. Please check with Admin.";
            try
            {
                blnUserFlag = _productDetails.insertProduct(_clsProduct);
                if (blnUserFlag == true)
                {
                    strStatus = "Product Added.";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }

            return Ok(strStatus);
        }
        [HttpGet("GetAllProductsByOwner")]
        public List<clsProduct> GetAllProducts(int intProductOwner)
        {
            List<clsProduct> _lstProducts = new List<clsProduct>();
            clsProductDetails _productDetails = new clsProductDetails();
            try
            {
                _lstProducts = _productDetails.viewAllProduct(intProductOwner);
            }
            catch (Exception ex)
            {
                clsErrorLog.insertErrorLog(ex.Message, ex.StackTrace.ToString(), ex.Source);
            }
            return _lstProducts;

        }
    }
}
