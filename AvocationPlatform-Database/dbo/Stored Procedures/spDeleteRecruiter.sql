CREATE PROCEDURE [dbo].[spDeleteRecruiter]
	@RecruiterId UNIQUEIDENTIFIER,
	@UserId UNIQUEIDENTIFIER = NULL,
	@Username VARCHAR(100) = NULL
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL AND @UserId IS NULL)
	BEGIN
		RETURN -1
	END

	IF(@Username IS NULL)
	BEGIN
		SELECT @Username = [NormalizedUsername] FROM [dbo].[Users] WHERE [Id] = @UserId
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
