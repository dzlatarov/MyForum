using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyForum.Services
{
    public class ThreadsServices : IThreadsServices
    {
        private readonly MyForumDbContext db;
        private readonly IUsersService usersService;

        public ThreadsServices(MyForumDbContext db, IUsersService usersService)
        {
            this.db = db;
            this.usersService = usersService;
        }

        public IQueryable<Thread> All()
        {
            var allTreads = this.db.Threads;
            return allTreads;
        }

        public void Create(string name, string authorId)
        {
            var thread = new Thread
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                ThreadCreatorId = authorId
            };

            var author = this.usersService.GetUserById(authorId);

            author.Threads.Add(thread);
            this.db.Threads.Add(thread);
            this.db.SaveChanges();
        }
    }
}