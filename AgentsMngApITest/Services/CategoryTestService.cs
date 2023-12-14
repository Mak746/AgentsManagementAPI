using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AgentsMngApITest.Services
{
    [TestClass]
    public class CategoryTestService
    {
        private readonly CategoryService _categoryService;
        public CategoryTestService(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [TestMethod]
        public void TestMethod1()
        {

            bool expected = true;
            bool actual = false;
            var category = new Category
            {
                Id = 12,
                Name = "Michael",

            };
            /* Assuming that test_function(obj) returns an object type of Some_object */
            if (category is Agent)
            {
                actual = true;
            }

            Assert.AreEqual(expected, actual);
        }

    }
}