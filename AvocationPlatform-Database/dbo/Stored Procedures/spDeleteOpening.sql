CREATE PROCEDURE [dbo].[spDeleteOpening]
	@OpeningId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END
	
	UPDATE [dbo].[Openings] SET 
		[SYS_STATUS] = 'X',
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @OpeningId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @OpeningId
	END
	ELSE
	BEGIN
		SELECT @OpeningId = NULL
	END
END
