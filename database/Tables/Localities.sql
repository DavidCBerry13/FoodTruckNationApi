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
