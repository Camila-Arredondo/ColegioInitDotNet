namespace ColegioAPI
{
    public class Utils
    {
        public static string ConexionSQL()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("SQL");

            return connectionString;
        }
    }
}
