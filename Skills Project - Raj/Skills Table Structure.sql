create table Employee(
EmpId int identity(1,1) constraint PK_EmpId_Employee primary key,
EmpName varchar(30)
)

create table Skills(
SkillId int identity(1,1) constraint PK_SkillId_Skills primary key,
Skill varchar(30),
Score decimal(5,2)
)

create table EmployeeSkill(
EmpId int 
constraint FK_EmpId_Employee_EmployeeSkill 
foreign key references Employee(EmpId),
SkillId int
constraint FK_SkillId_Skills_EmployeeSkill 
foreign key references Skills(SkillId)
)

alter table Skills alter column Skill varchar(60)

insert into Skills values('Data structures and algorithms',3.2)
insert into Skills values('Database and SQL',3.5)
insert into Skills values('Object-oriented programming (OOP) languages',2.5)
insert into Skills values('Integrated development environments (IDEs)',2)
insert into Skills values('Cloud computing',5)
insert into Skills values('Web development',4)
insert into Skills values('Containers',2)
insert into Skills values('Text editors',1.5)
insert into Skills values('Git version control',3)

select * from Skills
select scope_identity();
select * from Employee
select * from EmployeeSkill

select Skill from EmployeeSkill ES
INNER JOIN Skills S ON S.SkillId = ES.SkillId
WHERE EMPID = 1

select Employee.EmpId,EmpName,
STUFF(
(select ','+Skill from EmployeeSkill ES
INNER JOIN Skills S ON S.SkillId = ES.SkillId WHERE ES.EmpId = EmployeeSkill.EmpId
FOR XML PATH('')),1,1,'') as Skills
from Employee 
INNER JOIN EmployeeSkill ON EmployeeSkill.EmpId = Employee.EmpId
GROUP BY Employee.EmpId,EmployeeSkill.EmpId,EmpName

set identity_insert Skills off
set identity_insert Skills on 

dbcc CHECKIDENT('Skills',reseed,0);

select ','+Skill from EmployeeSkill ES
INNER JOIN Skills S ON S.SkillId = ES.SkillId WHERE ES.EmpId = 2
FOR XML PATH('')
select * from EmployeeSkill