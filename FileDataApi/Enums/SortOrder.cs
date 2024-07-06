using System.Runtime.Serialization;
using System.Text;

namespace FileDataApi.Enums
{
    public enum SortOrder
    {
        [EnumMember(Value = "asc")]
        Asc,

        [EnumMember(Value = "desc")]
        Desc
    }
}
