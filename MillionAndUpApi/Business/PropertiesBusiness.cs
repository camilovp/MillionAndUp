using Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using MillionAndUpApi.DTO_s;
using MillionAndUpApi.Interfaces.Business;
using MillionAndUpApi.Interfaces.Helpers;
using MillionAndUpApi.Interfaces.Mappers;
using MillionAndUpApi.Interfaces.Services;
using System.Data;

namespace MillionAndUpApi.Business
{
    public class PropertiesBusiness : IPropertiesBusiness
    {
        private readonly IDataServices _dataServices;
        private readonly IPropertyMapper _propertyMapper;
        private readonly IManageFiles _manageFiles;
        private readonly IConfiguration _configuration;

        public PropertiesBusiness(
            IDataServices propertyServices,
            IPropertyMapper propertyMapper,
            IManageFiles manageFiles,
            IConfiguration configuration)
        {
            _dataServices = propertyServices;
            _propertyMapper = propertyMapper;
            _manageFiles = manageFiles;
            _configuration = configuration;
        }

        /// <summary>
        /// Get Proporty By IdProperty
        /// </summary>
        /// <param name="id">IdProperty</param>
        /// <returns>PropertyDTO</returns>
        public async Task<PropertyDTO> GetPropertyById(Guid Id) 
        {
            Property property = await _dataServices.GetById<Property>(Id);
            if (property == null) return null;
            PropertyDTO response = _propertyMapper.PropertyToDTO(property);
            return response;
        }

        /// <summary>
        /// Get all property
        /// </summary>
        /// <returns>PropertyDTO</returns>
        public async Task<List<PropertyDTO>> GetAllProperty()
        {
            List<Property> getResponse = await _dataServices.GetAll<Property>();
            if (getResponse?.Count < 0) return null;
            return _propertyMapper.PropertyListToDTO(getResponse);
        }

        /// <summary>
        /// Get Property By Filter
        /// </summary>
        /// <param name="propertyFilterDTO">Parameters and Value</param>
        /// <returns>PropertyDTO</returns>
        public async Task<List<PropertyDTO>> GetAllByFilter(PropertyFilterDTO propertyFilterDTO)
        {
            List<Property> responseList = null;
            switch (propertyFilterDTO.FieldsByFilerProerty)
            {
                case Enums.FieldsByFilerProerty.name:
                    responseList = await _dataServices.GetAllByFiler<Property>(x => x.Name.Contains(propertyFilterDTO.Value));
                    if (responseList == null || responseList.Count < 1) return null;
                    return _propertyMapper.PropertyListToDTO(responseList);
                case Enums.FieldsByFilerProerty.address:
                    responseList = await _dataServices.GetAllByFiler<Property>(x => x.Address.Contains(propertyFilterDTO.Value));
                    if (responseList == null || responseList.Count < 1) return null;
                    return _propertyMapper.PropertyListToDTO(responseList);
                case Enums.FieldsByFilerProerty.price:
                    responseList = await _dataServices.GetAllByFiler<Property>(x => x.Price >= Convert.ToInt32(propertyFilterDTO.Value));
                    if (responseList == null || responseList.Count < 1) return null;
                    return _propertyMapper.PropertyListToDTO(responseList);
                case Enums.FieldsByFilerProerty.year:
                    responseList = await _dataServices.GetAllByFiler<Property>(x => x.Year >= Convert.ToInt32(propertyFilterDTO.Value));
                    if (responseList == null || responseList.Count < 1) return null;
                    return _propertyMapper.PropertyListToDTO(responseList);
            }

            return null;
        }

        /// <summary>
        /// Create Property
        /// </summary>
        /// <param name="propertyDTO">PropertyDTO</param>
        /// <returns><PropertyDTO/returns>
        public async Task<PropertyDTO> CreateProperty(PropertyDTO propertyDTO)
        {
            if (propertyDTO == null ||
                string.IsNullOrEmpty(propertyDTO.Name) ||
                string.IsNullOrEmpty(propertyDTO.Address))
                throw new Exception("empty fields");

            propertyDTO.IdProperty = new Guid().ToString();
            Property property = _propertyMapper.DTOToProperty(propertyDTO);
            var propertyResponse = await _dataServices.Create(property);
            if (propertyResponse == null) return null;
            PropertyDTO response = _propertyMapper.PropertyToDTO(propertyResponse);

            return response;
        }

        /// <summary>
        /// Create Image
        /// </summary>
        /// <param name="propertyImageDTO">PropertyImageDTO</param>
        /// <returns>PropertyImage</returns>
        public async Task<PropertyImageDTO> CreatePropertyImage(PropertyImageDTO propertyImageDTO)
        {
            propertyImageDTO.IdPropertyImage = new Guid().ToString();
            PropertyImage propertyImage = _propertyMapper.DTOToPropertyImage(propertyImageDTO, propertyImageDTO.File.FileName);
            PropertyImage createResponse = await _dataServices.Create(propertyImage);
            if (createResponse == null) return null;
            string path = Path.Combine(@Directory.GetCurrentDirectory(),"Images");
            _manageFiles.SaveFile(propertyImageDTO.File, path, createResponse.IdPropertyImage.ToString());
            PropertyImageDTO response = _propertyMapper.PropertyImageToDTO(createResponse, path + createResponse.IdPropertyImage.ToString());
            return response;
        }

        /// <summary>
        /// Create Property Trace
        /// </summary>
        /// <param name="propertyTraceDTO">PropertyTraceDTO</param>
        /// <returns>PropertyTraceDTO</returns>
        public async Task<PropertyTraceDTO> CreatePropertyTrace(PropertyTraceDTO propertyTraceDTO)
        {
            propertyTraceDTO.IdPropertyTrace = new Guid().ToString();
            PropertyTrace propertyTrace = _propertyMapper.DTOToPropertyTrace(propertyTraceDTO);
            PropertyTrace createResponse = await _dataServices.Create(propertyTrace);
            return _propertyMapper.PropertyTraceToDTO(createResponse);
        }

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="propertyDTO">PropertyDTO</param>
        /// <returns>PropertyDTO</returns>

        public async Task<PropertyDTO> UpdateProperty(PropertyDTO propertyDTO)
        {
            var validate = await _dataServices.ValidExists<Property>(x => x.IdProperty == Guid.Parse(propertyDTO.IdProperty));
            if (!validate) return null;
            Property property = _propertyMapper.DTOToProperty(propertyDTO);
            Property updateResponse = await _dataServices.Update(property);
            return _propertyMapper.PropertyToDTO(updateResponse);
        }

    }
}
