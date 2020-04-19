using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyForum.Infrastructure.Exceptions;
using MyForum.Services;
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
        private readonly IUsersService usersService;

        public CommentsController(ICommentsService commentsService, IThreadsService threadsService, IUsersService usersService)
        {
            this.commentsService = commentsService;
            this.threadsService = threadsService;
            this.usersService = usersService;
        }

        [Authorize]
        [Route("/Comments/Reply/{threadId}")]
        public IActionResult Reply(string threadId)
        {
            var user = this.usersService.GetUserById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user.IsDeactivate == true)
            {
                throw new DeactivateUserException();
            }

            var thread = this.threadsService.GetThreadById(threadId).Result;

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

            var thread = this.threadsService.GetThreadById(model.ThreadId).Result;

            this.commentsService.CreateComment(model.Content, model.ThreadId, creatorId);

            return this.Redirect($"/Categories/AllThreads/{thread.CategoryId}");
        }

        [Authorize]
        [Route("/Comments/All/{threadId}")]
        public IActionResult All(string threadId)
        {
            var user = this.usersService.GetUserById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user.IsDeactivate == true)
            {
                throw new DeactivateUserException();
            }

            var thread = this.threadsService.GetThreadById(threadId).Result;

            if (thread == null)
            {
                return NotFound();
            }

            var threadCreator = this.usersService.GetUserById(thread.ThreadCreatorId).UserName;

            var allComments = this.commentsService.GetAllComments()
                .Select(CommentsInfoViewModel.FromComment)
                .ToList();

            var model = new CommentsAllViewModel
            {
                ThreadContent = thread.Content,
                CreatorName = threadCreator,
                CreatedOn = thread.CreatedOn,
                ModifiedOn = thread.ModifiedOn,
                Comments = allComments
            };

            return this.View(model);
        }

        [Authorize]
        [Route("/Comments/Delete/{commentId}")]
        public async Task<IActionResult> Delete(string commentId)
        {
            var user = this.usersService.GetUserById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user.IsDeactivate == true)
            {
                throw new DeactivateUserException();
            }

            var comment = this.commentsService.GetCommentById(commentId).Result;

            if (comment == null)
            {
                return NotFound();
            }

            await this.commentsService.Delete(commentId);

            return this.RedirectToAction(nameof(All), new { threadId = comment.ThreadId });
        }

        [Authorize]
        [Route("/Comments/Edit/{commentId}")]
        public IActionResult Edit(string commentId)
        {
            var user = this.usersService.GetUserById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user.IsDeactivate == true)
            {
                throw new DeactivateUserException();
            }

            var comment = this.commentsService.GetCommentById(commentId).Result;

            if (comment == null)
            {
                return NotFound();
            }


            var model = new CommentsEditViewModel()
            {
                Id = comment.Id,
                Content = comment.Content,
                ThreadId = comment.ThreadId
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [Route("/Comments/Edit/{commentId}")]
        public async Task<IActionResult> Edit(string commentId, CommentsEditViewModel model)
        {
            var comment = this.commentsService.GetCommentById(commentId).Result;

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var modifiedOn = DateTime.UtcNow;
            await this.commentsService.Edit(comment.Id, model.Content, modifiedOn);

            return this.RedirectToAction(nameof(All), new { threadId = comment.ThreadId });
        }

        [Authorize]
        [Route("/Comments/Quote/{commentId}")]
        public IActionResult Quote(string commentId)
        {
            var comment = this.commentsService.GetCommentById(commentId).Result;

            if (comment == null)
            {
                return NotFound();
            }

            var user = this.usersService.GetUserById(comment.CommentCreatorId);

            var model = new CommentsQuoteViewModel
            {
                QuotedCommentId = comment.Id,
                QuotedCommentContent = comment.Content,
                QuotedCommentCreator = user.UserName,
                QuotedCommentCreatedOn = comment.CreatedOn
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [Route("/Comments/Quote/{commentId}")]
        public IActionResult Quote(string commentId, CommentsQuoteViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var creatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newCommentUsername = this.usersService.GetUserById(creatorId).UserName;
            var comment = this.commentsService.GetCommentById(commentId).Result;
            var quotedCommentUsername = this.usersService.GetUserById(comment.CommentCreatorId).UserName;
            //var content = "On " + comment.CreatedOn.Date + " at " + comment.CreatedOn.Hour + ":" + comment.CreatedOn.Minute + ", " + quotedCommentUsername + " wrote:" + "\n" + comment.Content + "\n" + "And " + newCommentUsername + " said:" + "\n" + model.NewCommentContent;
            var content = model.NewCommentContent;
            var quote = comment.Content;


            this.commentsService.CreateCommentQuote(content, comment.ThreadId, creatorId, quote);

            return this.RedirectToAction(nameof(All), new { threadId = comment.ThreadId });
        }
    }
}