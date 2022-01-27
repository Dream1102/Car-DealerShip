-------------------Vehicle SPROCS--------------------

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'BodyStyleSelectAll')
		DROP PROCEDURE BodyStyleSelectAll
GO

CREATE PROCEDURE BodyStyleSelectAll AS
BEGIN
	SELECT BodyStyleId, BodyStyleName
	FROM BodyStyle
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ExteriorColorSelectAll')
		DROP PROCEDURE ExteriorColorSelectAll
GO

CREATE PROCEDURE ExteriorColorSelectAll AS
BEGIN
	SELECT ExteriorColorId, ExteriorColorName
	FROM ExteriorColor
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InteriorColorSelectAll')
		DROP PROCEDURE InteriorColorSelectAll
GO

CREATE PROCEDURE InteriorColorSelectAll AS
BEGIN
	SELECT InteriorColorId, InteriorColorName
	FROM InteriorColor
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TransmissionTypeSelectAll')
		DROP PROCEDURE TransmissionTypeSelectAll
GO

CREATE PROCEDURE TransmissionTypeSelectAll AS
BEGIN
	SELECT TransmissionTypeId, TransmissionTypeName
	FROM TransmissionType
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakeSelectAll')
		DROP PROCEDURE MakeSelectAll
GO

CREATE PROCEDURE MakeSelectAll AS
BEGIN
	SELECT MakeId, MakeName, UserEmail, DateMakeCreated
	FROM Make
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MakeAndModelList')
		DROP PROCEDURE MakeAndModelList
GO

CREATE PROCEDURE MakeAndModelList AS
BEGIN
	SELECT mk.MakeName, ModelName, DateModelCreated, m.UserEmail 
	FROM VehicleModel m
	INNER JOIN Make mk ON mk.MakeId = m.MakeId 
	ORDER BY mk.MakeName, ModelName ASC
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'MakeInsert')
		DROP PROCEDURE MakeInsert
GO

CREATE PROCEDURE MakeInsert (
@MakeId int output,
@MakeName varchar(50),
@UserEmail varchar(50))

AS
BEGIN
	INSERT INTO Make(MakeName, UserEmail)
	VALUES(@MakeName,@UserEmail)
	
	SET @MakeId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ModelSelectAll')
		DROP PROCEDURE ModelSelectAll
GO

CREATE PROCEDURE ModelSelectAll AS
BEGIN
	SELECT ModelId, MakeId, ModelName, UserEmail, DateModelCreated
	FROM VehicleModel
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'ModelInsert')
		DROP PROCEDURE ModelInsert
GO

CREATE PROCEDURE ModelInsert (
@ModelId int output,
@MakeId int,
@ModelName varchar(50),
@UserEmail varchar(50))

AS
BEGIN
	INSERT INTO VehicleModel(MakeId, ModelName, UserEmail)
	VALUES(@MakeId, @ModelName,@UserEmail)
	
	SET @ModelId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'FeaturedSelect')
		DROP PROCEDURE FeaturedSelect
GO

CREATE PROCEDURE FeaturedSelect AS
BEGIN
	SELECT 
	VehicleId,  
	ListedPrice, 
	MakeName,
	ModelName,
	Year, 
	ImageFileName
FROM Vehicle v
	INNER JOIN VehicleModel m ON m.ModelId = v.ModelId
	INNER JOIN Make mk ON mk.MakeId = m.MakeId
WHERE IsFeatured = 1
ORDER BY ListedPrice DESC
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'VehicleQuickSearch')
		DROP PROCEDURE VehicleQuickSearch
GO

CREATE PROCEDURE VehicleQuickSearch 
@SearchParameter varchar(25) = null,
@IsUsed bit,
@MinPrice decimal(8,2),
@MaxPrice decimal(8,2),
@MinYear int,
@MaxYear int

AS
BEGIN
IF(@SearchParameter IS NOT NULL)
	SELECT TOP 20 
			VehicleId, Year, MakeName, ModelName, BodyStyleName, TransmissionTypeName, ExteriorColorName, InteriorColorName, Mileage, ListedPrice, MSRP, ImageFileName
		FROM Vehicle v
			INNER JOIN TransmissionType t ON t.TransmissionTypeId = v.TransmissionTypeId 
			INNER JOIN ExteriorColor exc ON exc.ExteriorColorId = v.ExteriorColorId 
			INNER JOIN InteriorColor inc ON inc.InteriorColorId = v.InteriorColorId 
			INNER JOIN BodyStyle b ON b.BodyStyleId = v.BodyStyleId 
			INNER JOIN VehicleModel m ON m.ModelId = v.ModelId 
			INNER JOIN Make mk ON mk.MakeId = m.MakeId 
		WHERE 1=1 AND IsUsed = @IsUsed AND IsSold = 0 AND (m.ModelName LIKE @SearchParameter + '%' OR mk.MakeName LIKE @SearchParameter+ '%' OR v.Year LIKE '%' + @SearchParameter)
		AND v.ListedPrice Between @MinPrice AND @MaxPrice AND v.Year BETWEEN @MinYear AND @MaxYear
		ORDER BY MSRP DESC;
ELSE
SELECT TOP 20 
			VehicleId, Year, MakeName, ModelName, BodyStyleName, TransmissionTypeName, ExteriorColorName, InteriorColorName, Mileage, ListedPrice, MSRP, ImageFileName
		FROM Vehicle v
			INNER JOIN TransmissionType t ON t.TransmissionTypeId = v.TransmissionTypeId 
			INNER JOIN ExteriorColor exc ON exc.ExteriorColorId = v.ExteriorColorId 
			INNER JOIN InteriorColor inc ON inc.InteriorColorId = v.InteriorColorId 
			INNER JOIN BodyStyle b ON b.BodyStyleId = v.BodyStyleId 
			INNER JOIN VehicleModel m ON m.ModelId = v.ModelId 
			INNER JOIN Make mk ON mk.MakeId = m.MakeId 
		WHERE 1=1 AND IsUsed = @IsUsed AND IsSold = 0 AND v.ListedPrice Between @MinPrice AND @MaxPrice AND v.Year BETWEEN @MinYear AND @MaxYear
		ORDER BY MSRP DESC;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'SalesVehicleQuickSearch')
		DROP PROCEDURE SalesVehicleQuickSearch
GO

CREATE PROCEDURE SalesVehicleQuickSearch 
@SearchParameter varchar(25) = null,
@MinPrice decimal(8,2),
@MaxPrice decimal(8,2),
@MinYear int,
@MaxYear int

AS
BEGIN
IF(@SearchParameter IS NOT NULL)
	SELECT TOP 20 
			VehicleId, Year, MakeName, ModelName, BodyStyleName, TransmissionTypeName, ExteriorColorName, InteriorColorName, Mileage, ListedPrice, MSRP, ImageFileName
		FROM Vehicle v
			INNER JOIN TransmissionType t ON t.TransmissionTypeId = v.TransmissionTypeId 
			INNER JOIN ExteriorColor exc ON exc.ExteriorColorId = v.ExteriorColorId 
			INNER JOIN InteriorColor inc ON inc.InteriorColorId = v.InteriorColorId 
			INNER JOIN BodyStyle b ON b.BodyStyleId = v.BodyStyleId 
			INNER JOIN VehicleModel m ON m.ModelId = v.ModelId 
			INNER JOIN Make mk ON mk.MakeId = m.MakeId 
		WHERE 1=1 AND IsSold = 0 AND (m.ModelName LIKE @SearchParameter + '%' OR mk.MakeName LIKE @SearchParameter+ '%' OR v.Year LIKE '%' + @SearchParameter)
		AND v.ListedPrice Between @MinPrice AND @MaxPrice AND v.Year BETWEEN @MinYear AND @MaxYear
		ORDER BY MSRP DESC;
ELSE
SELECT TOP 20 
			VehicleId, Year, MakeName, ModelName, BodyStyleName, TransmissionTypeName, ExteriorColorName, InteriorColorName, Mileage, ListedPrice, MSRP, ImageFileName
		FROM Vehicle v
			INNER JOIN TransmissionType t ON t.TransmissionTypeId = v.TransmissionTypeId 
			INNER JOIN ExteriorColor exc ON exc.ExteriorColorId = v.ExteriorColorId 
			INNER JOIN InteriorColor inc ON inc.InteriorColorId = v.InteriorColorId 
			INNER JOIN BodyStyle b ON b.BodyStyleId = v.BodyStyleId 
			INNER JOIN VehicleModel m ON m.ModelId = v.ModelId 
			INNER JOIN Make mk ON mk.MakeId = m.MakeId 
		WHERE 1=1 AND IsSold = 0 AND v.ListedPrice Between @MinPrice AND @MaxPrice AND v.Year BETWEEN @MinYear AND @MaxYear
		ORDER BY MSRP DESC;
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'VehicleDetails')
		DROP PROCEDURE VehicleDetails
GO

CREATE PROCEDURE VehicleDetails 
@VehicleId char(17)

AS
BEGIN
SELECT
	VehicleId, Year, mk.MakeId, MakeName, m.ModelId, ModelName, b.BodyStyleId, BodyStyleName, t.TransmissionTypeId, TransmissionTypeName, exc.ExteriorColorId, ExteriorColorName, inc.InteriorColorId,InteriorColorName, Mileage, ListedPrice, MSRP, ImageFileName, Description, IsFeatured, IsUsed, IsSold, v.UserEmail
	FROM Vehicle v
		INNER JOIN TransmissionType t ON t.TransmissionTypeId = v.TransmissionTypeId 
		INNER JOIN ExteriorColor exc ON exc.ExteriorColorId = v.ExteriorColorId 
		INNER JOIN InteriorColor inc ON inc.InteriorColorId = v.InteriorColorId 
		INNER JOIN BodyStyle b ON b.BodyStyleId = v.BodyStyleId 
		INNER JOIN VehicleModel m ON m.ModelId = v.ModelId 
		INNER JOIN Make mk ON mk.MakeId = m.MakeId 
WHERE 1=1 AND @VehicleId = v.VehicleId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'VehicleInsert')
		DROP PROCEDURE VehicleInsert
GO

CREATE PROCEDURE VehicleInsert (
@VehicleId char(17),
@ModelId int,
@IsUsed bit,
@BodyStyleId int,
@Year int,
@TransmissionTypeId int,
@ExteriorColorId int,
@InteriorColorId int,
@Mileage int,
@MSRP decimal(8,2),
@ListedPrice decimal(8,2),
@Description varchar(500),
@ImageFileName varchar(32),
@IsFeatured bit,
@IsSold bit,
@UserEmail varchar(50)
)
AS
BEGIN
	INSERT INTO Vehicle (ModelId, VehicleId, IsUsed,  BodyStyleId, Year, TransmissionTypeId, ExteriorColorId, InteriorColorId, Mileage, MSRP, ListedPrice, Description, ImageFileName, IsFeatured, IsSold, UserEmail)
	VALUES(@ModelId,@VehicleId,  @IsUsed, @BodyStyleId, @Year, @TransmissionTypeId, @ExteriorColorId, @InteriorColorId, @Mileage, @MSRP, @ListedPrice, @Description, @ImageFileName, @IsFeatured, @IsSold, @UserEmail)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'AllInventorySearch')
		DROP PROCEDURE AllInventorySearch
GO

CREATE PROCEDURE AllInventorySearch 
AS
BEGIN
	SELECT 
			VehicleId, Year, MakeName, ModelName, BodyStyleName, TransmissionTypeName, ExteriorColorName, InteriorColorName, Mileage, ListedPrice, MSRP, Description, ImageFileName, IsFeatured, IsUsed, IsSold, v.UserEmail
		FROM Vehicle v
			INNER JOIN TransmissionType t ON t.TransmissionTypeId = v.TransmissionTypeId 
			INNER JOIN ExteriorColor exc ON exc.ExteriorColorId = v.ExteriorColorId 
			INNER JOIN InteriorColor inc ON inc.InteriorColorId = v.InteriorColorId 
			INNER JOIN BodyStyle b ON b.BodyStyleId = v.BodyStyleId 
			INNER JOIN VehicleModel m ON m.ModelId = v.ModelId 
			INNER JOIN Make mk ON mk.MakeId = m.MakeId 
		WHERE 1=1 AND IsSold = 0
		ORDER BY MSRP DESC;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehicleUpdate')
		DROP PROCEDURE VehicleUpdate
GO

CREATE PROCEDURE VehicleUpdate(
@ModelId int,
@VehicleId char(17),
@InteriorColorId int,
@ExteriorColorId int,
@TransmissionTypeId int,
@BodyStyleId int,
@Mileage int,
@Year int,
@ListedPrice decimal(8,2),
@MSRP decimal(8,2),
@Description varchar(500),
@ImageFileName varchar(32),
@IsFeatured bit,
@IsUsed bit,
@IsSold bit,
@UserEmail varchar(50))
AS
BEGIN
	UPDATE Vehicle SET
		ModelId=@ModelId,
		InteriorColorId =@InteriorColorId,
		ExteriorColorId=@ExteriorColorId,
		TransmissionTypeId=@TransmissionTypeId,
		BodyStyleId=@BodyStyleId,
		Mileage=@Mileage,
		Year=@Year,
		ListedPrice=@ListedPrice,
		MSRP=@MSRP,
		Description=@Description,
		ImageFileName=@ImageFileName,
		IsFeatured=@IsFeatured,
		IsUsed=@IsUsed,
		IsSold=@IsSold,
		UserEmail=@UserEmail
	WHERE VehicleId=@VehicleId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehicleDelete')
		DROP PROCEDURE VehicleDelete
GO

CREATE PROCEDURE VehicleDelete (
	@VehicleId char(17)
) AS	
BEGIN
	BEGIN TRANSACTION

	DELETE FROM ContactRequests WHERE VehicleId = @VehicleId;
	DELETE FROM Purchases WHERE VehicleId = @VehicleId;
	DELETE FROM Vehicle WHERE VehicleId = @VehicleId;

	COMMIT TRANSACTION
END

GO

--------------------CUSTOMER SPROCS--------------------
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CustomersSelectAll')
		DROP PROCEDURE CustomersSelectAll
GO

CREATE PROCEDURE CustomersSelectAll AS
BEGIN
	SELECT CustomerId, StateAbbreviation, CustomerName, CustomerPhone, CustomerAddress1, CustomerAddress2, City, Zip, CustomerEmail
	FROM Customer
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'CustomerInsert')
		DROP PROCEDURE CustomerInsert
GO

CREATE PROCEDURE CustomerInsert (
@CustomerId int output,
@StateAbbreviation char(2),
@CustomerName varchar(100),
@CustomerPhone char(12),
@CustomerAddress1 varchar(100),
@CustomerAddress2 varchar(50) = null,
@City varchar(50),
@Zip char(50),
@CustomerEmail varchar(50))

AS
BEGIN
	INSERT INTO Customer(StateAbbreviation, CustomerName, CustomerPhone, CustomerAddress1, CustomerAddress2, City, Zip, CustomerEmail)
	VALUES(@StateAbbreviation, @CustomerName,@CustomerPhone,@CustomerAddress1,@CustomerAddress2, @City, @Zip, @CustomerEmail)
	
	SET @CustomerID = SCOPE_IDENTITY();
END
GO

--------------------SPECIALS SPROCS-------------------

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsSelectAll')
		DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN
SELECT SpecialId, SpecialName, SpecialImageFilename, SpecialDescription, s.UserEmail
	FROM Specials s
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'SpecialsInsert')
		DROP PROCEDURE SpecialsInsert
GO

CREATE PROCEDURE SpecialsInsert (
@SpecialId int output,
@SpecialName varchar(50),
@SpecialImageFilename varchar(50) =null,
@SpecialDescription varchar(500),
@UserEmail varchar(50))

AS
BEGIN
	INSERT INTO Specials(SpecialName, SpecialImageFilename, SpecialDescription, UserEmail)
	VALUES(@SpecialName,@SpecialImageFilename, @SpecialDescription, @UserEmail)
	
	SET @SpecialId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialDelete')
		DROP PROCEDURE SpecialDelete
GO

CREATE PROCEDURE SpecialDelete (
	@SpecialId int
) AS	
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Specials WHERE SpecialId = @SpecialId;

	COMMIT TRANSACTION
END

GO

--------------------PURCHASE SPROCS-------------------
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchaseTypeSelectAll')
		DROP PROCEDURE PurchaseTypeSelectAll
GO

CREATE PROCEDURE PurchaseTypeSelectAll AS
BEGIN
	SELECT PurchaseTypeId, PurchaseTypeName
	FROM PurchaseType
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PurchasesSelectAll')
		DROP PROCEDURE PurchasesSelectAll
GO

CREATE PROCEDURE PurchasesSelectAll AS
BEGIN
	SELECT PurchaseId, PurchaseTypeId, VehicleId, CustomerId, PurchaseDate, PurchasePrice, UserEmail
	FROM Purchases
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'PurchaseInsert')
		DROP PROCEDURE PurchaseInsert
GO

CREATE PROCEDURE PurchaseInsert (
@PurchaseId int output,
@PurchaseTypeId int,
@VehicleId char(17),
@CustomerId int,
@PurchasePrice decimal(8,2),
@UserEmail varchar(50))

AS
BEGIN
	INSERT INTO Purchases(PurchaseTypeId, VehicleId, CustomerId, PurchasePrice, UserEmail)
	VALUES(@PurchaseTypeId, @VehicleId,@CustomerId,@PurchasePrice,@UserEmail)
	
	SET @PurchaseId = SCOPE_IDENTITY();
END
GO



--------------------CONTACT REQUEST SPROCS-------------------
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES	
	WHERE ROUTINE_NAME = 'ContactRequestInsert')
		DROP PROCEDURE ContactRequestInsert
GO

CREATE PROCEDURE ContactRequestInsert (
@ContactRequestId int output,
@VehicleId char(17) = null,
@ContactName varchar(50),
@ContactEmail varchar(50) = null,
@ContactPhone char(12) = null,
@ContactMessage varchar(500))

AS
BEGIN
	INSERT INTO ContactRequests (VehicleId, ContactName, ContactEmail, ContactPhone, ContactMessage)
	VALUES(@VehicleId, @ContactName, @ContactEmail, @ContactPhone,@ContactMessage)
	
	SET @ContactRequestId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactRequestsSelectAll')
		DROP PROCEDURE ContactRequestsSelectAll
GO

CREATE PROCEDURE ContactRequestsSelectAll AS
BEGIN
	SELECT ContactRequestId, VehicleId, ContactName, ContactEmail, ContactPhone, ContactMessage, DateContactRequestCreated
	FROM ContactRequests;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'StateSelectAll')
		DROP PROCEDURE StateSelectAll
GO

CREATE PROCEDURE StateSelectAll AS
BEGIN
	SELECT StateAbbreviation, StateName
	FROM State
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'InventoryReport')
		DROP PROCEDURE InventoryReport
GO

CREATE PROCEDURE InventoryReport
AS
BEGIN
	Select
	Year,
	MakeName,
	ModelName,
	Count(ModelName) AS 'Count',
	SUM(MSRP) AS 'StockValue',
	IsUsed AS 'Used'
FROM Vehicle v
JOIN VehicleModel vm ON vm.ModelId = v.ModelId
JOIN Make m ON m.MakeId = vm.MakeId
WHERE IsSold = 0
GROUP BY Year, MakeName, ModelName, IsUsed
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME ='SalesReport')
		DROP PROCEDURE SalesReport
GO

CREATE PROCEDURE SalesReport
@UserEmail varchar(50) = null,
@FromDate DateTime2,
@ToDate DateTime2

AS
BEGIN
IF(@UserEmail IS NOT NULL)
	select
	CONCAT(FirstName, ' ', LastName) AS 'User',
	Sum(PurchasePrice) As 'TotalSales',
	Count(VehicleId) As 'TotalVehicles'
	FROM Purchases p
	JOIN AspNetUsers u on u.Email = p.UserEmail
	WHERE 1=1 AND p.UserEmail LIKE @UserEmail AND p.PurchaseDate Between @FromDate AND @ToDate
	GROUP BY CONCAT(FirstName, ' ', LastName)
	ORDER BY Sum(PurchasePrice) DESC
ELSE
select
	CONCAT(FirstName, ' ', LastName) AS 'User',
	Sum(PurchasePrice) As 'TotalSales',
	Count(VehicleId) As 'TotalVehicles'
	FROM Purchases p
	JOIN AspNetUsers u on u.Email = p.UserEmail
	WHERE 1=1 AND p.PurchaseDate Between @FromDate AND @ToDate
	GROUP BY CONCAT(FirstName, ' ', LastName)
	ORDER BY Sum(PurchasePrice) DESC
END
GO
