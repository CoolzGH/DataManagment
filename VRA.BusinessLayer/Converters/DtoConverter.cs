using System;
using System.Collections.Generic;
using System.Text;
using VRA.Dto;
using VRA.DataAccess.Entities;
using VRA.DataAccess;

namespace VRA.BusinessLayer.Converters
{
    public class DtoConverter
    {
        public static TeacherDto Convert(Teacher teacher)
        {
            if (teacher == null)
                return null;
            TeacherDto teacherDto = new TeacherDto();
            teacherDto.TeacherId = teacher.TeacherID;
            teacherDto.SecondName = teacher.SecondName;
            teacherDto.FirstName = teacher.FirstName;
            teacherDto.MiddleName = teacher.MiddleName;
            teacherDto.AcademicDegree = teacher.AcademicDegree;
            teacherDto.Position = teacher.Position;
            teacherDto.Experience = teacher.Experience;
            return teacherDto;
        }

        public static Teacher Convert(TeacherDto teacherDto)
        {
            if (teacherDto == null)
                return null;
            Teacher teacher = new Teacher();
            teacher.TeacherID = teacherDto.TeacherId;
            teacher.SecondName = teacherDto.SecondName;
            teacher.FirstName = teacherDto.FirstName;
            teacher.MiddleName = teacherDto.MiddleName;
            teacher.AcademicDegree = teacherDto.AcademicDegree;
            teacher.Position = teacherDto.Position;
            teacher.Experience = teacherDto.Experience;
            return teacher;
        }

        public static IList<TeacherDto> Convert(IList<Teacher> teachers)
        {
            if (teachers == null)
                return null;
            IList<TeacherDto> teacherDtos = new List<TeacherDto>();
            foreach (var teacher in teachers)
            {
                teacherDtos.Add(Convert(teacher));
            }
            return teacherDtos;
        }

        public static SubjectDto Convert(Subject subject)
        {
            if (subject == null)
                return null;
            SubjectDto subjectDto = new SubjectDto();
            subjectDto.SubjectId = subject.SubjectID;
            subjectDto.Title = subject.Title;
            subjectDto.SubjectHours = subject.SubjectHours;
            return subjectDto;
        }

        public static Subject Convert(SubjectDto subjectDto)
        {
            if (subjectDto == null)
                return null;
            Subject subject = new Subject();
            subject.SubjectID = subjectDto.SubjectId;
            subject.Title = subjectDto.Title;
            subject.SubjectHours = subjectDto.SubjectHours;
            return subject;
        }

        public static IList<SubjectDto> Convert(IList<Subject> subjects)
        {
            if (subjects == null)
                return null;
            IList<SubjectDto> subjectDtos = new List<SubjectDto>();
            foreach (var subject in subjects)
            {
                subjectDtos.Add(Convert(subject));
            }
            return subjectDtos;
        }

        public static TypeOfClassDto Convert(TypeOfClass typeofclass)
        {
            if (typeofclass == null)
                return null;
            TypeOfClassDto typeofclassDto = new TypeOfClassDto();
            typeofclassDto.TypeOfClassId = typeofclass.TypeOfClassID;
            typeofclassDto.TypeOfClassName = typeofclass.TypeOfClassName;
            typeofclassDto.ClassHours = typeofclass.ClassHours;
            return typeofclassDto;
        }

        public static TypeOfClass Convert(TypeOfClassDto typeofclassDto)
        {
            if (typeofclassDto == null)
                return null;
            TypeOfClass typeofclass = new TypeOfClass();
            typeofclass.TypeOfClassID = typeofclassDto.TypeOfClassId;
            typeofclass.TypeOfClassName = typeofclassDto.TypeOfClassName;
            typeofclass.ClassHours = typeofclassDto.ClassHours;
            return typeofclass;
        }

        public static IList<TypeOfClassDto> Convert(IList<TypeOfClass> typeofclasses)
        {
            if (typeofclasses == null)
                return null;
            IList<TypeOfClassDto> typeofclassDtos = new List<TypeOfClassDto>();
            foreach (var typeofclass in typeofclasses)
            {
                typeofclassDtos.Add(Convert(typeofclass));
            }
            return typeofclassDtos;
        }

        public static LoadDto Convert(Load load)
        {
            if (load == null)
                return null;
            LoadDto loadDto = new LoadDto();
            loadDto.LoadId = load.LoadID;
            loadDto.Teacher = Convert(DaoFactory.GetTeacherDao().Get(load.TeacherID));
            loadDto.GroupNumber = load.GroupNumber;
            loadDto.LoadDate = load.LoadDate;
            loadDto.Subject = Convert(DaoFactory.GetSubjectDao().Get(load.SubjectID));
            loadDto.TypeOfClass = Convert(DaoFactory.GetTypeOfClassDao().Get(load.TypeOfClassID));
            return loadDto;
        }

        public static Load Convert(LoadDto loadDto)
        {
            if (loadDto == null)
                return null;
            Load load = new Load();
            load.LoadID = loadDto.LoadId;
            load.TeacherID = loadDto.Teacher.TeacherId;
            load.GroupNumber = loadDto.GroupNumber;
            load.LoadDate = loadDto.LoadDate;
            load.SubjectID = loadDto.Subject.SubjectId;
            load.TypeOfClassID = loadDto.TypeOfClass.TypeOfClassId;
            return load;
        }

        public static IList<LoadDto> Convert(IList<Load> loads)
        {
            if (loads == null)
                return null;
            IList<LoadDto> loadDtos = new List<LoadDto>();
            foreach (var load in loads)
            {
                loadDtos.Add(Convert(load));
            }
            return loadDtos;
        }
    }
}