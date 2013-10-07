using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateFull.Models.Interfaces
{
    public interface ISummaryRequest
    {
        // define public properties
        string StationName { get; }
        IDictionary<string, int?> DateDict { get; }
    }
}