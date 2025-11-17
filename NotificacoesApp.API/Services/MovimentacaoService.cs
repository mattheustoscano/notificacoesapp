using MongoDB.Driver;

namespace NotificacoesApp.API.Services
{
    /*
     * Classe de serviço para o MongoDB
     */
    public class MovimentacaoService
    {
        //Atributo para acessar a collection do MongoDB para onde iremos
        //gravar os dados da movimentação
        private readonly IMongoCollection<NotificacaoMovimentacaoRequest> _collection;

        //Construtor para realizar a conexão com o banco de dados
        public MovimentacaoService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("NotificacoesApp");
            var mongoSettings = configuration.GetSection("MongoSettings");
            var databaseName = mongoSettings["Database"];
            var collectionName = mongoSettings["Collection"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _collection = database.GetCollection<NotificacaoMovimentacaoRequest>(collectionName);
        }

        //Método para inserir a movimentação na collection
        public async Task RegistrarMovimentacao(NotificacaoMovimentacaoRequest request)
        {
            await _collection.InsertOneAsync(request);
        }
    }
}



