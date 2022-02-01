CREATE PROCEDURE [dbo].[spDeleteTokenHistory]
	@TokenId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END
	
	UPDATE [dbo].[TokenHistory] SET 
		[ExpiresOn] = GETUTCDATE(),
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @TokenId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @TokenId
	END
	ELSE
	BEGIN
		SELECT @TokenId = NULL
	END
END
