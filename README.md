# Projeto de Cadastro e Gerenciamento de Pessoas e Endereços - ASP.NET MVC
Este projeto foi desenvolvido como parte de um teste técnico, implementando funcionalidades básicas de cadastro e gerenciamento de pessoas e endereços utilizando a arquitetura ASP.NET MVC. Ele foi criado para atender aos requisitos descritos, oferecendo uma interface amigável para CRUD de pessoas e endereços.

# Funcionalidades
Tela de Listagem de Pessoas
Listagem: Exibe todos os cadastros de pessoas com seus respectivos nomes.
Botão "+ Cadastro": Permite a navegação para a tela de criação de um novo cadastro.
Opções de Edição e Exclusão: Cada linha contém:
Um link no nome da pessoa para edição.
Um botão "X" para exclusão.
Tela de Cadastro de Pessoa
Campos de Entrada:
Nome
Telefone
CPF
Validação: Todos os campos são obrigatórios e possuem validações no front-end e no back-end.
Redirecionamento: Após salvar, o usuário é redirecionado para a tela de edição da pessoa.
Tela de Edição de Pessoa
Listagem de Endereços: Mostra todos os endereços cadastrados para a pessoa selecionada.
Botão "+ Endereço": Abre um modal para o cadastro de novos endereços.
Opções de Edição e Exclusão: Cada linha de endereço permite:
Edição clicando no endereço listado.
Exclusão através de um botão "X".
Cadastro de Endereço (Modal)
Campos de Entrada:
Endereço
CEP
Cidade
Estado
Consulta via API: (Opcional) Implementada integração com a API ViaCEP para consulta de endereço através do CEP.
Validação: Todos os campos são validados antes do envio.
Tecnologias Utilizadas
ASP.NET MVC: Framework para criação da aplicação web.
C#: Linguagem utilizada no desenvolvimento do back-end.
SQL Server: Banco de dados utilizado para armazenamento das informações.
Dapper: Biblioteca de mapeamento objeto-relacional para acesso aos dados.
T-SQL: Linguagem para escrita das queries de banco de dados.
Estrutura do Projeto
Camada de Apresentação:
Implementada utilizando Razor Views.
Layout responsivo com uso de CSS básico.
Camada de Negócio:
Serviços responsáveis por encapsular a lógica de negócio.
Integração com a API ViaCEP.
Camada de Dados:
Queries escritas em T-SQL para operações no banco de dados.
Utilização do Dapper para mapeamento e execução das queries.
Banco de Dados:
Projeto de Database Project para definição da estrutura do banco de dados.
Tabelas para pessoas e endereços com relacionamentos.
