CREATE PROCEDURE [dbo].[spInsertUpdateRoom]
	@RoomId UNIQUEIDENTIFIER = NULL,
	@Name VARCHAR(MAX),
	@SysStatus VARCHAR(5) = 'O',
	@Username VARCHAR(150) = NULL
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Rooms] WHERE [Id] = @RoomId))
	BEGIN
		UPDATE [dbo].[Rooms] SET
			[Name] = @Name,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @RoomId
	END
	ELSE
	BEGIN
		SET @RoomId = NEWID()

		INSERT INTO [Rooms]([Id]
			,[Name]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@RoomId
			,@Name
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @RoomId
	END
	ELSE
	BEGIN
		SELECT @RoomId = NULL
	END
END
