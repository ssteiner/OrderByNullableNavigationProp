using System;

namespace ConsoleApp3
{
    public class TestClass : IReferenceClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid ReferenceOneId { get; set; }

        public Guid? ReferenceTwoId { get; set; }


        public virtual ReferenceClassOne ReferenceOne { get; set; }

        public virtual ReferenceClassTwo ReferenceTwo { get; set; }
    }

    public interface IReferenceClass
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }

    public class ReferenceClassOne: IReferenceClass
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} -  {Id}";
        }
    }

    public class ReferenceClassTwo: IReferenceClass
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} -  {Id}";
        }
    }
}
