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