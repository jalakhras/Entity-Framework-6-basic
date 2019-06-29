CREATE PROCEDURE GetOldNinjas
	
AS
	SELECT * From Ninjas where DateOfBirth<='03/12/1994'
RETURN 0
