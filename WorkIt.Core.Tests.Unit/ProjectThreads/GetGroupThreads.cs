using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Core.DataAccess;
using Xunit;

namespace Core.Tests.ProjectThreads
{
    public class GetGroupThreads
    {

        private IEnumerable<ProjectThreadDto> groupThreadDtos;
        private IQueryable<ProjectThread> groupThreads;

        private Mock<IMapper> mapperMock;

        private static int PAGE_SIZE = 2;
        private static int GROUP_ID = 1;

        private readonly Mock<AppDbContext> mockContext;
        private readonly Mock<DbSet<ProjectThread>> mockSet;

        public GetGroupThreads()
        {
            mapperMock = new Mock<IMapper>();

            var groupIds = new List<int>() { 1, 2, 3, 4, 5 };
            groupThreads = groupIds.Select(id => new ProjectThread() { Id = id }).AsQueryable();
            groupThreadDtos = groupIds.Select(id => new ProjectThreadDto() { Id = id });

            mockSet = DbContextQueryableHelper<ProjectThread>.GetMockedDbSet(groupThreads);

            mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Threads).Returns(mockSet.Object);

            mapperMock.Setup(mapper => mapper.Map<IEnumerable<ProjectThreadDto>>(It.IsAny<IEnumerable<ProjectThread>>()))
                      .Returns(groupThreadDtos.Take(PAGE_SIZE));

        }

        // Tester at dersom negativt sidetall blir gitt, vil den defaulte til å
        // spørre repositoriet etter første side.

        [Fact]
        public void FetchingGroupsWithNegativePageNumber_AsksRepositoryForFirstPage()
        {
            int pageNumber = -100;

            var groupThreadService = new ProjectThreadService(mockContext.Object, mapperMock.Object);
            var pagedResults = groupThreadService.GetPagedByProjectId(GROUP_ID, pageNumber, PAGE_SIZE);

            Assert.Equal(PAGE_SIZE, pagedResults.Count());
            Assert.Equal(1, pagedResults.ElementAt(0).Id);
            Assert.Equal(2, pagedResults.ElementAt(1).Id);
        }

        // Tester at at dersom negativt sidestørrelse blir gitt, vil den defaulte videre
        // til 0, og hente ut tom liste fra repository.

        [Fact]
        public void FetchingGroupsWithNegativePageSize_AsksRepositoryForNoEntries()
        {
            int pageNumber = 1;
            int inputPageSize = -100;

            var groupThreadService = new ProjectThreadService(mockContext.Object, mapperMock.Object);
            var results = groupThreadService.GetPagedByProjectId(GROUP_ID, pageNumber, inputPageSize);

            Assert.Empty(results);

        }
    }
}
