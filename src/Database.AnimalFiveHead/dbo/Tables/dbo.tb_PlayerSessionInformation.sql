CREATE TABLE [dbo].[tb_PlayerSessionInformation]
(
    [SessionId] UNIQUEIDENTIFIER NOT NULL, 
    [PlayerId] INT NOT NULL, 
    [Score] INT NOT NULL, 
    [Cards] NVARCHAR(MAX) NOT NULL, 
    [CardIds] NVARCHAR(MAX) NOT NULL, 
    [GameSession] NCHAR(10) NOT NULL,
    [GameResult] NVARCHAR(50) NOT NULL,
    [DateTimeAdded] DATETIME2 NOT NULL, 
    [DateTimeUpdated] DATETIME2 NULL,
    CONSTRAINT [PK_PlayerSessionInformation] PRIMARY KEY ([SessionId], [PlayerId]) 
)
