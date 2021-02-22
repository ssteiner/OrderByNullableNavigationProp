using ConsoleApp3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var set = GenerateSet();
            var orderedSet = set.Order(nameof(TestClass.Name), true);
            var orderedList = orderedSet.ToList();
            var orderedSet2 = set.Order(nameof(TestClass.ReferenceOneId), true);
            var orderedList2 = orderedSet2.ToList();
            var orderedSet3 = set.Order(nameof(TestClass.ReferenceTwoId), true);
            var orderedList3 = orderedSet3.ToList();
        }

        private static IQueryable<TestClass> GenerateSet()
        {
            var list = new List<TestClass>
            {
                GenerateTestItem("item one", true, "lll reference"),
                GenerateTestItem("aaa item", true, "zzz reference"),
                GenerateTestItem("aab item", true, "xxx reference", true, "aaa reference"),
                GenerateTestItem("aac item", true, "ddd reference", true, "bbb reference"),
                GenerateTestItem("aad item", true, "uuu reference", true, "ccc reference"),
            };
            return list.AsQueryable();
        }

        private static TestClass GenerateTestItem(string name, bool includeFirstReference = false, string firstReferenceName = null, bool includeSecondReference = false, string secondReferenceName = null)
        {
            var item = new TestClass 
            { 
                Id = Guid.NewGuid(), 
                Name = name
            };
            if (includeFirstReference)
            {
                var refId = Guid.NewGuid();
                item.ReferenceOne = new ReferenceClassOne { Id = refId, Name = firstReferenceName };
                item.ReferenceOneId = refId;
            }
            if (includeSecondReference)
            {
                var refId = Guid.NewGuid();
                item.ReferenceTwo = new ReferenceClassTwo { Id = refId, Name = secondReferenceName };
                item.ReferenceTwoId = refId;
            }
            return item;
        }
    }
}
