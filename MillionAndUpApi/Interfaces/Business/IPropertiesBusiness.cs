using MillionAndUpApi.DTO_s;

namespace MillionAndUpApi.Interfaces.Business
{
    public interface IPropertiesBusiness
    {
        Task<PropertyDTO> GetPropertyById(Guid Id);
        Task<List<PropertyDTO>> GetAllProperty();
        Task<List<PropertyDTO>> GetAllByFilter(PropertyFilterDTO propertyFilterDTO);
        Task<PropertyDTO> CreateProperty(PropertyDTO propertyDTO);
        Task<PropertyImageDTO> CreatePropertyImage(PropertyImageDTO propertyImageDTO);
        Task<PropertyTraceDTO> CreatePropertyTrace(PropertyTraceDTO propertyTraceDTO);
        Task<PropertyDTO> UpdateProperty(PropertyDTO propertyDTO);
    }
}
