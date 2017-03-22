﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models.Parameter
{
    public class PageParameter
    {
        public PageParameter() : this(1, 20)
        {
        }

        public PageParameter(int page, int limit)
        {
            PageIndex = page < 1 ? 1 : page;
            PageSize = limit < 1 ? 20 : limit;
        }
        public PageParameter(int? page, int? limit)
        {
            PageIndex = page.HasValue ? page.Value < 1 ? 1 : page.Value : 1;
            PageSize = limit.HasValue ? limit.Value < 1 ? 20 : limit.Value : 20;
        }

        [JsonProperty("total")]
        public int RecordCount { get; set; }

        [JsonProperty("current")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount
        {
            get
            {
                var count = RecordCount / PageSize + (RecordCount % PageSize > 0 ? 1 : 0);
                return count < 1 ? 1 : count;
            }
        }
    }
}
