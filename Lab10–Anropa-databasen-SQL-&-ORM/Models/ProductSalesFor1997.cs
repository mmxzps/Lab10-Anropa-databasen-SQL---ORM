﻿using System;
using System.Collections.Generic;

namespace Lab10_Anropa_databasen_SQL___ORM.Models
{
    public partial class ProductSalesFor1997
    {
        public string CategoryName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal? ProductSales { get; set; }
    }
}
