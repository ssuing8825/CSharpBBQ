/*
Deployment script for Database
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Database"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
USE [master]
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [CSharpBbqLadder], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', FILEGROWTH = 1024 KB)
    LOG ON (NAME = [CSharpBbqLadder_log], FILENAME = '$(DefaultLogPath)$(DatabaseName)_log.LDF', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %) COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]
GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[EdmMetadata]...';


GO
CREATE TABLE [dbo].[EdmMetadata] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ModelHash] NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[LadderWeeks]...';


GO
CREATE TABLE [dbo].[LadderWeeks] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [WeekNumber] INT      NOT NULL,
    [StertDate]  DATETIME NOT NULL,
    [EndDate]    DATETIME NOT NULL,
    [IsCurrent]  BIT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Matches]...';


GO
CREATE TABLE [dbo].[Matches] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [WinnerId]     INT      NOT NULL,
    [LooserId]     INT      NOT NULL,
    [ChallengerId] INT      NOT NULL,
    [LadderWeekId] INT      NOT NULL,
    [DateOfMatch]  DATETIME NULL,
    [G1W]          SMALLINT NOT NULL,
    [G1L]          SMALLINT NOT NULL,
    [G2W]          SMALLINT NOT NULL,
    [G2L]          SMALLINT NOT NULL,
    [G3W]          SMALLINT NULL,
    [G3L]          SMALLINT NULL,
    [WinnerRank]   SMALLINT NOT NULL,
    [LooserRank]   SMALLINT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Players]...';


GO
CREATE TABLE [dbo].[Players] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (128) NULL,
    [Email] NVARCHAR (128) NULL,
    [Rank]  SMALLINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Standings]...';


GO
CREATE TABLE [dbo].[Standings] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [LadderWeekId] INT NOT NULL,
    [PlayerId]     INT NOT NULL,
    [Position]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating Match_Challenger...';


GO
ALTER TABLE [dbo].[Matches] WITH NOCHECK
    ADD CONSTRAINT [Match_Challenger] FOREIGN KEY ([ChallengerId]) REFERENCES [dbo].[Players] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Match_LadderWeek...';


GO
ALTER TABLE [dbo].[Matches] WITH NOCHECK
    ADD CONSTRAINT [Match_LadderWeek] FOREIGN KEY ([LadderWeekId]) REFERENCES [dbo].[LadderWeeks] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;


GO
PRINT N'Creating Match_Looser...';


GO
ALTER TABLE [dbo].[Matches] WITH NOCHECK
    ADD CONSTRAINT [Match_Looser] FOREIGN KEY ([LooserId]) REFERENCES [dbo].[Players] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;


GO
PRINT N'Creating Match_Winner...';


GO
ALTER TABLE [dbo].[Matches] WITH NOCHECK
    ADD CONSTRAINT [Match_Winner] FOREIGN KEY ([WinnerId]) REFERENCES [dbo].[Players] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Player_Standings...';


GO
ALTER TABLE [dbo].[Standings] WITH NOCHECK
    ADD CONSTRAINT [Player_Standings] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Players] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;


GO
PRINT N'Creating Standing_LadderWeek...';


GO
ALTER TABLE [dbo].[Standings] WITH NOCHECK
    ADD CONSTRAINT [Standing_LadderWeek] FOREIGN KEY ([LadderWeekId]) REFERENCES [dbo].[LadderWeeks] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;


GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
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

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Matches] WITH CHECK CHECK CONSTRAINT [Match_Challenger];

ALTER TABLE [dbo].[Matches] WITH CHECK CHECK CONSTRAINT [Match_LadderWeek];

ALTER TABLE [dbo].[Matches] WITH CHECK CHECK CONSTRAINT [Match_Looser];

ALTER TABLE [dbo].[Matches] WITH CHECK CHECK CONSTRAINT [Match_Winner];

ALTER TABLE [dbo].[Standings] WITH CHECK CHECK CONSTRAINT [Player_Standings];

ALTER TABLE [dbo].[Standings] WITH CHECK CHECK CONSTRAINT [Standing_LadderWeek];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
