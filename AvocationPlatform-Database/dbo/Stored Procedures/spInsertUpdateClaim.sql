CREATE PROCEDURE [dbo].[spInsertUpdateClaim]
	@ClaimId UNIQUEIDENTIFIER = NULL,
	@Name VARCHAR(100),
	@Value VARCHAR(MAX),
	@UserId UNIQUEIDENTIFIER = NULL,
	@RoleId UNIQUEIDENTIFIER = NULL,
	@AllowedOn DATETIME,
	@ExpiresOn DATETIME,
	@Username VARCHAR(150)
AS
BEGIN
	DECLARE @CurrentDate DATETIME = GETUTCDATE();

	IF(@Username IS NULL)
	BEGIN
		RETURN -1
	END

	IF(EXISTS(SELECT * FROM [dbo].[Claims] WHERE [Id] = @ClaimId))
	BEGIN
		UPDATE [Claims] SET [Name] = @Name,
			[Value] = @Value,
			[UserId] = @UserId,
			[RoleId] = @RoleId,
			[AllowedOn] = @AllowedOn,
			[ExpiresOn] = @ExpiresOn,
			[SYS_MODIFY_DATE] = GETUTCDATE(),
			[SYS_MODIFY_USER_ID] = @Username
		WHERE [Id] = @ClaimId
	END
	ELSE
	BEGIN
		SET @ClaimId = NEWID()

		INSERT INTO [Claims]([Id]
			,[Name]
			,[Value]
			,[UserId]
			,[RoleId]
			,[AllowedOn]
			,[ExpiresOn]
			,[SYS_CREATE_DATE]
			,[SYS_CREATE_USER_ID]
			,[SYS_MODIFY_DATE]
			,[SYS_MODIFY_USER_ID])
		VALUES(@ClaimId
			,@Name
			,@Value
			,@UserId
			,@RoleId
			,@AllowedOn
			,@ExpiresOn
			,@CurrentDate
			,@Username
			,@CurrentDate
			,@Username)
	END

	IF(@@ROWCOUNT > 0)
	BEGIN
		SELECT @ClaimId
	END
	ELSE
	BEGIN
		SELECT @ClaimId = NULL
	END
END
