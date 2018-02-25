MERGE INTO SocialMediaPlatforms AS Target
USING (VALUES
    (1, 'Facebook',  'https://www.facebook.com/{AccountName}', '^[a-z\d.]{5,}$'),
    (2, 'Twitter',   'https://www.twitter.com/{AccountName}', '^[a-zA-Z0-9_]{1,15}$'),
    (3, 'Instagram', 'https://www.instagram.com/{AccountName}', '^([A-Za-z0-9_](?:(?:[A-Za-z0-9_]|(?:\.(?!\.))){0,28}(?:[A-Za-z0-9_]))?)$')
)
AS Source (PlatformId, PlatformName, UrlTemplate, AccountNameRegex)
ON Target.PlatformId = Source.PlatformId
    WHEN MATCHED THEN
        UPDATE
		    SET
			PlatformName = Source.PlatformName,
			UrlTemplate = Source.UrlTemplate
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (PlatformId, PlatformName, UrlTemplate)
        VALUES (PlatformId, PlatformName, UrlTemplate);





