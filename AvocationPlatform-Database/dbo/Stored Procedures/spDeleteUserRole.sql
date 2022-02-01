CREATE PROCEDURE [dbo].[spDeleteUserRole]
	@UserId UNIQUEIDENTIFIER,
	@RoleId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @UserRoleId UNIQUEIDENTIFIER

	SELECT @UserRoleId = [Id] FROM [Users_Roles] WHERE [RoleId] = @RoleId AND [UserId] = @UserId

	UPDATE [Users_Roles] SET [SYS_STATUS] = 'X',
		[SYS_MODIFY_DATE] = GETUTCDATE(),
		[SYS_MODIFY_USER_ID] = @Username
	WHERE [RoleId] = @RoleId AND [UserId] = @UserId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @UserRoleId
	END
	ELSE
	BEGIN
		SELECT @UserRoleId = NULL
	END
END
