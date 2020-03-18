using MyForum.Domain;
using MyForum.Infrastructure.Exceptions;
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

        public void Create(string title, string content, string authorId, string categoryId)
        {
            var errorMessage = "";

            try
            {
                var thread = new Thread
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = title,
                    Content = content,
                    CreatedOn = DateTime.UtcNow,
                    ThreadCreatorId = authorId,
                    CategoryId = categoryId
                };


                this.db.Threads.Add(thread);
                this.db.SaveChanges();
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                throw new CreateThreadException();
            }            
        }

        public void Delete(string threadId)
        {
            var thread = this.db.Threads.FirstOrDefault(t => t.Id == threadId);
            this.db.Threads.Remove(thread);
            this.db.SaveChanges();
        }

        public string Edit(string threadId, string title, string content, DateTime modifiedOn)
        {
            var threadFromDb = this.db.Threads.FirstOrDefault(t => t.Id == threadId);

            threadFromDb.ModifiedOn = modifiedOn;
            threadFromDb.Title = title;
            threadFromDb.Content = content;

            this.db.Threads.Update(threadFromDb);
            this.db.SaveChanges();

            return threadFromDb.CategoryId;
        }

        public Thread GetThreadById(string id)
        {
            var thread = this.db.Threads.Where(t => t.Id == id).FirstOrDefault();
            return thread;
        }
    }
}