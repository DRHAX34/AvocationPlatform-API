CREATE PROCEDURE [dbo].[spDeleteClient]
	@ClientId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END
	
	UPDATE [dbo].[Clients] SET 
		[SYS_STATUS] = 'X',
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @ClientId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @ClientId
	END
	ELSE
	BEGIN
		SELECT @ClientId = NULL
	END
END
