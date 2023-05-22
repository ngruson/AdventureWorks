IF NOT EXISTS (SELECT TOP 1 1 FROM Employee)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table Employee...'

	INSERT [Employee] ([PersonID],[NationalIDNumber],[LoginID],[OrganizationNode],[OrganizationLevel],[JobTitle],[BirthDate],[MaritalStatus],[Gender],[HireDate],[SalariedFlag],[CurrentFlag])
    SELECT 1,N'295847284',N'ken0',NULL,NULL,N'Chief Executive Officer','1969-01-29',N'S',N'M','2009-01-14',1,1 UNION ALL
    SELECT 2,N'245797967',N'terri0',N'/1/',1,N'Vice President of Engineering','1971-08-01',N'S',N'F','2008-01-31',1,1 UNION ALL
    SELECT 3,N'509647174',N'roberto0',N'/1/1/',2,N'Engineering Manager','1974-11-12',N'M',N'M','2007-11-11',1,1 UNION ALL
    SELECT 4,N'112457891',N'rob0',N'/1/1/1/',3,N'Senior Tool Designer','1974-12-23',N'S',N'M','2007-12-05',0,1 UNION ALL
    SELECT 5,N'695256908',N'gail0',N'/1/1/2/',3,N'Design Engineer','1952-09-27',N'M',N'F','2008-01-06',1,1 UNION ALL
    SELECT 6,N'998320692',N'jossef0',N'/1/1/3/',3,N'Design Engineer','1959-03-11',N'M',N'M','2008-01-24',1,1 UNION ALL
    SELECT 7,N'134969118',N'dylan0',N'/1/1/4/',3,N'Research and Development Manager','1987-02-24',N'M',N'M','2009-02-08',1,1 UNION ALL
    SELECT 8,N'811994146',N'diane1',N'/1/1/4/1/',4,N'Research and Development Engineer','1986-06-05',N'S',N'F','2008-12-29',1,1 UNION ALL
    SELECT 9,N'658797903',N'gigi0',N'/1/1/4/2/',4,N'Research and Development Engineer','1979-01-21',N'M',N'F','2009-01-16',1,1 UNION ALL
    SELECT 10,N'879342154',N'michael6',N'/1/1/4/3/',4,N'Research and Development Manager','1984-11-30',N'M',N'M','2009-05-03',1,1 UNION ALL
    SELECT 11,N'974026903',N'ovidiu0',N'/1/1/5/',3,N'Senior Tool Designer','1978-01-17',N'S',N'M','2010-12-05',0,1 UNION ALL
    SELECT 12,N'480168528',N'thierry0',N'/1/1/5/1/',4,N'Tool Designer','1959-07-29',N'M',N'M','2007-12-11',0,1 UNION ALL
    SELECT 13,N'486228782',N'janice0',N'/1/1/5/2/',4,N'Tool Designer','1989-05-28',N'M',N'F','2010-12-23',0,1 UNION ALL
    SELECT 14,N'42487730',N'michael8',N'/1/1/6/',3,N'Senior Design Engineer','1979-06-16',N'S',N'M','2010-12-30',1,1 UNION ALL
    SELECT 15,N'56920285',N'sharon0',N'/1/1/7/',3,N'Design Engineer','1961-05-02',N'M',N'F','2011-01-18',1,1 UNION ALL
    SELECT 16,N'24756624',N'david0',N'/2/',1,N'Marketing Manager','1975-03-19',N'S',N'M','2007-12-20',1,1 UNION ALL
    SELECT 17,N'253022876',N'kevin0',N'/2/1/',2,N'Marketing Assistant','1987-05-03',N'S',N'M','2007-01-26',0,1 UNION ALL
    SELECT 18,N'222969461',N'john5',N'/2/2/',2,N'Marketing Specialist','1978-03-06',N'S',N'M','2011-02-07',0,1 UNION ALL
    SELECT 19,N'52541318',N'mary2',N'/2/3/',2,N'Marketing Assistant','1978-01-29',N'S',N'F','2011-02-14',0,1 UNION ALL
    SELECT 20,N'323403273',N'wanida0',N'/2/4/',2,N'Marketing Assistant','1975-03-17',N'M',N'F','2011-01-07',0,1 UNION ALL
    SELECT 21,N'243322160',N'terry0',N'/2/5/',2,N'Marketing Specialist','1986-02-04',N'M',N'M','2009-03-02',0,1 UNION ALL
    SELECT 22,N'95958330',N'sariya0',N'/2/6/',2,N'Marketing Specialist','1987-05-21',N'S',N'M','2008-12-12',0,1 UNION ALL
    SELECT 23,N'767955365',N'mary0',N'/2/7/',2,N'Marketing Specialist','1962-09-13',N'M',N'F','2009-01-12',0,1 UNION ALL
    SELECT 24,N'72636981',N'jill0',N'/2/8/',2,N'Marketing Specialist','1979-06-18',N'M',N'F','2009-01-18',0,1 UNION ALL
    SELECT 25,N'519899904',N'james1',N'/3/',1,N'Vice President of Production','1983-01-07',N'S',N'M','2009-02-03',1,1 UNION ALL
    SELECT 26,N'277173473',N'peter0',N'/3/1/',2,N'Production Control Manager','1982-11-03',N'M',N'M','2008-12-01',1,1 UNION ALL
    SELECT 27,N'446466105',N'jo0',N'/3/1/1/',3,N'Production Supervisor - WC60','1956-10-08',N'S',N'F','2008-02-27',0,1 UNION ALL
    SELECT 28,N'14417807',N'guy1',N'/3/1/1/1/',4,N'Production Technician - WC60','1988-03-13',N'M',N'M','2006-06-30',0,1 UNION ALL
    SELECT 29,N'948320468',N'mark1',N'/3/1/1/2/',4,N'Production Technician - WC60','1979-09-25',N'S',N'M','2009-01-23',0,1 UNION ALL
    SELECT 30,N'410742000',N'britta0',N'/3/1/1/3/',4,N'Production Technician - WC60','1989-09-28',N'M',N'F','2009-01-29',0,1 UNION ALL
    SELECT 31,N'750246141',N'margie0',N'/3/1/1/4/',4,N'Production Technician - WC60','1986-05-20',N'M',N'F','2009-01-04',0,1 UNION ALL
    SELECT 32,N'330211482',N'rebecca0',N'/3/1/1/5/',4,N'Production Technician - WC60','1977-07-10',N'M',N'F','2008-12-29',0,1 UNION ALL
    SELECT 33,N'801758002',N'annik0',N'/3/1/1/6/',4,N'Production Technician - WC60','1976-12-26',N'M',N'M','2008-12-17',0,1 UNION ALL
    SELECT 34,N'754372876',N'suchitra0',N'/3/1/1/7/',4,N'Production Technician - WC60','1987-06-10',N'M',N'F','2009-02-16',0,1 UNION ALL
    SELECT 35,N'999440576',N'brandon0',N'/3/1/1/8/',4,N'Production Technician - WC60','1977-01-10',N'M',N'M','2009-02-08',0,1 UNION ALL
    SELECT 36,N'788456780',N'jose0',N'/3/1/1/9/',4,N'Production Technician - WC60','1984-07-31',N'M',N'M','2009-02-10',0,1 UNION ALL
    SELECT 37,N'442121106',N'chris2',N'/3/1/1/10/',4,N'Production Technician - WC60','1986-08-07',N'S',N'M','2009-03-07',0,1 UNION ALL
    SELECT 38,N'6298838',N'kim1',N'/3/1/1/11/',4,N'Production Technician - WC60','1966-12-14',N'M',N'F','2010-01-16',0,1 UNION ALL
    SELECT 39,N'461786517',N'ed0',N'/3/1/1/12/',4,N'Production Technician - WC60','1971-09-11',N'S',N'M','2010-02-05',0,1 UNION ALL
    SELECT 40,N'309738752',N'jolynn0',N'/3/1/2/',3,N'Production Supervisor - WC60','1956-01-16',N'S',N'F','2007-12-26',0,1 UNION ALL
    SELECT 41,N'458159238',N'bryan0',N'/3/1/2/1/',4,N'Production Technician - WC60','1973-08-27',N'S',N'M','2009-01-21',0,1 UNION ALL
    SELECT 42,N'339712426',N'james0',N'/3/1/2/2/',4,N'Production Technician - WC60','1984-07-25',N'M',N'M','2008-12-27',0,1 UNION ALL
    SELECT 43,N'693325305',N'nancy0',N'/3/1/2/3/',4,N'Production Technician - WC60','1988-11-19',N'M',N'F','2009-01-02',0,1 UNION ALL
    SELECT 44,N'276751903',N'simon0',N'/3/1/2/4/',4,N'Production Technician - WC60','1990-05-17',N'S',N'M','2008-12-08',0,1 UNION ALL
    SELECT 45,N'500412746',N'thomas0',N'/3/1/2/5/',4,N'Production Technician - WC60','1986-01-10',N'M',N'M','2009-02-26',0,1 UNION ALL
    SELECT 46,N'66073987',N'eugene1',N'/3/1/2/6/',4,N'Production Technician - WC60','1976-02-10',N'S',N'M','2009-02-08',0,1 UNION ALL
    SELECT 47,N'33237992',N'andrew0',N'/3/1/3/',3,N'Production Supervisor - WC10','1988-09-06',N'S',N'M','2009-02-22',0,1 UNION ALL
    SELECT 48,N'690627818',N'ruth0',N'/3/1/3/1/',4,N'Production Technician - WC10','1956-06-04',N'M',N'F','2008-01-06',0,1 UNION ALL
    SELECT 49,N'912265825',N'barry0',N'/3/1/3/2/',4,N'Production Technician - WC10','1956-03-26',N'S',N'M','2008-01-07',0,1 UNION ALL
    SELECT 50,N'844973625',N'sidney0',N'/3/1/3/3/',4,N'Production Technician - WC10','1956-08-30',N'M',N'M','2008-02-02',0,1 UNION ALL
    SELECT 51,N'132674823',N'jeffrey0',N'/3/1/3/4/',4,N'Production Technician - WC10','1956-07-11',N'S',N'M','2008-02-20',0,1 UNION ALL
    SELECT 52,N'565090917',N'doris0',N'/3/1/3/5/',4,N'Production Technician - WC10','1956-04-04',N'M',N'F','2008-03-10',0,1 UNION ALL
    SELECT 53,N'9659517',N'diane0',N'/3/1/3/6/',4,N'Production Technician - WC10','1956-03-29',N'M',N'F','2008-03-28',0,1 UNION ALL
    SELECT 54,N'109272464',N'bonnie0',N'/3/1/3/7/',4,N'Production Technician - WC10','1986-09-10',N'M',N'F','2010-01-01',0,1 UNION ALL
    SELECT 55,N'233069302',N'taylor0',N'/3/1/4/',3,N'Production Supervisor - WC50','1956-04-01',N'M',N'M','2008-02-08',0,1 UNION ALL
    SELECT 56,N'652535724',N'denise0',N'/3/1/4/1/',4,N'Production Technician - WC50','1988-07-06',N'M',N'F','2009-02-05',0,1 UNION ALL
    SELECT 57,N'10708100',N'frank1',N'/3/1/4/2/',4,N'Production Technician - WC50','1971-07-24',N'S',N'M','2009-02-23',0,1 UNION ALL
    SELECT 58,N'571658797',N'kendall0',N'/3/1/4/3/',4,N'Production Technician - WC50','1986-05-30',N'M',N'M','2008-12-05',0,1 UNION ALL
    SELECT 59,N'843479922',N'bob0',N'/3/1/4/4/',4,N'Production Technician - WC50','1979-08-16',N'S',N'M','2008-12-24',0,1 UNION ALL
    SELECT 60,N'827686041',N'pete0',N'/3/1/4/5/',4,N'Production Technician - WC50','1977-02-03',N'S',N'M','2009-01-11',0,1 UNION ALL
    SELECT 61,N'92096924',N'diane2',N'/3/1/4/6/',4,N'Production Technician - WC50','1989-08-09',N'S',N'F','2009-01-18',0,1 UNION ALL
    SELECT 62,N'494170342',N'john0',N'/3/1/5/',3,N'Production Supervisor - WC60','1956-08-07',N'M',N'M','2008-03-17',0,1 UNION ALL
    SELECT 63,N'414476027',N'maciej0',N'/3/1/5/1/',4,N'Production Technician - WC60','1955-01-30',N'S',N'M','2010-01-29',0,1 UNION ALL
    SELECT 64,N'582347317',N'michael7',N'/3/1/5/2/',4,N'Production Technician - WC60','1973-09-05',N'S',N'M','2010-02-23',0,1 UNION ALL
    SELECT 65,N'8066363',N'randy0',N'/3/1/5/3/',4,N'Production Technician - WC60','1970-04-28',N'M',N'M','2010-02-23',0,1 UNION ALL
    SELECT 66,N'834186596',N'karan0',N'/3/1/5/4/',4,N'Production Technician - WC60','1970-03-07',N'S',N'M','2009-12-22',0,1 UNION ALL
    SELECT 67,N'63179277',N'jay0',N'/3/1/5/5/',4,N'Production Technician - WC60','1976-02-11',N'S',N'M','2009-03-05',0,1 UNION ALL
    SELECT 68,N'537092325',N'charles0',N'/3/1/5/6/',4,N'Production Technician - WC60','1971-09-02',N'S',N'M','2009-12-03',0,1 UNION ALL
    SELECT 69,N'752513276',N'steve0',N'/3/1/5/7/',4,N'Production Technician - WC60','1991-04-06',N'S',N'M','2009-02-15',0,1 UNION ALL
    SELECT 70,N'36151748',N'david2',N'/3/1/5/8/',4,N'Production Technician - WC60','1984-12-29',N'M',N'M','2008-12-15',0,1 UNION ALL
    SELECT 71,N'578935259',N'michael3',N'/3/1/6/',3,N'Production Supervisor - WC30','1989-01-29',N'S',N'M','2009-02-15',0,1 UNION ALL
    SELECT 72,N'443968955',N'steven0',N'/3/1/6/1/',4,N'Production Technician - WC30','1977-05-14',N'M',N'M','2008-12-01',0,1 UNION ALL
    SELECT 73,N'138280935',N'carole0',N'/3/1/6/2/',4,N'Production Technician - WC30','1983-10-19',N'M',N'F','2008-12-19',0,1 UNION ALL
    SELECT 74,N'420023788',N'bjorn0',N'/3/1/6/3/',4,N'Production Technician - WC30','1989-11-06',N'S',N'M','2009-01-07',0,1 UNION ALL
    SELECT 75,N'363996959',N'michiko0',N'/3/1/6/4/',4,N'Production Technician - WC30','1982-06-27',N'S',N'M','2009-01-26',0,1 UNION ALL
    SELECT 76,N'227319668',N'carol0',N'/3/1/6/5/',4,N'Production Technician - WC30','1988-10-17',N'M',N'F','2009-02-12',0,1 UNION ALL
    SELECT 77,N'301435199',N'merav0',N'/3/1/6/6/',4,N'Production Technician - WC30','1983-05-13',N'M',N'F','2009-03-03',0,1 UNION ALL
    SELECT 78,N'370989364',N'reuben0',N'/3/1/7/',3,N'Production Supervisor - WC40','1987-08-27',N'M',N'M','2008-12-15',0,1 UNION ALL
    SELECT 79,N'697712387',N'eric1',N'/3/1/7/1/',4,N'Production Technician - WC40','1966-12-08',N'M',N'M','2010-01-24',0,1 UNION ALL
    SELECT 80,N'943170460',N'sandeep0',N'/3/1/7/2/',4,N'Production Technician - WC40','1970-12-03',N'S',N'M','2010-01-17',0,1 UNION ALL
    SELECT 81,N'413787783',N'mihail0',N'/3/1/7/3/',4,N'Production Technician - WC40','1971-03-09',N'S',N'M','2009-12-29',0,1 UNION ALL
    SELECT 82,N'58791499',N'jack1',N'/3/1/7/4/',4,N'Production Technician - WC40','1973-08-29',N'S',N'M','2010-03-03',0,1 UNION ALL
    SELECT 83,N'988315686',N'patrick1',N'/3/1/7/5/',4,N'Production Technician - WC40','1973-12-23',N'M',N'M','2010-02-12',0,1 UNION ALL
    SELECT 84,N'947029962',N'frank3',N'/3/1/7/6/',4,N'Production Technician - WC40','1952-03-02',N'M',N'M','2010-02-05',0,1 UNION ALL
    SELECT 85,N'1662732',N'brian2',N'/3/1/7/7/',4,N'Production Technician - WC40','1970-12-23',N'S',N'M','2009-12-11',0,1 UNION ALL
    SELECT 86,N'769680433',N'ryan0',N'/3/1/7/8/',4,N'Production Technician - WC40','1972-06-13',N'M',N'M','2009-01-05',0,1 UNION ALL
    SELECT 87,N'7201901',N'cristian0',N'/3/1/8/',3,N'Production Supervisor - WC10','1984-04-11',N'M',N'M','2008-12-22',0,1 UNION ALL
    SELECT 88,N'294148271',N'betsy0',N'/3/1/8/1/',4,N'Production Technician - WC10','1966-12-17',N'S',N'F','2009-12-18',0,1 UNION ALL
    SELECT 89,N'90888098',N'patrick0',N'/3/1/8/2/',4,N'Production Technician - WC10','1986-09-10',N'S',N'M','2010-02-01',0,1 UNION ALL
    SELECT 90,N'82638150',N'danielle0',N'/3/1/8/3/',4,N'Production Technician - WC10','1986-09-07',N'S',N'F','2010-02-20',0,1 UNION ALL
    SELECT 91,N'390124815',N'kimberly0',N'/3/1/8/4/',4,N'Production Technician - WC10','1986-09-13',N'S',N'F','2010-01-12',0,1 UNION ALL
    SELECT 92,N'826454897',N'tom0',N'/3/1/8/5/',4,N'Production Technician - WC10','1986-10-01',N'M',N'M','2010-03-10',0,1 UNION ALL
    SELECT 93,N'778552911',N'kok-ho0',N'/3/1/9/',3,N'Production Supervisor - WC50','1980-04-28',N'S',N'M','2008-12-27',0,1 UNION ALL
    SELECT 94,N'718299860',N'russell0',N'/3/1/9/1/',4,N'Production Technician - WC50','1972-11-25',N'M',N'M','2008-12-12',0,1 UNION ALL
    SELECT 95,N'674171828',N'jim0',N'/3/1/9/2/',4,N'Production Technician - WC50','1986-09-08',N'M',N'M','2008-12-19',0,1 UNION ALL
    SELECT 96,N'912141525',N'elizabeth0',N'/3/1/9/3/',4,N'Production Technician - WC50','1990-01-25',N'M',N'F','2009-03-02',0,1 UNION ALL
    SELECT 97,N'370581729',N'mandar0',N'/3/1/9/4/',4,N'Production Technician - WC50','1986-03-21',N'S',N'M','2009-02-10',0,1 UNION ALL
    SELECT 98,N'152085091',N'sameer0',N'/3/1/9/5/',4,N'Production Technician - WC50','1978-06-26',N'M',N'M','2009-02-11',0,1 UNION ALL
    SELECT 99,N'431859843',N'nuan0',N'/3/1/9/6/',4,N'Production Technician - WC50','1979-03-29',N'S',N'M','2009-01-06',0,1 UNION ALL
    SELECT 100,N'204035155',N'lolan0',N'/3/1/9/7/',4,N'Production Technician - WC50','1973-01-24',N'M',N'M','2009-01-12',0,1 UNION ALL
    SELECT 101,N'153288994',N'houman0',N'/3/1/9/8/',4,N'Production Technician - WC50','1971-08-30',N'M',N'M','2009-01-25',0,1 UNION ALL
    SELECT 102,N'360868122',N'zheng0',N'/3/1/10/',3,N'Production Supervisor - WC10','1983-10-26',N'S',N'M','2008-12-03',0,1 UNION ALL
    SELECT 103,N'455563743',N'ebru0',N'/3/1/10/1/',4,N'Production Technician - WC10','1986-09-22',N'S',N'M','2009-12-06',0,1 UNION ALL
    SELECT 104,N'717889520',N'mary1',N'/3/1/10/2/',4,N'Production Technician - WC10','1986-09-19',N'M',N'F','2009-12-25',0,1 UNION ALL
    SELECT 105,N'801365500',N'kevin2',N'/3/1/10/3/',4,N'Production Technician - WC10','1986-09-19',N'S',N'M','2009-12-25',0,1 UNION ALL
    SELECT 106,N'561196580',N'john4',N'/3/1/10/4/',4,N'Production Technician - WC10','1986-09-28',N'S',N'M','2010-02-27',0,1 UNION ALL
    SELECT 107,N'393421437',N'christopher0',N'/3/1/10/5/',4,N'Production Technician - WC10','1986-10-01',N'M',N'M','2010-02-08',0,1 UNION ALL
    SELECT 108,N'630184120',N'jinghao0',N'/3/1/11/',3,N'Production Supervisor - WC50','1989-02-05',N'S',N'M','2008-12-08',0,1 UNION ALL
    SELECT 109,N'113695504',N'alice0',N'/3/1/11/1/',4,N'Production Technician - WC50','1978-01-26',N'M',N'F','2008-12-07',0,1 UNION ALL
    SELECT 110,N'857651804',N'jun0',N'/3/1/11/2/',4,N'Production Technician - WC50','1979-07-06',N'S',N'M','2008-12-14',0,1 UNION ALL
    SELECT 111,N'415823523',N'suroor0',N'/3/1/11/3/',4,N'Production Technician - WC50','1978-02-25',N'S',N'M','2008-12-17',0,1 UNION ALL
    SELECT 112,N'981597097',N'john1',N'/3/1/11/4/',4,N'Production Technician - WC50','1978-05-31',N'S',N'M','2009-01-01',0,1 UNION ALL
    SELECT 113,N'54759846',N'linda0',N'/3/1/11/5/',4,N'Production Technician - WC50','1987-07-17',N'M',N'F','2008-12-25',0,1 UNION ALL
    SELECT 114,N'342607223',N'mindaugas0',N'/3/1/11/6/',4,N'Production Technician - WC50','1978-05-07',N'M',N'M','2009-01-13',0,1 UNION ALL
    SELECT 115,N'563680513',N'angela0',N'/3/1/11/7/',4,N'Production Technician - WC50','1991-05-31',N'S',N'F','2009-01-20',0,1 UNION ALL
    SELECT 116,N'398737566',N'michael2',N'/3/1/11/8/',4,N'Production Technician - WC50','1974-05-03',N'S',N'M','2009-01-31',0,1 UNION ALL
    SELECT 117,N'599942664',N'chad0',N'/3/1/11/9/',4,N'Production Technician - WC50','1990-08-04',N'M',N'M','2009-02-18',0,1 UNION ALL
    SELECT 118,N'222400012',N'don0',N'/3/1/11/10/',4,N'Production Technician - WC50','1971-06-13',N'M',N'M','2009-02-13',0,1 UNION ALL
    SELECT 119,N'334834274',N'michael4',N'/3/1/11/11/',4,N'Production Technician - WC50','1989-06-15',N'S',N'M','2009-02-25',0,1 UNION ALL
    SELECT 120,N'211789056',N'kitti0',N'/3/1/11/12/',4,N'Production Technician - WC50','1987-06-06',N'S',N'F','2009-03-04',0,1 UNION ALL
    SELECT 121,N'521265716',N'pilar0',N'/3/1/12/',3,N'Shipping and Receiving Supervisor','1972-09-09',N'S',N'M','2009-01-02',1,1 UNION ALL
    SELECT 122,N'586486572',N'susan0',N'/3/1/12/1/',4,N'Stocker','1978-02-17',N'S',N'F','2008-12-07',0,1 UNION ALL
    SELECT 123,N'337752649',N'vamsi0',N'/3/1/12/2/',4,N'Shipping and Receiving Clerk','1977-03-18',N'M',N'M','2008-12-07',0,1 UNION ALL
    SELECT 124,N'420776180',N'kim0',N'/3/1/12/3/',4,N'Stocker','1984-04-30',N'S',N'F','2008-12-26',0,1 UNION ALL
    SELECT 125,N'584205124',N'matthias0',N'/3/1/12/4/',4,N'Shipping and Receiving Clerk','1973-11-11',N'M',N'M','2009-01-20',0,1 UNION ALL
    SELECT 126,N'652779496',N'jimmy0',N'/3/1/12/5/',4,N'Stocker','1985-05-04',N'M',N'M','2009-02-26',0,1 UNION ALL
    SELECT 127,N'750905084',N'david4',N'/3/1/13/',3,N'Production Supervisor - WC40','1983-07-02',N'S',N'M','2009-01-03',0,1 UNION ALL
    SELECT 128,N'384162788',N'paul0',N'/3/1/13/1/',4,N'Production Technician - WC40','1980-11-13',N'S',N'M','2008-12-04',0,1 UNION ALL
    SELECT 129,N'502058701',N'gary0',N'/3/1/13/2/',4,N'Production Technician - WC40','1988-05-16',N'S',N'M','2008-12-22',0,1 UNION ALL
    SELECT 130,N'578953538',N'rob1',N'/3/1/13/3/',4,N'Production Technician - WC40','1973-08-04',N'S',N'M','2009-02-13',0,1 UNION ALL
    SELECT 131,N'273260055',N'baris0',N'/3/1/13/4/',4,N'Production Technician - WC40','1990-10-07',N'S',N'M','2009-02-15',0,1 UNION ALL
    SELECT 132,N'1300049',N'nicole0',N'/3/1/13/5/',4,N'Production Technician - WC40','1986-04-09',N'M',N'F','2009-02-22',0,1 UNION ALL
    SELECT 133,N'830150469',N'michael1',N'/3/1/13/6/',4,N'Production Technician - WC40','1991-01-04',N'S',N'M','2009-01-10',0,1 UNION ALL
    SELECT 134,N'45615666',N'eric0',N'/3/1/14/',3,N'Production Supervisor - WC20','1985-01-19',N'M',N'M','2009-01-14',0,1 UNION ALL
    SELECT 135,N'964089218',N'ivo0',N'/3/1/14/1/',4,N'Production Technician - WC20','1982-01-03',N'M',N'M','2008-12-04',0,1 UNION ALL
    SELECT 136,N'701156975',N'sylvester0',N'/3/1/14/2/',4,N'Production Technician - WC20','1970-11-12',N'M',N'M','2009-12-11',0,1 UNION ALL
    SELECT 137,N'63761469',N'anibal0',N'/3/1/14/3/',4,N'Production Technician - WC20','1974-09-05',N'S',N'F','2009-02-23',0,1 UNION ALL
    SELECT 138,N'25011600',N'samantha0',N'/3/1/14/4/',4,N'Production Technician - WC20','1987-11-22',N'M',N'F','2009-02-04',0,1 UNION ALL
    SELECT 139,N'113393530',N'hung-fu0',N'/3/1/14/5/',4,N'Production Technician - WC20','1971-10-23',N'S',N'M','2010-01-06',0,1 UNION ALL
    SELECT 140,N'339233463',N'prasanna0',N'/3/1/14/6/',4,N'Production Technician - WC20','1953-04-30',N'M',N'M','2010-01-22',0,1 UNION ALL
    SELECT 141,N'872923042',N'min0',N'/3/1/14/7/',4,N'Production Technician - WC20','1974-09-10',N'M',N'M','2010-01-24',0,1 UNION ALL
    SELECT 142,N'163347032',N'olinda0',N'/3/1/14/8/',4,N'Production Technician - WC20','1970-04-04',N'S',N'F','2010-03-04',0,1 UNION ALL
    SELECT 143,N'56772045',N'krishna0',N'/3/1/14/9/',4,N'Production Technician - WC20','1971-09-05',N'S',N'M','2010-02-13',0,1 UNION ALL
    SELECT 144,N'886023130',N'paul1',N'/3/1/14/10/',4,N'Production Technician - WC20','1990-11-04',N'M',N'M','2009-01-17',0,1 UNION ALL
    SELECT 145,N'386315192',N'cynthia0',N'/3/1/15/',3,N'Production Supervisor - WC30','1981-08-18',N'S',N'F','2009-01-27',0,1 UNION ALL
    SELECT 146,N'160739235',N'jianshuo0',N'/3/1/15/1/',4,N'Production Technician - WC30','1989-06-25',N'S',N'M','2008-12-07',0,1 UNION ALL
    SELECT 147,N'604664374',N'sandra0',N'/3/1/15/2/',4,N'Production Technician - WC30','1975-11-05',N'M',N'F','2008-12-26',0,1 UNION ALL
    SELECT 148,N'733022683',N'jason0',N'/3/1/15/3/',4,N'Production Technician - WC30','1988-12-07',N'S',N'M','2009-01-14',0,1 UNION ALL
    SELECT 149,N'764853868',N'andy0',N'/3/1/15/4/',4,N'Production Technician - WC30','1983-10-20',N'M',N'M','2009-01-31',0,1 UNION ALL
    SELECT 150,N'878395493',N'michael5',N'/3/1/15/5/',4,N'Production Technician - WC30','1982-09-18',N'M',N'M','2009-02-26',0,1 UNION ALL
    SELECT 151,N'993310268',N'rostislav0',N'/3/1/15/6/',4,N'Production Technician - WC30','1977-09-13',N'M',N'M','2009-02-19',0,1 UNION ALL
    SELECT 152,N'319472946',N'yuhong0',N'/3/1/16/',3,N'Production Supervisor - WC20','1977-04-06',N'M',N'M','2009-02-01',0,1 UNION ALL
    SELECT 153,N'568596888',N'hanying0',N'/3/1/16/1/',4,N'Production Technician - WC20','1974-10-16',N'S',N'M','2008-12-16',0,1 UNION ALL
    SELECT 154,N'97728960',N'raymond0',N'/3/1/16/2/',4,N'Production Technician - WC20','1967-03-02',N'M',N'M','2008-12-23',0,1 UNION ALL
    SELECT 155,N'212801092',N'fadi0',N'/3/1/16/3/',4,N'Production Technician - WC20','1989-02-15',N'S',N'M','2009-01-04',0,1 UNION ALL
    SELECT 156,N'322160340',N'lane0',N'/3/1/16/4/',4,N'Production Technician - WC20','1974-09-23',N'M',N'M','2009-01-11',0,1 UNION ALL
    SELECT 157,N'812797414',N'linda1',N'/3/1/16/5/',4,N'Production Technician - WC20','1977-10-05',N'S',N'F','2009-02-03',0,1 UNION ALL
    SELECT 158,N'300946911',N'shelley0',N'/3/1/16/6/',4,N'Production Technician - WC20','1986-12-08',N'S',N'F','2009-03-07',0,1 UNION ALL
    SELECT 159,N'404159499',N'terrence0',N'/3/1/16/7/',4,N'Production Technician - WC20','1984-12-08',N'S',N'M','2009-02-16',0,1 UNION ALL
    SELECT 160,N'712885347',N'jeff0',N'/3/1/17/',3,N'Production Supervisor - WC45','1977-01-15',N'M',N'M','2009-01-21',0,1 UNION ALL
    SELECT 161,N'275962311',N'kirk0',N'/3/1/17/1/',4,N'Production Technician - WC45','1985-02-06',N'S',N'M','2008-12-15',0,1 UNION ALL
    SELECT 162,N'514829225',N'laura0',N'/3/1/17/2/',4,N'Production Technician - WC45','1980-12-25',N'S',N'F','2009-01-03',0,1 UNION ALL
    SELECT 163,N'377784364',N'alex0',N'/3/1/17/3/',4,N'Production Technician - WC45','1990-04-13',N'M',N'M','2009-02-08',0,1 UNION ALL
    SELECT 164,N'65848458',N'andrew1',N'/3/1/17/4/',4,N'Production Technician - WC45','1988-09-24',N'S',N'M','2009-03-06',0,1 UNION ALL
    SELECT 165,N'539490372',N'chris0',N'/3/1/17/5/',4,N'Production Technician - WC45','1988-12-16',N'M',N'M','2009-01-22',0,1 UNION ALL
    SELECT 166,N'60114406',N'jack0',N'/3/1/18/',3,N'Production Supervisor - WC30','1983-06-22',N'S',N'M','2009-02-21',0,1 UNION ALL
    SELECT 167,N'498138869',N'david1',N'/3/1/18/1/',4,N'Production Technician - WC30','1979-11-02',N'S',N'M','2008-12-02',0,1 UNION ALL
    SELECT 168,N'271438431',N'garrett0',N'/3/1/18/2/',4,N'Production Technician - WC30','1984-08-25',N'S',N'M','2008-12-07',0,1 UNION ALL
    SELECT 169,N'351069889',N'susan1',N'/3/1/18/3/',4,N'Production Technician - WC30','1983-04-02',N'S',N'F','2008-12-14',0,1 UNION ALL
    SELECT 170,N'476115505',N'george0',N'/3/1/18/4/',4,N'Production Technician - WC30','1977-04-16',N'M',N'M','2008-12-21',0,1 UNION ALL
    SELECT 171,N'746373306',N'david3',N'/3/1/18/5/',4,N'Production Technician - WC30','1981-08-03',N'S',N'M','2009-01-02',0,1 UNION ALL
    SELECT 172,N'364818297',N'marc0',N'/3/1/18/6/',4,N'Production Technician - WC30','1986-10-24',N'M',N'M','2009-01-16',0,1 UNION ALL
    SELECT 173,N'87268837',N'eugene0',N'/3/1/18/7/',4,N'Production Technician - WC30','1987-07-15',N'S',N'M','2009-01-21',0,1 UNION ALL
    SELECT 174,N'585408256',N'benjamin0',N'/3/1/18/8/',4,N'Production Technician - WC30','1986-01-05',N'S',N'M','2009-01-27',0,1 UNION ALL
    SELECT 175,N'259388196',N'reed0',N'/3/1/18/9/',4,N'Production Technician - WC30','1989-01-08',N'M',N'M','2009-02-02',0,1 UNION ALL
    SELECT 176,N'860123571',N'david7',N'/3/1/18/10/',4,N'Production Technician - WC30','1985-09-23',N'M',N'M','2009-02-14',0,1 UNION ALL
    SELECT 177,N'551346974',N'russell1',N'/3/1/18/11/',4,N'Production Technician - WC30','1982-02-11',N'M',N'M','2009-02-21',0,1 UNION ALL
    SELECT 178,N'568626529',N'john3',N'/3/1/18/12/',4,N'Production Technician - WC30','1982-03-24',N'S',N'M','2009-03-03',0,1 UNION ALL
    SELECT 179,N'587567941',N'jan0',N'/3/1/18/13/',4,N'Production Technician - WC30','1974-11-15',N'S',N'M','2009-03-05',0,1 UNION ALL
    SELECT 180,N'862951447',N'katie0',N'/3/1/19/',3,N'Production Supervisor - WC20','1984-11-18',N'S',N'F','2009-02-20',0,1 UNION ALL
    SELECT 181,N'545337468',N'michael0',N'/3/1/19/1/',4,N'Production Technician - WC20','1984-11-17',N'S',N'M','2008-12-09',0,1 UNION ALL
    SELECT 182,N'368920189',N'nitin0',N'/3/1/19/2/',4,N'Production Technician - WC20','1986-12-01',N'S',N'M','2008-12-28',0,1 UNION ALL
    SELECT 183,N'969985265',N'barbara0',N'/3/1/19/3/',4,N'Production Technician - WC20','1979-07-02',N'M',N'F','2009-01-22',0,1 UNION ALL
    SELECT 184,N'305522471',N'john2',N'/3/1/19/4/',4,N'Production Technician - WC20','1986-04-05',N'M',N'M','2009-02-09',0,1 UNION ALL
    SELECT 185,N'621932914',N'stefen0',N'/3/1/19/5/',4,N'Production Technician - WC20','1975-12-21',N'S',N'M','2009-02-28',0,1 UNION ALL
    SELECT 186,N'551834634',N'shane0',N'/3/1/20/',3,N'Production Supervisor - WC45','1990-05-24',N'S',N'M','2009-02-08',0,1 UNION ALL
    SELECT 187,N'713403643',N'yvonne0',N'/3/1/20/1/',4,N'Production Technician - WC45','1989-04-15',N'M',N'F','2008-12-09',0,1 UNION ALL
    SELECT 188,N'435234965',N'douglas0',N'/3/1/20/2/',4,N'Production Technician - WC45','1985-11-24',N'M',N'M','2008-12-27',0,1 UNION ALL
    SELECT 189,N'187369436',N'janeth0',N'/3/1/20/3/',4,N'Production Technician - WC45','1972-07-24',N'S',N'F','2009-01-15',0,1 UNION ALL
    SELECT 190,N'456839592',N'robert0',N'/3/1/20/4/',4,N'Production Technician - WC45','1985-02-28',N'S',N'M','2009-02-02',0,1 UNION ALL
    SELECT 191,N'399658727',N'lionel0',N'/3/1/20/5/',4,N'Production Technician - WC45','1988-03-14',N'S',N'M','2009-02-26',0,1 UNION ALL
    SELECT 192,N'634335025',N'brenda0',N'/3/1/21/',3,N'Production Supervisor - WC40','1983-02-28',N'M',N'F','2009-03-05',0,1 UNION ALL
    SELECT 193,N'761597760',N'alejandro0',N'/3/1/21/1/',4,N'Production Technician - WC40','1988-12-05',N'S',N'M','2008-12-06',0,1 UNION ALL
    SELECT 194,N'295971920',N'fred0',N'/3/1/21/2/',4,N'Production Technician - WC40','1989-06-25',N'S',N'M','2008-12-12',0,1 UNION ALL
    SELECT 195,N'918737118',N'kevin1',N'/3/1/21/3/',4,N'Production Technician - WC40','1985-12-25',N'S',N'M','2008-12-17',0,1 UNION ALL
    SELECT 196,N'370487086',N'shammi0',N'/3/1/21/4/',4,N'Production Technician - WC40','1980-10-04',N'M',N'M','2008-12-24',0,1 UNION ALL
    SELECT 197,N'632092621',N'rajesh0',N'/3/1/21/5/',4,N'Production Technician - WC40','1977-10-04',N'M',N'M','2008-12-31',0,1 UNION ALL
    SELECT 198,N'19312190',N'lorraine0',N'/3/1/21/6/',4,N'Production Technician - WC40','1988-11-26',N'M',N'F','2009-01-04',0,1 UNION ALL
    SELECT 199,N'992874797',N'paula1',N'/3/1/21/7/',4,N'Production Technician - WC40','1987-02-10',N'M',N'F','2009-01-12',0,1 UNION ALL
    SELECT 200,N'749211824',N'frank0',N'/3/1/21/8/',4,N'Production Technician - WC40','1987-09-06',N'M',N'M','2009-01-17',0,1 UNION ALL
    SELECT 201,N'746201340',N'brian0',N'/3/1/21/9/',4,N'Production Technician - WC40','1977-02-10',N'S',N'M','2009-01-29',0,1 UNION ALL
    SELECT 202,N'436757988',N'tawana0',N'/3/1/21/10/',4,N'Production Technician - WC40','1989-11-10',N'S',N'M','2009-02-05',0,1 UNION ALL
    SELECT 203,N'693168613',N'ken1',N'/3/1/21/11/',4,N'Production Technician - WC40','1981-05-28',N'M',N'M','2009-02-24',0,1 UNION ALL
    SELECT 204,N'440379437',N'gabe0',N'/3/1/21/12/',4,N'Production Technician - WC40','1988-05-10',N'M',N'M','2009-03-08',0,1 UNION ALL
    SELECT 205,N'332349500',N'lori0',N'/3/1/22/',3,N'Production Supervisor - WC45','1980-07-18',N'S',N'F','2009-02-26',0,1 UNION ALL
    SELECT 206,N'835460180',N'stuart0',N'/3/1/22/1/',4,N'Production Technician - WC45','1962-09-13',N'S',N'M','2008-12-02',0,1 UNION ALL
    SELECT 207,N'687685941',N'greg0',N'/3/1/22/2/',4,N'Production Technician - WC45','1970-10-18',N'S',N'M','2008-12-02',0,1 UNION ALL
    SELECT 208,N'199546871',N'scott0',N'/3/1/22/3/',4,N'Production Technician - WC45','1987-02-10',N'M',N'M','2009-01-08',0,1 UNION ALL
    SELECT 209,N'167554340',N'kathie0',N'/3/1/22/4/',4,N'Production Technician - WC45','1990-11-01',N'M',N'F','2009-01-27',0,1 UNION ALL
    SELECT 210,N'20244403',N'belinda0',N'/3/1/22/5/',4,N'Production Technician - WC45','1969-09-17',N'S',N'F','2009-02-20',0,1 UNION ALL
    SELECT 211,N'398223854',N'hazem0',N'/3/2/',2,N'Quality Assurance Manager','1977-10-26',N'S',N'M','2009-02-28',1,1 UNION ALL
    SELECT 212,N'885055826',N'peng0',N'/3/2/1/',3,N'Quality Assurance Supervisor','1976-03-18',N'M',N'M','2008-12-09',1,1 UNION ALL
    SELECT 213,N'343861179',N'sootha0',N'/3/2/1/1/',4,N'Quality Assurance Technician','1966-12-05',N'M',N'M','2010-02-23',0,1 UNION ALL
    SELECT 214,N'131471224',N'andreas0',N'/3/2/1/2/',4,N'Quality Assurance Technician','1989-03-28',N'M',N'M','2009-02-02',0,1 UNION ALL
    SELECT 215,N'381772114',N'mark0',N'/3/2/1/3/',4,N'Quality Assurance Technician','1986-04-30',N'S',N'M','2009-01-15',0,1 UNION ALL
    SELECT 216,N'403414852',N'sean0',N'/3/2/1/4/',4,N'Quality Assurance Technician','1976-03-06',N'S',N'M','2008-12-28',0,1 UNION ALL
    SELECT 217,N'345106466',N'zainal0',N'/3/2/2/',3,N'Document Control Manager','1976-01-30',N'M',N'M','2009-01-04',0,1 UNION ALL
    SELECT 218,N'540688287',N'tengiz0',N'/3/2/2/1/',4,N'Control Specialist','1990-04-28',N'S',N'M','2008-12-16',0,1 UNION ALL
    SELECT 219,N'242381745',N'sean1',N'/3/2/2/2/',4,N'Document Control Assistant','1987-03-12',N'S',N'M','2009-01-22',0,1 UNION ALL
    SELECT 220,N'260770918',N'karen0',N'/3/2/2/3/',4,N'Document Control Assistant','1975-12-25',N'M',N'F','2009-02-09',0,1 UNION ALL
    SELECT 221,N'260805477',N'chris1',N'/3/2/2/4/',4,N'Control Specialist','1987-05-26',N'M',N'M','2009-03-06',0,1 UNION ALL
    SELECT 222,N'685233686',N'ascott0',N'/3/3/',2,N'Master Scheduler','1968-09-17',N'S',N'M','2008-12-12',0,1 UNION ALL
    SELECT 223,N'981495526',N'sairaj0',N'/3/3/1/',3,N'Scheduling Assistant','1987-12-22',N'M',N'M','2009-01-26',0,1 UNION ALL
    SELECT 224,N'621209647',N'william0',N'/3/3/2/',3,N'Scheduling Assistant','1981-11-06',N'M',N'M','2009-01-07',0,1 UNION ALL
    SELECT 225,N'470689086',N'alan0',N'/3/3/3/',3,N'Scheduling Assistant','1984-03-29',N'M',N'M','2009-02-13',0,1 UNION ALL
    SELECT 226,N'368691270',N'brian1',N'/3/3/4/',3,N'Scheduling Assistant','1984-08-11',N'M',N'M','2009-03-03',0,1 UNION ALL
    SELECT 227,N'141165819',N'gary1',N'/3/4/',2,N'Facilities Manager','1971-02-18',N'M',N'M','2009-12-02',1,1 UNION ALL
    SELECT 228,N'553069203',N'christian0',N'/3/4/1/',3,N'Maintenance Supervisor','1976-01-18',N'M',N'M','2008-12-14',1,1 UNION ALL
    SELECT 229,N'879334904',N'lori1',N'/3/4/1/1/',4,N'Janitor','1970-07-31',N'M',N'F','2010-02-16',0,1 UNION ALL
    SELECT 230,N'28414965',N'stuart1',N'/3/4/1/2/',4,N'Janitor','1971-12-17',N'M',N'M','2010-03-05',0,1 UNION ALL
    SELECT 231,N'153479919',N'jo1',N'/3/4/1/3/',4,N'Janitor','1954-04-24',N'M',N'F','2010-03-07',0,1 UNION ALL
    SELECT 232,N'646304055',N'pat0',N'/3/4/1/4/',4,N'Janitor','1970-12-03',N'S',N'M','2010-01-27',0,1 UNION ALL
    SELECT 233,N'552560652',N'magnus0',N'/3/4/2/',3,N'Facilities Administrative Assistant','1971-08-27',N'M',N'M','2009-12-21',0,1 UNION ALL
    SELECT 234,N'184188301',N'laura1',N'/4/',1,N'Chief Financial Officer','1976-01-06',N'M',N'F','2009-01-31',1,1 UNION ALL
    SELECT 235,N'535145551',N'paula0',N'/4/1/',2,N'Human Resources Manager','1976-02-11',N'M',N'F','2008-12-06',1,1 UNION ALL
    SELECT 236,N'476980013',N'grant0',N'/4/1/1/',3,N'Human Resources Administrative Assistant','1976-04-16',N'S',N'M','2009-02-25',0,1 UNION ALL
    SELECT 237,N'416679555',N'hao0',N'/4/1/2/',3,N'Human Resources Administrative Assistant','1977-04-17',N'S',N'M','2009-02-06',0,1 UNION ALL
    SELECT 238,N'264306399',N'vidur0',N'/4/1/3/',3,N'Recruiter','1984-08-01',N'S',N'M','2009-01-01',0,1 UNION ALL
    SELECT 239,N'619308550',N'mindy0',N'/4/1/4/',3,N'Benefits Specialist','1984-11-20',N'M',N'F','2008-12-25',0,1 UNION ALL
    SELECT 240,N'332040978',N'willis0',N'/4/1/5/',3,N'Recruiter','1978-07-18',N'S',N'M','2008-12-13',0,1 UNION ALL
    SELECT 241,N'30845',N'david6',N'/4/2/',2,N'Accounts Manager','1983-07-08',N'M',N'M','2009-01-30',1,1 UNION ALL
    SELECT 242,N'363923697',N'deborah0',N'/4/2/1/',3,N'Accounts Receivable Specialist','1976-03-06',N'M',N'F','2008-12-18',0,1 UNION ALL
    SELECT 243,N'60517918',N'candy0',N'/4/2/2/',3,N'Accounts Receivable Specialist','1976-02-23',N'S',N'F','2009-01-06',0,1 UNION ALL
    SELECT 244,N'931190412',N'bryan1',N'/4/2/3/',3,N'Accounts Receivable Specialist','1984-09-20',N'S',N'M','2009-01-24',0,1 UNION ALL
    SELECT 245,N'363910111',N'barbara1',N'/4/2/4/',3,N'Accountant','1976-01-04',N'M',N'F','2009-02-18',1,1 UNION ALL
    SELECT 246,N'663843431',N'dragan0',N'/4/2/5/',3,N'Accounts Payable Specialist','1977-02-14',N'M',N'M','2009-02-11',0,1 UNION ALL
    SELECT 247,N'519756660',N'janet0',N'/4/2/6/',3,N'Accounts Payable Specialist','1979-03-09',N'M',N'F','2009-03-01',0,1 UNION ALL
    SELECT 248,N'480951955',N'mike0',N'/4/2/7/',3,N'Accountant','1979-07-01',N'S',N'M','2009-03-08',1,1 UNION ALL
    SELECT 249,N'121491555',N'wendy0',N'/4/3/',2,N'Finance Manager','1984-10-11',N'S',N'F','2008-12-25',1,1 UNION ALL
    SELECT 250,N'895209680',N'sheela0',N'/4/3/1/',3,N'Purchasing Manager','1978-02-10',N'S',N'F','2011-02-25',1,1 UNION ALL
    SELECT 251,N'603686790',N'mikael0',N'/4/3/1/1/',4,N'Buyer','1984-08-17',N'S',N'M','2009-02-10',0,1 UNION ALL
    SELECT 252,N'792847334',N'arvind0',N'/4/3/1/2/',4,N'Buyer','1974-08-21',N'M',N'M','2009-02-28',0,1 UNION ALL
    SELECT 253,N'407505660',N'linda2',N'/4/3/1/3/',4,N'Buyer','1970-11-30',N'M',N'F','2009-12-17',0,1 UNION ALL
    SELECT 254,N'482810518',N'fukiko0',N'/4/3/1/4/',4,N'Buyer','1970-11-24',N'M',N'M','2010-01-04',0,1 UNION ALL
    SELECT 255,N'466142721',N'gordon0',N'/4/3/1/5/',4,N'Buyer','1966-11-29',N'M',N'M','2010-01-11',0,1 UNION ALL
    SELECT 256,N'367453993',N'frank2',N'/4/3/1/6/',4,N'Buyer','1952-05-12',N'M',N'M','2010-01-23',0,1 UNION ALL
    SELECT 257,N'381073001',N'eric2',N'/4/3/1/7/',4,N'Buyer','1972-09-17',N'S',N'M','2010-01-27',0,1 UNION ALL
    SELECT 258,N'785853949',N'erin0',N'/4/3/1/8/',4,N'Buyer','1971-01-04',N'S',N'F','2010-01-31',0,1 UNION ALL
    SELECT 259,N'20269531',N'ben0',N'/4/3/1/9/',4,N'Buyer','1973-06-03',N'M',N'M','2010-03-09',0,1 UNION ALL
    SELECT 260,N'437296311',N'annette0',N'/4/3/1/10/',4,N'Purchasing Assistant','1978-01-29',N'M',N'F','2010-12-06',0,1 UNION ALL
    SELECT 261,N'280633567',N'reinout0',N'/4/3/1/11/',4,N'Purchasing Assistant','1978-01-17',N'M',N'M','2010-12-25',0,1 UNION ALL
    SELECT 262,N'231203233',N'david5',N'/4/4/',2,N'Assistant to the Chief Financial Officer','1964-06-21',N'S',N'M','2009-01-12',0,1 UNION ALL
    SELECT 263,N'441044382',N'jean0',N'/5/',1,N'Information Services Manager','1975-12-13',N'S',N'F','2008-12-11',1,1 UNION ALL
    SELECT 264,N'858323870',N'stephanie0',N'/5/1/',2,N'Network Manager','1984-03-25',N'S',N'F','2009-02-04',1,1 UNION ALL
    SELECT 265,N'749389530',N'ashvini0',N'/5/1/1/',3,N'Network Administrator','1977-03-27',N'S',N'M','2008-12-04',0,1 UNION ALL
    SELECT 266,N'672243793',N'peter1',N'/5/1/2/',3,N'Network Administrator','1980-05-28',N'S',N'M','2009-02-23',0,1 UNION ALL
    SELECT 267,N'58317344',N'karen1',N'/5/2/',2,N'Application Specialist','1978-05-19',N'S',N'F','2009-02-16',1,1 UNION ALL
    SELECT 268,N'314747499',N'ramesh0',N'/5/3/',2,N'Application Specialist','1988-03-13',N'S',N'M','2009-02-03',1,1 UNION ALL
    SELECT 269,N'671089628',N'dan0',N'/5/4/',2,N'Application Specialist','1987-05-26',N'M',N'M','2009-01-11',1,1 UNION ALL
    SELECT 270,N'643805155',N'françois0',N'/5/5/',2,N'Database Administrator','1975-05-17',N'S',N'M','2009-01-17',1,1 UNION ALL
    SELECT 271,N'929666391',N'dan1',N'/5/6/',2,N'Database Administrator','1976-01-06',N'M',N'M','2009-01-22',1,1 UNION ALL
    SELECT 272,N'525932996',N'janaina0',N'/5/7/',2,N'Application Specialist','1985-01-30',N'M',N'F','2008-12-23',1,1 UNION ALL
    SELECT 273,N'112432117',N'brian3',N'/6/',1,N'Vice President of Sales','1977-06-06',N'S',N'M','2011-02-15',1,1 UNION ALL
    SELECT 274,N'502097814',N'stephen0',N'/6/1/',2,N'North American Sales Manager','1951-10-17',N'M',N'M','2011-01-04',1,1 UNION ALL
    SELECT 275,N'841560125',N'michael9',N'/6/1/1/',3,N'Sales Representative','1968-12-25',N'S',N'M','2011-05-31',1,1 UNION ALL
    SELECT 276,N'191644724',N'linda3',N'/6/1/2/',3,N'Sales Representative','1980-02-27',N'M',N'F','2011-05-31',1,1 UNION ALL
    SELECT 277,N'615389812',N'jillian0',N'/6/1/3/',3,N'Sales Representative','1962-08-29',N'S',N'F','2011-05-31',1,1 UNION ALL
    SELECT 278,N'234474252',N'garrett1',N'/6/1/4/',3,N'Sales Representative','1975-02-04',N'M',N'M','2011-05-31',1,1 UNION ALL
    SELECT 279,N'716374314',N'tsvi0',N'/6/1/5/',3,N'Sales Representative','1974-01-18',N'M',N'M','2011-05-31',1,1 UNION ALL
    SELECT 280,N'61161660',N'pamela0',N'/6/1/6/',3,N'Sales Representative','1974-12-06',N'S',N'F','2011-05-31',1,1 UNION ALL
    SELECT 281,N'139397894',N'shu0',N'/6/1/7/',3,N'Sales Representative','1968-03-09',N'M',N'M','2011-05-31',1,1 UNION ALL
    SELECT 282,N'399771412',N'josé1',N'/6/1/8/',3,N'Sales Representative','1963-12-11',N'M',N'M','2011-05-31',1,1 UNION ALL
    SELECT 283,N'987554265',N'david8',N'/6/1/9/',3,N'Sales Representative','1974-02-11',N'S',N'M','2011-05-31',1,1 UNION ALL
    SELECT 284,N'90836195',N'tete0',N'/6/1/10/',3,N'Sales Representative','1978-01-05',N'M',N'M','2012-09-30',1,1 UNION ALL
    SELECT 285,N'481044938',N'syed0',N'/6/2/',2,N'Pacific Sales Manager','1975-01-11',N'M',N'M','2013-03-14',1,1 UNION ALL
    SELECT 286,N'758596752',N'lynn0',N'/6/2/1/',3,N'Sales Representative','1977-02-14',N'S',N'F','2013-05-30',1,1 UNION ALL
    SELECT 287,N'982310417',N'amy0',N'/6/3/',2,N'European Sales Manager','1957-09-20',N'M',N'F','2012-04-16',1,1 UNION ALL
    SELECT 288,N'954276278',N'rachel0',N'/6/3/1/',3,N'Sales Representative','1975-07-09',N'S',N'F','2013-05-30',1,1 UNION ALL
    SELECT 289,N'668991357',N'jae0',N'/6/3/2/',3,N'Sales Representative','1968-03-17',N'M',N'F','2012-05-30',1,1 UNION ALL
    SELECT 290,N'134219713',N'ranjit0',N'/6/3/3/',3,N'Sales Representative','1975-09-30',N'S',N'M','2012-05-30',1,1;
END