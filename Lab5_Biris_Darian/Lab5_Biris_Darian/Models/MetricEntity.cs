﻿using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_Biris_Darian.Models
{
    class MetricEntity:TableEntity
    {
        public MetricEntity(string university, string timestamp)
        {
            this.PartitionKey = university;
            this.RowKey = timestamp;
        }
        public MetricEntity() { }
        public int Count { get; set; }

    }
}