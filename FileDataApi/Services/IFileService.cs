using FileDataApi.Responses;

namespace FileDataApi.Services
{
    public interface IFileService
    {
        ProcessedFilesResponse ProcessFiles(List<IFormFile> files);
    }
}
