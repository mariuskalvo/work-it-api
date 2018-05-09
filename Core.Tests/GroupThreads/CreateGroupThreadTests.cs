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

namespace Core.Tests.GroupThreads
{
    public class CreateGroupThreadTests
    {
        private static string VALID_TITLE = "Valid title";
        private static long VALID_ID = 1;
        private static GroupThread VALID_THREAD = new GroupThread()
        {
            GroupId = VALID_ID,
            Title = VALID_TITLE
        };

        [Fact]
        public async void ThreadWithNullOrEmptyName_ThrowsException()
        {
            var mapper = new Mock<IMapper>();

            var mockContext = new Mock<AppDbContext>();
            var mockSet = new Mock<DbSet<GroupThread>>();

            var threadService = new GroupThreadService(mockContext.Object, mapper.Object);

            var invalidGroupWithEmptyTitle = new CreateGroupThreadDto()
            {
                Title = ""
            };

            var invalidGroupWithNullTitle = new CreateGroupThreadDto()
            {
                Title = null
            };

            Assert.Throws<InvalidModelStateException>(() => threadService.Create(invalidGroupWithEmptyTitle, string.Empty));
            Assert.Throws<InvalidModelStateException>(() => threadService.Create(invalidGroupWithNullTitle, string.Empty));
        }

        [Fact]
        public void ThreadHasValidField_ThredIsPersisted()
        {
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(mapper => mapper.Map<GroupThread>(It.IsAny<CreateGroupThreadDto>()))
                        .Returns(VALID_THREAD);

            mockedMapper.Setup(mapper => mapper.Map<GroupThreadDto>(It.IsAny<GroupThread>()))
                        .Returns(new GroupThreadDto()
                        {
                            Title = VALID_TITLE
                        });

            var mockContext = new Mock<AppDbContext>();
            var mockSet = new Mock<DbSet<GroupThread>>();
            mockContext.Setup(m => m.Threads).Returns(mockSet.Object);


            var groupService = new GroupThreadService(mockContext.Object, mockedMapper.Object);

            var validThread = new CreateGroupThreadDto()
            {
                Title = VALID_TITLE
            };

            groupService.Create(validThread, string.Empty);

            mockContext.Verify(m => m.SaveChanges(), Times.Once);
            mockSet.Verify(m => m.Add(It.IsAny<GroupThread>()), Times.Once);
        }
    }
}
