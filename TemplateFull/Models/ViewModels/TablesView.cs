using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateFull.Models.Interfaces;

namespace TemplateFull.Models.ViewModels
{
    public class TablesView : ITablesView, IDisposable
    {
        // create db context
        private QclcdEntities db = new QclcdEntities();

        // internal fields
        private string _stationName;
        private int? _beginMonth;
        private int? _beginDay;
        private int? _beginYear;
        private int? _endMonth;
        private int? _endDay;
        private int? _endYear;
        
        // define result set properties
        public List<usp_TempSummary_Result> TempSummary { get; private set; }
        public List<usp_PrecipSummary_Result> PrecipSummary { get; private set; }
        public List<usp_AvgWindSummary_Result> WindSummary { get; private set; }

        // constructor -- requires a SummaryRequest object
        public TablesView (ISummaryRequest summaryRequest) 
        {
            // set internal fields
            _stationName = summaryRequest.StationName;
            _beginMonth = summaryRequest.DateDict["beginMonth"];
            _beginDay = summaryRequest.DateDict["beginDay"];
            _beginYear = summaryRequest.DateDict["beginYear"];
            _endMonth = summaryRequest.DateDict["endMonth"];
            _endDay = summaryRequest.DateDict["endDay"];
            _endYear = summaryRequest.DateDict["endYear"];
            
            // build result lists
            TempSummary = GetTempSummary(_stationName, _beginMonth, _beginDay, _beginYear, _endMonth, _endDay, _endYear );
            PrecipSummary = GetPrecipSummary(_stationName, _beginMonth, _beginDay, _beginYear, _endMonth, _endDay, _endYear );
            WindSummary = GetWindSummary (_stationName, _beginMonth, _beginDay, _beginYear, _endMonth, _endDay, _endYear );
        }

        // methods to return results sets
        private List<usp_TempSummary_Result> GetTempSummary (string stationName, int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear ) 
        {
            var tempSummary = from t in db.usp_TempSummary(stationName, beginMonth, beginDay, beginYear, endMonth, endDay, endYear) select t;
            List<usp_TempSummary_Result> tempSummaryList = null;
            tempSummaryList = tempSummary.ToList();
            return tempSummaryList;
        }
        
        private List<usp_PrecipSummary_Result> GetPrecipSummary (string stationName, int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear ) 
        {
            var precipSummary = from p in db.usp_PrecipSummary(stationName, beginMonth, beginDay, beginYear, endMonth, endDay, endYear) select p;
            List<usp_PrecipSummary_Result> precipSummaryList = null;
            precipSummaryList = precipSummary.ToList();
            return precipSummaryList;
        }

        private List<usp_AvgWindSummary_Result> GetWindSummary (string stationName, int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear ) 
        {
            var windSummary = from w in db.usp_AvgWindSummary(stationName, beginMonth, beginDay, beginYear, endMonth, endDay, endYear) select w;
            List<usp_AvgWindSummary_Result> windSummaryList = null;
            windSummaryList = windSummary.ToList();
            return windSummaryList;
        }

        // dispose the db context object    
        public void Dispose()
        {
            db.Dispose();
        }

    }
}