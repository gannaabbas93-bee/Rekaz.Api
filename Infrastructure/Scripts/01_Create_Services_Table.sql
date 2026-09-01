-- SQL Script: Create Services Table and Seed Initial Data for Rekaz

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Services')
BEGIN
    CREATE TABLE [dbo].[Services] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [NameAr] NVARCHAR(100) NOT NULL,
        [NameEn] NVARCHAR(100) NOT NULL,
        [Icon] NVARCHAR(50) NOT NULL CONSTRAINT [DF_Services_Icon] DEFAULT '',
        [DescriptionAr] NVARCHAR(500) NOT NULL CONSTRAINT [DF_Services_DescriptionAr] DEFAULT '',
        [IsActive] BIT NOT NULL CONSTRAINT [DF_Services_IsActive] DEFAULT 1,
        [CreatedAt] DATETIME2 NOT NULL CONSTRAINT [DF_Services_CreatedAt] DEFAULT GETUTCDATE(),

        CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    -- Seed initial services matching Rekaz domain requirements
    INSERT INTO [dbo].[Services] ([NameAr], [NameEn], [Icon], [DescriptionAr], [IsActive])
    VALUES 
    (N'إدارة الحجوزات', N'Bookings Management', N'📅', N'حجز وإدارة الجلسات والخدمات بسهولة وسلاسة', 1),
    (N'إدارة الاشتراكات', N'Memberships Management', N'💳', N'تتبع خطط الاشتراكات والعضويات والتجديد التلقائي', 1),
    (N'تقارير وأداء', N'Reports & Analytics', N'📊', N'تحليلات دقيقة وإحصائيات مباشرة لمتابعة أداء العمل', 1),
    (N'دعم الدفع الإلكتروني', N'Online Payments Support', N'⚡', N'ربط كامل مع بوابات الدفع الإلكتروني الآمنة', 1);
END;
