CREATE TABLE [dbo].[Appointments] (
    [Id]          UNIQUEIDENTIFIER  NOT NULL,
    [CandidateId] UNIQUEIDENTIFIER  NOT NULL,
    [RecruiterId] UNIQUEIDENTIFIER  NOT NULL,
    [RoomId]      UNIQUEIDENTIFIER  NULL,
    [StartTime]   DATETIME      NULL,
    [EndTime]     DATETIME      NULL,
    [OpeningId]   UNIQUEIDENTIFIER  NULL,
    [Stage]       INT           NOT NULL,
    [SYS_SINGLE_USE_TOKEN] VARCHAR (MAX) NULL,
    [SYS_STATUS] VARCHAR(5) NOT NULL,
    [SYS_CREATE_DATE] DATETIME  NOT NULL,
    [SYS_CREATE_USER_ID] VARCHAR(100) NOT NULL,
    [SYS_MODIFY_DATE] DATETIME  NOT NULL,
    [SYS_MODIFY_USER_ID] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Appointments_Candidates_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidates] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointments_Openings_OpeningId] FOREIGN KEY ([OpeningId]) REFERENCES [dbo].[Openings] ([Id]),
    CONSTRAINT [FK_Appointments_Recruiters_RecruiterId] FOREIGN KEY ([RecruiterId]) REFERENCES [dbo].[Recruiters] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointments_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_RoomId]
    ON [dbo].[Appointments]([RoomId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_RecruiterId]
    ON [dbo].[Appointments]([RecruiterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_OpeningId]
    ON [dbo].[Appointments]([OpeningId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Appointments_CandidateId]
    ON [dbo].[Appointments]([CandidateId] ASC);

