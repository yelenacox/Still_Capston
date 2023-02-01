USE [master]

IF db_id('Still') IS NULl
  CREATE DATABASE [Still]
GO

USE [Still]
GO


DROP TABLE IF EXISTS [Picture];
DROP TABLE IF EXISTS [UserProfile];
DROP TABLE IF EXISTS [Page];
DROP TABLE IF EXISTS [PagePicture];
GO

CREATE TABLE [Picture] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserProfileId] int NOT NULL,
  [Description] nvarchar(255),
  [DateCreated] datetime NOT NULL,
  [PictureLocation] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FireBaseUserId] nvarchar(255) UNIQUE NOT NULL,
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Page] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserProfileId] int NOT NULL,
  [Title] nvarchar(255) NOT NULL,
  [Description] nvarchar(255),
  [DateCreated] datetime NOT NULL
)
GO

CREATE TABLE [PagePicture] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [PageId] int NOT NULL,
  [PictureId] int NOT NULL,
  [Description] nvarchar(255)
)
GO

ALTER TABLE [Picture] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Page] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [PagePicture] ADD FOREIGN KEY ([PageId]) REFERENCES [Page] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [PagePicture] ADD FOREIGN KEY ([PictureId]) REFERENCES [Picture] ([Id]) ON DELETE CASCADE
GO
