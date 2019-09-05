USE [SN7_Upgrade]
GO
SET IDENTITY_INSERT [dbo].[AccessTokens] ON 

GO
INSERT [dbo].[AccessTokens] ([AccessTokenId], [Value], [UserId], [ContentId], [Feature], [CreationDate], [ExpirationDate]) VALUES (1, N'Value1', 1, 2, NULL, CAST(N'2019-09-05T01:01:01.110' AS DateTime), CAST(N'2019-09-05T01:01:02.110' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AccessTokens] OFF
GO
