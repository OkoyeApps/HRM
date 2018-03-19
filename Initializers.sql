USE [ResourceEdgeDB]
GO

--Users
INSERT INTO [dbo].[AspNetUsers] ([Id], [emprole], [userstatus], [firstname], [lastname], [contactnumber], [businessunitId], [departmentId], [createdby], [modifiedby], [createddate], [modifieddate], [employeeId], [modeofentry], [other_modeofentry], [entrycomments], [selecteddate], [candidatereferredby], [company_id], [JobtitleId], [Isactive], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0007c9b8-7ac8-4c97-8757-e6a3442cc48b', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Tenece2', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'Manager@example.com', 0, N'AI3j1HC85a+4eyniaMN0rDYeotDSn+r3FSLgsJjpbm7b2dMG1pbPnI1/vWOIktcutw==', N'014ab6ce-f245-454d-9d8d-784626757583', NULL, 0, 0, NULL, 0, 0, N'Manager@example.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [emprole], [userstatus], [firstname], [lastname], [contactnumber], [businessunitId], [departmentId], [createdby], [modifiedby], [createddate], [modifieddate], [employeeId], [modeofentry], [other_modeofentry], [entrycomments], [selecteddate], [candidatereferredby], [company_id], [JobtitleId], [Isactive], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'398103f8-2b3a-4921-914a-281a9719efb9', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'Admin@example.com', 0, N'ANVLumEFN6QYGRtyPyrJByvor3QStPNI4nsYAdbOHtT7OohONcudNjeUwBqpChau5w==', N'b1a29d12-186c-4b81-9809-b3f5bd4b2ea9', NULL, 0, 0, NULL, 0, 0, N'Admin@example.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [emprole], [userstatus], [firstname], [lastname], [contactnumber], [businessunitId], [departmentId], [createdby], [modifiedby], [createddate], [modifieddate], [employeeId], [modeofentry], [other_modeofentry], [entrycomments], [selecteddate], [candidatereferredby], [company_id], [JobtitleId], [Isactive], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'42a3d192-abf7-4edc-930f-2da4b2820462', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Tenece1', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'Hr@example.com', 0, N'AMjFBnMG1/5ErcASKkm5PFmgBAWEj3hVYTO4T/gR54tUJ+EKi4VQca/u3LiSGJkHqA==', N'463578d7-d7d6-4fbf-a290-c77d02094547', NULL, 0, 0, NULL, 0, 0, N'Hr@example.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [emprole], [userstatus], [firstname], [lastname], [contactnumber], [businessunitId], [departmentId], [createdby], [modifiedby], [createddate], [modifieddate], [employeeId], [modeofentry], [other_modeofentry], [entrycomments], [selecteddate], [candidatereferredby], [company_id], [JobtitleId], [Isactive], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8dca2079-c74a-4c9e-9bef-15eaa7cfb9b3', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Tenece3', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'DeptHead@example.com', 0, N'AAyMbD5i6p2D25/2C9e8+4ZP2HATfHBEOYAAp5ncT4PLqSiP1qjEMHY2+PPp0a+img==', N'e5f47b48-8d83-4a23-83b9-4262e3babad0', NULL, 0, 0, NULL, 0, 0, N'DeptHead@example.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [emprole], [userstatus], [firstname], [lastname], [contactnumber], [businessunitId], [departmentId], [createdby], [modifiedby], [createddate], [modifieddate], [employeeId], [modeofentry], [other_modeofentry], [entrycomments], [selecteddate], [candidatereferredby], [company_id], [JobtitleId], [Isactive], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ba90a9e9-3c7c-42ba-983a-31d5c06affb0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Tenece0', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'Test1@example.com', 0, N'AP74p+n52EPnpAQWQmbzpNzlDeljZopOR/vF9CaFvrOW6rlLjNnAmqqrltKiZVGUyw==', N'7ef0af93-c595-47a3-8881-624a7c2747c3', NULL, 0, 0, NULL, 0, 0, N'Test1@example.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [emprole], [userstatus], [firstname], [lastname], [contactnumber], [businessunitId], [departmentId], [createdby], [modifiedby], [createddate], [modifieddate], [employeeId], [modeofentry], [other_modeofentry], [entrycomments], [selecteddate], [candidatereferredby], [company_id], [JobtitleId], [Isactive], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e950d1b6-edc3-4acf-9fe2-ae32c0e44897', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Tenece4', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'LocationHead@example.com', 0, N'AJCWOHVh3EQgc56NlCw3zLSkMtGdLH86KmOqKQRIi9vWPnN9x9L/Xr3CrlnAedFzlA==', N'0b98c988-fb7c-4833-8d4b-fe2122f0cb62', NULL, 0, 0, NULL, 0, 0, N'LocationHead@example.com')

--Roles
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4', N'Employee')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5', N'Head HR')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6', N'Head OF Unit')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'HR')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7', N'Location Head')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Management')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Manager')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6e2b3473-4542-4c95-b8d2-bd720eaff4ff', N'System Admin')


--User Roles
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0007c9b8-7ac8-4c97-8757-e6a3442cc48b', N'2')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'398103f8-2b3a-4921-914a-281a9719efb9', N'6e2b3473-4542-4c95-b8d2-bd720eaff4ff')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'42a3d192-abf7-4edc-930f-2da4b2820462', N'3')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ba90a9e9-3c7c-42ba-983a-31d5c06affb0', N'4')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'398103f8-2b3a-4921-914a-281a9719efb9', N'5')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'42a3d192-abf7-4edc-930f-2da4b2820462', N'5')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e950d1b6-edc3-4acf-9fe2-ae32c0e44897', N'7')




--AppraisalModes
SET IDENTITY_INSERT [dbo].[AppraisalMode] ON
INSERT INTO [dbo].[AppraisalMode] ([Id], [Name]) VALUES (1, N'Quarterly')
INSERT INTO [dbo].[AppraisalMode] ([Id], [Name]) VALUES (2, N'Half-Year')
INSERT INTO [dbo].[AppraisalMode] ([Id], [Name]) VALUES (3, N'Yearly')
SET IDENTITY_INSERT [dbo].[AppraisalMode] OFF

--[AppraisalPeriods]
SET IDENTITY_INSERT [dbo].[AppraisalPeriod] ON
INSERT INTO [dbo].[AppraisalPeriod] ([Id], [Name]) VALUES (1, N'Q1')
INSERT INTO [dbo].[AppraisalPeriod] ([Id], [Name]) VALUES (2, N'H1')
INSERT INTO [dbo].[AppraisalPeriod] ([Id], [Name]) VALUES (3, N'Yearly')
SET IDENTITY_INSERT [dbo].[AppraisalPeriod] OFF

--[AppraisalRatings]
SET IDENTITY_INSERT [dbo].[AppraisalRating] ON
INSERT INTO [dbo].[AppraisalRating] ([Id], [Name]) VALUES (1, N'1-6')
INSERT INTO [dbo].[AppraisalRating] ([Id], [Name]) VALUES (2, N'1-11')
INSERT INTO [dbo].[AppraisalRating] ([Id], [Name]) VALUES (3, N'1-4')
SET IDENTITY_INSERT [dbo].[AppraisalRating] OFF


--[AppraisalStatus]
SET IDENTITY_INSERT [dbo].[AppraisalStatus] ON
INSERT INTO [dbo].[AppraisalStatus] ([Id], [Name]) VALUES (1, N'Open')
INSERT INTO [dbo].[AppraisalStatus] ([Id], [Name]) VALUES (2, N'Closed')
INSERT INTO [dbo].[AppraisalStatus] ([Id], [Name]) VALUES (3, N'In-Progress')
SET IDENTITY_INSERT [dbo].[AppraisalStatus] OFF

--[Groups]
SET IDENTITY_INSERT [dbo].[Group] ON
INSERT INTO [dbo].[Group] ([Id], [GroupName], [Descriptions], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES (1, N'Tenece', NULL, N'2018-03-16 16:33:27', NULL, NULL, NULL)
INSERT INTO [dbo].[Group] ([Id], [GroupName], [Descriptions], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES (2, N'Genesys', NULL, N'2018-03-16 16:33:27', NULL, NULL, NULL)
INSERT INTO [dbo].[Group] ([Id], [GroupName], [Descriptions], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy]) VALUES (3, N'Piewa', NULL, N'2018-03-16 16:33:27', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Group] OFF

--[Locations]
SET IDENTITY_INSERT [dbo].[Location] ON
INSERT INTO [dbo].[Location] ([Id], [GroupId], [State], [City], [Country], [Address1], [Address2], [LocationHead1], [LocationHead2], [LocationHead3], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn]) VALUES (1, 1, N'Enugu', N'Enugu', N'Nigeria', N'Centinary City', NULL, N'e950d1b6-edc3-4acf-9fe2-ae32c0e44897', NULL, NULL, NULL, NULL, N'2018-03-16 16:33:27', N'2018-03-16 16:33:27')
INSERT INTO [dbo].[Location] ([Id], [GroupId], [State], [City], [Country], [Address1], [Address2], [LocationHead1], [LocationHead2], [LocationHead3], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn]) VALUES (2, 1, N'Lagos', N'Lagos', N'Nigeria', N'Centinary City', NULL, NULL, NULL, NULL, NULL, NULL, N'2018-03-16 16:33:27', N'2018-03-16 16:33:27')
SET IDENTITY_INSERT [dbo].[Location] OFF

--[IdentityCodes]
SET IDENTITY_INSERT [dbo].[IdentityCode] ON
INSERT INTO [dbo].[IdentityCode] ([Id], [employee_code], [backgroundagency_code], [vendors_code], [staffing_code], [users_code], [requisition_code], [GroupId], [createdBy], [createddate], [modifiedBy], [modifieddate], [Groups_Id]) VALUES (1, N'Tenece', N'Bck', N'Ven', N'TenStf', N'User', N'Req', 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[IdentityCode] OFF

--[BusinessUnits]
SET IDENTITY_INSERT [dbo].[BusinessUnit] ON
INSERT INTO [dbo].[BusinessUnit] ([Id], [GroupId], [unitname], [unitcode], [descriptions], [startdate], [reportManager1], [reportManager2], [LocationId], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (1, 0, N'TestUnit1', N'Test111', N'Tesing Business Unit Description', N'2018-03-16 16:33:28', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[BusinessUnit] ([Id], [GroupId], [unitname], [unitcode], [descriptions], [startdate], [reportManager1], [reportManager2], [LocationId], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (2, 0, N'TestUnit2', N'Test111', N'Tesing Business Unit Description', N'2018-03-16 16:33:28', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[BusinessUnit] ([Id], [GroupId], [unitname], [unitcode], [descriptions], [startdate], [reportManager1], [reportManager2], [LocationId], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (3, 0, N'TestUnit3', N'Test111', N'Tesing Business Unit Description', N'2018-03-16 16:33:28', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[BusinessUnit] OFF

--[Departments]
SET IDENTITY_INSERT [dbo].[Department] ON
INSERT INTO [dbo].[Department] ([ID], [deptname], [deptcode], [descriptions], [startdate], [reportManager1], [reportManager2], [depthead], [BunitId], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (1, N'TestDept', N'Test101', NULL, N'2018-03-16 16:33:28', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Department] ([ID], [deptname], [deptcode], [descriptions], [startdate], [reportManager1], [reportManager2], [depthead], [BunitId], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (2, N'TestDept2', N'Test101', NULL, N'2018-03-16 16:33:28', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Department] ([ID], [deptname], [deptcode], [descriptions], [startdate], [reportManager1], [reportManager2], [depthead], [BunitId], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (3, N'TestDept3', N'Test101', NULL, N'2018-03-16 16:33:28', NULL, NULL, NULL, 2, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Department] OFF

--[Jobtitles]
SET IDENTITY_INSERT [dbo].[Jobtitle] ON
INSERT INTO [dbo].[Jobtitle] ([ID], [GroupId], [jobtitlecode], [jobtitlename], [jobdescription], [minexperiencerequired], [jobpaygradecode], [jobpayfrequency], [comments], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (1, 0, N'JobT', N'TestJob', N'Test Job description', 2, N'A', N'Monthly', N'Testing Job', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Jobtitle] ([ID], [GroupId], [jobtitlecode], [jobtitlename], [jobdescription], [minexperiencerequired], [jobpaygradecode], [jobpayfrequency], [comments], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (2, 0, N'JobT', N'TestJob2', N'Test Job description', 2, N'A', N'Monthly', N'Testing Job', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Jobtitle] ([ID], [GroupId], [jobtitlecode], [jobtitlename], [jobdescription], [minexperiencerequired], [jobpaygradecode], [jobpayfrequency], [comments], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (3, 0, N'JobT', N'TestJob3', N'Test Job description', 2, N'A', N'Monthly', N'Testing Job', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Jobtitle] OFF

--[Levels]
SET IDENTITY_INSERT [dbo].[Level] ON
INSERT INTO [dbo].[Level] ([Id], [GroupId], [levelNo], [LevelName], [EligibleYears], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn]) VALUES (1, 1, 1, N'Beginner', 3, NULL, NULL, N'2018-03-16 16:33:29', N'2018-03-16 16:33:29')
INSERT INTO [dbo].[Level] ([Id], [GroupId], [levelNo], [LevelName], [EligibleYears], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn]) VALUES (2, 1, 2, N'Professional', 7, NULL, NULL, N'2018-03-16 16:33:29', N'2018-03-16 16:33:29')
SET IDENTITY_INSERT [dbo].[Level] OFF

--[Menu]
INSERT INTO [dbo].[Menu] ([Id], [Name], [Role], [Active]) VALUES (1, N'Question', N'Manager,HR,System Admin', 0)
INSERT INTO [dbo].[Menu] ([Id], [Name], [Role], [Active]) VALUES (2, N'EmployeeAppraisal', N'Employee, HR,Manager', 0)

--[MonthList]
SET IDENTITY_INSERT [dbo].[MonthList] ON
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (1, N'1', N'Jan', N'January', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (2, N'2', N'Feb', N'February', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (3, N'3', N'Mar', N'March', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (4, N'4', N'April', N'April', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (5, N'5', N'May', N'May', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (6, N'6', N'June', N'June', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (7, N'7', N'July', N'July', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (8, N'8', N'Aug', N'Aug', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (9, N'9', N'Sep', N'September', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (10, N'10', N'Oct', N'October', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (11, N'11', N'Nov', N'November', N'1', N'1', NULL, NULL, 1)
INSERT INTO [dbo].[MonthList] ([id], [MonthId], [MonthCode], [Description], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (12, N'12', N'Dec', N'December', N'1', N'1', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[MonthList] OFF

--[Month]
SET IDENTITY_INSERT [dbo].[Month] ON
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (1, N'1', N'January', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (2, N'2', N'February', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (3, N'3', N'March', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (4, N'4', N'April', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (5, N'5', N'May', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (6, N'6', N'June', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (7, N'7', N'July', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (8, N'8', N'August', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (9, N'9', N'September', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (10, N'10', N'October', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (11, N'11', N'November', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Month] ([id], [MonthId], [MonthName], [Createdby], [Modifiedby], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (12, N'12', N'December', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Month] OFF

--[Parameters]
SET IDENTITY_INSERT [dbo].[Parameter] ON
INSERT INTO [dbo].[Parameter] ([Id], [ParameterName], [Descriptions], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (1, N'KPI', N'Key Performance Index', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Parameter] OFF

--[Position]
SET IDENTITY_INSERT [dbo].[Position] ON
INSERT INTO [dbo].[Position] ([ID], [positionname], [jobtitleid], [description], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (1, N'TestPosi', 1, NULL, NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Position] ([ID], [positionname], [jobtitleid], [description], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (2, N'TestPosi', 1, NULL, NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Position] ([ID], [positionname], [jobtitleid], [description], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (3, N'TestPosi', 2, NULL, NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Position] ([ID], [positionname], [jobtitleid], [description], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (4, N'TestPosi', 2, NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Position] OFF

--[Prefix]
SET IDENTITY_INSERT [dbo].[Prefix] ON
INSERT INTO [dbo].[Prefix] ([ID], [prefixName], [description], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (1, N'Mr', NULL, NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Prefix] ([ID], [prefixName], [description], [createdby], [modifiedby], [createddate], [modifieddate], [isactive]) VALUES (2, N'Mrs', NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Prefix] OFF

--[ReportManager]
INSERT INTO [dbo].[ReportManager] ([managerId], [DepartmentId], [BusinessUnitId], [employeeId], [FullName]) VALUES (N'0007c9b8-7ac8-4c97-8757-e6a3442cc48b', 2, 1, 3, N'Test Manager')


--[WeekDays]
SET IDENTITY_INSERT [dbo].[WeekDay] ON
INSERT INTO [dbo].[WeekDay] ([id], [DayName], [DayShortCode], [DayLongCode], [description], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (1, N'1', N'Mo', N'Mon', N'Monday', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[WeekDay] ([id], [DayName], [DayShortCode], [DayLongCode], [description], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (2, N'2', N'Tu', N'Tue', N'Tueday', N'1', NULL, NULL, NULL, 1)
INSERT INTO [dbo].[WeekDay] ([id], [DayName], [DayShortCode], [DayLongCode], [description], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (3, N'3', N'We', N'Wed', N'Wednesday', N'1', NULL, NULL, NULL, 1)
INSERT INTO [dbo].[WeekDay] ([id], [DayName], [DayShortCode], [DayLongCode], [description], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (4, N'4', N'Th', N'Thu', N'Thursday', N'1', NULL, NULL, NULL, 1)
INSERT INTO [dbo].[WeekDay] ([id], [DayName], [DayShortCode], [DayLongCode], [description], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (5, N'5', N'Fr', N'Fri', N'Friday', N'1', NULL, NULL, NULL, 1)
INSERT INTO [dbo].[WeekDay] ([id], [DayName], [DayShortCode], [DayLongCode], [description], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (6, N'6', N'Sa', N'Sat', N'Saturday', N'1', NULL, NULL, NULL, 1)
INSERT INTO [dbo].[WeekDay] ([id], [DayName], [DayShortCode], [DayLongCode], [description], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (7, N'7', N'Su', N'Sun', N'Sunday', N'1', NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[WeekDay] OFF

--[Weeks]
SET IDENTITY_INSERT [dbo].[Week] ON
INSERT INTO [dbo].[Week] ([id], [WeekId], [WeekName], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (1, N'1', N'Sunday', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Week] ([id], [WeekId], [WeekName], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (2, N'2', N'Monday', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Week] ([id], [WeekId], [WeekName], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (3, N'3', N'Tuesday', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Week] ([id], [WeekId], [WeekName], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (4, N'4', N'Wednesday', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Week] ([id], [WeekId], [WeekName], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (5, N'5', N'Thursday', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Week] ([id], [WeekId], [WeekName], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (6, N'6', N'Friday', NULL, NULL, NULL, NULL, 1)
INSERT INTO [dbo].[Week] ([id], [WeekId], [WeekName], [CreatedBy], [ModifiedBy], [CreatedDate], [ModifiedDate], [Isactive]) VALUES (7, N'7', N'Saturday', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Week] OFF

--[Employees]
SET IDENTITY_INSERT [dbo].[Employee] ON
INSERT INTO [dbo].[Employee] ([ID], [userId], [empEmail], [empRoleId], [GroupId], [FullName], [PhoneNumber], [dateOfJoining], [dateOfLeaving], [reportingManager1], [reportingManager2], [empStatusId], [businessunitId], [departmentId], [jobtitleId], [positionId], [yearsExp], [LevelId], [LocationId], [prefixId], [officeNumber], [createdby], [modifiedby], [createddate], [modifieddate], [isactive], [isOrghead], [modeofEmployement], [IsUnithead], [IsDepthead], [Departments_DeptId], [BusinessUnits_Id]) VALUES (1, N'ba90a9e9-3c7c-42ba-983a-31d5c06affb0', N'Test1@example.com', 4, 1, N'Test User', NULL, NULL, NULL, NULL, NULL, N'Test User', 1, 2, 1, 1, NULL, 1, 1, 0, NULL, NULL, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Employee] ([ID], [userId], [empEmail], [empRoleId], [GroupId], [FullName], [PhoneNumber], [dateOfJoining], [dateOfLeaving], [reportingManager1], [reportingManager2], [empStatusId], [businessunitId], [departmentId], [jobtitleId], [positionId], [yearsExp], [LevelId], [LocationId], [prefixId], [officeNumber], [createdby], [modifiedby], [createddate], [modifieddate], [isactive], [isOrghead], [modeofEmployement], [IsUnithead], [IsDepthead], [Departments_DeptId], [BusinessUnits_Id]) VALUES (2, N'42a3d192-abf7-4edc-930f-2da4b2820462', N'Hr@example.com', 3, 1, N'Test HR', NULL, NULL, NULL, NULL, NULL, N'Test Hr', 1, 2, 1, 1, NULL, 1, 1, 0, NULL, NULL, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Employee] ([ID], [userId], [empEmail], [empRoleId], [GroupId], [FullName], [PhoneNumber], [dateOfJoining], [dateOfLeaving], [reportingManager1], [reportingManager2], [empStatusId], [businessunitId], [departmentId], [jobtitleId], [positionId], [yearsExp], [LevelId], [LocationId], [prefixId], [officeNumber], [createdby], [modifiedby], [createddate], [modifieddate], [isactive], [isOrghead], [modeofEmployement], [IsUnithead], [IsDepthead], [Departments_DeptId], [BusinessUnits_Id]) VALUES (3, N'0007c9b8-7ac8-4c97-8757-e6a3442cc48b', N'Manager@example.com', 2, 1, N'Test Manager', NULL, NULL, NULL, NULL, NULL, N'Test User', 1, 2, 1, 1, NULL, 1, 1, 0, NULL, NULL, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Employee] ([ID], [userId], [empEmail], [empRoleId], [GroupId], [FullName], [PhoneNumber], [dateOfJoining], [dateOfLeaving], [reportingManager1], [reportingManager2], [empStatusId], [businessunitId], [departmentId], [jobtitleId], [positionId], [yearsExp], [LevelId], [LocationId], [prefixId], [officeNumber], [createdby], [modifiedby], [createddate], [modifieddate], [isactive], [isOrghead], [modeofEmployement], [IsUnithead], [IsDepthead], [Departments_DeptId], [BusinessUnits_Id]) VALUES (4, N'8dca2079-c74a-4c9e-9bef-15eaa7cfb9b3', N'DeptHead@example.com', 2, 1, N'Test Dept', NULL, NULL, NULL, NULL, NULL, N'Test User', 1, 2, 1, 1, NULL, 1, 1, 0, NULL, NULL, NULL, NULL, NULL, 1, NULL, 0, NULL, 1, NULL, NULL)
INSERT INTO [dbo].[Employee] ([ID], [userId], [empEmail], [empRoleId], [GroupId], [FullName], [PhoneNumber], [dateOfJoining], [dateOfLeaving], [reportingManager1], [reportingManager2], [empStatusId], [businessunitId], [departmentId], [jobtitleId], [positionId], [yearsExp], [LevelId], [LocationId], [prefixId], [officeNumber], [createdby], [modifiedby], [createddate], [modifieddate], [isactive], [isOrghead], [modeofEmployement], [IsUnithead], [IsDepthead], [Departments_DeptId], [BusinessUnits_Id]) VALUES (5, N'e950d1b6-edc3-4acf-9fe2-ae32c0e44897', N'LocationHead@example.com', 7, 1, N'Test Location Head', NULL, NULL, NULL, NULL, NULL, N'Test location', 1, 2, 1, 1, NULL, 1, 1, 0, NULL, NULL, NULL, NULL, NULL, 1, NULL, 0, NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employee] OFF

