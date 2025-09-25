MERGE INTO Localities AS Target
USING (VALUES
    ('CHI'  , 'Chicago'),
    ('RCKFD', 'Rockford'),
    ('MKE'  , 'Milwaukee'),
    ('MAD'  , 'Madison'),
    ('NEWI' , 'Northeast Wisconsin')
)
AS Source (LocalityCode, LocalityName)
ON Target.LocalityCode = Source.LocalityCode
    WHEN MATCHED THEN
        UPDATE
		    SET
			LocalityName = Source.LocalityName
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (LocalityCode, LocalityName)
        VALUES (LocalityCode, LocalityName);
