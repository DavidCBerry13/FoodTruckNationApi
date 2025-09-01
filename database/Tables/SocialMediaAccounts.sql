CREATE TABLE SocialMediaAccounts
(
    SocialMediaAccountId     INT IDENTITY NOT NULL,
	FoodTruckId              INT          NOT NULL,
	PlatformId               INT          NOT NULL,
	AccountName              VARCHAR(40)  NOT NULL,
	RowValidFrom             DATETIME2(3) GENERATED ALWAYS AS ROW START,
    RowValidTo               DATETIME2(3) GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (RowValidFrom, RowValidTo),
	CONSTRAINT PK_SocialMediaAccounts
	    PRIMARY KEY (SocialMediaAccountId),
	CONSTRAINT FK_SocialMediaAccounts_FoodTruckId
        FOREIGN KEY (FoodTruckId) REFERENCES FoodTrucks (FoodTruckId),
	CONSTRAINT FK_SocialMediaAccounts_PlatformId
	    FOREIGN KEY (PlatformId) REFERENCES SocialMediaPlatforms (PlatformId)
)
WITH (SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = dbo.SocialMediaAccountsHistory));
GO

/* This index means a food truck can only have one social media account per platform */
CREATE UNIQUE INDEX UX_SocialMediaAccounts_FoodTruckId_PlatformId
    ON SocialMediaAccounts (FoodTruckId, PlatformId);