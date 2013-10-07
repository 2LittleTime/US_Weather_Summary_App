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
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using PagedList;

namespace TemplateFullTests
{
    /// <summary>
    /// Summary description for ControllerTests
    /// </summary>
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void TestAvgWindApiController()
        {
            // Test AvgWindApiController
            // Should be able to query and return records

            // arrange - create the controller
            AvgWindApiController controller = new AvgWindApiController();

            // act - call the method to retreive the summary
            var windResult = controller.GetAvgWindSummary("13904", 7, 5, 2013, 7, 10, 2013);

            // assert - should be not null with 6 records
            Assert.IsNotNull(windResult);
            Assert.AreEqual((int)6, windResult.Count());
        }

        [TestMethod]
        public void TestPrecipApiController()
        {
            // Test PrecipApiController
            // Should be able to query and return records

            // arrange - create the controller
            PrecipApiController controller = new PrecipApiController();

            // act - call the method to retreive the summary
            var precipResult = controller.GetPrecipSummary("13904", 7, 5, 2013, 7, 10, 2013);

            // assert - should be not null with 6 records
            Assert.IsNotNull(precipResult);
            Assert.AreEqual((int)6, precipResult.Count());
        }

        [TestMethod]
        public void TestTempApiController()
        {
            // Test TempApiController
            // Should be able to query and return records

            // arrange - create the controller
            TempApiController controller = new TempApiController();

            // act - call the method to retreive the summary
            var tempResult = controller.GetTempSummary("13904", 7, 5, 2013, 7, 10, 2013);

            // assert - should be not null with 6 records
            Assert.IsNotNull(tempResult);
            Assert.AreEqual((int)6, tempResult.Count());
        }

        [TestMethod]
        public void TestStationNameApiController()
        {
            // Test StationNameApiController
            // Should be able to query and return records

            // arrange - create the controller
            StationNameApiController controller = new StationNameApiController();

            // act - call the method to retreive the summary
            var stationsResult = controller.GetStationNames("TX");

            // act - call the method to retreive when empty
            var stationsResultEmpty = controller.GetStationNames("");

            // assert - should be not null with 6 records
            Assert.IsNotNull(stationsResult);
            Assert.IsTrue(stationsResult.Count() > 80);
            Assert.IsNotNull(stationsResultEmpty);
            Assert.IsTrue(stationsResultEmpty.Count() == 0);
        }

        [TestMethod]
        public void TestStationViewController()
        {
            // Check functions of station view controller
           
            // arrange - create the station view controller
            StationViewController svc = new StationViewController();

            // act - call nulls into Index() method
            ViewResult vr = svc.Index(null, null, null, null);

            // assert - initial null inputs version of view result
            Assert.IsNotNull(vr);
            Assert.AreEqual("StationView", vr.ViewName);
            Assert.IsInstanceOfType(vr.Model, typeof(IStationView));

            // arrange - create the station view controller
            StationViewController svcP = new StationViewController();

            // act - call values into Index() method
            // constructor signature - public ViewResult Index(string sortOrder, string currentFilter, int? page, string stationState)
            ViewResult vrP = svcP.Index("State", "TX", 2, "TX");

            // assert - initial null inputs version of view result
            Assert.IsNotNull(vrP);
            Assert.AreEqual("StationView", vrP.ViewName);
            Assert.IsInstanceOfType(vrP.Model, typeof(IStationView));
        }

        [TestMethod]
        public void TestSummaryChartsController()
        {
            // Check functions of summary charts controller

            // arrange - create the summary chart controller
            SummaryChartsController scc = new SummaryChartsController();

            // act - call nulls into Index() method
            ActionResult ar = scc.Index();

            // assert - initial null inputs version of view result
            Assert.IsNotNull(ar);
            Assert.IsInstanceOfType(ar, typeof(ActionResult));
        }

        [TestMethod]
        public void TestSummaryTablesController()
        {
            // Check functions of summary tables controller

            // arrange - create the summary tables controller
            SummaryTablesController stc = new SummaryTablesController();

            // act - call nulls into Index() method
            // method signature - public ActionResult Index(int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear, string stationState, string stationName)
            ActionResult ar = stc.Index(null, null, null, null, null, null, null, null);

            // assert - initial null inputs version of view result
            Assert.IsNotNull(ar);
            Assert.IsInstanceOfType(ar, typeof(ActionResult));

            // arrange - create the summary tables controller
            SummaryTablesController stcP = new SummaryTablesController();

            // act - call nulls into Index() method
            // method signature - public ActionResult Index(int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear, string stationState, string stationName)
            ActionResult arP = stcP.Index(7, 5, 2013, 7, 10, 2013, "TX", "13904");

            // assert - initial null inputs version of view result
            Assert.IsNotNull(arP);
            Assert.IsInstanceOfType(arP, typeof(ActionResult));
        }
    }
}
