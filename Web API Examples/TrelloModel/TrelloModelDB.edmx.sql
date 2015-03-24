
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/22/2015 19:59:31
-- Generated from EDMX file: c:\users\joão\documents\visual studio 2013\Projects\Web API Examples\TrelloModel\TrelloModelDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TrelloDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BoardList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lists] DROP CONSTRAINT [FK_BoardList];
GO
IF OBJECT_ID(N'[dbo].[FK_BoardCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cards] DROP CONSTRAINT [FK_BoardCard];
GO
IF OBJECT_ID(N'[dbo].[FK_ListCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cards] DROP CONSTRAINT [FK_ListCard];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Boards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Boards];
GO
IF OBJECT_ID(N'[dbo].[Lists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lists];
GO
IF OBJECT_ID(N'[dbo].[Cards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cards];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Board'
CREATE TABLE [dbo].[Board] (
    [BoardId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Discription] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'List'
CREATE TABLE [dbo].[List] (
    [ListId] int IDENTITY(1,1) NOT NULL,
    [Lix] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [BoardId] int  NOT NULL
);
GO

-- Creating table 'Card'
CREATE TABLE [dbo].[Card] (
    [CardId] int IDENTITY(1,1) NOT NULL,
    [Cix] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Discription] nvarchar(max)  NOT NULL,
    [CreationDate] time  NOT NULL,
    [DueDate] time  NOT NULL,
    [BoardId] int  NOT NULL,
    [ListId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BoardId] in table 'Board'
ALTER TABLE [dbo].[Board]
ADD CONSTRAINT [PK_Board]
    PRIMARY KEY CLUSTERED ([BoardId] ASC);
GO

-- Creating primary key on [ListId] in table 'List'
ALTER TABLE [dbo].[List]
ADD CONSTRAINT [PK_List]
    PRIMARY KEY CLUSTERED ([ListId] ASC);
GO

-- Creating primary key on [CardId] in table 'Card'
ALTER TABLE [dbo].[Card]
ADD CONSTRAINT [PK_Card]
    PRIMARY KEY CLUSTERED ([CardId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BoardId] in table 'List'
ALTER TABLE [dbo].[List]
ADD CONSTRAINT [FK_BoardList]
    FOREIGN KEY ([BoardId])
    REFERENCES [dbo].[Board]
        ([BoardId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BoardList'
CREATE INDEX [IX_FK_BoardList]
ON [dbo].[List]
    ([BoardId]);
GO

-- Creating foreign key on [BoardId] in table 'Card'
ALTER TABLE [dbo].[Card]
ADD CONSTRAINT [FK_BoardCard]
    FOREIGN KEY ([BoardId])
    REFERENCES [dbo].[Board]
        ([BoardId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BoardCard'
CREATE INDEX [IX_FK_BoardCard]
ON [dbo].[Card]
    ([BoardId]);
GO

-- Creating foreign key on [ListId] in table 'Card'
ALTER TABLE [dbo].[Card]
ADD CONSTRAINT [FK_ListCard]
    FOREIGN KEY ([ListId])
    REFERENCES [dbo].[List]
        ([ListId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ListCard'
CREATE INDEX [IX_FK_ListCard]
ON [dbo].[Card]
    ([ListId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------