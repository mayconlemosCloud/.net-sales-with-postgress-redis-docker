# Documentação do Projeto

## Como Rodar o Projeto

Este projeto foi projetado para rodar em um ambiente Dockerizado, facilitando a configuração e execução. Siga os passos abaixo para começar:

### Pré-requisitos
1. Certifique-se de ter o Docker e o Docker Compose instalados na sua máquina.
2. Clone este repositório para sua máquina local.

### Passos para Rodar
1. Abra um terminal e navegue até o diretório do projeto.
2. Execute o script `run-database-setup.bat` para configurar o banco de dados e inicializar as tabelas necessárias. Este script irá:
   - Iniciar os containers Docker usando o arquivo `docker-compose.override.yml`.
   - Inicializar o banco de dados com o script `init-database.sql`.
   - Exibir os dados das tabelas `Products` e `Customers` para verificação.

   Para executar o script, basta rodar:
   ```
   run-database-setup.bat
   ```

3. Após a conclusão do script, o banco de dados estará pronto e você poderá prosseguir para rodar a aplicação.

### Informações Adicionais
- O script `run-database-setup.bat` simplifica o processo de configuração automatizando as seguintes tarefas:
  - Inicialização dos containers Docker.
  - Execução do script de inicialização SQL.
  - Exibição do conteúdo das tabelas `Products` e `Customers` para verificação.

- Os IDs das tabelas `Products` e `Customers` são exibidos durante a execução do script para sua referência.

### Rodando a Aplicação
Após configurar o banco de dados, você pode rodar a aplicação executando os comandos apropriados ou utilizando sua IDE preferida para iniciar o projeto.

Para quaisquer problemas ou dúvidas, consulte o README do projeto ou entre em contato com a equipe de desenvolvimento.

## Fluxo de Funcionamento do Módulo de Sales

```mermaid
graph TD
    A[Criação de Vendas] -->|POST /api/sales| B[Validação dos Dados]
    B --> C[Persistência no Banco de Dados]
    C --> D[Retorno dos Detalhes da Venda]

    E[Consulta de Vendas] -->|GET /api/sales/saleId| F[Busca por ID no Banco de Dados]
    E -->|GET /api/sales| G[Busca de Todas as Vendas]
    F --> H[Retorno dos Detalhes da Venda]
    G --> H

    I[Atualização de Vendas] -->|PUT /api/sales/saleId| J[Validação dos Dados]
    J --> K[Atualização no Banco de Dados]
    K --> L[Retorno dos Detalhes Atualizados]

    M[Exclusão de Vendas] -->|DELETE /api/sales/saleId| N[Validação do ID]
    N --> O[Exclusão no Banco de Dados]
    O --> P[Confirmação da Exclusão]

    subgraph Regras_de_Negocio
        Q[Descontos por Quantidade de Itens]
        R[Publicação de Eventos]
    end
```

```mermaid
graph TD
    subgraph Regras_de_Negocio
        A[Compras acima de 4 itens idênticos têm 10% de desconto]
        B[Compras entre 10 e 20 itens idênticos têm 20% de desconto]
        C[Não é possível vender acima de 20 itens idênticos]
        D[Compras abaixo de 4 itens não têm desconto]
        A --> B --> C --> D
    end
```
