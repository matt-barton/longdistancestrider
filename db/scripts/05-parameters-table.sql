CREATE TABLE Parameters (
    Name VARCHAR(32) NOT NULL,
    Value VARCHAR(32) NOT NULL,

    PRIMARY KEY (Name)
)
GO

INSERT INTO Parameters (Name, Value) VALUES
    ('FirstYear', '2025'),
    ('CurrentYear', '2025')
GO