using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Tests
{
    [TestClass()]
    public class ChainTests
    {
        [TestMethod()]
        public void ChainTest()
        {
            var chain = new Chain();

            chain.Add("Blockchain", "Admin");

            Assert.AreEqual(2, chain.Blocks.Count);
            Assert.AreEqual("Blockchain", chain.LastBlock.Data);
        }

        [TestMethod()]
        public void CheckTest()
        {
            var chain = new Chain();

            chain.Add("Blockchain", "Admin");
            chain.Add("This day is perfect;", "user1");

            Assert.IsTrue(chain.Check());
        }
    }
}