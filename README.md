Neste desafio, será apresentado uma solução automatizada que capture e insira dados diretamente do site https://10fastfingers.com/typing-test/portuguese, utilizando a biblioteca Selenium. 
Além disso, será necessário garantir o armazenamento dos dados capturados em um banco de dados PostgreSQL, proporcionando uma abordagem completa e eficiente para a manipulação e análise posterior dos dados.  

Os critérios para o desenvolvimento desta automação são claros: você deve criar um script que leia informações de um campo específico no site indicado e as insira em um segundo campo designado. 
A precisão e eficácia da automação são cruciais, pois a performance da sua solução influenciará diretamente a pontuação obtida neste desafio. 

Instale o .NET Core SDK e o Chrome WebDriver
Bibliotecas:  
DotNetSeleniumExtras.WaitHelpers 
Selenium.WebDriver 
Selenium.Support 
Npgsql 


O projeto consiste em uma solução de automação desenvolvida em C# para realizar testes de digitação em um site específico (https://10fastfingers.com/typing-test/portuguese) e armazenar os resultados em um banco de dados PostgreSQL. 
A automação é dividida em três partes principais: a interface de linha de comando (CLI) representada pelo arquivo Program.cs, os testes manuais representados pela classe ManualTest.cs, e os testes automatizados representados pela classe RoboTeste.cs.

O arquivo Program.cs contém a interface de linha de comando que permite ao usuário escolher entre realizar um teste manual, um teste automatizado, consultar o banco de dados ou sair do programa. Através de um loop infinito, 
o programa aguarda a entrada do usuário e direciona para a função correspondente de acordo com a escolha feita. Após a conclusão de um teste, os resultados são salvos no banco de dados. 

A classe ManualTest é responsável por realizar os testes manuais de digitação. Utilizando a biblioteca Selenium WebDriver, o teste é executado no navegador Chrome, 
onde o usuário realiza o teste de digitação no site indicado. Os resultados são capturados e armazenados em um objeto TestResults, que é então enviado para o banco de dados. 

 
A classe RoboTeste implementa os testes automatizados de digitação. Novamente, utilizando a biblioteca Selenium WebDriver, o teste é realizado no navegador Chrome de forma automatizada, 
sem intervenção humana. Os resultados são capturados e armazenados em um objeto TestResults, que é então enviado para o banco de dados. 

A classe DatabaseManager é responsável por interagir com o banco de dados PostgreSQL. Ela possui métodos para salvar os resultados dos testes no banco de dados e para consultar os resultados salvos. 

A classe TestResults define o modelo de dados para os resultados dos testes. Ela armazena informações como o tipo de teste (manual ou automatizado), palavras por minuto, keystrokes, precisão da palavra, entre outros. 
Também inclui métodos para calcular a velocidade média de digitação e a precisão por palavra. 

Este projeto oferece uma abordagem abrangente para automação de testes de digitação, permitindo a realização de testes tanto manualmente quanto de forma automatizada, e armazenando os resultados de maneira organizada em um banco de dados para posterior análise e comparação. 
