using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using WorkIt.Infrastructure.DataAccess;
using System.Threading.Tasks;
using WorkIt.Core.Interfaces.Repositories;

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

        public CreateGroupThreadTests()
        {
            _threadRepoMock = new Mock<IProjectThreadRepository>();
        }

        [Fact]
        public async Task ThreadWithNullOrEmptyName_ThrowsException()
        {
            var mapper = new Mock<IMapper>();

            var threadService = new ProjectThreadService(_threadRepoMock.Object, mapper.Object);

            var invalidGroupWithEmptyTitle = new CreateProjectThreadDto()
            {
                Title = ""
            };

            var creationResult = await threadService.Create(invalidGroupWithEmptyTitle, string.Empty);
            Assert.Null(creationResult);
        }

        [Fact]
        public async Task ThreadHasValidField_ThredIsPersisted()
        {
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(mapper => mapper.Map<ProjectThread>(It.IsAny<CreateProjectThreadDto>()))
                        .Returns(VALID_THREAD);

            mockedMapper.Setup(mapper => mapper.Map<ProjectThreadDto>(It.IsAny<ProjectThread>()))
                        .Returns(new ProjectThreadDto()
                        {
                            Title = VALID_TITLE
                        });

            var mockContext = new Mock<AppDbContext>();
            var mockSet = new Mock<DbSet<ProjectThread>>();
            mockContext.Setup(m => m.Threads).Returns(mockSet.Object);


            var groupService = new ProjectThreadService(_threadRepoMock.Object, mockedMapper.Object);

            var validThread = new CreateProjectThreadDto()
            {
                Title = VALID_TITLE
            };

            var created = await groupService.Create(validThread, string.Empty);

            _threadRepoMock.Verify(r => r.Create(It.IsAny<ProjectThread>()), Times.Once);
        }
    }
}
