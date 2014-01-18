CREATE TABLE CacheEntries
	(
		CacheEntryId INTEGER PRIMARY KEY AUTOINCREMENT,
		Resource TEXT NOT NULL,
		ETag TEXT NOT NULL,
		Value TEXT NOT NULL,
		Unique (Resource)
	);