# 🛒 UXComex - Order Management

[![.NET](https://img.shields.io/badge/.NET-6.0-blueviolet?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-10.0-239120?logo=c-sharp)](https://docs.microsoft.com/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-red?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-blue?logo=bootstrap)](https://getbootstrap.com/)
[![Dapper](https://img.shields.io/badge/Dapper-ORM-orange)](https://github.com/DapperLib/Dapper)


Aplicação desenvolvida em **ASP.NET Core MVC (.NET 6)** com **Dapper** e **SQL Server**, simulando um sistema de **gestão de pedidos**.  
Projeto entregue como desafio técnico, mas estruturado de forma limpa para servir como exemplo de portfólio no GitHub.  

---

## ✨ Funcionalidades

- **Clientes (Customers)**
  - CRUD completo (cadastrar, editar, excluir, pesquisar por nome/email)
- **Produtos (Products)**
  - CRUD completo (cadastrar, editar, excluir, pesquisar por nome)
  - Controle de estoque
- **Pedidos (Orders)**
  - Criar pedido com múltiplos itens  
  - Validação de estoque no servidor (não deixa criar sem saldo)  
  - Abatimento automático de estoque ao confirmar  
  - Listagem com filtros (por cliente e status)  
  - Detalhes do pedido com itens, subtotal e total calculados  
  - Atualização de status (`New → Processing → Finished`)
- **Notificações**
  - Registro de cada alteração de status na tabela `Notifications`  

---

## 🛠️ Tecnologias utilizadas

- **Back-end:** ASP.NET Core MVC (C#) + .NET 6  
- **Banco de dados:** SQL Server  
- **ORM leve:** [Dapper](https://github.com/DapperLib/Dapper)  
- **UI:** Bootstrap 5 + jQuery  
- **Padrões:** Repositories, Dependency Injection, ViewModels  

---

📌 Observações

Projeto feito sem scaffolding, apenas código manual.

Estrutura clara: MVC + Repository Pattern + Dapper.

Fácil expansão para login/autenticação ou API REST.

## 🚀 Como rodar localmente

### 1) Pré-requisitos
- .NET SDK 6.0  
- SQL Server (instância local ou remota)  
- SQL Server Management Studio (SSMS) ou Azure Data Studio  

### 2) Criar o banco de dados
No SSMS, rode o script:

Esse script cria:

Banco UxComexOrdersDb

Tabelas (Customers, Products, Orders, OrderItems, Notifications)

Dados iniciais (3 clientes e 4 produtos)

Ajustar conexão

No arquivo WebApp/appsettings.json configure a connection string.

## 🧪 Como testar

Customers

Criar, editar, excluir e pesquisar clientes

Products

Criar, editar, excluir e pesquisar produtos

Orders

Criar pedido com vários itens

Validar erro ao tentar criar sem estoque

Conferir abatimento automático de estoque

Listar pedidos e aplicar filtros

Alterar status e verificar histórico em Notifications

## 📸 Telas principais :

 Dashboard : 

<img width="1366" height="653" alt="Screenshot 2025-08-18 at 14-52-37 UXComex - Order Manager" src="https://github.com/user-attachments/assets/30428bba-36af-4f3d-b800-7c6202f60332" />

Listagem de clientes : 

<img width="1366" height="653" alt="Screenshot 2025-08-18 at 14-54-32 UXComex - Order Manager" src="https://github.com/user-attachments/assets/396f9245-7a53-41ed-ad0c-b3dea6543a57" />

Criação de pedido :

<img width="1366" height="653" alt="Screenshot 2025-08-18 at 14-56-39 UXComex - Order Manager" src="https://github.com/user-attachments/assets/3381576a-1e89-40f7-8d6f-69df7b47b302" />

 Detalhes do pedido : 

<img width="1366" height="653" alt="Screenshot 2025-08-18 at 14-56-39 UXComex - Order Manager" src="https://github.com/user-attachments/assets/00ffa9da-057f-40b5-9e14-f7830d262468" />



👨‍💻 Desenvolvido por Lucas Alexandre — desafio técnico UXComex.



