
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/23/2019 08:00:00
-- Generated from EDMX file: C:\Users\Stefan Stojkovski\source\repos\TestApp\TestApp\ModelConnection.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_User];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tblUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUser];
GO
IF OBJECT_ID(N'[DB_LogInModelStoreContainer].[tblUploadLog]', 'U') IS NOT NULL
    DROP TABLE [DB_LogInModelStoreContainer].[tblUploadLog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tblUsers'
CREATE TABLE [dbo].[tblUsers] (
    [ClientID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Address1] nvarchar(50)  NOT NULL,
    [Address2] nvarchar(50)  NOT NULL,
    [DOB] datetime  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'tblUploadLogs'
CREATE TABLE [dbo].[tblUploadLogs] (
    [FileName] nvarchar(50)  NOT NULL,
    [DateAndTime] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClientID] in table 'tblUsers'
ALTER TABLE [dbo].[tblUsers]
ADD CONSTRAINT [PK_tblUsers]
    PRIMARY KEY CLUSTERED ([ClientID] ASC);
GO

-- Creating primary key on [FileName], [DateAndTime] in table 'tblUploadLogs'
ALTER TABLE [dbo].[tblUploadLogs]
ADD CONSTRAINT [PK_tblUploadLogs]
    PRIMARY KEY CLUSTERED ([FileName], [DateAndTime] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------