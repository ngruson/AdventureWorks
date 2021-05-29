∫G
éC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Controllers\CustomerController.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Controllers( 3
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
CustomerController #
:$ %
ControllerBase& 4
{ 
private 
readonly 
ILogger  
<  !
CustomerController! 3
>3 4
logger5 ;
;; <
private 
readonly 
	IMediator "
mediator# +
;+ ,
private 
readonly 
IMapper  
mapper! '
;' (
public 
CustomerController !
(! "
ILogger" )
<) *
CustomerController* <
>< =
logger> D
,D E
	IMediatorF O
mediatorP X
,X Y
IMapperZ a
mapperb h
)h i
=>j l
( 
this 
. 
logger 
, 
this 
. 
mediator '
,' (
this) -
.- .
mapper. 4
)4 5
=6 7
(8 9
logger9 ?
,? @
mediatorA I
,I J
mapperK Q
)Q R
;R S
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetCustomers) 5
(5 6
[6 7
	FromQuery7 @
]@ A
GetCustomersQueryB S
queryT Y
)Y Z
{ 	
logger 
. 
LogInformation !
(! "
$str" G
,G H
queryI N
)N O
;O P
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var   
	customers   
=   
await   !
mediator  " *
.  * +
Send  + /
(  / 0
query  0 5
)  5 6
;  6 7
if"" 
("" 
	customers"" 
=="" 
null"" !
||""" $
!""% &
	customers""& /
.""/ 0
	Customers""0 9
.""9 :
Any"": =
(""= >
)""> ?
)""? @
{## 
logger$$ 
.$$ 
LogInformation$$ %
($$% &
$str$$& :
)$$: ;
;$$; <
return%% 
new%% 
NotFoundResult%% )
(%%) *
)%%* +
;%%+ ,
}&& 
logger(( 
.(( 
LogInformation(( !
(((! "
$str((" 7
)((7 8
;((8 9
return)) 
Ok)) 
()) 
mapper)) 
.)) 
Map))  
<))  !
Models))! '
.))' (
GetCustomers))( 4
.))4 5
GetCustomersResult))5 G
>))G H
())H I
	customers))I R
)))R S
)))S T
;))T U
}** 	
[,, 	
HttpGet,,	 
(,, 
$str,, "
),," #
],,# $
public-- 
async-- 
Task-- 
<-- 
IActionResult-- '
>--' (
GetCustomer--) 4
(--4 5
[--5 6
	FromRoute--6 ?
]--? @
GetCustomerQuery--A Q
query--R W
)--W X
{.. 	
logger// 
.// 
LogInformation// !
(//! "
$str//" I
,//I J
query//K P
)//P Q
;//Q R
logger11 
.11 
LogInformation11 !
(11! "
$str11" A
)11A B
;11B C
var22 
customer22 
=22 
await22  
mediator22! )
.22) *
Send22* .
(22. /
query22/ 4
)224 5
;225 6
if44 
(44 
customer44 
==44 
null44  
)44  !
{55 
logger66 
.66 
LogInformation66 %
(66% &
$str66& >
)66> ?
;66? @
return77 
new77 
NotFoundResult77 )
(77) *
)77* +
;77+ ,
}88 
logger:: 
.:: 
LogInformation:: !
(::! "
$str::" 6
)::6 7
;::7 8
return;; 
Ok;; 
(;; 
mapper;; 
.;; 
Map;;  
<;;  !
Models;;! '
.;;' (
GetCustomer;;( 3
.;;3 4
Customer;;4 <
>;;< =
(;;= >
customer;;> F
);;F G
);;G H
;;;H I
}<< 	
[>> 	
HttpPost>>	 
]>> 
public?? 
async?? 
Task?? 
<?? 
IActionResult?? '
>??' (
AddCustomer??) 4
(??4 5
AddCustomerCommand??5 G
command??H O
)??O P
{@@ 	
loggerAA 
.AA 
LogInformationAA !
(AA! "
$strAA" 6
)AA6 7
;AA7 8
loggerCC 
.CC 
LogInformationCC !
(CC! "
$strCC" C
)CCC D
;CCD E
varDD 
customerDD 
=DD 
awaitDD  
mediatorDD! )
.DD) *
SendDD* .
(DD. /
commandDD/ 6
)DD6 7
;DD7 8
loggerFF 
.FF 
LogInformationFF !
(FF! "
$strFF" 6
)FF6 7
;FF7 8
returnGG 
CreatedGG 
(GG 
$"GG 
$strGG 
{GG 
customerGG '
.GG' (
AccountNumberGG( 5
}GG5 6
"GG6 7
,GG7 8
customerGG9 A
)GGA B
;GGB C
}HH 	
[JJ 	
HttpPutJJ	 
(JJ 
$strJJ "
)JJ" #
]JJ# $
publicKK 
asyncKK 
TaskKK 
<KK 
IActionResultKK '
>KK' (
UpdateCustomerKK) 7
(KK7 8
stringKK8 >
accountNumberKK? L
,KKL M
ModelsKKN T
.KKT U
UpdateCustomerKKU c
.KKc d
CustomerKKd l
customerKKm u
)KKu v
{LL 	
loggerMM 
.MM 
LogInformationMM !
(MM! "
$strMM" 9
)MM9 :
;MM: ;
loggerOO 
.OO 
LogInformationOO !
(OO! "
$strOO" F
)OOF G
;OOG H
varPP 
commandPP 
=PP 
mapperPP  
.PP  !
MapPP! $
<PP$ %!
UpdateCustomerCommandPP% :
>PP: ;
(PP; <
customerPP< D
)PPD E
;PPE F
commandQQ 
.QQ 
CustomerQQ 
.QQ 
AccountNumberQQ *
=QQ+ ,
accountNumberQQ- :
;QQ: ;
varRR 
updatedCustomerRR 
=RR  !
awaitRR" '
mediatorRR( 0
.RR0 1
SendRR1 5
(RR5 6
commandRR6 =
)RR= >
;RR> ?
loggerTT 
.TT 
LogInformationTT !
(TT! "
$strTT" 6
)TT6 7
;TT7 8
returnUU 
OkUU 
(UU 
mapperUU 
.UU 
MapUU  
<UU  !
ModelsUU! '
.UU' (
UpdateCustomerUU( 6
.UU6 7
CustomerUU7 ?
>UU? @
(UU@ A
updatedCustomerUUA P
)UUP Q
)UUQ R
;UUR S
}VV 	
[XX 	

HttpDeleteXX	 
]XX 
publicYY 
asyncYY 
TaskYY 
<YY 
IActionResultYY '
>YY' (
DeleteCustomerYY) 7
(YY7 8!
DeleteCustomerCommandYY8 M
commandYYN U
)YYU V
{ZZ 	
logger[[ 
.[[ 
LogInformation[[ !
([[! "
$str[[" ]
,[[] ^
command[[_ f
.[[f g
AccountNumber[[g t
)[[t u
;[[u v
logger]] 
.]] 
LogInformation]] !
(]]! "
$str]]" F
)]]F G
;]]G H
await^^ 
mediator^^ 
.^^ 
Send^^ 
(^^  
command^^  '
)^^' (
;^^( )
logger__ 
.__ 
LogInformation__ !
(__! "
$str__" 4
)__4 5
;__5 6
returnaa 
	NoContentaa 
(aa 
)aa 
;aa 
}bb 	
}cc 
}dd ¨
åC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Extensions\SwaggerExtensions.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (

Extensions( 2
{ 
public 

static 
class 
SwaggerExtensions )
{ 
public 
static 
IApplicationBuilder )#
UseSwaggerDocumentation* A
(A B
thisB F
IApplicationBuilderG Z
app[ ^
,^ _
string` f
virtualPathg r
,r s*
IApiVersionDescriptionProvider		 *
provider		+ 3
)		3 4
{

 	
app 
. 

UseSwagger 
( 
) 
. 
UseSwaggerUI 
( 
options %
=>& (
{ 
foreach 
( 
var  
description! ,
in- /
provider0 8
.8 9"
ApiVersionDescriptions9 O
)O P
{ 
options 
.  
SwaggerEndpoint  /
(/ 0
$"0 2
{2 3
virtualPath3 >
}> ?
$str? H
{H I
descriptionI T
.T U
	GroupNameU ^
}^ _
$str_ l
"l m
,m n
$"o q
$strq ~
{~ 
description	 ä
.
ä ã
	GroupName
ã î
.
î ï
ToUpperInvariant
ï •
(
• ¶
)
¶ ß
}
ß ®
"
® ©
)
© ™
;
™ ´
options 
.  
RoutePrefix  +
=, -
string. 4
.4 5
Empty5 :
;: ;
} 
options 
. 
DocumentTitle )
=* +
$str, H
;H I
} 
) 
; 
return 
app 
; 
} 	
} 
} ´+
êC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\JsonConverters\CustomerConverter.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
JsonConverters( 6
{ 
public		 

class		 
CustomerConverter		 "
<		" #
T		# $
,		$ %
TStore		& ,
,		, -
TIndividual		. 9
>		9 :
:		; <
JsonConverter		= J
<		J K
T		K L
>		L M
where

 
T

 
:

 
class

 
where 
TStore 
: 
class 
, 
T 
where 
TIndividual 
: 
class !
,! "
T# $
{ 
public 
override 
T 
Read 
( 
ref "
Utf8JsonReader# 1
reader2 8
,8 9
Type: >
typeToConvert? L
,L M!
JsonSerializerOptionsN c
optionsd k
)k l
{ 	
if 
( 
reader 
. 
	TokenType  
==! #
JsonTokenType$ 1
.1 2
Null2 6
)6 7
return8 >
null? C
;C D
var 

readerCopy 
= 
reader #
;# $
using 
var 
jsonDocument "
=# $
JsonDocument% 1
.1 2

ParseValue2 <
(< =
ref= @

readerCopyA K
)K L
;L M
var 

jsonObject 
= 
jsonDocument )
.) *
RootElement* 5
;5 6
var 
customerType 
= 

jsonObject )
.) *
GetProperty* 5
(5 6
$str6 D
)D E
;E F
Guard 
. 
Against 
. 
Null 
( 
customerType +
,+ ,
nameof- 3
(3 4
customerType4 @
)@ A
)A B
;B C
if 
( 
! 
string 
. 
IsNullOrEmpty %
(% &
customerType& 2
.2 3
	GetString3 <
(< =
)= >
)> ?
)? @
{ 
if 
( 
Enum 
. 
Parse 
< 
CustomerType +
>+ ,
(, -
customerType- 9
.9 :
	GetString: C
(C D
)D E
)E F
==G I
CustomerTypeJ V
.V W
StoreW \
)\ ]
return 
JsonSerializer )
.) *
Deserialize* 5
(5 6
ref6 9
reader: @
,@ A
typeofB H
(H I
TStoreI O
)O P
,P Q
optionsR Y
)Y Z
as[ ]
TStore^ d
;d e
if 
( 
Enum 
. 
Parse 
< 
CustomerType +
>+ ,
(, -
customerType- 9
.9 :
	GetString: C
(C D
)D E
)E F
==G I
CustomerTypeJ V
.V W

IndividualW a
)a b
return   
JsonSerializer   )
.  ) *
Deserialize  * 5
(  5 6
ref  6 9
reader  : @
,  @ A
typeof  B H
(  H I
TIndividual  I T
)  T U
,  U V
options  W ^
)  ^ _
as  ` b
TIndividual  c n
;  n o
}!! 
throw## 
new## !
NotSupportedException## +
(##+ ,
$"##, .
{##. /
customerType##/ ;
.##; <
	GetString##< E
(##E F
)##F G
??##H J
$str##K V
}##V W
$str##W o
"##o p
)##p q
;##q r
}$$ 	
public&& 
override&& 
void&& 
Write&& "
(&&" #
Utf8JsonWriter&&# 1
writer&&2 8
,&&8 9
T&&: ;
value&&< A
,&&A B!
JsonSerializerOptions&&C X
options&&Y `
)&&` a
{'' 	
switch(( 
((( 
value(( 
)(( 
{)) 
case** 
null** 
:** 
JsonSerializer++ "
.++" #
	Serialize++# ,
(++, -
writer++- 3
,++3 4
(++5 6
T++6 7
)++7 8
null++8 <
,++< =
options++> E
)++E F
;++F G
break,, 
;,, 
default-- 
:-- 
{.. 
JsonSerializer// &
.//& '
	Serialize//' 0
(//0 1
writer//1 7
,//7 8
value//9 >
,//> ?
value//@ E
.//E F
GetType//F M
(//M N
)//N O
,//O P
options//Q X
)//X Y
;//Y Z
break00 
;00 
}11 
}22 
}33 	
}44 
}55 Ì)
~C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\MappingProfile.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
{ 
public 

class 
MappingProfile 
:  !
BaseMappingProfile" 4
{ 
public		 
MappingProfile		 
(		 
)		 
:		  !
base		" &
(		& '
)		' (
{

 	%
ApplyMappingsFromAssembly %
(% &
Assembly& .
.. / 
GetExecutingAssembly/ C
(C D
)D E
)E F
;F G
	CreateMap 
< 
app 
. 
GetCustomers &
.& '
CustomerDto' 2
,2 3
Models4 :
.: ;
GetCustomers; G
.G H
CustomerH P
>P Q
(Q R
)R S
. 
Include 
< 
app 
. 
GetCustomers )
.) *!
IndividualCustomerDto* ?
,? @
ModelsA G
.G H
GetCustomersH T
.T U
IndividualCustomerU g
>g h
(h i
)i j
. 
Include 
< 
app 
. 
GetCustomers )
.) *
StoreCustomerDto* :
,: ;
Models< B
.B C
GetCustomersC O
.O P
StoreCustomerP ]
>] ^
(^ _
)_ `
;` a
	CreateMap 
< 
app 
. 
GetCustomer %
.% &
CustomerDto& 1
,1 2
Models3 9
.9 :
GetCustomer: E
.E F
CustomerF N
>N O
(O P
)P Q
. 
Include 
< 
app 
. 
GetCustomer (
.( )!
IndividualCustomerDto) >
,> ?
Models@ F
.F G
GetCustomerG R
.R S
IndividualCustomerS e
>e f
(f g
)g h
. 
Include 
< 
app 
. 
GetCustomer (
.( )
StoreCustomerDto) 9
,9 :
Models; A
.A B
GetCustomerB M
.M N
StoreCustomerN [
>[ \
(\ ]
)] ^
;^ _
	CreateMap 
< 
Models 
. 
UpdateCustomer +
.+ ,
Customer, 4
,4 5
app6 9
.9 :
UpdateCustomer: H
.H I!
UpdateCustomerCommandI ^
>^ _
(_ `
)` a
. 
	ForMember 
( 
m 
=> 
m  !
.! "
Customer" *
,* +
opt, /
=>0 2
opt3 6
.6 7
MapFrom7 >
(> ?
src? B
=>C E
srcF I
)I J
)J K
;K L
	CreateMap 
< 
Models 
. 
UpdateCustomer +
.+ ,
Customer, 4
,4 5
app6 9
.9 :
UpdateCustomer: H
.H I
CustomerDtoI T
>T U
(U V
)V W
. 
	ForMember 
( 
m 
=> 
m  !
.! "
AccountNumber" /
,/ 0
opt1 4
=>5 7
opt8 ;
.; <
Ignore< B
(B C
)C D
)D E
. 
Include 
< 
Models 
.  
UpdateCustomer  .
.. /
IndividualCustomer/ A
,A B
appC F
.F G
UpdateCustomerG U
.U V!
IndividualCustomerDtoV k
>k l
(l m
)m n
. 
Include 
< 
Models 
.  
UpdateCustomer  .
.. /
StoreCustomer/ <
,< =
app> A
.A B
UpdateCustomerB P
.P Q
StoreCustomerDtoQ a
>a b
(b c
)c d
. 

ReverseMap 
( 
) 
. 
Include 
< 
app 
. 
UpdateCustomer +
.+ ,!
IndividualCustomerDto, A
,A B
ModelsC I
.I J
UpdateCustomerJ X
.X Y
IndividualCustomerY k
>k l
(l m
)m n
. 
Include 
< 
app 
. 
UpdateCustomer +
.+ ,
StoreCustomerDto, <
,< =
Models> D
.D E
UpdateCustomerE S
.S T
StoreCustomerT a
>a b
(b c
)c d
;d e
} 	
}   
}!! ã
ÉC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\CustomerType.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
{ 
public 

enum 
CustomerType 
{ 

Individual 
, 
Store 
} 
} ô
ãC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\Address.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class 
Address 
: 
IMapFrom #
<# $

AddressDto$ .
>. /
{ 
public 
string 
AddressLine1 "
{# $
get% (
;( )
set* -
;- .
}/ 0
public		 
string		 
AddressLine2		 "
{		# $
get		% (
;		( )
set		* -
;		- .
}		/ 0
public

 
string

 

PostalCode

  
{

! "
get

# &
;

& '
set

( +
;

+ ,
}

- .
public 
string 
City 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
StateProvinceCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
CountryRegionCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} ˛

åC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\Customer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

abstract 
class 
Customer "
{ 
[ 	
JsonConverter	 
( 
typeof 
( #
JsonStringEnumConverter 5
)5 6
)6 7
]7 8
public		 
CustomerType		 
CustomerType		 (
{		) *
get		+ .
;		. /
set		0 3
;		3 4
}		5 6
public

 
string

 
AccountNumber

 #
{

$ %
get

& )
;

) *
set

+ .
;

. /
}

0 1
public 
string 
	Territory 
{  !
get" %
;% &
set' *
;* +
}, -
public 
List 
< 
CustomerAddress #
># $
	Addresses% .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
} 
} ¨
ìC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\CustomerAddress.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class 
CustomerAddress  
:! "
IMapFrom# +
<+ ,
CustomerAddressDto, >
>> ?
{ 
public 
string 
AddressType !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
Address		 
Address		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
}

 
} ‚
ñC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\GetCustomersResult.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class 
GetCustomersResult #
:$ %
IMapFrom& .
<. /
GetCustomersDto/ >
>> ?
{ 
public		 
List		 
<		 
Customer		 
>		 
	Customers		 '
{		( )
get		* -
;		- .
set		/ 2
;		2 3
}		4 5
public

 
int

 
TotalCustomers

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
} 
} π
ñC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\IndividualCustomer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class 
IndividualCustomer #
:$ %
Customer& .
,. /
IMapFrom0 8
<8 9!
IndividualCustomerDto9 N
>N O
{ 
public 
Person 
Person 
{ 
get "
;" #
set$ '
;' (
}) *
}		 
}

 ì
äC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\Person.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class 
Person 
: 
IMapFrom "
<" #
	PersonDto# ,
>, -
{ 
public		 
string		 
Title		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
string

 
	FirstName

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
public 
string 

MiddleName  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
LastName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Suffix 
{ 
get "
;" #
set$ '
;' (
}) *
public 
List 
< 
PersonEmailAddress &
>& '
EmailAddresses( 6
{7 8
get9 <
;< =
set> A
;A B
}C D
public 
List 
< 
PersonPhone 
>  
PhoneNumbers! -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} 
} ö
ñC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\PersonEmailAddress.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class 
PersonEmailAddress #
:$ %
IMapFrom& .
<. /!
PersonEmailAddressDto/ D
>D E
{ 
public 
string 
EmailAddress "
{# $
get% (
;( )
set* -
;- .
}/ 0
}		 
}

 ß
èC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\PersonPhone.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class 
PersonPhone 
: 
IMapFrom '
<' (
PersonPhoneDto( 6
>6 7
{ 
public 
string 
PhoneNumberType %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public		 
string		 
PhoneNumber		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
}

 
} °	
ëC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\StoreCustomer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class 
StoreCustomer 
:  
Customer! )
,) *
IMapFrom+ 3
<3 4
StoreCustomerDto4 D
>D E
{ 
public		 
string		 
Name		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public

 
string

 
SalesPerson

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
public 
List 
<  
StoreCustomerContact (
>( )
Contacts* 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
} 
} ¿
òC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomers\StoreCustomerContact.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomers/ ;
{ 
public 

class  
StoreCustomerContact %
:& '
IMapFrom( 0
<0 1#
StoreCustomerContactDto1 H
>H I
{ 
public 
string 
ContactType !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
Person		 
ContactPerson		 #
{		$ %
get		& )
;		) *
set		+ .
;		. /
}		0 1
}

 
} ó
äC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\Address.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class 
Address 
: 
IMapFrom #
<# $

AddressDto$ .
>. /
{ 
public 
string 
AddressLine1 "
{# $
get% (
;( )
set* -
;- .
}/ 0
public		 
string		 
AddressLine2		 "
{		# $
get		% (
;		( )
set		* -
;		- .
}		/ 0
public

 
string

 

PostalCode

  
{

! "
get

# &
;

& '
set

( +
;

+ ,
}

- .
public 
string 
City 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
StateProvinceCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
CountryRegionCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} œ
ãC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\Customer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

abstract 
class 
Customer "
{ 
[ 	
JsonConverter	 
( 
typeof 
( #
JsonStringEnumConverter 5
)5 6
)6 7
]7 8
public		 
CustomerType		 
CustomerType		 (
{		) *
get		+ .
;		. /
set		0 3
;		3 4
}		5 6
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
	Territory 
{  !
get" %
;% &
set' *
;* +
}, -
public 
List 
< 
CustomerAddress #
># $
	Addresses% .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
List 
< 

SalesOrder 
> 
SalesOrders  +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
} 
} ™
íC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\CustomerAddress.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class 
CustomerAddress  
:! "
IMapFrom# +
<+ ,
CustomerAddressDto, >
>> ?
{ 
public 
string 
AddressType !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
Address		 
Address		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
}

 
} ∑
ïC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\IndividualCustomer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class 
IndividualCustomer #
:$ %
Customer& .
,. /
IMapFrom0 8
<8 9!
IndividualCustomerDto9 N
>N O
{ 
public 
Person 
Person 
{ 
get "
;" #
set$ '
;' (
}) *
}		 
}

 ë
âC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\Person.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class 
Person 
: 
IMapFrom "
<" #
	PersonDto# ,
>, -
{ 
public		 
string		 
Title		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
string

 
	FirstName

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
public 
string 

MiddleName  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
LastName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Suffix 
{ 
get "
;" #
set$ '
;' (
}) *
public 
List 
< 
PersonEmailAddress &
>& '
EmailAddresses( 6
{7 8
get9 <
;< =
set> A
;A B
}C D
public 
List 
< 
PersonPhone 
>  
PhoneNumbers! -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} 
} ò
ïC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\PersonEmailAddress.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class 
PersonEmailAddress #
:$ %
IMapFrom& .
<. /!
PersonEmailAddressDto/ D
>D E
{ 
public 
string 
EmailAddress "
{# $
get% (
;( )
set* -
;- .
}/ 0
}		 
}

 •
éC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\PersonPhone.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class 
PersonPhone 
: 
IMapFrom '
<' (
PersonPhoneDto( 6
>6 7
{ 
public 
string 
PhoneNumberType %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public		 
string		 
PhoneNumber		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
}

 
} ≈
çC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\SalesOrder.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class 

SalesOrder 
: 
IMapFrom &
<& '
SalesOrderDto' 4
>4 5
{		 
public

 
DateTime

 
	OrderDate

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
public 
DateTime 
DueDate 
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 
? 
ShipDate !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
JsonConverter	 
( 
typeof 
( #
JsonStringEnumConverter 5
)5 6
)6 7
]7 8
public 
SalesOrderStatus 
Status  &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
bool 
OnlineOrderFlag #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
SalesOrderNumber &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
PurchaseOrderNumber )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 
TotalDue 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} «
ìC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\SalesOrderStatus.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

enum 
SalesOrderStatus  
:! "
byte# '
{ 
	InProcess 
= 
$num 
, 
Approved 
= 
$num 
, 
Backordered 
= 
$num 
, 
Rejected 
= 
$num 
, 
Shipped		 
=		 
$num		 
,		 
	Cancelled

 
=

 
$num

 
} 
} ü	
êC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\StoreCustomer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class 
StoreCustomer 
:  
Customer! )
,) *
IMapFrom+ 3
<3 4
StoreCustomerDto4 D
>D E
{ 
public		 
string		 
Name		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public

 
string

 
SalesPerson

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
public 
List 
<  
StoreCustomerContact (
>( )
Contacts* 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
} 
} æ
óC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\GetCustomer\StoreCustomerContact.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
GetCustomer/ :
{ 
public 

class  
StoreCustomerContact %
:& '
IMapFrom( 0
<0 1#
StoreCustomerContactDto1 H
>H I
{ 
public 
string 
ContactType !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
Person		 
ContactPerson		 #
{		$ %
get		& )
;		) *
set		+ .
;		. /
}		0 1
}

 
} ≈
çC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\Address.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

class 
Address 
: 
IMapFrom #
<# $

AddressDto$ .
>. /
{ 
public		 
string		 
AddressLine1		 "
{		# $
get		% (
;		( )
set		* -
;		- .
}		/ 0
public

 
string

 
AddressLine2

 "
{

# $
get

% (
;

( )
set

* -
;

- .
}

/ 0
public 
string 

PostalCode  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
City 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
StateProvinceCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
CountryRegionCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Address %
,% &

AddressDto' 1
>1 2
(2 3
)3 4
. 

ReverseMap 
( 
) 
; 
} 	
} 
} ◊	
éC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\Customer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

abstract 
class 
Customer "
{ 
[ 	
JsonConverter	 
( 
typeof 
( #
JsonStringEnumConverter 5
)5 6
)6 7
]7 8
public		 
abstract		 
CustomerType		 $
CustomerType		% 1
{		2 3
get		4 7
;		7 8
}		9 :
public 
string 
	Territory 
{  !
get" %
;% &
set' *
;* +
}, -
public 
List 
< 
CustomerAddress #
># $
	Addresses% .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
} 
} Ë

ïC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\CustomerAddress.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

class 
CustomerAddress  
:! "
IMapFrom# +
<+ ,
CustomerAddressDto, >
>> ?
{ 
public		 
string		 
AddressType		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public

 
Address

 
Address

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
CustomerAddress -
,- .
CustomerAddressDto/ A
>A B
(B C
)C D
. 

ReverseMap 
( 
) 
; 
} 	
} 
} á
òC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\IndividualCustomer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

class 
IndividualCustomer #
:$ %
Customer& .
,. /
IMapFrom0 8
<8 9!
IndividualCustomerDto9 N
>N O
{ 
public		 
override		 
CustomerType		 $
CustomerType		% 1
=>		2 4
CustomerType		5 A
.		A B

Individual		B L
;		L M
public

 
Person

 
Person

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
=

+ ,
new

- 0
Person

1 7
(

7 8
)

8 9
;

9 :
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
IndividualCustomer 0
,0 1!
IndividualCustomerDto2 G
>G H
(H I
)I J
. 
	ForMember 
( 
m 
=> 
m  !
.! "
Person" (
,( )
opt* -
=>. 0
opt1 4
.4 5
MapFrom5 <
(< =
src= @
=>A C
srcD G
.G H
PersonH N
)N O
)O P
. 

ReverseMap 
( 
) 
; 
} 	
} 
} Ω
åC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\Person.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

class 
Person 
: 
IMapFrom "
<" #
	PersonDto# ,
>, -
{		 
public

 
string

 
Title

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
public 
string 
	FirstName 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 

MiddleName  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
LastName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Suffix 
{ 
get "
;" #
set$ '
;' (
}) *
public 
List 
< 
PersonEmailAddress &
>& '
EmailAddresses( 6
{7 8
get9 <
;< =
set> A
;A B
}C D
public 
List 
< 
PersonPhone 
>  
PhoneNumbers! -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Person $
,$ %
	PersonDto& /
>/ 0
(0 1
)1 2
. 

ReverseMap 
( 
) 
; 
} 	
} 
} ‹	
òC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\PersonEmailAddress.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

class 
PersonEmailAddress #
:$ %
IMapFrom& .
<. /!
PersonEmailAddressDto/ D
>D E
{ 
public		 
string		 
EmailAddress		 "
{		# $
get		% (
;		( )
set		* -
;		- .
}		/ 0
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
PersonEmailAddress 0
,0 1!
PersonEmailAddressDto2 G
>G H
(H I
)I J
. 

ReverseMap 
( 
) 
; 
} 	
} 
} €

ëC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\PersonPhone.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

class 
PersonPhone 
: 
IMapFrom '
<' (
PersonPhoneDto( 6
>6 7
{ 
public		 
string		 
PhoneNumberType		 %
{		& '
get		( +
;		+ ,
set		- 0
;		0 1
}		2 3
public

 
string

 
PhoneNumber

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
PersonPhone )
,) *
PersonPhoneDto+ 9
>9 :
(: ;
); <
. 

ReverseMap 
( 
) 
; 
} 	
} 
} í
ìC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\StoreCustomer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

class 
StoreCustomer 
:  
Customer! )
,) *
IMapFrom+ 3
<3 4
StoreCustomerDto4 D
>D E
{		 
public

 
override

 
CustomerType

 $
CustomerType

% 1
=>

2 4
CustomerType

5 A
.

A B
Store

B G
;

G H
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
SalesPerson !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
List 
<  
StoreCustomerContact (
>( )
Contacts* 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
StoreCustomer +
,+ ,
StoreCustomerDto- =
>= >
(> ?
)? @
. 

ReverseMap 
( 
) 
; 
} 	
} 
} Ü
öC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Models\UpdateCustomer\StoreCustomerContact.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
.' (
Models( .
.. /
UpdateCustomer/ =
{ 
public 

class  
StoreCustomerContact %
:& '
IMapFrom( 0
<0 1#
StoreCustomerContactDto1 H
>H I
{ 
public		 
string		 
ContactType		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public

 
Person

 
ContactPerson

 #
{

$ %
get

& )
;

) *
set

+ .
;

. /
}

0 1
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
<  
StoreCustomerContact 2
,2 3#
StoreCustomerContactDto4 K
>K L
(L M
)M N
. 

ReverseMap 
( 
) 
; 
} 	
} 
} “
wC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Program.cs
	namespace

 	
AW


 
.

 
Services

 
.

 
Customer

 
.

 
REST

 #
.

# $
API

$ '
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
. $
ConfigureWebHostDefaults )
() *

webBuilder* 4
=>5 7
{ 

webBuilder 
. 

UseStartup )
<) *
Startup* 1
>1 2
(2 3
)3 4
;4 5
} 
) 
; 
} 
} ¯F
wC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.REST.API\Startup.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
REST #
.# $
API$ '
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public   
void   
ConfigureServices   %
(  % &
IServiceCollection  & 8
services  9 A
)  A B
{!! 	
services"" 
."" 
AddControllers"" #
(""# $
)""$ %
.## 
AddJsonOptions## 
(##  
options##  '
=>##( *
{$$ 
options%% 
.%% !
JsonSerializerOptions%% 1
.%%1 2 
PropertyNamingPolicy%%2 F
=%%G H
JsonNamingPolicy%%I Y
.%%Y Z
	CamelCase%%Z c
;%%c d
options&& 
.&& !
JsonSerializerOptions&& 1
.&&1 2

Converters&&2 <
.&&< =
Add&&= @
(&&@ A
new&&A D#
JsonStringEnumConverter&&E \
(&&\ ]
)&&] ^
)&&^ _
;&&_ `
options(( 
.(( !
JsonSerializerOptions(( 1
.((1 2

Converters((2 <
.((< =
Add((= @
(((@ A
new)) 
JsonConverters)) *
.))* +
CustomerConverter))+ <
<))< =
Models** "
.**" #
GetCustomers**# /
.**/ 0
Customer**0 8
,**8 9
Models++ "
.++" #
GetCustomers++# /
.++/ 0
StoreCustomer++0 =
,++= >
Models,, "
.,," #
GetCustomers,,# /
.,,/ 0
IndividualCustomer,,0 B
>,,B C
(,,C D
),,D E
)-- 
;-- 
options// 
.// !
JsonSerializerOptions// 1
.//1 2

Converters//2 <
.//< =
Add//= @
(//@ A
new00 
JsonConverters00 *
.00* +
CustomerConverter00+ <
<00< =
Models11 "
.11" #
GetCustomer11# .
.11. /
Customer11/ 7
,117 8
Models22 "
.22" #
GetCustomer22# .
.22. /
StoreCustomer22/ <
,22< =
Models33 "
.33" #
GetCustomer33# .
.33. /
IndividualCustomer33/ A
>33A B
(33B C
)33C D
)44 
;44 
options66 
.66 !
JsonSerializerOptions66 1
.661 2

Converters662 <
.66< =
Add66= @
(66@ A
new77 
JsonConverters77 *
.77* +
CustomerConverter77+ <
<77< =
Models88 "
.88" #
UpdateCustomer88# 1
.881 2
Customer882 :
,88: ;
Models99 "
.99" #
UpdateCustomer99# 1
.991 2
StoreCustomer992 ?
,99? @
Models:: "
.::" #
UpdateCustomer::# 1
.::1 2
IndividualCustomer::2 D
>::D E
(::E F
)::F G
);; 
;;; 
}<< 
)<< 
;<< 
services>> 
.>> 

AddMvcCore>> 
(>>  
)>>  !
.?? 
AddApiExplorer?? 
(??  
)??  !
;??! "
servicesAA 
.AA 
AddApiVersioningAA %
(AA% &
optionsAA& -
=>AA. 0
optionsAA1 8
.AA8 9
ReportApiVersionsAA9 J
=AAK L
trueAAM Q
)AAQ R
.BB #
AddVersionedApiExplorerBB (
(BB( )
optionsCC 
=>CC 
{DD 
optionsEE 
.EE  
GroupNameFormatEE  /
=EE0 1
$strEE2 :
;EE: ;
optionsFF 
.FF  %
SubstituteApiVersionInUrlFF  9
=FF: ;
trueFF< @
;FF@ A
}GG 
)HH 
;HH 
servicesJJ 
.JJ 
AddSwaggerGenJJ "
(JJ" #
cJJ# $
=>JJ% '
{KK 
cLL 
.LL 
CustomSchemaIdsLL !
(LL! "
xLL" #
=>LL$ &
xLL' (
.LL( )
FullNameLL) 1
)LL1 2
;LL2 3
cMM 
.MM ,
 DescribeAllParametersInCamelCaseMM 2
(MM2 3
)MM3 4
;MM4 5
cNN 
.NN 

SwaggerDocNN 
(NN 
$strNN !
,NN! "
newNN# &
OpenApiInfoNN' 2
{NN3 4
TitleNN5 :
=NN; <
$strNN= K
,NNK L
VersionNNM T
=NNU V
$strNNW [
}NN\ ]
)NN] ^
;NN^ _
}OO 
)OO 
;OO 
servicesQQ 
.QQ 
AddDbContextQQ !
<QQ! "
	AWContextQQ" +
>QQ+ ,
(QQ, -
cQQ- .
=>QQ/ 1
{RR 
cSS 
.SS 
LogToSS 
(SS 
ConsoleSS 
.SS  
	WriteLineSS  )
)SS) *
;SS* +
cTT 
.TT 
UseSqlServerTT 
(TT 
ConfigurationTT ,
.TT, -
GetConnectionStringTT- @
(TT@ A
$strTTA O
)TTO P
)TTP Q
;TTQ R
cUU 
.UU &
EnableSensitiveDataLoggingUU ,
(UU, -
)UU- .
;UU. /
}VV 
)VV 
;VV 
servicesWW 
.WW 
	AddScopedWW 
(WW 
typeofWW %
(WW% &
IRepositoryBaseWW& 5
<WW5 6
>WW6 7
)WW7 8
,WW8 9
typeofWW: @
(WW@ A
EfRepositoryWWA M
<WWM N
>WWN O
)WWO P
)WWP Q
;WWQ R
servicesXX 
.XX 
AddAutoMapperXX "
(XX" #
cXX# $
=>XX% '
cXX( )
.XX) * 
AddCollectionMappersXX* >
(XX> ?
)XX? @
,XX@ A
typeofXXB H
(XXH I
MappingProfileXXI W
)XXW X
.XXX Y
AssemblyXXY a
,XXa b
typeofXXc i
(XXi j
GetCustomersQueryXXj {
)XX{ |
.XX| }
Assembly	XX} Ö
)
XXÖ Ü
;
XXÜ á
servicesYY 
.YY 

AddMediatRYY 
(YY  
typeofYY  &
(YY& '
GetCustomersQueryYY' 8
)YY8 9
)YY9 :
;YY: ;
}ZZ 	
public]] 
void]] 
	Configure]] 
(]] 
IApplicationBuilder]] 1
app]]2 5
,]]5 6
IWebHostEnvironment]]7 J
env]]K N
,]]N O*
IApiVersionDescriptionProvider]]P n
provider]]o w
)]]w x
{^^ 	
var__ 
virtualPath__ 
=__ 
$str__ -
;__- .
app`` 
.`` 
Map`` 
(`` 
virtualPath`` 
,``  
builder``! (
=>``) +
{aa 
ifbb 
(bb 
envbb 
.bb 
IsDevelopmentbb %
(bb% &
)bb& '
)bb' (
{cc 
builderdd 
.dd %
UseDeveloperExceptionPagedd 5
(dd5 6
)dd6 7
;dd7 8
}ee 
buildergg 
.gg 
UseForwardedHeadersgg +
(gg+ ,
newgg, /#
ForwardedHeadersOptionsgg0 G
{hh 
ForwardedHeadersii $
=ii% &
ForwardedHeadersii' 7
.ii7 8
XForwardedForii8 E
|iiF G
ForwardedHeadersiiH X
.iiX Y
XForwardedProtoiiY h
}jj 
)jj 
;jj 
builderll 
.ll #
UseSwaggerDocumentationll /
(ll/ 0
virtualPathll0 ;
,ll; <
providerll= E
)llE F
;llF G
buildermm 
.mm 

UseRoutingmm "
(mm" #
)mm# $
;mm$ %
buildernn 
.nn 
UseAuthorizationnn (
(nn( )
)nn) *
;nn* +
builderoo 
.oo 
UseEndpointsoo $
(oo$ %
	endpointsoo% .
=>oo/ 1
{pp 
	endpointsqq 
.qq 
MapControllersqq ,
(qq, -
)qq- .
;qq. /
}rr 
)rr 
;rr 
}ss 
)ss 
;ss 
}tt 	
}uu 
}vv 