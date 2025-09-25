
SET IDENTITY_INSERT Locations ON

MERGE INTO Locations AS Target
USING (VALUES
	(10,  'Lyric Opera House (Chicago Loop)'  , 'CHI',   '20 N Wacker Drive'   , 'Chicago'   , 'IL', '60606', 41.88250, -87.63721),
	(11,  'Millennium Park'                   , 'CHI',   '201 E Randolph St'  , 'Chicago'   , 'IL', '60601', 41.88416, -87.62235),
	(12,  'Magnificent Mile'                  , 'CHI',   '701 N Michigan Ave'  , 'Chicago'   , 'IL', '60611', 41.89512, -87.62376),
	(13,  'West Loop (Randolph Street)'       , 'CHI',   '1040 W Randolph St'  , 'Chicago'   , 'IL', '60607', 41.88453, -87.65358),
	(14,  'Lincoln Square'                    , 'CHI',   '4836 Lincoln Ave'    , 'Chicago'   , 'IL', '60625', 41.97015, -87.68922),
	(15,  'Wicker Park'                       , 'CHI',   '1425 N Damen Ave'    , 'Chicago'   , 'IL', '60622', 41.90801, -87.67672),
	(16,  'UIC Student Center'                , 'CHI',   '750 S Halsted'       , 'Chicago'   , 'IL', '60607', 41.87204, -87.64793),
	(17,  'Downtown Evanston'                 , 'CHI',   '1616 Sherman Ave'   , 'Evanston'  , 'IL', '60201', 42.04774, -87.68223),
	(18,  'Downtown Park Ridge'               , 'CHI',   '186 Euclid Ave'      , 'Park Ridge', 'IL', '60068', 42.00973, -87.82895),
	(30,  'US Bank Center'                    , 'MKE',   '777 E Wisconsin Ave' , 'Milwaukee' , 'WI', '54301', 43.03850, -87.90189),
	(31,  'Marquette University Student Union', 'MKE',   '1442 W Wisconsin Ave', 'Milwaukee' , 'WI', '53233', 43.03967, -87.93097),
    (51,  'Downtown Appleton (Houdini Plaza)' , 'NEWI',  '111 W College Ave'    , 'Appleton'  , 'WI', '54911', 44.26153, -88.40649),
    (52,  'Downtown Neenah'                   , 'NEWI',  '111 E Wisconsin Ave'  , 'Neenah'    , 'WI', '54956', 44.18601, -88.46110),
    (53,  'UW Oshkosh Student Center'         , 'NEWI',  '748 Algoma Blvd'      , 'Oshkosh'   , 'WI', '54901', 44.02526, -88.54914),
    (54,  'Downtown Oshkosh'                  , 'NEWI',  '300 N Main St'        , 'Oshkosh'   , 'WI', '54901', 44.01745, -88.53717),
    (55,  'Pierce Park Appleton'              , 'NEWI',  '1031 W Prospect Ave'  , 'Appleton'  , 'WI', '54914', 44.25478, -88.42208),
    (56,  'Riverside Park (Neenah)'           , 'NEWI',  '165 N Park Ave'       , 'Neenah'    , 'WI', '54956', 44.18200, -88.44565),
    (57,  'UW Green Bay'                      , 'NEWI',  '2420 Nicolet Dr'      , 'Green Bay' , 'WI', '54311', 44.53126, -87.92082),
    (58,  'NWTC Green Bay'                    , 'NEWI',  '2740 W Mason St'      , 'Green Bay' , 'WI', '54307', 44.52737, -88.10720),
	(59,  'Downtown Green Bay'                , 'NEWI',  '235 N Jefferson St'   , 'Green Bay' , 'WI', '54301', 44.51496, -88.01258)
)
AS Source (LocationId, LocationName, LocalityCode, StreetAddress, City, State, ZipCode, Latitude, Longitude)
ON Target.LocationId = Source.LocationId
    WHEN MATCHED THEN
        UPDATE
		    SET
			LocationName = Source.LocationName,
			LocalityCode = Source.LocalityCode,
			StreetAddress = Source.StreetAddress,
		    City = Source.City,
			State = Source.State,
			ZipCode = Source.ZipCode,
			Latitude = Source.Latitude,
			Longitude = Source.Longitude
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (LocationId, LocationName, LocalityCode, StreetAddress, City, State, ZipCode, Latitude, Longitude)
        VALUES (LocationId, LocationName, LocalityCode, StreetAddress, City, State, ZipCode, Latitude, Longitude);


DECLARE @nextLocationId INT;
SELECT @nextLocationId = (
        SELECT max(LocationId)
            FROM Locations
	) ;


DBCC CHECKIDENT (Locations, RESEED, @nextLocationId)

SET IDENTITY_INSERT Locations OFF


