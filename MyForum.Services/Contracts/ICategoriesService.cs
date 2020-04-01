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
    }
}