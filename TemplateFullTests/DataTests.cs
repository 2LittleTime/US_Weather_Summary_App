using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using TemplateFull.Models;
using TemplateFull.Models.ViewModels;
using TemplateFull.Models.Interfaces;
using TemplateFull.Controllers;


namespace TemplateFullTests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void CreateDbContext()
        {
            // checks if an EF database context can be created
            
            // arrange - create db context
            QclcdEntities db = new QclcdEntities();

            // act

            // assert - created and of correct type
            Assert.IsNotNull(db);
            Assert.IsInstanceOfType(db, typeof(QclcdEntities));

        }

        [TestMethod]
        public void TestStationTable()
        {
            // Station table can be accessed and queried

            // arrange - create db context
            QclcdEntities db = new QclcdEntities();

            // act - create station list
            var stations = from s in db.Stations select s;

            // assert - check if stations list created and has records
            Assert.IsNotNull(stations);
            Assert.AreNotEqual((int)0, stations.Count());

        }

        [TestMethod]
        public void TestDateRefTable()
        {
            // DateReference table can be accessed and queried

            // arrange - create db context
            QclcdEntities db = new QclcdEntities();

            // act- create date reference list
            var dateRefs = from d in db.DateReferences select d;

            // assert - check if date reference list created and has records
            // should have 365 rows
            Assert.IsNotNull(dateRefs);
            Assert.AreNotEqual((int)0, dateRefs.Count());
            Assert.AreEqual((int)365, dateRefs.Count());

        }

        [TestMethod]
        public void TestDailySummaryTable()
        {
            // DailySummary table can be accessed and queried

            // arrange - create db context
            QclcdEntities db = new QclcdEntities();

            // act - create daily summary record count
            int dailySummaryCount = (from d in db.DailySummaries select d).Count();

            // assert - check if daily summary record count around 2.5M rows
            Assert.IsNotNull(dailySummaryCount);
            Assert.AreNotEqual((int)0, dailySummaryCount);
            Assert.IsTrue(dailySummaryCount > 2500000);
        }

        
    }
}
