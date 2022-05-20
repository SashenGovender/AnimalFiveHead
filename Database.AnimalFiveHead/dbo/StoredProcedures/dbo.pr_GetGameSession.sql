CREATE PROCEDURE [dbo].[pr_GetGameSession]
  @SessionId uniqueidentifier

AS
  BEGIN
    SELECT  sessionInfo.SessionId AS SessionId,
            sessionInfo.PlayerId AS PlayerId,
            sessionInfo.Score AS Score,
            sessionInfo.Cards AS Cards,
            sessionInfo.CardIds AS CardIds
    FROM dbo.tb_PlayerSessionInformation sessionInfo (NOLOCK)
    WHERE SessionId = @SessionId and GameSession= 'Active'
  END
GO
