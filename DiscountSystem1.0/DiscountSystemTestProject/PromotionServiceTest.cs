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
        public void TestCalculateTotalPricenDefault()
        {
            try
            {
                promotionSvc.CalculateTotalPrice(String.Empty);
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
        public void TestCalculateTotalPricenPositive()
        {
            try
            {
                var retVal = promotionSvc.CalculateTotalPrice("A");
                Assert.AreEqual(1,retVal);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestCalculateTotalPriceNegative()
        {
            try
            {
                var retVal = promotionSvc.CalculateTotalPrice("A");
                Assert.AreNotEqual(0, retVal);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetPurchaseListDefault()
        {
            try
            {
                promotionSvc.GetPurchaseList(String.Empty);
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
        public void TestGetPurchaseListPositive()
        {
            try
            {
                var actual = promotionSvc.GetPurchaseList("3A,B");
                Assert.AreEqual(new List<char>() { 'A','A','A','B'}, actual);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestGetPurchaseListNegative()
        {
            try
            {
                var actual = promotionSvc.GetPurchaseList("A");
                Assert.AreNotEqual(new List<char>() { 'B' }, actual);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetPromotionSimplifiedDefault()
        {
            try
            {
                promotionSvc.GetPromotionSimplified();
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
        public void TestGetPromotionSimplifiedPositive()
        {
            try
            {
                var actual = promotionSvc.GetPromotionSimplified();
                var expected = new Dictionary<List<char>, float>()
                    {
                        { new List<char>{ 'A','A','A'}, 2 },
                        { new List<char>{ 'A','B','C'}, 4 },
                    };

                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestGetPromotionSimplifiedNegative()
        {
            try
            {
                var actual = promotionSvc.GetPromotionSimplified();
                var expected = new Dictionary<List<char>, float>()
                    {
                        { new List<char>{ 'A','A','A'}, 2 },
                        { new List<char>{ 'A','B','C'}, 3 },
                    };
                Assert.AreNotEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestCalculateMinimumPossiblePriceDefault()
        {
            try
            {
                promotionSvc.CalculateMinimumPossiblePrice(null,0);
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
        public void TestCalculateMinimumPossiblePricePositive()
        {
            try
            {
                var purchaseList = new List<char>() { 'A' };
                var simplifiedPromotionList = new Dictionary<List<char>, float>()
                    {
                        { new List<char>{ 'A','A','A'}, 2 },
                        { new List<char>{ 'A','B','C'}, 3 },
                    };
                var actual = promotionSvc.CalculateMinimumPossiblePrice(purchaseList, simplifiedPromotionList);
                Assert.AreEqual(1, actual);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestCalculateMinimumPossiblePriceNegative()
        {
            try
            {
                var purchaseList = new List<char>() { 'A' };
                var simplifiedPromotionList = new Dictionary<List<char>, float>()
                    {
                        { new List<char>{ 'A','A','A'}, 2 },
                        { new List<char>{ 'A','B','C'}, 3 },
                    };
                var actual = promotionSvc.CalculateMinimumPossiblePrice(purchaseList, simplifiedPromotionList);
                Assert.AreEqual(0, actual);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}