using Microsoft.Extensions.Hosting;

namespace ConsoleApp_L06_Biris_Darian
{
    class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}
