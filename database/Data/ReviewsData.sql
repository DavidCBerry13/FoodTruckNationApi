
SET IDENTITY_INSERT Reviews ON

MERGE INTO Reviews AS Target
USING (VALUES
    (1,  1, '2017-03-01', 5.0, 'Really really good.  Mushroom Swiss burger was one of the best I have ever had'  ),
    (2,  1, '2017-03-03', 4.0, 'Had the bacon cheeseburger and it was really tasty'  ),
    (3,  1, '2017-03-05', 4.0, 'My burger was good and kids loved the hot dogs'  ),
    (4,  1, '2017-03-08', 3.0, 'So so.  You can get a better burder at Five Guys'  ),
    (5,  1, '2017-03-10', 4.0, 'Enjoyed very much.  Wish they came by my workplace more often.  Way better than the cafeteria burgers'  ),
    (6,  1, '2017-03-11', 5.0, 'Yummy'  ),
    (7,  1, '2017-03-14', 1.0, 'Did not like at all.  Burger was burned on the outside and raw on the inside'  ),


    (10,  2, '2017-03-02', 5.0, 'Loooove Grilled Cheese.  The very best food truck around'  ),
    (11,  2, '2017-03-05', 5.0, 'Makes me feel like a kid again, except grilled cheese was never this good as a kid'  ),
    (12,  2, '2017-03-07', 5.0, 'Grilled cheese so good there should be a law requiring everyone to eat here once a day'  ),
    (13,  2, '2017-03-11', 4.0, 'Had a panini.  Was good, but should have had the grilled cheese.  They looked way better'  )

)
AS Source (ReviewId, FoodTruckId, ReviewDate, Rating, Details)
ON Target.ReviewId = Source.ReviewId
    WHEN MATCHED THEN
        UPDATE
		    SET
			FoodTruckId = Source.FoodTruckId,
			ReviewDate = Source.ReviewDate,
			Rating = Source.Rating,
			Details = Source.Details
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (ReviewId, FoodTruckId, ReviewDate, Rating, Details)
        VALUES (ReviewId, FoodTruckId, ReviewDate, Rating, Details);


DECLARE @nextReviewId INT;
SELECT @nextReviewId = (
        SELECT max(ReviewId)
            FROM Reviews
	);


DBCC CHECKIDENT (Reviews, RESEED, @nextReviewId)

SET IDENTITY_INSERT Reviews OFF


