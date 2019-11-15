﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Sales_System.Common
{
    static class DbConnectionString
    {
        public const string DevelopmentConnection= @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SalesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string DefaultConnection { get; set; }
    }
}
