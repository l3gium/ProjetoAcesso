using System.Threading.Tasks;

namespace ProjetoAcesso
{
    // Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
    class Program
    {
        static async Task Main(string[] args)
        {
            var appMenu = new appMenu();
            await appMenu.Run();
        }
    }
}
