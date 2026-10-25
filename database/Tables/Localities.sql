CREATE TABLE [dbo].[Localities]
(
	LocalityCode  VARCHAR(5)      NOT NULL,
	LocalityName  VARCHAR(30)     NOT NULL,
	CONSTRAINT PK_Localities
	    PRIMARY KEY (LocalityCode),
	CONSTRAINT CC_Localities_LocalityCode_MinLength
	    CHECK (len(LocalityCode) >= 3),
	CONSTRAINT CC_Localities_LocalityCode_OnlyLetters
	    CHECK (LocalityCode NOT LIKE '%[^A-Z]%'),
	CONSTRAINT CC_Localities_LocalityCode_Uppercase
	    CHECK (LocalityCode = upper(LocalityCode) COLLATE Latin1_General_CS_AI)
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The unique code for this locality.  This must be a 3-5 character upper case string',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Localities',
    @level2type = N'COLUMN',
    @level2name = N'LocalityCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of this locality',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Localities',
    @level2type = N'COLUMN',
    @level2name = N'LocalityName'