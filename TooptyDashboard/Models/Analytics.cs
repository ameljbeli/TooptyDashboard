using TooptyDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TooptyDashboard.Models
{
    public class AnalyticsViewModel
    {
        public List<OrderDateGroup> OrderData { get; set; }

        public List<OrderDateGroup> OrderDataForToday { get; set; }
    }
}