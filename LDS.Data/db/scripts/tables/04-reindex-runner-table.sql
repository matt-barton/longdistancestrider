
DROP INDEX IF EXISTS idx_RunnerName ON [Runner]
GO

CREATE UNIQUE INDEX idx_RunnerNameGender ON Runner (LastName, FirstName, Gender);
GO