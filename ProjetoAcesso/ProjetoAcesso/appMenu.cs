using ProjetoAcesso.Data;
using ProjetoAcesso.Models;
using ProjetoAcesso.Services;
using System;
using System.Threading.Tasks;

namespace ProjetoAcesso
{
    // Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
    public class appMenu
    {
        public async Task Run()
        {
            var dbContext = new MongoDbContext();
            var usuarioService = new UsuarioService(dbContext);
            var ambienteService = new AmbienteService(dbContext);

            while (true)
            {
                Console.WriteLine("\nSelecione uma opção:");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar ambiente");
                Console.WriteLine("2. Consultar ambiente");
                Console.WriteLine("3. Excluir ambiente");
                Console.WriteLine("4. Cadastrar usuário");
                Console.WriteLine("5. Consultar usuário");
                Console.WriteLine("6. Excluir usuário");
                Console.WriteLine("7. Conceder permissão de acesso ao usuário");
                Console.WriteLine("8. Revogar permissão de acesso ao usuário");
                Console.WriteLine("9. Registrar acesso");
                Console.WriteLine("10. Consultar logs de acesso");

                if (!int.TryParse(Console.ReadLine(), out int option) || option < 0 || option > 10)
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    continue;
                }

                if (option == 0) break;

                try
                {
                    switch (option)
                    {
                        case 1:
                            Console.Write("Digite o nome do ambiente: ");
                            string nomeAmbiente = Console.ReadLine();
                            var ambiente = new Ambiente(nomeAmbiente);
                            await ambienteService.AdicionarAmbiente(ambiente);
                            Console.WriteLine("Ambiente cadastrado com sucesso!");
                            break;

                        case 2:
                            Console.Write("Digite o ID do ambiente: ");
                            string idAmbiente = Console.ReadLine();
                            var ambienteEncontrado = await ambienteService.PesquisarAmbiente(idAmbiente);
                            Console.WriteLine(ambienteEncontrado != null
                                ? $"Ambiente encontrado: {ambienteEncontrado.Nome}"
                                : "Ambiente não encontrado.");
                            break;

                        case 3:
                            Console.Write("Digite o ID do ambiente: ");
                            string idAmbienteExcluir = Console.ReadLine();
                            bool ambienteExcluido = await ambienteService.RemoverAmbiente(idAmbienteExcluir);
                            Console.WriteLine(ambienteExcluido ? "Ambiente excluído com sucesso!" : "Erro ao excluir o ambiente.");
                            break;

                        case 4:
                            Console.Write("Digite o nome do usuário: ");
                            string nomeUsuario = Console.ReadLine();
                            var usuario = new Usuario(nomeUsuario);
                            await usuarioService.AdicionarUsuario(usuario);
                            Console.WriteLine("Usuário cadastrado com sucesso!");
                            break;

                        case 5:
                            Console.Write("Digite o ID do usuário: ");
                            string idUsuario = Console.ReadLine();
                            var usuarioEncontrado = await usuarioService.PesquisarUsuario(idUsuario);
                            Console.WriteLine(usuarioEncontrado != null
                                ? $"Usuário encontrado: {usuarioEncontrado.Nome}"
                                : "Usuário não encontrado.");
                            break;

                        case 6:
                            Console.Write("Digite o ID do usuário: ");
                            string idUsuarioExcluir = Console.ReadLine();
                            bool usuarioExcluido = await usuarioService.RemoverUsuario(idUsuarioExcluir);
                            Console.WriteLine(usuarioExcluido ? "Usuário excluído com sucesso!" : "Erro ao excluir o usuário.");
                            break;

                        case 7:
                            Console.Write("Digite o ID do usuário: ");
                            string idUsuarioPermissao = Console.ReadLine();
                            Console.Write("Digite o ID do ambiente: ");
                            string idAmbientePermissao = Console.ReadLine();
                            bool permissaoConcedida = await usuarioService.ConcederPermissao(idUsuarioPermissao, idAmbientePermissao);
                            Console.WriteLine(permissaoConcedida
                                ? "Permissão concedida com sucesso!"
                                : "Erro ao conceder permissão.");
                            break;

                        case 8:
                            Console.Write("Digite o ID do usuário: ");
                            string idUsuarioRevogar = Console.ReadLine();
                            Console.Write("Digite o ID do ambiente: ");
                            string idAmbienteRevogar = Console.ReadLine();
                            bool permissaoRevogada = await usuarioService.RevogarPermissao(idUsuarioRevogar, idAmbienteRevogar);
                            Console.WriteLine(permissaoRevogada
                                ? "Permissão revogada com sucesso!"
                                : "Erro ao revogar permissão.");
                            break;

                        case 9:
                            Console.Write("Digite o ID do usuário: ");
                            string idUsuarioAcesso = Console.ReadLine();
                            Console.Write("Digite o ID do ambiente: ");
                            string idAmbienteAcesso = Console.ReadLine();
                            bool acessoRegistrado = await ambienteService.RegistrarAcesso(idUsuarioAcesso, idAmbienteAcesso);
                            Console.WriteLine(acessoRegistrado
                                ? "Acesso registrado com sucesso!"
                                : "Erro ao registrar acesso.");
                            break;

                        case 10:
                            Console.Write("Digite o ID do ambiente: ");
                            string idAmbienteLog = Console.ReadLine();
                            Console.Write("Filtrar por (1) Autorizados (2) Negados (3) Todos: ");
                            int filtro = int.Parse(Console.ReadLine());
                            var logs = await ambienteService.ConsultarLogs(idAmbienteLog, filtro);

                            foreach (var log in logs)
                            {
                                Usuario user = await usuarioService.PesquisarUsuario(log.UsuarioId);
                                string nomeUser = user != null ? user.Nome : "Usuário não encontrado";
                                Console.WriteLine($"Data: {log.DtAcesso}, Usuário: {nomeUser}, Tipo: {(log.TipoAcesso ? "Autorizado" : "Negado")}");
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }
    }

}
