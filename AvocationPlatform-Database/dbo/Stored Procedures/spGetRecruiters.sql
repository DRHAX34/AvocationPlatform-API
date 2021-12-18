CREATE PROCEDURE [dbo].[spGetRecruiters]
	@RecruiterId UNIQUEIDENTIFIER = NULL,
	@CandidateId UNIQUEIDENTIFIER = NULL,
	@ClientId UNIQUEIDENTIFIER = NULL,
	@OpeningId UNIQUEIDENTIFIER = NULL,
	@RoomId UNIQUEIDENTIFIER = NULL,
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
	FROM [dbo].[Recruiters] WHERE
		(@RecruiterId IS NULL OR [Id] = @RecruiterId)
		AND
		(@CandidateId IS NULL OR [Id] IN (SELECT [RecruiterId] FROM [dbo].[Appointments] WHERE [CandidateId] = @CandidateId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@RoomId IS NULL OR [Id] IN (SELECT [RecruiterId] FROM [dbo].[Appointments] WHERE [RoomId] = @RoomId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@OpeningId IS NULL OR [Id] IN (SELECT [RecruiterId] FROM [dbo].[Appointments] WHERE [OpeningId] = @OpeningId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@ClientId IS NULL OR [Id] IN (SELECT [RecruiterId] FROM [dbo].[Appointments] WHERE [OpeningId] IN (SELECT [OpeningId] FROM [dbo].[Openings] WHERE [ClientId] = @ClientId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))) AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
END