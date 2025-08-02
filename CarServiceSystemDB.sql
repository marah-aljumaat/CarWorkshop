
USE master;
GO
DROP DATABASE IF EXISTS CarServiceSystem
CREATE DATABASE CarServiceSystem 
GO 
USE CarServiceSystem
GO
DROP SCHEMA IF EXISTS workshop 
GO 
CREATE SCHEMA workshop  
GO 

-----------------------------------CREATING TABLES------------------------------
DROP TABLE IF EXISTS workshop.Customers
CREATE TABLE workshop.Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(50) ,
    CustomerType NVARCHAR(20) ,
    CustomerPhone NVARCHAR(20) ,
    CustomerEmail NVARCHAR(50) 
)
GO 

--Foreign key : fieldName DataType FOREING KEY REFERENCES Schema.ParentTable(fieldName)

DROP TABLE IF EXISTS workshop.CustomerContacts
CREATE TABLE workshop.CustomerContacts (
    ContactID INT IDENTITY(10,1) PRIMARY KEY,
    CustomerID INT FOREIGN KEY REFERENCES workshop.Customers(CustomerID),
    ContactName NVARCHAR(50) ,
    ContactRole NVARCHAR(50) ,
    ContactPhone NVARCHAR(20) ,
    ContactEmail NVARCHAR(50) 
)
GO 

--enums like: mycol DataType NOT NULL CHECK (mycol IN('Value1', 'Value2', 'Value3'))

DROP TABLE IF EXISTS workshop.CustomerCars
CREATE TABLE workshop.CustomerCars (
    CarID INT IDENTITY(100,1) PRIMARY KEY,
    CustomerID INT FOREIGN KEY REFERENCES workshop.Customers(CustomerID),
    PlateNumber NVARCHAR(MAX) ,
    Color NVARCHAR(20) ,
    Model NVARCHAR(50) ,
    ManufactureYear INT , 
    ChassisNumber NVARCHAR(MAX) ,
    EngineNumber NVARCHAR(MAX) ,
    WarrantyStartDate DATE,
    WarrantyEndDate DATE,
    WarrantyCoveredDistance INT,
    WarrantyDuration INT,
    OdometerReading INT, 
    CarStatus NVARCHAR(MAX),   
    PlateType NVARCHAR(12) NOT NULL CHECK(PlateType IN ('Private','Commercial','Government','Taxi','Diplomatic','Temporary','Custom')) ,
    EngineType NVARCHAR(10) NOT NULL CHECK (EngineType IN ('Gasoline','Diesel','Hybrid','Electric')), 
    TransmissionType NVARCHAR(10) NOT NULL CHECK(TransmissionType IN ('Manual','Automatic'))
)
GO 

DROP TABLE IF EXISTS workshop.Projects
CREATE TABLE workshop.Projects (
    ProjectID INT IDENTITY(300,1) PRIMARY KEY,
    CustomerID INT FOREIGN KEY REFERENCES workshop.Customers(CustomerID),
    ProjectName NVARCHAR(MAX) ,
    ProjectDescription NVARCHAR(MAX),
    ProjectStartDate DATE,
    ProjectEndDate DATE,
    ProjectStatus NVARCHAR(MAX)
)
GO 

DROP TABLE IF EXISTS workshop.Invoices
CREATE TABLE workshop.Invoices (
    InvoiceID INT IDENTITY(600,1) PRIMARY KEY,
    CustomerID INT FOREIGN KEY REFERENCES workshop.Customers(CustomerID),
    DateIssued DATE,
    DueDate DATE,
    TotalAmount DECIMAL(20,2),
    InvoiceNotes NVARCHAR(MAX),
    InvoiceStatus NVARCHAR(MAX)    
)
GO 


DROP TABLE IF EXISTS workshop.Tasks
CREATE TABLE workshop.Tasks (
    TaskID INT IDENTITY(400,1) PRIMARY KEY,
    CustomerID INT FOREIGN KEY REFERENCES workshop.Customers(CustomerID),
    ProjectID INT FOREIGN KEY REFERENCES workshop.Projects(ProjectID),
    CarID INT FOREIGN KEY REFERENCES workshop.CustomerCars(CarID),
    TaskName NVARCHAR(MAX),
    TaskDescription NVARCHAR(MAX),
    TaskStatus NVARCHAR(MAX),
    TaskStartDate DATE,
    TaskEndDate DATE,   
    CarReceivedAt DATE,
    CarDeliveredAt DATE
)
GO 


DROP TABLE IF EXISTS workshop.Inventory
CREATE TABLE workshop.Inventory (
    InventoryID INT IDENTITY(900,1) PRIMARY KEY,
    ItemName NVARCHAR(50),
    ItemType NVARCHAR(50),
    ItemPrice DECIMAL(20,2),
    ItemStatus NVARCHAR(50)
)
GO 

DROP TABLE IF EXISTS workshop.InventoryGroups
CREATE TABLE workshop.InventoryGroups (
    GroupID INT IDENTITY(950,1) PRIMARY KEY,
    GroupName NVARCHAR(50),
    GroupDescription NVARCHAR(MAX)
)
GO 

DROP TABLE IF EXISTS workshop.InventoryGroupItems
CREATE TABLE workshop.InventoryGroupItems (
    InventoryID INT FOREIGN KEY REFERENCES workshop.Inventory(InventoryID),
    GroupID INT FOREIGN KEY REFERENCES workshop.InventoryGroups(GroupID),
    PRIMARY KEY (InventoryID,GroupID)
)
GO 

DROP TABLE IF EXISTS workshop.Employees
CREATE TABLE workshop.Employees (
    EmployeeID INT IDENTITY(700,1) PRIMARY KEY,
    EmployeeName NVARCHAR(50),
    JobTitle NVARCHAR(50),
    Specialty NVARCHAR(50),
    CommissionRate DECIMAL(20,2),
    UserName NVARCHAR(50),
    EmpPassword NVARCHAR(8),
    AttendanceStatus NVARCHAR(20),
    AttendanceDate DATE
)
GO

DROP TABLE IF EXISTS workshop.TaskLines
CREATE TABLE workshop.TaskLines (
    TaskLineID INT IDENTITY(450,1) PRIMARY KEY,
    TaskID INT FOREIGN KEY REFERENCES workshop.Tasks(TaskID),
    InventoryID INT FOREIGN KEY REFERENCES workshop.Inventory(InventoryID),
    EmployeeID INT FOREIGN KEY REFERENCES workshop.Employees(EmployeeID),
    Quantity DECIMAL(20,2),
    UnitPrice DECIMAL(20,2),
    LineTotal DECIMAL(20,2),
    TaskLineDescription NVARCHAR(MAX)
)
GO 

DROP TABLE IF EXISTS workshop.InvoiceLines
CREATE TABLE workshop.InvoiceLines (
    LineID INT IDENTITY(650,1) PRIMARY KEY,
    TaskLineID INT FOREIGN KEY REFERENCES workshop.TaskLines(TaskLineID),
    InvoiceID INT FOREIGN KEY REFERENCES workshop.Invoices(InvoiceID),
    InventoryID INT FOREIGN KEY REFERENCES workshop.Inventory(InventoryID),
    Quantity DECIMAL(20,2),
    UnitPrice DECIMAL(20,2),
    LineTotal DECIMAL(20,2),
    LineDescription NVARCHAR(MAX)  
)
GO

-----------------------------------INSERTING VALUES------------------------------
-------------------EMPLOYEES

INSERT INTO workshop.Employees(
    [Employees].[EmployeeName],
    [Employees].[JobTitle],
    [Employees].[Specialty],
    [Employees].[CommissionRate],
    [Employees].[UserName],
    [Employees].[EmpPassword],
    [Employees].[AttendanceStatus],
    [Employees].[AttendanceDate]
)VALUES
('Kutaiba Wael','Data Entry Specialist','Wrokshop Records',1.00,'kutaiba','Uu123456','Present',GETDATE()),
('Ali Wael', 'Technician', 'Engine', 5.00, 'ali', 'Aa123456', 'Present', GETDATE()),
('Sara Nihad', 'Receptionist', 'Customer Service', 3.50, 'sara', 'Ss123456', 'Present', GETDATE()),
('Khaled Youssef', 'Supervisor', 'Diagnostics', 6.75, 'khaled', 'Kk123456', 'Absent', GETDATE()),
('Waleed Murad', 'Mechanic', 'Transmission', 4.20, 'waleed', 'Ww123456', 'Present', GETDATE());

SELECT 
    [Employees].[EmployeeID],
    [Employees].[EmployeeName],
    [Employees].[JobTitle],
    [Employees].[Specialty],
    [Employees].[CommissionRate],
    [Employees].[UserName],
    [Employees].[EmpPassword],
    [Employees].[AttendanceStatus],
    [Employees].[AttendanceDate]
FROM workshop.Employees AS Employees

--Test
--Assign the password manually
UPDATE workshop.Employees SET [EmpPassword] = 'Mm123456'
   WHERE [UserName] = 'moaz';
-----------------------------CUSTOMERS

INSERT INTO workshop.Customers(
    [Customers].[CustomerName],
    [Customers].[CustomerType],
    [Customers].[CustomerPhone],
    [Customers].[CustomerEmail] 
)VALUES
('Omar Nabil','Individual','05555489112','omnb@gmail.com'),
('Nur Hizan', 'Individual', '05331234567', 'nur.hz@gmail.com'),
('Brothers Co.', 'Company', '02123456789', 'info@brothers.com'),
('Aya Al-Attar', 'Individual', '05441234567', 'aya12@gmail.com');

SELECT 
    [Customers].[CustomerID],
    [Customers].[CustomerName],
    [Customers].[CustomerType],
    [Customers].[CustomerPhone],
    [Customers].[CustomerEmail] 
FROM workshop.Customers AS Customers 


-----------------------CONTACT

INSERT INTO  workshop.CustomerContacts (
    [Cotacts].[CustomerID],
    [Cotacts].[ContactName],
    [Cotacts].[ContactRole],
    [Cotacts].[ContactPhone],
    [Cotacts].[ContactEmail] 
)VALUES
(3, 'Yusuf Ali', 'Assistant', '02123334455', 'yusuf@brothers.com'),
(2, 'Muhammad', 'Driver', '05445556677', 'mh2hz@gmail.com');

SELECT
    [Cotacts].[ContactID],
    [Cotacts].[CustomerID],
    [Cotacts].[ContactName],
    [Cotacts].[ContactRole],
    [Cotacts].[ContactPhone],
    [Cotacts].[ContactEmail]
FROM workshop.CustomerContacts AS Cotacts

-----------------------------CARS


INSERT INTO workshop.CustomerCars(
     [Cars].[CustomerID],
    [Cars].[PlateNumber],
    [Cars].[Color],
    [Cars].[Model],
    [Cars].[ManufactureYear],
    [Cars].[ChassisNumber],
    [Cars].[EngineNumber],
    [Cars].[WarrantyStartDate],
    [Cars].[WarrantyEndDate],
    [Cars].[WarrantyCoveredDistance],
    [Cars].[WarrantyDuration],
    [Cars].[OdometerReading],
    [Cars].[CarStatus],
    [Cars].[PlateType],
    [Cars].[EngineType],
    [Cars].[TransmissionType]
    )VALUES
    (1, '34A1234', 'Black', 'Toyota Corolla', 2020, 'JTDBU4EE9A9123456', '1NZFXE123456', '2020-03-15', '2023-03-15', 100000, 36, 85000, 'Under Repair', 'Private', 'Gasoline', 'Automatic'),
    (3, '06D9876', 'White', 'Hyundai H1', 2019, 'KMJWA37KLU1234567', 'G4KJ123456', '2019-06-01', '2022-06-01', 120000, 36, 105000, 'Active', 'Commercial', 'Diesel', 'Manual'),
    (3, '34CORP89', 'Silver', 'Nissan Leaf', 2022, '1N4AZ1CP2NC123456', 'EVNISSAN789654', '2022-04-15', '2026-04-15', 160000, 48, 18000, 'In Service', 'Commercial', 'Electric', 'Automatic'),
    (2, '13B4321', 'Gray', 'Kia Sportage', 2021, 'KNDPB3AC4B1234567', 'G4NA456789', '2021-01-01', '2024-01-01', 80000, 36, 30000, 'Ready for Delivery', 'Private', 'Gasoline', 'Automatic'),
    (4, '21C6789', 'Red', 'Tesla Model 3', 2022, '5YJ3E1EA7JF123456', 'EVBATT000789', '2022-08-10', '2026-08-10', 200000, 48, 12000, 'Under Inspection', 'Private', 'Electric', 'Automatic');

SELECT 
    [Cars].[CarID],
    [Cars].[CustomerID],
    [Cars].[PlateNumber],
    [Cars].[Color],
    [Cars].[Model],
    [Cars].[ManufactureYear],
    [Cars].[ChassisNumber],
    [Cars].[EngineNumber],
    [Cars].[WarrantyStartDate],
    [Cars].[WarrantyEndDate],
    [Cars].[WarrantyCoveredDistance],
    [Cars].[WarrantyDuration],
    [Cars].[OdometerReading],
    [Cars].[CarStatus],
    [Cars].[PlateType],
    [Cars].[EngineType],
    [Cars].[TransmissionType] 
FROM workshop.CustomerCars AS Cars

-----------------------PROJECTS

INSERT INTO workshop.Projects(
    [Projects].[CustomerID],
    [Projects].[ProjectName],
    [Projects].[ProjectDescription],
    [Projects].[ProjectStartDate],
    [Projects].[ProjectEndDate],
    [Projects].[ProjectStatus]
    )VALUES
(2, 'Routine Maintenance', 'Basic oil change and inspection', '2025-07-20', '2025-07-22', 'Completed'),
(1, 'Transmission Repair', 'Fixing gearbox issues', '2025-07-23', NULL, 'Ongoing');

SELECT 
    [Projects].[ProjectID],
    [Projects].[CustomerID],
    [Projects].[ProjectName],
    [Projects].[ProjectDescription],
    [Projects].[ProjectStartDate],
    [Projects].[ProjectEndDate],
    [Projects].[ProjectStatus]
FROM workshop.Projects AS Projects 

--------------------TASK

INSERT INTO workshop.Tasks(
    [Tasks].[CustomerID],
    [Tasks].[ProjectID],
    [Tasks].[CarID],
    [Tasks].[TaskName],
    [Tasks].[TaskDescription],
    [Tasks].[TaskStatus],
    [Tasks].[TaskStartDate],
    [Tasks].[TaskEndDate],
    [Tasks].[CarReceivedAt],
    [Tasks].[CarDeliveredAt]
)VALUES
(2, 300, 103, 'Oil Change', 'Engine oil and filter change', 'Completed', '2025-07-20', '2025-07-21', '2025-07-20', '2025-07-22'),
(1, 301, 101, 'Gearbox Repair', 'Disassemble and replace gearbox', 'In Progress', '2025-07-23', NULL, '2025-07-23', NULL);

SELECT 
    [Tasks].[TaskID],
    [Tasks].[CustomerID],
    [Tasks].[ProjectID],
    [Tasks].[CarID],
    [Tasks].[TaskName],
    [Tasks].[TaskDescription],
    [Tasks].[TaskStatus],
    [Tasks].[TaskStartDate],
    [Tasks].[TaskEndDate],
    [Tasks].[CarReceivedAt],
    [Tasks].[CarDeliveredAt]
FROM workshop.Tasks AS Tasks

-----------------------INVOICES

INSERT INTO workshop.Invoices(
    [Invoices].[CustomerID],
    [Invoices].[DateIssued],
    [Invoices].[DueDate],
    [Invoices].[TotalAmount],
    [Invoices].[InvoiceNotes],
    [Invoices].[InvoiceStatus]
) VALUES
(2, '2025-07-23', '2025-08-01', 1200.00, 'Paid in cash', 'Paid'),
(1, '2025-07-24', '2025-08-05', 3500.00, 'Pending approval', 'Unpaid');

SELECT 
    [Invoices].[InvoiceID],
    [Invoices].[CustomerID],
    [Invoices].[DateIssued],
    [Invoices].[DueDate],
    [Invoices].[TotalAmount],
    [Invoices].[InvoiceNotes],
    [Invoices].[InvoiceStatus]
FROM workshop.Invoices AS Invoices

----------------------------INVENTORY

INSERT INTO workshop.Inventory(
    [Inventory].[ItemName],
    [Inventory].[ItemType],
    [Inventory].[ItemPrice],
    [Inventory].[ItemStatus] 
)VALUES
('Engine Oil', 'Fluid', 120.00, 'Available'),
('Brake Fluid', 'Fluid', 85.00, 'Available'),
('Oil Filter', 'Part', 75.00, 'Available'),
('Air Filter', 'Part', 65.00, 'Available'),
('Battery', 'Electronics', 450.00, 'Available'),
('Technician Labor (1 Hour)', 'Service', 150.00, 'Always Available'),
('Diagnostic Check', 'Service', 100.00, 'Always Available');

SELECT 
    [Inventory].[InventoryID],
    [Inventory].[ItemName],
    [Inventory].[ItemType],
    [Inventory].[ItemPrice],
    [Inventory].[ItemStatus]
FROM workshop.Inventory AS Inventory

-----------------------GROUPS

INSERT INTO workshop.InventoryGroups(
       [InventoryGroups].[GroupName],
       [InventoryGroups].[GroupDescription]
)VALUES
('Fluids', 'Engine Oil, Brake Fluid '),
('Filters', 'Oil Filter, Air Filter '),
('Electronics', 'Battery'),
('Services', 'Technician Labor (1 Hour), Diagnostic Check');

SELECT 
    [InventoryGroups].[GroupID],
    [InventoryGroups].[GroupName],
    [InventoryGroups].[GroupDescription] 
FROM workshop.InventoryGroups AS InventoryGroups

-----------------------GROUP-ITEMS

INSERT INTO workshop.InventoryGroupItems (InventoryID, GroupID) VALUES
(900, 950), 
(901, 950), 
(902, 951), 
(903, 951), 
(904, 952), 
(905, 953), 
(906, 953); 

SELECT 
    [InventoryID],
    [GroupID]
FROM workshop.InventoryGroupItems

--------------------------------TASK-LINE

INSERT INTO workshop.TaskLines (
[TaskLines].[TaskID],
[TaskLines].[InventoryID],
[TaskLines].[EmployeeID],
[TaskLines].[Quantity],
[TaskLines].[UnitPrice],
[TaskLines].[LineTotal],
[TaskLines].[TaskLineDescription]
)VALUES
(400, 900, 701, 1, 120.00, 120.00, 'Used 1 liter of engine oil'),
(400, 902, 701, 1, 75.00, 75.00, 'Replaced oil filter'),
(400, 905, 701, 1, 150.00, 150.00, 'Technician labor charge for oil change'),
(401, 905, 703, 3, 150.00, 450.00, '3 hours labor for gearbox disassembly'),
(401, 906, 703, 1, 100.00, 100.00, 'Diagnostic check before repair');

SELECT 
    [TaskLines].[TaskLineID],
    [TaskLines].[TaskID],
    [TaskLines].[InventoryID],
    [TaskLines].[EmployeeID],
    [TaskLines].[Quantity],
    [TaskLines].[UnitPrice],
    [TaskLines].[LineTotal],
    [TaskLines].[TaskLineDescription]
FROM workshop.TaskLines AS TaskLines


-----------------------INVOICES-LINE

INSERT INTO workshop.InvoiceLines(
[InvoiceLines].[TaskLineID],
[InvoiceLines].[InvoiceID],
[InvoiceLines].[InventoryID],
[InvoiceLines].[Quantity],
[InvoiceLines].[UnitPrice],
[InvoiceLines].[LineTotal],
[InvoiceLines].[LineDescription]
)VALUES
(450, 600, 900, 1, 120.00, 120.00, 'Engine Oil'),
(451, 600, 902, 1, 75.00, 75.00, 'Oil Filter'),
(452, 600, 905, 1, 150.00, 150.00, 'Labor: Oil Change'),
(453, 601, 905, 3, 150.00, 450.00, 'Labor: Gearbox Repair (3 hrs)'),
(454, 601, 906, 1, 100.00, 100.00, 'Diagnostic Check');

SELECT 
    [InvoiceLines].[LineID],
    [InvoiceLines].[TaskLineID],
    [InvoiceLines].[InvoiceID],
    [InvoiceLines].[InventoryID],
    [InvoiceLines].[Quantity],
    [InvoiceLines].[UnitPrice],
    [InvoiceLines].[LineTotal],
    [InvoiceLines].[LineDescription] 
FROM workshop.InvoiceLines AS InvoiceLines

-----------------------------------------PROCEDURES 
GO
CREATE OR ALTER PROCEDURE workshop.spTasksSummary_Get 
AS
    BEGIN
       SELECT COUNT(*) AS DailyTasks
       FROM workshop.Tasks AS Tasks
       WHERE DAY(TaskStartDate) = DAY(GETDATE());

       SELECT COUNT(*) AS WeeklyTasks
       FROM workshop.Tasks AS Tasks
       WHERE DATEDIFF(DAY,TaskStartDate,GETDATE()) <= 7;

       SELECT COUNT(*) AS MonthlyTasks
       FROM workshop.Tasks AS Tasks
       WHERE MONTH(TaskStartDate) = MONTH(GETDATE())
         AND YEAR(TaskStartDate)=YEAR(GETDATE());
    END 
GO 
-- EXEC workshop.spTasksSummary_Get 

CREATE OR ALTER PROCEDURE workshop.spTaskStatusSummary_Get
AS 
    BEGIN
       SELECT  
           TaskStatus,
           COUNT(*) AS TS_Count 
       FROM workshop.Tasks AS Tasks
       GROUP BY TaskStatus;
    END
GO
--EXEC  workshop.spTaskStatusSummary_Get

--Tasks count , Worked hours (taskLines quantity) 
CREATE OR ALTER PROCEDURE workshop.spEmployeesPerformanceSummary_Get
AS 
    BEGIN
       SELECT 
       [Employees].[EmployeeID],
       [Employees].[EmployeeName],
       COUNT([TaskLines].[TaskID]) AS NumberOfTasks,
       SUM([TaskLines].[Quantity]) AS WorkedHours
       FROM workshop.Employees AS Employees 
       LEFT JOIN workshop.TaskLines AS TaskLines ON Employees.EmployeeID = TaskLines.EmployeeID 
       GROUP BY
       [Employees].[EmployeeID],
       [Employees].[EmployeeName]
       ORDER BY 
       [Employees].[EmployeeName]
    END
GO

--EXEC workshop.spEmployeesPerformanceSummary_Get


---CAR , DATE , TASKS TOTAL cost
CREATE OR ALTER PROCEDURE workshop.spCustomerServiceHistory_Get
 --@CustomerID INT 
AS 
    BEGIN
       SELECT 
          [CustomerCars].[CustomerID],
          [CustomerCars].[CarID],
          [CustomerCars].[PlateNumber],
          [Tasks].[TaskName],
          [Tasks].[TaskStartDate],
          [Tasks].[TaskEndDate],
          SUM([TaskLines].[LineTotal]) AS TotalTaskCost
       FROM workshop.CustomerCars AS CustomerCars
        INNER JOIN workshop.Tasks AS Tasks ON CustomerCars.CarID = Tasks.CarID
        INNER JOIN workshop.TaskLines AS TaskLines ON Tasks.TaskID = TaskLines.TaskID
        --WHERE CustomerCars.CustomerID = @CustomerID
        GROUP BY
          [CustomerCars].[CustomerID],
          [CustomerCars].[CarID],
          [CustomerCars].[PlateNumber],
          [Tasks].[TaskName],
          [Tasks].[TaskStartDate],
          [Tasks].[TaskEndDate]
        ORDER BY TaskStartDate;
    END
GO
EXEC workshop.spCustomerServiceHistory_Get --@CustomerID = 2;