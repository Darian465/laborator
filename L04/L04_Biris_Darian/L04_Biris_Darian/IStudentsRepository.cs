
using L04_Biris_Darian.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L04_Biris_Darian.Models;

namespace L04_Biris_Darian
{
    public interface IStudentsRepository
    {
        Task<List<StudentEntity>> GetAllStudents();
        Task CreateStudent(StudentEntity student);
        Task DeleteStudent(JObject delete);
        Task UpdateStudent(JObject update);
    }
}
