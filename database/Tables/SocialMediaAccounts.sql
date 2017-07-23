CREATE TABLE SocialMediaAccounts
(
    SocialMediaAccountId      INT IDENTITY NOT NULL,
	FoodTruckId              INT          NOT NULL,
	PlatformId               INT          NOT NULL,
	AccountName              VARCHAR(40)
	CONSTRAINT PK_SocialMediaAccounts
	    PRIMARY KEY (SocialMediaAccountId)
);