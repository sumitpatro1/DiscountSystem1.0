using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionSystem1._0.Services
{
    interface IPromotionService
    {
        float CalculateTotalPrice(string purchase);
        void DisplayRateChart();
        void DisplayPromotionList();
        List<char> GetPurchaseList(string purchase);
        Dictionary<List<char>, float> GetPromotionSimplified();
        float CalculateMinimumPossiblePrice(List<char> purchaseList, Dictionary<List<char>, float> simplifiedPromotionList);
    }
}
