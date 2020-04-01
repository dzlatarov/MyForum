using MyForum.Domain;
using MyForum.Infrastructure.Exceptions;
using MyForum.Persistence;
using MyForum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Services
{
    public class ThreadsService : IThreadsService
    {
        private readonly MyForumDbContext db;
        private readonly IUsersService usersService;
        private readonly ICategoriesService categoriesService;

        public ThreadsService(MyForumDbContext db, IUsersService usersService, ICategoriesService categoriesService)
        {
            this.db = db;
            this.usersService = usersService;
            this.categoriesService = categoriesService;
        }

        public IQueryable<Thread> All()
        {
            var allTreads = this.db.Threads;
            return allTreads;
        }

        public async Task Create(string title, string content, string authorId, string categoryId)
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
                await this.db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                throw new CreateThreadException();
            }
        }

        public async Task Delete(string threadId)
        {
            var thread = this.db.Threads.FirstOrDefault(t => t.Id == threadId);
            this.db.Threads.Remove(thread);
            await this.db.SaveChangesAsync();
        }

        public async Task<string> Edit(string threadId, string title, string content, DateTime modifiedOn)
        {
            var threadFromDb = this.db.Threads.FirstOrDefault(t => t.Id == threadId);

            threadFromDb.ModifiedOn = modifiedOn;
            threadFromDb.Title = title;
            threadFromDb.Content = content;

            this.db.Threads.Update(threadFromDb);
            await this.db.SaveChangesAsync();

            return threadFromDb.CategoryId;
        }

        public Thread GetThreadById(string id)
        {
            var thread = this.db.Threads.Where(t => t.Id == id).FirstOrDefault();
            return thread;
        }
    }
}