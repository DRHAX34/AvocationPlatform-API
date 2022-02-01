CREATE PROCEDURE [dbo].[spGetClaims]
	@ClaimId UNIQUEIDENTIFIER = NULL,
	@UserId UNIQUEIDENTIFIER = NULL,
	@RoleId UNIQUEIDENTIFIER = NULL,
	@WithDeleted BIT = 0
AS
BEGIN
	SELECT [Id]
		,[Name]
		,[Value]
		,[UserId]
		,[RoleId]
		,[AllowedOn]
		,[ExpiresOn]
		,[SYS_CREATE_DATE] as 'SysCreateDate'
		,[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,[SYS_MODIFY_DATE] as 'SysModifyDate'
		,[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[Claims] WHERE
		(@ClaimId IS NULL OR [Id] = @ClaimId)
		AND
		(@UserId IS NULL OR 
		([UserId] = @UserId OR [RoleId] IN (SELECT r.[Id] FROM [Roles] r INNER JOIN [Users_Roles] ur ON r.[Id] = ur.[RoleId]
		WHERE ur.[UserId] = @UserId AND r.[SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1))
		AND ur.[SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@RoleId IS NULL OR [RoleId] = @RoleId)
		AND
		(@WithDeleted = 1 OR ([AllowedOn] <= GETUTCDATE() AND [ExpiresOn] > GETUTCDATE()))
END
