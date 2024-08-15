-- Create Database productDb
CREATE DATABASE productDb

-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- productDb.dbo.Products definition

-- Drop table

-- DROP TABLE productDb.dbo.Products;

CREATE TABLE productDb.dbo.Products (
	OPC varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CLO varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ItemCode varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	RPL varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ModifyDateTime datetime DEFAULT getdate() NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	CONSTRAINT Products_PK PRIMARY KEY (ID,OPC)
);


-- productDb.dbo.Users definition

-- Drop table

-- DROP TABLE productDb.dbo.Users;

CREATE TABLE productDb.dbo.Users (
	Id int IDENTITY(0,1) NOT NULL,
	Username varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Password varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT Users_PK PRIMARY KEY (Id)
);


-- productDb.dbo.[__EFMigrationsHistory] definition

-- Drop table

-- DROP TABLE productDb.dbo.[__EFMigrationsHistory];

CREATE TABLE productDb.dbo.[__EFMigrationsHistory] (
	MigrationId nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProductVersion nvarchar(32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
);