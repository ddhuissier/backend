CREATE PROC spGetProduct
(
		@ID INT
)
AS
BEGIN
  SELECT * FROM PRODUCTS 
  WHERE Id = @ID

END