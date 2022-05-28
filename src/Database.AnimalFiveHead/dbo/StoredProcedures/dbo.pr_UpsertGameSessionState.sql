CREATE PROCEDURE [dbo].[pr_UpsertGameSessionState]
  @GameStateId int,
  @GameStateName nvarchar(50)

AS
  BEGIN
    IF NOT EXISTS ( SELECT GameStateId FROM dbo.tb_GameSessionState (NOLOCK) WHERE GameStateId = @GameStateId )
      BEGIN
        INSERT INTO dbo.tb_GameSessionState (GameStateId , GameStateName )
        VALUES (@GameStateId, @GameStateName )
      END
    ELSE
      BEGIN
        UPDATE dbo.tb_GameSessionState
        SET GameStateName = @GameStateName
        WHERE GameStateId = @GameStateId
      END
  END
GO
