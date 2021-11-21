﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_L06_Biris_Darian.Repository;
using WebApplication_L06_Biris_Darian.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;

namespace WebApplication_L06_Biris_Darian.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController: ControllerBase
    {
        private IStudentsRepository _studentsRepository;
       

        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentEntity>> Get()
        {
            return await _studentsRepository.GetAllStudents();
        }
        [HttpPost]
        public async Task Post([FromBody] StudentEntity student)
        {
            await _studentsRepository.CreateStudent(student);
        }
        [HttpPut]
        public async void Put([FromBody] Object obj)
        {
            await _studentsRepository.UpdateStudent(JObject.Parse(obj.ToString()));
        }
        [HttpDelete]
        public async Task Delete([FromBody] Object obj)
        {
            await _studentsRepository.DeleteStudent(JObject.Parse(obj.ToString()));
        }
    }
}
