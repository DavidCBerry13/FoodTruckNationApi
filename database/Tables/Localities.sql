CREATE TABLE [dbo].[Localities]
(
	LocalityCode  VARCHAR(5)      NOT NULL,
	LocalityName  VARCHAR(30)     NOT NULL,
	CONSTRAINT PK_Localities
	    PRIMARY KEY (LocalityCode)
)
