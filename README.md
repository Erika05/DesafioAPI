# Desafio API Rest

## APIs usados na construção do projeto:
   - **Mantis ->** https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP?version=latest
   - **Spotify->** https://developer.spotify.com/console/get-current-user-playlists/
   
## Geração token OAuth 2.0   
### Configurar Client ID e Client Secret do Spotify
   - Acessar o link abaixo:
        
       https://developer.spotify.com/dashboard/applications
       
   - Criar um aplicativo (Registrar):
   
![dasboard](https://user-images.githubusercontent.com/37153504/87255860-cfc83680-c464-11ea-8eda-9b695469ead6.png)

   - Adicionar nome e descrição.

![createanapp](https://user-images.githubusercontent.com/37153504/87255862-d191fa00-c464-11ea-8b87-807acc9899ea.png)

![tokenGerado](https://user-images.githubusercontent.com/37153504/87255939-609f1200-c465-11ea-8602-4d4c1399607f.png)

   - Editar configurações:
   
        Informe o campo "Redirect URIs" com o valor http://localhost:8888/callback/
          
![editSettings](https://user-images.githubusercontent.com/37153504/87257686-584dd380-c473-11ea-8726-6351ae5a632d.png)

***Documentação oficial: https://developer.spotify.com/documentation/general/guides/app-settings/***

### Gerar refresh token

Token Name: qualquer nome 

Auth URL: https://accounts.spotify.com/authorize

Access Token URL: https://accounts.spotify.com/api/token

Client ID:

Client Secret:

Scope: playlist-read-private playlist-modify-private

Grant Type: Authorization Code
   
## Relatório dos Testes

![RelatorioI](https://user-images.githubusercontent.com/37153504/86861290-21577680-c09d-11ea-9d3e-11af3330a22c.PNG)
![relatorioIII](https://user-images.githubusercontent.com/37153504/86861296-23213a00-c09d-11ea-80db-d9d9fd419302.PNG)
![relatorioII](https://user-images.githubusercontent.com/37153504/86861295-23213a00-c09d-11ea-92f4-a99dfaa6e498.PNG)

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
