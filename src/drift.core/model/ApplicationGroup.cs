// -----------------------------------------------------------------------
// <copyright file="ApplicationGroup.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core
{
    using System.Collections.Generic;

    public class ApplicationGroup
    {
        public string Descriptor { get; set; }

        public bool IsSpecial { get; set; }

        public string[] Members { get; set; } = System.Array.Empty<string>();

        public string Name { get; set; }

        public IList<Namespace> Namespaces { get; } = new List<Namespace>();
    }
}