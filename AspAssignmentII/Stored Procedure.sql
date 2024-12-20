select * from [dbo].[People_Details]
go
ALTER PROCEDURE spInsertPeople
(@username varchar(50)
,@name varchar(50)
,@gender varchar(10)
,@address varchar(70)
,@result int output)
AS
BEGIN
	SELECT [name] FROM [dbo].[People_Details] where [username]=@username
	IF @@ROWCOUNT>0
	BEGIN
		SET @result=0
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[People_Details]
           ([username],[name],[gender],[address])
		VALUES
           (@username, @name,@gender,@address)
		SET @result=1
	END
END
go
ALTER PROCEDURE spSearchPerson
(@name varchar(50)=null
,@id int=null)
AS
BEGIN
	IF @id=-1
	BEGIN
		SELECT * FROM [dbo].[People_Details] where [name]=@name
	END
	ELSE
	BEGIN
		SELECT * FROM [dbo].[People_Details] where [id]=@id
	END
END
GO
ALTER PROCEDURE spShowPeople
AS
BEGIN
		SELECT * FROM [dbo].[People_Details]
END
GO
EXEC spShowPeople
EXEC spSearchPerson 'John Doe',7
DECLARE @result1 INT
EXEC spInsertPeople 'RiyaWaves','Riya Biswas','Female','108 Kachrapara Main road',@result1 output

go

CREATE TABLE BankUser (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(100) NOT NULL,
    AccNo INT UNIQUE NOT NULL,
    Balance DECIMAL(18, 2) DEFAULT 0
);
GO
ALTER PROCEDURE GetBalance
    @Username VARCHAR(50)
AS
BEGIN
    SELECT Balance FROM BankUser WHERE Username = @Username;
END
GO

CREATE PROCEDURE TransferMoney
    @SenderUsername VARCHAR(50), @ReceiverUsername VARCHAR(50),
	@Amount DECIMAL(18, 2),@MESSAGE VARCHAR(50) OUTPUT
AS
BEGIN
    DECLARE @SenderBalance DECIMAL(18, 2),@ReceiverBalance DECIMAL(18, 2);
    SELECT * FROM BankUser WHERE Username = @ReceiverUsername;
    IF @@ROWCOUNT = 0
    BEGIN
        SET @MESSAGE='Receiver does not exist';
        RETURN;
    END
    SELECT @SenderBalance = Balance FROM BankUser WHERE Username = @SenderUsername;
    IF @SenderBalance < @Amount
    BEGIN
        SET @MESSAGE='Insufficient balance in sender''s account';
        RETURN;
    END
    BEGIN TRANSACTION;
    UPDATE BankUser SET Balance = Balance - @Amount WHERE Username = @SenderUsername;
    UPDATE BankUser SET Balance = Balance + @Amount WHERE Username = @ReceiverUsername; 
    COMMIT TRANSACTION; 
    SET @MESSAGE='Money transfer successful' ;
END
GO
ALTER PROCEDURE SpLoginBank
    @Username VARCHAR(50),@Password VARCHAR(100),@MESSAGE VARCHAR(50) OUTPUT
AS
BEGIN
SELECT * FROM BankUser WHERE Username=@Username and Password= @Password
	IF(@@ROWCOUNT>0)
	BEGIN
		SET @MESSAGE='LogIn successful'
	END
	ELSE
	BEGIN
		SET @MESSAGE='LogIn unsuccessful'
	END
END
go

CREATE PROCEDURE SpSignUpBank
    @Name VARCHAR(100),@Username VARCHAR(50),
    @Password VARCHAR(100),@AccNo INT,
    @Balance DECIMAL(18, 2),@MESSAGE VARCHAR(50) OUTPUT
AS
BEGIN
SELECT * FROM BankUser WHERE Username=@Username and Password= @Password
	IF(@@ROWCOUNT<0)
	BEGIN
		SET @MESSAGE='User already exists'
	END
	ELSE
	BEGIN
		INSERT INTO BankUser VALUES (@Name,@Username,@Password,@AccNo,@Balance)
		SET @MESSAGE='Signup successful'
	END
END

SELECT * FROM BankUser