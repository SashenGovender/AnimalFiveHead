--https://stackoverflow.com/questions/1379437/checking-if-a-sql-server-login-already-exists

IF NOT EXISTS(SELECT * FROM SYS.SYSLOGINS WHERE name='NoNameLogin')
  BEGIN
    CREATE LOGIN [NoNameLogin] WITH PASSWORD = 'ThisIsALongPassword&50'
    PRINT 'NoNameLogin created'
  END
