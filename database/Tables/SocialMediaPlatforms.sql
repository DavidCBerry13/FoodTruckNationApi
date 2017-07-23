CREATE TABLE SocialMediaPlatforms
(
    PlatformId      INT           NOT NULL,
	PlatformName    VARCHAR(30)   NOT NULL,
	UrlTemplate     VARCHAR(100)  NOT NULL,
	CONSTRAINT PK_SocialMediaPlatforms
	    PRIMARY KEY (PlatformId)
);