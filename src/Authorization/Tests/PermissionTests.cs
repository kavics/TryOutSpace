using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class PermissionTests
    {

        [TestMethod]
        public void PermissionTest()
        {
            var _ = typeof(Client.Class1); // preload assembly

            // ARRANGE
            var host = new Host();

            var content = new Content { Name = "Content1" };
            content.AddPermission("Group1", PermissionType.Open);
            content.AddPermission("Group1", PermissionType.RunApplication);
            host.SaveContent(content);

            content = new Content { Name = "Content2" };
            content.AddPermission("Group1", PermissionType.See);
            content.AddPermission("Group1", PermissionType.Open);
            content.AddPermission("Group1", PermissionType.RunApplication);
            host.SaveContent(content);

            content = new Content { Name = "Content3" };
            content.AddPermission("Group1", PermissionType.Open);
            host.SaveContent(content);

            // ACTION / ASSERT
            Assert.AreEqual("Content1: ok.", host.Invoke("Client", "Class1", "DoIt", "Content1"));
            Assert.AreEqual("Content2: ok.", host.Invoke("Client", "Class1", "DoIt", "Content2"));
            Assert.AreEqual("Access denied.", host.Invoke("Client", "Class1", "DoIt", "Content3"));
        }
    }
}
