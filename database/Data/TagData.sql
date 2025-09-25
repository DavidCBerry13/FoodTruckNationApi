
SET IDENTITY_INSERT Tags ON

MERGE INTO Tags AS Target
USING (VALUES
    (1,  'American'),
    (2,  'Asian'),
	(3,  'BBQ'),
    (4,  'Breakfast'),
	(5,  'Burgers'),
    (6,  'Chicken'),
    (7,  'Chinese'),
	(8,  'Cuban'),
	(9,  'Deserts'),
	(10, 'Hawaiian'),
	(11, 'Italian'),
    (12, 'Japanese'),
    (13, 'Korean'),
    (14, 'Mexican'),
    (15, 'Pizza'),
	(16, 'Sandwiches'),
    (17, 'Seafood'),
    (18, 'Tacos'),
    (19, 'Thai'),
    (20, 'French'),
    (21, 'Crepes'),
    (22, 'Vegetarian')
)
AS Source (TagId, TagName)
ON Target.TagId = Source.TagId
    WHEN MATCHED THEN
        UPDATE
		    SET
			TagName = Source.TagName
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (TagId, TagName)
        VALUES (TagId, TagName);


DECLARE @nextTagId INT;
SELECT @nextTagId = (
        SELECT max(TagId)
            FROM Tags
	);


DBCC CHECKIDENT (Tags, RESEED, @nextTagId)

SET IDENTITY_INSERT Tags OFF

