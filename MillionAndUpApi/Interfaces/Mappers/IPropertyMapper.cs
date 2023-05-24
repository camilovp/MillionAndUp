using Data.Entities;
using MillionAndUpApi.DTO_s;

namespace MillionAndUpApi.Interfaces.Mappers
{
    public interface IPropertyMapper
    {
        PropertyDTO PropertyToDTO(Property property);
        Property DTOToProperty(PropertyDTO propertyDTO);
        List<PropertyDTO> PropertyListToDTO(List<Property> properties);
        PropertyImage DTOToPropertyImage(PropertyImageDTO propertyImageDTO, string fileName);
        PropertyImageDTO PropertyImageToDTO(PropertyImage propertyImage, string path);
        PropertyTrace DTOToPropertyTrace(PropertyTraceDTO propertyTrace);
        PropertyTraceDTO PropertyTraceToDTO(PropertyTrace propertyTrace);

    }
}
