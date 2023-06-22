using FluentValidation;
using Shortener1.DTO.Urls;

namespace Shortener1;

public class UrlInputDtoValidator : AbstractValidator<UrlInputDto>
{
    public UrlInputDtoValidator()
    {
        RuleFor(dto => dto.Url)
            .NotEmpty().WithMessage(ResponseMessages.UrlValidatorStringIsMandatory)
            .Must(IsValidUrl).WithMessage(ResponseMessages.UrlValidatorFailedMessageInvalidStructure);
    }
    private static bool IsValidUrl(string url)
    {
        try
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var result))
            {
                return result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}   