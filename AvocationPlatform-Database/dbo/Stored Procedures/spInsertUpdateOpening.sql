CREATE PROCEDURE [dbo].[spInsertUpdateOpening]
	@OpeningId UNIQUEIDENTIFIER = NULL,
	@ClientId UNIQUEIDENTIFIER,
	@Title VARCHAR(MAX),
	@Description VARCHAR(MAX),
	@SysStatus VARCHAR(5) = 'O',
	@Username VARCHAR(150) = NULL
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Openings] WHERE [Id] = @OpeningId))
	BEGIN
		UPDATE [dbo].[Openings] SET
			[Title] = @Title,
			[Description] = @Description,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @OpeningId
	END
	ELSE
	BEGIN
		SET @OpeningId = NEWID()

		INSERT INTO [Openings]([Id]
			,[ClientId]
			,[Title]
			,[Description]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@OpeningId
			,@ClientId
			,@Title
			,@Description
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @OpeningId
	END
	ELSE
	BEGIN
		SELECT @OpeningId = NULL
	END
END
