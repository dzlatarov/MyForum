using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Services.Contracts;
using MyForum.Web.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForum.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly IThreadsService threadsService;

        public CommentsController(ICommentsService commentsService, IThreadsService threadsService)
        {
            this.commentsService = commentsService;
            this.threadsService = threadsService;
        }

        [Authorize]
        [Route("/Comments/Reply/{threadId}")]
        public IActionResult Reply(string threadId)
        {
            var thread = this.threadsService.GetThreadById(threadId);

            if (thread == null)
            {
                return NotFound();
            }

            var model = new CommentsCreateViewModel()
            {
                ThreadId = thread.Id,
                ThreadContent = thread.Content
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [Route("/Comments/Reply/{threadId}")]
        public IActionResult Reply(CommentsCreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var thread = this.threadsService.GetThreadById(model.ThreadId);

            this.commentsService.CreateComment(model.Content, model.ThreadId, model.CreatorId);

            return this.Redirect($"/Categories/AllThreads/{thread.CategoryId}");
        }
    }
}
