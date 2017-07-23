
SET IDENTITY_INSERT Locations ON

MERGE INTO Locations AS Target
USING (VALUES
    (1,  'Downtown Appleton (Houdini Plaza)',     '111 W College Ave',     'Appleton',  'WI', '54911'),
    (2,  'Downtown Neenah',                       '111 E Wisconsin Ave',   'Neenah',    'WI', '54956'),
    (3,  'UW Oshkosh Student Center',             '748 Algoma Blvd',       'Oshkosh',   'WI', '54901'),
    (4,  'Downtown Oshkosh',                      '300 N Main St',         'Oshkosh',   'WI', '54901'),
    (5,  'Pierce Park Appleton',                  '1031 W Prospect Ave',   'Appleton',  'WI', '54914'),
    (6,  'Riverside Park (Neenah)',               '165 N Park Ave',        'Neenah',    'WI', '54956'),
    (7,  'UW Green Bay',                          '2420 Nicolet Dr',       'Green Bay', 'WI', '54311'),
    (8,  'NWTC Green Bay',                        '2740 W Mason St',       'Green Bay', 'WI', '54307'),
	(9,  'Downtown Green Bay',                    '235 N Jefferson St',    'Green Bay', 'WI', '54301')
)
AS Source (LocationId, LocationName, StreetAddress, City, State, ZipCode)
ON Target.LocationId = Source.LocationId
    WHEN MATCHED THEN
        UPDATE
		    SET
			LocationName = Source.LocationName,
			StreetAddress = Source.StreetAddress,
		    City = Source.City,
			State = Source.State,
			ZipCode = Source.ZipCode
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (LocationId, LocationName, StreetAddress, City, State, ZipCode)
        VALUES (LocationId, LocationName, StreetAddress, City, State, ZipCode);


DECLARE @nextLocationId INT;
SELECT @nextLocationId = (
        SELECT max(LocationId)
            FROM Locations
	) ;


DBCC CHECKIDENT (Locations, RESEED, @nextLocationId)

SET IDENTITY_INSERT Locations OFF


