using System;
using FagdagCqrs.Backend.Bootstrapping;
using Microsoft.Owin.Hosting;

namespace FagdagCqrs.Backend
{
    class ConsoleHost
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:8080";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
