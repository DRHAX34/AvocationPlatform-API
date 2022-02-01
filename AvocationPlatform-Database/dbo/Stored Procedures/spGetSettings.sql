CREATE PROCEDURE [dbo].[spGetSettings]
	@SettingId UNIQUEIDENTIFIER = NULL,
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
		,[SYS_STATUS] as 'SysStatus'
		,[SYS_CREATE_DATE] as 'SysCreateDate'
		,[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,[SYS_MODIFY_DATE] as 'SysModifyDate'
		,[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[Settings] s
	WHERE
		(@SettingId IS NULL OR [Id] = @SettingId)
		AND
		(@UserId IS NULL OR 
		([UserId] = @UserId OR [RoleId] IN (SELECT r.[Id] FROM [Roles] r INNER JOIN [Users_Roles] ur ON r.[Id] = ur.[RoleId]
		WHERE ur.[UserId] = @UserId AND r.[SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1))
		AND ur.[SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))))
		AND
		(@RoleId IS NULL OR [RoleId] = @RoleId)
		AND
		([SYS_STATUS] IN ('O', (SELECT 'X' WHERE @WithDeleted = 1)))
END
