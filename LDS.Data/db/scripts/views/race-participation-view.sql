DROP VIEW IF EXISTS vRaceParticipation
GO

CREATE VIEW vRaceParticipation
AS
SELECT ra.Id AS RaceId,
       ru.Id AS RunnerId,
       ra.[Name] AS RaceName,
       ra.[Date],
       CONCAT(ru.FirstName, ' ', ru.LastName) AS RunnerName,
       ru.LastName AS RunnerLastName,
       re.Miles
FROM Race ra,
     Runner ru,
     RaceEntry re
WHERE ra.Id = re.RaceId
  AND ru.Id = re.RunnerId
GO