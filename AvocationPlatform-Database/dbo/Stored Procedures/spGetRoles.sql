CREATE PROCEDURE [dbo].[spGetRoles]
	@RoleId UNIQUEIDENTIFIER = NULL,
	@UserId UNIQUEIDENTIFIER = NULL,
	@WithDeleted BIT = 0
AS
BEGIN
	SELECT DISTINCT r.[Id]
		,r.[Name]
		,r.[Description]
		,r.[SYS_STATUS] as 'SysStatus'
		,r.[SYS_CREATE_DATE] as 'SysCreateDate'
		,r.[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,r.[SYS_MODIFY_DATE] as 'SysModifyDate'
		,r.[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[Roles] r
	INNER JOIN [dbo].[Users_Roles] ur
	ON r.Id = ur.RoleId
	INNER JOIN [dbo].[Users] u
	ON ur.UserId = u.Id
	WHERE
		(@RoleId IS NULL OR r.[Id] = @RoleId)
		AND
		(@UserId IS NULL OR u.[Id] = @UserId)
		AND
		(r.[SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
		AND
		(u.[SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
END
