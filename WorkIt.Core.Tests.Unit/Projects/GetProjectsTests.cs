using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.AutoMapper;
using Core.Services;
using Core.Services.Interfaces;
using Moq;
using WorkIt.Core.Entities;
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

        private readonly IEnumerable<long> _openProjectIds = new List<long>() { 3, 4, 5 };
        private readonly IEnumerable<long> _projectsWithMembershipIds = new List<long>() { 1, 3, 5 };
        private readonly IEnumerable<long> _allProjectIds = new List<long>() { 1, 2, 3, 4, 5, 6 };

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

            Assert.Equal(_projectsWithMembershipIds.OrderBy(p => p), userProjectIds.OrderBy(p => p));
            Assert.Equal(new List<long>() { 2, 4, 6 }.OrderBy(p => p), nonUserProjectIds.OrderBy(p => p));
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

        [Fact]
        public async Task GetProjects_ProjectsWithMembershipAreSortedFirst()
        {
            _projectService = SetupProjectService();
            var response = await _projectService.GetProjects(It.IsAny<string>());

            var projectsWithMembership = response.Data.TakeWhile(p => p.IsUserMember);
            var restOfProjects = response.Data.SkipWhile(p => p.IsUserMember);

            Assert.All(projectsWithMembership, p => Assert.True(p.IsUserMember));
            Assert.All(restOfProjects, p => Assert.False(p.IsUserMember));
        }

        [Fact]
        public async Task GetProjects_OpenProjectsAreSortedSecondly()
        {
            _projectService = SetupProjectService();
            var response = await _projectService.GetProjects(It.IsAny<string>());

            var projectsWithMembership = response.Data.TakeWhile(p => p.IsUserMember);

            var projectsWithoutMembership = response.Data.Except(projectsWithMembership);

            var openProjects = projectsWithoutMembership.TakeWhile(p => p.IsOpenToJoin);
            var restOfProjects = projectsWithoutMembership.SkipWhile(p => p.IsOpenToJoin);

            Assert.All(openProjects, p => Assert.True(p.IsOpenToJoin));
            Assert.All(restOfProjects, p => Assert.False(p.IsOpenToJoin));
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
            return _projectsWithMembershipIds.Select(pId => new Project()
            {
                Id = pId,
                Title = pId.ToString(),
                IsOpenToJoin = _openProjectIds.Contains(pId)
            });
        }

        private IEnumerable<Project> SetupProjects()
        {
            return _allProjectIds.Select(pId => new Project()
            {
                Id = pId,
                Title = pId.ToString(),
                IsOpenToJoin = _openProjectIds.Contains(pId)
            });
        }
        #endregion

    }
}
