/*
Post-Deployment Script Template              
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.    
 Use SQLCMD syntax to include a file in the post-deployment script.      
 Example:      :r .\myfile.sql                
 Use SQLCMD syntax to reference a variable in the post-deployment script.    
 Example:      :setvar TableName MyTable              
               SELECT * FROM [$(TableName)]          
--------------------------------------------------------------------------------------
*/

:r ..\dbo\Tables\Presets\dbo.tb_GameSessionStates.Presets.sql
:r ..\dbo\Tables\Presets\dbo.tb_PlayerTypes.Presets.sql

:r ..\Security\NoNameUser.sql
