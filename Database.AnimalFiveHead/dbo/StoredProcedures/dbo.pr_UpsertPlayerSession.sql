CREATE PROCEDURE [dbo].[pr_UpsertPlayerSession]
  @SessionId uniqueidentifier,
  @PlayerId int,
  @Score int,
  @Cards nvarchar(MAX),
  @CardIds nvarchar(MAX)

AS
  BEGIN
    IF NOT EXISTS ( SELECT SessionId FROM dbo.tb_PlayerSessionInformation (NOLOCK) WHERE SessionId = @SessionId AND PlayerId = @PlayerId)
      BEGIN
        INSERT INTO dbo.tb_PlayerSessionInformation (SessionId,PlayerId,Score,Cards,CardIds,DateTimeAdded, GameSession)
        VALUES(@SessionId,@PlayerId,@Score,@Cards,@CardIds,SYSUTCDATETIME(), 'Active')
      END
    ELSE
      BEGIN
        UPDATE dbo.tb_PlayerSessionInformation
        SET Score=@Score, Cards=@Cards, CardIds=@CardIds, DateTimeUpdated=SYSUTCDATETIME()
        WHERE SessionId=@SessionId AND PlayerId=@PlayerId
      END
  END
GO
