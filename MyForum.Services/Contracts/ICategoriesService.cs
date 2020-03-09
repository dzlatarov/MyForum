using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Services.Contracts
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetAll();
    }
}