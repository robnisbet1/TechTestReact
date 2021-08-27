namespace ThirdPartySDK
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FundDataService : IFundData
    {
        private static readonly Random random = new Random();
        private static readonly Dictionary<string, FundData> funds = new Dictionary<string, FundData>
        {
            { "TSLA", new FundData("TSLA", random) },
            { "GOOG", new FundData("GOOG", random) },
            { "MSFT", new FundData("MSFT", random) },
            { "FBK", new FundData("FBK", random) },
            { "APPL", new FundData("APPL", random) },
            { "AMZN", new FundData("AMZN", random) },
            { "DELL", new FundData("DELL", random) },
        };

        public Task<FundData> GetFund(string fundId) => Task.FromResult(funds[fundId]);

        public Task<IEnumerable<string>> AvailableFunds() => Task.FromResult<IEnumerable<string>>(funds.Keys.ToList());
    }
}
