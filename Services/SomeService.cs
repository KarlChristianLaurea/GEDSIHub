namespace GADApplication.Services
{
    public class SomeService
    {
        private readonly IConfiguration _configuration;

        public SomeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PrintDatabaseSettings()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);
        }
    }
}
