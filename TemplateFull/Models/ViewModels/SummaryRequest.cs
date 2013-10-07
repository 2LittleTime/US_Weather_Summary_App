using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateFull.Models.Interfaces;

namespace TemplateFull.Models.ViewModels
{
    public class SummaryRequest : ISummaryRequest
    {
        // public properties
        public IDictionary<string, int?> DateDict { get; private set; }
        public string StationName { get; private set; }

        // private nullable int dictionary
        private IDictionary<string, int?> _dateDict;

        // constructor
        public SummaryRequest(string stationName, int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear)
        {
            // set StationName
            StationName = stationName;

            // need IDictionary object
            _dateDict = new Dictionary<string, int?>();

            // populate private dictionary
            _dateDict.Add("beginMonth", beginMonth);
            _dateDict.Add("beginDay", beginDay);
            _dateDict.Add("beginYear", beginYear);
            _dateDict.Add("endMonth", endMonth);
            _dateDict.Add("endDay", endDay);
            _dateDict.Add("endYear", endYear);

            // push private dictionary to public property
            DateDict = _dateDict;
        }
    }
}