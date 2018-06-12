using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.DTOs;
using WorkIt.Core.DTOs;
using WorkIt.Core.DTOs.Project;
using WorkIt.Core.Entities;

namespace Core.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CreateProjectDto, Project>()
                .ForMember(p => p.IsOpenToJoin, opt => opt.MapFrom(entity => !entity.RequiresInvitation))
                .ForMember(p => p.IsPubliclyVisible, opt => opt.MapFrom(entity => entity.PubliclyVisible));

            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, RecentlyUpdatedProjectDto>().ReverseMap();

            CreateMap<Project, ProjectDetailsDto>()
                .ForMember(p => p.CreatedBy, opt => opt.MapFrom(projectEntity => projectEntity.CreatedBy.Firstname));

            CreateMap<Project, DetailedProjectListEntryDto>()
                .ForMember(p => p.CreatedBy, opt => opt.MapFrom(projectEntity => projectEntity.CreatedBy.Firstname));

            CreateMap<ProjectThread, ProjectThreadDto>().ReverseMap();

            CreateMap<CreateThreadEntryDto, ThreadEntry>();
            CreateMap<ThreadEntry, ThreadEntryDto>().ReverseMap();

            CreateMap<AddEntryReactionDto, ThreadEntryReaction>();
        }
    }
}
