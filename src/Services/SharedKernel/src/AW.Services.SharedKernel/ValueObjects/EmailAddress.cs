using CSharpFunctionalExtensions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AW.Services.SharedKernel.ValueObjects
{
    public record EmailAddress
    {
        public string? LocalPart { get; }
        public string? Domain { get; }

        private EmailAddress(string emailAddress)
        {
            LocalPart = emailAddress[..emailAddress.IndexOf("@")];
            Domain = emailAddress[(emailAddress.IndexOf("@") + 1)..];
        }

        private EmailAddress() { }

        public static Result<EmailAddress> Create(string emailAddress)
        {              
            emailAddress = (emailAddress ?? string.Empty).Trim();

            if (IsValidEmailAddress(emailAddress) == false)
                return Result.Failure<EmailAddress>("Email address is invalid");

            return Result.Success(new EmailAddress(emailAddress));
        }

        //See https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        private static bool IsValidEmailAddress(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return false;

            try
            {
                // Normalize the domain
                emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(
                    emailAddress,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, 
                    TimeSpan.FromMilliseconds(250)
                );
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}