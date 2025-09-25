
SET IDENTITY_INSERT FoodTrucks ON

MERGE INTO FoodTrucks AS Target
USING (VALUES
    (1,  'Taco ''Bout Tasty'            , 'CHI', 'Authentic Mexican flavors, one taco at a time.'                          , 'http://foodtrucknation/TacoBoutTasty'),
    (2,  'Crepe Crusaders'              , 'CHI', 'A little bit of Paris in your day'                                       , 'http://foodtrucknation/CrepeCrusaders'),
    (3,  'Wrap Stars'                   , 'CHI', 'Breaking down your hunger, in a wrap!'                                   , 'http://foodtrucknation/WrapStars'),
    (4,  'Burger Bliss'                 , 'CHI', 'Hand-crafted burgers with fresh, locally sourced ingredients.'           , 'http://foodtrucknation/BurgerBliss'),
    (5,  'Vegan Vibes'                  , 'CHI', 'Hand-crafted, healthy dishes with fresh, authentic ingredients'          , 'http://foodtrucknation/VeganVibes'),
    (6,  'The Grilled Cheese Guru'      , 'CHI', 'Grown up grilled cheese for the connoisseur in you'                      , 'http://foodtrucknation/GriledCheeseGuru'),
    (7,  'The Wandering Wok'            , 'CHI', 'Wok this way for deliciousness'                                          , 'http://foodtrucknation/TheWanderingWok'),
    (8,  'BBQ Boss'                     , 'CHI', 'Where the smoke meets the street'                                        , 'http://foodtrucknation/BBQBoss'),
    (9,  'Fry Daddy''s'                 , 'CHI', 'Deep fried chicken and shrimp down southern style'                       , 'http://foodtrucknation/FryDaddys'),
    (10, 'Seafood Shack'                , 'CHI', 'A taste of the ocean, one delicious bite at a time'                      , 'http://foodtrucknation/SeafoodShack'),
    (11,  'Three Brothers Pizza'        , 'CHI', 'Deep dish and thin crust pizza by the slice'                             , 'http://foodtrucknation/ThreeBrothersPizza'),
    (12,  'Yummy Yummy Thai'            , 'CHI', 'Thai favorites including Pad Thai, Pad See Ew, and Basil Fried Rice'     , 'http://foodtrucknation/YummyYummyThai'),
    (13,  'Milwaukee Burger Company'    , 'MKE', 'Classic burgers served on a pretzel bun'                                 , 'http://foodtrucknation/MkeBurgerTruck'),
    (14,  'Grilled Cheese Please!'      , 'MKE', 'Six kinds of grilled cheese, Paninis and more'                           , 'http://foodtrucknation/GrilledSamdwichTruck'),
    (15,  'Taste of the Windy City'     , 'MKE', 'Classic Chicago Dogs and Italian Beef'                                   , 'http://foodtrucknation/TasteOfWindyCity'),
    (16,  'Rice Bowl'                   , 'MKE', 'Asian favorites served in a bowl of rice'                                , 'http://foodtrucknation/RiceBowl'),
    (17,  'Great Lakes Bagels Company'  , 'MKE', 'Bagels, Breakfast sandwiches at breakfast and bagel sandwiches at lunch' , 'http://foodtrucknation/BagelTruck'),
    (18,  'Dr. Taco'                    , 'MAD', 'Steak, chicken, pork (carnitas), fish and shrimp tacos.'                 , 'http://foodtrucknation/TacoTruck'),
    (19,  'Little Havana'               , 'CHI', 'Cuban sandwiches, Platanos Maduro and other Cuban favorites'             , 'http://foodtrucknation/LittleHavana'),
    (20,  'Bento Box'                   , 'CHI', 'Japanese favorites like teriyaki chicken, yaki udon and of course sushi' , 'http://foodtrucknation/BentoBox'),
    (21,  'Tacos in Motion'             , 'NEWI', 'The best tacos in town, on wheels.'                                      , 'http://foodtrucknation/TacosInMotion'),
    (22,  'Poke Paradise'               , 'CHI', 'Classic Hawaiian poke bowls served ahi tuna and our special soy ginger marinade' , 'http://foodtrucknation/PokeParadise'),
    (23,  'Hometown Grill'              , 'NEWI', 'Classic burgers, chicken, and wings'                                     , 'http://foodtrucknation/HometownGrill'),
    (24,  'Chicken Coop'                , 'NEWI', 'Chicken sandwiches done better'                                          , 'http://foodtrucknation/ChickenCoop'),
    (25,  'The Wood Fired Pizza Company', 'MKE', 'Traditional wood fired pizzas with a thin crispy crust and smokey flavor', 'http://foodtrucknation/WoodFiredPizzaCompany'),
    (26,  'Sweet Tooth Symphony'        , 'CHI', 'Cookies, cakes, and other deserts to satisfy your sweet tooth'           , 'http://foodtrucknation/ChickenCoop'),
    (27,  'Ramen to the Rescue'         , 'MKE', 'Authentic Japanese flavors, one bowl at a time'                          , 'http://foodtrucknation/RamenToTheRescue'),
    (28,  'Symphony of Sushi'           , 'MAD', 'Authentic Japanese flavors, rolled fresh for you.'                       , 'http://foodtrucknation/SymphonyOfSushi'),
    (29,  'Heart and Seoul'             , 'CHI', 'The perfect blend of tradition and innovation.'                          , 'http://foodtrucknation/HeartAndSeoul'),
    (30,  'Blue Ribbon Burgers'         , 'MAD', 'Gourmet Wagyu beef burgers served on a brioche bun'                      , 'http://foodtrucknation/BlueRibbonBurgers')
)
AS Source (FoodTruckId, TruckName, LocalityCode, Description, Website)
ON Target.FoodTruckId = Source.FoodTruckId
    WHEN MATCHED THEN
        UPDATE
		    SET
			TruckName = Source.TruckName,
			Description = Source.Description,
			Website = Source.Website,
            LocalityCode = Source.LocalityCode
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (FoodTruckId, TruckName, Description, Website, LocalityCode)
        VALUES (FoodTruckId, TruckName, Description, Website, LocalityCode);


DECLARE @nextFoodTruckId INT;
SELECT @nextFoodTruckId = (
        SELECT max(FoodTruckId)
            FROM FoodTrucks
	) ;


DBCC CHECKIDENT (FoodTrucks, RESEED, @nextFoodTruckId)

SET IDENTITY_INSERT FoodTrucks OFF
