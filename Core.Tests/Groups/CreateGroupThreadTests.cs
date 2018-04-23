using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.DTOs;
using Core.DTOS;
using Core.Entities;
using Core.Exceptions;
using Core.RepositoryInterfaces;
using Core.Services;
using Moq;
using Xunit;

namespace Core.Tests.Groups
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
            var threadRepository = new Mock<IGroupThreadRepository>();
            var threadService = new GroupThreadService(threadRepository.Object, mapper.Object);

            var invalidGroupWithEmptyTitle = new CreateGroupThreadDto()
            {
                Title = ""
            };

            var invalidGroupWithNullTitle = new CreateGroupThreadDto()
            {
                Title = null
            };

            await Assert.ThrowsAsync<InvalidModelStateException>(() => threadService.Create(invalidGroupWithEmptyTitle));
            await Assert.ThrowsAsync<InvalidModelStateException>(() => threadService.Create(invalidGroupWithNullTitle));
        }

        [Fact]
        public async void ThreadHasValidField_ThredIsPersisted()
        {
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(mapper => mapper.Map<GroupThread>(It.IsAny<CreateGroupThreadDto>()))
                        .Returns(VALID_THREAD);

            mockedMapper.Setup(mapper => mapper.Map<GroupThreadDto>(It.IsAny<GroupThread>()))
                        .Returns(new GroupThreadDto()
                        {
                            Title = VALID_TITLE
                        });

            var threadRepository = new Mock<IGroupThreadRepository>();
            threadRepository.Setup(gr => gr.Add(It.IsAny<GroupThread>())).ReturnsAsync(true);

            var groupService = new GroupThreadService(threadRepository.Object, mockedMapper.Object);

            var validThread = new CreateGroupThreadDto()
            {
                Title = VALID_TITLE
            };

            var createdGroupDto = await groupService.Create(validThread);
            threadRepository.Verify(gr => gr.Add(It.IsAny<GroupThread>()));
        }
    }
}
