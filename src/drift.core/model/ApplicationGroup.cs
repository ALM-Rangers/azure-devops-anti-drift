using System.Collections.Generic;

namespace Rangers.Antidrift.Drift.Core
{
    public class ApplicationGroup
    {
        public string Descriptor { get; set; }

        public bool IsSpecial { get; set; }

        public string[] Members { get; set; } = new string[0];

        public string Name { get; set; }

        public IList<Namespace> Namespaces { get; set; } = new List<Namespace>();
    }
}