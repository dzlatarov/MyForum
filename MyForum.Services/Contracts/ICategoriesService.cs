using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services.Contracts
{
    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();

        Task<Category> GetCategoryById(string id);

        Task Create(string name, string description, string imageUrl);

        Category GetCategoryByName(string name);

        Task Delete(string categoryId);

        Task Edit(string categoryId, string name, string description, string imageUrl);
    }
}