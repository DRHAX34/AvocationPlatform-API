CREATE PROCEDURE [dbo].[spInsertRoleUser]
	@RoleId UNIQUEIDENTIFIER,
	@UserId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	
	DECLARE @UserRoleId UNIQUEIDENTIFIER

	SELECT @UserRoleId = [Id] FROM [Users_Roles] WHERE [RoleId] = @RoleId AND [UserId] = @UserId

	IF(@UserRoleId IS NOT NULL)
	BEGIN
		UPDATE [Users_Roles] SET [SYS_STATUS] = 'O',
			[SYS_MODIFY_DATE] = GETUTCDATE(),
			[SYS_MODIFY_USER_ID] = @Username
		WHERE [RoleId] = @RoleId AND [UserId] = @UserId
	END
	ELSE
	BEGIN
		SET @UserRoleId = NEWID()

		INSERT INTO [Users_Roles]([Id]
			,[RoleId]
			,[UserId]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES (@UserRoleId
			,@RoleId
			,@UserId
			,'O'
			,GETUTCDATE()
			,@Username
			,GETUTCDATE()
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @UserRoleId
	END
	ELSE
	BEGIN
		SELECT @UserRoleId = NULL
	END
END
