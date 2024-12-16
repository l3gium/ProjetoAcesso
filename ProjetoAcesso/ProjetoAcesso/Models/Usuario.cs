using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ProjetoAcesso.Models
{
    // Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id;

        private string nome;
        private List<string> ambientes;

        public string Id => id;
        public string Nome { get => nome; set => nome = value; }
        public List<string> Ambientes { get => ambientes; set => ambientes = value; }

        public Usuario(string nome)
        {
            this.nome = nome;
            this.ambientes = new List<string>();
        }

        public bool ConcederPermissao(string ambienteId)
        {
            if (ambientes.Contains(ambienteId)) return false;
            ambientes.Add(ambienteId);
            return true;
        }

        public bool RevogarPermissao(string ambienteId)
        {
            return ambientes.Remove(ambienteId);
        }
    }
}
