# Code Vault API

API para gerenciar códigos em um banco de dados SQL Server, utilizando ASP.NET, C# e Entity Framework.

## Autenticação

A autenticação é feita utilizando o IdentityCore com JWT Tokens.

## Endpoints

### Usuários

#### POST /account/register

Cria um novo usuário.

#### POST /account/login

Realiza o login de um usuário e retorna um token JWT.

### Snippets

#### GET /snippets

Retorna todos os snippets do banco de dados.

#### GET /snippets/{id}

Retorna um snippet específico pelo ID.

#### POST /snippets

Cria um novo snippet.

#### PUT /snippets/{id}

Atualiza um snippet específico pelo ID.

#### DELETE /snippets/{id}

Deleta um snippet específico pelo ID.

### Tags

#### GET /tags

Retorna todas as tags do banco de dados.

#### GET /tags/{id}

Retorna uma tag específica pelo ID.

#### POST /tags

Cria uma nova tag.

#### PUT /tags/{id}

Atualiza uma tag específica pelo ID.

#### DELETE /tags/{id}

Deleta uma tag específica pelo ID.

## Tecnologias utilizadas

- ASP.NET
- C#
- Entity Framework
- SQL Server
- IdentityCore
- JWT Tokens
