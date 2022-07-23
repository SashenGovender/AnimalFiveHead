CREATE TABLE [dbo].[tb_GameSessionState]
(
  [GameStateId] INT NOT NULL,
  [GameStateName] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_tb_GameSessionState] PRIMARY KEY ([GameStateId])

)
