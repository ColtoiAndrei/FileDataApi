using FileDataApi.Models;

namespace FileDataApi.Requests
{
    public class SortCompaniesRequest
    {
        public List<Company> Companies { get; set; }
        public SortOptions SortOptions { get; set; }
    }
}
