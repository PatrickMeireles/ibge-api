# Ibge-Api

- Repositório baseado no desafio do Balta.io contém dados de cidade e estado de todo o Brasil.
    - [Repo](https://github.com/andrebaltieri/ibge)

## Funcionalidades 

- Autenticação e Autorização
    - Cadastro de E-mail e Senha
    - Login (Token, JWT)
- CRUD de Localidade
    - Código, Estado, Cidade (Id, City, State)
- Pesquisa por cidade
- Pesquisa por estado
- Pesquisa por código (IBGE)
- Boas práticas da API
    - Versionamento
    - Padronização
    - Documentação (Swagger)

### Modelagem

- Estado (Unidade Federativa)

|Campo | Tipo | Descrição
|-|-|-|
|Id | Guid | Chave Primária do Estado
|Code | int | Código do Estado
|Name | string | Nome do Estado
|Acronym | string| Sigla do Estado

- Cidade (Município)

|Campo | Tipo | Descrição
|-|-|-|
|Id | Guid | Chave Primária da Cidade
|Code | int | Código do Município
|Name | string | Nome do Município
|StateId| Guid| Chave Estrangeira do Estado

### Endpoints

- Usuários (Users)

|Endpoint | Descrição |
|-|-|
|[POST]/api/v1/users | Cadastrar um novo Usuário |
|[POST]/api/v1/users:auth | Autentica um novo Usuário |

- Estado (Unidade Federativa)

|Endpoint | Descrição |
|-|-|
|[POST]/api/v1/states | Cadastrar um novo Estado |
|[GET]/api/v1/states | Recupera uma lista de Estados |
|[GET]/api/v1/states/{id} | Recupera um Estado pelo Id |
|[PUT]/api/v1/states/{id} | Atualiza um Estado|
|[DELETE]/api/v1/states/{id} | Deleta um Estado|

- Cidade (Município)

|Endpoint | Descrição |
|-|-|
|[POST]/api/v1/cities | Cadastrar uma nova Cidade |
|[GET]/api/v1/cities | Recupera uma lista de Cidades |
|[GET]/api/v1/cities/{id} | Recupera uma Cidade pelo Id |
|[PUT]/api/v1/cities/{id} | Atualiza uma Cidade|
|[DELETE]/api/v1/cities/{id} | Deleta uma Cidade|

- Importação

|Endpoint | Descrição |
|-|-|
|[POST]/api/v1/import-xlsx | Importa um lote de Estado e Cidades baseado nesse modelo de [arquivo](https://github.com/andrebaltieri/ibge/blob/main/SQL%20INSERTS%20-%20API%20de%20localidades%20IBGE.xlsx) |

## Execução

### Docker
- Para executar o projeto utilizando docker, deverá executar os seguintes comandos:

    - Criar a imagem local, execute o comando `docker build -t patrick-amorim/ibge-api:latest -f Ibge.Api/Dockerfile .` na pasta `src`
    - Em seguida, executar o docker compose com o comando `docker compose up` na pasta `Ibge.api`
    - A aplicação irá executar na porta `8090`

### Linha de Comando
- Para executar o projeto utilizando linha de comando deverá executar os seguintes passos:

    - Na pasta `Ibge.Api` executar `dotnet build` para realizar um build da aplicação
    - Em seguida, executar `dotnet run`
    - A aplicação irá executar na porta `8090`


## Tecnologias
- .Net 6
- Entity Framework 6
- Memory Cache
- Channels
- Sqlite
- Carter
- Ardalis
- Fluent Validation
- MediatR
- BCrypt
- NPOI
- Scrutor
- Fixture
- Moq
- MSTest