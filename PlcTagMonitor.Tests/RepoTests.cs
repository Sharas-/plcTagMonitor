using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlcTagMonitor.Repository;
using System.Net;

namespace PlcTagMonitor.Tests
{
    [TestClass]
    public class RepoTests
    {
        [TestMethod]
        public void tags_can_be_read()
        {
            IPAddress ip = null;
            IPAddress.TryParse("192.168.10.1", out ip);
            var tags = new Tags(ip).GetAll();
            Assert.IsTrue(tags.Count != 0);
        }
    }
}
