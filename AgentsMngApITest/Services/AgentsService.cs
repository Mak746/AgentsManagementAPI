using Core.Entities;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgentsMngApITest.Services
{

    [TestClass]
    public class AgentsService
    {
        private readonly AgentService _agentService;
        public AgentsService(AgentService agentService)
        {
            _agentService = agentService;
        }

        [TestMethod]
        public void TestMethod1()
        {

            bool expected = true;
            bool actual = false;
            var singleAgent = new Agent
            {
                Id = 12,
                FirstName = "Michael",
                LastName = "Mogos",
                DateOfBirth = System.DateTime.Now,
                Commission = 100,
                CategoryId = 1,
                AgentPhotoPath = "E:\\MICHAEL\\project\\AgentsManagementAPI\\API\\wwwroot\\images\\1.jpg",

            };
            /* Assuming that test_function(obj) returns an object type of Some_object */
            if (singleAgent is Agent)
            {
                actual = true;
            }

            Assert.AreEqual(expected, actual);
        }

    }
}
