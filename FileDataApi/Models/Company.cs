namespace FileDataApi.Models
{
    public class Company
    {
        public string CompanyName { get; set; }
        public int YearsInBusiness { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
    }
}
