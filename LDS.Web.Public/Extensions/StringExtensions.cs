using Microsoft.EntityFrameworkCore.Storage.Json;

namespace LDS.Web.Public.Extensions;

public static class StringExtensions
{
    public static string ToGenderDisplay(this string genderCode)
    {
        return genderCode switch
        {
            "M" => "Men",
            "F" => "Women",
            _ => "Unknown"
        };
    }
}