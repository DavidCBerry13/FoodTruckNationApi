
SET IDENTITY_INSERT FoodTruckTags ON

MERGE INTO FoodTruckTags AS Target
USING (VALUES
    (1,  1, 14 ),
    (2,  1, 18 ),
	(3,  2, 20 ),
    (4,  2, 21 ),
	(5,  3, 1  ),
    (6,  3, 16 ),
    (7,  4, 1  ),
	(8,  4, 5  ),
	(9,  5, 1  ),
	(10, 5, 22 ),
	(11, 6, 1  ),
    (12, 6, 16 ),
    (13, 7, 2  ),
    (14, 7, 7  ),
    (15, 8, 1  ),
    (16, 8, 3  ),
    (17, 9, 1  ),
    (18, 9, 6  ),
    (19, 10, 1 ),
    (20, 10, 17),
    (21, 11, 11),
    (22, 11, 15),
    (23, 12, 2 ),
    (24, 12, 19),
    (25, 13, 1 ),
    (26, 13, 5 ),
    (27, 14, 1 ),
    (28, 14, 16),
    (29, 15, 1 ),
    (30, 15, 16),
    (31, 16, 2 ),
    (32, 16, 7 ),
    (33, 17, 4 ),
    (34, 17, 16),
    (35,  18, 14  ),
    (36,  18, 18  ),
    (37,  19, 8   ),
    (38,  19, 16),
    (39,  20, 2   ),
    (40,  20, 12  ),
    (41,  21, 14  ),
    (42,  21, 18  ),
    (43,  22, 10  ),
    (44,  22, 17  ),
    (45,  23, 1   ),
    (46,  23, 5   ),
    (47,  23, 6   ),
    (48,  24, 1   ),
    (49,  24, 6   ),
    (50,  25, 11  ),
    (51,  25, 15  ),
    (52,  26, 9   ),
    (53,  27, 2   ),
    (54,  27, 12  ),
    (55,  28, 2   ),
    (56,  28, 12  ),
    (57,  29, 2   ),
    (58,  29, 13  ),
    (59,  30, 1   ),
	(60,  30, 5   )
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


