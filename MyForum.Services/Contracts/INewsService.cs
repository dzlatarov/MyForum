using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services.Contracts
{
    public interface INewsService
    {
        Task Create(string name, string content, string imageUrl, string creatorId);
    }
}
