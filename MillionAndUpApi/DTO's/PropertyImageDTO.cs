using MillionAndUpApi.Enums;
using MillionAndUpApi.Helpers;

namespace MillionAndUpApi.DTO_s
{
    public class PropertyImageDTO
    {
        public string IdPropertyImage { get; set; }

        [ValidateMaxWeightImg(4)]
        [ValidateFileType(FileTypes.image)]
        public IFormFile File { get; set; }
        public bool Enabled { get; set; } = false;
        public string IdProperty { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}
