# Golden Raspberry Awards - Backend

## Descrição

Este projeto é uma API desenvolvida em .NET para gerenciar e exibir informações relacionadas aos prêmios Golden Raspberry Awards. A API fornece dados como anos com múltiplos vencedores, intervalos de produtores premiados e estúdios mais premiados.

---

## Funcionalidades

- **Endpoints**:
  - Anos com múltiplos vencedores.
  - Intervalos de produtores premiados.
  - Estúdios mais premiados.
  - Listagem de filmes com paginação.
  - Anos disponíveis.

- **Swagger**:
  - Swagger para testes dos endpoints.

- **Tests**:
  - Possui projeto de teste de integração.

- **Banco de Dados**:
  - Configurado para utilizar um banco de dados em memória (InMemory) para simplificar testes e execução local.

- **Massa de Dados**:
  - Proveniente do arquivo csv em wwwroot/data/movie.csv

---

## Requisitos

- **.NET 7** ou superior.
- **IDE:** Visual Studio 2022.

---

## Configuração

1. Clone o repositório:

   ```bash
   git clone https://github.com/wandersmartins/golden-raspberry-backend.git

2. Abra o preojeto pelo Visual Studio ou Clicando 2 vezes sobre a Solutiion em: GoldenRaspberry.Api/GoldenRaspberry.Api.sln

3. Se necessário defina o projeto GoldenRaspberry.Api como "startup project", clicando sobre ele com o botão direito.

4. Clique em "play" para rodar a API e visualizar a página do Swagger.

5. Rodar o projeto de Frontend Angular.

## Teste de Integração

1.Para rodar o teste de integração vá no **menu > Test > Test Manager ** ao apresentar a janela clique no botão "play"