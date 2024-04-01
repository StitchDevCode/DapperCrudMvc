# Aprendiendo Dapper

## Descripción del Proyecto
Este proyecto es un ejemplo simple de cómo utilizar Dapper en una aplicación ASP.NET Core MVC para interactuar con una base de datos SQL Server o PostgreSQL. Dapper es un micro-ORM (Object-Relational Mapper) diseñado para el acceso a datos en .NET. Proporciona un rendimiento excepcional y una sintaxis simple para realizar consultas y operaciones de base de datos.

## Tecnologías Utilizadas
- ASP.NET Core MVC
- Dapper
- SQL Server
- PostgreSQL
- HTML/CSS
- JavaScript

## Funcionalidades Principales
- CRUD (Crear, Leer, Actualizar, Eliminar) de registros de clientes.
- Interfaz de usuario simple y amigable.
- Uso de la librería SweetAlert2 para mensajes emergentes interactivos.

## Configuración de la Base de Datos
Antes de ejecutar la aplicación, asegúrate de crear la base de datos y la tabla necesaria:

```sql
-- SQL Server

CREATE DATABASE DapperDB;

USE DapperCrudMvc;

CREATE TABLE Cliente (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Edad Int NOT NULL
);
```

```sql
-- PostgreSQL

CREATE DATABASE "DapperDB"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Spanish_Nicaragua.1252'
    LC_CTYPE = 'Spanish_Nicaragua.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

CREATE SCHEMA IF NOT EXISTS dbo
    AUTHORIZATION postgres;

CREATE TABLE IF NOT EXISTS dbo."Cliente"
(
    "IdCliente" integer NOT NULL DEFAULT nextval('dbo."Cliente_IdCliente_seq"'::regclass),
    "PrimerNombre" character varying COLLATE pg_catalog."default",
    "PrimerApellido" character varying COLLATE pg_catalog."default",
    "Edad" numeric,
    CONSTRAINT "Cliente_pkey" PRIMARY KEY ("IdCliente")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS dbo."Cliente"
    OWNER to postgres;
```
