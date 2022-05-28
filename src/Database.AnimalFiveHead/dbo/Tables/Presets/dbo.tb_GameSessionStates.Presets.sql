
EXEC dbo.pr_UpsertGameSessionState
  @GameStateId = 1,
  @GameStateName = 'Active'

EXEC dbo.pr_UpsertGameSessionState
  @GameStateId = 2,
  @GameStateName = 'Complete'
