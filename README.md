# Teste Técnico DataOn

Este repositório contém duas aplicações .NET 10.0 independentes em uma mesma solução:

- `Exe_01_CRUD_de_Tarefas` - API para gerenciamento de tarefas (CRUD)
- `Exe_02_Consulta_de_CEP` - API para consulta de endereço por CEP usando a API ViaCep

## Requisitos

- .NET SDK 10.0 instalado
- Acesso à internet para o projeto de CEP (via API ViaCep)

## Como executar

### 1. Abrir o terminal

Abra um terminal no diretório raiz do projeto:

```powershell
cd c:\Users\lucas.DESKTOP-ZILL\Teste_Tecnico_DataOn
```

### 2. Restaurar dependências e compilar

```powershell
dotnet restore
dotnet build Teste_Tecnico_DataOn.sln
```

## Como rodar cada projeto

### Exe_01_CRUD_de_Tarefas

```powershell
cd Exe_01_CRUD_de_Tarefas
dotnet run
```

A API ficará disponível em:

- `http://127.0.0.1:5001`
- Swagger UI: `http://127.0.0.1:5001/swagger`

#### Endpoints disponíveis

- `GET /api/tarefas` - Lista todas as tarefas
- `GET /api/tarefas/{id}` - Retorna uma tarefa pelo ID
- `POST /api/tarefas` - Cria uma nova tarefa
- `PUT /api/tarefas/{id}` - Atualiza uma tarefa existente
- `DELETE /api/tarefas/{id}` - Remove uma tarefa

### Exe_02_Consulta_de_CEP

```powershell
cd Exe_02_Consulta_de_CEP
dotnet run
```

A API ficará disponível em:

- `http://127.0.0.1:5002`
- Swagger UI: `http://127.0.0.1:5002/swagger`

#### Endpoint disponível

- `GET /api/endereco/{cep}` - Consulta um endereço por CEP (8 dígitos)

Exemplo de uso:

```powershell
curl http://127.0.0.1:5002/api/endereco/01001000
```

## Testando os projetos

1. Abra o Swagger UI em cada porta indicada e teste os endpoints diretamente na interface.
2. Para o projeto de tarefas, faça requisições POST, GET, PUT e DELETE para verificar o comportamento CRUD.
3. Para o projeto de CEP, informe um CEP válido de 8 dígitos e confira se a API retorna o endereço esperado.

## Observações

- Os dados do projeto `Exe_01_CRUD_de_Tarefas` são mantidos em memória. Ao reiniciar a aplicação, os dados são reiniciados.
- O projeto `Exe_02_Consulta_de_CEP` depende de conexão com a API externa ViaCep.

## SQL

A pasta `sql/` contém scripts de banco de dados exemplos que não estão integrados diretamente com as APIs atuais.

- `01_create.sql` - Criação das tabelas do banco de dados.
- `02_insert.sql` - Inserção de dados de exemplo.
- `03_queries.sql` - Consultas solicitadas para o banco de dados.
- `04_update_delete.sql` - Scripts de atualização e exclusão.

Esses scripts são compatíveis com MySQL/InnoDB e servem como base para adicionar persistência ao projeto no futuro.
