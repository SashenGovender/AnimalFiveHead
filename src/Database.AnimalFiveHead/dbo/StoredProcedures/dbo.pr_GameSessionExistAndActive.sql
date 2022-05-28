CREATE PROCEDURE [dbo].[pr_GameSessionExistAndActive]
  @SessionId uniqueidentifier

AS
  BEGIN
    DECLARE @SessionExistsAndActive INT;
    DECLARE @MessageResult NVARCHAR(50);

    IF EXISTS ( SELECT SessionId FROM dbo.tb_PlayerSessionInformation (NOLOCK) WHERE SessionId = @SessionId AND GameSession = 'Active' )
      BEGIN
        SET @SessionExistsAndActive = 1
      END
    ELSE
      BEGIN
        SET @SessionExistsAndActive = 0
      END

    SELECT @SessionExistsAndActive 
  END
GO

