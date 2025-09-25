CREATE TABLE Locations
(
	LocationId      INT IDENTITY(1,1)      NOT NULL,
	LocalityCode    VARCHAR(5)             NOT NULL,
	LocationName    VARCHAR(50)            NOT NULL,
	StreetAddress   VARCHAR(50)            NOT NULL,
	City            VARCHAR(30)            NOT NULL,
	State           VARCHAR(2)             NOT NULL,
	ZipCode         VARCHAR(5)             NOT NULL,
	Latitude        DECIMAL(8,5)           NOT NULL,
	Longitude       DECIMAL(8,5)           NOT NULL,
	RowValidFrom     DATETIME2(3) GENERATED ALWAYS AS ROW START,
    RowValidTo       DATETIME2(3) GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (RowValidFrom, RowValidTo),
	CONSTRAINT PK_Locations
	    PRIMARY KEY (LocationId),
	CONSTRAINT FK_Locations_LocalityCode
	    FOREIGN KEY (LocalityCode) REFERENCES Localities (LocalityCode)
)
WITH (SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = dbo.LocationsHistory));
