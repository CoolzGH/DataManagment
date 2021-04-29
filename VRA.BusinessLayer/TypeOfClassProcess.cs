using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public class TypeOfClassProcess : ITypeOfClassProcess
    {
        private static readonly IDictionary<int, TypeOfClassDto> TypeOfClasses = new Dictionary<int, TypeOfClassDto>();
        public IList<TypeOfClassDto> GetList()
        {
            return new List<TypeOfClassDto>(TypeOfClasses.Values);
        }

        public TypeOfClassDto Get(int id)
        {
            return TypeOfClasses.ContainsKey(id) ? TypeOfClasses[id] : null;
        }

        public void Add(TypeOfClassDto typeofclass)
        {
            int max = TypeOfClasses.Keys.Count == 0 ? 1 : TypeOfClasses.Keys.Max(p => p) + 1;
            typeofclass.TypeOfClassId = max;
            TypeOfClasses.Add(max, typeofclass);
        }

        public void Update(TypeOfClassDto typeofclass)
        {
            if (TypeOfClasses.ContainsKey(typeofclass.TypeOfClassId))
            {
                TypeOfClasses[typeofclass.TypeOfClassId] = typeofclass;
            }
        }

        public void Delete(int id)
        {
            if (TypeOfClasses.ContainsKey(id))
            {
                TypeOfClasses.Remove(id);
            }
        }
    }
}