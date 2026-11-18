
-- ==========================================
-- STUDENT MANAGEMENT API DATABASE
-- ==========================================

-- 1. CREATE DATABASE
CREATE DATABASE StudentAPI;
GO

-- 2. USE DATABASE
USE StudentAPI;
GO

-- 3. CREATE STUDENTS TABLE
CREATE TABLE Students
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Course NVARCHAR(100) NOT NULL
);
GO

-- 4. INSERT 4 STUDENT RECORDS
INSERT INTO Students (Name, Email, Course)
VALUES
('Rahul Patel', 'rahul@gmail.com', 'BCA'),
('Priya Sharma', 'priya@gmail.com', 'MCA'),
('Amit Shah', 'amit@gmail.com', 'B.Tech'),
('Neha Desai', 'neha@gmail.com', 'BBA');
GO

-- 5. DISPLAY ALL STUDENTS
SELECT * FROM Students;
GO

-- 6. DISPLAY STUDENT BY ID
SELECT * FROM Students
WHERE Id = 1;
GO

-- 7. DISPLAY STUDENTS BY COURSE
SELECT * FROM Students
WHERE Course = 'BCA';
GO
