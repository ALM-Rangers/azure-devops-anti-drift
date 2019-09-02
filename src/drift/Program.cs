// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift
{
    using System;
    using System.Threading.Tasks;
    using CommandLine;
    using Microsoft.VisualStudio.Services.Client;
    using Microsoft.VisualStudio.Services.Common;
    using Microsoft.VisualStudio.Services.WebApi;
    using Rangers.Antidrift.Drift.Arguments;
    using Rangers.Antidrift.Drift.Core;

    /// <summary>
    /// Anti-drift execution class.
    /// </summary>
    public static class Program
    {
        private const int ErrorBadArguments = 0xA0;

        /// <summary>
        /// Entry point of the command-line utility.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            using (var parser = new Parser(with =>
            {
                with.CaseSensitive = false;
                with.CaseInsensitiveEnumValues = true;
                with.EnableDashDash = true;
                with.HelpWriter = Console.Out;
            }))
            {
                ArgumentOptions options = null;

                parser.ParseArguments<ArgumentOptions>(args)
                    .WithParsed(opts => options = opts)
                    .WithNotParsed(errors => Environment.Exit(ErrorBadArguments));

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
                        credentials = new VssClientCredentials(
                            new WindowsCredential(false),
                            new VssFederatedCredential(false),
                            CredentialPromptType.PromptIfNeeded);
                        break;
                    default:
                        break;
                }

                VssConnection connection = new VssConnection(baseUri, credentials);

                try
                {
                    var container = Container.Build(connection);

                    var organization = new Organization(); // TODO: Use Factory method to generate org from yaml/json files and container.
                    // TODO: Expand patterns so we can use expressions, like [$teampProject]\Project Administrators
                    organization.Expand();

                    var deviations = await organization.CollectDeviations();

                    // TODO: Decide what to do with the deviations (print to screen/file) or remediate them.
                }
                catch (Exception)
                {
                    // TODO: handle Exception
                    throw;
                }
            }
        }
    }
}
