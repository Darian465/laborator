using System;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp_L06_Biris_Darian
{
    public class StudentEntity : TableEntity
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
