# GerenciadorTarefasAPI

## Descrição

Esta é uma API RESTful simples para gerenciar tarefas. Ela permite criar, listar, obter, atualizar e excluir tarefas.

## Tecnologias Utilizadas

* **.NET 6:** Plataforma de desenvolvimento robusta e moderna.
* **C#:** Linguagem de programação principal.
* **ASP.NET Core Web API:** Framework para construir APIs RESTful.
* **Entity Framework Core (InMemory):** ORM para interagir com a base de dados (configurado em memória para este teste).
* **xUnit:** Framework para criação de testes unitários.
* **Moq:** Biblioteca para criação de mocks (objetos de teste simulados) para isolar dependências durante os testes.
* **Swashbuckle:** Biblioteca para gerar documentação da API no formato Swagger.
* **Arquitetura:** A API utiliza uma **Arquitetura em Camadas** para separar as responsabilidades e facilitar a manutenção e o teste do código. As camadas principais são: **Camada de Apresentação (Controllers)**, **Camada de Serviço (Services)**, **Camada de Acesso a Dados (Data)**, **Camada de Domínio (Domain)**, **Camada de Transferência de Dados (DTOs)** e **Camada de Testes (Tests)**.

## Como Executar a API

Para executar a API localmente, siga os seguintes passos:

1.  Certifique-se de ter o .NET 6 SDK (ou superior) instalado em sua máquina. Você pode verificar a instalação executando o comando `dotnet --version` no terminal.
2.  Navegue até o diretório raiz do projeto da API (`GerenciadorTarefas.API`) no terminal.
3.  Execute o seguinte comando para construir e executar a API:

    ```bash
    dotnet run
    ```

    A API estará disponível em uma URL como `https://localhost:7082` (a porta será especificada na saída do comando).

## Como Acessar a Documentação do Swagger

A documentação da API está disponível através do Swagger UI. Para acessá-la:

1.  Certifique-se de que a API esteja em execução (veja a seção anterior).
2.  Abra seu navegador web e navegue para a URL do Swagger UI. Geralmente, ela está localizada em:

    ```
    https://localhost:7082/swagger/index.html
    ```

    Substitua `7082` pela porta em que sua API está rodando. Nesta página, você poderá visualizar todos os endpoints da API, seus parâmetros, exemplos de requisição e resposta, e interagir com a API diretamente.

## Como Executar os Testes Automatizados

Para executar os testes unitários da camada de serviço, siga os seguintes passos:

1.  Navegue até o diretório do projeto de testes (`GerenciadorTarefas.Tests`) no terminal.
2.  Execute o seguinte comando para executar os testes:

    ```bash
    dotnet test
    ```

    O terminal exibirá os resultados dos testes (quantos passaram, quantos falharam, etc.).

## Funcionalidades da API e Como Utilizá-las

A API de Gerenciamento de Tarefas oferece as seguintes funcionalidades:

* **Criar Tarefa (POST /api/Tarefas):**
    * Permite criar uma nova tarefa.
    * Requer um objeto JSON no corpo da requisição com os detalhes da tarefa (título - obrigatório, descrição - opcional, dataVencimento - opcional, status - opcional, padrão é "Pendente").
    * Exemplo de corpo da requisição:
        ```json
        {
          "titulo": "Nova Tarefa Importante",
          "descricao": "Detalhes da tarefa.",
          "dataVencimento": "2026-05-10T15:00:00",
          "status": 1
        }
        ```
    * status: 0 = 
    * Retorna um código de status `201 Created` com a tarefa criada no corpo da resposta e a URL para acessá-la no cabeçalho `Location`.

* **Listar Tarefas (GET /api/Tarefas):**
    * Permite listar todas as tarefas.
    * Suporta filtros opcionais via query string:
        * `status`: Filtra por status (Pendente, EmProgresso, Concluida). Exemplo: `/api/Tarefas?status=Pendente`.
        * `dataVencimento`: Filtra por data de vencimento (no formato YYYY-MM-DD). Exemplo: `/api/Tarefas?dataVencimento=2025-05-10`.
    * Retorna um código de status `200 OK` com uma lista de tarefas no corpo da resposta.

* **Obter Tarefa por ID (GET /api/Tarefas/{id}):**
    * Permite obter os detalhes de uma tarefa específica pelo seu ID.
    * `{id}` deve ser substituído pelo ID da tarefa desejada.
    * Retorna um código de status `200 OK` com a tarefa no corpo da resposta se encontrada, ou `404 Not Found` se não encontrada.

* **Atualizar Tarefa (PUT /api/Tarefas/{id}):**
    * Permite atualizar os detalhes de uma tarefa existente.
    * `{id}` deve ser substituído pelo ID da tarefa a ser atualizada.
    * Requer um objeto JSON no corpo da requisição com os dados a serem atualizados (o `id` no corpo deve corresponder ao `id` na rota). Os campos são opcionais, mas o `Id` é obrigatório no DTO de atualização.
    * Exemplo de corpo da requisição:
        ```json
        {
          "id": "...",
          "titulo": "Título Atualizado",
          "status": 2
        }
        ```
    * Retorna um código de status `200 OK` com a tarefa atualizada no corpo da resposta se bem-sucedida, `400 Bad Request` se o ID na rota não corresponder ao ID no corpo ou se os dados forem inválidos, ou `404 Not Found` se a tarefa não for encontrada.

* **Excluir Tarefa (DELETE /api/Tarefas/{id}):**
    * Permite excluir uma tarefa pelo seu ID.
    * `{id}` deve ser substituído pelo ID da tarefa a ser excluída.
    * Retorna um código de status `200 OK` com a tarefa excluída no corpo da resposta se bem-sucedida, ou `404 Not Found` se a tarefa não for encontrada.