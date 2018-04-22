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
        public async void GroupWithNullOrEmptyName_ThrowsException()
        {
            var mapper = new Mock<IMapper>();
            var groupRepository = new Mock<IGroupRepository>();

            var groupService = new GroupService(groupRepository.Object, mapper.Object);

            var invalidGroupWithEmptyTitle = new CreateGroupDto()
            {
                Title = ""
            };

            var invalidGroupWithNullTitle = new CreateGroupDto()
            {
                Title = null
            };

            await Assert.ThrowsAsync<InvalidModelStateException>(() => groupService.Create(invalidGroupWithEmptyTitle));
            await Assert.ThrowsAsync<InvalidModelStateException>(() => groupService.Create(invalidGroupWithNullTitle));
        }

        [Fact]
        public async void GroupHasValidFields_GroupIsPersisted()
        {
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(mapper => mapper.Map<Group>(It.IsAny<CreateGroupDto>()))
                        .Returns(VALID_GROUP);

            mockedMapper.Setup(mapper => mapper.Map<GroupDto>(It.IsAny<Group>()))
                        .Returns(new GroupDto() {
                            Title = VALID_TITLE
                        });

            var groupRepository = new Mock<IGroupRepository>();
            groupRepository.Setup(gr => gr.Add(It.IsAny<Group>()))
                           .ReturnsAsync(VALID_GROUP);

            var groupService = new GroupService(groupRepository.Object, mockedMapper.Object);

            var validGroup = new CreateGroupDto()
            {
                Title = VALID_TITLE
            };

            var createdGroupDto = await groupService.Create(validGroup);
            groupRepository.Verify(gr => gr.Add(It.IsAny<Group>()));
        }
    }
}
