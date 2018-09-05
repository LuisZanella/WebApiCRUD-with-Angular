
CREATE DATABASE [OurLives]
GO
USE [OurLives]
GO

CREATE TABLE [User](
	Id INT PRIMARY KEY IDENTITY,	
	[Name] VARCHAR(50),
	FLastName VARCHAR(50),
	SLastName VARCHAR(50),
	Nick  VARCHAR(50),
	[Password]  VARCHAR(50),
	BirthDate DATE DEFAULT GETDATE(),
	[Type] INT DEFAULT 0,
	[Photo] NVARCHAR(MAX) DEFAULT '',
	[Status] BIT DEFAULT 1
)
GO
INSERT INTO [User] ([Name],FLastName,SLastName,Nick,[Password],BirthDate,[Status]) VALUES
	('Luis', 'Zanella', 'Contreras', 'Mua', 'Zane!', '1999-04-17',1),
	('Juan', 'Viloria', 'Alvárez', 'Carlitos', 'Carlos', '1999-01-10',1),
	('Sofia', 'Enriquez', 'Lopéz', 'Kim', 'SoySofiLaBonita :) <3', '1999-02-28',1)
GO
CREATE PROCEDURE dbo.sp_AllUsers
AS
	SELECT * FROM [User] WHERE [Status] = 1 ORDER BY Id ASC 
GO
CREATE PROCEDURE dbo.sp_InsertUser
@Name VARCHAR(50),
@FLastName VARCHAR(50),
@SLastName VARCHAR(50),
@Nick	VARCHAR(50),
@Password VARCHAR(50),
@BirthDate DATE,
@Photo NVARCHAR(Max) = null
AS
BEGIN
	IF(@Photo IS NOT NULL)
		INSERT INTO [User] ([Name],FLastName,SLastName,Nick,[Password],BirthDate, Photo) VALUES
		(@Name,@FLastName,@SLastName,@Nick,@Password,@BirthDate, @Photo)
	IF(@Photo IS NULL)
		INSERT INTO [User] ([Name],FLastName,SLastName,Nick,[Password],BirthDate) VALUES
		(@Name,@FLastName,@SLastName,@Nick,@Password,@BirthDate)
END
GO
CREATE PROCEDURE dbo.sp_UpdatetUser
@Id INT,
@Name VARCHAR(50) = null,
@FLastName VARCHAR(50) = null,
@SLastName VARCHAR(50) = null,
@Nick	VARCHAR(50) = null,
@Password VARCHAR(50) = null,
@BirthDate DATE = null
AS
BEGIN

    IF (@Name IS NOT NULL)
        -- Update Just for Name
		UPDATE [User] 
		SET [Name] = @Name 
		WHERE Id = @Id
    IF (@FLastName IS NOT NULL)
        -- Update Just for First Last Name
		UPDATE [User] 
		SET [FLastName] = @FLastName 
		WHERE Id = @Id
    IF (@SLastName IS NOT NULL)
        -- Update Just for Second Last Name
		UPDATE [User] 
		SET SLastName = @SLastName 
		WHERE Id = @Id
	IF (@Nick IS NOT NULL)
        -- Update Just for Nick
		UPDATE [User] 
		SET Nick = @Nick 
		WHERE Id = @Id
	IF (@Password IS NOT NULL)
        -- Update Just for Password
		UPDATE [User] 
		SET [Password] = @Password 
		WHERE Id = @Id
	IF (@BirthDate IS NOT NULL)
        -- Update Just for BirthDate
		UPDATE [User] 
		SET BirthDate = @BirthDate 
		WHERE Id = @Id
END
GO
CREATE PROCEDURE dbo.sp_DeleteUser
@Id Int
AS
	UPDATE [User]
	SET [Status] = 0
	WHERE Id = @Id
GO

CREATE TABLE Diary(
	Id INT PRIMARY KEY IDENTITY,
	[Description] VARCHAR(MAX),
	[Date] DATE DEFAULT GETDATE(),
	[User_Id] INT DEFAULT 0,
	[Status] BIT DEFAULT 1
)
GO
CREATE PROCEDURE dbo.sp_AllDiaries
AS
	SELECT * FROM [Diary] WHERE [Status] = 1 ORDER BY [Date] ASC 
GO
CREATE PROCEDURE dbo.sp_InsertDiary
@Description VARCHAR(50),
@Date DATE = Null,
@IdUser INT = Null
AS
BEGIN
	IF(@Date IS NOT NULL AND @IdUser IS NOT NULL)
		INSERT INTO [Diary] ([Description],[Date],[User_Id]) VALUES
		(@Description,@Date,@IdUser)
	IF(@Date IS NULL  AND @IdUser IS NOT NULL)
		INSERT INTO [Diary] ([Description],[User_Id]) VALUES
		(@Description,@IdUser)
	IF(@IdUser IS NULL  AND @Date IS NOT NULL)
		INSERT INTO [Diary] ([Description],[Date]) VALUES
		(@Description,@Date)
END
GO
CREATE PROCEDURE dbo.sp_UpDiary
@Description NVARCHAR (MAX) = NULL,
@Date DATE = NULL,
@Id INT
AS
BEGIN

    IF (@Description IS NOT NULL)
        -- Update Just for Description
		UPDATE [Diary] 
		SET [Description] = @Description 
		WHERE Id = @Id
    IF (@Date IS NOT NULL)
        -- Update Just for Date
		UPDATE [Diary] 
		SET [Date] = @Date 
		WHERE Id = @Id
END
GO
CREATE PROCEDURE dbo.sp_DeleteDiary
@Id Int
AS
	UPDATE [Diary]
	SET [Status] = 0
	WHERE Id = @Id
GO
