﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Data
{
    public static class ApplicationConstants
    {
        public const int MINIMUM_PASSWORD_LENGTH = 8;

        public const decimal DEFAULT_GROSS_RATE = 4000m;

        public const decimal RATE_VALUE_DIFFERENCE = 0.75m;

        public const decimal MAX_PERCENTAGE_DEDUCTION_VALUE = 1m;

        public const string EMPLOYEE_IS_NOT_PAID = "Not Paid";
       
        public const string EMPLOYEE_IS_PAID = "Paid";
        
        public const string EMPLOYEE_IS_PENDING = "Pending";

        public static Dictionary<int, string> MONTH_LIST = new Dictionary<int, string>()
        {
            {1, "Styczeń"},
            {2, "Luty"},
            {3, "Marzec"},
            {4, "Kwiecień"},
            {5, "Maj"},
            {6, "Czerwiec"},
            {7, "Lipiec"},
            {8, "Sierpień"},
            {9, "Wrzesień"},
            {10, "Październik"},
            {11, "Listopad"},
            {12, "Grudzień"}
        };
    }
}
