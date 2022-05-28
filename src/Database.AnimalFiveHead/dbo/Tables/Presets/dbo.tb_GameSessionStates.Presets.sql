
EXEC dbo.pr_GetGameSession
  @GameStateId = 1,
  @GameStateName = 'Active'

EXEC dbo.pr_GetGameSession
  @GameStateId = 2,
  @GameStateName = 'Complete'
