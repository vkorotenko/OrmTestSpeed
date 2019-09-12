USE [testapp]
GO
/****** Object:  StoredProcedure [dbo].[sp_Add_Item]    Script Date: 11.09.2015 22:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Korotenko Vladimir>
-- Create date: <11 September 2015>
-- =============================================
ALTER PROCEDURE [dbo].[sp_Add_Item] 
	
	@CoreID uniqueidentifier,
	@Name nvarchar(MAX),
	@CityID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Items]
           ([CoreID]
           ,[Name]
           ,[CityID])
     VALUES
           (@CoreID
           ,@Name
           ,@CityID)
END