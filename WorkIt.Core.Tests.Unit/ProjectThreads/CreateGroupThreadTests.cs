using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.DTOs;
using Core.Exceptions;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using WorkIt.Infrastructure.DataAccess;
using System.Threading.Tasks;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Core.Entities;
using WorkIt.Core.Services.Interfaces;
using WorkIt.Core.Constants;

namespace Core.Tests.ProjectThreads
{
    public class CreateGroupThreadTests
    {
        private static string VALID_TITLE = "Valid title";
        private static long VALID_ID = 1;
        private static ProjectThread VALID_THREAD = new ProjectThread()
        {
            ProjectId = VALID_ID,
            Title = VALID_TITLE
        };

        private readonly Mock<IProjectThreadRepository> _threadRepoMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserInfoRepository> _userInfoRepoMock;
        private readonly Mock<IProjectMembershipRepository> _projectMembershipRepoMock;
        private readonly Mock<IMapper> _mapperMock;


        public CreateGroupThreadTests()
        {
            _threadRepoMock = new Mock<IProjectThreadRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _userInfoRepoMock = new Mock<IUserInfoRepository>();
            _projectMembershipRepoMock = new Mock<IProjectMembershipRepository>();
            _mapperMock = new Mock<IMapper>();

        }

        [Fact]
        public async Task ThreadWithNullOrEmptyName_ReturnServiceStatusWithBadRequest()
        {

            var threadService = new ProjectThreadService(
                _threadRepoMock.Object,
                _projectMembershipRepoMock.Object,
                _userInfoRepoMock.Object,
                _projectRepositoryMock.Object,
                _mapperMock.Object);

            var invalidGroupWithEmptyTitle = new CreateProjectThreadDto()
            {
                Title = ""
            };

            var creationResult = await threadService.Create(invalidGroupWithEmptyTitle, string.Empty);
            Assert.Equal(ServiceStatus.BadRequest, creationResult.Status);
        }

        [Fact]
        public async Task ThreadHasValidField_ThredIsPersisted()
        {
            _mapperMock.Setup(mapper => mapper.Map<ProjectThread>(It.IsAny<CreateProjectThreadDto>()))
                .Returns(VALID_THREAD);

            _mapperMock.Setup(mapper => mapper.Map<ProjectThreadDto>(It.IsAny<ProjectThread>()))
                .Returns(new ProjectThreadDto() { Title = VALID_TITLE });

            _userInfoRepoMock.Setup(u => u.GetUserInfoByOpenIdSub(It.IsAny<string>()))
                .ReturnsAsync(new UserInfo() { Id = 1 });

            _projectMembershipRepoMock.Setup(p => p.GetProjectMembership(It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(new ApplicationUserProjectMember() { ProjectId = 1, UserInfoId = 1 });

            var mockContext = new Mock<AppDbContext>();
            var mockSet = new Mock<DbSet<ProjectThread>>();
            mockContext.Setup(m => m.Threads).Returns(mockSet.Object);


            var threadService = new ProjectThreadService(
                _threadRepoMock.Object,
                _projectMembershipRepoMock.Object,
                _userInfoRepoMock.Object,
                _projectRepositoryMock.Object,
                _mapperMock.Object);

            var validThread = new CreateProjectThreadDto()
            {
                Title = VALID_TITLE
            };

            var created = await threadService.Create(validThread, string.Empty);

            _threadRepoMock.Verify(r => r.Create(It.IsAny<ProjectThread>()), Times.Once);
        }
    }
}
