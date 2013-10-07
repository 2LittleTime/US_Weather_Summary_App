using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateFull.Models.Interfaces;

namespace TemplateFull.Models
{
    public class WeatherSummaryFilter : IWeatherSummaryFilter, IDisposable
    {
        // db context object
        private QclcdEntities db = new QclcdEntities();

        // define public properties
        public string StationName { get; set; }
        public string StationState { get; set; }
        public string WBanId { get; set; }
        public int? BeginMonth { get; set; }
        public int? BeginDay { get; set; }
        public int? BeginYear { get; set; }
        public int? EndMonth { get; set; }
        public int? EndDay { get; set; }
        public int? EndYear { get; set; }
        public IEnumerable<SelectListItem> StationStates { get; private set; }
        public IEnumerable<SelectListItem> StationNames { get; private set; }
        public IEnumerable<SelectListItem> BeginMonthList { get; private set; }
        public IEnumerable<SelectListItem> BeginDayList { get; private set; }
        public IEnumerable<SelectListItem> BeginYearList { get; private set; }
        public IEnumerable<SelectListItem> EndMonthList { get; private set; }
        public IEnumerable<SelectListItem> EndDayList { get; private set; }
        public IEnumerable<SelectListItem> EndYearList { get; private set; }

        // constructor with values
        public WeatherSummaryFilter(int? beginMonth, int? beginDay, int? beginYear, int? endMonth, int? endDay, int? endYear, string stationState, string stationName)
        {
            BeginMonth = beginMonth;
            BeginDay = beginDay;
            BeginYear = beginYear;
            EndMonth = endMonth;
            EndDay = endDay;
            EndYear = endYear;
            StationStates = getStationStates();
            StationNames = getStationNames(stationState);
            BeginMonthList = getMonths();
            BeginDayList = getDays();
            BeginYearList = getYears();
            EndMonthList = getMonths();
            EndDayList = getDays();
            EndYearList = getYears();
            StationState = stationState;
            StationName = stationName;
        }

        // parameterless constructor
        public WeatherSummaryFilter()
        {
            StationStates = getStationStates();
            StationNames = getStationNames();
            BeginMonthList = getMonths();
            BeginDayList = getDays();
            BeginYearList = getYears();
            EndMonthList = getMonths();
            EndDayList = getDays();
            EndYearList = getYears();
        }

        // create state list - parameterless version
        private IEnumerable<SelectListItem> getStationStates()
        {
            //var stateList = from s in db.usp_StatesWithData() select s;
            var sl = db.usp_StatesWithData().Select(x => new SelectListItem { Text = x, Value = x });
            return sl.ToList();
        }

        // create station list - default to AK (first state) if null
        private IEnumerable<SelectListItem> getStationNames(string state)
        {
            if (state == null)
            {
                state = "AK";
            }
            
            // get and return stations list
            var sn = db.usp_StationNamesWithData(state).Select(x => new SelectListItem { Text = x.StationName, Value = x.WbanId });
            return sn.ToList();
        }

        // create station list - parameterless version
        private IEnumerable<SelectListItem> getStationNames()
        {
            //Stub new object with AK city list -- first value in state select list
            var sn = db.usp_StationNamesWithData("AK").Select(x => new SelectListItem { Text = x.StationName, Value = x.WbanId });
            return sn.ToList();
        }

        // default months list
        private IEnumerable<SelectListItem> getMonths()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Jan", Value = "1" });
            items.Add(new SelectListItem { Text = "Feb", Value = "2" });
            items.Add(new SelectListItem { Text = "Mar", Value = "3" });
            items.Add(new SelectListItem { Text = "Apr", Value = "4" });
            items.Add(new SelectListItem { Text = "May", Value = "5" });
            items.Add(new SelectListItem { Text = "Jun", Value = "6" });
            items.Add(new SelectListItem { Text = "Jul", Value = "7" });
            items.Add(new SelectListItem { Text = "Aug", Value = "8" });
            items.Add(new SelectListItem { Text = "Sep", Value = "9" });
            items.Add(new SelectListItem { Text = "Oct", Value = "10" });
            items.Add(new SelectListItem { Text = "Nov", Value = "11" });
            items.Add(new SelectListItem { Text = "Dec", Value = "12" });

            return items;
        }

        // default days list - 31 days
        private IEnumerable<SelectListItem> getDays()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            for (int i = 1; i <= 31; i++)
            {
                string textString = i.ToString();
                items.Add(new SelectListItem { Text = textString, Value = textString });
            }

            return items;
        }

        // default years list
        private IEnumerable<SelectListItem> getYears()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            for (int i = 2005; i <= 2013; i++)
            {
                string textString = i.ToString();
                items.Add(new SelectListItem { Text = textString, Value = textString });
            }

            return items;
        }

        // dispose db context
        public void Dispose()
        {
            db.Dispose();
        }
    }
}