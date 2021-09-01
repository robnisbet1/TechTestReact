namespace Parmenion.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Parmenion.Models;
    using ThirdPartySDK;

    [Route("/api/growth")]
    [ApiController]
    public class GrowthController : ControllerBase
    {
        private readonly IFundDataService fundDataService;

        public GrowthController(IFundDataService fundDataService)
        {
            this.fundDataService = fundDataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Growth>> CalculateGrowth(string fundId, decimal initialInvestment, decimal monthlyInvestment, int yearsToInvest)
        {
            var fund = fundDataService.GetFund(fundId).Result;

            var firstMonth = new Growth
            {
                Invested = initialInvestment,
                HighGrowth = initialInvestment,
                LowGrowth = initialInvestment,
                MediumGrowth = initialInvestment,
                MonthOfInvestment = 0
            };

            var results = Enumerable.Range(1, yearsToInvest * 12)
                .Aggregate(new List<Growth> { firstMonth }, (growths, month) =>
                {
                    var lastMonth = growths.Last();
                    var thisMonth = new Growth
                    {
                        MonthOfInvestment = month,
                        Invested = lastMonth.Invested + monthlyInvestment,
                        LowGrowth = (lastMonth.LowGrowth * (1 + (fund.LowGrowth / 12))) + monthlyInvestment,
                        MediumGrowth = (lastMonth.MediumGrowth * (1 + (fund.ExpectedGrowth / 12))) + monthlyInvestment,
                        HighGrowth = (lastMonth.HighGrowth * (1 + (fund.HighGrowth / 12))) + monthlyInvestment,
                    };
                    growths.Add(thisMonth);
                    return growths;
                });

            return results;
        }
    }
}
