PROCEDIMENTOS PARA EXECUTAR O PROJETO

1. PRE REQUISITOS

Sistema Operacional

O sistame operacional instalado para executar o projeto é:

- Windows 7 (requer PowerShell 3.0 ou superior instalado)

- Windows 8/Windows 8.1

- Windows Server 2012 R2

- Windows 10

Service Fabric runtime, SDK and tooling

Instalar o Service Fabric runtime, SDK and tools for Visual Studio 2015 Update 2 ou superior.
Habilitar a execução de scripts do PowerShell

Abrir o PowerShell como administrador e executar o seguinte comando:

Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Force -Scope CurrentUser

2. Abrir e executar o Projeto

Clonar ou fazer o download do projeto de https://github.com/diogo-conigiero/DesafioSciensa

Abrir o Visual Studio 2017 como administrador 

Abrir a solution DesafioSciensa.sln do repositório que foi clonado

Pressionar F5 para iniciar a aplicação

Obs.: A primeira vez que a aplicação executar, o Visual Studio irá criar um cluster local. Esta operação pode levar um tempo para ser executada. 
O status da criação do cluster pode ser visto na janela output.

Caso uma janela do Browser não seja aberta automaticamente com a aplicação, iniciar uma instância de um Browser e entrar o endereço http://localhost:8657/
