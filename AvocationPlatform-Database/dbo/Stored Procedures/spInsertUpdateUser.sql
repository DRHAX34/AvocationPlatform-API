CREATE PROCEDURE [dbo].[spInsertUpdateUser]
	@UserId UNIQUEIDENTIFIER = NULL,
	@Photo VARCHAR(MAX),
	@FirstName VARCHAR(150),
	@LastName VARCHAR(150),
	@AffectedUsername VARCHAR(150),
	@Email VARCHAR(320),
	@Phone VARCHAR(15) = NULL,
	@HashedPassword VARCHAR(MAX),
	@SysStatus VARCHAR(5) = 'O',
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Users] WHERE [Id] = @UserId))
	BEGIN
		UPDATE [Users] SET [Photo] = @Photo,
			[FirstName] = @FirstName,
			[LastName] = @LastName,
			[Username] = @Username,
			[Email] = @Email,
			[Phone] = @Phone,
			[HashedPassword] = @HashedPassword,
			[SYS_STATUS] = @SysStatus,
			[SYS_MODIFY_DATE] = GETUTCDATE(),
			[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @UserId
	END
	ELSE
	BEGIN
		SET @UserId = NEWID()

		INSERT INTO [Users]([Id]
			,[Photo]
			,[FirstName]
			,[LastName]
			,[Username]
			,[Email]
			,[Phone]
			,[HashedPassword]
			,[SYS_STATUS]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@UserId
			,@Photo
			,@FirstName
			,@LastName
			,@AffectedUsername
			,@Email
			,@Phone
			,@HashedPassword
			,@SysStatus
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @UserId
	END
	ELSE
	BEGIN
		SELECT @UserId = NULL
	END
END
