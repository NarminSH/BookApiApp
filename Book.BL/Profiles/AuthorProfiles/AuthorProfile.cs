using AutoMapper;
using Book.BL.DTOs.AuthorDtos;
using Book.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BL.Profiles.AuthorProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorCreateDto, Author>();     
            CreateMap<AuthorCreateDto, Author>().ReverseMap();     
        }
    }
}
