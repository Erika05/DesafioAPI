# Desafio API Rest

## APIs usados na construção do projeto:
   - **Mantis ->** https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP?version=latest
   - **Spotify->** https://developer.spotify.com/console/get-current-user-playlists/
   
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
     
- General  
   - Selecionar GitHub Project           
      - GitHub Project
      - Adicionar o valor -> https://github.com/Erika05/DesafioAPI.git/

![general](https://user-images.githubusercontent.com/37153504/86859007-21a14300-c098-11ea-909c-1a2b940f08a4.PNG)
          
- Gerenciamento de código fonte   
   - Selecionar Git     
      - Repositories       
           - Adicionar repositor URL-> https://github.com/Erika05/DesafioAPI.git          
      - Branches to build
           - Adicionar branch-> */master          

![gerenciamentoCodigoFonte](https://user-images.githubusercontent.com/37153504/86859008-21a14300-c098-11ea-96d4-4c4f44c7693e.PNG)

 - Trigger de builds  
   - Selecionar Consultar periodicamente o SCM.

![triggerBuild](https://user-images.githubusercontent.com/37153504/86859012-22d27000-c098-11ea-8eb0-c0138ae373cd.PNG)

 - Ambiente de build
   - Selecionar Delete workspace before build starts

![ambienteBuild](https://user-images.githubusercontent.com/37153504/86858997-1e0dbc00-c098-11ea-94fc-989d03ad2198.PNG)

- Build
  - Adicionar passo na build para executar no comando Windows:
    
        - "C:\\Program Files (x86)\\NuGet\\nuget.exe" restore "C:\Users\erika\\Documents\GitHub\DesafioAPI\DesafioAPI.sln"
       
        - "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\MSBuild.exe" /t:Clean,Build /p:Configuration=Debug "C:\Users\erika\Documents\GitHub\DesafioAPI\DesafioAPI.sln"
       
        - cd C:\\Users\\erika\\Documents\\GitHub\\DesafioAPI\\RestSharpTemplate\\bin\\Debug
        "C:\Program Files (x86)\NUnit.Console-3.11.1\bin\net35\nunit3-console.exe" "DesafioAPI.dll" --inprocess --labels=On

![buildI](https://user-images.githubusercontent.com/37153504/86859003-20701600-c098-11ea-9f60-0d9d3994df4b.PNG)

![buildII](https://user-images.githubusercontent.com/37153504/86859005-20701600-c098-11ea-8603-43b9bb6dc15a.PNG)

## Observações Gerais
- **Teste com Data-Driven:** Mantis ->Tarefa -> CadastraTarefasTests -> CadastrarTarefaMinimal
- **Autenticação com OAuth2:** Spotify
- **Validação usando REGEX(Expressões Regulares):** Mantis -> Usuario -> CadastraUsuariosTests -> CadastrarUsuario
- **Script com código Node.js:** Mantis ->Tarefa -> CadastraTarefasTests -> CadastrarTarefa
