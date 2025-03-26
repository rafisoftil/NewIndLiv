using IndiaLivings_Web_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Repositories
{
    public interface IProductRepository
    {
        public List<CategoryModel> GetCategoriesCount();
        public List<SubCategoryModel> GetSubCategories(int CategoryId);

        public List<AdConitionTypeModel> GetAdConditions();

        public bool InsertProduct(ProductModel product);
    }
}
