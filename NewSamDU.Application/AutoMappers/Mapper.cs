using System;
using AutoMapper;
using NewSamDU.Application.DTOs.NewsDTOs;
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
    }
}
