using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ConsoleApp_L06_Biris_Darian
{
    public static class HttpTrigger1
    {
        [FunctionName("HttpTrigger1")]
        [TableOutput("student")]
        public static StudentEntity Run([QueueTrigger("myqueue", Connection = "StudentBirisDarian")] string myQueueItem,
            FunctionContext context)
        {
            var logger = context.GetLogger("HttpTrigger1");
            var student = JsonConvert.DeserializeObject<StudentEntity>(myQueueItem);
            return student;
        }
    }
}