using System;
using System.Configuration;
using Topshelf;

namespace MinInt.NotificacionesCorreo
{
    class Program
    {
        static void Main(string[] args)
        {
            //configure topshelf to use NotificacionesCorreoService as the service class
            HostFactory.Run(serviceConfig => 
                            {
                                //serviceConfig.UseNLog();

                                serviceConfig.Service<NotificacionesCorreoService>(
                                        serviceInstance => 
                                        {
                                            serviceInstance.ConstructUsing(
                                                () => new NotificacionesCorreoService());

                                            serviceInstance.WhenStarted(
                                                execute => execute.Start());

                                            serviceInstance.WhenStopped(
                                                execute => execute.Stop());
                                        }
                                    );

                                serviceConfig.SetServiceName(ConfigurationManager
                                                                        .AppSettings["ServiceName"].ToString());

                                serviceConfig.SetDisplayName(ConfigurationManager
                                                                        .AppSettings["DisplayName"].ToString());

                                serviceConfig.SetDescription(ConfigurationManager
                                                                        .AppSettings["Description"].ToString());

                                serviceConfig.StartAutomatically();

                                serviceConfig.EnableServiceRecovery(recoveryOption => 
                                        {
                                            //primer fallo
                                            recoveryOption
                                                    .RestartService(
                                                            Convert.ToInt32(
                                                                    ConfigurationManager
                                                                        .AppSettings["MinutosReiniciarServicio"].ToString()));

                                            //segundo fallo
                                            recoveryOption
                                                    .RestartService(
                                                            Convert.ToInt32(
                                                                    ConfigurationManager
                                                                        .AppSettings["MinutosReiniciarServicio"].ToString()));

                                            //tercer fallo
                                            recoveryOption
                                                    .RestartService(
                                                            Convert.ToInt32(
                                                                    ConfigurationManager
                                                                        .AppSettings["MinutosReiniciarServicio"].ToString()));

                                        });
                            });
        }
    }
}
