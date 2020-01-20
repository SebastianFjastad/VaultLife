CREATE TABLE [dbo].[Imagedetails](
 [ImageID] [int] IDENTITY(1,1) NOT NULL,
 [ImageName] [varchar](100) NOT NULL,
 [ImageContent] [image] NOT NULL,
 [ProductId] int not null,
 [ContentType] [varchar](100) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE Imagedetails
ADD FOREIGN KEY (ProductID)
REFERENCES Product(ProductID)

ALTER TABLE Imagedetails
ADD PRIMARY KEY (ImageID)