using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Core.Tests.Accounts
{
    public class CreateAccountTests
    {
        private readonly UserManager<ApplicationUser> userManagerMock;


        private static readonly string EMAIL = "Email@email.com";
        private readonly ApplicationUser VALID_APPLICATION_USER = new ApplicationUser()
        {
            Email = EMAIL
        };

        private readonly Mock<IdentityResult>  INVALID_IDENTITY_RESULT  = new Mock<IdentityResult>();

        public CreateAccountTests()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new UserManager<ApplicationUser>(userStore.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task CreatingAccountWithNonMatchingPassword_ReturnsValidationResponseWithError()
        {
           var accountService = new AccountService(userManagerMock, null);
            var invalidCreateAccountDto = new CreateAccountDto()
            {
                Email = EMAIL,
                Password = "PASS1",
                RepeatPassword = "PASS2"
            };

            var validationResult = await accountService.CreateAccount(invalidCreateAccountDto);
            Assert.False(validationResult.Success);
            Assert.NotEmpty(validationResult.Errors);
        }
    }
}
