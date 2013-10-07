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
    public class PrecipApiController : ApiController
    {
        // build EF db context object
        private QclcdEntities db = new QclcdEntities();

        public IEnumerable<usp_PrecipSummary_Result> GetPrecipSummary(string wBan, int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear)
        {
            
            // call precip summary sproc
            var precipSummary = from t in db.usp_PrecipSummary(wBan, beginMonth, beginDay, beginYear, endMonth, endDay, endYear) select t;

            //  return result set
            return precipSummary;
        }

        //destroy db context object
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
