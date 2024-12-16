using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ProjetoAcesso.Models
{
    // Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
    public class Ambiente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id;

        private string nome;
        private Queue<Log> logs { get; set; } = new Queue<Log>();

        public string Id => id;
        public string Nome
        {
            get => nome;
            set => nome = value;
        }
        public IReadOnlyCollection<Log> Logs => logs;

        public Ambiente(string nome)
        {
            this.nome = nome;
            this.logs = new Queue<Log>();
        }

        public void RegistrarLog(Log log)
        {
            if (logs.Count >= 100) logs.Dequeue();
            logs.Enqueue(log);
        }
    }
}
