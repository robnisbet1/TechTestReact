namespace ThirdPartySDK
{
    using System;

    public class FundData
    {
        public FundData(string fundId, Random random)
        {
            FundId = fundId;
            LowGrowth = random.Next(5) * 0.01m;
            ExpectedGrowth = random.Next(5, 7) * 0.01m;
            HighGrowth = random.Next(7, 15) * 0.01m;
        }

        public string FundId { get; }
        public decimal LowGrowth { get; }
        public decimal ExpectedGrowth { get; }
        public decimal HighGrowth { get; }
    }
}
