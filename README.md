# PROJETO ACESSO
Uma empresa possui um controle de acessos em seus diversos ambientes.

Cada ambiente possui um dispositivo que, verificando as permissões do usuário, decide se o mesmo pode ou não acessar a sala, registrando TODA AÇÃO (acessos e tentativas de acesso) em um log. Nesse log, devem ficar registradas, no máximo, 100 ocorrências por dispositivo, sendo descartadas as mais antigas.

Utilizar o seguinte modelo para o sistema:

|Usuario|
|-|
|- id: int|
|- nome: string|
|- ambientes: List<Ambiente>|
||
|+ concederPermissao(Ambiente ambiente): bool|
|+ revogarPermissao(Ambiente ambiente): bool|

**OBS:**
    a) Cada usuário só pode ter uma permissão para cada ambiente

|Ambiente|
|-|
|- id: int|
|- nome: string|
|- logs: Queue<Log>|
||
|+ registrarLog(Log log): void|

**OBS:**
    a) Cada ambiente registrará, no máximo, 100 logs

|Log|
|-|
|- dtAcesso: DateTime|
|- usuario: Usuario|
|- tipoAcesso: bool (true=Autorizado, false=Negado)|

|Cadastro|
|-|
|- usuarios: List<Usuario>|
|- ambientes: List<Ambiente>|
||
|+ adicionarUsuario(Usuario usuario): void|
|+ removerUsuario(Usuario usuario): bool|
|+ pesquisarUsuario(Usuario usuario): Usuario|
|+ adicionarAmbiente(Ambiente ambiente): void|
|+ removerAmbiente(Ambiente ambiente): bool|
|+ pesquisarAmbiente(Ambiente ambiente): Ambiente|
|+ upload(): void|
|+ download(): void|

**OBS:** 
    a) Métodos de remoção devem indicar o sucesso da operação  
    b) Usuários só poderão ser removidos se estiverem sem nenhum tipo de permissão de acesso

-----------------------------------------------------------------------------------------------

Opções para o seletor na interface:

 0. Sair
 1. Cadastrar ambiente
 2. Consultar ambiente
 3. Excluir ambiente
 4. Cadastrar usuario
 5. Consultar usuario
 6. Excluir usuario
 7. Conceder permissão de acesso ao usuario (informar ambiente e usuário - vincular ambiente ao usuário)
 8. Revogar permissão de acesso ao usuario (informar ambiente e usuário - desvincular ambiente do usuário)
 9. Registrar acesso (informar o ambiente e o usuário - registrar o log respectivo)
10. Consultar logs de acesso (informar o ambiente e listar os logs - filtrar por logs autorizados/negados/todos)
**OBS:**

  a) Realizar a persistência dos dados quando a aplicação for encerrada (upload)  
  b) Fazer a carga dos dados ao executar a aplicação (download)
  Sugestão: CRIAR UM MODELO RELACIONAL PARA IMPLEMENTAR A PERSISTÊNCIA DA APLICAÇÃO

-----------------------------------------------------------------------------------------------

### Desenvolvido por Beatriz Bastos Borges e Miguel Luizatto Alves
