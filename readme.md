# Golden Raspberry Awards Backend

## Descrição

Este projeto é um sistema completo para exibir informações relacionadas aos prêmios Golden Raspberry Awards. O frontend é construído em Angular, enquanto a API é desenvolvida com .NET. O sistema inclui um dashboard interativo e uma listagem paginada de filmes.

---

## Funcionalidades

### Frontend (Angular):

- **Dashboard**: Exibe informações sobre intervalos de produtores, top estúdios e anos com múltiplos vencedores.
- **Listagem de Filmes**: Permite filtrar e paginar os filmes disponíveis.
- **Carregamento com Spinner**: Indicador visual enquanto as chamadas à API estão sendo realizadas.

### Backend (.NET):

- **Endpoints**:
  - Anos com múltiplos vencedores.
  - Intervalos de produtores premiados.
  - Estúdios mais premiados.
  - Listagem de filmes com paginação.
  - Anos disponíveis.

---

## Requisitos

### Backend:

- **.NET 7** ou superior.
- Banco de dados configurado para armazenamento em memória (InMemory).

### Frontend:

- **Node.js** (versão 16 ou superior).
- **Angular CLI** (versão 13).

---

## Configuração

### Backend:

1. Clone o repositório:

   ```bash
   git clone <url_do_repositorio>
   ```

2. Navegue até a pasta do backend:

   ```bash
   cd backend
   ```

3. Restaure as dependências:

   ```bash
   dotnet restore
   ```

4. Execute a aplicação:

   ```bash
   dotnet run
   ```

A API estará disponível em `http://localhost:5270`.

### Frontend:

1. Navegue até a pasta do frontend:

   ```bash
   cd frontend
   ```

2. Instale as dependências:

   ```bash
   npm install
   ```

3. Execute a aplicação:

   ```bash
   ng serve
   ```

O frontend estará disponível em `http://localhost:4200`.

---

## Estrutura do Projeto

### Backend:

- **`Controllers`**: Contêm os endpoints da API.
- **`Services`**: Camada de regras de negócio.
- **`Repositories`**: Interação com o banco de dados.
- **`Models`**: Modelos e DTOs do sistema.
- **`Tests`**: Testes unitários para as controllers.

### Frontend:

- **`src/app`**:
  - **`features`**: Contém os componentes do dashboard e listagem de filmes.
  - **`core`**: Inclui os services e configurações gerais.
  - **`shared`**: Componentes compartilhados.

---

## Testes

### Backend:

1. Navegue até a pasta do backend:

   ```bash
   cd backend
   ```

2. Execute os testes:

   ```bash
   dotnet test
   ```

### Frontend:

1. Execute os testes:
   ```bash
   ng test
   ```

---

## Deploy

### Backend:

- Configure o banco de dados em produção.
- Publique a aplicação usando o comando:
  ```bash
  dotnet publish -c Release
  ```

### Frontend:

- Gere os arquivos estáticos:
  ```bash
  ng build --prod
  ```

---

## Contribuição

- Fork o repositório.
- Crie um branch para sua feature:
  ```bash
  git checkout -b minha-feature
  ```
- Envie suas mudanças:
  ```bash
  git push origin minha-feature
  ```
-
