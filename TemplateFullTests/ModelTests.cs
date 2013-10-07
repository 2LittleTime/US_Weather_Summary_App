using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using TemplateFull.Models;
using TemplateFull.Models.ViewModels;
using TemplateFull.Models.Interfaces;
using TemplateFull.Controllers;
using Moq;


namespace TemplateFullTests
{
    /// <summary>
    /// Summary description for ModelTests
    /// </summary>
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void BuildSummaryRequestObject()
        {
            // this test attempts to build a summary request object
            // and verify the data properties returned
            
            // arrange - build a summary request object with values
            SummaryRequest sr = new SummaryRequest("13904", 7, 5, 2013, 7, 10, 2013);

            // act

            // assert - check returned values valid
            Assert.IsNotNull(sr);
            Assert.AreEqual(sr.StationName, "13904");
            Assert.AreEqual(sr.DateDict["beginMonth"], (int)7);
            Assert.AreEqual(sr.DateDict["beginDay"], (int)5);
            Assert.AreEqual(sr.DateDict["beginYear"], (int)2013);
            Assert.AreEqual(sr.DateDict["endMonth"], (int)7);
            Assert.AreEqual(sr.DateDict["endDay"], (int)10);
            Assert.AreEqual(sr.DateDict["endYear"], (int)2013);

            // arrange - build a summary request object with nulls
            SummaryRequest srNull = new SummaryRequest(null, null, null, null, null, null, null);
            
            // act

            // assert - check returned values valid
            // object itself should exist, but all properties are null
            Assert.IsNotNull(srNull);
            Assert.AreEqual(srNull.StationName, null);
            Assert.AreEqual(srNull.DateDict["beginMonth"], null);
            Assert.AreEqual(srNull.DateDict["beginDay"], null);
            Assert.AreEqual(srNull.DateDict["beginYear"], null);
            Assert.AreEqual(srNull.DateDict["endMonth"], null);
            Assert.AreEqual(srNull.DateDict["endDay"], null);
            Assert.AreEqual(srNull.DateDict["endYear"], null);
        }

        [TestMethod]
        public void BuildTablesViewObject()
        {
            // this test attempts to build a tables view object
            // and verify the data properties returned

            // arrange - mock up ISummaryRequest
            Mock<ISummaryRequest> mockSR = new Mock<ISummaryRequest>();

            // build dictionary for mock ISummaryRequest
            IDictionary<string, int?> dateDict = new Dictionary<string, int?>();

            // populate mock dictionary
            dateDict.Add("beginMonth", 7);
            dateDict.Add("beginDay", 5);
            dateDict.Add("beginYear", 2013);
            dateDict.Add("endMonth", 7);
            dateDict.Add("endDay", 10);
            dateDict.Add("endYear", 2013);
            
            // define mock properties for mock ISummaryRequest
            mockSR.SetupGet(m => m.StationName).Returns("13904");
            mockSR.SetupGet(m => m.DateDict).Returns(dateDict);

            //act - build a tables view from mock ISummaryRequest
            ITablesView tv = new TablesView(mockSR.Object);

            //assert - count returned list rows - should each be 6
            Assert.AreEqual(tv.PrecipSummary.Count(), 6);
            Assert.AreEqual(tv.TempSummary.Count(), 6);
            Assert.AreEqual(tv.WindSummary.Count(), 6);
        }

        [TestMethod]
        public void BuildStationViewObject()
        {
            // this test attempts to build a station view object
            // and verify the data properties returned

            // arrange  - build a stations list  - use Texas
            TemplateFull.Models.QclcdEntities db = new TemplateFull.Models.QclcdEntities();
            string stationState = "TX";
            var stations = (from s in db.Stations select s).Where(s => s.StationState.ToUpper().Contains(stationState.ToUpper()));

            // stations have to be sorted before being passed into a paged list
            stations = stations.OrderByDescending(s => s.WbanId);

            // create page number and page size variables
            int pgNum = 1;
            int pgSz = 20;

            // act - create the station view
            IStationView sv = new StationView(stations, pgNum, pgSz);

            // assert - verifiy the properties of the station view
            Assert.IsNotNull(sv);
            Assert.IsInstanceOfType(sv, typeof(IStationView));
            Assert.IsTrue(sv.StationPageList.PageCount > 0);
            Assert.IsTrue(sv.StationPageList.PageSize == 20);
        }

        [TestMethod]
        public void BuildWeatherSummaryFilterObject()
        {
            // this test attempts to build a weather summary filter object 
            // from both the parameterless and values recieved constructors
 
            // arrange - new parameterless version
            IWeatherSummaryFilter wsf = new WeatherSummaryFilter();

            // act 

            // assert - check if not null and for proper type
            Assert.IsNotNull(wsf);
            Assert.IsInstanceOfType(wsf, typeof(IWeatherSummaryFilter));
           
            // arrange - constructor with parameters
            //constructor signature - public WeatherSummaryFilter(int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear, string stationState, string stationName)
            IWeatherSummaryFilter wsfP = new WeatherSummaryFilter(7, 5, 2013, 7, 10, 2013, "TX", "13904");
            
            // act

            // assert - check if not null and for proper type and values
            Assert.IsNotNull(wsfP);
            Assert.IsInstanceOfType(wsfP, typeof(IWeatherSummaryFilter));
            Assert.IsTrue(wsfP.BeginMonth == 7);
            Assert.IsTrue(wsfP.BeginDay == 5);
            Assert.IsTrue(wsfP.BeginYear == 2013);
            Assert.IsTrue(wsfP.EndMonth == 7);
            Assert.IsTrue(wsfP.EndDay == 10);
            Assert.IsTrue(wsfP.EndYear == 2013);
            Assert.IsTrue(wsfP.StationName == "13904");
            Assert.IsTrue(wsfP.StationState == "TX");
        }



    }
}
