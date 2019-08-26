namespace Rangers.Antidrift.Drift
{
    using System;
    using System.Threading.Tasks;
    using CommandLine;
    using Microsoft.VisualStudio.Services.Client;
    using Microsoft.VisualStudio.Services.Common;
    using Microsoft.VisualStudio.Services.WebApi;
    using Rangers.Antidrift.Drift.Arguments;

    class Program
    {
        private const int ERROR_BAD_ARGUMENTS = 0xA0;

        static async Task Main(string[] args)
        {
            using Parser parser = new Parser(with =>
             {
                 with.CaseSensitive = false;
                 with.CaseInsensitiveEnumValues = true;
                 with.EnableDashDash = true;
                 with.HelpWriter = Console.Out;
             });
            ArgumentOptions options = null;

            parser.ParseArguments<ArgumentOptions>(args)
                .WithParsed(opts => options = opts)
                .WithNotParsed(errors => Environment.Exit(ERROR_BAD_ARGUMENTS));

            Uri baseUri = new Uri(options.ServiceUrl);
            VssCredentials credentials = null;

            switch (options.AuthType)
            {
                case AuthType.Pat:
                    credentials = new VssBasicCredential(string.Empty, options.Token);
                    break;
                case AuthType.Basic:
                    credentials = new VssBasicCredential(options.Username, options.Password);
                    break;
                case AuthType.Ntlm:
                    credentials = new VssCredentials();
                    break;
                case AuthType.Interactive:
                    credentials = new VssClientCredentials(new WindowsCredential(false),
                                    new VssFederatedCredential(false),
                                    CredentialPromptType.PromptIfNeeded);
                    break;
                default:
                    break;
            }

            VssConnection connection = new VssConnection(baseUri, credentials);

            try
            {
                await connection.ConnectAsync().ConfigureAwait(false);

                // TODO: Expand patterns so we can use expressions, like [$teampProject]\Project Administrators
            }
            catch (Exception)
            {
                // handle Exception
                throw;
            }
        }
    }
}
