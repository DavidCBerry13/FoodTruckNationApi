
SET IDENTITY_INSERT Tags ON

MERGE INTO Tags AS Target
USING (VALUES
    (1,  'Burgers' ),
    (2,  'Pizza' ),
	(3,  'Breakfast' ),
    (4,  'Japanese Food' ),
	(5,  'Asian' ),
    (6,  'Chinese Food' ),
    (7,  'Sandwiches' ),
	(8,  'Grilled Cheese' ),
	(9,  'Paninis' ),
	(10, 'Thai Food' ),
	(11, 'Indian Food' ),
    (12, 'Tacos' ),
    (13, 'Mexican Food' ),
    (14, 'Sushi' ),
    (15, 'Hot Dogs' ),
	(16, 'Italian Beef')
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


