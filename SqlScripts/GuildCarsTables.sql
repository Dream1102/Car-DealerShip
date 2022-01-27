USE GuildCars;
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Purchases')
	DROP TABLE Purchases
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Customer')
	DROP TABLE Customer
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ContactRequests')
	DROP TABLE ContactRequests
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Vehicle')
	DROP TABLE Vehicle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='VehicleModel')
	DROP TABLE VehicleModel
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Make')
	DROP TABLE Make
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='State')
	DROP TABLE State
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseType')
	DROP TABLE PurchaseType
GO



IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyle')
	DROP TABLE BodyStyle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='InteriorColor')
	DROP TABLE InteriorColor
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ExteriorColor')
	DROP TABLE ExteriorColor
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='TransmissionType')
	DROP TABLE TransmissionType
GO

CREATE TABLE TransmissionType(
TransmissionTypeId int identity(1,1) not null primary key,
TransmissionTypeName varchar(10) not null
)

CREATE TABLE ExteriorColor(
ExteriorColorId int identity(1,1) not null primary key,
ExteriorColorName varchar(10) not null
)

CREATE TABLE InteriorColor(
InteriorColorId int identity(1,1) not null primary key,
InteriorColorName varchar(10) not null
)

CREATE TABLE BodyStyle(
BodyStyleId int identity(1,1) not null primary key,
BodyStyleName varchar(5) not null
)

CREATE TABLE Make(
MakeId int identity(1,1) not null primary key,
MakeName varchar(50) not null,
UserEmail varchar(50) not null,
DateMakeCreated datetime2 not null default(getdate())
)

CREATE TABLE VehicleModel(
ModelId int identity(1,1) not null primary key,
MakeId int not null foreign key references Make(MakeId),
ModelName varchar(50) not null,
UserEmail varchar(50) not null,
DateModelCreated datetime2 not null default(getdate())
)

CREATE TABLE PurchaseType(
PurchaseTypeId int identity(1,1) not null primary key,
PurchaseTypeName varchar(15) not null
)

CREATE TABLE State(
StateAbbreviation char(2) not null primary key,
StateName varchar(50) not null
)

CREATE TABLE Vehicle(
VehicleId char(17) not null primary key,
ModelId int not null foreign key references VehicleModel(ModelId),
InteriorColorId int not null foreign key references InteriorColor(InteriorColorId),
ExteriorColorId int not null foreign key references ExteriorColor(ExteriorColorId),
TransmissionTypeId int not null foreign key references TransmissionType(TransmissionTypeId),
BodyStyleId int not null foreign key references BodyStyle(BodyStyleId),
Mileage int not null,
Year int not null,
DateAdded datetime2 not null default(getdate()),
ListedPrice decimal(8,2) not null,
MSRP decimal(8,2) not null,
Description varchar(500) not null,
ImageFileName varchar(32) not null,
IsFeatured bit not null,
IsUsed bit not null,
IsSold bit not null,
UserEmail varchar(50) not null
)

CREATE TABLE Specials(
SpecialId int identity(1,1) not null primary key,
SpecialImageFilename varchar(50) null,
SpecialName varchar(50),
SpecialDescription varchar(500),
DateSpecialCreated datetime2 not null default(getdate()),
UserEmail varchar(50) not null
)

CREATE TABLE ContactRequests(
ContactRequestId int identity(1,1) not null primary key,
VehicleId char(17) null,
ContactName varchar(50) not null,
ContactEmail varchar(50) null,
ContactPhone char(12) null,
ContactMessage varchar(500) not null,
DateContactRequestCreated datetime2 not null default(getdate())
)

CREATE TABLE Customer (
CustomerId int identity(1,1) not null primary key,
StateAbbreviation char(2) not null foreign key references State(StateAbbreviation),
CustomerName varchar(100) not null,
CustomerPhone char(12) null,
CustomerAddress1 varchar(100) not null,
CustomerAddress2 varchar(50) null,
City varchar(50) not null,
Zip char(5) not null,
CustomerEmail varchar(50) null
)

CREATE TABLE Purchases(
PurchaseId int identity(1,1) not null primary key,
PurchaseTypeId int not null foreign key references PurchaseType(PurchaseTypeId),
VehicleId char(17) not null foreign key references Vehicle(VehicleId),
CustomerId int not null foreign key references Customer(CustomerId),
PurchaseDate datetime2 not null default(getdate()),
PurchasePrice decimal(8,2) not null,
UserEmail varchar(50) not null
)





