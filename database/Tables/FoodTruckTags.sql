CREATE TABLE [dbo].[FoodTruckTags]
(
	FoodTruckTagId     INT IDENTITY(1,1)  NOT NULL,
	FoodTruckId        INT                NOT NULL,
	TagId              INT                NOT NULL,
	RowValidFrom       DATETIME2(3) GENERATED ALWAYS AS ROW START,
    RowValidTo         DATETIME2(3) GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (RowValidFrom, RowValidTo),
	CONSTRAINT PK_FoodTruckTags
	    PRIMARY KEY (FoodTruckTagId),
	CONSTRAINT FK_FoodTruckTags_FoodTruckTagId
	    FOREIGN KEY (FoodTruckId) REFERENCES FoodTrucks (FoodTruckId),
	CONSTRAINT FK_FoodTruckTags_TagId
	    FOREIGN KEY (TagId) REFERENCES Tags (TagId)
)
WITH (SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = dbo.FoodTruckTagsHistory));
