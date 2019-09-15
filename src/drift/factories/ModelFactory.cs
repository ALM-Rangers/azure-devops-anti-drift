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
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using Rangers.Antidrift.Drift.Core;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class ModelFactory : IModelFactory
    {
        private readonly IObjectFactory factory;

        public ModelFactory(IObjectFactory factory)
        {
            this.factory = factory;
        }

        public async Task<Organization> Create(params string[] files)
        {  
            var combined = string.Join("\n", files.Select(f => File.ReadAllText(f)));

            return await Task.Factory.StartNew(() =>
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(new PascalCaseNamingConvention())
                    .WithObjectFactory(this.factory)
                    .WithTagMapping("!Security", typeof(SecurityPattern))
                    .Build();

                return deserializer.Deserialize<Organization>(combined);
            }).ConfigureAwait(false);
        }
    }
}