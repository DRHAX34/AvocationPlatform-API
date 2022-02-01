CREATE PROCEDURE [dbo].[spInsertUpdateRole]
	@RoleId UNIQUEIDENTIFIER,
	@Name VARCHAR(150),
	@Description VARCHAR(300),
	@SysStatus VARCHAR(5) = 'O',
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Roles] WHERE [Id] = @RoleId))
	BEGIN
		UPDATE [dbo].[Roles] SET
			[Name] = @Name,
			[Description] = @Description,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @RoleId
	END
	ELSE
	BEGIN
		SET @RoleId = NEWID()

		INSERT INTO [Roles]([Id]
			,[Name]
			,[Description]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@RoleId
			,@Name
			,@Description
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @RoleId
	END
	ELSE
	BEGIN
		SELECT @RoleId = NULL
	END
END
