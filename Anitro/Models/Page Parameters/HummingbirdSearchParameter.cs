﻿using AnimeTrackingServiceWrapper.UniversalServiceModels;
using Anitro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anitro.Models.Page_Parameters
{
    public class HummingbirdSearchParameter
    {
        public HummingbirdUser User;

        public string SearchTerms;
        public SearchFilter Filter;
    }
}
