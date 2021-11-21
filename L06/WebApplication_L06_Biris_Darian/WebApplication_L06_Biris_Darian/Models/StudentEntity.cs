using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication_L06_Biris_Darian.Models
{
    public class StudentEntity: TableEntity
    {
        public StudentEntity(string university, string cnp)
        {
            this.PartitionKey = university;
            this.RowKey = cnp;
        }
        public StudentEntity() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
    }
}
