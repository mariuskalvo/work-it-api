using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.DTOs;
using Core.DTOS;
using Core.Entities;

namespace Core.AutoMapper
{
    class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CreateGroupDto, Group>();
            CreateMap<Group, GroupDto>().ReverseMap();

            CreateMap<CreateGroupThreadDto, GroupThread>();
            CreateMap<GroupThread, GroupThreadDto>().ReverseMap();

            CreateMap<CreateThreadEntryDto, ThreadEntry>();
            CreateMap<ThreadEntry, ThreadEntryDto>().ReverseMap();
        }
    }
}
