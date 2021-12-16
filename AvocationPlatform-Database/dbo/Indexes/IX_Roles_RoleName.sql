CREATE UNIQUE INDEX [IX_Roles_RoleName]
	ON [dbo].[Roles]
	([NormalizedName])
	WHERE [NormalizedName] IS NOT NULL
	
