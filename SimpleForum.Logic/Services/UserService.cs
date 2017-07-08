using SimpleForum.Data.Contracts;
using SimpleForum.Data.Entities;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.DTO.User;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Register(UserRegisterDTO user)
        {
            string errorMessage = "";
            bool succeeded = Validate(user, ref errorMessage);

            if (succeeded)
            {
                try
                {
                    succeeded = unitOfWork.Accounts.ValidateCredentials(user.Email, user.Login, ref errorMessage);
                    if (succeeded)
                    {
                        ApplicationUser appUser = new ApplicationUser
                        {
                            UserName = user.Login,
                            Email = user.Email,
                        };

                        succeeded = unitOfWork.Accounts.RegisterUser(appUser, user.Password, ref errorMessage);
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
                }
                catch (Exception ex)
                {
                    succeeded = false;
                    errorMessage = ExceptionMessageBuilder.Build(ex);
                }
            }

            return new ServiceMessage
            {
                Succeeded = succeeded,
                ErrorMessage = errorMessage
            };
        }

        private bool Validate(UserRegisterDTO user, ref string errorMessage)
        {
            bool validated = true;

            if (String.IsNullOrEmpty(user.Email))
            {
                validated = false;
                errorMessage = "Incorrect email";
            }
            else if (String.IsNullOrEmpty(user.Login))
            {
                validated = false;
                errorMessage = "Incorrect login";
            }
            else if (String.IsNullOrEmpty(user.Password))
            {
                validated = false;
                errorMessage = "Password required";
            }
            else if (String.IsNullOrEmpty(user.FirstName))
            {
                validated = false;
                errorMessage = "First name required";
            }
            else if (String.IsNullOrEmpty(user.LastName))
            {
                validated = false;
                errorMessage = "Last name required";
            }

            return validated;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
