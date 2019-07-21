using CommandLine;
using System;
using System.IO;

namespace rangers.antidrift.drift.Arguments
{
    public class ArgumentOptions
    {
        private string configurationFilePath;
        private string serviceUrl;

        [Option('c', "config-file",
            Required = true,
            HelpText = "Path to the configuration file.")]
        public string ConfigurationFilePath
        {
            get
            {
                return configurationFilePath;
            }

            set
            {
                string fullpath = Path.GetFullPath(value);

                if (!File.Exists(fullpath))
                {
                    throw new FileNotFoundException($"Configuration file was not found at '{fullpath}' ", fullpath);
                }

                configurationFilePath = fullpath;
            }
        }

        [Option('s', "service-url",
            Required = true,
            HelpText = "URL to the service you will connect to, e.g. https://youraccount.visualstudio.com/DefaultCollection")]
        public string ServiceUrl
        {
            get { return serviceUrl; }
            set
            {
                if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    throw new Exception($"URL '{value}' is not a valid URL.");
                }

                serviceUrl = value;
            }
        }

        [Option('a', "auth-type",
            Default = AuthType.Pat,
            HelpText = "Method of authentication")]
        public AuthType AuthType { get; set; }

        [Option('t', "token",
            Required = true,
            SetName = "pat",
            HelpText = "Personal access token. Valid only if method of authentication is set to PAT.")]
        public string Token { get; set; }

        [Option('u', "username",
            Required = true,
            SetName = "basic",
            HelpText = "Username to use for basic authentication. Valid only if method of authentication is set to Basic.")]
        public string Username { get; set; }

        [Option('p', "password",
            Required = true,
            SetName = "basic",
            HelpText = "Password to use for basic authentication. Valid only if method of authentication is set to Basic.")]
        public string Password { get; set; }
    }
}
