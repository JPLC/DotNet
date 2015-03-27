use [TrelloDB]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--List Stored Procedures
CREATE PROCEDURE [dbo].[EditList]
	@ListId int,
	@Lix int,
	@BoardId int,
	@Name varchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			DECLARE @oldindex int = (select Lix from List where ListId=@ListId)

			IF(@oldindex>@Lix)
			BEGIN 
				UPDATE List
				SET Lix = Lix+1
				WHERE @BoardId = BoardId and Lix < @oldindex and Lix >= @Lix 
			END
			ELSE
			BEGIN
				update List
				set Lix = Lix-1
				Where @BoardId = BoardId and Lix > @oldindex and Lix <= @Lix 
			END

			UPDATE List
			SET Lix=@Lix, BoardId=@BoardId, Name=@Name
			Where ListId=@ListId

		COMMIT
	END TRY

	BEGIN CATCH
		ROLLBACK
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[DeleteList]
	@ListId int,
	@Lix int,
	@BoardId int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			DELETE FROM List Where ListId=@ListId

			UPDATE List
			SET Lix = Lix-1
			WHERE @BoardId = BoardId and Lix > @Lix 

		COMMIT
	END TRY

	BEGIN CATCH
		ROLLBACK
	END CATCH
END
GO

--Card Stored Procedures

CREATE PROCEDURE [dbo].[DeleteCard]
	@CardId int,
	@Cix int,
	@ListId int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN
			DELETE FROM [Card] Where CardId=@CardId

			UPDATE [Card]
			SET Cix = Cix-1
			WHERE ListId = @ListId and Cix > @Cix 

			COMMIT
	END TRY

	BEGIN CATCH
		ROLLBACK
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[EditCard]
	 @CardId int
	,@Cix int
	,@Name varchar(255)
	,@Discription varchar(255)
	,@CreationDate Date
	,@DueDate Date
	,@ListId int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRAN

		COMMIT
	END TRY

	BEGIN CATCH
		ROLLBACK
	END CATCH
END
GO