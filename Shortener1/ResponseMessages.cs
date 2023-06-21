namespace Shortener1;

public static class ResponseMessages
{
    public const string UrlValidatorFailedMessageInvalidStructure = "Failed to create new shortened url! Url is Invalid!";
    public const string UrlCreatedFailedMessageUrlIsDuplicate = "Failed to create new shortened url! It is already exists!";
    public const string FailedToFindLongUrlFromShortened = "Long url for redirect was`t found! See massage: ";
    public const string UrlCreatedFailedMessage = "Failed to create new shortened url! See massage: ";
    public const string SuccessUrlCreatedMessage = "New shortened url was created!";
    public const string RegisterErrorEmailAlreadyExists = "Didn't register! Email already exists!";
    public const string RegisterErrorUserNameAlreadyExists = "Didn't register! User name already exists!";
    public const string RegisterErrorRegistrationFailed = "Didn't register!";
    public const string AuthenticateErrorIncorrectCredentials = "Username or password is incorrect";
    public const string MarketingDataErrorWrongMessageFormat = "Wrong message format";
    public const string UrlValidatorStringIsMandatory = "Failed to create new shortened URL, original URL is Mandatory";
}