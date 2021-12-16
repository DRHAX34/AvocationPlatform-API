CREATE PROCEDURE [dbo].[spDeleteCandidate]
	@CandidateId UNIQUEIDENTIFIER,
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
	
	UPDATE [dbo].[Candidates] SET 
		[SYS_STATUS] = 'X',
		[SYS_MODIFY_DATE] = @CurrentDate,
		[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @CandidateId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @CandidateId
	END
	ELSE
	BEGIN
		SELECT @CandidateId = NULL
	END
END
