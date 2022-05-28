CREATE PROCEDURE [dbo].[pr_UpsertPlayerType]
  @PlayerId int,
  @PlayerName nvarchar(50)

AS
  BEGIN
    IF NOT EXISTS ( SELECT PlayerId FROM dbo.tb_PlayerType (NOLOCK) WHERE PlayerId = @PlayerId )
      BEGIN
        INSERT INTO dbo.tb_PlayerType (PlayerId, PlayerName)
        VALUES (@PlayerId, @PlayerName)
      END
    ELSE
      BEGIN
        UPDATE dbo.tb_PlayerType
        SET PlayerName = @PlayerName
        WHERE PlayerId = @PlayerId
      END
  END
GO
