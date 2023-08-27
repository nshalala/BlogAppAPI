﻿using AutoMapper;
using BlogApp.Business.DTOs.UserDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Profiles;

public class AppUserMappingProfile:Profile
{
    public AppUserMappingProfile()
    {
        CreateMap<RegisterDto, AppUser>();
    }
}