CREATE PROCEDURE [dbo].[spGetClients]
	@ClientId UNIQUEIDENTIFIER = NULL,
	@OpeningId UNIQUEIDENTIFIER = NULL,
	@RecruiterId UNIQUEIDENTIFIER = NULL,
	@CandidateId UNIQUEIDENTIFIER = NULL,
	@RoomId UNIQUEIDENTIFIER = NULL,
	@WithDeleted BIT = 0
AS
BEGIN
	SELECT [Id]
		,[Name]
		,[VAT]
		,[Address]
		,[ZipCode]
		,[City]
		,[Phone]
		,[Email]
		,[SYS_STATUS] as 'SysStatus'
		,[SYS_CREATE_DATE] as 'SysCreateDate'
		,[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,[SYS_MODIFY_DATE] as 'SysModifyDate'
		,[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[Clients] WHERE
		(@ClientId IS NULL OR [Id] = @ClientId)
		AND
		(@OpeningId IS NULL OR [Id] IN (SELECT [ClientId] FROM [dbo].[Openings] WHERE [Id] = @OpeningId))
		AND
		(@RecruiterId IS NULL OR [Id] IN (SELECT [ClientId] FROM [dbo].[Openings] WHERE [Id] IN (SELECT [OpeningId] FROM [dbo].[Appointments] WHERE [RecruiterId] = @RecruiterId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))) AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@CandidateId IS NULL OR [Id] IN (SELECT [ClientId] FROM [dbo].[Openings] WHERE [Id] IN (SELECT [OpeningId] FROM [dbo].[Appointments] WHERE [CandidateId] = @CandidateId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))) AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@RoomId IS NULL OR [Id] IN (SELECT [ClientId] FROM [dbo].[Openings] WHERE [Id] IN (SELECT [OpeningId] FROM [dbo].[Appointments] WHERE [RoomId] = @RoomId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))) AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
END
