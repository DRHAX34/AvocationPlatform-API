CREATE PROCEDURE [dbo].[spDeleteClaim]
	@ClaimId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END
	
	UPDATE [dbo].[Claims] SET 
		[ExpiresOn] = GETUTCDATE(),
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @ClaimId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @ClaimId
	END
	ELSE
	BEGIN
		SELECT @ClaimId = NULL
	END
END
