using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Application.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetById(int id);
        Category PostCategory(Category category);
        void DeleteCategory(int id);

        void UpdateCategory(int id, Category category);
    }
}
