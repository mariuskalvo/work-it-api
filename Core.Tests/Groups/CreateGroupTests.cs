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

namespace Core.Tests.Groups
{
    public class CreateGroupTests
    {
        private static readonly string VALID_TITLE = "Valid title";
        private static readonly Group VALID_GROUP = new Group()
        {
            Id = 1,
            Title = VALID_TITLE,
            CreatedAt = new DateTime(1990, 6, 30)
        };

        [Fact]
        public void GroupWithNullOrEmptyName_ThrowsException()
        {
            var mapper = new Mock<IMapper>();

            var mockSet = new Mock<DbSet<Group>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Groups).Returns(mockSet.Object);

            var groupService = new GroupService(mockContext.Object, mapper.Object);

            var invalidGroupWithEmptyTitle = new CreateGroupDto()
            {
                Title = ""
            };

            var invalidGroupWithNullTitle = new CreateGroupDto()
            {
                Title = null
            };

            Assert.Throws<InvalidModelStateException>(() => groupService.Create(invalidGroupWithEmptyTitle));
            Assert.Throws<InvalidModelStateException>(() => groupService.Create(invalidGroupWithNullTitle));
        }

        [Fact]
        public void GroupHasValidFields_GroupIsPersisted()
        {
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(mapper => mapper.Map<Group>(It.IsAny<CreateGroupDto>()))
                        .Returns(VALID_GROUP);

            mockedMapper.Setup(mapper => mapper.Map<GroupDto>(It.IsAny<Group>()))
                        .Returns(new GroupDto() {
                            Title = VALID_TITLE
                        });

            var mockSet = new Mock<DbSet<Group>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Groups).Returns(mockSet.Object);

            var groupService = new GroupService(mockContext.Object, mockedMapper.Object);

            var validGroup = new CreateGroupDto()
            {
                Title = VALID_TITLE
            };

            groupService.Create(validGroup);

            mockSet.Verify(m => m.Add(It.IsAny<Group>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
