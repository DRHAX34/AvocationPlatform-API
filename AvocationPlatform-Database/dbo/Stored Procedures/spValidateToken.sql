CREATE PROCEDURE [dbo].[spValidateToken]
	@Token VARCHAR(MAX)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	SELECT [SignatureKey] FROM [dbo].[TokenHistory] WHERE [Token] = @Token AND [ExpiresOn] > @CurrentDate AND [AllowedOn] <= @CurrentDate
END
