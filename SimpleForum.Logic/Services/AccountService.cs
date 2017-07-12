using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SimpleForum.Data.Contracts.Repositories;
using SimpleForum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleForum.Core;
using SimpleForum.Logic.DTO.User;
using SimpleForum.Logic.Identity;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.Infrastructure;
using SimpleForum.Data.Contracts;

namespace SimpleForum.Logic.Services
{
    public class AccountService : IAccountService
    {
        public const string UserRole = "User";

        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager userManager;
        private readonly RoleManager roleManager;
        private readonly SignInManager signInManager;

        public AccountService(IUnitOfWork unitOfWork, IAuthenticationManager authenticationManager)
        {
            this.unitOfWork = unitOfWork;
            userManager = new UserManager(unitOfWork.Context);
            roleManager = new RoleManager(unitOfWork.Context);
            signInManager = new SignInManager(userManager, authenticationManager);

            InitializeRoles(UserRole);
        }

        public ServiceMessage RegisterUser(UserRegisterDTO user)
        {
            List<string> errors = new List<string>();
            bool succeeded = Validate(user, errors);

            if (succeeded)
            {
                try
                {
                    ApplicationUser appUser = new ApplicationUser
                    {
                        UserName = user.Login,
                        Email = user.Email,
                    };

                    succeeded = Register(appUser, user.Password, errors, UserRole);
                    if (succeeded)
                    {
                        UserEntity userEntity = new UserEntity
                        {
                            Id = appUser.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        };

                        unitOfWork.Users.Add(userEntity);
                        unitOfWork.Commit();
                    }
                }
                catch (Exception ex)
                {
                    succeeded = false;
                    ExceptionMessageBuilder.FillErrors(ex, errors);
                }
            }

            return new ServiceMessage
            {
                Succeeded = succeeded,
                Errors = errors
            };
        }

        public ServiceMessage SignIn(string login, string password)
        {
            List<string> errors = new List<string>();
            bool succeeded = CanSignIn(login, errors);

            if (succeeded)
            {
                signInManager.AuthenticationManager.SignOut();

                SignInStatus status = signInManager.PasswordSignIn(login, password, true, false);
                if (status != SignInStatus.Success)
                {
                    succeeded = false;
                    errors.Add("Invalid login or password");
                }
            }

            return new ServiceMessage
            {
                Succeeded = succeeded,
                Errors = errors
            };
        }

        public void SignOut()
        {
            signInManager.AuthenticationManager.SignOut();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private void InitializeRoles(params string[] roles)
        {
            foreach (string role in roles.Where(role => !roleManager.RoleExists(role)))
            {
                roleManager.Create(new ApplicationRole { Name = role });
            }
        }

        private bool Validate(UserRegisterDTO user, ICollection<string> errors)
        {
            bool validated = true;

            if (String.IsNullOrEmpty(user.Email))
            {
                validated = false;
                errors.Add("Incorrect email");
            }
            if (String.IsNullOrEmpty(user.Login))
            {
                validated = false;
                errors.Add("Incorrect login");
            }
            if (String.IsNullOrEmpty(user.Password))
            {
                validated = false;
                errors.Add("Password required");
            }
            if (String.IsNullOrEmpty(user.FirstName))
            {
                validated = false;
                errors.Add("First name required");
            }
            if (String.IsNullOrEmpty(user.LastName))
            {
                validated = false;
                errors.Add("Last name required");
            }
            if (validated && !ValidateCredentials(user.Email, user.Login, errors))
            {
                validated = false;
            }

            return validated;
        }

        private bool ValidateCredentials(string email, string login, ICollection<string> errors)
        {
            bool validated = true;

            try
            {
                if (userManager.FindByEmail(email) != null)
                {
                    validated = false;
                    errors.Add("Such email already exists");
                }
                if (userManager.FindByName(login) != null)
                {
                    validated = false;
                    errors.Add("Such login already exists");
                }
            }
            catch (Exception ex)
            {
                ExceptionMessageBuilder.FillErrors(ex, errors);
                validated = false;
            }

            return validated;
        }

        private bool Register(ApplicationUser applicationUser, string password, ICollection<string> errors, string role)
        {
            var result = userManager.Create(applicationUser, password);
            bool succeeded = result.Succeeded;

            if (succeeded)
            {
                userManager.AddToRole(applicationUser.Id, role);
            }
            else
            {
                foreach (string error in result.Errors)
                {
                    errors.Add(error);
                }
            }

            return succeeded;
        }

        private bool CanSignIn(string login, ICollection<string> errors)
        {
            bool canSignIn = true;

            try
            {
                ApplicationUser appUser = userManager.FindByName(login);
                if (appUser != null)
                {
                    if (appUser.User.IsDeleted)
                    {
                        canSignIn = false;
                        errors.Add("Account deleted");
                    }
                }
                else
                {
                    canSignIn = false;
                }
            }
            catch (Exception ex)
            {
                canSignIn = false;
                ExceptionMessageBuilder.FillErrors(ex, errors);
            }

            return canSignIn;
        }
    }
}
