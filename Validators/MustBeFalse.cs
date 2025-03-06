namespace  TodoApi.Validations;

using System.ComponentModel.DataAnnotations;

public class MustBeFalseAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is bool booleanValue && !booleanValue;
    }
}
