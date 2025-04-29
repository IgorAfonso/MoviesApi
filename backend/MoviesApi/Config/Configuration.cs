namespace MoviesApi.Config
{
    public class Configuration
    {
        private static IConfiguration _configuration = null!;
        private static IConfiguration Conf
        {
            get
            {
                if(_configuration is null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                }
                return _configuration;
            }
        }
        public static string? GetSectionValue(string section, string value) => 
            Conf
            .GetSection(section)
            .GetValue<string>(value);
    }
}
