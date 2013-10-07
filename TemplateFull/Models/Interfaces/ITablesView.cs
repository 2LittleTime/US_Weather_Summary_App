using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateFull.Models.Interfaces
{
    public interface ITablesView
    {
        // define result set properties
        List<usp_TempSummary_Result> TempSummary { get; }
        List<usp_PrecipSummary_Result> PrecipSummary { get; }
        List<usp_AvgWindSummary_Result> WindSummary { get; }
    }
}