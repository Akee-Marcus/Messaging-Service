--Introducing the sneder and new message into the db tables
SELECT message FROM `received` WHERE id = 1;
SET message = "new message";
INSERT message;

Select contact FROM 'received' WHERE id = 2;
SET name = contact;
INSERT contact;

--Creating the Columns for the tables
ALTER TABLE received;
ADD Name CHARACTER(100);
INSERT INTO name (Name);

ALTER TABLE received;
ADD Message CHARACTER(100);
INSERT INTO message (Message);
