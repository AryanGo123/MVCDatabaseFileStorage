CREATE TABLE [dbo].[FileTable]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CreatorName] NVARCHAR(50) NULL, 
    [TaskName] NVARCHAR(50) NULL, 
    [DateCreated] DATETIME NULL, 
    [FileName] NVARCHAR(50) NULL, 
    [MimeType] NVARCHAR(150) NULL, 
    [Base64String] NVARCHAR(MAX) NULL
)
