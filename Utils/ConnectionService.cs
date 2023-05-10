using Microsoft.Extensions.Configuration;

namespace DLL_DAO
{
    public class ConnectionService
    {
        public static string GetConnectionString()
        {
            // Chemin vers l'executable
            string workingDirectory = Environment.CurrentDirectory;
            
            // Chemin "raccourci" vers le dossier GSB_Contrat
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            var configuration = new ConfigurationBuilder()
            .SetBasePath(projectDirectory)
            .AddJsonFile($"appsettings.json");

            var config = configuration.Build();
            var connectionString = config.GetConnectionString("Connection");
            return connectionString;
        }
    }
}
