CREATE PROCEDURE [dbo].[spGetTokenHistory]
	@TokenId UNIQUEIDENTIFIER = NULL,
	@RefreshToken VARCHAR(MAX) = NULL,
	@UserId UNIQUEIDENTIFIER = NULL,
	@WithDeleted BIT = 0
AS
BEGIN
	SELECT [Id]
		,[Token]
		,[RefreshToken]
		,[AllowedOn]
		,[ExpiresOn]
		,[UserId]
		,[SYS_CREATE_DATE] as 'SysCreateDate'
		,[SYS_CREATE_USER_ID] as 'SysCreateUserId'
		,[SYS_MODIFY_DATE] as 'SysModifyDate'
		,[SYS_MODIFY_USER_ID] as 'SysModifyUserId'
	FROM [dbo].[TokenHistory]
	WHERE
		(@TokenId IS NULL OR [Id] = @TokenId)
		AND
		(@RefreshToken IS NULL OR [RefreshToken] = @RefreshToken)
		AND
		(@UserId IS NULL OR [UserId] = @UserId)
		AND
		(@WithDeleted = 1 OR (AllowedOn < GETUTCDATE() AND ExpiresOn > GETUTCDATE()))
END
