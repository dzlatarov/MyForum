using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Services.Contracts;
using MyForum.Web.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var creatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var thread = this.threadsService.GetThreadById(model.ThreadId);

            this.commentsService.CreateComment(model.Content, model.ThreadId, creatorId);

            return this.Redirect($"/Categories/AllThreads/{thread.CategoryId}");
        }

        [Authorize]
        [Route("/Comments/All/{threadId}")]
        public IActionResult All(string threadId)
        {
            var thread = this.threadsService.GetThreadById(threadId);

            if (thread == null)
            {
                return NotFound();
            }

            var allComments = this.commentsService.GetAllComments()
                .Select(CommentsInfoViewModel.FromComment)
                .ToList();

            var model = new CommentsAllViewModel
            {
                ThreadContent = thread.Content,
                Comments = allComments
            };

            return this.View(model);
        }
    }
}