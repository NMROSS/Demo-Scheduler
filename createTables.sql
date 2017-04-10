-----------------------------------
--      DROP Whole Database       -
-- WARNING WILL DELETE EVERYTHING -
-----------------------------------
drop table preferredPaper
drop table enrolled
drop table previouslyDemoed
drop table grade
drop table demos
drop table Demonstrator
drop table lab
drop table paper

-- Create DB tables
-------------------
create table Paper
(
 code varchar(20) NOT NULL,
 name varchar(200),
 primary key (code)
)

create table Lab
(
 id int NOT NULL IDENTITY(1,1),
 paper varchar(20),
 labday varchar(10),
 labtime int,
 room varchar(10),
 primary key(id),
 foreign key (paper) references Paper(code)
)


create table Demonstrator
(
 studentID int NOT NULL unique,
 name varchar(200) not null,
 lastName varchar(200),
 phoneNo varchar(200),
 age integer,
 gender varchar(20),
 username varchar(200),
 summerAddr varchar(200),
 major varchar(200),
 degree varchar(200),
 studyYear int,
 email varchar(200),
 hoursWanted int,
 lastYear bit, 
 profileIMG image,
 primary key(studentID)
)

create table Demos
(
	LabID int,
	DemoID int,
	foreign key (LabID) references Lab(id),
	foreign key (DemoID) references Demonstrator(studentID)
)

create table Grade
(
	DemoID int NOT NULL,
	paperCode varchar(20)NOT NULL, -- e.g. PSYH101
	grade varchar(2)NOT NULL,
	foreign key (DemoID) references Demonstrator(studentID),
)

create table preferredPaper
(
	paperCode varchar(20) NOT NULL,
	demoID int NOT NULL,
	foreign key (DemoID) references Demonstrator(studentID)
)

create table enrolled
(
	paperCode varchar(20) NOT NULL,
	demoID int  NOT NULL,
	foreign key (DemoID) references Demonstrator(studentID)
)

create table previouslyDemoed
(
	paperCode varchar(20) NOT NULL,
	demoID int NOT NULL,
	foreign key (DemoID) references Demonstrator(studentID)
)

				
-- View All Tables content
SELECT * FROM paper
SELECT * FROM LAB
SELECT * FROM DEMONSTRATOR
SELECT * FROM DEMOS
SELECT * FROM GRADE
select * from enrolled 
select * from previouslyDemoed
select * from preferredPaper

--create global accessable schema
CREATE SYNONYM paper
FOR [VirtualChair].[SCMS\nmr13].paper
CREATE SYNONYM LAB
FOR [VirtualChair].[SCMS\nmr13].LAB
CREATE SYNONYM DEMONSTRATOR
FOR [VirtualChair].[SCMS\nmr13].DEMONSTRATOR
CREATE SYNONYM DEMOS
FOR [VirtualChair].[SCMS\nmr13].DEMOS
CREATE SYNONYM GRADE
FOR [VirtualChair].[SCMS\nmr13].GRADE
CREATE SYNONYM enrolled
FOR [VirtualChair].[SCMS\nmr13].enrolled
CREATE SYNONYM previouslyDemoed
FOR [VirtualChair].[SCMS\nmr13].previouslyDemoed
CREATE SYNONYM preferredPaper
FOR [VirtualChair].[SCMS\nmr13].preferredPaper
