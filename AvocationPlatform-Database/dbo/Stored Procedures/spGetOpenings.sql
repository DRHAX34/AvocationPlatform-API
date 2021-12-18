CREATE PROCEDURE [dbo].[spGetOpenings]
	@OpeningId UNIQUEIDENTIFIER = NULL,
	@ClientId UNIQUEIDENTIFIER = NULL,
	@RecruiterId UNIQUEIDENTIFIER = NULL,
	@CandidateId UNIQUEIDENTIFIER = NULL,
	@WithDeleted BIT = 0
AS
BEGIN
	SELECT [Id]
		,[ClientId]
		,[Title]
		,[Description]
		,[SYS_STATUS] as 'SysStatus'
		,[SYS_CREATE_DATE] as 'SysCreateDate'
		,[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,[SYS_MODIFY_DATE] as 'SysModifyDate'
		,[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[Openings] WHERE
		(@OpeningId IS NULL OR [Id] = @OpeningId)
		AND
		(@ClientId IS NULL OR [ClientId] = @ClientId)
		AND
		(@RecruiterId IS NULL OR [Id] IN (SELECT [OpeningId] FROM [dbo].[Appointments] WHERE [RecruiterId] = @RecruiterId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@CandidateId IS NULL OR [Id] IN (SELECT [OpeningId] FROM [dbo].[Appointments] WHERE [CandidateId] = @CandidateId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
END
