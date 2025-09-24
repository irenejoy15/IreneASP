using System;
using API.Models.Domain;
using API.Models.DTO;
using API.Models.DTO.Student;
using API.Models.DTO.Teacher;
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

        // TEACHERS
        CreateMap<Teacher, CreateTeacherDto>().ReverseMap();
        CreateMap<Teacher, TeacherDto>().ReverseMap();

        // STUDENTS
        CreateMap<StudentHeader, StudentHeaderDto>().ReverseMap();
        CreateMap<StudentDetail, StudentDetailDto>().ReverseMap();
        CreateMap<CreateStudentDto, StudentHeader>().ReverseMap();
        CreateMap<CreateStudentDetailDto, StudentDetail>().ReverseMap();
    }
}
