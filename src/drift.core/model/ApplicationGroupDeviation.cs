namespace Rangers.Antidrift.Drift.Core
{
    public class ApplicationGroupDeviation : Deviation
    {
        public ApplicationGroup ApplicationGroup { get; set; }
        
        public DeviationType Type { get; set; }
    }
}