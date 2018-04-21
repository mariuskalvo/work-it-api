using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.DTOs;
using Core.Exceptions;
using Core.Services;
using Moq;
using Xunit;

namespace Core.Tests.Group
{
    public class CreateGroupTests
    {
        [Fact]
        public async void GroupWithNullOrEmptyNameThrowsException()
        {
            var mapper = new Mock<IMapper>();
            var groupService = new GroupService(mapper.Object);

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
    }
}
