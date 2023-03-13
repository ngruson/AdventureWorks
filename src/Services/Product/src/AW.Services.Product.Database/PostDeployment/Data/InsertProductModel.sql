IF NOT EXISTS (SELECT TOP 1 1 FROM ProductModel)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table ProductModel...'

	SET IDENTITY_INSERT [ProductModel] ON

    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (1, N'Classic Vest', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (2, N'Cycling Cap', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (3, N'Full-Finger Gloves', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (4, N'Half-Finger Gloves', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (5, N'HL Mountain Frame', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (6, N'HL Road Frame', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (7, N'HL Touring Frame', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesFR-210B Instructions for Manufacturing HL Touring Frame. Summary: This document contains manufacturing instructions for manufacturing the HL Touring Frame, Product Model 7. Instructions are work center specific and are identified by Work Center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="2.5" LotSize="100" MachineHours="3" SetupHours="0.5" LocationID="10">
                        Work Center 10 - Frame Forming. The following instructions pertain to Work Center 10. (Setup hours = .5, Labor Hours = 2.5, Machine Hours = 3, Lot Sizing = 100)<step>
                            Insert <material>aluminum sheet MS-2341</material> into the <tool>T-85A framing tool</tool>.
                        </step><step>
                            Attach <tool>Trim Jig TJ-26</tool> to the upper and lower right corners of the aluminum sheet.
                        </step><step>
                            Using a <tool>router with a carbide tip 15</tool>, route the aluminum sheet following the jig carefully.
                        </step><step>
                            Insert the frame into <tool>Forming Tool FT-15</tool> and press Start.
                        </step><step>
                            When finished, inspect the forms for defects per Inspection Specification <specs>INFS-111</specs>.
                        </step><step>Remove the frames from the tool and place them in the Completed or Rejected bin as appropriate.</step></Location><Location LaborHours="1.75" LotSize="1" MachineHours="2" SetupHours="0.15" LocationID="20">
                        Work Center 20 - Frame Welding. The following instructions pertain to Work Center 20. (Setup hours = .15, Labor Hours = 1.75, Machine Hours = 2, Lot Sizing = 1)<step>
                            Assemble all frame components following blueprint <blueprint>1299</blueprint>.
                        </step><step>
                            Weld all frame components together as shown in illustration <diag>3</diag></step><step>
                            Inspect all weld joints per Adventure Works Cycles Inspection Specification <specs>INFS-208</specs>.
                        </step></Location><Location LaborHours="1" LotSize="1" LocationID="30">
                        Work Center 30 - Debur and Polish. The following instructions pertain to Work Center 30. (Setup hours = 0, Labor Hours = 1, Machine Hours = 0, Lot Sizing = 1)<step>
                            Using the <tool>standard debur tool</tool>, remove all excess material from weld areas.
                        </step><step>
                            Using <material>Acme Polish Cream</material>, polish all weld areas.
                        </step></Location><Location LaborHours="0.5" LotSize="20" MachineHours="0.65" LocationID="45">
                        Work Center 45 - Specialized Paint. The following instructions pertain to Work Center 45. (Setup hours = 0, Labor Hours = .5, Machine Hours = .65, Lot Sizing = 20)<step>
                            Attach <material>a maximum of 20 frames</material> to <tool>paint harness</tool> ensuring frames are not touching.
                        </step><step>
                            Mix <material>primer PA-529S</material>. Test spray pattern on sample area and correct flow and pattern as required per engineering spec <specs>AWC-501</specs>.
                        </step><step>Apply thin coat of primer to all surfaces.  </step><step>After 30 minutes, touch test for dryness. If dry to touch, lightly sand all surfaces. Remove all surface debris with compressed air. </step><step>
                            Mix <material>paint</material> per manufacturer instructions.
                        </step><step>
                            Test spray pattern on sample area and correct flow and pattern as required per engineering spec <specs>AWC-509</specs>.
                        </step><step>Apply thin coat of paint to all surfaces. </step><step>After 60 minutes, touch test for dryness. If dry to touch, reapply second coat. </step><step>
                            Allow paint to cure for 24 hours and inspect per <specs>AWC-5015</specs>.
                        </step></Location><Location LaborHours="3" LotSize="1" SetupHours="0.25" LocationID="50">
                        Work Center 50 - SubAssembly. The following instructions pertain to Work Center 50. (Setup hours = .25, Labor Hours = 3, Machine Hours = 0, Lot Sizing = 1)<step>Add Seat Assembly. </step><step>Add Brake assembly.   </step><step>Add Wheel Assembly. </step><step>Inspect Front Derailleur. </step><step>Inspect Rear Derailleur. </step></Location><Location LaborHours="4" LotSize="1" LocationID="60">
                        Work Center 60 - Final Assembly. The following instructions pertain to Work Center 60. (Setup hours = 0, Labor Hours = 4, Machine Hours = 0, Lot Sizing = 1)<step>
                            Perform final inspection per engineering specification <specs>AWC-915</specs>.
                        </step><step>Complete all required certification forms.</step><step>Move to shipping.</step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (8, N'LL Mountain Frame', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (9, N'LL Road Frame', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (10, N'LL Touring Frame', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesFR-200A Instructions for Manufacturing LL Touring Frame Summary: This document contains manufacturing instructions for manufacturing the LL Touring Frame, Product Model 10. Instructions are work center specific and are identified by work center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="2" LotSize="100" MachineHours="2" SetupHours="0.1" LocationID="10">
                        Work Center 10 - Frame Forming. The following instructions pertain to Work Center 10. (Setup hours = .10, Labor Hours = 2, Machine Hours = 2, Lot Sizing = 100)<step>
                            Insert <material>aluminum sheet MS-6061</material> into tool <tool>T-99 framing tool</tool>.
                        </step><step>
                            Attach <tool>Trim Jig TJ-25</tool> to the upper and lower right corners of the aluminum sheet.
                        </step><step>
                            Using a <tool>router with a carbide tip 26</tool>, route the aluminum sheet following the jig carefully.
                        </step><step>
                            Insert the frame into <tool>Forming Tool FT-25</tool> and press Start.
                        </step><step>
                            When finished, inspect the form for defects per <specs>Inspection Specification INFS-110</specs>.
                        </step><step>Remove the frame from the tool and place it in the Completed or Rejected bin as appropriate.</step></Location><Location LaborHours="1.5" LotSize="1" MachineHours="1.75" SetupHours="0.25" LocationID="20">
                        Work Center 20 - Frame Welding. The following instructions pertain to Work Center 20. (Setup hours = .25, Labor Hours = 1.5, Machine Hours = 1.75, Lot Sizing = 1)<step>
                            Assemble all frame components following blueprint <blueprint>12345</blueprint>.
                        </step><step>
                            Weld all frame components together as shown in illustration <diag>3</diag></step><step>
                            Inspect all weld joints per Adventure Works Cycles Inspection Specification <specs>INFS-206</specs>.
                        </step></Location><Location LaborHours="1" LotSize="1" LocationID="30">
                        Work Center 30 - Debur and Polish. The following instructions pertain to Work Center 30. (Setup hours = 0, Labor Hours = 1, Machine Hours = 0, Lot Sizing = 1)<step>
                            Using the <tool>standard debur tool</tool>, remove all excess material from weld areas.
                        </step><step>
                            Using <material>Acme Polish Cream</material>, polish all weld areas.
                        </step></Location><Location LaborHours="1.5" LotSize="20" LocationID="4">
                        Work Center 40 - Paint. The following instructions pertain to Work Center 40. (Setup hours = 0, Labor Hours = 1.5, Machine Hours = 0, Lot Sizing = 20)<step>
                            Attach a <material>maximum of 20 frames</material> to paint harness ensuring frames are not touching.
                        </step><step>
                            Mix <material>primer PA-345</material>. Test spray pattern on sample area and correct flow and pattern as required per engineering spec <specs>AWC-502</specs>.
                        </step><step>Apply thin coat of primer to all surfaces.  </step><step>
                            After 30 minutes, touch test for dryness. If dry to touch, lightly sand all surfaces. Remove all surface debris with <material>compressed air</material>.
                        </step><step>
                            Mix <material>paint</material> per manufacturer instructions.
                        </step><step>
                            Test spray pattern on sample area and correct flow and pattern as required per engineering specification<specs>AWC-503</specs>.
                        </step><step>Apply thin coat of paint to all surfaces. </step><step>After 60 minutes, touch test for dryness. If dry to touch, reapply second coat. </step><step>
                            Allow paint to cure for 24 hours and inspect per <specs>AWC-5010</specs>.
                        </step></Location><Location LaborHours="3" LotSize="1" SetupHours="0.25" LocationID="50">
                        Work Center 50 - SubAssembly. The following instructions pertain to Work Center 50. (Setup hours = .25, Labor Hours = 3, Machine Hours = 0, Lot Sizing = 1)<step>
                            Add <material>Seat Assembly</material>.
                        </step><step>
                            Add <material>Brake assembly</material>.
                        </step><step>
                            Add <material>Wheel Assembly</material>.
                        </step><step>Inspect Front Derailleur. </step><step>Inspect Rear Derailleur.</step></Location><Location LaborHours="4" LotSize="1" LocationID="60">
                        Work Center 60 - Final Assembly. The following instructions pertain to Work Center 60. (Setup hours = 0, Labor Hours = 4, Machine Hours = 0, Lot Sizing = 1)<step>
                            Perform final inspection per engineering specification <specs>AWC-910</specs>.
                        </step><step>Complete all required certification forms.</step><step>Move to shipping.</step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (11, N'Long-Sleeve Logo Jersey', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (12, N'Men''s Bib-Shorts', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (13, N'Men''s Sports Shorts', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (14, N'ML Mountain Frame', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (15, N'ML Mountain Frame-W', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (16, N'ML Road Frame', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (17, N'ML Road Frame-W', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (18, N'Mountain Bike Socks', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (19, N'Mountain-100', N'<?xml-stylesheet href="ProductDescription.xsl" type="text/xsl"?><p1:ProductDescription xmlns:p1="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription" xmlns:wm="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain" xmlns:wf="http://www.adventure-works.com/schemas/OtherFeatures" xmlns:html="http://www.w3.org/1999/xhtml" ProductModelID="19" ProductModelName="Mountain 100"><p1:Summary><html:p>Our top-of-the-line competition mountain bike. 
 				    Performance-enhancing options include the innovative HL Frame, 
				    super-smooth front suspension, and traction for all terrain.
                            </html:p></p1:Summary><p1:Manufacturer><p1:Name>AdventureWorks</p1:Name><p1:Copyright>2002</p1:Copyright><p1:ProductURL>HTTP://www.Adventure-works.com</p1:ProductURL></p1:Manufacturer><p1:Features>These are the product highlights. 
                     <wm:Warranty><wm:WarrantyPeriod>3 years</wm:WarrantyPeriod><wm:Description>parts and labor</wm:Description></wm:Warranty><wm:Maintenance><wm:NoOfYears>10 years</wm:NoOfYears><wm:Description>maintenance contract available through your dealer or any AdventureWorks retail store.</wm:Description></wm:Maintenance><wf:wheel>High performance wheels.</wf:wheel><wf:saddle><html:i>Anatomic design</html:i> and made from durable leather for a full-day of riding in comfort.</wf:saddle><wf:pedal><html:b>Top-of-the-line</html:b> clipless pedals with adjustable tension.</wf:pedal><wf:BikeFrame>Each frame is hand-crafted in our Bothell facility to the optimum diameter 
				    and wall-thickness required of a premium mountain frame. 
				    The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.</wf:BikeFrame><wf:crankset> Triple crankset; alumunim crank arm; flawless shifting. </wf:crankset></p1:Features><!-- add one or more of these elements... one for each specific product in this product model --><p1:Picture><p1:Angle>front</p1:Angle><p1:Size>small</p1:Size><p1:ProductPhotoID>118</p1:ProductPhotoID></p1:Picture><!-- add any tags in <specifications> --><p1:Specifications> These are the product specifications.
                       <Material>Almuminum Alloy</Material><Color>Available in most colors</Color><ProductLine>Mountain bike</ProductLine><Style>Unisex</Style><RiderExperience>Advanced to Professional riders</RiderExperience></p1:Specifications></p1:ProductDescription>', NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (20, N'Mountain-200', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (21, N'Mountain-300', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (22, N'Mountain-400-W', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (23, N'Mountain-500', N'<?xml-stylesheet href="ProductDescription.xsl" type="text/xsl"?><p1:ProductDescription xmlns:p1="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription" xmlns:wm="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain" xmlns:wf="http://www.adventure-works.com/schemas/OtherFeatures" xmlns:html="http://www.w3.org/1999/xhtml" ProductModelID="23" ProductModelName="Mountain-500"><p1:Summary><html:p>Suitable for any type of riding, on or off-road. 
			    Fits any budget. Smooth-shifting with a comfortable ride.
                            </html:p></p1:Summary><p1:Manufacturer><p1:Name>AdventureWorks</p1:Name><p1:Copyright>2002</p1:Copyright><p1:ProductURL>HTTP://www.Adventure-works.com</p1:ProductURL></p1:Manufacturer><p1:Features>Product highlights include: 
                     <wm:Warranty><wm:WarrantyPeriod>1 year</wm:WarrantyPeriod><wm:Description>parts and labor</wm:Description></wm:Warranty><wm:Maintenance><wm:NoOfYears>3 years</wm:NoOfYears><wm:Description>maintenance contact available through dealer</wm:Description></wm:Maintenance><wf:wheel>Stable, durable wheels suitable for novice riders.</wf:wheel><wf:saddle>Made from synthetic leather and features gel padding for increased comfort.</wf:saddle><wf:pedal><html:b>Expanded platform</html:b> so you can ride in any shoes; great for all-around riding.</wf:pedal><wf:crankset> Super rigid spindle. </wf:crankset><wf:BikeFrame>Our best value frame utilizing the same, ground-breaking technology as the ML aluminum frame.</wf:BikeFrame></p1:Features><!-- add one or more of these elements... one for each specific product in this product model --><p1:Picture><p1:Angle>front</p1:Angle><p1:Size>small</p1:Size><p1:ProductPhotoID>1</p1:ProductPhotoID></p1:Picture><!-- add any tags in <specifications> --><p1:Specifications> These are the product specifications.
                       <Height>Varies</Height> Centimeters.
                       <Material>Aluminum Alloy</Material><Color>Available in all colors.</Color><ProductLine>Mountain bike</ProductLine><Style>Unisex</Style><RiderExperience>Novice to Intermediate riders</RiderExperience></p1:Specifications></p1:ProductDescription>', NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (24, N'Racing Socks', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (25, N'Road-150', N'<?xml-stylesheet href="ProductDescription.xsl" type="text/xsl"?><p1:ProductDescription xmlns:p1="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription" xmlns:wm="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain" xmlns:wf="http://www.adventure-works.com/schemas/OtherFeatures" xmlns:html="http://www.w3.org/1999/xhtml" ProductModelID="25" ProductModelName="Road-150"><p1:Summary><html:p>This bike is ridden by race winners. Developed with the 
			    Adventure Works Cycles professional race team, it has a extremely light 
			    heat-treated aluminum frame, and steering that allows precision control.
                            </html:p></p1:Summary><p1:Manufacturer><p1:Name>AdventureWorks</p1:Name><p1:Copyright>2002</p1:Copyright><p1:ProductURL>HTTP://www.Adventure-works.com</p1:ProductURL></p1:Manufacturer><p1:Features>These are the product highlights. 
                     <wm:Warranty><wm:WarrantyPeriod>4 years</wm:WarrantyPeriod><wm:Description>parts and labor</wm:Description></wm:Warranty><wm:Maintenance><wm:NoOfYears>7 years</wm:NoOfYears><wm:Description>maintenance contact available through dealer or any Adventure Works Cycles retail store.</wm:Description></wm:Maintenance><wf:handlebar>Designed for racers; high-end anatomically shaped bar from aluminum alloy.</wf:handlebar><wf:wheel>Strong wheels with double-walled rims.</wf:wheel><wf:saddle><html:i>Lightweight </html:i> kevlar racing saddle.</wf:saddle><wf:pedal><html:b>Top-of-the-line</html:b> clipless pedals with adjustable tension.</wf:pedal><wf:BikeFrame><html:i>Our lightest and best quality </html:i> aluminum frame made from the newest alloy;
			    it is welded and heat-treated for strength. 
			    Our innovative design results in maximum comfort and performance.</wf:BikeFrame></p1:Features><!-- add one or more of these elements... one for each specific product in this product model --><p1:Picture><p1:Angle>front</p1:Angle><p1:Size>small</p1:Size><p1:ProductPhotoID>126</p1:ProductPhotoID></p1:Picture><!-- add any tags in <specifications> --><p1:Specifications> These are the product specifications.
                       <Material>Aluminum</Material><Color>Available in all colors.</Color><ProductLine>Road bike</ProductLine><Style>Unisex</Style><RiderExperience>Intermediate to Professional riders</RiderExperience></p1:Specifications></p1:ProductDescription>', NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (26, N'Road-250', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (27, N'Road-350-W', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (28, N'Road-450', N'<?xml-stylesheet href="ProductDescription.xsl" type="text/xsl"?><p1:ProductDescription xmlns:p1="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription" xmlns:wm="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain" xmlns:wf="http://www.adventure-works.com/schemas/OtherFeatures" xmlns:html="http://www.w3.org/1999/xhtml" ProductModelID="28" ProductModelName="Road-450"><p1:Summary><html:p>A true multi-sport bike that offers streamlined riding and a revolutionary design. 
			           Aerodynamic design lets you ride with the pros, and the gearing will conquer hilly roads.
                            </html:p></p1:Summary><p1:Manufacturer><p1:Name>AdventureWorks</p1:Name><p1:Copyright>2002</p1:Copyright><p1:ProductURL>HTTP://www.Adventure-works.com</p1:ProductURL></p1:Manufacturer><p1:Features>These are the product highlights. 
                     <wm:Warranty><wm:WarrantyPeriod>1 year</wm:WarrantyPeriod><wm:Description>parts and labor</wm:Description></wm:Warranty><wm:Maintenance><wm:NoOfYears>5 years</wm:NoOfYears><wm:Description>maintenance contact available through dealer</wm:Description></wm:Maintenance><wf:handlebar><html:i>Incredibly strong</html:i> professional grade handlebar.</wf:handlebar><wf:wheel>Aluminum alloy rim with stainless steel spokes; built for speed on our high quality rubber tires.</wf:wheel><wf:saddle><html:i>Comfortable</html:i> saddle with bump absorping rubber bumpers.</wf:saddle><wf:pedal><html:b>Top-of-the-line</html:b> clipless pedals with adjustable tension.</wf:pedal><wf:BikeFrame><html:i>aluminum alloy </html:i> frame
                         and features a lightweight down-tube milled to the perfect diameter for optimal strength.</wf:BikeFrame></p1:Features><!-- add one or more of these elements... one for each specific product in this product model --><p1:Picture><p1:Angle>front</p1:Angle><p1:Size>small</p1:Size><p1:ProductPhotoID>111</p1:ProductPhotoID></p1:Picture><!-- add any tags in <specifications> --><p1:Specifications> These are the product specifications.
                       <Height>Varies</Height> Centimeters.
                       <Material>Aluminum Alloy</Material><Weight>Varies with size </Weight><Color>Available in all colors.</Color><ProductLine>Road bike</ProductLine><Style>Unisex</Style><RiderExperience>Novice to Advanced riders</RiderExperience></p1:Specifications></p1:ProductDescription>', NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (29, N'Road-550-W', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (30, N'Road-650', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (31, N'Road-750', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (32, N'Short-Sleeve Classic Jersey', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (33, N'Sport-100', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (34, N'Touring-1000', N'<?xml-stylesheet href="ProductDescription.xsl" type="text/xsl"?><p1:ProductDescription xmlns:p1="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription" xmlns:wm="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain" xmlns:wf="http://www.adventure-works.com/schemas/OtherFeatures" xmlns:html="http://www.w3.org/1999/xhtml" ProductModelID="34" ProductModelName="Touring-1000"><p1:Summary><html:p>Travel in style and comfort. Designed for maximum comfort and safety. 
			    Wide gear range takes on all hills. High-tech aluminum alloy construction provides durability without added weight.
                            </html:p></p1:Summary><p1:Manufacturer><p1:Name>AdventureWorks</p1:Name><p1:Copyright>2002</p1:Copyright><p1:ProductURL>HTTP://www.Adventure-works.com</p1:ProductURL></p1:Manufacturer><p1:Features>These are the product highlights. 
                     <wm:Warranty><wm:WarrantyPeriod>1 year</wm:WarrantyPeriod><wm:Description>parts and labor</wm:Description></wm:Warranty><wm:Maintenance><wm:NoOfYears>5 years</wm:NoOfYears><wm:Description>maintenance contact available through dealer</wm:Description></wm:Maintenance><wf:handlebar>A light yet stiff aluminum bar for long distance riding.</wf:handlebar><wf:wheel>Excellent aerodynamic rims guarantee a smooth ride.</wf:wheel><wf:saddle><html:i>Cut-out shell </html:i> for a more comfortable ride.</wf:saddle><wf:pedal>A stable pedal for all-day riding.</wf:pedal><wf:BikeFrame><html:i>aluminum alloy </html:i> frame
                         and features a lightweight down-tube milled to the perfect diameter for optimal strength.</wf:BikeFrame></p1:Features><!-- add one or more of these elements... one for each specific product in this product model --><p1:Picture><p1:Angle>front</p1:Angle><p1:Size>small</p1:Size><p1:ProductPhotoID>87</p1:ProductPhotoID></p1:Picture><!-- add any tags in <specifications> --><p1:Specifications> These are the product specifications.
                   
                       <Material>Aluminum alloy </Material><Color>Available in most colors.</Color><ProductLine>Touring bike</ProductLine><Style>Unisex</Style><RiderExperience>Novice to Advanced riders</RiderExperience></p1:Specifications></p1:ProductDescription>', NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (35, N'Touring-2000', N'<?xml-stylesheet href="ProductDescription.xsl" type="text/xsl"?><p1:ProductDescription xmlns:p1="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription" xmlns:wm="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain" xmlns:wf="http://www.adventure-works.com/schemas/OtherFeatures" xmlns:html="http://www.w3.org/1999/xhtml" ProductModelID="35" ProductModelName="Touring-2000"><p1:Summary><html:p>The plush custom saddle keeps you riding all day, and there''s plenty of space
			     to add panniers and bike bags to the newly-redesigned carrier. 
				    This bike has great stability when fully-loaded.
                            </html:p></p1:Summary><p1:Manufacturer><p1:Name>AdventureWorks</p1:Name><p1:Copyright>2002</p1:Copyright><p1:ProductURL>HTTP://www.Adventure-works.com</p1:ProductURL></p1:Manufacturer><p1:Features>These are the product highlights. 
                     <wm:Warranty><wm:WarrantyPeriod>1 year</wm:WarrantyPeriod><wm:Description>parts and labor</wm:Description></wm:Warranty><wm:Maintenance><wm:NoOfYears>5 years</wm:NoOfYears><wm:Description>maintenance contact available through dealer</wm:Description></wm:Maintenance><wf:handlebar>A light yet stiff aluminum bar for long distance riding.</wf:handlebar><wf:saddle><html:i>New design </html:i> relieves pressure for long rides.</wf:saddle><wf:pedal><html:b>Top-of-the-line</html:b> clipless pedals with adjustable tension.</wf:pedal><wf:crankset> High-strength crank arm. </wf:crankset><wf:BikeFrame>The aluminum frame is custom-shaped for both good looks and strength; 
				    it will withstand the most rigorous challenges of daily riding.</wf:BikeFrame></p1:Features><!-- add one or more of these elements... one for each specific product in this product model --><p1:Picture><p1:Angle>front</p1:Angle><p1:Size>small</p1:Size><p1:ProductPhotoID>87</p1:ProductPhotoID></p1:Picture><!-- add any tags in <specifications> --><p1:Specifications> These are the product specifications.
                       <Material>Aluminum</Material><Color>Available in all colors except metallic.</Color><ProductLine>Touring bike</ProductLine><Style>Men''s</Style><FrameMaterial>Aluminium alloy</FrameMaterial><RiderExperience>Intermediate to Advanced riders</RiderExperience></p1:Specifications></p1:ProductDescription>', NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (36, N'Touring-3000', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (37, N'Women''s Mountain Shorts', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (38, N'Women''s Tights', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (39, N'Mountain-400', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (40, N'Road-550', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (41, N'Road-350', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (42, N'LL Mountain Front Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (43, N'Touring Rear Wheel', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesWA-150C Instructions for Assembling Touring Front Wheel Summary: This document contains manufacturing instructions for assembling the Touring Front Wheel, Product Model 43. Instructions are work center specific and are identified by work center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="3" LotSize="1" LocationID="50">
                        Work Center - 50 Frame Forming. The following instructions pertain to Work Center 10. (Setup hours = .0, Labor Hours = 3, Machine Hours = 0, Lot Sizing = 1)<step>
                            Inspect the <material>rim</material> for dents, cracks or other damage.
                        </step><step>
                            Slide each <material>spoke</material> through the <material>hub flange</material> working in a clockwise direction.
                        </step><step>
                            For each spoke, screw on the <material>spoke nipple (NI-9522)</material>. Do not over tighten.
                        </step><step>
                            Place the <material>wheel</material> in the truing stand.
                        </step><step>Pluck each spoke. The sound from each spoke should be consistent. Adjust as needed. </step><step>
                            Inflate the <material>tire tube</material>to one-quarter pressure.
                        </step><step>
                            Insert the <material>valve stem</material> through <material>Rim RM-M823</material>.
                        </step><step>Tuck the tube into the tire ensuring there are no wrinkles or kinks.</step><step>Inflate the tube to half pressure. </step><step>Spin the wheel and ensure the bead line is just above the rim. </step><step>Inflate the tube to 35 PSI.</step><step>
                            Attach reflector as shown in illustration <diag>7</diag></step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (44, N'Touring Front Wheel', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesWA-151C Instructions for Assembling Touring Rear Wheel Summary: This document contains manufacturing instructions for assembling the Touring Rear Wheel, Product Model 44. Instructions are work center specific and are identified by work center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="3" LotSize="1" LocationID="50">
                        Work Center - 50 Frame Forming. The following instructions pertain to Work Center 10. (Setup hours = .0, Labor Hours = 3, Machine Hours = 0, Lot Sizing = 1)<step>
                            Inspect the <material>rim</material> for dents, cracks or other damage.
                        </step><step>
                            Slide each <material>spoke</material> through the <material>hub flange</material> working in a clockwise direction.
                        </step><step>
                            For each spoke, screw on the <material>spoke nipple (NI-9525)</material>. Do not over tighten.
                        </step><step>
                            Place the wheel in the <tool>truing stand</tool>.
                        </step><step>Pluck each spoke. The sound from each spoke should be consistent. Adjust as needed.</step><step>Inflate the tire tube to one-quarter pressure.</step><step>
                            Insert the <material>valve stem</material> through <material>Rim RM-M825</material>.
                        </step><step>Tuck the tube into the tire ensuring there are no wrinkles or kinks.</step><step>Inflate the tube to half pressure. </step><step>Spin the wheel and ensure the bead line is just above the rim.</step><step>Inflate the tube to 35 PSI.</step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (45, N'ML Mountain Front Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (46, N'HL Mountain Front Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (47, N'LL Touring Handlebars', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesWA-190A Instructions for Manufacturing and Assembling the LL Touring Handlebars Summary: This document contains manufacturing instructions for manufacturing and assembling the LL Touring Handlebars, Product Model 47. Instructions are work center specific and are identified by work center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="1" LotSize="100" MachineHours="2" SetupHours="0.1" LocationID="10">
                        Work Center 10 - Frame Forming. The following instructions pertain to Work Center 10. (Setup hours = .10, Labor Hours = 1, Machine Hours = 2, Lot Sizing = 100)<step>
                            Insert <material>aluminum sheet MS-2259</material> into tool <tool>T-50 Tube Forming tool</tool>.
                        </step><step>
                            Attach <tool>Trim Jig TJ-8</tool> to the upper and lower right corners of the aluminum sheet.
                        </step><step>Route the aluminum sheet following the jig carefully. </step><step>
                            Insert the cut pieces into <tool>Tube Forming Tool FT-90</tool> and press Start.
                        </step><step>
                            When finished, inspect the form for defects per Inspection Specification <specs>INFS-90</specs>.
                        </step><step>Remove the lengths of tube from the tool and place it in the Completed or Rejected bin as appropriate.</step></Location><Location LaborHours="1" LotSize="1" MachineHours="1.75" SetupHours="0.25" LocationID="20">
                        Work Center 20 - Frame Welding. The following instructions pertain to Work Center 20. (Setup hours = .25, Labor Hours = 1.0, Machine Hours = 1.75, Lot Sizing = 1)<step>
                            Assemble all <material>handlebar components</material> following blueprint <blueprint>1111</blueprint>.
                        </step><step>
                            Weld all components together as shown in illustration <diag>5</diag></step><step>
                            Inspect all weld joints per Adventure Works Cycles Inspection Specification <specs>INFS-222</specs>.
                        </step></Location><Location LaborHours="3.5" LotSize="1" LocationID="50">
                        Work Center - 50 Frame Forming. The following instructions pertain to Work Center 10. (Setup hours = .0, Labor Hours = 3.5, Machine Hours = 0, Lot Sizing = 1)<step>
                            Slide the <material>stem</material> onto the <material>handlebar</material> centering it over the knurled section.
                        </step><step>Take care not to scratch the handlebar.</step><step>The ends of the handlebar should turn toward the rear. </step><step>
                            Attach the <material>Pinch Bolt (Product Number PB-6109)</material>, <material>Lock Washer 10 (Product Number LW-1210)</material>, and <material>Lock Nut 7 (Product Number LN-1224)</material> onto the stem.
                        </step><step>Tighten the nut just enough to keep the handlebar from rotating in the stem. Do not over tighten. </step><step>
                            Put a <material>Flat Washer 1 (Product Number FW-1000)</material> onto the stem bolt, and then insert the bolt down into the stem.
                        </step><step>
                            Thread the plug nut onto the stem bolt aligning it with the stem body as shown in illustration <diag>4</diag>. Insert the stem down into the lock nut until the minimum insertion line marked on the stem is hidden inside the nut.
                        </step><step>
                            Attach the <material>grips</material>.
                        </step><step>
                            Inspect per specification <specs>FI-225</specs>.
                        </step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (48, N'HL Touring Handlebars', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesWA-190A Instructions for Manufacturing and Assembling the HL Touring Handlebars Summary: This document contains manufacturing instructions for manufacturing and assembling the HL Touring Handlebars, Product Model 48. Instructions are work center specific and are identified by work center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="1" LotSize="100" MachineHours="2.5" SetupHours="0.1" LocationID="10">
                        Work Center 10 - Frame Forming. The following instructions pertain to Work Center 10. (Setup hours = .10, Labor Hours = 1, Machine Hours = 2.5, Lot Sizing = 100)<step>
                            Insert <material>aluminum sheet MS-2259</material> into tool <tool>T-51 Tube Forming tool</tool>.
                        </step><step>
                            Attach <tool>Trim Jig TJ-9</tool> to the upper and lower right corners of the aluminum sheet.
                        </step><step>Route the aluminum sheet following the jig carefully. </step><step>
                            Insert the cut pieces into <tool>Tube Forming Tool FT-91</tool> and press Start.
                        </step><step>
                            When finished, inspect the form for defects per Inspection Specification <specs>INFS-90</specs>.
                        </step><step>Remove the lengths of tube from the tool and place it in the Completed or Rejected bin as appropriate.</step></Location><Location LaborHours="1" LotSize="1" MachineHours="2" SetupHours="0.25" LocationID="20">
                        Work Center 20 - Frame Welding. The following instructions pertain to Work Center 20. (Setup hours = .25, Labor Hours = 1.0, Machine Hours = 2, Lot Sizing = 1)<step>
                            Assemble all handlebar components following blueprint <blueprint>1112</blueprint>.
                        </step><step>
                            Using <tool>weld torch</tool>, weld all components together as shown in illustration <diag>5</diag></step><step>
                            Inspect all weld joints per Adventure Works Cycles Inspection Specification <specs>INFS-222</specs>.
                        </step></Location><Location LaborHours="3.5" LotSize="1" LocationID="50">
                        Work Center 50 - SubAssembly. The following instructions pertain to Work Center 50. (Setup hours = .0, Labor Hours = 3.5, Machine Hours = 0, Lot Sizing = 1)<step>
                            Slide the <material>stem</material> onto the <material>handlebar</material> centering it over the knurled section.
                        </step><step>Take care not to scratch the handlebar.</step><step>The ends of the handlebar should turn toward the rear. </step><step>
                            Attach the <material>Pinch Bolt (Product Number PB-6109)</material>, <material>Lock Washer 7 (Product Number LI-3800)</material>, and <material>Lock Nut 16 (Product Number LN-1213)</material> onto the stem.
                        </step><step>Tighten the nut just enough to keep the handlebar from rotating in the stem. Do not over tighten. </step><step>
                            Put a <material>Flat Washer 9 (Product Number FW-3400)</material> onto the <material>stem bolt</material>, and then insert the bolt down into the stem.
                        </step><step>
                            Thread the plug nut onto the stem bolt aligning it with the stem body as shown in illustration <diag>4</diag>. Insert the stem down into the lock nut until the minimum insertion line marked on the stem is hidden inside the nut.
                        </step><step>
                            Attach the <material>grips</material>.
                        </step><step>
                            Inspect per specification <specs>FI-225</specs>.
                        </step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (49, N'LL Road Front Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (50, N'ML Road Front Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (51, N'HL Road Front Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (52, N'LL Mountain Handlebars', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (53, N'Touring Pedal', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesWA-500 Instructions Assembling the Touring Pedal Summary: This document contains manufacturing instructions for assembling the Touring Pedal, Product Model 53. Instructions are work center specific and are identified by work center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="0.5" LotSize="1" LocationID="50">
                        Work Center 50 - SubAssembly. The following instructions pertain to Work Center 50. (Setup hours = .0, Labor Hours = .5, Machine Hours = 0, Lot Sizing = 1)<step>
                            Visually examine the pedal spindles to determine <material>left and right pedals</material>. The left and right pedals have different threading directions. It is important you identify them correctly.
                        </step><step>
                            Apply a small amount of <material>grease</material> to the left pedal and thread the pedal onto the <material>left crank arm</material> by hand.
                        </step><step>If the threads do not turn easily, back the spindle out and re-start.</step><step>
                            Securely tighten the spindle against the crank arm using a <tool>small wrench</tool>.
                        </step><step>Apply a small amount of grease to the right pedal and thread the pedal onto the right crank arm by hand.</step><step>If the threads do not turn easily, back the spindle out and re-start.</step><step>Securely tighten the spindle against the crank arm using a small wrench.</step><step>
                            Inspect per specification <specs>FI-520</specs>.
                        </step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (54, N'ML Mountain Handlebars', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (55, N'HL Mountain Handlebars', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (56, N'LL Road Handlebars', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (57, N'ML Road Handlebars', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (58, N'HL Road Handlebars', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (59, N'LL Headset', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (60, N'ML Headset', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (61, N'HL Headset', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (62, N'LL Mountain Pedal', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (63, N'ML Mountain Pedal', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (64, N'HL Mountain Pedal', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (65, N'ML Touring Seat/Saddle', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (66, N'LL Touring Seat/Saddle', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesWA-620 Instructions Assembling the LL Touring Seat Summary: This document contains manufacturing instructions for assembling the LL Touring Seat, Product Model 63. Instructions are work center specific and are identified by work center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="1.5" LotSize="1" LocationID="50">
                        Work Center 50 - SubAssembly. The following instructions pertain to Work Center 50. (Setup hours = .0, Labor Hours = 1.5, Machine Hours = 0, Lot Sizing = 1)<step>
                            Put the <material>Seat post Lug (Product Number SL-0931)</material> on the <material>Seat Post (Product Number SP-2981)</material>.
                        </step><step>
                            Insert the <material>Pinch Bolt (Product Number PB-6109)</material> and tighten until it is secure but still able to slide up or down the post as shown in illustration <diag>6</diag>.
                        </step><step>
                            Attach the <material>LL Seat (Product Number SE-T312)</material> to the top of the Seat Post and tighten securely.
                        </step><step>
                            Inspect per specification <specs>FI-620</specs>.
                        </step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (67, N'HL Touring Seat/Saddle', NULL, N'<root xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions">
                    Adventure Works CyclesWA-620 Instructions Assembling the HL Touring Seat Summary: This document contains manufacturing instructions for assembling the HL Touring Seat, Product Model 67. Instructions are work center specific and are identified by work center ID. These instructions must be followed in the order presented. Deviation from the instructions is not permitted unless an authorized Change Order detailing the deviation is provided by the Engineering Manager.<Location LaborHours="1" LotSize="1" SetupHours="0.25" LocationID="50">
                        Work Center 50 - SubAssembly. The following instructions pertain to Work Center 50. (Setup hours = .25, Labor Hours = 1, Machine Hours = 0, Lot Sizing = 1)<step>
                            Put the <material>Seatpost Lug (Product Number SL-0932)</material> on the <material>Seat Post (Product Number SP-3981)</material>.
                        </step><step>
                            Insert the <material>Pinch Bolt (Product Number PB-6109)</material> and tighten until it is secure but still able to slide up or down the post. See illustration <diag>6</diag>.
                        </step><step>
                            Attach the <material>HL Seat (Product Number SE-T315)</material> to the top of the Seat Post and tighten securely.
                        </step><step>
                            Inspect per specification <specs>FI-625</specs>.
                        </step></Location></root>')
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (68, N'LL Road Pedal', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (69, N'ML Road Pedal', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (70, N'HL Road Pedal', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (71, N'LL Mountain Seat/Saddle 1', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (72, N'ML Mountain Seat/Saddle 1', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (73, N'HL Mountain Seat/Saddle 1', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (74, N'LL Road Seat/Saddle 2', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (75, N'ML Road Seat/Saddle 1', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (76, N'HL Road Seat/Saddle 1', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (77, N'ML Road Rear Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (78, N'HL Road Rear Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (79, N'LL Mountain Seat/Saddle 2', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (80, N'ML Mountain Seat/Saddle 2', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (81, N'HL Mountain Seat/Saddle 2', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (82, N'LL Road Seat/Saddle 1', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (83, N'ML Road Seat/Saddle 2', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (84, N'HL Road Seat/Saddle 2', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (85, N'LL Mountain Tire', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (86, N'ML Mountain Tire', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (87, N'HL Mountain Tire', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (88, N'LL Road Tire', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (89, N'ML Road Tire', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (90, N'HL Road Tire', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (91, N'Touring Tire', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (92, N'Mountain Tire Tube', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (93, N'Road Tire Tube', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (94, N'Touring Tire Tube', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (95, N'LL Bottom Bracket', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (96, N'ML Bottom Bracket', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (97, N'HL Bottom Bracket', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (98, N'Chain', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (99, N'LL Crankset', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (100, N'ML Crankset', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (101, N'HL Crankset', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (102, N'Front Brakes', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (103, N'Front Derailleur', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (104, N'LL Fork', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (105, N'ML Fork', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (106, N'HL Fork', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (107, N'Hydration Pack', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (108, N'Taillight', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (109, N'Headlights - Dual-Beam', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (110, N'Headlights - Weatherproof', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (111, N'Water Bottle', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (112, N'Mountain Bottle Cage', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (113, N'Road Bottle Cage', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (114, N'Patch kit', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (115, N'Cable Lock', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (116, N'Minipump', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (117, N'Mountain Pump', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (118, N'Hitch Rack - 4-Bike', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (119, N'Bike Wash', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (120, N'Touring-Panniers', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (121, N'Fender Set - Mountain', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (122, N'All-Purpose Bike Stand', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (123, N'LL Mountain Rear Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (124, N'ML Mountain Rear Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (125, N'HL Mountain Rear Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (126, N'LL Road Rear Wheel', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (127, N'Rear Derailleur', NULL, NULL)
    
    INSERT [dbo].[ProductModel] ([ProductModelID], [Name], [CatalogDescription], [Instructions]) VALUES (128, N'Rear Brakes', NULL, NULL)
    

    SET IDENTITY_INSERT [ProductModel] OFF
END