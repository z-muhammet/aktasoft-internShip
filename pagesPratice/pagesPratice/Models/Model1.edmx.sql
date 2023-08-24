
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/21/2023 11:55:41
-- Generated from EDMX file: D:\aktasoft-internShip\pagesPratice\pagesPratice\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [page];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[books]', 'U') IS NOT NULL
    DROP TABLE [dbo].[books];
GO
IF OBJECT_ID(N'[dbo].[page_Table]', 'U') IS NOT NULL
    DROP TABLE [dbo].[page_Table];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'books'
CREATE TABLE [dbo].[books] (
    [id] int IDENTITY(1,1) NOT NULL,
    [book_name] varchar(255)  NULL,
    [book_author] varchar(255)  NULL,
    [book_summary] varchar(max)  NULL,
    [book_category] int  NULL,
    [book_page] int  NULL
);
GO

-- Creating table 'book_category'
CREATE TABLE [dbo].[book_category] (
    [id] int IDENTITY(1,1) NOT NULL,
    [category_name] nvarchar(255)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'books'
ALTER TABLE [dbo].[books]
ADD CONSTRAINT [PK_books]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'book_category'
ALTER TABLE [dbo].[book_category]
ADD CONSTRAINT [PK_book_category]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------