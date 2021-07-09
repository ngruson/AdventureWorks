IF NOT EXISTS (SELECT TOP 1 1 FROM Territory)
BEGIN
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'Northwest', N'US', N'North America')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'Northeast', N'US', N'North America')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'Central', N'US', N'North America')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'Southwest', N'US', N'North America')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'Southeast', N'US', N'North America')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'Canada', N'CA', N'North America')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'France', N'FR', N'Europe')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'Germany', N'DE', N'Europe')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'Australia', N'AU', N'Pacific')
    INSERT Territory ([Name], [CountryRegionCode], [Group]) VALUES (N'United Kingdom', N'GB', N'Europe')
END