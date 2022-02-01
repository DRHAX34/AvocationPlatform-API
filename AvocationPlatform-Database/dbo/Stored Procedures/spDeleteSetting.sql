CREATE PROCEDURE [dbo].[spDeleteSetting]
	@SettingId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END
	
	UPDATE [dbo].[Settings] SET 
		[SYS_STATUS] = 'X',
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @SettingId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @SettingId
	END
	ELSE
	BEGIN
		SELECT @SettingId = NULL
	END
END
