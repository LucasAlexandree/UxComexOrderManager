# 🛒 UXComex - Order Management

[![.NET](https://img.shields.io/badge/.NET-6.0-blueviolet?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-10.0-239120?logo=c-sharp)](https://docs.microsoft.com/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-red?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-blue?logo=bootstrap)](https://getbootstrap.com/)
[![Dapper](https://img.shields.io/badge/Dapper-ORM-orange)](https://github.com/DapperLib/Dapper)

<!-- Badges de GitHub Actions (CI/CD) -->
[![Build](https://github.com/SEU_USUARIO/SEU_REPOSITORIO/actions/workflows/dotnet.yml/badge.svg)](https://github.com/SEU_USUARIO/SEU_REPOSITORIO/actions/workflows/dotnet.yml)
[![Tests](https://github.com/SEU_USUARIO/SEU_REPOSITORIO/actions/workflows/tests.yml/badge.svg)](https://github.com/SEU_USUARIO/SEU_REPOSITORIO/actions/workflows/tests.yml)

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

## 🚀 Como rodar localmente

### 1) Pré-requisitos
- .NET SDK 6.0  
- SQL Server (instância local ou remota)  
- SQL Server Management Studio (SSMS) ou Azure Data Studio  

### 2) Criar o banco de dados
No SSMS, rode o script:

```sql


