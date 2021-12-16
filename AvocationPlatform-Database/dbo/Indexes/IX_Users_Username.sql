CREATE UNIQUE INDEX [IX_Users_Username]
	ON [dbo].[Users]
	([NormalizedUsername])
	WHERE [NormalizedUsername] IS NOT NULL
