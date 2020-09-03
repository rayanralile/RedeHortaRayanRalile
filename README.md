# RedeHortaRayanRalile
https://rayan-ralile-hortabr.azurewebsites.net/

Rede Social para cultivadores e entusiastas de hortas indoor (casa e apartamento) - Projeto de Bloco de graduação em Análise e Desenvolvimento de Sistemas pelo Instituto INFNET.
## Instrução para o Build e o Deploy do projeto - Visual Studio 2019
### Caso queira rodar localmente (banco de dados, blob do Azure rodando localmente simulado e API localhost
1. Abra o arquivo GlobalConstants.cs em Infrastructure.Crosscutting.IoC
2. Coloque isOnline para false
3. coloque o connection string do storage local que você criou simulado no Visual Studio ou em outra ferramenta. Esta connection string entra na variável blobStorage
4. Selecione como Startup Project o Application.API e no PMC (Package Manager Console, do NuGet) coloque como Default Project o Infrastructure.Data
5. execute update-database // Você estará criando o Database local para a aplicação
6. Agora coloque como Startup Project o RedeHortaRayanRalile e como Default Project o RedeHortaRayanRalile
7. execute update-database // Você estará criando o Database de gestão de login (entity framework)

### Caso queira rodar na nuvem (Banco de Dados SQL Server e Storage na Azure e API e apresentação publicados online no próprio Azure)
1. Abra o arquivo GlobalConstants.cs em Infrastructure.Crosscutting.IoC
2. Coloque isOnline para true
3. Coloque o connection string do storage que você criou no Azure. A variável que recebe o valor é a blobStorage
4. Abra o appsettings.json do RedeHortaRayanRalile
5. Coloque como valor da string "AzureIdentityDB" a sua connection string da database do entity framework nuvem Azure
6. Abra o appsettings.json do Application.Api
7. Coloque como valor da string "AzureConnection" a sua connection string do database da aplicação na nuvem Azure
8. Selecione como Startup Project o Application.API e no PMC (Package Manager Console, do NuGet) coloque como Default Project o Infrastructure.Data
9. execute update-database // Você estará criando as tabelas para a aplicação na nuvem
10. Agora coloque como Startup Project o RedeHortaRayanRalile e como Default Project o RedeHortaRayanRalile
11. execute update-database // Você estará criando as tabelas de gestão de login (entity framework) na nuvem
