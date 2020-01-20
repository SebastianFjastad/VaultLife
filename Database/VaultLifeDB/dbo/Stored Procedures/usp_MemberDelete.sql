﻿
CREATE PROC [dbo].[usp_MemberDelete] @MemberID INT
AS
SET NOCOUNT ON
SET XACT_ABORT ON

BEGIN TRAN

DELETE
FROM [dbo].[Member]
WHERE [MemberID] = @MemberID

COMMIT
