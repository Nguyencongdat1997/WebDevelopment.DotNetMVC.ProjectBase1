Create Database DotNetMVCBaseProject
Use DotNetMVCBaseProject

Create Table Account(
	ID int IDENTITY(1,1),
	Username nvarchar(50) PRIMARY KEY,
	Passsword nvarchar(50),
	Name nvarchar(50),
	DateOfBirth date,
	Gender bit,
	Roles nvarchar(50),
)

insert into Account values(
	'Admin','1','Dat','1997-05-27',1,'Admin'
)
insert into Account values(
	'User1','1','Dat','1997-05-27',1,'User'
)
insert into Account values(
	'User2','1','Dat','1997-05-27',1,'User'
)