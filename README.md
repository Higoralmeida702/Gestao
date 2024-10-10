# Projeto de Gestão Acadêmica

🚀 **Descrição do Projeto:**
Este projeto é um sistema de gestão acadêmica desenvolvido com o objetivo de facilitar o gerenciamento de informações de professores e alunos. O sistema permite um controle eficiente sobre as disciplinas, notas e registros acadêmicos, promovendo uma experiência intuitiva para os usuários.

## Funcionalidades

- 👩‍🏫 **Cadastro de Professores:** Professores podem ser cadastrados com informações detalhadas, como nome, email, cargo e disciplinas que lecionam. Ao se cadastrar, eles podem selecionar entre 6 matérias predefinidas.

- 🔢 **Registro do Aluno (RA):** Cada aluno recebe um Registro Acadêmico único, facilitando sua identificação e o acompanhamento de suas informações.

- 📝 **Adição e Modificação de Notas:** Professores podem adicionar novas notas e modificar as notas existentes dos alunos, assegurando um acompanhamento contínuo do desempenho acadêmico.

- 📚 **Cadastro de Matérias:** As matérias são predefinidas no banco de dados, e cada professor pode selecionar as disciplinas que deseja lecionar.

- 🔒 **Autenticação e Autorização:** Implementação de um sistema de login seguro utilizando JWT Bearer, garantindo acesso às funcionalidades apropriadas para cada usuário.

- 🔗 **Relacionamento Muitos-para-Muitos:** Professores podem lecionar várias matérias, e os alunos podem estar vinculados a diversas disciplinas, permitindo um gerenciamento flexível e eficiente.

## Tecnologias Utilizadas

- **ASP.NET Core** para o desenvolvimento da API
- **Entity Framework Core** para interação com o banco de dados
- **SQL Server** como sistema de gerenciamento de banco de dados
- **Swagger** para documentação da API e fácil acesso aos endpoints

## Desenvolvimento e Aprendizado

Durante o desenvolvimento deste projeto, aprendi a importância de entender os relacionamentos na hora de programar, especialmente em um sistema complexo como este. A experiência foi extremamente valiosa, e estou animado com o resultado final!

## Como Executar o Projeto

## 1. Clone este repositório:
git clone https://github.com/Higoralmeida702/Gestao

## 2. Navegue até a pasta do projeto:
cd Gestao

## 3. Abra o projeto no Visual Studio ou na sua IDE de preferência.

## 4. Restaure as dependências:
dotnet restore

## 5. Execute a aplicação:
dotnet run

## 6. Acesse a documentação da API no Swagger

## 7. Autenticação no Swagger:
- **Crie uma conta de professor.
- **Faça login com as credenciais.
- **No Swagger, vá até o botão "Authorize" e insira:
- **Token que voce recebeu ao realizar o login, para poder acessar os endpoints disponíveis para o professor.
