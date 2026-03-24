CREATE DATABASE [lds-test] AS COPY OF [lds-db];
GO;

select * from Runner;
select * from Race;
select * from RaceEntry;
select * from RaceParticipation
 where RaceId = 133


select * from runner order by LastName


select top(1) * from Race order by Id desc

select * from RaceEntry where RaceId in (142,210)

select * from RaceEntry where RunnerId=204 and raceid=262;

update raceentry set miles=1.5 where RunnerId=204 and raceid=262

update raceentry set runnerId=4 where runnerid=2 and raceid=5

select * from runner where firstname LIKE 'dav%' order by lastname asc
select * from runner where lastname like '%mulvey%'
select * from race where Name like '%percy%'

delete from race where id=419

select * from vRaceParticipation where RunnerName like '%taylor%'
order by RunnerName

select * from vRaceParticipation where RaceName like '%silks%'
order by RunnerName

select * from vRaceParticipation where RaceId in (154)

select * from runner order by LastName ASC

select * from runner where id in (212)

insert into RUnner (FirstName, LastName, Gender) VALUES ('Alex', 'Shepherd', 'M')

select * from runner where gender=''
update runner set lastname='Neri' where id=111

update runner set firstname='Michael', lastname='Cambell', gender='M' where id=202

update raceentry set runnerid=32 where runnerid=234

select * from runner where id = 207

select count(*) from RaceEntry where raceid=31

select * from RaceEntry where raceid=133

UPDATE RaceEntry SET Miles = 6.3 WHERE RunnerId = 38 AND RaceId = 20
UPDATE RaceEntry SET Miles = 6.3 WHERE RunnerId = 47 AND RaceId = 20
UPDATE RaceEntry SET Miles = 6.3 WHERE RunnerId = 78 AND RaceId = 20
UPDATE RaceEntry SET Miles = 6.3 WHERE RunnerId = 79 AND RaceId = 20
UPDATE RaceEntry SET Miles = 6.3 WHERE RunnerId = 80 AND RaceId = 20
select * 
from RaceEntry AS re
join Runner AS r
  ON re.RunnerId = r.Id
where re.RaceId = 20
  and r.Gender = 'M'

   insert into Race (Name, Date) VALUES ('half mara', '2026-01-02');
insert into Race (Name, Date) VALUES ('10k', '2026-01-03');
insert into Race (Name, Date) VALUES ('marathon', '2026-01-04');
    
insert into RaceEntry (RunnerId, RaceId, Miles) VALUES (1, 4, 6.2);
insert into RaceEntry (RunnerId, RaceId, Miles) VALUES (2, 3, 13.1);
insert into RaceEntry (RunnerId, RaceId, Miles) VALUES (3, 5, 26.2);

delete from RaceEntry where RaceId=6

select v.*
  from vTotalMiles as v,
       Runner as r
 where v.RunnerId = r.Id
   and r.Gender = 'M'
   --and v.
 order by v.TotalMiles DESC, r.LastName ASC, r.FirstName ASC

select * from vTotalMiles WHERE Gender = 'M'
 order by TotalMiles DESC, LastName ASC, FirstName ASC

select * from fLeaderboard ('2025', 'M') ORDER BY TotalMiles DESC

select * from parameters

delete from RaceEntry;
delete from Runner;
delete from Race;

delete from raceentry where raceid=95


update race set Date='2025-04-27' where id=133

select * from RunnerAlias


update raceentry set miles = miles+1 where raceid=229

select * from RaceEntry where raceid=229

update Parameters set VAlue='2026' where name='CurrentYear'
