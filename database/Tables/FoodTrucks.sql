CREATE TABLE dbo.FoodTrucks
(
	FoodTruckId      INT IDENTITY(1,1)    NOT NULL,
	TruckName        VARCHAR(50)          NOT NULL,
	LocalityCode     VARCHAR(5)           NOT NULL,
	Description      VARCHAR(512)         NOT NULL,
	Website          VARCHAR(100)         NULL,
	RowValidFrom     DATETIME2(3) GENERATED ALWAYS AS ROW START,
    RowValidTo       DATETIME2(3) GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (RowValidFrom, RowValidTo),
	CONSTRAINT PK_FoodTrucks
	    PRIMARY KEY (FoodTruckId),
	CONSTRAINT FK_FoodTrucks_LocalityCode
	    FOREIGN KEY (LocalityCode) REFERENCES Localities (LocalityCode)
)
WITH (SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = dbo.FoodTrucksHistory));


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Auto-generated, unique id number assigned to the food truck',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FoodTrucks',
    @level2type = N'COLUMN',
    @level2name = N'FoodTruckId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The name of the food truck',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FoodTrucks',
    @level2type = N'COLUMN',
    @level2name = N'TruckName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The locality (region) this food truck operates in',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FoodTrucks',
    @level2type = N'COLUMN',
    @level2name = N'LocalityCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'A description of the type of cuisine this food truck serves',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FoodTrucks',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The website of this food truck',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'FoodTrucks',
    @level2type = N'COLUMN',
    @level2name = N'Website'