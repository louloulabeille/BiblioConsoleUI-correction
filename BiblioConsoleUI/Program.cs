using Microsoft.Extensions.DependencyInjection;
using System;
using Bibliotheque.Repository;
using Bibliotheque.BLL;
using Bibliotheque.DALEF;
using Bibliotheque.DALADO;
using System.Configuration;
using Bibliotheque.BOL;

namespace BiblioConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                Configuration.TypeDAL = args[0];
                Configuration.TypeSGBD = args[1];
                Configuration.ConfigurerServices();
                GestionMenus();
                Configuration.DisposeServices();

            }
            else
            {
                throw new ApplicationException("Paramètres manquants /r/n Application ne peut être exécutée");
            }
        }

        private static void GestionMenus()
        {
            string Rep = "";
            do
            {
                Console.WriteLine("Choisir une options L pour Liste A pour Ajout S pour sortir");
                Rep = Console.ReadLine();
                switch (Rep)
                {
                    
                    case "L":
                     AdherentManager adhMan = new AdherentManager(Configuration._serviceProvider.GetService<IAdherentRepository>());
                        foreach (Adherent adh in adhMan.Lister())
                        {
                            Console.WriteLine($"Code {adh.IdAdherent} Nom {adh.NomAdherent} prénom {adh.PrenomAdherent}");
                        }
                        break;
                    case "A":
                     AdherentManager adhMan2 = new AdherentManager(Configuration._serviceProvider.GetService<IAdherentRepository>());
                        break;
                    default:
                        break;
                }
            } while (Rep!="S");
        }
    }

    public static class Configuration
    {
        internal static IServiceProvider _serviceProvider;
        internal static String TypeDAL { get; set; }
        internal static String TypeSGBD { get; set; }
        internal static void ConfigurerServices()
        {
            var collection = new ServiceCollection();
           
            
            if (TypeDAL == "DALADO")
            {
                collection.AddScoped<IAdherentRepository, AdherentADO>();
                if (TypeSGBD== "SqlServer")
                {
                    DBDAO.DbConnectionString = "Server=localhost;Database=Bibliotheque;Trusted_Connection=True;";
                    DBDAO.DbProviderName = "System.Data.SqlClient";
                    DBDAO.TypeSGBD = "SqlServer";
                }
                if (TypeSGBD == "MySQL")
                {
                    DBDAO.DbConnectionString = "server=localhost;database=Bibliotheque;user=96GB011;password=Wince1301";
                    DBDAO.DbProviderName = "MySql.Data.MySqlClient";
                    DBDAO.TypeSGBD = "MySQL";
                }
            }
            if (TypeDAL == "DALEF")
            {
                collection.AddScoped<IAdherentRepository, AdherentEF>();
                if (TypeSGBD == "SqlServer")
                {
                    DBEF.DbConnectionString = "Server=localhost;Database=Bibliotheque;Trusted_Connection=True;";
                    DBEF.DbProviderName = "System.Data.SqlClient";
                }
                if (TypeSGBD == "MySQL")
                {
                    DBEF.DbConnectionString = "server=localhost;database=Bibliotheque;user=96GB011;password=Wince1301";
                    DBEF.DbProviderName = "MySql.Data.MySqlClient";
                }
            }
           
            _serviceProvider = collection.BuildServiceProvider();
           

        }
        internal static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }

    }
}
