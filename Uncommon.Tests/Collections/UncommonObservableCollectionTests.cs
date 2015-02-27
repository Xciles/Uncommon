using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xciles.Uncommon.Collections;

namespace Xciles.Uncommon.Tests.Collections
{
    [TestClass]
    public class UncommonObservableCollectionTests
    {
        [TestMethod]
        public void CreateNewObservableCollection()
        {
            var list = new List<string>()
                {
                    "First",
                    "Second",
                    "Third"
                };

            var mObs = new UncommonObservableCollection<string>(list);

            Assert.IsTrue(mObs.Count == 3);
            list.ToList().ForEach(s => Assert.IsTrue(mObs.Contains(s)));
        }
    }
}
