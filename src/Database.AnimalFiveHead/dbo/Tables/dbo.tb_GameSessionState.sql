CREATE TABLE [dbo].[tb_GameSessionState]
(
  [GameStateId] NCHAR(10) NOT NULL,
  [GameStateName] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_tb_GameSessionState] PRIMARY KEY ([GameStateId])

)
