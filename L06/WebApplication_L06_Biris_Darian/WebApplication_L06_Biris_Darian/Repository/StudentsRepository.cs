﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_L06_Biris_Darian.Models;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication_L06_Biris_Darian.Repository
{
    public class StudentsRepository: IStudentsRepository
    {
        private CloudTableClient _tableClient;
        private CloudTable _studentsTable;
        private string _connectionString;

        private async Task InitializeTable()
        {
            var account = CloudStorageAccount.Parse(_connectionString);
            _tableClient = account.CreateCloudTableClient();
            _studentsTable = _tableClient.GetTableReference("student");
            await _studentsTable.CreateIfNotExistsAsync();
        }

        public StudentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue(typeof(string), "AzureStorageConnectionString").ToString();
            Task.Run(async () => { await InitializeTable(); })
                .GetAwaiter()
                .GetResult();
        }

        public async Task CreateStudent(StudentEntity student)
        {
            //var insertOperation = TableOperation.Insert(student);
            //await _studentsTable.ExecuteAsync(insertOperation);
            var jsonStudent = JsonConvert.SerializeObject(student);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonStudent);
            var base64String = System.Convert.ToBase64String(plainTextBytes);

            QueueClient queueClient = new QueueClient(
                _connectionString,
                "myqueue"
                );
            queueClient.CreateIfNotExists();

            await queueClient.SendMessageAsync(base64String);
        }

        public async Task UpdateStudent(JObject update)
        {
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>().Where(TableQuery.CombineFilters(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, update["RowKey"].ToString()), TableOperators.And, TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, update["PartitionKey"].ToString())));
            TableContinuationToken token = null;
            TableQuerySegment<StudentEntity> resultSegment = await _studentsTable.ExecuteQuerySegmentedAsync(query, token);
            token = resultSegment.ContinuationToken;
            switch (update["Field"].ToString())
            {
                case "FirstName":
                    resultSegment.Results[0].FirstName = update["Value"].ToString();
                    break;
                case "LastName":
                    resultSegment.Results[0].LastName = update["Value"].ToString();
                    break;
                case "Faculty":
                    resultSegment.Results[0].Faculty = update["Value"].ToString();
                    break;

            }
            var updateOperation = TableOperation.Replace(resultSegment.Results[0]);
            await _studentsTable.ExecuteAsync(updateOperation);
        }

        public async Task DeleteStudent(JObject delete)
        {
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>().Where(TableQuery.CombineFilters(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, delete["RowKey"].ToString()), TableOperators.And, TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, delete["PartitionKey"].ToString())));
            TableContinuationToken token = null;
            TableQuerySegment<StudentEntity> resultSegment = await _studentsTable.ExecuteQuerySegmentedAsync(query, token);
            token = resultSegment.ContinuationToken;
            var deleteOperation = TableOperation.Delete(resultSegment.Results[0]);
            await _studentsTable.ExecuteAsync(deleteOperation);
        }

        public async Task<List<StudentEntity>> GetAllStudents()
        {
            var students = new List<StudentEntity>();
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>();
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await _studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                students.AddRange(resultSegment);
            } while (token != null);
            return students;
        }
    }
}
