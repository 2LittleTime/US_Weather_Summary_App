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
    public class StationNameApiController : ApiController
    {
        // create db context
        private QclcdEntities db = new QclcdEntities();

        // define station names method
        public IEnumerable<usp_StationNamesWithData_Result> GetStationNames(string state)
        {
            // call sproc
            var states = from t in db.usp_StationNamesWithData(state) select t;
            
            // return result set 
            return states;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
