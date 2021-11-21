using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_L06_Biris_Darian.Models;

namespace WebApplication_L06_Biris_Darian
{
    public interface IStudentsRepository
    {
        Task<List<StudentEntity>> GetAllStudents();
        Task CreateStudent(StudentEntity student);
        Task DeleteStudent(JObject delete);
        Task UpdateStudent(JObject update);
    }
}
