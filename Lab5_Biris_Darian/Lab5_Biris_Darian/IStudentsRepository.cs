using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab5_Biris_Darian.Models;

namespace Lab5_Biris_Darian
{
    public interface IStudentsRepository
    {
        Task<List<StudentEntity>> GetAllStudents();
    }
}
