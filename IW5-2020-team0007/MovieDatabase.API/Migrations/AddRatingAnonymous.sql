﻿ALTER TABLE [Ratings]
	ADD [Anonymous] BIT NOT NULL;

ALTER TABLE [Ratings]
	ALTER COLUMN [UserID] BIGINT NOT NULL;