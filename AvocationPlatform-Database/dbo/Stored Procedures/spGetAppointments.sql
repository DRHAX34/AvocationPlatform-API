CREATE PROCEDURE [dbo].[spGetAppointments]
	@AppointmentId UNIQUEIDENTIFIER = NULL,
	@CandidateId UNIQUEIDENTIFIER = NULL,
	@RecruiterId UNIQUEIDENTIFIER = NULL,
	@RoomId UNIQUEIDENTIFIER = NULL,
	@OpeningId UNIQUEIDENTIFIER = NULL,
	@ClientId UNIQUEIDENTIFIER = NULL,
	@WithDeleted BIT = 0
AS
BEGIN
	SELECT A.[Id]
		,[CandidateId]
		,[RecruiterId]
		,[RoomId]
		,[StartTime]
		,[EndTime]
		,[OpeningId]
		,[ClientId]
		,[Stage]
		,[SYS_SINGLE_USE_TOKEN] as 'SysSingleUseToken'
		,A.[SYS_STATUS] as 'SysStatus'
		,A.[SYS_CREATE_DATE] as 'SysCreateDate'
		,A.[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,A.[SYS_MODIFY_DATE] as 'SysModifyDate'
		,A.[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[Appointments] A LEFT JOIN [dbo].[Openings] B
	ON A.[OpeningId] = B.[Id] WHERE 
		(@AppointmentId IS NULL OR A.[Id] = @AppointmentId)
		AND
		(@CandidateId IS NULL OR [CandidateId] = (SELECT [Id] FROM [dbo].[Candidates] WHERE [Id] = @CandidateId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@RecruiterId IS NULL OR [RecruiterId] = (SELECT [Id] FROM [dbo].[Recruiters] WHERE [Id] = @RecruiterId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@RoomId IS NULL OR [RoomId] = (SELECT [Id] FROM [dbo].[Rooms] WHERE [Id] = @RoomId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@OpeningId IS NULL OR [OpeningId] = (SELECT [Id] FROM [dbo].[Openings] WHERE [Id] = @OpeningId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@ClientId IS NULL OR [OpeningId] IN (SELECT [Id] FROM [dbo].[Openings] WHERE [ClientId] = @ClientId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(A.[SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
		AND
		(B.[SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
	ORDER BY [EndTime] DESC
END
