CREATE TABLE [dbo].[Candidates] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Name]          VARCHAR (MAX) NOT NULL,
    [Surname]       VARCHAR (MAX) NOT NULL,
    [PreferredName] VARCHAR (MAX) NOT NULL,
    [Email]         VARCHAR (320) NOT NULL,
    [Phone]         VARCHAR (40)  NULL,
    [ProfilePictureUri] VARCHAR(MAX) NULL,
    [VAT]           VARCHAR (MAX) NULL,
    [Company]       VARCHAR (MAX) NULL,
    [Address]       VARCHAR (MAX) NULL,
    [ZipCode]       VARCHAR (MAX) NULL,
    [City]          VARCHAR (MAX) NULL,
    [SYS_STATUS] VARCHAR(5) NOT NULL,
    [SYS_CREATE_DATE] DATETIME  NOT NULL,
    [SYS_CREATE_USER_ID] VARCHAR(100) NOT NULL,
    [SYS_MODIFY_DATE] DATETIME  NOT NULL,
    [SYS_MODIFY_USER_ID] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Candidates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

