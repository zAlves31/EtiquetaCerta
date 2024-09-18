# API .NET

Este é um guia para rodar a API desenvolvida em .NET. Siga os passos abaixo para configurar o ambiente e iniciar a API.

## Vídeo do Projeto
[Video do Projeto](https://www.youtube.com/watch?v=8PhEE5nUtrY)

## Pré-requisitos

Antes de começar, você precisará ter instalado em sua máquina:

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 

## Clonando o repositório

Clone o repositório para sua máquina local:

```bash
git clone https://github.com/zAlves31/EtiquetaCerta.git


## Substituindo a String de Conexão com o Banco de Dados

Para que a API possa se conectar ao seu banco de dados, você precisará substituir a string de conexão padrão no arquivo de configuração. Siga os passos abaixo:

1. **Abra o arquivo `appsettings.json`**: No diretório do seu projeto, localize e abra o arquivo `appsettings.json`.

2. **Localize a seção de conexão**: Dentro do arquivo, você encontrará uma seção chamada `ConnectionStrings`. Ela deve se parecer com isso:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=NomeDoBanco;User Id=Usuario;Password=Senha;"
    }
    ```

3. **Substitua os valores**: Edite a string de conexão para corresponder às suas configurações de banco de dados. Aqui está um exemplo de como pode ficar:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=SEU_SERVIDOR;Database=SEU_BANCO;User Id=SEU_USUARIO;Password=SUASENHA;"
    }
    ```

4. **Salve as alterações**: Após fazer as alterações, salve o arquivo `appsettings.json`.

5. **Execute a API**: Agora você pode executar a API e ela deverá se conectar ao seu banco de dados usando a nova string de conexão.

6.**Substitua também a string de conexao na program.cs e contexts.

Se precisar de mais alguma coisa, é só avisar!

