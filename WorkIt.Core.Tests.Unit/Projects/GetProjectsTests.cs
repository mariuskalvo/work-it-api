using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.AutoMapper;
using Core.Entities;
using Core.Services;
using Core.Services.Interfaces;
using Moq;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Core.Services.Interfaces;
using Xunit;

namespace WorkIt.Core.Tests.Unit.Projects
{
    public class GetProjectsTests
    {
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<IProjectMembershipRepository> _projectMembershipRepositoryMock;
        private Mock<IUserService> _userServiceMock;
        private readonly IMapper _mapper;

        private IProjectService _projectService;

        public GetProjectsTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _projectMembershipRepositoryMock = new Mock<IProjectMembershipRepository>();
            _userServiceMock = new Mock<IUserService>();

            var automapperProfile = new DomainProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(automapperProfile));
            _mapper = new Mapper(mapperConfig);

            _projectRepositoryMock
                .Setup(repo => repo.GetMemberProjectsForUser(It.IsAny<string>()))
                .ReturnsAsync(SetupMemberProjects());

            _projectRepositoryMock
                .Setup(repo => repo.GetProjects())
                .ReturnsAsync(SetupProjects());
        }

        [Fact]
        public async Task GetProjects_UserIsMemberOfProjects_IsMemberMarkedAsTrue()
        {
            _projectService = SetupProjectService();
            var response = await _projectService.GetProjects(It.IsAny<string>());
            var projects = response.Data;

            var userProjectIds = projects.Where(p => p.IsUserMember).Select(p => p.Id).ToList();
            var nonUserProjectIds = projects.Where(p => !p.IsUserMember).Select(p => p.Id).ToList();

            Assert.Equal(new List<long>() { 1, 2, 3 }, userProjectIds);
            Assert.Equal(new List<long>() { 4, 5, 6 }, nonUserProjectIds);
        }

        [Fact]
        public async Task GetProjects_UserIsMemberOfProjects_NoDuplicateProjects()
        {
            _projectService = SetupProjectService();
            var response = await _projectService.GetProjects(It.IsAny<string>());
            var projects = response.Data;

            var userProjectIds = projects.Where(p => p.IsUserMember).Select(p => p.Id).ToList();
            var nonUserProjectIds = projects.Where(p => !p.IsUserMember).Select(p => p.Id).ToList();

            Assert.Empty(userProjectIds.Intersect(nonUserProjectIds));
        }

        #region Helpers

        private IProjectService SetupProjectService()
        {
            return new ProjectService(_projectRepositoryMock.Object,
                                      _projectMembershipRepositoryMock.Object,
                                      _userServiceMock.Object,
                                      _mapper);
        }
        private IEnumerable<Project> SetupMemberProjects()
        {
            var projectIds = new List<int>() { 1, 2, 3 };
            return projectIds.Select(pId => new Project() { Id = pId, Title = pId.ToString() });
        }

        private IEnumerable<Project> SetupProjects()
        {
            var projectIds = new List<int>() { 1, 2, 3, 4, 5, 6 };
            return projectIds.Select(pId => new Project() { Id = pId, Title = pId.ToString() });
        }
        #endregion

    }
}
