use [PostOffice_Web]

drop table if exists Subscriptions
drop table if exists Subscribers
drop table if exists Addresses
drop table if exists Selections
drop table if exists People
drop table if exists UserAccounts
drop table if exists Publications
drop table if exists TypesOfPublications
drop table if exists Roles

-- Справочная таблица ролей.
CREATE TABLE dbo.Roles
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Name] NVARCHAR(20) NOT NULL
)

CREATE TABLE dbo.UserAccounts
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Login] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL
)

-- Таблица людей.
CREATE TABLE dbo.People
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[PersonName]       NVARCHAR(20) NOT NULL,
	[PersonSurname]    NVARCHAR(20) NOT NULL,
	[PersonPatronymic] NVARCHAR(20) NOT NULL,
	RoleId INT NOT NULL,
	UserAccountId INT NOT NULL,

	constraint FK_People_RoleId foreign key (RoleId) references dbo.Roles,
	constraint FK_People_UserAccountId foreign key (UserAccountId) references dbo.UserAccounts,
)

-- Таблица участков, которые обслуживаются определённым почтальоном.
CREATE TABLE dbo.Selections
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	PostmanId INT NOT NULL,

	constraint FK_Selections_PostmanId foreign key (PostmanId) references dbo.People
)

-- Справочная таблица адресов.
CREATE TABLE dbo.Addresses
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Address] NVARCHAR(20) NOT NULL,
	SelectionId int NOT NULL

	constraint FK_Addresses_SelectionId foreign key (SelectionId) references dbo.Selections
)

-- Справочная таблица типов изданий.
CREATE TABLE dbo.TypesOfPublications
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Name] NVARCHAR(20) NOT NULL
)

-- Таблица изданий.
CREATE TABLE dbo.Publications
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Name] NVARCHAR(20) NOT NULL,
	Cost INT NOT NULL,
	TypeOfPublicationId INT NOT NULL

	constraint FK_Publications_TypeOfPublicationId foreign key (TypeOfPublicationId) references dbo.TypesOfPublications
)


-- Таблица подписчиков на издания.
CREATE TABLE dbo.Subscribers
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	PersonId INT NOT NULL,
	AddressId INT NOT NULL,

	constraint FK_Subscribers_PersonID foreign key (PersonId) references dbo.People,
	constraint FK_Subscribers_AddressId foreign key (AddressId) references dbo.Addresses,
)


-- Таблица подписок на издания.
CREATE TABLE dbo.Subscriptions
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	SubscriptionStartDate date NOT NULL,
	PublicationId INT NOT NULL,
	SubscriberId INT NOT NULL,

	constraint FK_Subscriptions_PublicationId foreign key (PublicationId) references dbo.Publications,
	constraint FK_Subscriptions_SubscriberId foreign key (SubscriberId) references dbo.Subscribers
)