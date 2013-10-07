using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using TemplateFull.Models;

namespace TemplateFull.Controllers
{
    public class TempApiController : ApiController
    {
        //build EF db context object
        private QclcdEntities db = new QclcdEntities();

        public IEnumerable<usp_TempSummary_Result> GetTempSummary(string wBan, int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear)
        {
            // call temp summary sproc 
            var tempSummary = from t in db.usp_TempSummary(wBan, beginMonth, beginDay, beginYear, endMonth, endDay, endYear) select t;

            // return result set
            return tempSummary;
        }

        //destroy db context object
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
