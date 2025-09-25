using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using NUnit.Framework;
using RealStateDataModel.Models;
using RealStateService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApiTest.ControllerTest
{
    public class RealStateApiTest
    {
        private readonly IPropertyApiService _propertyService;
        public RealStateApiTest(IPropertyApiService propertyService)
        {
            _propertyService = propertyService;
        }

        [Test]
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        [TearDown]
        public void ValidateGetProperties()
        {
            var pro = _propertyService.GetProperties().Result;

            foreach (var x in pro)
            {
                Assert.That(x.IdProperty != 0);
            }
        }

        [Test]
        [TearDown]
        public void ValidateGetPropertiesFilter()
        {
            PropertyFilters filters = new PropertyFilters();
            filters.Name = "1";

            var pro = _propertyService.GetProperties(filters).Result;

            foreach (var x in pro)
            {
                Assert.That(x.Name.Contains(filters.Name));
            }
        }
    }
}
