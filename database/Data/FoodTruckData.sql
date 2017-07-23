
SET IDENTITY_INSERT FoodTrucks ON

MERGE INTO FoodTrucks AS Target
USING (VALUES
    (1,  'American Burger',             'Classic American Burgers, Hot Dogs and Fries',                            'http://foodtrucknation/BurgerTruck'),
    (2,  'Grilled Cheese Please!',      'Six kinds of grilled cheese, Paninis and more',                           'http://foodtrucknation/GrilledSamdwichTruck'),
    (3,  'Taste of the Windy City',     'Classic Chicago Dogs and Italian Beef',                                   'http://foodtrucknation/TasteOfWindyCity'),
    (4,  'Rice Bowl',                   'Asian favorites served in a bowl of rice',                                'http://foodtrucknation/RiceBowl'),
    (5,  'Empire State Bagels',         'Bagels, Breakfast sandwiches at breakfast and bagel sandwiches at lunch', 'http://foodtrucknation/BagelTruck'),
    (6,  'Dr. Taco',                    'Steak, chicken, pork (carnitas), fish and shrimp tacos.',                 'http://foodtrucknation/TacoTruck'),
    (7,  'Little Havana',               'Cuban sandwiches, Platanos Maduro and other Cuban favorites',                         'http://foodtrucknation/LittleHavana'),
    (8,  'Bento Box',                   'Japanese favorites like teriyaki chicken, yaki udon and of course sushi', 'http://foodtrucknation/BentoBox')
)
AS Source (FoodTruckId, TruckName, Description, Website)
ON Target.FoodTruckId = Source.FoodTruckId
    WHEN MATCHED THEN
        UPDATE
		    SET
			TruckName = Source.TruckName,
			Description = Source.Description,
			Website = Source.Website
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (FoodTruckId, TruckName, Description, Website)
        VALUES (FoodTruckId, TruckName, Description, Website);


DECLARE @nextFoodTruckId INT;
SELECT @nextFoodTruckId = (
        SELECT max(FoodTruckId)
            FROM FoodTrucks
	) ;


DBCC CHECKIDENT (FoodTrucks, RESEED, @nextFoodTruckId)

SET IDENTITY_INSERT FoodTrucks OFF


