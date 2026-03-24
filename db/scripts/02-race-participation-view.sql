CREATE VIEW vRaceParticipation
AS
SELECT ra.Id AS RaceId,
       ru.Id AS RunnerId,
       ra.[Name] AS RaceName,
       ra.[Date],
       CONCAT(ru.FirstName, ' ', ru.LastName) AS RunnerName,
       re.Miles
  FROM Race ra,
       Runner ru,
       RaceEntry re
 WHERE ra.Id = re.RaceId
   AND ru.Id = re.RunnerId
