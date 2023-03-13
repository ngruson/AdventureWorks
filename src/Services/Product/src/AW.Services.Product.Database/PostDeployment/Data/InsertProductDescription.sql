IF NOT EXISTS (SELECT TOP 1 1 FROM ProductDescription)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table ProductDescription...'

	SET IDENTITY_INSERT [ProductDescription] ON

    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (3, N'Chromoly steel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (4, N'Aluminum alloy cups; large diameter spindle.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (5, N'Aluminum alloy cups and a hollow axle.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (8, N'Suitable for any type of riding, on or off-road. Fits any budget. Smooth-shifting with a comfortable ride.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (64, N'This bike delivers a high-level of performance on a budget. It is responsive and maneuverable, and offers peace-of-mind when you decide to  off-road.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (88, N'For true trail addicts.  An extremely durable bike that will  anywhere and keep you in control on challenging terrain - without breaking your budget.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (128, N'Serious back-country riding. Perfect for all levels of competition. Uses the same HL Frame as the Mountain-100.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (168, N'Top-of-the-line competition mountain bike. Performance-enhancing options include the innovative HL Frame, super-smooth front suspension, and traction for all terrain.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (170, N'Suitable for any type of off-road trip. Fits any budget.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (209, N'Entry level adult bike; offers a comfortable ride cross-country or down the block. Quick-release hubs and rims.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (249, N'Value-priced bike with many features of our top-of-the-line models. Has the same light, stiff frame, and the quick acceleration we''re famous for.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (320, N'Same technology as all of our Road series bikes, but the frame is sized for a woman.  Perfect all-around bike for road or racing.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (321, N'Same technology as all of our Road series bikes.  Perfect all-around bike for road or racing.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (337, N'A true multi-sport bike that offers streamlined riding and a revolutionary design. Aerodynamic design lets you ride with the pros, and the gearing will conquer hilly roads.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (375, N'Cross-train, race, or just socialize on a sleek, aerodynamic bike.  Advanced seat technology provides comfort all day.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (376, N'Cross-train, race, or just socialize on a sleek, aerodynamic bike designed for a woman.  Advanced seat technology provides comfort all day.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (409, N'Alluminum-alloy frame provides a light, stiff ride, whether you are racing in the velodrome or on a demanding club ride on country roads.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (457, N'This bike is ridden by race winners. Developed with the Adventure Works Cycles professional race team, it has a extremely light heat-treated aluminum frame, and steering that allows precision control.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (513, N'All-occasion value bike with our basic comfort and safety features. Offers wider, more stable tires for a ride around town or weekend trip.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (554, N'The plush custom saddle keeps you riding all day,  and there''s plenty of space to add panniers and bike bags to the newly-redesigned carrier.  This bike has stability when fully-loaded.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (594, N'Travel in style and comfort. Designed for maximum comfort and safety. Wide gear range takes on all hills. High-tech aluminum alloy construction provides durability without added weight.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (613, N'Superior shifting performance.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (618, N'Super rigid spindle.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (619, N'High-strength crank arm.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (620, N'Triple crankset; alumunim crank arm; flawless shifting.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (627, N'All-weather brake pads; provides superior stopping by applying more surface to the rim.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (630, N'Wide-link design.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (633, N'Stout design absorbs shock and offers more precise steering.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (634, N'Composite road fork with an aluminum steerer tube.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (635, N'High-performance carbon road fork with curved legs.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (637, N'Our best value utilizing the same, ground-breaking frame technology as the ML aluminum frame.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (642, N'The ML frame is a heat-treated aluminum frame made with the same detail and quality as our HL frame. It offers superior performance. Men''s version.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (644, N'The ML frame is a heat-treated aluminum frame made with the same detail and quality as our HL frame. It offers superior performance. Women''s version.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (647, N'Each frame is hand-crafted in our Bothell facility to the optimum diameter and wall-thickness required of a premium mountain frame. The heat-treated welded aluminum frame has a larger diameter tube that absorbs the bumps.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (661, N'Made from the same aluminum alloy as our top-of-the line HL frame, the ML features a lightweight down-tube milled to the perfect diameter for optimal strength. Women''s version.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (686, N'Replacement mountain wheel for entry-level rider.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (687, N'Replacement mountain wheel for the casual to serious rider.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (688, N'High-performance mountain replacement wheel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (689, N'Replacement road front wheel for entry-level cyclist.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (690, N'Sturdy alloy features a quick-release hub.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (691, N'Strong wheel with double-walled rim.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (692, N'Aerodynamic rims for smooth riding.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (697, N'All-purpose bar for on or off-road.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (698, N'Tough aluminum alloy bars for downhill.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (699, N'Flat bar strong enough for the pro circuit.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (700, N'Unique shape provides easier reach to the levers.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (701, N'Anatomically shaped aluminum tube bar will suit all riders.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (702, N'Designed for racers; high-end anatomically shaped bar from aluminum alloy.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (703, N'Unique shape reduces fatigue for entry level riders.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (704, N'A light yet stiff aluminum bar for long distance riding.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (744, N'Threadless headset provides quality at an economical price.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (745, N'Sealed cartridge keeps dirt out.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (746, N'High-quality 1" threadless headset with a grease port for quick lubrication.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (847, N'Expanded platform so you can ride in any shoes; great for all-around riding.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (848, N'Lightweight, durable, clipless pedal with adjustable tension.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (849, N'Stainless steel; designed to shed mud easily.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (850, N'Clipless pedals - aluminum.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (851, N'Lightweight aluminum alloy construction.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (852, N'Top-of-the-line clipless pedals with adjustable tension.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (853, N'A stable pedal for all-day riding.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (856, N'Refillable shoes; polished aluminum calipers.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (858, N'10-speed aluminum derailleur with sealed pulley bearings.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (867, N'Replacement rear mountain wheel for entry-level rider.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (868, N'Replacement rear mountain wheel for the casual to serious rider.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (869, N'Extra-strong rims guarantee durability.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (870, N'Replacement rear wheel for entry-level cyclist.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (871, N'Aluminum alloy rim with stainless steel spokes; built for speed.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (872, N'Strong rear wheel with double-walled rim.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (873, N'Excellent aerodynamic rims guarantee a smooth ride.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (885, N'Synthetic leather. Features gel for increased comfort.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (886, N'Designed to absorb shock.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (887, N'Anatomic design for a full-day of riding in comfort. Durable leather.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (888, N'Lightweight foam-padded saddle.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (889, N'Rubber bumpers absorb bumps.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (890, N'Lightweight kevlar racing saddle. Leather.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (891, N'Comfortable, ergonomically shaped gel saddle.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (892, N'New design relieves pressure for long rides.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (893, N'Cut-out shell for a more comfortable ride.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (903, N'Comparible traction, less expensive wire bead casing.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (904, N'Great traction, high-density rubber.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (905, N'Incredible traction, lightweight carbon reinforced.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (906, N'Same great treads as more expensive tire with a less expensive wire bead casing.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (907, N'Higher density rubber.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (908, N'Lightweight carbon reinforced  for an unrivaled ride at an un-compromised weight.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (909, N'High-density rubber.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (912, N'Self-sealing tube.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (913, N'Conventional all-purpose tube.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (914, N'General purpose tube.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1020, N'The LL Frame provides a safe comfortable ride, while offering superior bump absorption in a value-priced aluminum frame.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1062, N'Made from the same aluminum alloy as our top-of-the line HL frame, the ML features a lightweight down-tube milled to the perfect diameter for optimal strength. Men''s version.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1090, N'Our lightest and best quality aluminum frame made from the newest alloy; it is welded and heat-treated for strength. Our innovative design results in maximum comfort and performance.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1146, N'Lightweight butted aluminum frame provides a more upright riding position for a trip around town.  Our ground-breaking design provides optimum comfort.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1182, N'The HL aluminum frame is custom-shaped for both good looks and strength; it will withstand the most rigorous challenges of daily riding. Men''s version.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1183, N'Affordable light for safe night riding - uses 3 AAA batteries')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1185, N'Aluminum cage is lighter than our mountain version; perfect for long distance trips.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1186, N'AWC logo water bottle - holds 30 oz; leak-proof.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1187, N'Carries 4 bikes securely; steel construction, fits 2" receiver hitch.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1188, N'Clip-on fenders fit most mountain bikes.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1189, N'Combination of natural and synthetic fibers stays dry and provides just the right cushioning.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1191, N'Designed for convenience. Fits in your pocket. Aluminum barrel. 160psi rated.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1192, N'Designed for the AWC team with stay-put straps, moisture-control, chamois padding, and leg grippers.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1193, N'Durable, water-proof nylon construction with easy access. Large enough for weekend trips.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1194, N'Full padding, improved finger flex, durable palm, adjustable closure.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1195, N'Synthetic palm, flexible knuckles, breathable mesh upper. Worn by the AWC team riders.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1196, N'Heavy duty, abrasion-resistant shorts feature seamless, lycra inner shorts with anti-bacterial chamois for comfort.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1197, N'Includes 8 different size patches, glue and sandpaper.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1199, N'Light-weight, wind-resistant, packs to fit into a pocket.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1200, N'Men''s 8-panel racing shorts - lycra with an elastic waistband and leg grippers.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1201, N'Perfect all-purpose bike stand for working on your bike at home. Quick-adjusting clamps and steel construction.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1202, N'Rechargeable dual-beam headlight.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1203, N'Rugged weatherproof headlight.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1205, N'Short sleeve classic breathable jersey with superior moisture control, front zipper, and 3 back pockets.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1206, N'Simple and light-weight. Emergency patches stored in handle.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1208, N'Thin, lightweight and durable with cuffs that stay up.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1209, N'Tough aluminum cage holds bottle securly on tough terrain.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1210, N'Traditional style with a flip-up brim; one-size fits all.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1211, N'Unisex long-sleeve AWC logo microfiber cycling jersey')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1212, N'Universal fit, well-vented, lightweight , snap-on visor.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1213, N'Versatile 70 oz hydration pack offers extra storage, easy-fill access, and a waist belt.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1214, N'Warm spandex tights for winter riding; seamless chamois construction eliminates pressure points.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1215, N'Washes off the toughest road grime; dissolves grease, environmentally safe. 1-liter bottle.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1216, N'Wraps to fit front and rear tires, carrier and 2 keys included.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1360, N'الفولاذ الكرومولي')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1361, N'فناجين من سبيكة الألومنيوم؛ ذات محور دوران كبير القطر.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1362, N'فناجين من سبيكة الألومنيوم ومحور أجوف.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1363, N'ملائمة لأي نوع من أنواع القيادة، سواءً على الطرق الممهدة أو غير الممهدة. وتناسب أية ميزانية. نقل سرعات سلس مع قيادة مريحة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1364, N'توفر هذه الدراجة مستوى عاليًا من الأداء في حدود ميزانية معينة. فهي تتميز بسرعة الاستجابة وإمكانية القيام بمناورات، هذا بالإضافة إلى إمكاناتها الفائقة التي تمنح الثقة في القيادة على الطرق غير الممهدة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1365, N'لعاشقي قيادة الدراجات في الممرات غير الممهدة. دراجة شديدة التحمل يمكنك الانطلاق بها إلى أي مكان بما توفره لك من تحكم على الطرق الوعرة، كل هذا دون أن تتجاوز ميزانيتك.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1366, N'لقيادة المحترفين في المناطق الريفية. دراجة مُثلى لجميع مستويات المنافسة. تستخدم نفس هيكل HL Frame المستخدم في طراز Mountain-100.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1367, N'دراجة سباقات مخصصة للقيادة في الجبال من أعلى طراز. تتضمن خيارات تحسين الأداء هيكل HL Frame الإبداعي، والتعليق الأمامي ذي السلاسة الفائقة، وقوة الجر المناسبة لجميع أنواع الأراضي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1368, N'مناسبة لجميع أنواع الرحلات على الطرق غير الممهدة. تناسب أية ميزانية.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1369, N'إنها دراجة مناسبة للمبتدئين من البالغين؛ فهي توفر قيادة مريحة سواءً على الطرق الوعرة أو في ساحة المدينة. يتميز محورا العجلتين وإطاريهما المعدنيين بسرعة التفكيك.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1370, N'إنها دراجة ذات سعر مناسب لإمكاناتها كما تتمتع بالعديد من مزايا أعلى طرازات دراجاتنا. إنها تتمتع أيضًا بما نشتهر به من هيكل خفيف وصلب، وسرعة في التسارع.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1371, N'ولها نفس التقنية المتوفرة في جميع دراجات سلسلة الطريق لدينا، ولكن هيكل الدراجة قد تم تغيير حجمه ليناسب القيادة النسائية. إنها دراجة مثالية تصلح للعديد من الأغراض، وتناسب القيادة على الطرق العادية أو القيادة في السباقات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1372, N'ولها نفس التقنية المتوفرة في جميع دراجات سلسلة الطريق لدينا. إنها دراجة مثالية متعددة الأغراض، تناسب القيادة على الطرق العادية أو القيادة في السباقات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1373, N'إنها دراجة تصلح للعديد من الرياضات وهي مصممة لتوفير أقصى فعالية للقيادة، كما تتميز بتصميم ابتكاري. يتيح لك تصميمها الديناميكي الهوائي إمكانية القيادة مع المحترفين، حيث إنها مزودة بتروس يمكنها الانطلاق داخل الطرق المرتفعة شديدة الانحدار.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1374, N'يمكنك باستخدام هذه الدراجة الأنيقة التي تتميز بخاصية الديناميكية الهوائية التدرب على مهارات مختلفة أو التسابق أو مجرد المشاركة في نشاط اجتماعي. وتوفر لك تقنية المقاعد المتقدمة الراحة طوال اليوم.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1375, N'يمكنكِ باستخدام هذه الدراجة الأنيقة التي تتميز بخاصية الديناميكية الهوائية والمصممة للنساء التدرب على مهارات مختلفة أو التسابق أو مجرد المشاركة في نشاط اجتماعي. وتوفر لكِ تقنية المقاعد المتقدمة الراحة طوال اليوم.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1376, N'يوفر الهيكل المصنوع من سبيكة ألومنيوم قيادة قوية وخفيفة الحركة في نفس الوقت، سواءً كنت تتسابق في حلبة سباق دراجات أو في سباق يتطلب مهارات خاصة ـ بأحد النوادي ـ داخل طرقات ريفية.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1377, N'إنها الدراجة التي يقودها الفائزون بسباقات الدراجات. ونظرًا لأنه تم تطويرها عن طريق فريق سباق المحترفين Adventure Works Cycles، فقد تم تصميم هيكلها من الألومنيوم الخفيف المُعالج حراريًا، وتم تزويدها بنظام قيادة يسمح بالتحكم الدقيق في الدراجة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1378, N'إنها دراجة قيّمة تصلح لكافة المناسبات وهي تشتمل على ميزات الراحة والأمان الأساسية في منتجاتنا. وهي تتميز بإطارات أعرض وأكثر توازنًا من غيرها بما يلائم القيادة في أرجاء البلدة أو أثناء رحلات العطلات الأسبوعية.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1379, N'إن مقعد الدراجة المخصص المصنوع من نسيج البلش يجعلك لا تمل أبدًا من القيادة طوال اليوم، كما يوفر لك حامل الدراجة بتصميمه المحدّث مساحة كبيرة تستوعب أكثر من سلة وشنطة على الدراجة. وتحتفظ هذه الدراجة بتوازنها حتى عند تحميلها بالكامل.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1380, N'عجلة احتياطية خلفية مخصصة للقيادة في الجبال تناسب جميع الراكبين بدءًا من الراكب العادي إلى الراكب المحترف.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1381, N'إطاران معدنيان شديدا القوة يضمنان قوة التحمل.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1382, N'عجلة خلفية بديلة لراكبي الدراجات المبتدئين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1383, N'إطار معدني مصنوع من سبيكة الألومنيوم بقضبان شعاعية من الفولاذ الذي لا يصدأ، مصمم لزيادة السرعة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1384, N'عجلة خلفية قوية بإطار معدني ذي طبقتين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1385, N'إطاران معدنيان ممتازان يتميزان بخاصية الديناميكية الهوائية لضمان القيادة السلسلة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1386, N'جلد صناعي. تحتوي على مواد جيلاتينية لتوفير مزيد من الراحة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1387, N'الاستمتاع بالمظهر الأنيق والراحة عند السفر. لقد تم تصميم هذه الدراجة لتوفير أقصى درجات الراحة والأمان. يتيح لك النطاق الواسع من التروس إمكانية القيادة فوق كافة أنواع المرتفعات. توفر تركيبة سبيكة الألومنيوم عالية التقنية قوة تحمّل دون وزن إضافي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1388, N'أداء فائق في نقل السرعات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1389, N'محور دوران صلب فائق الأداء.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1390, N'ذراع تدوير شديد القوة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1391, N'مجموعة تدوير ثلاثية، وذراع تدوير ألومنيوم، وإمكانية نقل سرعات دون حدوث أخطاء.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1392, N'تحتوي على بطانات (تيل) فرامل، تناسب كافة الظروف الجوية، مما يتيح إمكانية إيقاف فائقة وذلك بتطبيق مساحة أكبر من سطح الفرامل على الإطار المعدني.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1393, N'تصميم عريض الوصلات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1394, N'تصميم قوي يمتص الصدمات ويوفر قيادة أكثر دقة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1395, N'شوكة طريق ماسكة للعجلة الأمامية مركبة ذات عمود توجيه مصنوع من الألومنيوم.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1396, N'شوكة طريق ماسكة للعجلة الأمامية من الكربون عالية الأداء ذات شعبتين مقوسين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1397, N'إنها تأتي ضمن أفضل منتجاتنا فنيًا حيث تستخدم نفس تقنية الهيكل الابتكارية المستخدمة في هيكل ML المصنوع من الألومنيوم.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1398, N'إن هيكل ML هو هيكل مصنوع من الألومنيوم مُعالج حراريًا ومصمم بنفس تفاصيل وجودة هيكل HL الخاص بنا. وهو يوفر أداءً فائقًا. الطراز الرجالي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1399, N'إن هيكل ML هو هيكل مصنوع من الألومنيوم مُعالج حراريًا ومصمم بنفس تفاصيل وجودة هيكل HL الخاص بنا. وهو يوفر أداءً فائقًا. الطراز النسائي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1400, N'وقد تم تصنيع كل هيكل من تلك الهياكل يدويًا في مصنعنا بمدينة Bothel بحيث يصل اتساع قطره وسمكه إلى أفضل قيمتين مطلوبتين لهيكل دراجة مخصصة للقيادة في الجبال من الدرجة الأولى. ويتميز هيكل الألومنيوم الملحوم والُمعالج حراريًا بأنبوبة ذات قطر كبير يمتص الصدمات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1401, N'يحتوي هيكل ML على أنبوبة سفلية خفيف الوزن تم تشكيلها بحيث تحتوي على أفضل قطر يوفر أعلى درجات القوة، وقد تم صناعتها من نفس سبيكة الألومنيوم التي تم صناعة هيكل HL - الذي يعد من أعلى طرازاتنا - منها. الطراز النسائي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1402, N'عجلة احتياطية مخصصة للقيادة في الجبال لراكبي الدراجات المبتدئين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1403, N'عجلة احتياطية مخصصة للقيادة في الجبال تناسب كافة أنواع الركاب بدءًا من الراكب العادي إلى الراكب المحترف.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1404, N'عجلة احتياطية مخصصة للقيادة في الجبال عالية الأداء.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1405, N'عجلة طريق أمامية بديلة لقائدي الدراجات المبتدئين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1406, N'تتميز بوجود سبيكة قوية تحتوي على محور عجلة سريع التفكيك.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1407, N'عجلة قوية بإطار معدني ذي طبقتين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1408, N'توفر خاصية الديناميكية الهوائية للإطارين المعدنيين قيادة سلسة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1409, N'قضيب قيادة ملائم لكافة الأغراض على الطرق الممهدة وغير الممهدة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1410, N'قضبان قيادة قوية من سبيكة الألومنيوم لتحمّل القيادة فوق المنحدرات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1411, N'قضيب قيادة مسطح وقوي بدرجة كافية لجولات المحترفين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1412, N'شكل فريد يوفر وصولاً أسهل إلى تروس التحكم في السرعة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1413, N'قضيب قيادة أنبوبي الشكل مصنوع من الألومنيوم ومشكّل بطريقة تتكيف مع بنية الراكب الجسمية ويناسب جميع الرّكاب.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1414, N'أحدث قضيب قيادة مشكّل بطريقة تتكيف مع بنية الراكب الجسمية وهو مصنوع من سبيكة ألومنيوم، ومصمم للمتسابقين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1415, N'شكل فريد يقلل عناء القيادة على الركاب المبتدئين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1416, N'قضيب قيادة مصنوع من الألومنيوم خفيف الوزن ولكنه قوي يتحمل القيادة لمسافات طويلة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1417, N'توفر الوصلة غير الملولبة بين الشوكة الماسكة للعجلة الأمامية وبقية هيكل الدراجة الجودة المطلوبة وبسعر اقتصادي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1418, N'تمنع الخرطوشة محكمة الإغلاق الأوساخ من الدخول.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1419, N'وصلة غير ملولبة بين الشوكة الماسكة للعجلة الأمامية وبقية هيكل الدراجة بمساحة 1 بوصة ذات جودة عالية مزودة بمنفذ تشحيم لإجراء التشحيم بشكل سريع.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1420, N'هيكل أساسي واسع بحيث يمكنك القيادة بأي حذاء، وهذا أمر رائع للقيادة متعددة الأغراض.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1421, N'دواسة خفيفة الوزن شديدة التحمّل بلا مشابك وذات قوة شدّ يمكن تعديلها.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1422, N'مصنوعة من الفولاذ الذي لا يصدأ، والذي تم تصميمه لإزالة الطين بسهولة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1423, N'دواستان بلا مشابك مصنوعتان من الألومنيوم.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1424, N'تركيبة سبيكة ألومنيوم خفيفة الوزن.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1425, N'دواستان من أعلى الطرازات بقوة شدّ يمكن تعديلها.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1426, N'دواسة ثابتة ومتوازنة للقيادة طوال اليوم.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1427, N'أحذية قيادة دراجات؛ كماشات فرامل مصنوعة من الألومنيوم ومصقولة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1429, N'ناقل سرعات مصنوع من الألومنيوم ذو عشر سرعات مع محامل بكرات مغلقة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1430, N'عجلة احتياطية خلفية مخصصة للقيادة في الجبال للراكبين المبتدئين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1431, N'مصممة لامتصاص الصدمات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1432, N'تصميم يتناسب مع بنية الراكب الجسمية لتوفير قيادة مريحة طوال اليوم. جلد شديد التحمل.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1433, N'مقعد خفيف الوزن ذو حشو رغوي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1434, N'مصدات مطاطية تمتص الصدمات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1435, N'مقعد سباق خفيف الوزن مصنوع من مادة كيفلار. جلد.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1436, N'مقعد جيلاتيني مصمم بشكل يتناسب مع حركة الجسم.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1437, N'تصميم جديد لتخفيف الضغط عند القيادة لفترات طويلة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1438, N'هيكل خارجي تم تصميمه لتوفير مزيد من الراحة أثناء القيادة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1439, N'قوة جر تضاهي أحدث الطرازات، غطاء حافة بارزة مطاطي منخفض الثمن.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1440, N'قوة جر كبيرة، مطاط عالي الكثافة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1441, N'قوة جر هائلة، كربون مقوى خفيف الوزن.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1442, N'السطح الخارجي للإطار بنفس جودة أسطح الإطارات الخارجية الأغلى ثمنًا، ولكنه مزود بغطاء حافة بارزة مطاطي أقل سعرًا.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1443, N'مطاط أعلى كثافة من الطرازات الأخرى.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1444, N'كربون مقوى خفيف الوزن لقيادة منقطعة النظير مع تحمل أوزان مرتفعة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1445, N'مطاط عالي الكثافة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1446, N'أنبوبة ذاتية القفل.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1447, N'أنبوبة تقليدية تناسب كافة الأغراض.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1448, N'أنبوبة متعددة الأغراض.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1449, N'يوفر الهيكل LL Frame قيادة آمنة ومريحة، مع توفير إمكانية فائقة لامتصاص الصدمات في هيكل من الألومنيوم جيد السعر بالنسبة لإمكاناته.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1450, N'يحتوي هيكل ML على أنبوبة سفلية خفيفة الوزن تم تشكيلها بحيث تحتوي على أفضل قطر يوفر أعلى درجات القوة، وقد تم صناعتها من نفس سبيكة الألومنيوم التي تم صناعة هيكل HL - الذي يعد من أعلى طرازاتنا - منها. الطراز الرجالي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1451, N'لقد تم صناعة هيكل دراجتنا الألومنيوم الأخف وزنًا والأعلى جودة، من أحدث السبائك المتوفرة، وتم لحامه ومعالجته حراريًا لزيادة قوته. ويوفر تصميمنا الإبداعي أقصى درجات الراحة والأداء.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1452, N'يوفر هيكل الألومنيوم سميك الأطراف خفيف الوزن وضع قيادة أكثر انتصابًا أثناء القيام برحلات في أرجاء البلدة. يوفر تصميمنا الابتكاري أعلى درجات الراحة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1453, N'وقد تم تخصيص شكل هيكل HL المصنوع من الألومنيوم لتوفير كل من جمال الشكل والقوة، كما أن بإمكانه مواجهة أصعب تحديات القيادة اليومية. الطراز الرجالي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1454, N'إضاءة ذات سعر منخفض للقيادة بشكل آمن ليلاً، تستخدم 3 بطاريات AAA.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1455, N'هيكل قفصي من الألومنيوم أخف من مثيله في طرازنا المخصص للقيادة في الجبال، وهو مثالي في الرحلات ذات المسافات الطويلة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1456, N'زجاجة مياه تحمل شعار AWC تستوعب 840 غرامًا من المياه، وهي مضادة للتسرب.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1457, N'يحمل 4 دراجات بشكل آمن، ذو بنية فولاذية، ويناسب عقدة تعليق بمساحة 2 بوصة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1458, N'رفارف مثبتة بمشابك تلائم معظم الدراجات المخصصة للقيادة في الجبال.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1459, N'تركيبة من الفيبر الطبيعي والصناعي تظل محتفظة بجفافها وتعمل كوسائد ملائمة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1460, N'مصممة لتوفير الراحة التامة. تلائم حجم الجيب. ماسورة من الألومنيوم. بمعدل رطل في البوصة المربعة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1461, N'مصممة لفريق AWC مع أشرطة ثابتة، وتحكم في الرطوبة، وحشو شمواة، وماسكات للرجلين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1462, N'تركيبة نيلون شديدة التحمل مضادة للماء يمكن الوصول إليها بسهولة. واسعة بدرجة كافية لرحلات عطلة نهاية الأسبوع.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1463, N'قفاز يد ذو حشوة كاملة، وثنية أصابع محسنة، وغطاء قوي لراحة اليد، كما يمكن تعديل مكان إغلاقه.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1464, N'قفاز ذو راحة يد صناعية، ومفاصل أصابع مرنة، وفتحات علوية تسمح بمرور الهواء. يرتديه راكبو دراجات فريق AWC.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1465, N'قمصان شديدة التحمل مقاومة للاحتكاكات تحتوي على قمصان داخلية أخرى خالية من الفتحات مصنوعة من قماش سباندكس ومبطنة بشمواة مضاد للبكتريا لتوفير الراحة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1466, N'تشمل 8 رقع لصق مختلفة الحجم، ومادة غروية لاصقة، وورق سنفرة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1467, N'علب خفيفة الوزن، ومقاومة للريح، تناسب حجم الجيب.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1468, N'قمصان سباق رجالي ذات ثمانية أقسام، من قماش سباندكس وذات حزام مرن وماسكات للرجلين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1469, N'حامل دراجة ممتاز متعدد الأغراض يلائم صيانة دراجتك بالمنزل. قوامط سريعة الضبط وتركيبة فولاذية.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1470, N'مصباح أمامي مزدوج الإضاءة قابل لإعادة الشحن.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1471, N'مصباح أمامي قوي مضاد للظروف الجوية.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1472, N'قميص صوفي قصير الأكمام كلاسيكي يسمح بمرور الهواء خلاله مع تحكم فائق في الرطوبة، مزود بزمام منزلق أمامي، وثلاثة جيوب خلفية.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1473, N'بسيطة وخفيفة الوزن. رقع لصق للطوارئ مخزنة في قضيب القيادة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1474, N'رفيعة وخفيفة الوزن وشديدة التحمل ذات أكمام تظل ثابتة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1475, N'هيكل قفصي من الألومنيوم يُثبت الزجاجة بإحكام أثناء القيادة على الأراضي الوعرة.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1476, N'نمط تقليدي ذو حافة قابلة للانقلاب لأعلى، بحجم واحد يلائم الجميع.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1477, N'قميص صوفي موحد للجنسين بأكمام طويلة للتسابق مصنوع من المايكروفيبر يحمل شعار AWC.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1478, N'ملائمة بشكل عام، وجيدة التهوية، وخفيفة الوزن بقناع واق من الشمس مزود بخاصية الالتصاق التلقائي.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1479, N'شنطة ظهر مائية متعددة الأغراض والاستعمالات تزن 1.960 كيلوجرام وتوفر مساحة تخزين إضافية، وإمكانية تعبئة سهلة، مع حزام وسط.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1480, N'تساعد الرداءات المحكمة المصنوعة من الاسباندكس المخصصة للقيادة في فصل الشتاء، مع تركيبة الشمواة الانسيابية على التخلص من مواضع الضغط.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1481, N'يمكنها إزالة أصعب أوساخ الطريق، وإذابة المواد الزيتية، كما أنها آمنة بيئيًا. زجاجة بسعة لتر واحد.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1482, N'أغلفة تناسب الإطارين الأمامي والخلفي، وتشمل حاملاً ومفتاحين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1484, N'Acier chromé.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1485, N'Cuvettes en alliage d''aluminium ; axe de grand diamètre.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1486, N'Cuvettes en alliage d''aluminium et axe creux.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1487, N'Adapté à tous les usages, sur route ou tout-terrain. Pour toutes les bourses. Changement de braquet en douceur et conduite confortable.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1488, N'Ce vélo offre un excellent rapport qualité-prix. Vif et facile à manœuvrer, il se conduit en toute tranquillité sur les chemins et les sentiers.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1489, N'Pour les véritables passionnés du VTT. Un vélo extrêmement robuste qui vous permettra d''aller partout, même sur les terrains difficiles, pour un budget raisonnable.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1490, N'Conduite sur terrains très accidentés. Idéal pour tous les niveaux de compétition. Utilise le même cadre HL que le Montain-100.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1491, N'VTT de compétition haut de gamme. Plusieurs options d''amélioration des performances : cadre HL, suspension avant particulièrement souple et traction adaptée à tous les terrains.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1492, N'Adapté à tous les terrains. Pour toutes les bourses.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1493, N'Vélo d''adulte d''entrée de gamme ; permet une conduite confortable en ville ou sur les chemins de campagne. Moyeux et rayons à blocage rapide.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1494, N'Vélo très séduisant comportant de nombreuses caractéristiques des modèles haut de gamme. Bénéficie du cadre léger et rigide, mais aussi de la ligne performante qui ont fait notre réputation.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1495, N'Équipé de la même technologie que tous nos vélos de route, avec un cadre femmes. Idéal pour la promenade ou la course sur route.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1496, N'Équipé de la même technologie que tous nos vélos de route. Idéal pour la promenade ou la course sur route.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1497, N'Un véritable vélo multi-sports, qui offre une conduite optimisée et une ligne révolutionnaire. Sa ligne aérodynamique vous permet de l''utiliser en course et ses vitesses de gravir les cols.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1498, N'VTT, course ou promenade entre amis sur un vélo aérodynamique et léger. Bénéficie d''un système de selle perfectionné qui offre un confort optimal.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1499, N'Tout terrain, course ou promenade entre amis sur un vélo aérodynamique et léger (cadre femmes). Bénéficie d''un système de selle perfectionné qui offre un confort optimal.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1500, N'Cadre en alliage d''aluminium qui offre une conduite légère et rapide, sur pistes ou sur routes de campagne.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1501, N'Ce vélo est destiné aux champions cyclistes. Mis au point par une équipe cycliste professionnelle, ce vélo possède un cadre en aluminium traité à chaud extrêmement léger et un guidon qui permet une conduite très précise.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1502, N'Vélo de qualité pour tous usages, doté d''un bon niveau de confort et de sécurité. Présente des pneus plus larges et plus stables pour les sorties en ville ou les randonnées du week-end.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1503, N'La selle rembourrée offre un confort optimal. Le porte-bagages nouvellement remanié offre diverses possibilités d''ajout de paniers ou de sacoches. Ce vélo reste parfaitement stable une fois chargé.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1504, N'Voyagez confortablement et avec élégance. Confort et sécurité maximum. Large éventail de vitesses pour gravir toutes les côtes. Sa fabrication en alliage d''aluminium haute technologie est synonyme de robustesse, sans ajout de poids.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1505, N'Système de changement de vitesse très performant.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1506, N'Axe très rigide.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1507, N'Manivelle haute résistance.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1508, N'Pédalier triple plateaux ; manivelle en aluminium ; changement de braquet impeccable.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1509, N'Patins de freinage pour tous les temps ; freinage renforcé par l''application d''une plus grande surface sur la jante.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1510, N'Conception liaison large.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1511, N'Conception robuste permettant d''absorber les chocs et d''offrir une conduite plus précise.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1512, N'Fourche composite pour route avec tube de direction en aluminium.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1513, N'Fourche pour route en carbone hautes performances avec bras incurvés.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1514, N'Utilise la même technologie de cadre que celle adoptée sur le cadre en aluminium ML.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1515, N'Le cadre ML est un cadre en aluminium traité à chaud fabriqué avec le même niveau de détail et de qualité que notre cadre HL. Il offre des performances exceptionnelles. Version hommes.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1516, N'Le cadre ML est un cadre en aluminium traité à chaud fabriqué avec le même niveau de détail et de qualité que notre cadre HL. Il offre des performances exceptionnelles. Version femmes.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1517, N'Doté du même alliage en aluminium que notre cadre HL haut de gamme, le ML possède un tube léger dont le diamètre est prévu pour offrir une résistance optimale. Version femmes.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1518, N'Roue de secours tout-terrain pour vététiste occasionnel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1519, N'Roue de secours tout-terrain pour vététiste amateur à confirmé.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1520, N'Roue de secours tout-terrain hautes performances.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1521, N'Roue avant pour vélo de route pour cycliste occasionnel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1522, N'Alliage robuste avec moyeu à blocage rapide.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1523, N'Roue solide avec jante double paroi.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1524, N'Jantes aérodynamiques pour conduite en souplesse.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1525, N'Barre d''appui tous usages pour conduite sur route ou tout-terrain.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1526, N'Barres d''appui en alliage d''aluminium solide pour descente.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1527, N'Barre d''appui plate suffisamment solide pour le circuit professionnel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1528, N'Forme très réussie destinée à faciliter l''accès aux leviers.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1529, N'Barre d''appui avec tube en aluminium ergonomique pour répondre aux besoins de tous les cyclistes.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1530, N'Conçu pour la compétition ; barre d''appui ergonomique haut de gamme en alliage d''aluminium.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1531, N'Forme très réussie destinée à diminuer la fatigue des cyclistes occasionnels.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1532, N'Barre d''appui en aluminium légère et solide pour les longues randonnées.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1533, N'Jeu de direction de qualité sans filetage à un prix abordable.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1534, N'Boîtier de protection hermétique.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1535, N'Jeu de direction sans filetage 2,54 cm de grande qualité avec un dispositif de lubrification rapide.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1536, N'Plate-forme allongée permettant de rouler avec tous types de chaussures ; idéal pour la randonnée.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1537, N'Pédales automatiques légères et robustes avec tension réglable.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1538, N'Acier inoxydable ; facile à nettoyer.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1539, N'Pédales automatiques - aluminium.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1540, N'Fabrication en alliage d''aluminium léger.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1541, N'Pédales automatiques haut de gamme avec tension réglable.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1542, N'Pédale stable pour longs trajets.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1543, N'Bouchon de remplissage ; étriers en aluminium poli.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1544, N'Dérailleur en aluminium 10 vitesses avec supports de galet hermétiques.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1545, N'Roue de secours tout-terrain arrière pour vététiste occasionnel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1546, N'Roue de secours tout-terrain arrière pour vététiste amateur à confirmé.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1547, N'Jantes très robustes, solidité garantie.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1548, N'Roue de secours arrière pour cycliste occasionnel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1549, N'Jante en alliage d''aluminium avec rayons en acier inoxydable ; pour une vitesse optimale.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1550, N'Roue arrière solide avec jante doublée.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1551, N'Excellentes jantes aérodynamiques pour une conduite en souplesse.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1552, N'Cuir synthétique. Confort amélioré grâce au couvre-selle en gel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1553, N'Conçu pour absorber les chocs.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1554, N'Conception ergonomique pour randonnée longue distance confortable. Cuir de qualité.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1555, N'Selle légère avec renfort en mousse.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1556, N'Amortisseurs en caoutchouc pour absorber les bosses.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1557, N'Selle de course légère en Kevlar. Cuir.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1558, N'Selle confortable et ergonomique profilée avec couvre-selle en gel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1559, N'Nouvelle conception pour éviter la fatigue lors des longues randonnées.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1560, N'Cadre profilé pour améliorer le confort.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1561, N'Traction comparable aux modèles haut de gamme, gomme de pneu moins chère.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1562, N'Grande traction, caoutchouc haute densité.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1563, N'Traction exceptionnelle ; carbone léger renforcé.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1564, N'Même dessin que les pneus plus chers, mais doté d''une gomme moins chère.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1565, N'Caoutchouc plus dense que sur les autres modèles.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1566, N'Carbone léger renforcé pour un confort de conduite inégalé, sans ajout de poids.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1567, N'Caoutchouc haute densité.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1568, N'Tube à autovulcanisation.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1569, N'Tube classique tous usages.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1570, N'Tube à usage générique.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1571, N'Le cadre LL en aluminium offre une conduite confortable, une excellente absorption des bosses pour un très bon rapport qualité-prix.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1572, N'Doté du même alliage en aluminium que notre cadre HL haut de gamme, le ML possède un tube léger dont le diamètre est prévu pour offrir une résistance optimale. Version hommes.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1573, N'Notre cadre en aluminium plus léger et de qualité supérieure fabriqué à partir du tout dernier alliage ; cadre soudé et traité à chaud pour une meilleure résistance. Le résultat d''une conception innovante pour un confort et des performances maximum.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1574, N'Cadre renforcé léger pour une position plus droite ; idéal pour les promenades en ville. Ligne remarquable pour un confort optimal.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1575, N'Le cadre HL est profilé afin d''associer élégance et robustesse ; il est prévu pour résister à une utilisation quotidienne intensive. Version hommes.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1576, N'Éclairage peu onéreux pour la conduite de nuit - utilise 3 piles AAA.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1577, N'Le porte-bidon en aluminium est plus léger que la version VTT ; idéal pour les longues randonnées.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1578, N'Bidon d''eau avec le logo de l''équipe AWC - capacité 0,75 litre, entièrement étanche.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1579, N'Porte-vélo sécurisé pour 4 vélos ; fabrication en acier ; adaptable sur attache-remorque.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1580, N'Garde-boue adapté à la plupart des VTT.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1581, N'Combinaison de fibres naturelles et synthétiques ; reste sèche et offre le confort nécessaire.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1582, N'Très pratique. Tient dans la poche. Corps en aluminium. 11,2 bars.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1583, N'Conçu pour l''équipe professionnelle AWC, à bretelles, contrôle de l''humidité, peau de chamois et bande anti-remontée.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1584, N'Fabrication en nylon résistant étanche d''accès facile. Suffisamment grand pour les randonnées du week-end.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1585, N'Entièrement rembourré, amélioration de la souplesse de mouvement des doigts, paume renforcée, fermeture réglable.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1586, N'Paume synthétique, poignets souples, ouverture maillée anti-transpiration. Adopté par les professionnels de l''équipe AWC.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1587, N'Cuissards résistants à l''usure pour utilisation intensive, doublés à l''intérieur en Spandex, sans couture, peau de chamois anti-bactérie pour un meilleur confort.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1588, N'Comprend 8 rustines de tailles différentes, de la colle et du papier de verre.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1589, N'Sacs légers et résistants au vent ; tiennent dans la poche.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1590, N'Cuissards de course pour hommes - en Spandex avec un élastique à la ceinture et bande anti-remontée.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1591, N'Support de type home trainer idéal pour vous entraîner en intérieur (pour tous les modèles de vélo). Fixations rapides, fabrication en acier.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1592, N'Feu avant rechargeable à deux faisceaux.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1593, N'Feu avant robuste.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1594, N'Maillot manches courtes classique et anti-transpiration avec contrôle de l''humidité, fermeture avant à glissière et 3 poches arrière.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1595, N'Simple et léger. Rustines de secours rangées dans la poignée.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1596, N'Fin, léger et résistant avec des poignets qui restent en place.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1597, N'Porte-bidon en aluminium robuste qui maintient le bidon sur les terrains accidentés.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1598, N'Style classique avec une visière relevable ; taille unique.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1599, N'Maillot de cycliste en microfibre avec le logo de l''équipe AWV, manches longues, unisexe.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1600, N'Légère, aérée, taille unique, avec une visière amovible.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1601, N'Sac d''hydratation de 3 l polyvalent. Permet de stocker des aliments supplémentaires. Facile d''accès et fourni avec une ceinture.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1602, N'Collants chauds en Spandex pour l''hiver ; fabrication en chamoisine sans couture pour éliminer les points de frottement.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1603, N'Nettoie les saletés ; dissout la graisse. Protège l''environnement. Bouteille d''1 litre.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1604, N'S''enroule pour s''adapter aux pneus avant et arrière, sac et deux clés fournis.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1605, N'Chaque cadre est fabriqué artisanalement dans notre atelier de Bordeaux afin d''obtenir le diamètre et l''épaisseur adaptés à un vélo tout-terrain de premier choix. Le cadre en aluminium soudé à chaud présente un tube d''un plus grand diamètre, afin d''absorber les bosses.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1606, N'โลหะโครโมลี')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1607, N'ดุมอลูมิเนียมอัลลอยด์ แกนเพลาขนาดใหญ่')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1608, N'ดุมอลูมิเนียมอัลลอยด์และเพลากลวง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1609, N'เหมาะสำหรับการขี่ทุกประเภท ทั้งบนถนนและแบบออฟโรด  ในราคาย่อมเยา เปลี่ยนเกียร์อย่างนุ่มนวล พร้อมการขับขี่ที่แสนสบาย')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1610, N'จักรยานรุ่นนี้มีประสิทธิภาพสูง ในราคาประหยัด  ควบคุมง่าย ทันใจ และให้ความมั่นใจเปี่ยมล้นเมื่อคุณต้องการขี่แบบออฟโรด')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1611, N'สำหรับนักปั่นทางวิบากตัวจริง  เป็นจักรยานที่ทนทานเป็นเยี่ยม พร้อมสำหรับทุกเส้นทาง มีระบบบังคับทิศทางที่สมบูรณ์แบบในพื้นที่วิบาก โดยไม่ทำให้คุณกระเป๋าฉีก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1612, N'สำหรับการขี่ในเส้นทางผจญภัย  เหมาะที่สุดสำหรับการแข่งขันทุกระดับ  ใช้เฟรม HL เช่นเดียวกับในรุ่น Mountain-100')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1613, N'สุดยอดจักรยานภูเขาสำหรับการแข่งขัน สมบูรณ์แบบด้วยองค์ประกอบสำหรับประสิทธิภาพสูงสุด เช่น เฟรม HL นวัตกรรมใหม่ ระบบกันกระเทือนด้านหน้าที่นุ่มนวลเป็นพิเศษ และยางที่ยึดเกาะทุกสภาพเส้นทาง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1614, N'เหมาะสำหรับเส้นทางวิบากทุกประเภท  ในราคาย่อมเยา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1615, N'จักรยานระดับเริ่มต้นสำหรับผู้ใหญ่ ให้ความสบายในการขับขี่แม้ในเส้นทางทุรกันดารหรือในเมือง  ดุมและขอบล้อถอดได้สะดวก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1616, N'จักรยานที่ให้ความคุ้มค่า ด้วยคุณสมบัติพิเศษมากมายจากรุ่นที่ดีที่สุด  พร้อมเฟรมที่เบา แกร่ง และอัตราเร่งที่สร้างชื่อเสียงให้กับเรา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1617, N'เทคโนโลยีเดียวกับจักรยานรุ่น Road ทั้งหมด แต่ใช้เฟรมขนาดกะทัดรัดสำหรับสุภาพสตรี  เหมาะสำหรับการขับขี่อเนกประสงค์ ไม่ว่าจะเป็นท้องถนนทั่วไปหรือการแข่งขัน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1618, N'เทคโนโลยีเดียวกับจักรยานรุ่น Road ทั้งหมด  เหมาะสำหรับการขับขี่อเนกประสงค์ ไม่ว่าจะเป็นท้องถนนทั่วไปหรือการแข่งขัน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1619, N'จักรยานแข่งขันอเนกประสงค์ที่ให้การขับขี่ที่คล่องตัว และรูปลักษณ์ที่ไม่ซ้ำใคร  การออกแบบแอโรไดนามิค ช่วยให้คุณเทียบชั้นกับมืออาชีพ พร้อมกับระบบเกียร์ที่จะนำคุณทะยานเหนือเส้นทางบนภูเขาได้สบาย')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1620, N'ออกกำลัง แข่งขัน หรือสนุกสนานไปบนจักรยานคันเก๋ในรูปทรงแอโรไดนามิค  พร้อมด้วยเทคโนโลยีเบาะนั่งชั้นสูงที่ช่วยให้คุณขี่อย่างสบายได้ทั้งวัน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1621, N'ออกกำลัง แข่งขัน หรือสนุกสนานไปบนจักรยานคันเก๋ในรูปทรงแอโรไดนามิค ได้รับการออกแบบสำหรับนักปั่นสตรีโดยเฉพาะ  พร้อมด้วยเทคโนโลยีเบาะนั่งชั้นสูงที่ช่วยให้คุณขี่อย่างสบายได้ทั้งวัน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1622, N'เฟรมอลูมิเนียมอัลลอยด์ ให้การขับขี่ที่เบา มั่นคง ไม่ว่าคุณจะแข่งอยู่ในเวโลโดรม หรือเผชิญกับเส้นทางที่ท้าทายเพียงใด')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1623, N'จักรยานคู่ใจของนักแข่งระดับแชมเปี้ยน  ได้รับการพัฒนาโดยทีมแข่ง Adventure Works Cycles ใช้เฟรมอลูมิเนียมผ่านความร้อนซึ่งเบาเป็นพิเศษ และการบังคับที่แม่นยำ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1624, N'จักรยานอเนกประสงค์สุดคุ้ม พร้อมความสบายและความปลอดภัยมาตรฐาน  ใช้ยางขนาดใหญ่ ให้ความมั่นใจในการขับขี่ในเมืองหรือในการท่องเที่ยวสุดสัปดาห์')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1625, N'เบาะนั่งหุ้มพิเศษช่วยให้คุณขี่อย่างสบายตลอดทั้งวัน ตะแกรงออกแบบใหม่ มีพื้นที่เหลือเฟือสำหรับตะกร้าและกระเป๋า ขับขี่อย่างมั่นคง แม้ขณะบรรทุกของ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1626, N'เดินทางอย่างมาดมั่นและสะดวกสบาย  ได้รับการออกแบบเพื่อให้ปลอดภัยและขับขี่สบายสูงสุด  ช่วงเกียร์กว้าง พร้อมทุกเส้นทางลาดชัน  โครงสร้างอลูมิเนียมอัลลอยด์จากวิทยาการขั้นสูง ให้ความทนทานและมีน้ำหนักเบา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1627, N'ประสิทธิภาพการเปลี่ยนเกียร์ที่เหนือกว่า')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1628, N'เพลาล้อสุดแกร่ง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1629, N'คันบังคับแข็งแรงเป็นพิเศษ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1630, N'ชุดควบคุมสามชั้น คันบังคับอลูมิเนียม และการเปลี่ยนเกียร์ที่สมบูรณ์แบบ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1631, N'ยางเบรกสำหรับทุกสภาพอากาศ เพื่อการหยุดที่มั่นใจ ด้วยหน้าสัมผัสกับขอบล้อมากกว่า')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1632, N'การออกแบบให้มีจุดเชื่อมกว้าง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1633, N'การออกแบบที่มั่นคง รับแรงกระแทกและให้การควบคุมที่แม่นยำ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1634, N'ตะเกียบคู่โลหะผสม พร้อมท่อคันบังคับอลูมิเนียม')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1635, N'ตะเกียบคู่คาร์บอนประสิทธิภาพสูง พร้อมขาโค้ง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1636, N'ความคุ้มค่าที่มาพร้อมกับเทคโนโลยีการออกแบบเฟรมเหมือนกับเฟรมอลูมิเนียม ML')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1637, N'เฟรม ML เป็นเฟรมอลูมิเนียมผ่านความร้อน สร้างอย่างพิถีพิถันเช่นเดียวกับเฟรม HL  ให้ประสิทธิภาพที่เหนือกว่า  เวอร์ชันสำหรับสุภาพบุรุษ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1638, N'เฟรม ML เป็นเฟรมอลูมิเนียมผ่านความร้อน สร้างอย่างพิถีพิถันเช่นเดียวกับเฟรม HL  ให้ประสิทธิภาพที่เหนือกว่า  เวอร์ชันสำหรับสุภาพสตรี')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1639, N'เฟรมแต่ละชิ้นผ่านการสร้างด้วยมือในโรงงาน Bothell เพื่อให้ได้ขนาดและความหนาที่เหมาะที่สุดสำหรับเฟรมจักรยานภูเขาชั้นดี  เฟรมอลูมิเนียมหล่อด้วยความร้อน มีโพรงขนาดใหญ่ซึ่งช่วยซึมซับแรงกระแทก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1640, N'สร้างจากอลูมิเนียมอัลลอยชนิดเดียวกับสุดยอดเฟรม HL เฟรม ML นี้มีน้ำหนักเบา สร้างขึ้นตามขนาดที่ให้ความแข็งแกร่งเป็นเลิศ  เวอร์ชันสำหรับสุภาพสตรี')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1641, N'ล้ออะไหล่จักรยานภูเขาสำหรับนักปั่นระดับเริ่มต้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1642, N'ล้ออะไหล่จักรยานภูเขาสำหรับนักปั่นสมัครเล่นไปจนถึงมืออาชีพ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1643, N'ล้ออะไหล่จักรยานภูเขาประสิทธิภาพสูง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1644, N'ล้อหน้าจักรยานภูเขาอะไหล่สำหรับนักปั่นระดับเริ่มต้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1645, N'ดุมล้ออัลลอยด์ที่ทนทาน ถอดได้อย่างรวดเร็ว')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1646, N'ล้อที่แข็งแกร่งพร้อมขอบล้อผนังสองชั้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1647, N'ขอบล้อแอโรไดนามิคสำหรับการขับขี่อย่างนุ่มนวล')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1648, N'คันบังคับอเนกประสงค์สำหรับทุกเส้นทาง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1649, N'คันบังคับอลูมิเนียมอัลลอยด์ แกร่งสำหรับเส้นทางลาดชัน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1650, N'คันบังคับแบน แข็งแรงพอสำหรับสนามโปรเซอร์กิต')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1651, N'รูปร่างที่เป็นเอกลักษณ์ ช่วยให้ใช้คานบันไดได้สะดวก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1652, N'คานอลูมิเนียมที่ออกแบบตามหลักสรีรศาสตร์ เหมาะสำหรับนักปั่นทุกคน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1653, N'คันบังคับชั้นเยี่ยม ได้รับการออกแบบตามหลักสรีรศาสตร์สำหรับนักแข่ง ทำจากอลูมิเนียมอัลลอยด์')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1654, N'รูปร่างที่เป็นเอกลักษณ์ช่วยลดความล้าสำหรับนักปั่นระดับเริ่มต้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1655, N'คันบังคับอลูมิเนียมแข็งแกร่ง เบา เหมาะสำหรับการขี่ระยะไกล')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1656, N'ชุดประกอบส่วนหัวไม่มีเกลียว คุณภาพสูงในราคาประหยัด')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1657, N'กล่องผนึกสนิทป้องกันฝุ่นละออง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1658, N'ชุดประกอบส่วนหัวไม่มีเกลียวคุณภาพสูง ขนาด 1 นิ้วพร้อมช่องจาระบีสำหรับการหล่อลื่นอย่างรวดเร็ว')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1659, N'คันถีบขนาดใหญ่ สำหรับการขับขี่ด้วยรองเท้าชนิดใดก็ได้ เหมาะสำหรับการขับขี่อเนกประสงค์')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1660, N'คันถีบน้ำหนักเบา ทนทาน ไม่มีคลิป และสามารถปรับความตึงได้')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1661, N'โลหะสเตนเลส ได้รับการออกแบบให้สลัดโคลนออกได้ง่าย')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1662, N'คันถีบไม่มีคลิป - อลูมิเนียม')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1663, N'โครงสร้างอลูมิเนียมอัลลอยด์น้ำหนักเบา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1664, N'คันถีบระดับเยี่ยม ไม่มีคลิป ปรับความตึงได้')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1665, N'คันถีบที่มั่นคง สำหรับการขี่เป็นเวลานาน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1666, N'รองเท้าถอดเปลี่ยนพื้นด้านในได้ พร้อมแคลิเปอร์อลูมิเนียมขัดเงา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1667, N'ชุดเฟืองอลูมิเนียม 10 สปีดพร้อมตลับลูกปืนรอกผนึกแน่นหนา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1668, N'ล้อหลังอะไหล่จักรยานภูเขาสำหรับนักปั่นระดับเริ่มต้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1669, N'ล้อหลังอะไหล่จักรยานภูเขาสำหรับนักปั่นยามว่างไปจนถึงมืออาชีพ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1670, N'ขอบล้อแข็งแกร่งเป็นพิเศษเพื่อความทนทานที่รับประกันได้')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1671, N'ล้อหลังอะไหล่สำหรับนักปั่นระดับเริ่มต้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1672, N'ขอบอลูมิเนียมอัลลอยด์พร้อมซี่ล้อสเตนเลส สำหรับความเร็วสูง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1673, N'ล้อหลังที่แข็งแกร่งพร้อมขอบล้อผนังสองชั้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1674, N'ขอบล้อแอโรไดนามิคสำหรับการขับขี่ที่นุ่มนวล')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1675, N'หนังสังเคราะห์  พร้อมเจลนุ่มสบายเป็นพิเศษ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1676, N'ได้รับการออกแบบเพื่อซึมซับแรงกระแทก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1677, N'ออกแบบตามหลักสรีรศาสตร์ สำหรับการขับขี่อย่างสบายตลอดทั้งวัน  ทำจากหนังที่ทนทาน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1678, N'เบาะที่นั่งโฟมน้ำหนักเบา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1679, N'กันชนยางกันแรงกระแทก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1680, N'เบาะนั่งเคฟลาร์สำหรับแข่ง น้ำหนักเบา ทำจากหนัง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1681, N'เบาะนั่งบรรจุเจล นุ่มสบาย มีรูปทรงตามหลักสรีรศาสตร์')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1682, N'ออกแบบใหม่เพื่อบรรเทาแรงกดสำหรับการขับขี่ระยะไกล')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1683, N'เปลือกพิเศษสำหรับการขับขี่ที่แสนสบาย')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1684, N'แรงยึดเกาะเทียบชั้นกับรุ่นระดับสูง ใช้โครงไวร์บีดราคาประหยัด')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1685, N'แรงยึดเกาะที่เหนือกว่า ด้วยยางความหนาแน่นสูง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1686, N'เกาะถนนเหลือเชื่อ เสริมคาร์บอนน้ำหนักเบา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1687, N'ดอกยางชั้นยอดเช่นเดียวกับยางราคาแพง แต่ใช้โครงไวร์บีดราคาประหยัด')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1688, N'ใช้ยางความหนาแน่นสูงกว่ารุ่นอื่นๆ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1689, N'เสริมคาร์บอนน้ำหนักเบา สำหรับการขับขี่ที่เหนือกว่า โดยไม่เสียเปรียบในด้านน้ำหนัก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1690, N'ยางความหนาแน่นสูง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1691, N'จุ๊บยางผนึกด้วยตนเอง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1692, N'จุ๊บอเนกประสงค์แบบเดิม')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1693, N'จุ๊บสำหรับการใช้งานทั่วไป')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1694, N'เฟรม LL ให้ความปลอดภัยและสบายขณะขับขี่ พร้อมทั้งซึมซับแรงกระแทก คุ้มราคาสำหรับเฟรมอลูมิเนียม')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1695, N'สร้างจากอลูมิเนียมอัลลอยชนิดเดียวกับสุดยอดเฟรม HL เฟรม ML นี้มีน้ำหนักเบา มีขนาดที่ให้ความแข็งแกร่งเป็นเลิศ  เวอร์ชันสำหรับสุภาพบุรุษ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1696, N'เฟรมอลูมิเนียมคุณภาพสูงสุดและน้ำหนักเบาที่สุด สร้างจากอัลลอยด์ชนิดใหม่ ได้รับการหลอมและให้ความร้อนเพื่อความแกร่ง  ด้วยนวัตกรรมการออกแบบ ทำให้มีความสบายและประสิทธิภาพสูงในเวลาเดียวกัน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1697, N'เฟรมอลูมิเนียมน้ำหนักเบา ทำให้ท่วงท่าในการขับขี่ยืดตรง สำหรับทางเรียบ  ปฏิวัติการออกแบบเพื่อความสบายสูงสุด')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1698, N'เฟรมอลูมิเนียม HL ได้รับการออกแบบอย่างพิถีพิถัน เพื่อรูปลักษณ์และความแข็งแกร่ง พร้อมผจญทุกเส้นทางในการขับขี่ประจำวัน  เวอร์ชันสำหรับสุภาพบุรุษ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1699, N'ไฟราคาประหยัดสำหรับการขับขี่ยามค่ำคืนที่ปลอดภัย - ใช้แบตเตอรี่ AAA 3 ก้อน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1700, N'โครงอลูมิเนียมมีน้ำหนักเบากว่ารุ่นภูเขา เหมาะสำหรับการเดินทางระยะไกล')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1701, N'ขวดน้ำติดโลโก้ AWC - บรรจุ 30 ออนซ์ พร้อมป้องกันการรั่วไหล')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1702, N'บรรทุกรถจักรยาน 4 คันอย่างปลอดภัย ด้วยโครงสร้างเหล็กกล้า ใช้กับขอเกี่ยวขนาด 2 นิ้ว')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1703, N'แผ่นป้องกันแบบคลิปออน สำหรับจักรยานภูเขาทุกรุ่น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1704, N'การผสมผสานไฟเบอร์จากธรรมชาติและสังเคราะห์ แห้งสนิทและให้การรองรับที่เหมาะสม')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1705, N'ออกแบบเพื่อความสะดวก  ขนาดกะทัดรัด ใส่พอดีกระเป๋า  กระบอกอลูมิเนียม  ขนาด 160psi')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1706, N'ได้รับการออกแบบสำหรับทีม AWC พร้อมสายรัด ตัวควบคุมความชื้น บุชามัวส์ และที่รัดขา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1707, N'ทนทาน ทำจากไนลอนกันน้ำ ใช้งานง่าย  มีขนาดใหญ่พอสำหรับการเดินทางในช่วงสุดสัปดาห์')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1708, N'บุเต็มทุกส่วน เพิ่มการยืดหยุ่นของนิ้ว แผ่นฝ่ามือทนทาน ที่รัดปรับได้')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1709, N'แผ่นฝ่ามือสังเคราะห์ ข้อนิ้วยืดหยุ่น และตาข่ายส่วนบนช่วยระบายอากาศ  ได้รับความไว้วางใจจากนักแข่งทีม AWC')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1710, N'กางเกงขาสั้น ทนการขีดข่วน ทำจากเส้นใยสแปนเด็กซ์ไร้ตะเข็บ พร้อมด้วยชามัวส์ป้องกันแบคทีเรีย สวมสบาย')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1711, N'ประกอบด้วยแผ่นปะยาง 8 ขนาด กาว และกระดาษทราย')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1712, N'น้ำหนักเบา กันลม ขนาดกะทัดรัดพอดีกระเป๋า')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1713, N'กางเกงแข่ง 8 ช่องสำหรับสุภาพบุรุษ - เส้นใยสแปนเด็กซ์พร้อมยางยืดและที่รัดขา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1714, N'ขาตั้งจักรยานอเนกประสงค์สำหรับจักรยานคันโปรดของคุณที่บ้าน  ที่ยึดปรับสะดวก และโครงสร้างเหล็กกล้า')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1715, N'ไฟหน้าสองดวงแบบชาร์จซ้ำได้')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1716, N'ไฟหน้าแบบทนทาน กันน้ำ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1717, N'เสื้อแขนสั้นแบบคลาสสิค ระบายอากาศได้ดี ควบคุมความชื้น ซิปหน้า พร้อมกระเป๋าหลัง 3 ใบ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1718, N'เรียบง่าย น้ำหนักเบา  ที่ปะยางฉุกเฉินเก็บได้ในคันบังคับ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1719, N'บาง น้ำหนักเบา และทนทาน พร้อมส่วนพับที่ไม่หลุด')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1720, N'โครงอลูมิเนียมแข็งแกร่ง สำหรับใส่ขวดน้ำในเส้นทางวิบาก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1721, N'แบบดั้งเดิม พร้อมขอบพับ ใช้ได้กับทุกคน')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1722, N'เสื้อไมโครไฟเบอร์ติดโลโก้ AWC')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1723, N'แว่นกันลมขนาดสากล ระบายอากาศได้ดี น้ำหนักเบา')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1724, N'แพ็คขนาด 70 ออนซ์ บรรจุเครื่องดื่มสำรอง เติมง่าย พร้อมสายรัดเอว')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1725, N'กางเกงสแปนเด็กซ์ให้ความอบอุ่นในการขับขี่หน้าหนาว บุชามัวส์ไร้ตะเข็บสำหรับจุดกดทับ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1726, N'ทำความสะอาดสิ่งสกปรกที่ฝังแน่น ละลายจาระบี และไม่เป็นอันตรายต่อสิ่งแวดล้อม  ขวดบรรจุ 1 ลิตร')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1727, N'ที่หุ้มยางสำหรับยางหน้าหรือยางหลัง พร้อมที่ใส่และกุญแจสองดอก')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1728, N'פלדת כרומולי (כרום-מולובדניום)')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1729, N'כיסויים מסגסוגת אלומיניום; ציר רחב-קוטר.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1730, N'כיסויים מסגסוגת אלומיניום וציר חלול.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1731, N'מתאימים לכל סוג רכיבה, בדרכים סלולות ובשבילים כאחת. מתאימים לכל תקציב. החלפת הילוכים חלקה יחד עם רכיבה נוחה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1732, N'אופניים אלה מספקים רמת ביצועים גבוהה במחיר סביר.  הם נענים וקלים לתמרון ומציעים "ראש שקט" כשתחליט לרכב במשעולים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1733, N'מתאימים למכורים לרכיבת שטח של ממש. אופניים עמידים להפליא שניתן להגיע אתם לכל מקום מבלי לאבד את השליטה בשטח מאתגר – בלי לגרום לחור בתקציב.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1734, N'לרכיבה רצינית בשטחים נידחים. מתאימים לכל רמה של תחרות. באופניים אלה נעשה שימוש ב- HL Frame (מסגרת HL) זהה לזו המצויה בדגם Mountain-100 (100-הררי)')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1735, N'אופני הרים תחרותיים מהשורה הראשונה. אפשרויות לשיפור ביצועים הכוללות HL Frame (מסגרת (HL, שיכוך קדמי חלק במיוחד ואחיזת כביש המתאימה לכל סוגי השטח.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1736, N'מתאימים לכל סוג רכיבה בשבילים. מתאימים לכל תקציב.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1737, N'אופני מבוגרים למתחילים; מציעים רכיבה נוחה "מחוף לחוף" או לאורך הרחוב. טבורים וחישורים לשחרור מהיר.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1738, N'אופניים המהווים תמורה נאותה למחירם ובעלי רבות מתכונות דגמי הקו המוביל שלנו. מאופיינים במסגרת המוצקה והקלה וההאצה המהירה שבהם אנו מפורסמים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1739, N'טכנולוגיה זהה לזו המצוי באופני סדרת "כביש" שלנו, אולם המסגרת בגודל המותאם לנשים. מושלמים כאופניים לכל מטרה לכביש ולתחרויות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1740, N'טכנולוגיה זהה לזו המצויה באופני סדרת "כביש" שלנו. מושלמים כאופניים לכל מטרה לכביש ולתחרויות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1741, N'אופניים שבאמת מתאימים לסוגי ספורט מרובים המציעים רכיבה חלקה ועיצוב מהפכני. עיצוב אווירודינמי מאפשר רכיבה עם מקצוענים ותשלובת גלגלי השיניים תתמודד עם כל דרך הררית.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1742, N'חציית שטחים, תחרות או סתם בילוי חברתי על אופניים מלוטשים ואווירודינמיים. טכנולוגיה מתקדמת של המושב מספקת נוחות לאורך כל היום.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1743, N'חציית שטחים, תחרות או סתם בילוי חברתי על אופניים בעלי עיצוב מלוטש ואווירודינמי לנשים. טכנולוגיה מתקדמת של המושב מספקת נוחות לאורך כל היום.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1744, N'מסגרת מסגסוגת אלומיניום מספקת רכיבה קלה ומוצקה, בין אם במסלול מרוצים ובין אם ברכיבת מועדון תובענית בדרכים כפריות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1745, N'אופניים אלה משמשים את מנצחי התחרויות. לאופניים, שפותחו יחד עם צוות המרוצים של Adventure Works Cycles (אופני חוויה), יש מסגרת אלומיניום קלה ביותר שהוקשחה בחום והיגוי המאפשר שליטה מדויקת.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1747, N'אופני איכות לכל מטרה עם תכונות הנוחות והבטיחות הבסיסיות שלנו. מציעים צמיגים רחבים ויציבים יותר לרכיבה ברחבי העיר או לטיולי סופשבוע.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1748, N'המושב המשובח והמותאם אישית מאפשר רכיבה לאורך היום כולו וקיים מקום רב להוספת תיקי אוכף ותיקי אופניים למנשא שעוצב מחדש. אופניים אלה שומרים על יציבות גם בעומס מלא.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1749, N'רכיבה מסוגננת ונוחה. עיצוב המיועד למרב הנוחות והבטיחות. טווח רחב של מערכות הילוכים מצליח להתגבר על כל השיפועים. מבנה סגסוגת אלומיניום עתיר ידע מספק עמידות ללא תוספת משקל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1750, N'ביצועי מעולים בחילוף הילוכים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1751, N'ציר קשיח במיוחד.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1752, N'זרוע ארכובה עתירת עוצמה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1753, N'ערכת ארכובה משולשת, זרוע ארכובה מאלומיניום, חילוף הילוכים מושלם.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1754, N'רפידות מעצור המתאימות לכל מזג אוויר; מספקות עצירה מעולה על-ידי החלת יותר שטח לחישוק.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1755, N'עיצוב רחב-חוליות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1756, N'עיצוב חזק סופג זעזועים ומציע היגוי מדויק יותר.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1757, N'מזלג כביש מורכב עם צינור עמוד היגוי מאלומיניום.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1758, N'מזלג כביש מפחמן עם רגליים מוטות לביצועים גבוהים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1759, N'האיכות הטובה ביותר שלנו שמנצלת טכנולוגיית מסגרת חדשנית זהה לזו שקיימת במסגרת ML מאלומיניום.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1760, N'מסגרת ה- ML היא מסגרת אלומיניום מוקשח בחום המיוצרת באותה הקפדה ואיכות כמו מסגרות HL שלנו. היא מציעה ביצועים מעולים. גירסה לגברים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1761, N'מסגרת ה- ML היא מסגרת אלומיניום מוקשח בחום המיוצרת באותה הקפדה ואיכות כמו מסגרות HL שלנו. היא מציעה ביצועים מעולים. גירסה לנשים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1763, N'תאורה במחיר סביר לרכיבה לילית בטוחה – פועלת באמצעות סוללת AAA')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1764, N'כלוב האלומיניום קל יותר מאשר הגירסה ההררית שלנו; מושלם עבור רכיבה למרחקים ארוכים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1765, N'בקבוק מים מתוצרת AWC – מכיל 850 גרם; עמיד מפני נזילה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1766, N'נושא 4 זוגות אופניים בבטחה; מבנה פלדה; מתאים לרתמת מתלה של 2 אינץ''.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1767, N'פגושים מתחברים המתאימים לרוב האופנים לרכיבה הררית.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1768, N'כל מסגרת מיוצרת ידנית במפעל שלנו ב- Bothell, לקבלת הקוטר ועובי הדופן האופטימליים שנדרשים למסגרת הטובה ביותר לרכיבה הררית. למסגרת האלומיניום המרותכת שהוקשחה בחום, יש צינור בקוטר גדול יותר הסופג את החבטות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1770, N'מסגרת ה- ML, העשויה סגסוגת אלומיניום זהה לזו המשמשת ליצור מסגרת HL מהקו המוביל שלנו, מתאפיינת בצלע תחתונה קלת משקל החרוטה לקוטר המושלם הנדרש לחוזק אופטימלי. גירסה לנשים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1771, N'גלגל חלופי לרכיבה הררית לרוכב המתחיל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1772, N'גלגל חלופי לרכיבה הררית לרוכב החובב והרציני.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1773, N'גלגל חלופי עתיר ביצועים לרכיבה הררית.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1774, N'גלגל חלופי קדמי לרכיבת כביש לרוכב המתחיל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1775, N'סגסוגת איתנה מאפיינת את מנגנון השחרור המהיר של טבור האופן.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1776, N'גלגל חזק עם חישוק בעל הלחמה כפולה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1777, N'חישוקים אווירודינמיים לרכיבה חלקה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1778, N'ידיות לכל מטרה לרכיבת כביש ולשבילים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1779, N'ידיות קשיחות מסגסוגת אלומיניום לרכיבה במורד.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1780, N'ידית שטוחה החזקה מספיק עבור הרוכב המקצועי.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1781, N'צורה ייחודית מספקת גישה קלה יותר לידיות ההילוכים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1782, N'צינור אלומיניום לידית המעוצבת בצורה אנטומית שתתאים לכל רוכב.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1783, N'עיצוב המיוחד למתחרים; ידית אנטומית המעוצבת בצורה איכותית מסגסוגת אלומיניום.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1784, N'צורה ייחודית המפחיתה מעייפות הרוכב המתחיל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1785, N'ידית אלומיניום קלה אך עם זאת קשיחה לרכיבה למרחקים ארוכים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1786, N'טבעת נטולת תבריג מספקת איכות במחיר חסכוני.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1787, N'קסטה אטומה שומרת מפני לכלוך.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1788, N'טבעת נטולת תבריג בקוטר 1 אינץ'' עם פתח סיכה לסיכה מהירה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1789, N'משטח דוושות מורחב המתאים לרכיבה בכל סוגי הנעלים; מצוין לרכיבה לכל מטרה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1790, N'דוושה עמידה נטולת חבקים עם מאמץ מתכוונן.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1791, N'פלדת אל-חלד; עיצוב המשיר בוץ בקלות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1792, N'דוושות ללא חבקים – אלומיניום.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1793, N'מבנה סגסוגת אלומיניום קל משקל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1794, N'דוושות נטולות חבקים מהשורה הראשונה עם מאמץ מתכוונן.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1795, N'דוושה יציבה המתאימה לרכיבה לאורך כל היום.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1796, N'נעליים למילוי מחדש; חבקים מאלומיניום מלוטש.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1797, N'מעתק אלומיניום ל- 10 מהירויות עם גלגלת מסבים אטומה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1799, N'גלגל חלופי אחורי לרכיבה הררית לרוכב המתחיל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1800, N'גלגל חלופי אחורי לרכיבה הררית לרוכב החובב והרציני.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1801, N'חישוקים חזקים במיוחד המבטיחים עמידות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1802, N'גלגל חלופי אחורי לרוכב המתחיל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1803, N'חישוק מסגסוגת אלומיניום עם חישורים מפלדת אל-חלד; בנויים למהירות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1804, N'גלגל אחורי חזק עם חישוק בעל הלחמה כפולה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1805, N'חישוקים אווירודינמיים מצוינים מבטיחים רכיבה חלקה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1806, N'עור מלאכותי. מאופיין בג''ל להגברת הנוחות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1807, N'מעוצב לספיגת זעזועים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1808, N'עיצוב אנטומי לרכיבה נוחה לאורך כל היום. עור עמיד.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1809, N'מושב קל-משקל מרופד בקצף.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1810, N'פגושי גומי הסופגים חבטות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1811, N'מושב Kevlar קל-משקל למרוצים. עור.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1812, N'מושב ג''ל נוח בעל עיצוב ארגונומי.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1813, N'עיצוב חדש להקלת לחץ ברכיבות ארוכות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1814, N'מעטפת קעורה לרכיבה נוחה יותר.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1815, N'אחיזת כביש נוחה בדומה לדגמים היוקרתיים, יציקת תילי צמיג יקרה פחות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1816, N'אחיזת כביש מעולה, גומי בצפיפות גבוהה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1817, N'אחיזת כביש שלא תאמן; חיזוק קל-משקל מפחמן.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1818, N'חריצי צמיג זהים לאלה המצויים בצמיגים יקרים יותר, אולם עם יציקת תילי צמיג יקרה פחות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1819, N'גומי בצפיפות גבוהה יותר מאשר דגמים אחרים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1820, N'קל-משקל מחוזק בפחמן לרכיבה שאין כמוה למעמסה ללא פשרות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1821, N'גומי בצפיפות גבוהה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1822, N'פנימית עם אטימה עצמית.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1823, N'פנימית קונבנציונלית לכל מטרה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1824, N'פנימית למטרות כלליות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1825, N'מסגרת ה- LL מספקת רכיבה בטוחה ונוחה ועם זאת, מציעה ספיגת חבטות מעולה של מסגרת אלומיניום במחיר סביר.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1826, N'מסגרת ה- ML, העשויה סגסוגת אלומיניום זהה לזו המשמשת ליצור מסגרת HL מהקו המוביל שלנו, מתאפיינת בצלע תחתונה קלת משקל החרוטה לקוטר המושלם הנדרש לחוזק אופטימלי. גירסה לגברים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1827, N'מסגרת האלומיניום הקלה והאיכותית ביותר שלנו עשויה מהסגסוגת החדישה ביותר; להענקת חוזק, היא מרותכת ומוקשחת בחום. העיצוב החדשני שלנו בא לידי ביטוי בנוחות ובביצועים מרביים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1828, N'מסגרת קלת משקל מאלומיניום חרוץ מספקת תנוחת רכיבה זקופה יותר לנסיעות בתוך העיר. העיצוב החדשני שלנו מספק נוחות מרבית.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1829, N'מסגרת ה- HL מאלומיניום מעוצבת בהתאמה הן למראה טוב והן לחוזק; היא תעמוד באתגרים המחמירים ביותר של רכיבה יומיומית. גירסה לגברים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1830, N'שילוב של סיבים טבעיים וסינטטיים נשאר יבש ומספק בדיוק את הריפוד הנכון.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1831, N'עיצוב המיועד לנוחות. מתאים לכיס. מיכל אלומיניום. 160 psi נקובים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1832, N'מעוצב עבור הקבוצה של AWC עם רצועות הידוק, בקרת לחות, ריפוד מעור רך והידוק לרגליים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1833, N'מבנה ניילון יציב ועמיד למים עם גישה קלה. גדול מספיק למסעות סופשבוע.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1834, N'ריפוד מלא, אפשרות כיפוף משופרת לאצבעות, עמידות באזור כף היד, סגירה ניתנת להתאמה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1835, N'אזור כף יד מחומר סינטטי, מפרקים גמישים, רשת נושמת בחלק העליון. נלבש על-ידי רוכבי קבוצת AWC.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1836, N'מכנסיים קצרים לפעילות מאומצת, מונעי שפשוף מאופיינים בחלק פנימי מספנדקס נטול תפרים ועור רך אנטי-בקטריאלי לתוספת נוחות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1837, N'כולל 8 טלאים בגדלים שונים, דבק ונייר זכוכית.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1838, N'קל-משקל, מגן מרוח, מתקפל לגודל המתאים לכיס.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1839, N'מכנסי גברים קצרים לתחרויות, 8 חלקים – עשויים ספנדקס עם חגורה אלסטית והידוק רגליים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1840, N'מעמד רב-תכליתי לאופניים המיועד לטיפולי אופניים הנערכים בבית. כליבות להתאמה מהירה ומבנה פלדה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1841, N'פנס כפול-אלומה הניתן לטעינה חוזרת.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1842, N'פנס קשיח ועמיד למים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1843, N'סריג קלאסי,נושם, שרוול קצר, עם בקרת לחות מעולה, רוכסן קדמי ושלושה כיסי גב.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1844, N'פשוט וקל-משקל. טלאי חירום מאוחסנים בידית.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1845, N'דק, קל-משקל ועמיד, עם חפתים שנותרים מורמים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1846, N'כלוב אלומיניום חזק שומר על הבקבוק בשטח קשה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1847, N'סגנון מסורתי עם מצחייה מתרוממת; גודל אחד מתאים לכולם.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1848, N'סריג לרכיבה, שרוול ארוך, יוניסקס, עשוי microfiber מתוצרת AWC.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1850, N'מידה אונברסלית, מאווררת היטב, קלת-משקל, עם מצחייה מתחברת.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1851, N'מיכל 2 ליטר רב-תכליתי לשתייה מציע מקום אחסון נוסף, גישה קלה למילוי וחגורת מותניים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1852, N'מכנסי ספנדקס צמודים וחמים לרכיבה חורפית; עור רך נטול תפרים מפחית מנקודות הלחיצה.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1853, N'לשטיפת לכלוך הנאסף בדרכים קשות; ממס שומנים, בטוח לסביבה. בקבוק של 1 ליטר.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1854, N'נכרך להתאמה לצמיג קדמי ואחורי, מנשא ושני מפתחות כלולים.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1855, N'铬钢。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1856, N'铝合金车圈；大直径脚蹬轴。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1857, N'铝合金车圈和空心轴。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1858, N'适合所有类型的使用，不论是公路骑乘还是越野。不论预算多少，均可称心如意。变速平稳，骑乘舒适。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1859, N'此自行车具有优越的性价比。它灵敏且易于操控，越野骑乘也可轻松胜任。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1860, N'适用于真正的越野车迷。此自行车极其耐用，无论身处何地，地形如何复杂，一切均在掌控之中，真正物超所值!')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1861, N'适用于环境恶劣的野外骑乘。可应对各种比赛的完美赛车。使用与 Mountain-100 相同的 HL 车架。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1862, N'高档竞赛山地车。性能得到进一步增强，包括创新的 HL 车架、极其平稳的前悬架以及适用于所有地形的出色牵引力。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1863, N'适用于所有类型的越野旅行。不论预算多少，均可称心如意。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1864, N'入门级成人自行车；确保越野旅行或公路骑乘的舒适。快拆式车毂和轮缘。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1865, N'此自行车经济实惠，具有我们的高档车型所拥有的许多功能。相同的车灯、刚架以及我们驰名业界的快速加速器。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1866, N'使用的技术与我们所有的公路系列自行车完全相同，但车架尺寸专为女士设计。完美的全能自行车，可作一般用途也可参加比赛。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1867, N'使用的技术与我们所有的公路系列自行车完全相同。完美的全能自行车，可作一般用途也可参加比赛。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1868, N'真正的多项运动自行车，骑乘自如，设计新颖。符合空气动力学的设计给您带来专业车手的体验，极佳的传动装置可以轻易征服陡峭的路面。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1869, N'此自行车设计简约且符合空气动力学，您可参加越野训练、比赛或与亲朋好友共享悠闲生活。高级座椅技术确保全天候的骑乘舒适。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1870, N'此自行车专为女士设计，造型清丽且符合空气动力学，您可参加越野训练、比赛或与亲朋好友共享悠闲生活。高级座椅技术确保全天候的骑乘舒适。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1871, N'铝合金车架确保骑乘轻快、稳固，可用于室内比赛或参加俱乐部的越野活动。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1872, N'此自行车曾为赛车手夺冠立下汗马功劳。与 Adventure Works Cycles 专业赛车队联合设计，它的铝制车架极其轻巧且经过热处理，操纵装置可以实现精准控制。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1873, N'全能经济型自行车，具备基本的舒适和安全特征。提供了更宽也更稳固的轮胎，适用于环城游或周末旅行。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1874, N'豪华的定制车座，确保您全天舒适骑乘，重新设计过的行李架上有足够的空间可添加驮篮和车筐。此自行车在完全负重情况下非常稳固。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1875, N'享受时尚舒适的旅行。专门设计，最大程度地确保舒适和安全。速度可调，轻松翻越所有类型的山坡。采用高科技铝合金构造，经久耐用，车身轻盈。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1876, N'卓越的变速性能。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1877, N'极其坚实的脚踏轴。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1878, N'高强度的曲臂。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1879, N'三重齿盘；铝制曲臂；完美变速。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1880, N'全天候刹车垫；通过增加与轮缘的接触面积来提供优异的制动功能。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1882, N'宽连杆设计。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1883, N'结实的设计不仅避震还可实现更精确的操控。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1884, N'具有铝制前叉竖管的组合式公路车前叉。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1885, N'具有弯管的高性能碳纤维公路车前叉。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1886, N'使用与 ML 铝制车架相同的开创性车架技术，经济实惠。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1887, N'ML 车架是经过热处理的铝制车架，它的细节设计和质量与 HL 车架完全相同。它的性能卓越。属男用自行车。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1888, N'ML 车架是经过热处理的铝制车架，它的细节设计和质量与 HL 车架完全相同。它的性能卓越。属女用自行车。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1889, N'每个车架都是在我们位于 Bothell 的工厂中手工制作，具有高质量山地车架所必需的最佳直径和壁厚。经过热处理后焊接在一起的铝制车架具有可避震的大口径叉管。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1890, N'ML 车架使用与我们的高档 HL 车架完全相同的铝合金，它的特征是轻型下管经过磨制达到最佳口径从而实现最大强度。女用自行车。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1891, N'适用于入门级骑乘者的备用山地车轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1892, N'适用于一般和高级骑乘者的备用山地车轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1893, N'高性能的山地车备用轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1894, N'适用于入门级骑乘者的公路型备用前轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1895, N'坚固的合金具有快拆式车毂。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1896, N'具有双层轮缘的坚固车轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1897, N'轮缘的设计符合空气动力学，确保平稳骑乘。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1898, N'公路越野两用的全功能车把。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1899, N'适用于高山速降的坚固耐用的铝合金车把。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1900, N'平头车把异常坚固，足可用于专业巡回赛。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1901, N'独特的外形设计使得刹车把更易使用。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1903, N'符合人体解剖学的铝制车把适用于所有骑乘者。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1904, N'专为竞赛者设计；高档且符合人体解剖学的铝合金车把。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1905, N'独特的外形设计有助于减轻入门级骑乘者的疲劳。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1906, N'轻且坚固的铝制车把适用于长途骑乘。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1907, N'无螺纹的车头碗组不仅确保质量而且经济实惠。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1908, N'密封的缓冲油管，纤尘不染。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1909, N'高质量的一英寸无螺纹车头碗组具有油口，可确保快速润滑。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1910, N'展开式踏板便于您穿任何鞋子骑乘；适用于各种情况。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1911, N'无扣带式脚踏轻型耐用且松紧可调。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1912, N'不锈钢材质；易于去泥。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1913, N'无扣带式脚踏 – 铝制。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1914, N'轻型铝合金结构。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1915, N'松紧可调的高档无扣带式脚踏。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1916, N'踏板稳固，可供全天候骑乘。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1918, N'可填充的骑行鞋；抛光型铝制钳形闸。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1919, N'具有密封式滑轮轴承的 10 速铝制变速器。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1920, N'适用于入门级骑乘者的山地车备用后轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1921, N'适用于一般和高级骑乘者的山地车备用后轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1922, N'特别加固的轮缘确保耐用。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1923, N'适用于入门级骑乘者的备用后轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1924, N'带不锈钢辐条的铝合金轮缘；特别为提速而设计。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1925, N'具有双层轮缘的坚固后轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1926, N'出色的符合空气动力学的轮缘设计确保平稳骑乘。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1927, N'人造皮革。含有凝胶，提升舒适度。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1928, N'专门设计，确保避震。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1929, N'符合人体解剖学的设计确保全天候舒适骑乘。皮革材质持久耐用。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1930, N'轻型泡沫填充的车座。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1931, N'橡皮减震器可避免颠簸。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1932, N'含轻型凯夫拉纤维的比赛用车座。皮革材质。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1933, N'舒适且符合人体工程学的含凝胶车座。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1934, N'新型设计可缓解长时间骑乘的压力。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1935, N'精致的车架，令您骑乘更舒适。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1936, N'钢丝撑轮圈外胎的价格便宜，牵引力却可与高档车型相媲美。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1937, N'出色的牵引力、高密度的橡皮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1938, N'惊人的牵引力；使用轻型碳纤维加强。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1939, N'钢丝撑轮圈外胎的价格便宜，轮胎面却与价格更贵的轮胎同样出色。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1940, N'橡皮的密度较其他车型更高。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1941, N'使用轻型碳纤维加固，车身轻盈如常，骑乘感觉非同一般。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1942, N'高密度橡皮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1943, N'自封内胎。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1944, N'常规的全功能内胎。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1945, N'一般用内胎。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1946, N'LL 车架提供了安全舒适的骑乘，经济实惠的铝制车架提供了卓越的避震功能。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1947, N'ML 车架使用与我们的高档 HL 车架完全相同的铝合金，它的特征是轻型下管经过磨制达到最佳口径从而实现最大强度。男用自行车。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1948, N'重量最轻、质量最好的铝制车架采用最新的合金材质；经过焊接和热处理，坚固牢靠。我们创新的设计确保了最大程度的舒适和卓越的性能。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1949, N'轻型一体式铝制车架，可以更直立的骑乘姿势进行环城游。我们开创性的设计提供了最佳舒适度。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1950, N'HL 铝制车架经过特别打造，不仅外形悦目而且坚固耐用；不论何时何地，日常骑乘，轻松掌控。男用自行车。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1951, N'价格适中的车灯，确保夜间骑乘安全 – 使用三节 AAA 电池')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1952, N'铝制外胎比山地车胎更轻盈；是长途旅行的完美伴侣。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1953, N'带有 AWC 徽标的水瓶容量为 30 盎司；防漏设计。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1954, N'可安全运载四辆自行车；钢结构；配有二英寸的固定装置。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1955, N'卡扣式挡泥板适合大多数山地车。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1956, N'混合使用天然纤维和合成纤维，确保干燥并提供恰到好处的减震功能。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1957, N'专门设计，确保方便。可装在您的口袋里。铝制气筒。额定气压为 160psi。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1958, N'专为 AWC 赛车队设计，配有固定扣带、湿度控制器、麂皮垫和腿夹。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1959, N'易于拿取的耐用防水型尼龙结构。容量尽可满足周末旅行所需。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1960, N'完全填充、手指活动更自如、手掌材质更耐用、大小可调。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1961, N'合成材质手掌、灵活的指关节、上部带有透气型网眼。AWC车队赛手专用。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1963, N'高弹力、耐磨型运动短裤，无缝氨纶内衬带有抗菌麂皮，确保穿着舒适。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1964, N'包括八种不同尺寸的补片、胶水和砂纸。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1965, N'轻型抗风，可折叠放入口袋。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1966, N'男士八拼片竞赛用运动短裤 – 氨纶材质、弹性腰带并带腿夹。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1967, N'完美设计的全能型自行车支架，适用于在家中立地骑乘。钢结构，具有快速可调的固定夹。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1968, N'充电式双光束车灯。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1969, N'耐用型防风雨车灯。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1970, N'短袖、式样经典的透气型运动衫，极佳的湿度控制、前拉练并带有三个后袋。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1971, N'简单轻便。应急补片存放在手把中。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1972, N'贴身设计，轻便耐穿，带有紧口袖边。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1973, N'结实的铝壳可在任何环境下保持瓶子稳固。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1974, N'经典式样，带有翻边；均码。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1975, N'中性长袖带有 AWC 徽标的微纤维赛车用运动衫')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1976, N'通用型透气良好且轻便，带有自合型帽沿。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1977, N'多用途 70 盎司水袋的空间更大，易于装填并配有腰带。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1978, N'保暖型氨纶紧身运动衣适用于冬季骑乘；无缝麂皮结构可化解压力。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1979, N'可洗去最顽固的污垢；可溶解油脂，利于环保。一升瓶装。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1980, N'所携装备可置于前面和后面，包括行李架和两把钥匙。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1981, N'Replacement mountain wheel for entry-level rider.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1982, N'عجلة احتياطية مخصصة للقيادة في الجبال لراكبي الدراجات المبتدئين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1983, N'Roue de secours tout-terrain pour vététiste occasionnel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1984, N'ล้ออะไหล่จักรยานภูเขาสำหรับนักปั่นระดับเริ่มต้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1985, N'גלגל חלופי לרכיבה הררית לרוכב המתחיל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1986, N'适用于入门级骑乘者的备用山地车轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1987, N'Replacement mountain wheel for the casual to serious rider.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1988, N'عجلة احتياطية مخصصة للقيادة في الجبال تناسب كافة أنواع الركاب بدءًا من الراكب العادي إلى الراكب المحترف.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1989, N'Roue de secours tout-terrain pour vététiste amateur à confirmé.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1990, N'ล้ออะไหล่จักรยานภูเขาสำหรับนักปั่นสมัครเล่นไปจนถึงมืออาชีพ')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1991, N'גלגל חלופי לרכיבה הררית לרוכב החובב והרציני.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1992, N'适用于一般和高级骑乘者的备用山地车轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1993, N'High-performance mountain replacement wheel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1994, N'عجلة احتياطية مخصصة للقيادة في الجبال عالية الأداء.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1995, N'Roue de secours tout-terrain hautes performances.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1996, N'ล้ออะไหล่จักรยานภูเขาประสิทธิภาพสูง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1997, N'גלגל חלופי עתיר ביצועים לרכיבה הררית.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1998, N'高性能的山地车备用轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (1999, N'Replacement road rear wheel for entry-level cyclist.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2000, N'عجلة طريق أمامية بديلة لقائدي الدراجات المبتدئين.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2001, N'Roue avant pour vélo de route pour cycliste occasionnel.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2002, N'ล้อหน้าจักรยานภูเขาอะไหล่สำหรับนักปั่นระดับเริ่มต้น')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2003, N'גלגל חלופי קדמי לרכיבת כביש לרוכב המתחיל.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2004, N'适用于入门级骑乘者的公路型备用前轮。')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2005, N'Wide-link design.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2006, N'تصميم عريض الوصلات.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2007, N'Conception liaison large.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2008, N'การออกแบบให้มีจุดเชื่อมกว้าง')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2009, N'עיצוב רחב-חוליות.')
    
    INSERT [dbo].[ProductDescription] ([ProductDescriptionID], [Description]) VALUES (2010, N'宽连杆设计。')

    SET IDENTITY_INSERT [ProductDescription] OFF
END