using MyForum.Domain;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyForum.Services
{
    public class ThreadsService : IThreadsService
    {
        private readonly MyForumDbContext db;
        private readonly IUsersService usersService;

        public ThreadsService(MyForumDbContext db, IUsersService usersService)
        {
            this.db = db;
            this.usersService = usersService;
        }

        public IQueryable<Thread> All()
        {
            var allTreads = this.db.Threads;
            return allTreads;
        }

        public void Create(string content, string authorId)
        {
            var thread = new Thread
            {
                Id = Guid.NewGuid().ToString(),
                Content = content,
                ThreadCreatorId = authorId
            };

            var author = this.usersService.GetUserById(authorId);

            author.Threads.Add(thread);            
            this.db.SaveChanges();
        }

        public Thread GetThreadById(string id)
        {
            var thread = this.db.Threads.Where(t => t.Id == id).FirstOrDefault();
            return thread;
        }
    }
}