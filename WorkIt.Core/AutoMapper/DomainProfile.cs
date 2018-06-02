using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.DTOs;
using Core.DTOS;
using Core.Entities;
using WorkIt.Core.DTOs;

namespace Core.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CreateProjectDto, Project>();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, RecentlyUpdatedProjectDto>().ReverseMap();

            CreateMap<CreateProjectThreadDto, ProjectThread>();
            CreateMap<ProjectThread, ProjectThreadDto>().ReverseMap();

            CreateMap<CreateThreadEntryDto, ThreadEntry>();
            CreateMap<ThreadEntry, ThreadEntryDto>().ReverseMap();

            CreateMap<AddEntryReactionDto, ThreadEntryReaction>();
        }
    }
}
