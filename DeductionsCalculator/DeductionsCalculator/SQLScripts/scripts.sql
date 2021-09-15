--scripts
-------------------
--CREATE DATABASE [CodeChallenge]

--SELECT Name from sys.Databases

USE [CodeChallenge]
GO

------------------------------------------------------------------------------------------------------------
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employee] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Employee] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Employee] ADD  DEFAULT (sysdatetimeoffset()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Employee] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdatedAt]
GO


------------------------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Dependent](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Dependent] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Dependent] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Dependent] ADD  DEFAULT (sysdatetimeoffset()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Dependent] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdatedAt]
GO

ALTER TABLE [dbo].[Dependent]  WITH CHECK ADD  CONSTRAINT [FK_Dependent_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[Dependent] CHECK CONSTRAINT [FK_Dependent_Employee]
GO



---------------------------------------------------------------------------------------

CREATE TABLE [dbo].[BenefitsPackage](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[YearlyEmployeeCost] [decimal](19, 4) NOT NULL,
	[YearlyDependentCost] [decimal](19, 4) NOT NULL,
	[DiscountInitial] [nvarchar](1) NULL,
	[DiscountInitialPercentage] [decimal](5, 4) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[UpdatedAt] [datetimeoffset](7) NOT NULL,
	[IsDefault] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BenefitsPackage] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[BenefitsPackage] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[BenefitsPackage] ADD  DEFAULT (sysdatetimeoffset()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[BenefitsPackage] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdatedAt]
GO



--insert data given from challenge prompt

INSERT into BenefitsPackage (Name, YearlyEmployeeCost, YearlyDependentCost, DiscountInitial, DiscountInitialPercentage)
VALUES ('Default Package', 1000.00, 500.00, 'A', .1)