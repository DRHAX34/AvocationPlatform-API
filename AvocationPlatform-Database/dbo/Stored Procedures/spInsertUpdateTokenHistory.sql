CREATE PROCEDURE [dbo].[spInsertUpdateTokenHistory]
	@TokenId UNIQUEIDENTIFIER = NULL,
	@Token VARCHAR(MAX),
	@RefreshToken VARCHAR(MAX),
	@SignatureKey VARCHAR(MAX),
	@AllowedOn DATETIME,
	@ExpiresOn DATETIME,
	@UserId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[TokenHistory] WHERE [Id] = @TokenId OR [Token] = @Token))
	BEGIN
		UPDATE [dbo].[TokenHistory] SET
			[Token] = @Token,
			[RefreshToken] = @RefreshToken,
			[SignatureKey] = @SignatureKey,
			[AllowedOn] = @AllowedOn,
			[ExpiresOn] = @ExpiresOn,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @TokenId
	END
	ELSE
	BEGIN
		SET @TokenId = NEWID()

		INSERT INTO [TokenHistory]([Id]
			,[Token]
			,[RefreshToken]
			,[SignatureKey]
			,[AllowedOn]
			,[ExpiresOn]
			,[UserId]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@TokenId
			,@Token
			,@RefreshToken
			,@SignatureKey
			,@AllowedOn
			,@ExpiresOn
			,@UserId
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @TokenId
	END
	ELSE
	BEGIN
		SELECT @TokenId = NULL
	END
END