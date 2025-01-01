﻿using StockKube.Core.Enums;
using StockKube.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.ExternalSources
{
    public interface ISourceDataProvider
    {
        Task<RateDTO> GetRateAsync(SymbolDTO symbol);
    }
}