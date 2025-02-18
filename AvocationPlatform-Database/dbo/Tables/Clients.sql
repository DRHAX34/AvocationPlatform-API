﻿CREATE TABLE [dbo].[Clients] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [Name]    VARCHAR (MAX) NOT NULL,
    [VAT]     VARCHAR(MAX)  NOT NULL,
    [PictureUri] VARCHAR(MAX) NULL,
    [Address] VARCHAR (MAX) NULL,
    [ZipCode] VARCHAR (MAX) NULL,
    [City]    VARCHAR (MAX) NULL,
    [Phone]   VARCHAR (40) NULL,
    [Email]   VARCHAR (320) NULL,
    [SYS_STATUS] VARCHAR(5) NOT NULL,
    [SYS_CREATE_DATE] DATETIME  NOT NULL,
    [SYS_CREATE_USER_ID] VARCHAR(100) NOT NULL,
    [SYS_MODIFY_DATE] DATETIME  NOT NULL,
    [SYS_MODIFY_USER_ID] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

