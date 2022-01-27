IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
		DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN

DELETE FROM Purchases;
DELETE FROM Customer;
DELETE FROM ContactRequests;
DELETE FROM Specials;
DELETE FROM Vehicle;
DELETE FROM State;
DELETE FROM PurchaseType;
DELETE FROM VehicleModel;
DELETE FROM Make;
DELETE FROM BodyStyle;
DELETE FROM InteriorColor;
DELETE FROM ExteriorColor;
DELETE FROM TransmissionType;

DBCC CHECKIDENT (ContactRequests, RESEED, 2)
DBCC CHECKIDENT (Specials, RESEED, 2)
DBCC CHECKIDENT (Customer, RESEED, 1)
DBCC CHECKIDENT (Purchases, RESEED, 1)
DBCC CHECKIDENT (VehicleModel, RESEED, 11)
DBCC CHECKIDENT (Make, RESEED, 5)

SET IDENTITY_INSERT TransmissionType ON;

INSERT INTO TransmissionType(TransmissionTypeId, TransmissionTypeName)
VALUES(1, 'Automatic'),
(2, 'Manual');

SET IDENTITY_INSERT TransmissionType OFF;

SET IDENTITY_INSERT ExteriorColor ON;

INSERT INTO ExteriorColor(ExteriorColorId, ExteriorColorName)
VALUES(1, 'Black'),
(2, 'Guild Red'),
(3, 'Guild Gray'),
(4, 'Blue'), 
(5, 'Silver');

SET IDENTITY_INSERT ExteriorColor OFF;

SET IDENTITY_INSERT InteriorColor ON;

INSERT INTO InteriorColor(InteriorColorId, InteriorColorName)
VALUES(1, 'Black'),
(2, 'Guild Red'),
(3, 'Guild Gray'),
(4, 'Blue'), 
(5, 'Yellow');

SET IDENTITY_INSERT InteriorColor OFF;

SET IDENTITY_INSERT BodyStyle ON;

INSERT INTO BodyStyle(BodyStyleId, BodyStyleName)
VALUES(1, 'Car'),
(2, 'SUV'),
(3, 'Truck'),
(4, 'Van');

SET IDENTITY_INSERT BodyStyle OFF;

SET IDENTITY_INSERT Make ON;

INSERT INTO Make(MakeId, MakeName, UserEmail, DateMakeCreated)
VALUES(1, 'Ford', 'jamie@guildcars.com', '11/29/2021'),
(2, 'GMC', 'jamie@guildcars.com', '11/29/2021'),
(3, 'Toyota', 'jamie@guildcars.com', '11/29/2021'),
(4, 'Kia', 'jamie@guildcars.com', '11/29/2021'),
(5, 'Dodge', 'jamie@guildcars.com', '11/29/2021');

SET IDENTITY_INSERT Make OFF;

SET IDENTITY_INSERT VehicleModel ON;

INSERT INTO VehicleModel(ModelId, MakeId, ModelName, UserEmail, DateModelCreated)
VALUES(1, 1, 'Edge','jamie@guildcars.com', '11/29/2021'),
(2, 1, 'Explorer', 'jamie@guildcars.com', '11/29/2021'),
(3, 2, 'Acadia', 'jamie@guildcars.com', '11/29/2021'),
(4, 2, 'Terrain', 'jamie@guildcars.com', '11/29/2021'),
(5, 3, 'ForeRunner', 'jamie@guildcars.com', '11/29/2021'),
(6, 3, 'Highlander', 'jamie@guildcars.com', '11/29/2021'),
(7, 4, 'Sorento', 'jamie@guildcars.com', '11/29/2021'),
(8, 4, 'Telluride', 'jamie@guildcars.com', '11/29/2021'),
(9, 5, 'Challenger', 'jamie@guildcars.com', '11/29/2021'),
(10, 5, 'Durango', 'jamie@guildcars.com', '11/29/2021'),
(11, 5, 'Grand Caravan', 'jamie@guildcars.com', '11/29/2021');

SET IDENTITY_INSERT VehicleModel OFF;

SET IDENTITY_INSERT PurchaseType ON;

INSERT INTO PurchaseType(PurchaseTypeId, PurchaseTypeName)
VALUES(1,'Bank Finance'),
(2, 'Cash'),
(3, 'Dealer Finance');

SET IDENTITY_INSERT PurchaseType OFF;

INSERT INTO State(StateAbbreviation, StateName)
VALUES('KY','Kentucky'),
('OH', 'Ohio'),
('IN', 'Indiana'),
('IL', 'Illinois'),
('PA', 'Pennsylvania');

INSERT INTO Vehicle(VehicleId, ModelId, InteriorColorId, ExteriorColorId, TransmissionTypeId, BodyStyleId, Mileage, Year, DateAdded, ListedPrice, MSRP, Description, ImageFileName, IsFeatured, IsUsed, IsSold, UserEmail)
VALUES('1C4SDJGJ9JC416162', 10, 1, 1, 1, 2, 11503, 2018, '11/29/2021', 69900, 72000, 'A really nice looking vehicle', 'inventory-1C4SDJGJ9JC416162.jpg',1,1, 0,'jamie@guildcars.com'),
('1C4SDHCT2HC833522', 10, 2, 3, 1, 2, 85122, 2017, '11/29/2021', 35900, 38000, 'A really nice looking used vehicle', 'inventory-1C4SDHCT2HC833522.jpg',0,1, 0,'jamie@guildcars.com'),
('2C3CDZAG3GH124663', 9, 2, 3, 2, 1, 105193, 2016, '11/29/2021', 21900, 24000, 'A really fast blue car!', 'inventory-2C3CDZAG3GH124663.jpg',0,1, 0,'jamie@guildcars.com'),
('2C3CDZGG7HH628383', 9, 3, 5, 2, 1, 96, 2021, '11/29/2021', 36900, 40000, 'A fast silver car!', 'inventory-2C3CDZGG7HH628383.jpg',1,0, 0,'jamie@guildcars.com'),
('2C4RDGDG4GR321504', 11, 1, 3, 1, 4, 103235, 2016, '11/29/2021', 14900, 16000, 'A really useful van', 'inventory-2C4RDGDG4GR321504.jpg',0,1, 0,'jamie@guildcars.com'),
('1FM5K8GTXGGB60169', 2, 3, 2, 1, 2, 125884, 2016, '11/29/2021', 27900, 31000, 'A used......Explorer', 'inventory-1FM5K8GTXGGB60169.jpg',1,1, 0,'jamie@guildcars.com'),
('1FMHK7F83BGA02897', 2, 2, 3, 1, 2, 50, 2019, '11/29/2021', 55000, 57000, 'A very new......Explorer', 'inventory-1FMHK7F83BGA02897.jpg',0,0, 1,'jamie@guildcars.com'),
('1FM5K8F88DGB36574', 5, 2, 3, 1, 2, 85122, 2017, '11/29/2021', 35900, 38000, 'A really nice looking used vehicle', 'inventory-1FM5K8F88DGB36574.jpg',0,1, 0,'jamie@guildcars.com'),
('NM0LS6BN3CT075262', 5, 2, 3, 1, 2, 85122, 2017, '11/29/2021', 35900, 38000, 'A really nice looking used vehicle', 'inventory-NM0LS6BN3CT075262.jpg',0,1, 0,'jamie@guildcars.com'),
('1FMEE5BH1MLA96289', 5, 2, 3, 1, 2, 100, 2021, '11/29/2021', 35900, 38000, 'A really nice looking used vehicle', 'inventory-1FMEE5BH1MLA96289.jpg',0,0, 0,'jamie@guildcars.com');

SET IDENTITY_INSERT Specials ON;

INSERT INTO Specials(SpecialId, SpecialImageFilename, SpecialName, SpecialDescription, DateSpecialCreated, UserEmail)
VALUES(1,'freeTires.jpg','Free Tires For Life!','You can earn free tires for the life of your vehicle if you purchase today.', '11/29/2021', 'jamie@guildcars.com'),
(2, 'inventory-2C4RDGDG4GR321504.jpg', 'Buy this vehicle!', 'Seriously, we need to sell it. Make us an offer!', '11/29/2021', 'jamie@guildcars.com');

SET IDENTITY_INSERT Specials OFF;

SET IDENTITY_INSERT ContactRequests ON;

INSERT INTO ContactRequests(ContactRequestId, VehicleId, ContactName, ContactEmail, ContactPhone, ContactMessage, DateContactRequestCreated)
VALUES(1, '1FMEE5BH1MLA96289', 'Bob Bilby', NULL, '502-555-5555', 'I would like to buy this car!', '11/29/2021'),
(2, '2C4RDGDG4GR321504', 'Burt McHandsome', 'bert@mchandsome.com', NULL,'I will gladly pay you Tuesday for this car today!', '11/29/2021');

SET IDENTITY_INSERT ContactRequests OFF;

SET IDENTITY_INSERT Customer ON;

INSERT INTO Customer(CustomerId, StateAbbreviation, CustomerName, CustomerPhone, CustomerAddress1, CustomerAddress2, City, Zip, CustomerEmail)
VALUES( 1, 'KY','Telemachus', '502-555-5555', '523 Colonel Hill', NULL, 'Louisville', '40242','tel@machus.com');

SET IDENTITY_INSERT Customer OFF;

SET IDENTITY_INSERT Purchases ON;

INSERT INTO Purchases(PurchaseId, PurchaseTypeId, VehicleId, CustomerId, PurchaseDate, PurchasePrice, UserEmail)
VALUES(1, 2, '1FMHK7F83BGA02897', 1, '11/29/2021', 10000, 'jamie@guildcars.com');

SET IDENTITY_INSERT Purchases OFF;

END
GO











