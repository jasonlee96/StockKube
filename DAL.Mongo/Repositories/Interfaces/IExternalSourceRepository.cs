﻿using DAL.Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mongo.Repositories.Interfaces
{
    public interface IExternalSourceRepository
    {
        Task<List<ExternalSource>> GetAllExternalSourcesAsync();
    }
}