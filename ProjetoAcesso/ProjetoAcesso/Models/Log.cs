using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProjetoAcesso.Models
{
    // Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
    public class Log
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DtAcesso { get; set; }
        public string UsuarioId { get; set; }
        public bool TipoAcesso { get; set; }

        public Log() { }

        public Log(DateTime dtAcesso, string usuarioId, bool tipoAcesso)
        {
            DtAcesso = dtAcesso;
            UsuarioId = usuarioId;
            TipoAcesso = tipoAcesso;
        }
    }
}
