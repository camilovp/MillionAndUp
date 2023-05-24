namespace MillionAndUpApi.Interfaces.Helpers
{
    public interface IManageFiles
    {
        void SaveFile(IFormFile formFile, string path, string nameFolder);
    }
}
