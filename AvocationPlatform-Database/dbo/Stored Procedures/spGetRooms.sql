CREATE PROCEDURE [dbo].[spGetRooms]
	@RoomId UNIQUEIDENTIFIER = NULL,
	@RecruiterId UNIQUEIDENTIFIER = NULL,
	@CandidateId UNIQUEIDENTIFIER = NULL,
	@OpeningId UNIQUEIDENTIFIER = NULL,
	@ClientId UNIQUEIDENTIFIER = NULL,
	@WithDeleted BIT = 0
AS
BEGIN

	SELECT [Id]
		,[Name]
		,[SYS_STATUS] as 'SysStatus'
		,[SYS_CREATE_DATE] as 'SysCreateDate'
		,[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,[SYS_MODIFY_DATE] as 'SysModifyDate'
		,[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[Rooms] WHERE
		(@RoomId IS NULL OR [Id] = @RoomId)
		AND
		(@RecruiterId IS NULL OR [Id] IN (SELECT [RoomId] FROM [dbo].[Appointments] WHERE [RecruiterId] = @RecruiterId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@CandidateId IS NULL OR [Id] IN (SELECT [RoomId] FROM [dbo].[Appointments] WHERE [CandidateId] = @CandidateId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@OpeningId IS NULL OR [Id] IN (SELECT [RoomId] FROM [dbo].[Appointments] WHERE [OpeningId] = @OpeningId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@ClientId IS NULL OR [Id] IN (SELECT [RoomId] FROM [dbo].[Appointments] WHERE [OpeningId] IN (SELECT [Id] FROM [dbo].[Openings] WHERE [ClientId] = @ClientId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))) AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
END