--https://stackoverflow.com/questions/1379437/checking-if-a-sql-server-login-already-exists

IF NOT EXISTS(SELECT * FROM SYS.SYSLOGINS WHERE name='NoNameUser')
  BEGIN
    CREATE LOGIN [NoNameUser] WITH PASSWORD = 'ThisIsALongPassword&50'
    PRINT 'NoNameUser created'
  END



--https://stackoverflow.com/questions/1601186/sql-server-script-to-create-a-new-user

IF EXISTS(SELECT * FROM SYS.SYSLOGINS WHERE name='NoNameUser')
  BEGIN
    IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'NoNameUser')
      BEGIN
        CREATE USER [NoNameUser] FOR LOGIN [NoNameUser]
        PRINT 'NoNameUser created'
      END
  END;

--GRANT CONNECT TO [NoNameUser]

 EXEC sp_addrolemember @rolename = N'db_owner',  @membername = N'NoNameUser'

GO
