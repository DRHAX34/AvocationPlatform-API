CREATE PROCEDURE [dbo].[spInsertUpdateSetting]
	@SettingId UNIQUEIDENTIFIER = NULL,
	@Name VARCHAR(150),
	@Value VARCHAR(MAX),
	@UserId UNIQUEIDENTIFIER = NULL,
	@RoleId UNIQUEIDENTIFIER = NULL,
	@SysStatus VARCHAR(5) = 'O',
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Settings] WHERE [Id] = @SettingId))
	BEGIN
		UPDATE [dbo].[Settings] SET
			[Name] = @Name,
			[Value] = @Value,
			[UserId] = @UserId,
			[RoleId] = @RoleId,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @SettingId
	END
	ELSE
	BEGIN
		SET @SettingId = NEWID()

		INSERT INTO [Settings]([Id]
			,[Name]
			,[Value]
			,[UserId]
			,[RoleId]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@SettingId
			,@Name
			,@Value
			,@UserId
			,@RoleId
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @SettingId
	END
	ELSE
	BEGIN
		SELECT @SettingId = NULL
	END
END
