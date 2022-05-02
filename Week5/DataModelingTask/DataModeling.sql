--Create
CREATE TABLE Courses (
	Id INT Identity(1, 1) PRIMARY KEY,
	CourseName NVARCHAR(50) NOT NULL,
	LessonsCount INT NOT NULL CHECK (LessonsCount > 0)
	)
GO

CREATE TABLE Teachers (
	Id INT Identity(1, 1) PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL
	)
GO

CREATE TABLE Students (
	Id INT Identity(1, 1) PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL
	)
GO

CREATE TABLE Lessons (
	Id INT Identity(1, 1) PRIMARY KEY,
	CourseId INT,
	TeacherId INT,
	LessonName NVARCHAR(50) NOT NULL,
	FOREIGN KEY (CourseId) REFERENCES Courses(Id),
	FOREIGN KEY (TeacherId) REFERENCES Teachers(Id)
	)
GO

CREATE TABLE Scores (
	Mark INT NOT NULL CHECK (
		0 < Mark
		AND Mark < 101
		),
	StudentId INT,
	LessonId INT,
	FOREIGN KEY (StudentId) REFERENCES Students(Id),
	FOREIGN KEY (LessonId) REFERENCES Lessons(Id),
	)

--Insert
INSERT INTO Teachers
VALUES (
	'Aziz',
	'Kassimov'
	);
GO

INSERT INTO Teachers
VALUES (
	'Arlan',
	'Orazbekov'
	);
GO

INSERT INTO Teachers
VALUES (
	'Vadim',
	'Belyakov'
	);
GO

----------------------------------------------------
INSERT INTO Students
VALUES (
	'Ivan',
	'Ivanov'
	);
GO

INSERT INTO Students
VALUES (
	'Alex',
	'Kostylev'
	);
GO

INSERT INTO Students
VALUES (
	'Dmitry',
	'Kuplinov'
	);
GO

INSERT INTO Students
VALUES (
	'Calvin',
	'Haynes'
	);
GO

INSERT INTO Students
VALUES (
	'Blaze',
	'Bright'
	);
GO

INSERT INTO Students
VALUES (
	'Josiah',
	'Avery'
	);
GO

INSERT INTO Students
VALUES (
	'Hedwig',
	'Middleton'
	);
GO

INSERT INTO Students
VALUES (
	'Jamalia',
	'Rojas'
	);
GO

INSERT INTO Students
VALUES (
	'Daniel',
	'Carlson'
	);
GO

INSERT INTO Students
VALUES (
	'Dmitry',
	'Hamilton'
	);
GO

----------------------------------------------------
INSERT INTO Courses
VALUES (
	'Programming',
	1
	);
GO

INSERT INTO Courses
VALUES (
	'Math',
	2
	);
GO

INSERT INTO Courses
VALUES (
	'Physics',
	3
	);
GO

----------------------------------------------------
INSERT INTO Lessons
VALUES (
	1,
	1,
	'Programming - lesson 1'
	);
GO

INSERT INTO Lessons
VALUES (
	1,
	1,
	'Programming - lesson 2'
	);
GO

INSERT INTO Lessons
VALUES (
	1,
	1,
	'Programming - lesson 3'
	);
GO

INSERT INTO Lessons
VALUES (
	2,
	2,
	'Math - lesson 1'
	);
GO

INSERT INTO Lessons
VALUES (
	2,
	2,
	'Math - lesson 2'
	);
GO

INSERT INTO Lessons
VALUES (
	2,
	2,
	'Math - lesson 3'
	);
GO

INSERT INTO Lessons
VALUES (
	3,
	3,
	'Physics - lesson 3'
	);
GO

INSERT INTO Lessons
VALUES (
	3,
	3,
	'Physics - lesson 2'
	);
GO

INSERT INTO Lessons
VALUES (
	3,
	3,
	'Physics - lesson 3'
	);
GO

----------------------------------------------------
INSERT INTO Scores
VALUES (
	91,
	1,
	1
	);
GO

INSERT INTO Scores
VALUES (
	78,
	2,
	1
	);
GO

INSERT INTO Scores
VALUES (
	53,
	3,
	1
	);
GO

INSERT INTO Scores
VALUES (
	83,
	1,
	2
	);
GO

INSERT INTO Scores
VALUES (
	77,
	2,
	2
	);
GO

INSERT INTO Scores
VALUES (
	63,
	3,
	2
	);
GO

INSERT INTO Scores
VALUES (
	75,
	1,
	3
	);
GO

INSERT INTO Scores
VALUES (
	88,
	2,
	3
	);
GO

INSERT INTO Scores
VALUES (
	70,
	3,
	3
	);
GO

-------------------------------------
INSERT INTO Scores
VALUES (
	91,
	4,
	4
	);
GO

INSERT INTO Scores
VALUES (
	70,
	5,
	4
	);
GO

INSERT INTO Scores
VALUES (
	93,
	6,
	4
	);
GO

INSERT INTO Scores
VALUES (
	50,
	4,
	5
	);
GO

INSERT INTO Scores
VALUES (
	77,
	5,
	5
	);
GO

INSERT INTO Scores
VALUES (
	63,
	6,
	5
	);
GO

INSERT INTO Scores
VALUES (
	43,
	4,
	6
	);
GO

INSERT INTO Scores
VALUES (
	88,
	5,
	6
	);
GO

INSERT INTO Scores
VALUES (
	78,
	6,
	6
	);
GO

--------------------------------------
INSERT INTO Scores
VALUES (
	91,
	7,
	7
	);
GO

INSERT INTO Scores
VALUES (
	78,
	8,
	7
	);
GO

INSERT INTO Scores
VALUES (
	93,
	9,
	7
	);
GO

INSERT INTO Scores
VALUES (
	83,
	10,
	7
	);
GO

INSERT INTO Scores
VALUES (
	77,
	7,
	8
	);
GO

INSERT INTO Scores
VALUES (
	63,
	8,
	8
	);
GO

INSERT INTO Scores
VALUES (
	75,
	9,
	8
	);
GO

INSERT INTO Scores
VALUES (
	88,
	10,
	8
	);
GO

INSERT INTO Scores
VALUES (
	78,
	7,
	9
	);
GO

INSERT INTO Scores
VALUES (
	84,
	8,
	9
	);
GO

INSERT INTO Scores
VALUES (
	99,
	9,
	9
	);
GO

INSERT INTO Scores
VALUES (
	72,
	10,
	9
	);

----------------------------------------------------
--Select
SELECT *,
	CASE 
		WHEN Final_Table.Final_Score >= 75
			THEN 'Passed'
		ELSE 'Falied'
		END AS Pass_Exam
FROM (
	SELECT s.StudentId AS Student_ID,
		CONCAT (
			ss.FirstName,
			' ',
			ss.LastName
			) AS Student_Name,
		c.CourseName AS Course_Name,
		avg(s.Mark) AS Final_Score
	FROM Scores s
	JOIN Lessons l ON s.LessonId = l.Id
	JOIN Courses c ON l.CourseId = c.Id
	JOIN Students ss ON s.StudentId = ss.Id
	GROUP BY s.StudentId,
		c.CourseName,
		ss.FirstName,
		ss.LastName
	) AS Final_Table
ORDER BY Final_Table.Student_ID

SELECT t.FirstName,
	t.LastName,
	c.CourseName,
	count(l.LessonName) AS Hours
FROM Teachers t
JOIN Lessons l ON t.Id = l.TeacherId
JOIN Courses c ON l.CourseId = c.Id
GROUP BY t.FirstName,
	t.LastName,
	c.CourseName;

--Drop
DROP TABLE Teachers;
GO

DROP TABLE Students;
GO

DROP TABLE Courses;
GO

DROP TABLE Lessons;
GO

DROP TABLE Scores;