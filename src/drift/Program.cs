using CommandLine;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using rangers.antidrift.drift.Arguments;
using System;
using System.Threading.Tasks;

namespace rangers.antidrift.drift
{
    class Program
    {
        private const int ERROR_BAD_ARGUMENTS = 0xA0;

        static async Task Main(string[] args)
        {
            Parser parser = new Parser(with =>
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
                default:
                    break;
            }

            VssConnection connection = new VssConnection(baseUri, credentials);

            try
            {
                await connection.ConnectAsync();
            }
            catch (Exception)
            {
                // handle Exception
                throw;
            }
        }
    }
}
