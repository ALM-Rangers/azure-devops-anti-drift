namespace Rangers.Antidrift.Drift.Core
{
    public class ApplicationGroupMemberDeviation : ApplicationGroupDeviation
    {
        public string Member { get; set; }

        public DeviationType Type { get; set; }
    }
}