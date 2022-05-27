CREATE PROCEDURE [dbo].[pr_CompleteGameSession]
  @SessionId uniqueidentifier

AS
  BEGIN
    UPDATE dbo.tb_PlayerSessionInformation
    SET GameSession = 'Complete'
    WHERE SessionId = @SessionId
  END
GO

