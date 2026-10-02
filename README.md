# Extensões do VSCode (Identifiers)
- dotnettools.csdevkit
- thekalinga.bootstrap4-vscode
- formulahendry.code-runner
- dbaeumer.vscode-eslint
- ecmel.vscode-html-css
- xabikos.javascriptsnippets
- yzhang.markdown-all-in-one
- esbenp.prettier-vscode
- qwtel.sqlite-viewer

# Comandos DOTNET
- Para criar o projeto da aplicação:
  - ```dotnet new list``` serve para retornar a lista de templates
  - ```dotnet new [template-name] [options]``` serve para gerar o projeto
  - Para gerar o projeto usei este comando acima: ```dotnet new console```
  
- Para correr o projeto:
  - ```dotnet run``` corre com o projeto e depois é só acessar ao link disponibilizado na console

# C#
Classes:
- Pedido
  - Propriedades:
    - Id
    - NIF
    - Idade
    - PrestacoesAtuais
    - RendimentoMensal
    - ValorPretendido
    - Prazo
    - IncidentesCredito
    - SituacaoProfissional
    - DecisaoFinal
    - Motivos
    - PrestacaoNova
    - TaxaEsforco
  - Metodos
    - CriarPedidoCredito()
    - ExtrairEstatisticasPedidos()

# SQLite
Primeiro tenho de instalar os packages Sqlite e Design:
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 10.0.*

dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.*
```

Depois tenho de criar o arquivo AppDbContext que fica na pasta Data, mapear as classes às tabelas:
```
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    public DbSet<Pedido> Pedidos { get; set; }
```

Avançado para a parte de gerar os scripts de migração, é necessário correr ```dotnet new tool-manifest``` para poder instalar o dotnet-ef localmente, apenas no projeto. Este comando gera o ficheiro dotnet-tools.json.
Com isto posso com a instalação do dotnet-ef com: ```dotnet tool install dotnet-ef```
Com isto já tenho o equivalente a um requirements.txt no python pip, é só preciso correr: ```dotnet tool restore``` para instalar o dotnet-ef ou quais outras tools que vir a instalar com o tempo.

Depois disto corremos:
- ```dotnet ef migrations add [script-name] --output-dir Data/Migrations``` para gerar os scripts de migração
- ```dotnet ef database update``` para gerar a Db

# Improvements
Ainda é preciso rever melhor o código e reorganiza-lo, investir mais algum tempo na estrutura da base de dados, remover alguns hardcodes
Será preciso integrar esta aplicação em blazor e fazer os testes