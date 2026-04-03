DROP VIEW IF EXISTS vTotalMiles
GO

CREATE VIEW vTotalMiles
AS
    
  SELECT re.RunnerId,
         SUM(re.Miles) AS 'Miles',
         r.LastName,
         r.FirstName,
         r.Gender
    FROM RaceEntry AS re
    JOIN Runner AS r ON r.Id = re.RunnerId
    JOIN Race AS race ON race.Id = re.RaceId
   WHERE DATEPART(year, race.Date) = (
                                      SELECT Value
                                        FROM Parameters
                                       WHERE Name = 'CurrentYear'
                                     )
   GROUP BY re.RunnerId, r.LastName, r.FirstName, r.Gender
GO

DROP VIEW IF EXISTS vTotalMiles2025
GO

CREATE VIEW vTotalMiles2025
AS
  SELECT re.RunnerId,
       SUM(re.Miles) AS 'Miles',
       r.LastName,
       r.FirstName,
       r.Gender
  FROM RaceEntry AS re
  JOIN Runner AS r ON r.Id = re.RunnerId
  JOIN Race AS race ON race.Id = re.RaceId
 WHERE DATEPART(year, race.Date) = 2025
 GROUP BY re.RunnerId, r.LastName, r.FirstName, r.Gender
GO