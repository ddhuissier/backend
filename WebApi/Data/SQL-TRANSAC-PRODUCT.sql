BEGIN TRY
	BEGIN TRAN addProd
	INSERT INTO PRODUCTS (Name,Description,Price) VALUES ('New product', 'New description', 152.63)
	COMMIT TRAN addProd
END TRY
BEGIN CATCH
	ROLLBACK TRAN addProd
	PRINT 'Error: Update product item.'
	SELECT  
        ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage;  
END CATCH