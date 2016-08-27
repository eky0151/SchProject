--IF OBJECT_ID('Bank', 'U') IS NOT NULL DROP TABLE Bank;
--IF OBJECT_ID('Worker', 'U') IS NOT NULL DROP TABLE Worker;
--IF OBJECT_ID('Logs', 'U') IS NOT NULL DROP TABLE Logs;
--IF OBJECT_ID('LoginData', 'U') IS NOT NULL DROP TABLE LoginData;
--IF OBJECT_ID('Technician', 'U') IS NOT NULL DROP TABLE Technician;
--IF OBJECT_ID('RegUser', 'U') IS NOT NULL DROP TABLE RegUser;
--IF OBJECT_ID('SolvedQuestion', 'U') IS NOT NULL DROP TABLE SolvedQuestion;
--GO


CREATE TABLE [dbo].[Bank]
(
    [ID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] VARCHAR(50) NOT NULL, 
    [Account] VARCHAR(25) NOT NULL
)

CREATE TABLE [dbo].[Worker]
(
    [ID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Fullname] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(20) NULL, 
    [BankID] INT NOT NULL, 
    [Address] VARCHAR(50) NOT NULL, 
    [Phone] VARCHAR(12) NULL, 
    [Picture] IMAGE NULL,
    CONSTRAINT [FK_Worker_Bank] FOREIGN KEY ([BankID]) REFERENCES [Bank]([ID])
)

CREATE TABLE [dbo].[LoginData]
(
    [ID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [WorkerID] INT NOT NULL, 
    [Username] VARCHAR(20) NOT NULL, 
    [Password] VARCHAR(20) NOT NULL, 
    [Urole] VARCHAR(10) NOT NULL,
    CONSTRAINT [FK_Login_Worker] FOREIGN KEY ([WorkerID]) REFERENCES [Worker]([ID]),
    CONSTRAINT [CK_Login_Urole] CHECK ([Urole] in ('Admin', 'Worker', 'Guest' ))
)

CREATE TABLE [dbo].[Logs]
(
    [ID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LoginID] INT NOT NULL, 
    [Date] DATETIME NOT NULL,
    CONSTRAINT [FK_LoginData_Login] FOREIGN KEY ([LoginID]) REFERENCES [LoginData]([ID])
)

CREATE TABLE [dbo].[Technician]
(
    [ID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [WorkerID] INT NOT NULL, 
    [Available] BIT NOT NULL,
    CONSTRAINT [FK_Technician_Worker] FOREIGN KEY ([WorkerID]) REFERENCES [Worker]([ID])
)

CREATE TABLE [dbo].[RegUser]
(
    [ID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Fullname] VARCHAR(50) NOT NULL, 
    [Regtime] DATETIME NOT NULL, 
    [Username] VARCHAR(20) NOT NULL, 
    [Email] VARCHAR(20) NOT NULL, 
    [Points] INT NOT NULL, 
    [Questions] INT NOT NULL, 
    [Picture] IMAGE NULL,
	[Password] VARCHAR(20) NOT NULL
)

CREATE TABLE [dbo].[SolvedQuestion]
(
    [ID] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserID] INT NOT NULL, 
    [WorkerID] INT NOT NULL, 
    [Question ] TEXT NOT NULL, 
    [Answer] TEXT NOT NULL, 
    [Category] VARCHAR(20) NOT NULL, 
    [Timeasked] DATETIME NOT NULL, 
    [Timeanswered] DATETIME NOT NULL,
	[KeyWords] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_SolvedQuestion_User] FOREIGN KEY ([UserID]) REFERENCES [RegUser]([ID]), 
    CONSTRAINT [FK_SolvedQuestion_Login] FOREIGN KEY ([WorkerID]) REFERENCES [LoginData]([ID]), 
    CONSTRAINT [CK_SolvedQuestion_Category] CHECK ( Category in ('Tech','Develop','Support'))
)

CREATE TABLE [dbo].[TechWorks] (
    [ID]              INT          IDENTITY (1, 1) NOT NULL,
    [TechID]          INT          NOT NULL,
    [Start]           DATETIME     NOT NULL,
    [Finish]          DATETIME     NOT NULL,
    [Payment]         INT          NOT NULL,
    [Customername]    VARCHAR (50) NOT NULL,
    [Customeraddress] VARCHAR (50) NOT NULL,
    CONSTRAINT [FK_TechWorks_Technician] FOREIGN KEY ([TechID]) REFERENCES [dbo].[Technician] ([Id]),
	CONSTRAINT [PK_TechWorks] PRIMARY KEY ([ID])
);

GO
SET IDENTITY_INSERT [dbo].[Bank] ON
INSERT INTO [dbo].[Bank] ([Id], [Name], [Account]) VALUES (1, N'Magyar Nemzeti Bank', N'111213141516')
INSERT INTO [dbo].[Bank] ([Id], [Name], [Account]) VALUES (2, N'Erste Bank', N'112372283687')
INSERT INTO [dbo].[Bank] ([Id], [Name], [Account]) VALUES (3, N'Országos Takarék Pénztár', N'178643748244')
INSERT INTO [dbo].[Bank] ([Id], [Name], [Account]) VALUES (4, N'Magyar Nemzeti Bank', N'124242424141')
INSERT INTO [dbo].[Bank] ([Id], [Name], [Account]) VALUES (5, N'Magyar Nemzeti Bank', N'219869586969')
INSERT INTO [dbo].[Bank] ([Id], [Name], [Account]) VALUES (6, N'Magyar Nemzeti Bank', N'246546456566')
INSERT INTO [dbo].[Bank] ([Id], [Name], [Account]) VALUES (7, N'Országos Takarék Pénztár', N'872472839955')
SET IDENTITY_INSERT [dbo].[Bank] OFF

SET IDENTITY_INSERT [dbo].[Worker] ON
INSERT INTO [dbo].[Worker] ([ID], [Fullname], [Email], [BankID], [Address], [Phone], [Picture]) VALUES (1, N'Kiss András', NULL, 1, N'Halásztelek, Kossuth Lajos 13', NULL, NULL)
INSERT INTO [dbo].[Worker] ([ID], [Fullname], [Email], [BankID], [Address], [Phone], [Picture]) VALUES (2, N'Nagy Imre', N'imike@gmail.com', 2, N'XII Kocsis utca 4b', N'06302324444',NULL)
INSERT INTO [dbo].[Worker] ([ID], [Fullname], [Email], [BankID], [Address], [Phone], [Picture]) VALUES (3, N'Közepes Péter', N'peti@p.hu', 4, N'Lakihegy, Duna u. 80', N'06702134063', NULL)
INSERT INTO [dbo].[Worker] ([ID], [Fullname], [Email], [BankID], [Address], [Phone], [Picture]) VALUES (4, N'Szabó Barnabás', NULL, 4, N'1112, Szigethalom Alma u. 10', NULL, NULL)
INSERT INTO [dbo].[Worker] ([ID], [Fullname], [Email], [BankID], [Address], [Phone], [Picture]) VALUES (5, N'Petri Zoli', N'pz@gmail.com', 5, N'XIX Petőfi u. 16', N'06703125678', NULL)
SET IDENTITY_INSERT [dbo].[Worker] OFF

SET IDENTITY_INSERT [dbo].[LoginData] ON
INSERT INTO [dbo].[LoginData] ([ID], [WorkerID], [Username], [Password], [URole]) VALUES (1, 1, N'echo', N'echo01', N'Admin')
INSERT INTO [dbo].[LoginData] ([ID], [WorkerID], [Username], [Password], [URole]) VALUES (2, 2, N'fan', N'fan10', N'Worker')
INSERT INTO [dbo].[LoginData] ([ID], [WorkerID], [Username], [Password], [URole]) VALUES (3, 3, N'Peti', N'peti', N'Guest')
SET IDENTITY_INSERT [dbo].[LoginData] OFF

SET IDENTITY_INSERT [dbo].[Technician] ON
INSERT INTO [dbo].[Technician] ([ID], [WorkerID], [Available]) VALUES (1, 4, 0)
INSERT INTO [dbo].[Technician] ([ID], [WorkerID], [Available]) VALUES (2, 5, 1)
SET IDENTITY_INSERT [dbo].[Technician] OFF