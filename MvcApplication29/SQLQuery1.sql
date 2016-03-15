
CREATE TABLE [dbo].[Messages]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,  
    [UserGet] INT NOT NULL, 
    [UserPost] INT NOT NULL, 
    [Text] NVARCHAR(MAX) NOT NULL, 
	[TIME] DATETIME NOT NULL,
	[IsRead] BIT NOT NULL,
     FOREIGN KEY ([UserGet]) REFERENCES [dbo].[UserProfile] ([UserId]),
	 FOREIGN KEY ([UserPost]) REFERENCES [dbo].[UserProfile] ([UserId])
  )