﻿CREATE PROCEDURE [dbo].[spInsertUpdateCandidate]
	@CandidateId UNIQUEIDENTIFIER = NULL,
	@Name VARCHAR(MAX),
	@Surname VARCHAR(MAX),
	@PreferredName VARCHAR(MAX),
	@Email VARCHAR(320),
	@ProfilePictureUri VARCHAR(MAX) = NULL,
	@Phone VARCHAR(40) = NULL,
	@VAT VARCHAR(MAX) = NULL,
	@Company VARCHAR(MAX) = NULL,
	@Address VARCHAR(MAX) = NULL,
	@ZipCode VARCHAR(MAX) = NULL,
	@City VARCHAR(MAX) = NULL,
	@UserId UNIQUEIDENTIFiER = NULL,
	@SysStatus VARCHAR(5) = 'O',
	@Username VARCHAR(150) = NULL
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Candidates] WHERE [Id] = @CandidateId))
	BEGIN
		UPDATE [dbo].[Candidates] SET
			[Name] = @Name,
			[Surname] = @Surname,
			[PreferredName] = @PreferredName,
			[Email] = @Email,
			[Phone] = @Phone,
			[ProfilePictureUri] = @ProfilePictureUri,
			[VAT] = @VAT,
			[Company] = @Company,
			[Address] = @Address,
			[ZipCode] = @ZipCode,
			[City] = @City,
			[UserId] = @UserId,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = @CurrentDate,
			[SYS_MODIFY_USER_ID] = @Username
			WHERE [Id] = @CandidateId
	END
	ELSE
	BEGIN
		SET @CandidateId = NEWID()

		INSERT INTO [Candidates]([Id]
			,[Name]
			,[Surname]
			,[PreferredName]
			,[Email]
			,[Phone]
			,[ProfilePictureUri]
			,[VAT]
			,[Company]
			,[Address]
			,[ZipCode]
			,[City]
			,[UserId]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@CandidateId
			,@Name
			,@Surname
			,@PreferredName
			,@Email
			,@Phone
			,@ProfilePictureUri
			,@VAT
			,@Company
			,@Address
			,@ZipCode
			,@City
			,@UserId
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @CandidateId
	END
	ELSE
	BEGIN
		SELECT @CandidateId = NULL
	END
END
