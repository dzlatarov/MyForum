using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyForum.Services.Contracts
{
    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();

        Category GetCategoryById(string id);
    }
}