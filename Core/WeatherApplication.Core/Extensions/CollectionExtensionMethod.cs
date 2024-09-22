using System.Text;
using WeatherApplication.Core.Validation;

namespace WeatherApplication.Core.Extensions;
public static class CollectionExtensionMethod
{
    public static string ValidationErrorMessageWithNewLine(this List<ValidationErrorModel> errors)
    {
        StringBuilder sb = new();
        foreach (var error in errors)
        {
            sb.Append(error.ErrorMessage);
            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }
}

