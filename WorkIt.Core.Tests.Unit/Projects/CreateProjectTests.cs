using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.DTOs;
using Core.DTOS;
using Core.Entities;
using Core.Exceptions;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Threading.Tasks;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Infrastructure.DataAccess;

namespace Core.Tests.Projects
{
    public class CreateProjectTests
    {
        private Mock<IProjectRepository> _projectRepositoryMock;

        private static readonly string VALID_TITLE = "Valid title";
        private static readonly Project VALID_GROUP = new Project()
        {
            Id = 1,
            Title = VALID_TITLE,
            CreatedAt = new DateTime(1990, 6, 30)
        };

        public CreateProjectTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
        }

        [Fact]
        public async Task GroupWithNullOrEmptyName_ReturnsNull()
        {
            var mapper = new Mock<IMapper>();

            var mockContext = new Mock<AppDbContext>();
            var groupService = new ProjectService(_projectRepositoryMock.Object, mapper.Object);

            var invalidGroupWithEmptyTitle = new CreateProjectDto()
            {
                Title = ""
            };

            var result = await groupService.Create(invalidGroupWithEmptyTitle, String.Empty);
            Assert.Null(result);
        }

        [Fact]
        public async Task GroupHasValidFields_GroupIsPersisted()
        {
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(mapper => mapper.Map<Project>(It.IsAny<CreateProjectDto>()))
                        .Returns(VALID_GROUP);

            mockedMapper.Setup(mapper => mapper.Map<ProjectDto>(It.IsAny<Project>()))
                        .Returns(new ProjectDto() {
                            Title = VALID_TITLE
                        });

            var mockSet = new Mock<DbSet<Project>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            var groupService = new ProjectService(_projectRepositoryMock.Object, mockedMapper.Object);

            var validGroup = new CreateProjectDto()
            {
                Title = VALID_TITLE
            };

            var created = await groupService.Create(validGroup, string.Empty);
            _projectRepositoryMock.Verify(p => p.Create(It.IsAny<Project>()), Times.Once);
        }
    }
}
