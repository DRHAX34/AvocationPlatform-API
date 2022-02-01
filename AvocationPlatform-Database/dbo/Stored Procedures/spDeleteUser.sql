﻿CREATE PROCEDURE [dbo].[spDeleteUser]
	@UserId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END
	
	UPDATE [dbo].[Users] SET 
		[SYS_STATUS] = 'X',
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @UserId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @UserId
	END
	ELSE
	BEGIN
		SELECT @UserId = NULL
	END
END