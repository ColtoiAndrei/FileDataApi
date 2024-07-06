using FileDataApi.Enums;
using FileDataApi.Models;

namespace FileDataApi.Services
{
    public class SortService : ISortService
    {
        public List<Company> SortCompanies(List<Company> companies, SortOptions sortOptions)
        {
            switch (sortOptions.SortType)
            {
                case SortType.Company:
                    if (sortOptions.SortOrder == SortOrder.Asc)
                    {
                        return companies.OrderBy(c => c.CompanyName).ToList();
                    }
                    else
                    {
                        return companies.OrderByDescending(c => c.CompanyName).ToList();
                    }

                case SortType.Contact:
                    if (sortOptions.SortOrder == SortOrder.Asc)
                    {
                        return companies.OrderBy(c => c.ContactName).ToList();
                    }
                    else
                    {
                        return companies.OrderByDescending(c => c.ContactName).ToList();
                    }

                case SortType.YearsInBusiness:
                    if (sortOptions.SortOrder == SortOrder.Asc)
                    {
                        return companies.OrderBy(c => c.YearsInBusiness).ThenBy(c => c.CompanyName).ToList();
                    }
                    else
                    {
                        return companies.OrderByDescending(c => c.YearsInBusiness).ThenBy(c => c.CompanyName).ToList();
                    }

                default:
                    throw new ArgumentException("Invalid sort type specified.");
            }
        }
    }
}
