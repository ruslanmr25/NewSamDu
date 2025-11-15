using System;
using AutoMapper;
using NewSamDU.Application.DTOs.AnnouncementDTO;
using NewSamDU.Application.DTOs.MenuDTO;
using NewSamDU.Application.DTOs.NewsDTOs;
using NewSamDU.Application.DTOs.PagesDTO;
using NewSamDU.Application.DTOs.SlideDTO;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Application.AutoMappers;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<CreateNewsDTO, News>();

        CreateMap<UpdateNewsDTO, News>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateSlideDTO, Slide>();

        CreateMap<UpdateSlideDTO, Slide>()
            .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));

        CreateMap<CreateAnnouncementDto, Announcement>();

        CreateMap<UpdateAnnouncementDTO, Announcement>()
            .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));

        CreateMap<CreatePageDTO, Page>();

        CreateMap<UpdatePageDTO, Page>()
            .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));

        CreateMap<CreateMenuDTO, Menu>();

        CreateMap<UpdateMenuDTO, Menu>()
            .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));
    }
}
