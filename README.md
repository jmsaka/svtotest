# Teste Analista Programador

## Descrição

Os testes estão divididos de acordo com as questões:


## 1. O que você entende por arquitetura MVC?

A arquitetura MVC bastante conhecida no mercado, representa Model, View, Controller.
Em termos práticos, trata-se de uma padrão aplicado a 3 camadas: de visão, do modelo e de controle.
Esse tipo de arquitetura é muito comum para padronizar projetos web, permitindo a fácil segregação de coluções que envolvam CRUD, além do benefício de ser expansível, ou seja, não está limitado somente as 3 camadas, mas, elas podem ser o norte para o início de qualquer aplicação web (a princípio).

## 2. Escolha um padrão de projeto e escreva seu conceito, objetivo e um cenário de uso.

O padrão que mais tenho visto, utilizado e praticado é o CQRS, Command Query Responsibility, Segregation. Traduzindo, Separação de Responsibilidades de Comando e Consulta. Esse padrão de projeto, além da versatilidade, permite a construção de soluções escaláveis e de fácil manutenção. Além de possibilitar uma lógica de Domínio mais clara.
No passado, muito se falava do CQRS, voltado a operações de persistência de dados em bancos relacionais e consultas em bancos não relacionais. Mas, é possível ir muito além dentro dessa estrutura e além de poder ser aplicado em API's, também pode ser aplicado em CronJobs e Serviços de Mensageria.
Quando esse projeto é aplicado em soluções de API em C#, podemos deixar o padrão mais robusto ao aplicar:
Mediator, Fluent Validation, AutoMapper, Logger, IoC, além de uma gama de serviços que podems ser aplicados na camada de infraestrutura, por exemplo, padrão Repository, Service Client para chamada de API's e Web Services, Serviços de Mensageria e FTP.
Quando trazemos os testes unitários para serem vinculados a este padrão, podemos considerar o udo de Mocks e a possibilidade de simular a execução de command handler, dessa forma, podemos simular cenários de execução afim de prever possíveis falhas.

## 3. Crie um programa em C# (Console App) que leia uma lista de objetos Produto...

Todo o conteúdo referente a essa questão pode ser encontrado na pasta: https://github.com/jmsaka/svtotest/tree/main/Produto

Arquitetura:

- Console
- Application
- Ioc
- Fluent Validation
- Mediator
- Repository
- Unit Tests
- LINQ
- AutoMapper

## 4. Desenvolva uma API mínima usando ASP.NET Core que exponha um endpoint GET /sum que receba dois parâmetros...

Todo o conteúdo referente a essa questão pode ser encontrado na pasta: https://github.com/jmsaka/svtotest/tree/main/ApiParams

Arquitetura:

- Swagger
- Ioc
- Fluent Validation
- Mediator

## 5. Implemente um programa Console App que simule uma fila de tarefas a serem processadas de forma assíncron

Todo o conteúdo referente a essa questão pode ser encontrado na pasta: https://github.com/jmsaka/svtotest/tree/main/ConsoleAppFilaTarefas

A ideia desse projeto é simular a geração de ordens de execução verificando o tempo que foi utilizado para executar.
As ordens não são executadas por ordem de ID por ser assíncrono, porém todos são executadas em seus respectivos tempos.
