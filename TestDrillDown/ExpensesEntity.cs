namespace TestDrillDown
{
    public class ExpensesEntity
    {
        public int Lvl { get; set; }
        public int OuId { get; set; }
        public string OuName { get; set; }
        public string ExpenseName { get; set; }
        public decimal ValueTotal { get; set; }
        public int  Year { get; set; }
        public int Month { get; set; }

    }
}