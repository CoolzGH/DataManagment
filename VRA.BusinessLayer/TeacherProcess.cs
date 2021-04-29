using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class TeacherProcess : ITeacherProcess
    {
        private static readonly IDictionary<int, TeacherDto> Teachers = new Dictionary<int, TeacherDto>();
        public IList<TeacherDto> GetList()
        {
            return new List<TeacherDto>(Teachers.Values);
        }

        public TeacherDto Get(int id)
        {
            return Teachers.ContainsKey(id) ? Teachers[id] : null;
        }

        public void Add(TeacherDto teacher)
        {
            int max = Teachers.Keys.Count == 0 ? 1 : Teachers.Keys.Max(p => p) + 1;
            teacher.TeacherId = max;
            Teachers.Add(max, teacher);
        }

        public void Update(TeacherDto teacher)
        {
            if (Teachers.ContainsKey(teacher.TeacherId))
            {
                Teachers[teacher.TeacherId] = teacher;
            }
        }

        public void Delete(int id)
        {
            if (Teachers.ContainsKey(id))
            {
                Teachers.Remove(id);
            }
        }
    }
}