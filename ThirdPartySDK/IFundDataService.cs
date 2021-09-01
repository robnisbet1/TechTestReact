namespace ThirdPartySDK
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFundDataService
    {
        Task<IEnumerable<string>> AvailableFunds();

        Task<FundData> GetFund(string fundId);
    }
}
