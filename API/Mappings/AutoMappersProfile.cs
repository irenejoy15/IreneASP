using System;
using API.Models.Domain;
using API.Models.DTO;
using AutoMapper;

namespace API.Mappings;

public class AutoMappersProfile:Profile
{
    public AutoMappersProfile()
    {
        // SUBJECTS
        CreateMap<Subject, CreateSubjectDto>().ReverseMap();
        CreateMap<Subject, SubjectDto>().ReverseMap();
        CreateMap<Subject, UpdateSubjectDto>().ReverseMap();
    }
}
