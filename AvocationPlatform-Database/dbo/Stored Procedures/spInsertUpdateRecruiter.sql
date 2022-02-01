CREATE PROCEDURE [dbo].[spInsertUpdateRecruiter]
	@RecruiterId UNIQUEIDENTIFIER = NULL,
	@Name VARCHAR(MAX),
	@SysStatus VARCHAR(5) = 'O',
	@Username VARCHAR(150) = NULL
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Recruiters] WHERE [Id] = @RecruiterId))
	BEGIN
		UPDATE [dbo].[Recruiters] SET
			[Name] = @Name,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @RecruiterId
	END
	ELSE
	BEGIN
		SET @RecruiterId = NEWID()

		INSERT INTO [Recruiters]([Id]
			,[Name]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@RecruiterId
			,@Name
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @RecruiterId
	END
	ELSE
	BEGIN
		SELECT @RecruiterId = NULL
	END
END
