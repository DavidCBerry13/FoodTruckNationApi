
SET IDENTITY_INSERT FoodTruckTags ON

MERGE INTO FoodTruckTags AS Target
USING (VALUES
    (1,  1, 1  ),
    (2,  1, 15 ),
	(3,  2, 8  ),
    (4,  2, 9  ),
	(5,  2, 7  ),
    (6,  3, 15 ),
    (7,  3, 16 ),
	(8,  4, 5  ),
	(9,  4, 6  ),
	(10, 5, 7  ),
	(11, 5, 3  ),
    (12, 6, 12 ),
    (13, 6, 13 ),
    (14, 8, 14 ),
    (15, 8, 4  )
)
AS Source (FoodTruckTagId, FoodTruckId, TagId)
ON Target.FoodTruckTagId = Source.FoodTruckTagId
    WHEN MATCHED THEN
        UPDATE
		    SET
			FoodTruckId = Source.FoodTruckId,
			TagId = Source.TagId
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (FoodTruckTagId, FoodTruckId, TagId)
        VALUES (FoodTruckTagId, FoodTruckId, TagId);


DECLARE @nextFoodTruckTagId INT;
SELECT @nextFoodTruckTagId = (
        SELECT max(FoodTruckTagId)
            FROM FoodTruckTags
	);


DBCC CHECKIDENT (FoodTruckTags, RESEED, @nextFoodTruckTagId)

SET IDENTITY_INSERT FoodTruckTags OFF


