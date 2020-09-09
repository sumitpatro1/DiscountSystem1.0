using NUnit.Framework;
using PromotionSystem1._0.Core;
using PromotionSystem1._0.Services;
using System;
using System.Collections.Generic;

namespace PromotionSystemTestProject
{
    public class Tests
    {
        PromotionService promotionSvc;
        [SetUp]
        public void Setup()
        {
            promotionSvc = new PromotionService();
            promotionSvc.PromotionList = new List<Tuple<string, float, PromotionTypes>>()
            {
                Tuple.Create<string, float, PromotionTypes>("3A", 2 , PromotionTypes.MULTI ),
                Tuple.Create<string, float, PromotionTypes>( "A+B+C", 4, PromotionTypes.CUMULATIVE )
            };
            promotionSvc.RateChart = new Dictionary<char, float>()
            {
                { 'A', 1 },
                { 'B', 2 },
                { 'C', 3 },
                { 'D', 4 }
            }; ;
        }

        [Test]
        public void TestDisplayRateChart()
        {
            try
            {
                promotionSvc.DisplayRateChart();
                Assert.Pass();
            }
            catch (SuccessException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestDisplayPromotionList()
        {
            try
            {
                promotionSvc.DisplayPromotionList();
                Assert.Pass();
            }
            catch (SuccessException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestCalculatePromotionDefault()
        {
            try
            {
                promotionSvc.CalculatePromotion();
                Assert.Pass();
            }
            catch (SuccessException)
            {

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}