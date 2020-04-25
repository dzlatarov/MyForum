using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services.Contracts
{
    public interface INewsService
    {
        Task Create(string name, string content, string imageUrl, string creatorId);

        IQueryable<News> GetAll();

        News GetNewsById(string newsId);

        Task Edit(string newsId, string name, string content, string imageUrl);

        Task Delete(string newsId);
    }
}
