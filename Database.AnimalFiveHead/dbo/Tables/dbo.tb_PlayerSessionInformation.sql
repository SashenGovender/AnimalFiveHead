CREATE TABLE [dbo].[tb_PlayerSessionInformation]
(
    [SessionId] UNIQUEIDENTIFIER NOT NULL, 
    [PlayerId] TINYINT NOT NULL, 
    [Score] SMALLINT NOT NULL, 
    [Cards] NVARCHAR(MAX) NOT NULL, 
    [CardIds] NVARCHAR(MAX) NOT NULL, 
    [DateTimeAdded] DATETIME2 NULL, 
    [DateTimeUpdated] DATETIME2 NULL, 
    [GameSession] NCHAR(10) NULL, 
    CONSTRAINT [PK_PlayerSessionInformation] PRIMARY KEY ([SessionId], [PlayerId]) 
)
