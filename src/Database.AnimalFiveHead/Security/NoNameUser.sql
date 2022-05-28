--CREATE USER [NoNameUser]
--  FOR LOGIN NoNameLogin

--GO

--GRANT CONNECT TO [NoNameUser]


--https://stackoverflow.com/questions/1601186/sql-server-script-to-create-a-new-user

IF EXISTS(SELECT * FROM SYS.SYSLOGINS WHERE name='NoNameLogin')
  BEGIN
    IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'NoNameUser')
      BEGIN
        CREATE USER [NoNameUser] FOR LOGIN [NoNameLogin]
        PRINT 'NoNameUser created'
      END
  END;

GRANT CONNECT TO [NoNameUser]

 EXEC sp_addrolemember @rolename = N'db_owner',  @loginame = N'NoNameLogin'

GO
