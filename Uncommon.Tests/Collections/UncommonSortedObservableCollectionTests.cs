using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xciles.Uncommon.Collections;

namespace Xciles.Uncommon.Tests.Collections
{
    [TestClass]
    public class UncommonSortedObservableCollectionTests
    {
        private class StringObjectComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return String.Compare(x, y, StringComparison.InvariantCulture);
            }
        }

        [TestMethod]
        public void CreateNewObservableCollection()
        {
            var list = new List<string>()
                {
                    "First",
                    "Second",
                    "Third"
                };

            var mObs = new UncommonSortedObservableCollection<string>(new StringObjectComparer());

            Assert.IsFalse(mObs.IsReadOnly);

            list.ForEach(x => mObs.InsertItem(x));

            Assert.IsTrue(mObs.Count == 3);
            list.ToList().ForEach(s => Assert.IsTrue(mObs.Contains(s)));
        }
    }
}
