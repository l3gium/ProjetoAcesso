using MongoDB.Driver;
using ProjetoAcesso.Data;
using ProjetoAcesso.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjetoAcesso.Services
{
    // Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
    public class AmbienteService
    {
        private readonly MongoDbContext _context;

        public AmbienteService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAmbiente(Ambiente ambiente)
        {
            await _context.Ambientes.InsertOneAsync(ambiente);
        }

        public async Task<Ambiente> PesquisarAmbiente(string id)
        {
            return await _context.Ambientes.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoverAmbiente(string id)
        {
            var result = await _context.Ambientes.DeleteOneAsync(a => a.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task RegistrarLog(string ambienteId, Log log)
        {
            var ambiente = await PesquisarAmbiente(ambienteId);
            if (ambiente != null)
            {
                ambiente.RegistrarLog(log);
                var update = Builders<Ambiente>.Update.Set(a => a.Logs, ambiente.Logs.ToList());
                await _context.Ambientes.UpdateOneAsync(a => a.Id == ambienteId, update);
            }
        }

        public async Task<bool> RegistrarAcesso(string userId, string ambienteId)
        {
            var ambiente = await PesquisarAmbiente(ambienteId);
            if (ambiente == null) return false;

            bool autorizado = new System.Random().Next(0, 2) == 1;

            Log log = new Log(DateTime.Now, userId, autorizado);

            ambiente.RegistrarLog(log);

            await _context.Ambientes.ReplaceOneAsync(a => a.Id == ambienteId, ambiente);
            return true;
        }

        public async Task<List<Log>> ConsultarLogs(string ambienteId, int filtro)
        {
            var ambiente = await PesquisarAmbiente(ambienteId);
            if (ambiente == null) return new List<Log>();

            return filtro switch
            {
                1 => ambiente.Logs.Where(l => l.TipoAcesso).ToList(),
                2 => ambiente.Logs.Where(l => !l.TipoAcesso).ToList(),
                _ => ambiente.Logs.ToList(),
            };
        }
    }
}
