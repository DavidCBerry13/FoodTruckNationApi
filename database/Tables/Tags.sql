﻿CREATE TABLE [dbo].[Tags]
(
    TagId     INT IDENTITY(1,1)  NOT NULL,
	TagName   VARCHAR(30)        NOT NULL,
	RowValidFrom     DATETIME2(3) GENERATED ALWAYS AS ROW START,
    RowValidTo       DATETIME2(3) GENERATED ALWAYS AS ROW END,
	PERIOD FOR SYSTEM_TIME (RowValidFrom, RowValidTo),
	CONSTRAINT PK_Tags
	    PRIMARY KEY (TagId)
)
WITH (SYSTEM_VERSIONING = ON
    (HISTORY_TABLE = dbo.TagsHistory));