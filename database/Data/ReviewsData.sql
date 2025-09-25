
SET IDENTITY_INSERT Reviews ON

MERGE INTO Reviews AS Target
USING (VALUES
    (1,  4, GetDate() - 58,  5, 'Really really good.  Mushroom Swiss burger was one of the best I have ever had'  ),
    (2,  4, GetDate() - 53,  4, 'Had the bacon cheeseburger and it was really tasty.  Their fries are good too, nicely seasoned'  ),
    (3,  4, GetDate() - 50,  5, 'Excellent burger!  Went for the classic double cheeseburger and it was perfect.  Its perfectly seasoned, moist, and perfectly done.  The bun is buttered and slightly toasted which adds a whole other element'  ),
    (4,  4, GetDate() - 45,  3, 'I had really high expectations but something was just off. Seemed like the patty was a little dry.  Maybe they had an off day'  ),
    (5,  4, GetDate() - 37,  4, 'Enjoyed very much.  Wish they came by my workplace more often.  Way better than the cafeteria burgers'  ),
    (6,  4, GetDate() - 29,  5, 'Went for a double cheeseburger and wow.  Really good but really big.  '),
    (7,  4, GetDate() - 14,  3, 'Liked the burger but I thought the price was too high'  ),
    (10, 6, GetDate() - 52,  5, 'Loooove Grilled Cheese.  The very best food truck around'  ),
    (11, 6, GetDate() - 34,  5, 'Makes me feel like a kid again, except grilled cheese was never this good as a kid'  ),
    (12, 6, GetDate() - 26,  5, 'Grilled cheese so good there should be a law requiring everyone to eat here once a day'  ),
    (13, 6, GetDate() - 11,  4, 'I went for the gruyere and bacon grilled cheese and it was just perfect'  ),
    (41, 12, GetDate() - 42, 4, 'The Thai Red Curry bowl is excellent.  Just wish the portions were a bit bigger'  ),
    (42, 12, GetDate() - 35, 5, 'Basil Fried Rice with Chickn is my go to'  ),
    (43, 16, GetDate() - 33, 4, 'Like the Sesame chicken, just with the bowls were bigger'  ),
    (44, 16, GetDate() - 24, 3, 'Some interesting bowls, but for what you pay you should get more'  ),
    (45, 16, GetDate() - 19, 5, 'Perfect for lunch.  Always get this when they come by'  )
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


