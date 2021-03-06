﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesCRUD.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 5;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}