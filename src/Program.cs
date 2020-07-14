using log4net;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DebuggableWindowsService.Properties;


namespace DebuggableWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            //Set the working directory to the folder that this executable resides
            string exeName = Assembly.GetExecutingAssembly().Location;
            string currentDirectory = Path.GetDirectoryName(exeName);
            Environment.CurrentDirectory = currentDirectory;

            string serviceName;
            string serviceDescription;
            serviceName = Settings.Default.serviceName;
            serviceDescription = Settings.Default.serviceDescription;

            ILog logger = LogManager.GetLogger(typeof(Program));

            if (Environment.UserInteractive)
            {


                if (args.Length > 0)
                {
                    try
                    {
                        if (args[0].ToLower().Equals("/install"))
                        {
                            IDictionary ht = new Hashtable();
                            AssemblyInstaller installer = new AssemblyInstaller();
                            installer.Path = exeName;
                            installer.UseNewContext = true;
                            installer.Install(ht);
                            installer.Commit(ht);
                            logger.Info(serviceName + " installed.");
                            Console.ReadKey();
                            return;
                        }
                        else if (args[0].ToLower().Equals("/uninstall"))
                        {
                            IDictionary ht = new Hashtable();
                            AssemblyInstaller installer = new AssemblyInstaller();
                            installer.Path = exeName;
                            installer.UseNewContext = true;
                            installer.Uninstall(ht);
                            logger.Info(serviceName + " uninstalled.");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Valid arguments: /install and /uninstall");
                            Console.ReadKey();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("error installing service:", ex);
                        Console.ReadKey();
                        return;
                    }
                }

                logger.Info("Starting application...");

                SampleBackgroundService _backgroundService = new SampleBackgroundService();
                _backgroundService.Start();

                logger.Info("Application started. Press enter to stop...");
                Console.ReadLine();

                logger.Info("Stopping application...");
                _backgroundService.Stop();
                logger.Info("Stopped.");

            }
            else //Create and run the Windows service instance 
            {
                logger.Info("Starting service...");
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new SampleWindowsService()
                };
                ServiceBase.Run(ServicesToRun);
                logger.Info("Service started");
            }
        }
    }
}
