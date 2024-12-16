using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProjetoAcesso.Models;

namespace ProjetoAcesso.Data
{
    // Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
    public class MongoDbContext
    {
        private readonly IMongoDatabase database;

        public MongoDbContext()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration["MongoDbSettings:ConnectionString"];
            string databaseName = configuration["MongoDbSettings:DatabaseName"];

            var client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Usuario> Usuarios => database.GetCollection<Usuario>("usuarios");
        public IMongoCollection<Ambiente> Ambientes => database.GetCollection<Ambiente>("ambientes");
    }
}
