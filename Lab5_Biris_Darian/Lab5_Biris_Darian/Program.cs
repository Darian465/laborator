using System;
using Lab5_Biris_Darian.Models;
using Lab5_Biris_Darian.Repository;
using System.Collections.Generic;

namespace Lab5_Biris_Darian
{
    class Program
    {
        private static IStudentsRepository _studentsRepository;
        private static IMetricRepository _metricRepository;
        static void Main(string[] args)
        {
            _studentsRepository = new StudentsRepository();
            _metricRepository = new MetricRepository(_studentsRepository.GetAllStudents().Result);
            _metricRepository.GenerateMetric();
        }
    }
}
