using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.RepositoryInterfaces;
using Core.Services;
using Moq;
using Xunit;

namespace Core.Tests.GroupThreads
{
    public class GetGroupThreads
    {

        private IEnumerable<GroupThreadDto> groupThreadDtos;
        private IEnumerable<GroupThread> groupThreads;

        private Mock<IGroupThreadRepository> threadRepoMock;
        private Mock<IMapper> mapperMock;

        private static int PAGE_SIZE = 2;
        private static int GROUP_ID = 1;

        public GetGroupThreads()
        {

            threadRepoMock = new Mock<IGroupThreadRepository>();
            mapperMock = new Mock<IMapper>();

            var groupIds = new List<int>() { 1, 2, 3, 4, 5 };
            groupThreads = groupIds.Select(id => new GroupThread() { Id = id });
            groupThreadDtos = groupIds.Select(id => new GroupThreadDto() { Id = id });

            threadRepoMock.Setup(repo => repo.GetByGroupIdWithSkipAndLimit(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(groupThreads.Take(PAGE_SIZE));

            mapperMock.Setup(mapper => mapper.Map<IEnumerable<GroupThreadDto>>(It.IsAny<IEnumerable<GroupThread>>()))
                      .Returns(groupThreadDtos.Take(PAGE_SIZE));
        }

        // Tester at dersom negativt sidetall blir gitt, vil den defaulte til å
        // spørre repositoriet etter første side.

        [Fact]
        public async Task FetchingGroupsWithNegativePageNumber_AsksRepositoryForFirstPage()
        {
            int pageNumber = -100;
            int expectedSkips = 0;

            var groupThreadService = new GroupThreadService(threadRepoMock.Object, mapperMock.Object);
            var pagedResults = await groupThreadService.GetPagedByGroupId(GROUP_ID, pageNumber, PAGE_SIZE);
            threadRepoMock.Verify(repo => repo.GetByGroupIdWithSkipAndLimit(GROUP_ID, PAGE_SIZE, expectedSkips), Times.Once);
        }

        // Tester at at dersom negativt sidestørrelse blir gitt, vil den defaulte videre
        // til 0, og hente ut tom liste fra repository.

        [Fact]
        public async void FetchingGroupsWithNegativePageSize_AsksRepositoryForNoEntries()
        {
            int pageNumber = 1;
            int expectedSkips = 0;

            int inputPageSize = -100;
            int expectedPageSize = 0;

            var groupThreadService = new GroupThreadService(threadRepoMock.Object, mapperMock.Object);
            var pagedResults = await groupThreadService.GetPagedByGroupId(GROUP_ID, pageNumber, inputPageSize);

            threadRepoMock.Verify(repo => repo.GetByGroupIdWithSkipAndLimit(GROUP_ID, expectedPageSize, expectedSkips), Times.Once);
        }
    }
}
