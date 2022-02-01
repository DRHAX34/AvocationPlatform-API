CREATE PROCEDURE [dbo].[spDeleteRole]
	@RoleId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END
	
	UPDATE [dbo].[Roles] SET 
		[SYS_STATUS] = 'X',
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @RoleId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @RoleId
	END
	ELSE
	BEGIN
		SELECT @RoleId = NULL
	END
END
