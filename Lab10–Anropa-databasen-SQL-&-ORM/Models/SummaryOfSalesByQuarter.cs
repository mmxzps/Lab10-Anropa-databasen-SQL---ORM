using System;
using System.Collections.Generic;

namespace Lab10_Anropa_databasen_SQL___ORM.Models
{
    public partial class SummaryOfSalesByQuarter
    {
        public DateTime? ShippedDate { get; set; }
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
