using Azure;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MillionAndUpApi.Business;
using MillionAndUpApi.Controllers;
using MillionAndUpApi.DTO_s;
using MillionAndUpApi.Helpers;
using MillionAndUpApi.Enums;
using MillionAndUpApi.Interfaces.Helpers;
using MillionAndUpApi.Interfaces.Services;
using MillionAndUpApi.Mappers;
using MillionAndUpApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.PropertyTest
{
    public class PropertyBusinessTests : BaseTests
    {

        [Test]
        public async Task CreatePropertySuccess()
        {
            //arrange
            var context = BuildContext("millionUpTest");
            Owner owner = new Owner()
            {
                IdOwner = Guid.NewGuid(),
                Address = "Address",
                Birthday = DateTime.Now,
                Name = "Test",
                Photo = "Test"
            };
            context.Owner.Add(owner);
            await context.SaveChangesAsync();
            var dataServices = new DataServices(context);
            var propertyMapper = new PropertyMapper();
            var manageFiles = new ManageFiles();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            var propertiesBusiness = new PropertiesBusiness(dataServices, propertyMapper, manageFiles, configuration);
            var propertyDTO = new PropertyDTO()
            {
                Name = "NamePrueba",
                Address = "Address",
                IdOwner = owner.IdOwner.ToString(),
                Price = 13000,
                Year = 2000,
                CodeInternal = ""
            };

            //act
            var result = await propertiesBusiness.CreateProperty(propertyDTO);

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreatePropertyExeptionEmptyFields()
        {
            //arrange
            var context = BuildContext("millionUpTest");
            Owner owner = new Owner()
            {
                IdOwner = Guid.NewGuid(),
                Address = "Address",
                Birthday = DateTime.Now,
                Name = "Test",
                Photo = "Test"
            };
            context.Owner.Add(owner);
            await context.SaveChangesAsync();
            var dataServices = new DataServices(context);
            var propertyMapper = new PropertyMapper();
            var manageFiles = new ManageFiles();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            var propertiesBusiness = new PropertiesBusiness(dataServices, propertyMapper, manageFiles, configuration);
            var propertyDTO = new PropertyDTO()
            {
                Name = "",
                Address = "",
                IdOwner = owner.IdOwner.ToString(),
                Price = 13000,
                Year = 2000,
                CodeInternal = ""
            };

            //assert
            var ex = Assert.ThrowsAsync<Exception>(() => propertiesBusiness.CreateProperty(propertyDTO));
            StringAssert.Contains("empty fields", ex.Message);
        }

        [Test]
        public async Task CreatePropertyImageSuccess()
        {
            //arrange
            var context = BuildContext("millionUpTest");
            Owner owner = new Owner()
            {
                IdOwner = Guid.NewGuid(),
                Address = "Address",
                Birthday = DateTime.Now,
                Name = "Test",
                Photo = "Test"
            };
            context.Owner.Add(owner);
            var dataServices = new DataServices(context);
            var propertyMapper = new PropertyMapper();
            var manageFiles = new ManageFiles();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            var propertiesBusiness = new PropertiesBusiness(dataServices, propertyMapper, manageFiles, configuration);
            var property = new Property()
            {
                IdProperty = new Guid(),
                Name = "NamePrueba",
                Address = "Address",
                IdOwner = owner.IdOwner,
                Price = 13000,
                Year = 2000,
            };
            context.Property.Add(property);
            await context.SaveChangesAsync();
            var content = "file";
            var fileName = "test.png";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            IFormFile formFile = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var propertyImageDTO = new PropertyImageDTO()
            {
                IdProperty = property.IdProperty.ToString(),
                File = formFile,
                Enabled = true,
            };

            //act
            var result = await propertiesBusiness.CreatePropertyImage(propertyImageDTO);

            //asert
            Assert.NotNull(result);

        }

        [Test]
        public async Task CreatePropertyTraceSuccess()
        {
            //arrange
            var context = BuildContext("millionUpTest");
            Owner owner = new Owner()
            {
                IdOwner = Guid.NewGuid(),
                Address = "Address",
                Birthday = DateTime.Now,
                Name = "Test",
                Photo = "Test"
            };
            context.Owner.Add(owner);
            var dataServices = new DataServices(context);
            var propertyMapper = new PropertyMapper();
            var manageFiles = new ManageFiles();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            var propertiesBusiness = new PropertiesBusiness(dataServices, propertyMapper, manageFiles, configuration);
            var property = new Property()
            {
                IdProperty = new Guid(),
                Name = "NamePrueba",
                Address = "Address",
                IdOwner = owner.IdOwner,
                Price = 13000,
                Year = 2000,
            };
            context.Property.Add(property);
            await context.SaveChangesAsync();
            var propertyTraceDTO = new PropertyTraceDTO()
            {
                DateSale= DateTime.Now,
                IdProperty = property.IdProperty.ToString(),
                Name = "Prueba",
                Tax = 1,
                Value = "13000000"
            };

            //act
            var result = await propertiesBusiness.CreatePropertyTrace(propertyTraceDTO);

            //asert
            Assert.NotNull(result);

        }

        [Test]
        public async Task GetPropertyNull()
        {
            //arrange
            var context = BuildContext("millionUpTest");
            Owner owner = new Owner()
            {
                IdOwner = Guid.NewGuid(),
                Address = "Address",
                Birthday = DateTime.Now,
                Name = "Test",
                Photo = "Test"
            };
            context.Owner.Add(owner);
            var dataServices = new DataServices(context);
            var propertyMapper = new PropertyMapper();
            var manageFiles = new ManageFiles();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            var propertiesBusiness = new PropertiesBusiness(dataServices, propertyMapper, manageFiles, configuration);
            var property = new Property()
            {
                IdProperty = new Guid(),
                Name = "NamePrueba",
                Address = "Address",
                IdOwner = owner.IdOwner,
                Price = 13000,
                Year = 2000,
            };
            context.Property.Add(property);
            await context.SaveChangesAsync();
            var id = new Guid();
            //act
            var result = await propertiesBusiness.GetPropertyById(id);

            //assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetPropertyByIdSuccess()
        {
            //arrange
            var context = BuildContext("millionUpTest");
            Owner owner = new Owner()
            {
                IdOwner = Guid.NewGuid(),
                Address = "Address",
                Birthday = DateTime.Now,
                Name = "Test",
                Photo = "Test"
            };
            context.Owner.Add(owner);
            var dataServices = new DataServices(context);
            var propertyMapper = new PropertyMapper();
            var manageFiles = new ManageFiles();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            var propertiesBusiness = new PropertiesBusiness(dataServices, propertyMapper, manageFiles, configuration);
            var property = new Property()
            {
                IdProperty = new Guid(),
                Name = "NamePrueba",
                Address = "Address",
                IdOwner = owner.IdOwner,
                Price = 13000,
                Year = 2000,
            };
            context.Property.Add(property);
            await context.SaveChangesAsync();
            //act
            var result = await propertiesBusiness.GetPropertyById(property.IdProperty);

            //assert
            Assert.AreEqual(property.Name, result.Name);
        }

        [Test]
        public async Task GetPropertyByFilterSuccess()
        {
            //arrange
            var context = BuildContext("millionUpTest");
            Owner owner = new Owner()
            {
                IdOwner = Guid.NewGuid(),
                Address = "Address",
                Birthday = DateTime.Now,
                Name = "Test",
                Photo = "Test"
            };
            context.Owner.Add(owner);
            var dataServices = new DataServices(context);
            var propertyMapper = new PropertyMapper();
            var manageFiles = new ManageFiles();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            var propertiesBusiness = new PropertiesBusiness(dataServices, propertyMapper, manageFiles, configuration);
            var property = new Property()
            {
                IdProperty = new Guid(),
                Name = "NamePrueba",
                Address = "Address",
                IdOwner = owner.IdOwner,
                Price = 13000,
                Year = 2000,
            };
            context.Property.Add(property);
            await context.SaveChangesAsync();
            var filter = new PropertyFilterDTO()
            {
                FieldsByFilerProerty = FieldsByFilerProerty.name,
                Value = "Prueba"
            };

            //act
            var result = await propertiesBusiness.GetAllByFilter(filter);

            //assert
            Assert.AreEqual(property.Name, result.FirstOrDefault().Name);
        }

        [Test]
        public async Task GetPropertyByFilterIsNull()
        {
            //arrange
            var context = BuildContext("millionUpTest");
            Owner owner = new Owner()
            {
                IdOwner = Guid.NewGuid(),
                Address = "Address",
                Birthday = DateTime.Now,
                Name = "Test",
                Photo = "Test"
            };
            context.Owner.Add(owner);
            var dataServices = new DataServices(context);
            var propertyMapper = new PropertyMapper();
            var manageFiles = new ManageFiles();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            var propertiesBusiness = new PropertiesBusiness(dataServices, propertyMapper, manageFiles, configuration);
            var property = new Property()
            {
                IdProperty = new Guid(),
                Name = "NamePrueba",
                Address = "Address",
                IdOwner = owner.IdOwner,
                Price = 13000,
                Year = 2000,
            };
            context.Property.Add(property);
            await context.SaveChangesAsync();
            var filter = new PropertyFilterDTO()
            {
                FieldsByFilerProerty = FieldsByFilerProerty.name,
                Value = "test"
            };

            //act
            var result = await propertiesBusiness.GetAllByFilter(filter);

            //assert
            Assert.IsNull(result);
        }

    }
}
