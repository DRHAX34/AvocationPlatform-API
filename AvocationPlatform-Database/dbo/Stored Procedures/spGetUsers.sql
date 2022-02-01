CREATE PROCEDURE [dbo].[spGetUsers]
	@UserId UNIQUEIDENTIFIER = NULL,
	@RoleId UNIQUEIDENTIFIER = NULL,
	@WithDeleted BIT = 0
AS
BEGIN
	SELECT [Id]
		,[Photo]
		,[FirstName]
		,[LastName]
		,[Username]
		,[Email]
		,[Phone]
		,[SYS_STATUS] as 'SysStatus'
		,[SYS_CREATE_DATE] as 'SysCreateDate'
		,[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,[SYS_MODIFY_DATE] as 'SysModifyDate'
		,[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[Users]
	WHERE
		(@UserId IS NULL OR [Id] = @UserId)
		AND
		(@RoleId IS NULL OR [Id] IN (SELECT [UserId] FROM [Users_Roles] WHERE [RoleId] = @RoleId AND ([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
END
