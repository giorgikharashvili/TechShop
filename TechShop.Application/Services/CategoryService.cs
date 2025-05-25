using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private static List<Category> Categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Personal Computers (PC)",
                Description = "Gaming and Office Personal Computers"
            },
            new Category
            {
                Id = 2,
                Name = "Mouses",
                Description = "Gaming and office Mouses"
            },
            new Category
            {
                Id = 3,
                Name = "Mechanical Keyboards",
                Description = "Gaming and office Keyboards"
            },
        };

        public void DeleteCategory(int id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category != null) Categories.Remove(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return Categories;
        }

        public Category GetById(int id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) throw new KeyNotFoundException($"Category with the {id} was not found");

            return category;
        }

        public Category PostCategory(Category category)
        {
            category.Id = Categories.Count > 0 ? Categories.Max(c => c.Id) + 1 : 1;
            Categories.Add(category);

            return category;
        }

        public void UpdateCategory(int id, Category category)
        {
            var existingCategory = Categories.FirstOrDefault(c => c.Id == id);
            if (existingCategory == null) throw new KeyNotFoundException($"Category with the {id} was not found");
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.Id = category.Id;
        }
    }
}
