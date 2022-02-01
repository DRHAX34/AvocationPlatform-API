CREATE PROCEDURE [dbo].[spDeleteRecruiter]
	@RecruiterId UNIQUEIDENTIFIER,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END
	
	UPDATE [dbo].[Recruiters] SET 
		[SYS_STATUS] = 'X',
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @RecruiterId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @RecruiterId
	END
	ELSE
	BEGIN
		SELECT @RecruiterId = NULL
	END
END
