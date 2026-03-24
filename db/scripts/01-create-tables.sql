

CREATE TABLE Runner (
    Id INT NOT NULL IDENTITY PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Gender VARCHAR(1) NOT NULL
);
CREATE UNIQUE INDEX idx_RunnerName ON Runner (LastName, FirstName);


CREATE TABLE Race (
    Id INT NOT NULL IDENTITY PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    Date DATE NOT NULL
);
CREATE INDEX idx_Date ON Race (Date);
CREATE UNIQUE INDEX idx_RaceDate ON Race (Name, Date);

CREATE TABLE RaceEntry (
    RunnerId INT NOT NULL,
    RaceId INT NOT NULL,
    Miles DECIMAL (6, 3),

    PRIMARY KEY (RunnerId, RaceId),
    FOREIGN KEY (RunnerId) REFERENCES Runner(Id),
    FOREIGN KEY (RaceId) REFERENCES Race(Id)
);

