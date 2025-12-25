🏠 Controle de Gasto Residencial

Sistema para gestão financeira doméstica, focado na visualização de receitas e despesas. O projeto utiliza uma estrutura modular no back-end e uma interface baseada em componentes no front-end.

🚀 Tecnologias e Versões

Back-end
Linguagem: C#

Framework: .NET 9.0 (ASP.NET Core Web API)

Banco de Dados: PostgreSQL 16

Bibliotecas Principais:

MediatR: Para gerenciamento de comandos e consultas (CQRS).

FluentValidation: Para validação das regras de entrada de dados.

Entity Framework Core: Como ORM para persistência de dados.

Front-end
Linguagem: TypeScript

Framework: React

Estilização: Tailwind CSS

Ambiente de Build: Node.js 20-alpine

🏛️ Arquitetura e Estrutura

O sistema foi desenvolvido utilizando os princípios da Clean Architecture, separando a lógica de negócio das implementações externas:

Api.API: Camada de interface Web (Controllers e Configurações).

Api.Application: Camada de lógica de aplicação, onde residem os Handlers e Validators.

Api.Domain: Contém as entidades principais e regras de negócio essenciais.

Api.Infrastructure: Implementação técnica de repositórios e acesso a dados (PostgreSQL).

Api.Authentication: Camada estrutural para futura implementação de segurança.

gastoresidencial-frontend: Interface do usuário desenvolvida em React.

🛠️ Como rodar o projeto

Como o projeto utiliza Docker, todo o ambiente (API, Banco de Dados e Frontend) é configurado automaticamente.

1. Clonar o Repositório
Bash

git clone https://github.com/josefelipesouza/ControleGastoResidencial.git
cd ControleGastoResidencial
2. Iniciar os Serviços
No terminal da pasta raiz, execute o comando para buildar e subir os containers:

Bash

docker-compose up --build
[!NOTE] Este comando irá baixar as imagens necessárias, compilar o código e preparar o ambiente completo.

3. Criação do Banco e Tabelas
O Docker subirá o container do PostgreSQL (gasto-residencial-db).

A API aguardará o banco estar pronto (healthcheck) para iniciar automaticamente.

4. Acessar o Sistema
Interface (Frontend): http://localhost:3000

Documentação API (Swagger): http://localhost:5000/swagger/index.html

🛑 Encerrar o Projeto

Para parar a execução e remover todos os containers e redes criadas pelo Docker, utilize:

Bash

docker-compose down
