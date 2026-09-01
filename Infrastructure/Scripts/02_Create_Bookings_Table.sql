-- SQL Script: Create Bookings Table for Rekaz

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bookings')
BEGIN
    CREATE TABLE [dbo].[Bookings] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [FullName] NVARCHAR(150) NOT NULL,
        [BusinessType] NVARCHAR(100) NOT NULL,
        [CountryCode] NVARCHAR(10) NOT NULL,
        [Phone] NVARCHAR(20) NOT NULL,
        [ServiceId] INT NOT NULL,
        [BookingDate] NVARCHAR(20) NOT NULL,
        [SelectedSlot] NVARCHAR(20) NOT NULL,
        [CreatedAt] DATETIME2 NOT NULL CONSTRAINT [DF_Bookings_CreatedAt] DEFAULT GETUTCDATE(),

        CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Bookings_Services] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id])
    );
END;
