using FileDataApi.Models;

namespace FileDataApi.Services
{
    public interface ISortService
    {
        List<Company> SortCompanies(List<Company> companies, SortOptions sortOptions);
    }
}
