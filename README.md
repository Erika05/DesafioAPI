# Desafio API Rest

## API usados na construção do projeto:
   - https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP?version=latest
   - https://developer.spotify.com/console/get-current-user-playlists/
   
## Adicionar teste em uma rotina no Jenkins
### Configuração da máquina:
- Instalação Jenkins: 
   - https://blog.tiagopariz.com/jenkins-instalar-no-windows/
- Nuget: ***Baixar pacote/dependências do projeto***
   - https://www.nuget.org/downloads
- MsBuild: ***Compilar o projeto de testes***
   - https://www.microsoft.com/en-us/download/details.aspx?id=48159
- Nunit3Console: ***Executar os testes***
   - https://nunit.org/download/

### Criar rotina:   
- Criar build
   - Novo job -> Construir um projeto de software free-style
     - Geral  
       - Selecionar GitHub Project           
           - GitHub Project
              - Adicionar o valor -> https://github.com/Erika05/DesafioAPI.git/
          
          
  - Gerenciamento de código fonte   
     - Selecionar Git     
           - Repositories       
                  -> Adicionar repositor URL-> https://github.com/Erika05/DesafioAPI.git          
           - Branches to build
                  -> Adicionar branch-> */master          
          
  - Trigger de builds  
     - Selecionar Consultar periodicamente o SCM.
    
   - Ambiente de build
     - Selecionar Delete workspace before build starts
  
  - Build
     - Adicionar passo na build para executar no comando Windows:
    
         - "C:\\Program Files (x86)\\NuGet\\nuget.exe" restore "C:\Users\erika\\Documents\GitHub\DesafioAPI\DesafioAPI.sln"
       
         - "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\MSBuild.exe" /t:Clean,Build /p:Configuration=Debug "C:\Users\erika\Documents\GitHub\DesafioAPI\DesafioAPI.sln"
       
        - cd C:\\Users\\erika\\Documents\\GitHub\\DesafioAPI\\RestSharpTemplate\\bin\\Debug
       "C:\Program Files (x86)\NUnit.Console-3.11.1\bin\net35\nunit3-console.exe" "DesafioAPI.dll" --inprocess --labels=On

## Observações Gerais
- Teste com Data-Driven: Mantis ->Tarefa -> CadastraTarefasTests -> CadastrarTarefaMinimal
- Autenticação com OAuth2: Spotify
- Validação usando REGEX(Expressões Regulares): Mantis -> Usuario -> CadastraUsuariosTests -> CadastrarUsuario
- Script com código Node.js: Mantis ->Tarefa -> CadastraTarefasTests -> CadastrarTarefa
