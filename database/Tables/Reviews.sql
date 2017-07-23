CREATE TABLE [dbo].[Reviews]
(
	ReviewId         INT IDENTITY(1, 1)    NOT NULL,
	FoodTruckId      INT                   NOT NULL,
	ReviewDate       DATE                  NOT NULL,
	Rating           INT                   NOT NULL,
	Details          VARCHAR(1024)         NULL,
	RowValidFrom     DATETIME2(3) GENERATED ALWAYS AS ROW START,
    RowValidTo       DATETIME2(3) GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (RowValidFrom, RowValidTo),
	CONSTRAINT PK_Reviews
	    PRIMARY KEY (ReviewId),
	CONSTRAINT FK_Reviews_FoodTruckId
	    FOREIGN KEY (FoodTruckId) REFERENCES FoodTrucks (FoodTruckId)
)
WITH (SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = dbo.ReviewsHistory));
