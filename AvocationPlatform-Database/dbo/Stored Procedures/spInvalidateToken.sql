CREATE PROCEDURE [dbo].[spInvalidateToken]
	@Token VARCHAR(MAX),
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	DECLARE @TokenId UNIQUEIDENTIFIER

	SELECT @TokenId = [Id] FROM [TokenHistory] WHERE [Token] = @Token
	
	UPDATE [dbo].[TokenHistory] SET 
		[RefreshToken] = NULL,
		[ExpiresOn] = GETUTCDATE(),
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Token] = @Token

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @TokenId
	END
	ELSE
	BEGIN
		SELECT @TokenId = NULL
	END
END
