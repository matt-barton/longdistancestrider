CREATE TABLE RunnerAlias (
    RunnerId INT NOT NULL,
    Alias VARCHAR(100) NOT NULL,

    PRIMARY KEY (RunnerId, Alias),
    FOREIGN KEY (RunnerId) REFERENCES Runner(Id)
)
GO

CREATE INDEX idx_RunnerAlias ON RunnerAlias (RunnerId)
GO

CREATE UNIQUE INDEX idx_Alias ON RunnerAlias (Alias)
GO
