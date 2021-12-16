CREATE PROCEDURE [dbo].[spInsertUpdateClient]
	@ClientId UNIQUEIDENTIFIER = NULL,
	@Name VARCHAR(MAX),
	@VAT VARCHAR(MAX),
	@PictureUri VARCHAR(MAX) = NULL,
	@Address VARCHAR(MAX) = NULL,
	@ZipCode VARCHAR(MAX) = NULL,
	@City VARCHAR(MAX) = NULL,
	@Phone VARCHAR(40) = NULL,
	@Email VARCHAR(320) = NULL,
	@SysStatus VARCHAR(5) = 'O',
	@UserId UNIQUEIDENTIFIER = NULL,
	@Username VARCHAR(100) = NULL
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL AND @UserId IS NULL)
	BEGIN
		RETURN -1
	END

	IF(@Username IS NULL)
	BEGIN
		SELECT @Username = [NormalizedUsername] FROM [dbo].[Users] WHERE [Id] = @UserId
	END

	IF(EXISTS(SELECT * FROM [dbo].[Clients] WHERE [Id] = @ClientId))
	BEGIN
		UPDATE [dbo].[Clients] SET
			[Name] = @Name,
			[VAT] = @VAT,
			[PictureUri] = @PictureUri,
			[Address] = @Address,
			[ZipCode] = @ZipCode,
			[City] = @City,
			[Phone] = @Phone,
			[Email] = @Email,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @ClientId
	END
	ELSE
	BEGIN
		SET @ClientId = NEWID()

		INSERT INTO [Clients]([Id]
			,[Name]
			,[VAT]
			,[PictureUri]
			,[Address]
			,[ZipCode]
			,[City]
			,[Phone]
			,[Email]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@ClientId
			,@Name
			,@VAT
			,@PictureUri
			,@Address
			,@ZipCode
			,@City
			,@Phone
			,@Email
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @ClientId
	END
	ELSE
	BEGIN
		SELECT @ClientId = NULL
	END
END
