CREATE TABLE Schedules
(
	ScheduleId       INT IDENTITY(1,1)      NOT NULL,
	FoodTruckId      INT                    NOT NULL,
	LocationId       INT                    NOT NULL,
	StartTime        DATETIME               NOT NULL,
	EndTime          DATETIME               NOT NULL,
	RowValidFrom     DATETIME2(3) GENERATED ALWAYS AS ROW START,
    RowValidTo       DATETIME2(3) GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (RowValidFrom, RowValidTo),
    CONSTRAINT PK_Schedules
	    PRIMARY KEY (ScheduleId),
	CONSTRAINT FK_Schedules_FoodTruckId
	    FOREIGN KEY (FoodTruckId) REFERENCES FoodTrucks (FoodTruckId),
	CONSTRAINT FK_Schedules_LocationId
	    FOREIGN KEY (LocationId) REFERENCES Locations (LocationId)
)
WITH (SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = dbo.SchedulesHistory));
