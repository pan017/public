CREATE TABLE [dbo].[Messages] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [UserGet]  INT            NOT NULL,
    [UserPost] INT            NOT NULL,
    [Text]     NVARCHAR (MAX) NOT NULL,
    [TIME]     DATETIME       NOT NULL,
    [IsRead]   BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UserGet_UserId] FOREIGN KEY ([UserGet]) REFERENCES [dbo].[UserProfile] ([UserId]),
    CONSTRAINT [UserPost_UserId] FOREIGN KEY ([UserPost]) REFERENCES [dbo].[UserProfile] ([UserId])
);

