# ASP.NET CORE MVC de aluguel de Carros 

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/ezequiel-lima/gestao-de-cursos/blob/master/LICENSE.txt)

O Projeto é uma aplicação web desenvolvida em .NET 8 com ASP.NET CORE MVC para gerenciar o aluguel de carros. 
O sistema permite realizar operações CRUD (Create, Read, Update, Delete) para carros e aluguéis, e mantém uma integração entre estas operações para garantir a consistência dos dados.

## Imagens do Projeto
![image](https://github.com/user-attachments/assets/6d88416b-3855-4608-8841-e2d24b2f27a5)
![image](https://github.com/user-attachments/assets/164df07e-9891-4918-b904-3e23b94c75c9)
![image](https://github.com/user-attachments/assets/73e01dae-80dd-4a13-9ad7-dacf9ba79314)

## Funcionalidades
### Gerenciamento de Carros

A aplicação permite o cadastro, edição, visualização e exclusão de carros. Cada carro possui os seguintes atributos:

- Modelo
- Marca
- Ano de Fabricação
- Placa
- Status (disponível ou alugado)

### Gerenciamento de Aluguéis

A aplicação também permite o gerenciamento de aluguéis, onde um aluguel é associado a um carro e a um usuário. Cada aluguel possui os seguintes atributos:

- Data de Devolução
- Carro
- Usuario (Cliente)

### Integração entre Carros e Aluguéis

Uma característica essencial do Projeto é a integração entre as operações de carros e aluguéis. 
Quando um aluguel é criado, o status do carro associado é automaticamente atualizado para "alugado". Da mesma forma, quando um aluguel é finalizado ou deletado,
o status do carro retorna para "disponível". Isso garante a consistência dos dados e facilita o controle da frota.
