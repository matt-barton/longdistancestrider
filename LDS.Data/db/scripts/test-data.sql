
insert into Runner (FirstName, LastName, Gender) VALUES ('Matt', 'Barton', 'M')
insert into Runner (FirstName, LastName, Gender) VALUES ('Darren', 'Huckerby', 'M')
insert into Runner (FirstName, LastName, Gender) VALUES ('Teemu', 'Pukki', 'M')

insert into Race (Name, Date) VALUES ('the race', '2026-01-01');
insert into Race (Name, Date) VALUES ('half mara', '2026-01-02');
insert into Race (Name, Date) VALUES ('10k', '2026-01-03');
insert into Race (Name, Date) VALUES ('marathon', '2026-01-04');

insert into RaceEntry (RunnerId, RaceId, Miles) VALUES (1, 1, 10);
insert into RaceEntry (RunnerId, RaceId, Miles) VALUES (1, 4, 6.2);
insert into RaceEntry (RunnerId, RaceId, Miles) VALUES (2, 1, 10);
insert into RaceEntry (RunnerId, RaceId, Miles) VALUES (2, 3, 13.1);
insert into RaceEntry (RunnerId, RaceId, Miles) VALUES (3, 5, 26.2);
