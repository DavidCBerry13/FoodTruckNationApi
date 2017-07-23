MERGE INTO SocialMediaPlatforms AS Target
USING (VALUES
    (1, 'Facebook',  'https://www.facebook.com/{AccountName}'),
    (2, 'Twitter',   'https://www.twitter.com/{AccountName}'),
    (3, 'Instagram', 'https://www.instagram.com/{AccountName}')
)
AS Source (PlatformId, PlatformName, UrlTemplate)
ON Target.PlatformId = Source.PlatformId
    WHEN MATCHED THEN
        UPDATE
		    SET
			PlatformName = Source.PlatformName,
			UrlTemplate = Source.UrlTemplate
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (PlatformId, PlatformName, UrlTemplate)
        VALUES (PlatformId, PlatformName, UrlTemplate);

