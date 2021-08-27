namespace ThirdPartySDK
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFundData
    {
        Task<IEnumerable<string>> AvailableFunds();

        Task<FundData> GetFund(string fundId);
    }
}
