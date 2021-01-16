# Desenvolvedor .Net Core - Global Tecnologia

### Requisitos

- [x] .Net Core 3.1
- [ ] Banco de dados em memória, como o InMemory.
- [x] Persistência com Entity e consultas com Dapper.
- [x] Entregar um repositório público (github ou bitbucket) com o código fonte.
- [x] Organizar o projeto em camadas

### Requisitos desejáveis

- [x] JWT como token
- [x] Testes unitários
- [ ] Usar o ASP .NET Identity
- [x] Criptogafia não reversível (hash) na senha e no token
- [x] Criar um Dockerfile para executarmos a aplicação via Docker

### Cadastro de Usuários

- [x] Crie uma aplicação que exponha uma API RESTful de criação de usuários e login.
- [x] Todos os endpoints devem aceitar e responder somente JSON, inclusive ao responder mensagens de erro.
- [x] Todas as mensagens de erro devem ter o formato:
```
   {"mensagem": "mensagem de erro"}
```

### Cadastro

- [x] Esse endpoint deverá receber um usuário com os campos "nome", "email", "senha", mais uma lista de objetos "telefone", seguindo o formato abaixo:
```
{
  "name": "João da Silva",
  "email": "joao@silva.org",
  "password": "hunter2",
  "phones": [
    {
      "number": "987654321",
      "ddd": "21"
    } 
  ]
}
```

- [x] Responder o código de status HTTP apropriado
- [x] Em caso de sucesso, retorne o usuário, mais os campos:
  - [x] id: id do usuário (pode ser o próprio gerado pelo banco,porém seria interessante se fosse um **GUID** :heavy_check_mark: )
  - [x] created: data da criação do usuário
  - [x] modified: data da última atualização do usuário
  - [x] last_login: data do último login (no caso da criação,será a mesma que a criação)
  - [x] token: token de acesso da API (pode ser um GUID ou um **JWT** :white_check_mark: )
- [x] Caso o e-mail já exista, deverá retornar erro com a mensagem "E-mail já existente".
- [x] O token deverá ser persistido junto com o usuário

### Login

- [x] Este endpoint irá receber um objeto com e-mail e senha.
- [x] Caso o e-mail e a senha correspondam a um usuário existente, retornar igual ao endpoint de Criação.
- [x] Caso o e-mail não exista, retornar erro com status apropriado mais a mensagem "Usuário e/ou senha inválidos"
- [x] Caso o e-mail exista mas a senha não bata, retornar o status apropriado 401 mais a mensagem "Usuário e/ou senha inválidos"

### Perfil do Usuário
- [x] Caso o token não exista, retornar erro com status apropriado com a mensagem "Não autorizado".
- [x] Caso o token exista, buscar o usuário pelo id passado no path e comparar se o token no modelo é igual ao token passado no header.
- [x] Caso não seja o mesmo token, retornar erro com status apropriado e mensagem "Não autorizado"
- [x] Caso seja o mesmo token, verificar se o último login foi a MENOS que 30 minutos atrás. Caso não seja a MENOS que 30 minutos atrás, retornar erro com status apropriado com mensagem "Sessão inválida".
- [x] Caso tudo esteja ok, retornar o usuário no mesmo formato do retorno do Login.
