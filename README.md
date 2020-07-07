## Desafio API Rest

- API usados na construção do projeto:
   - https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP?version=latest
   - https://developer.spotify.com/console/get-current-user-playlists/
- Teste com Data-Driven: Mantis ->Tarefa -> CadastraTarefasTests -> CadastrarTarefaMinimal
- Autenticação com OAuth2: Spotify
- Validação usando REGEX(Expressões Regulares): Mantis -> Usuario -> CadastraUsuariosTests -> CadastrarUsuario
- Script com código Node.js: Mantis ->Tarefa -> CadastraTarefasTests -> CadastrarTarefa

Configuração Jenkins
Instalação Jenkins: https://blog.tiagopariz.com/jenkins-instalar-no-windows/
 1.Nuget: Baixar pacote/dependências do projeto  
     - https://www.nuget.org/downloads
 2.MsBuild: Compilar o projeto de testes
      - https://www.microsoft.com/en-us/download/details.aspx?id=48159
 3.Nunit3Console: Executar os testes
    - https://nunit.org/download/
Criar build
Novo job -> Construir um projeto de software free-style
  1.Geral
  
    a.Selecionar GitHub Project
    
       i.GitHub Project
       
          1.Adicionar o valor -> https://github.com/Erika05/DesafioAPI.git/
          
  2.Gerenciamento de código fonte 
  
    a.Selecionar Git
    
       i.Repositories
       
          1.Adicionar repositor URL-> https://github.com/Erika05/DesafioAPI.git
          
       ii.Branches to build
          1.Adicionar branch-> */master          
          
  3.Trigger de builds
  
    a.Selecionar Consultar periodicamente o SCM.
    
4.Ambiente de build

    a.Selecionar Delete workspace before build starts

5.Build
    a.Adicionar passo na build para executar no comando Windows:
       i."C:\\Program Files (x86)\\NuGet\\nuget.exe" restore "C:\Users\erika\\Documents\GitHub\DesafioAPI\DesafioAPI.sln"
       
       ii."C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\MSBuild.exe" /t:Clean,Build /p:Configuration=Debug "C:\Users\erika\Documents\GitHub\DesafioAPI\DesafioAPI.sln"
       
      iii.cd C:\\Users\\erika\\Documents\\GitHub\\DesafioAPI\\RestSharpTemplate\\bin\\Debug
"C:\Program Files (x86)\NUnit.Console-3.11.1\bin\net35\nunit3-console.exe" "DesafioAPI.dll" --inprocess --labels=On
