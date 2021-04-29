using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class SubjectProcess : ISubjectProcess
    {
        private static readonly IDictionary<int, SubjectDto> Subjects = new Dictionary<int, SubjectDto>();
        public IList<SubjectDto> GetList()
        {
            return new List<SubjectDto>(Subjects.Values);
        }

        public SubjectDto Get(int id)
        {
            return Subjects.ContainsKey(id) ? Subjects[id] : null;
        }

        public void Add(SubjectDto subject)
        {
            int max = Subjects.Keys.Count == 0 ? 1 : Subjects.Keys.Max(p => p) + 1;
            subject.SubjectId = max;
            Subjects.Add(max, subject);
        }

        public void Update(SubjectDto subject)
        {
            if (Subjects.ContainsKey(subject.SubjectId))
            {
                Subjects[subject.SubjectId] = subject;
            }
        }

        public void Delete(int id)
        {
            if (Subjects.ContainsKey(id))
            {
                Subjects.Remove(id);
            }
        }
    }
}