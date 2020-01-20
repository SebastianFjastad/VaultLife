USE [VaultLifeApplication]
GO

/****** Object:  View [dbo].[getRecentSales]    Script Date: 2014-11-19 11:07:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[getRecentSales]
AS
SELECT TOP (5) g.GameID
	,g.GameName
	,g.GameDescription
	,dbo.GameRule.ExcecuteTime
	,dbo.ProductInGame.PriceInGame
	,dbo.ProductInGame.ReferencePrice
	,dbo.Imagedetails.ImageID
FROM dbo.Game AS g
INNER JOIN dbo.GameRule ON g.GameID = dbo.GameRule.GameID
INNER JOIN dbo.ProductInGame ON g.GameID = dbo.ProductInGame.GameID
INNER JOIN dbo.Imagedetails ON dbo.ProductInGame.ProductID = dbo.Imagedetails.ProductId
WHERE (dbo.GameRule.GameRuleCode = 'RESOLVEPOTENTIALWINNERS')

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[55] 4[6] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "g"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 298
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "GameRule"
            Begin Extent = 
               Top = 93
               Left = 298
               Bottom = 307
               Right = 508
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ProductInGame"
            Begin Extent = 
               Top = 0
               Left = 706
               Bottom = 234
               Right = 889
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Imagedetails"
            Begin Extent = 
               Top = 164
               Left = 523
               Bottom = 294
               Right = 693
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1950
         Width = 2985
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      En' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'getRecentSales'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'd
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'getRecentSales'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'getRecentSales'
GO

