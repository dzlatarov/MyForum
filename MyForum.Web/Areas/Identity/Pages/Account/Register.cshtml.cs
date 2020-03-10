using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MyForum.Domain;
using MyForum.Domain.Enums;
using MyForum.Infrastructure;

namespace MyForum.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;        

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }       

        public class InputModel
        {
            [Required]
            [StringLength(20, ErrorMessage = GlobalConstants.LengthError, 
                MinimumLength = 5)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
                MinimumLength = 3)]
            [Display(Name ="First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(20, ErrorMessage = GlobalConstants.LengthError,
                MinimumLength = 3)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            public Gender Gender { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = GlobalConstants.LengthError, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = GlobalConstants.ConfirmPasswordEr)]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Day of birth")]
            public DateTime DayOfBirth { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;           
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content(GlobalConstants.LoginPath);
            
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Username,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    DateOfBirth = DateTime.Parse(Input.DayOfBirth.ToString("dd-mm-yyyy", CultureInfo.InvariantCulture)),
                    Gender = Input.Gender,
                    Email = Input.Email             
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                   this._logger.LogInformation("User created a new account with password.");

                    if(this._userManager.Users.Count() == 1)
                    {
                       await this._userManager.AddToRoleAsync(user, GlobalConstants.AdminRole);
                    }
                    else
                    {
                        await this._userManager.AddToRoleAsync(user, GlobalConstants.UserRole);
                    }

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}