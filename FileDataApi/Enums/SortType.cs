using System.Runtime.Serialization;

namespace FileDataApi.Enums
{
    public enum SortType
    {
        [EnumMember(Value = "companyName")]
        CompanyName,

        [EnumMember(Value = "contactName")]
        ContactName,

        [EnumMember(Value = "yearsInBusiness")]
        YearsInBusiness
    }
}
