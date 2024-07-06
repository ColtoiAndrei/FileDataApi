using FileDataApi.Models;

namespace FileDataApi.Responses
{
    public class ProcessedFilesResponse
    {
        public List<string> ValidFiles { get; set; }
        public List<string> InvalidFiles { get; set; }
        public List<Company> Companies { get; set; }
    }
}
