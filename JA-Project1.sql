CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1, 1),
    UserName NVARCHAR(30) NOT NULL UNIQUE,
    Password NVARCHAR(30) NOT NULL,
    IsManager BIT DEFAULT 0
);

INSERT INTO Users 
VALUES
('admin', 'pass', 1),
('jallen', '1234', 0),
('test', 'test', 0);

SELECT * FROM Users;

CREATE TABLE Tickets (
    Id INT PRIMARY KEY IDENTITY(100, 1),
    UserId INT FOREIGN KEY REFERENCES Users(Id),
    Description NVARCHAR(MAX) NOT NULL,
    Amount MONEY NOT NULL,
    DateSubmitted DATE DEFAULT GETDATE(),
    Status NVARCHAR(10) NOT NULL
);

INSERT INTO Tickets VALUES (2, 'Pizza Party', 60.00, GETDATE(), 'Pending');
INSERT INTO Tickets (UserId, [Description], Amount, [Status]) OUTPUT INSERTED.Id, INSERTED.DateSubmitted VALUES (2, 'Hotel', 130.00, 'Pending');

SELECT * FROM Tickets;

SELECT Tickets.Id, Users.UserName, Tickets.Amount FROM Tickets JOIN Users ON Tickets.UserId = Users.Id;

-- DROP TABLE Tickets;

INSERT INTO Tickets ([UserId], [Description], [Amount], [Status]) OUTPUT INSERTED.Id VALUES (@userid, @description, @amount, @status);

UPDATE Tickets SET Status = 'Approved' WHERE Id = 100;