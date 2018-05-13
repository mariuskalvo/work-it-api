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
using Core.DataAccess;
using Xunit;

namespace Core.Tests.Projects
{
    public class CreateGroupTests
    {
        private static readonly string VALID_TITLE = "Valid title";
        private static readonly Project VALID_GROUP = new Project()
        {
            Id = 1,
            Title = VALID_TITLE,
            CreatedAt = new DateTime(1990, 6, 30)
        };

        [Fact]
        public void GroupWithNullOrEmptyName_ThrowsException()
        {
            var mapper = new Mock<IMapper>();

            var mockSet = new Mock<DbSet<Project>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            var groupService = new ProjectService(mockContext.Object, mapper.Object);

            var invalidGroupWithEmptyTitle = new CreateProjectDto()
            {
                Title = ""
            };

            var invalidGroupWithNullTitle = new CreateProjectDto()
            {
                Title = null
            };

            Assert.Throws<InvalidModelStateException>(() => groupService.Create(invalidGroupWithEmptyTitle, String.Empty));
            Assert.Throws<InvalidModelStateException>(() => groupService.Create(invalidGroupWithNullTitle, String.Empty));
        }

        [Fact]
        public void GroupHasValidFields_GroupIsPersisted()
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

            var groupService = new ProjectService(mockContext.Object, mockedMapper.Object);

            var validGroup = new CreateProjectDto()
            {
                Title = VALID_TITLE
            };

            groupService.Create(validGroup, string.Empty);

            mockSet.Verify(m => m.Add(It.IsAny<Project>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
