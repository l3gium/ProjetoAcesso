using MongoDB.Driver;
using ProjetoAcesso.Data;
using ProjetoAcesso.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAcesso.Services
{
    // Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
    public class UsuarioService
    {
        private readonly MongoDbContext _context;

        public UsuarioService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarUsuario(Usuario usuario)
        {
            await _context.Usuarios.InsertOneAsync(usuario);
        }

        public async Task<Usuario> PesquisarUsuario(string id)
        {
            return await _context.Usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoverUsuario(string id)
        {
            var result = await _context.Usuarios.DeleteOneAsync(a => a.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> ConcederPermissao(string usuarioId, string ambienteId)
        {
            var usuario = await PesquisarUsuario(usuarioId);

            if (usuario != null)
            {
                if (usuario.ConcederPermissao(ambienteId))
                {
                    var update = Builders<Usuario>.Update.Set(u => u.Ambientes, usuario.Ambientes);
                    var result = await _context.Usuarios.UpdateOneAsync(u => u.Id == usuarioId, update);
                    return result.ModifiedCount > 0;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> RevogarPermissao(string userId, string ambienteId)
        {
            var usuario = await PesquisarUsuario(userId);
            if (usuario == null) return false;

            if (usuario.Ambientes.Contains(ambienteId))
            {
                var ambientesLista = usuario.Ambientes.ToList();
                ambientesLista.Remove(ambienteId);
                usuario.Ambientes = ambientesLista;

                await _context.Usuarios.ReplaceOneAsync(u => u.Id == userId, usuario);
                return true;
            }

            return false;
        }
    }
}
