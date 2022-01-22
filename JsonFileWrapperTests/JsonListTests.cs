// -----------------------------------------------------------------------------------------------
//  JsonListTests.cs by Marcus Medina, Copyright (C) 2021, http://MarcusMedina.Pro.
//  Published under Apache License 2.0 (Apache-2.0)
//  https://tldrlegal.com/license/apache-license-2.0-%28apache-2.0%29
// -----------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarcusMedinaPro.JsonFileWrapper;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarcusMedinaPro.JsonFileWrapper.Tests
{
    /// <summary>
    /// The test item.
    /// </summary>
    public class TestItem: IComparable
    {
        public string Name { get; set; }
        public string Color { get; set; }

        /// <summary>
        /// Compares the to.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An int.</returns>
        public int CompareTo(object? obj) => Name.CompareTo((obj as TestItem)?.Name);
        public override string ToString() => $"{Name} color {Color}";
    }

    public class TestItemNameComparer : IComparer<TestItem>
    {
        public int Compare(TestItem first, TestItem second)
        {
            return first.Name.CompareTo(second.Name);
        }
    }


    [TestClass()]
    public class JsonListTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            if (File.Exists("TestItem.json"))
                File.Delete("TestItem.json");
            if (File.Exists("TestItem2.json"))
                File.Delete("TestItem2.json");

            var lst = new JsonList<TestItem>("TestItem");
            lst.Add(new TestItem[]
            {
            new TestItem { Name = "Cherry", Color = "Yellow" },
            new TestItem { Name = "Cherry", Color = "Red" },
            new TestItem { Name = "Cherry", Color = "Black" },
            new TestItem { Name = "Apple", Color = "Green" },
            new TestItem { Name = "Apple", Color = "Red" },
            });
            lst.Save();

            lst = new JsonList<TestItem>("TestItem2");
            lst.Add(new TestItem[]
            {
            new TestItem { Name = "Kiwi", Color = "Green" },
            new TestItem { Name = "Kiwi", Color = "Yellow" },
            });
            lst.Save();
        }
        [TestMethod()]
        public void JsonListTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            Assert.AreEqual(5, lst.Length);
        }

        [TestMethod()]
        public void AddTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            lst.Add(new TestItem[]
            {
            new TestItem { Name = "Orange", Color = "Orange" },
            });
            Assert.AreEqual(6, lst.Length);
        }

        [TestMethod()]
        public void AddTest1()
        {
            var lst = new JsonList<TestItem>("TestItem");
            lst.Add(new TestItem[]
            {
            new TestItem { Name = "Orange", Color = "Yellow" },
            new TestItem { Name = "Orange", Color = "Orange" },
            });
            Assert.AreEqual(7, lst.Length);
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            lst.AddRange(new TestItem[]
            {
            new TestItem { Name = "Orange", Color = "Yellow" },
            new TestItem { Name = "Orange", Color = "Orange" },
            });
            Assert.AreEqual(7, lst.Length);
        }

        [TestMethod()]
        public void ClearTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            lst.Clear();
            Assert.AreEqual(0, lst.Length);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 3 ];
            lst.Delete(2);
            var actual = lst[ 2 ];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DeleteTest1()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 3 ];
            lst.Delete(lst[ 2 ]);
            var actual = lst[ 2 ];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DeleteTest2()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 2 ];
            lst.Delete(c => c.Color == "Red");
            var actual = lst[ 1 ];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ForEachTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = "GreenRedYellowRedBlack";
            var actual = "";
            lst.ForEach(f => actual += f.Color);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            Assert.IsTrue(lst.GetEnumerator() is System.Collections.IEnumerator);

        }

        [TestMethod()]
        public void GetEnumeratorTest2()
        {
            var lst = new JsonList<TestItem>("TestItem");
            Assert.IsTrue(lst.GetEnumerator() is not null);

        }
        [TestMethod()]
        public void IsEmptyTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            Assert.IsFalse(lst.IsEmpty());
        }

        [TestMethod()]
        public void IsEmptyTest2()
        {
            var lst = new JsonList<TestItem>("TestItem");
            lst.Clear();
            Assert.IsTrue(lst.IsEmpty());
        }

        [TestMethod()]
        public void ImportTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            _ = lst.Import("Testitem2.json");
            Assert.AreEqual(7, lst.Length);        }

        [TestMethod()]
        public void RemoveTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 3 ];
            lst.Remove(lst[ 2 ]);
            var actual = lst[ 2 ];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RemoveAtTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 3 ];
            lst.Delete(2);
            var actual = lst[ 2 ];

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void SortTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 3 ];
            lst.Sort();
            var actual = lst[ 0 ];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SortTest1()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 3 ];
            lst.Sort((a, b) => a.Color.CompareTo(b.Color));
            var actual = lst[ 1 ];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SortTest2()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 3 ];
            lst.Sort((a, b) => a.Name.CompareTo(b.Name));
            var actual = lst[ 0 ];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SortTest3()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[2 ];
            lst.Sort(1, 3, new TestItemNameComparer());
            var actual = lst[ 2 ];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SortTest4()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var expected = lst[ 2 ];
            lst.Sort(t=>t.Color);
            var actual = lst[ 0 ];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CloneTest()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var lst2 = lst.Clone(t=>new TestItem { Color=t.Color, Name=t.Name });

            Assert.AreNotEqual(lst[0].GetHashCode(), lst2[0].GetHashCode());
        }

        [TestMethod()]
        public void CloneTest1()
        {
            var lst = new JsonList<TestItem>("TestItem");
            var lst2 = lst.Clone();

            Assert.AreEqual(lst[ 0 ].GetHashCode(), lst2[ 0 ].GetHashCode());

        }
    }
}
