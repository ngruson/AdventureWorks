ì
üC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomerAddress\AddCustomerAddressCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomerAddress+ =
{ 
public 

class %
AddCustomerAddressCommand *
:+ ,
IRequest- 5
<5 6
Unit6 :
>: ;
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
CustomerAddressDto !
CustomerAddress" 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
}		 
}

 Ì
¶C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomerAddress\AddCustomerAddressCommandHandler.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomerAddress+ =
{ 
public 

class ,
 AddCustomerAddressCommandHandler 1
:2 3
IRequestHandler4 C
<C D%
AddCustomerAddressCommandD ]
,] ^
Unit_ c
>c d
{ 
private 
readonly 
ILogger  
<  !,
 AddCustomerAddressCommandHandler! A
>A B
loggerC I
;I J
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
public ,
 AddCustomerAddressCommandHandler /
(/ 0
ILogger 
< ,
 AddCustomerAddressCommandHandler 4
>4 5
logger6 <
,< =
IMapper 
mapper 
, 
IRepositoryBase 
< 
Domain "
." #
Customer# +
>+ ,
customerRepository- ?
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  
mapper  &
,& '
this( ,
., -
customerRepository- ?
)? @
=A B
(C D
loggerD J
,J K
mapperL R
,R S
customerRepositoryT f
)f g
;g h
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '%
AddCustomerAddressCommand' @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
customer 
= 
await  
customerRepository! 3
.3 4
GetBySpecAsync4 B
(B C
new $
GetCustomerSpecification ,
(, -
request- 4
.4 5
AccountNumber5 B
)B C
)   
;   
Guard"" 
."" 
Against"" 
."" 
Null"" 
("" 
customer"" '
,""' (
nameof"") /
(""/ 0
customer""0 8
)""8 9
)""9 :
;"": ;
logger$$ 
.$$ 
LogInformation$$ !
($$! "
$str$$" >
)$$> ?
;$$? @
var%% 
customerAddress%% 
=%%  !
mapper%%" (
.%%( )
Map%%) ,
<%%, -
CustomerAddress%%- <
>%%< =
(%%= >
request%%> E
.%%E F
CustomerAddress%%F U
)%%U V
;%%V W
customer&& 
.&& 
	Addresses&& 
.&& 
Add&& "
(&&" #
customerAddress&&# 2
)&&2 3
;&&3 4
logger(( 
.(( 
LogInformation(( !
(((! "
$str((" ?
)((? @
;((@ A
await)) 
customerRepository)) $
.))$ %
UpdateAsync))% 0
())0 1
customer))1 9
)))9 :
;)): ;
return++ 
Unit++ 
.++ 
Value++ 
;++ 
},, 	
}-- 
}.. §
êC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomerAddress\AddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomerAddress+ =
{ 
public 

class 

AddressDto 
: 
IMapFrom &
<& '
Domain' -
.- .
Address. 5
>5 6
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
}4 5
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
< 

AddressDto (
,( )
Domain* 0
.0 1
Address1 8
>8 9
(9 :
): ;
. 
	ForMember 
( 
m 
=> 
m  !
.! "
Id" $
,$ %
opt& )
=>* ,
opt- 0
.0 1
Ignore1 7
(7 8
)8 9
)9 :
;: ;
} 	
} 
} ‰
òC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomerAddress\CustomerAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomerAddress+ =
{ 
public 

class 
CustomerAddressDto #
:$ %
IMapFrom& .
<. /
Domain/ 5
.5 6
CustomerAddress6 E
>E F
{ 
public 

AddressDto 
Address !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
string		 
AddressType		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
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
CustomerAddressDto 0
,0 1
Domain2 8
.8 9
CustomerAddress9 H
>H I
(I J
)J K
. 
	ForMember 
( 
m 
=> 
m  !
.! "
Id" $
,$ %
opt& )
=>* ,
opt- 0
.0 1
Ignore1 7
(7 8
)8 9
)9 :
. 
	ForMember 
( 
m 
=> 
m  !
.! "
	AddressID" +
,+ ,
opt- 0
=>1 3
opt4 7
.7 8
Ignore8 >
(> ?
)? @
)@ A
;A B
} 	
} 
} œ
ëC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\AddCustomerCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class 
AddCustomerCommand #
:$ %
IRequest& .
<. /
CustomerDto/ :
>: ;
{ 
public 
CustomerDto 
Customer #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
}		 ≈
òC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\AddCustomerCommandHandler.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{		 
public

 

class

 %
AddCustomerCommandHandler

 *
:

+ ,
IRequestHandler

- <
<

< =
AddCustomerCommand

= O
,

O P
CustomerDto

Q \
>

\ ]
{ 
private 
readonly 
ILogger  
<  !%
AddCustomerCommandHandler! :
>: ;
logger< B
;B C
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
private 
readonly 
IMapper  
mapper! '
;' (
public %
AddCustomerCommandHandler (
(( )
ILogger 
< %
AddCustomerCommandHandler -
>- .
logger/ 5
,5 6
IRepositoryBase 
< 
Domain "
." #
Customer# +
>+ ,
customerRepository- ?
,? @
IMapper 
mapper 
) 
=> 
( 
this 
. 
logger 
, 
this "
." #
customerRepository# 5
,5 6
this7 ;
.; <
mapper< B
)B C
=D E
(F G
loggerG M
,M N
customerRepositoryO a
,a b
mapperc i
)i j
;j k
public 
async 
Task 
< 
CustomerDto %
>% &
Handle' -
(- .
AddCustomerCommand. @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" ?
)? @
;@ A
var 
customer 
= 
mapper !
.! "
Map" %
<% &
Domain& ,
., -
Customer- 5
>5 6
(6 7
request7 >
.> ?
Customer? G
)G H
;H I
logger 
. 
LogInformation !
(! "
$str" ?
)? @
;@ A
await 
customerRepository $
.$ %
AddAsync% -
(- .
customer. 6
)6 7
;7 8
logger   
.   
LogInformation   !
(  ! "
$str  " 6
)  6 7
;  7 8
return!! 
mapper!! 
.!! 
Map!! 
<!! 
CustomerDto!! )
>!!) *
(!!* +
customer!!+ 3
)!!3 4
;!!4 5
}"" 	
}## 
}$$ ù
öC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\AddCustomerCommandValidator.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public		 

class		 '
AddCustomerCommandValidator		 ,
:		- .
AbstractValidator		/ @
<		@ A
AddCustomerCommand		A S
>		S T
{

 
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
public '
AddCustomerCommandValidator *
(* +
IRepositoryBase+ :
<: ;
Domain; A
.A B
CustomerB J
>J K
customerRepositoryL ^
)^ _
{ 	
this 
. 
customerRepository #
=$ %
customerRepository& 8
;8 9
RuleFor 
( 
cmd 
=> 
cmd 
. 
Customer '
)' (
. 
NotNull 
( 
) 
. 
WithMessage &
(& '
$str' =
)= >
;> ?
RuleFor 
( 
cmd 
=> 
cmd 
. 
Customer '
.' (
AccountNumber( 5
)5 6
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( D
)D E
. 
MaximumLength 
( 
$num !
)! "
." #
WithMessage# .
(. /
$str/ ]
)] ^
. 
	MustAsync 
( 
	NotExists $
)$ %
.% &
WithMessage& 1
(1 2
$str2 Q
)Q R
. 
When 
( 
cmd 
=> 
cmd  
.  !
Customer! )
!=* ,
null- 1
)1 2
;2 3
} 	
private 
async 
Task 
< 
bool 
>  
	NotExists! *
(* +
string+ 1
accountNumber2 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 	
var 
customer 
= 
await  
customerRepository! 3
.3 4
GetBySpecAsync4 B
(B C
newC F$
GetCustomerSpecificationG _
(_ `
accountNumber` m
)m n
)n o
;o p
return 
customer 
== 
null #
;# $
} 	
}   
}!! Ω
âC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\AddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class 

AddressDto 
: 
IMapFrom &
<& '
Address' .
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
;4 5
} 	
} 
} ©

ëC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\CustomerAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class 
CustomerAddressDto #
:$ %
IMapFrom& .
<. /
Domain/ 5
.5 6
CustomerAddress6 E
>E F
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
public		 

AddressDto		 
Address		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
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
< 
Domain $
.$ %
CustomerAddress% 4
,4 5
CustomerAddressDto6 H
>H I
(I J
)J K
;K L
} 	
} 
} Ò
äC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\CustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

abstract 
class 
CustomerDto %
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
	Territory 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
List		 
<		 
CustomerAddressDto		 &
>		& '
	Addresses		( 1
{		2 3
get		4 7
;		7 8
set		9 <
;		< =
}		> ?
}

 
} £

îC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\IndividualCustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class !
IndividualCustomerDto &
:' (
CustomerDto) 4
,4 5
IMapFrom6 >
<> ?
Domain? E
.E F
IndividualCustomerF X
>X Y
{ 
public 
	PersonDto 
Person 
{  !
get" %
;% &
set' *
;* +
}, -
=. /
new0 3
	PersonDto4 =
(= >
)> ?
;? @
public

 
void

 
Mapping

 
(

 
Profile

 #
profile

$ +
)

+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
IndividualCustomer% 7
,7 8!
IndividualCustomerDto9 N
>N O
(O P
)P Q
;Q R
} 	
} 
} Å
àC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\PersonDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class 
	PersonDto 
: 
IMapFrom %
<% &
Domain& ,
., -
Person- 3
>3 4
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
< !
PersonEmailAddressDto )
>) *
EmailAddresses+ 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 
List 
< 
PersonPhoneDto "
>" #
PhoneNumbers$ 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
Person% +
,+ ,
	PersonDto- 6
>6 7
(7 8
)8 9
;9 :
} 	
} 
} ö	
îC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\PersonEmailAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class !
PersonEmailAddressDto &
:' (
IMapFrom) 1
<1 2
Domain2 8
.8 9
PersonEmailAddress9 K
>K L
{ 
public 
string 
EmailAddress "
{# $
get% (
;( )
set* -
;- .
}/ 0
public

 
void

 
Mapping

 
(

 
Profile

 #
profile

$ +
)

+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
PersonEmailAddress% 7
,7 8!
PersonEmailAddressDto9 N
>N O
(O P
)P Q
;Q R
} 	
} 
} ô

çC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\PersonPhoneDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class 
PersonPhoneDto 
:  !
IMapFrom" *
<* +
Domain+ 1
.1 2
PersonPhone2 =
>= >
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
}		. /
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
< 
Domain $
.$ %
PersonPhone% 0
,0 1
PersonPhoneDto2 @
>@ A
(A B
)B C
;C D
} 	
} 
} «

ñC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\StoreCustomerContactDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class #
StoreCustomerContactDto (
:) *
IMapFrom+ 3
<3 4
Domain4 :
.: ; 
StoreCustomerContact; O
>O P
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
public		 
	PersonDto		 
ContactPerson		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
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
< 
Domain $
.$ % 
StoreCustomerContact% 9
,9 :#
StoreCustomerContactDto; R
>R S
(S T
)T U
;U V
} 	
} 
} ù
èC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddCustomer\StoreCustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
AddCustomer+ 6
{ 
public 

class 
StoreCustomerDto !
:" #
CustomerDto$ /
,/ 0
IMapFrom1 9
<9 :
Domain: @
.@ A
StoreCustomerA N
>N O
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
< #
StoreCustomerContactDto +
>+ ,
Contacts- 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
StoreCustomer% 2
,2 3
StoreCustomerDto4 D
>D E
(E F
)F G
;G H
} 	
} 
} ¿
ΩC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddIndividualCustomerEmailAddress\AddIndividualCustomerEmailAddressCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +-
!AddIndividualCustomerEmailAddress+ L
{ 
public 

class 4
(AddIndividualCustomerEmailAddressCommand 9
:: ;
IRequest< D
<D E
UnitE I
>I J
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
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
 õ
ƒC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddIndividualCustomerEmailAddress\AddIndividualCustomerEmailAddressCommandHandler.cs
	namespace		 	
AW		
 
.		 
Services		 
.		 
Customer		 
.		 
Application		 *
.		* +-
!AddIndividualCustomerEmailAddress		+ L
{

 
public 

class ;
/AddIndividualCustomerEmailAddressCommandHandler @
:A B
IRequestHandlerC R
<R S4
(AddIndividualCustomerEmailAddressCommandS {
,{ |
Unit	} Å
>
Å Ç
{ 
private 
readonly 
ILogger  
<  !;
/AddIndividualCustomerEmailAddressCommandHandler! P
>P Q
loggerR X
;X Y
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
IndividualCustomer0 B
>B C(
individualCustomerRepositoryD `
;` a
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '4
(AddIndividualCustomerEmailAddressCommand' O
requestP W
,W X
CancellationTokenY j
cancellationTokenk |
)| }
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
individualCustomer "
=# $
await% *(
individualCustomerRepository+ G
.G H
GetBySpecAsyncH V
(V W
new .
"GetIndividualCustomerSpecification 6
(6 7
request7 >
.> ?
AccountNumber? L
)L M
) 
; 
logger 
. 
LogInformation !
(! "
$str" D
)D E
;E F
var 
emailAddress 
= 
new "
Domain# )
.) *
PersonEmailAddress* <
{= >
EmailAddress? K
=L M
requestN U
.U V
EmailAddressV b
}c d
;d e
individualCustomer 
. 
Person %
.% &
EmailAddresses& 4
.4 5
Add5 8
(8 9
emailAddress9 E
)E F
;F G
logger 
. 
LogInformation !
(! "
$str" ?
)? @
;@ A
await (
individualCustomerRepository .
.. /
UpdateAsync/ :
(: ;
individualCustomer; M
)M N
;N O
return!! 
Unit!! 
.!! 
Value!! 
;!! 
}"" 	
}## 
}$$ ü
ØC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddIndividualCustomerPhone\AddIndividualCustomerPhoneCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
AddIndividualCustomerPhone+ E
{ 
public 

class -
!AddIndividualCustomerPhoneCommand 2
:3 4
IRequest5 =
<= >
Unit> B
>B C
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
PhoneDto 
Phone 
{ 
get  #
;# $
set% (
;( )
}* +
}		 
}

 ò
∂C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddIndividualCustomerPhone\AddIndividualCustomerPhoneCommandHandler.cs
	namespace		 	
AW		
 
.		 
Services		 
.		 
Customer		 
.		 
Application		 *
.		* +&
AddIndividualCustomerPhone		+ E
{

 
public 

class 4
(AddIndividualCustomerPhoneCommandHandler 9
:: ;
IRequestHandler< K
<K L-
!AddIndividualCustomerPhoneCommandL m
,m n
Unito s
>s t
{ 
private 
readonly 
ILogger  
<  !4
(AddIndividualCustomerPhoneCommandHandler! I
>I J
loggerK Q
;Q R
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
IndividualCustomer0 B
>B C(
individualCustomerRepositoryD `
;` a
public 4
(AddIndividualCustomerPhoneCommandHandler 7
(7 8
ILogger 
< 4
(AddIndividualCustomerPhoneCommandHandler <
>< =
logger> D
,D E
IMapper 
mapper 
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  
mapper  &
)& '
=( )
(* +
logger+ 1
,1 2
mapper3 9
)9 :
;: ;
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '-
!AddIndividualCustomerPhoneCommand' H
requestI P
,P Q
CancellationTokenR c
cancellationTokend u
)u v
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
individualCustomer "
=# $
await% *(
individualCustomerRepository+ G
.G H
GetBySpecAsyncH V
(V W
new .
"GetIndividualCustomerSpecification 6
(6 7
request7 >
.> ?
AccountNumber? L
)L M
) 
; 
logger 
. 
LogInformation !
(! "
$str" <
)< =
;= >
var   
phone   
=   
mapper   
.   
Map   "
<  " #
Domain  # )
.  ) *
PersonPhone  * 5
>  5 6
(  6 7
request  7 >
.  > ?
Phone  ? D
)  D E
;  E F
individualCustomer!! 
.!! 
Person!! %
.!!% &
PhoneNumbers!!& 2
.!!2 3
Add!!3 6
(!!6 7
phone!!7 <
)!!< =
;!!= >
logger## 
.## 
LogInformation## !
(##! "
$str##" ?
)##? @
;##@ A
await$$ (
individualCustomerRepository$$ .
.$$. /
UpdateAsync$$/ :
($$: ;
individualCustomer$$; M
)$$M N
;$$N O
return&& 
Unit&& 
.&& 
Value&& 
;&& 
}'' 	
}(( 
})) û
ñC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddIndividualCustomerPhone\PhoneDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
AddIndividualCustomerPhone+ E
{ 
public 

class 
PhoneDto 
{ 
public 
string 
PhoneNumberType %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
PhoneNumber !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} ¨
©C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddStoreCustomerContact\AddStoreCustomerContactCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +#
AddStoreCustomerContact+ B
{ 
public 

class *
AddStoreCustomerContactCommand /
:0 1
IRequest2 :
<: ;
Unit; ?
>? @
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public #
StoreCustomerContactDto &
CustomerContact' 6
{7 8
get9 <
;< =
set> A
;A B
}C D
}		 
}

 ≥
∞C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddStoreCustomerContact\AddStoreCustomerContactCommandHandler.cs
	namespace		 	
AW		
 
.		 
Services		 
.		 
Customer		 
.		 
Application		 *
.		* +#
AddStoreCustomerContact		+ B
{

 
public 

class 1
%AddStoreCustomerContactCommandHandler 6
:7 8
IRequestHandler9 H
<H I*
AddStoreCustomerContactCommandI g
,g h
Uniti m
>m n
{ 
private 
readonly 
ILogger  
<  !1
%AddStoreCustomerContactCommandHandler! F
>F G
loggerH N
;N O
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
StoreCustomer0 =
>= >
storeRepository? N
;N O
public 1
%AddStoreCustomerContactCommandHandler 4
(4 5
ILogger 
< 1
%AddStoreCustomerContactCommandHandler 9
>9 :
logger; A
,A B
IMapper 
mapper 
, 
IRepositoryBase 
< 
Domain "
." #
StoreCustomer# 0
>0 1
storeRepository2 A
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  
mapper  &
,& '
this( ,
., -
storeRepository- <
)< =
=> ?
(@ A
loggerA G
,G H
mapperI O
,O P
storeRepositoryQ `
)` a
;a b
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '*
AddStoreCustomerContactCommand' E
requestF M
,M N
CancellationTokenO `
cancellationTokena r
)r s
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
store 
= 
await 
storeRepository -
.- .
GetBySpecAsync. <
(< =
new )
GetStoreCustomerSpecification 1
(1 2
request2 9
.9 :
AccountNumber: G
)G H
) 
; 
logger   
.   
LogInformation   !
(  ! "
$str  " ;
)  ; <
;  < =
var!! 
contact!! 
=!! 
mapper!!  
.!!  !
Map!!! $
<!!$ %
Domain!!% +
.!!+ , 
StoreCustomerContact!!, @
>!!@ A
(!!A B
request!!B I
.!!I J
CustomerContact!!J Y
)!!Y Z
;!!Z [
store"" 
."" 
Contacts"" 
."" 
Add"" 
("" 
contact"" &
)""& '
;""' (
logger$$ 
.$$ 
LogInformation$$ !
($$! "
$str$$" ?
)$$? @
;$$@ A
await%% 
storeRepository%% !
.%%! "
UpdateAsync%%" -
(%%- .
store%%. 3
)%%3 4
;%%4 5
return'' 
Unit'' 
.'' 
Value'' 
;'' 
}(( 	
})) 
}** †	
öC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddStoreCustomerContact\EmailAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +#
AddStoreCustomerContact+ B
{ 
public 

class 
EmailAddressDto  
:! "
IMapFrom# +
<+ ,
Domain, 2
.2 3
PersonEmailAddress3 E
>E F
{ 
public 
string 
EmailAddress "
{# $
get% (
;( )
set* -
;- .
}/ 0
public

 
void

 
Mapping

 
(

 
Profile

 #
profile

$ +
)

+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
PersonEmailAddress% 7
,7 8
EmailAddressDto9 H
>H I
(I J
)J K
;K L
} 	
} 
} ’
îC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddStoreCustomerContact\PersonDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +#
AddStoreCustomerContact+ B
{ 
public 

class 
	PersonDto 
: 
IMapFrom %
<% &
Domain& ,
., -
Person- 3
>3 4
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
< 
EmailAddressDto #
># $
EmailAddresses% 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
=B C
newD G
ListH L
<L M
EmailAddressDtoM \
>\ ]
(] ^
)^ _
;_ `
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
< 
Domain $
.$ %
Person% +
,+ ,
	PersonDto- 6
>6 7
(7 8
)8 9
;9 :
} 	
} 
} ﬂ

¢C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\AddStoreCustomerContact\StoreCustomerContactDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +#
AddStoreCustomerContact+ B
{ 
public 

class #
StoreCustomerContactDto (
:) *
IMapFrom+ 3
<3 4
Domain4 :
.: ; 
StoreCustomerContact; O
>O P
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
public		 
	PersonDto		 
ContactPerson		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
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
< 
Domain $
.$ % 
StoreCustomerContact% 9
,9 :#
StoreCustomerContactDto; R
>R S
(S T
)T U
;U V
} 	
} 
} á

ëC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Common\AutofacValidatorFactory.cs
	namespace 	
AW
 
. 
Core 
. 
Application 
. 
Autofac %
{ 
public 

class #
AutofacValidatorFactory (
:) * 
ValidatorFactoryBase+ ?
{ 
readonly

 
IIndex

 
<

 
Type

 
,

 

IValidator

 (
>

( )
_validators

* 5
;

5 6
public #
AutofacValidatorFactory &
(& '
IIndex' -
<- .
Type. 2
,2 3

IValidator4 >
>> ?

validators@ J
)J K
{ 	
_validators 
= 

validators $
;$ %
} 	
public 
override 

IValidator "
CreateInstance# 1
(1 2
Type2 6
validatorType7 D
)D E
{ 	
return 
_validators 
[ 
validatorType ,
], -
;- .
} 	
} 
} ∂
ûC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Common\Behaviours\RequestValidationBehavior.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
Common+ 1
.1 2

Behaviours2 <
{		 
public

 

class

 %
RequestValidationBehavior

 *
<

* +
TRequest

+ 3
,

3 4
	TResponse

5 >
>

> ?
:

@ A
IPipelineBehavior

B S
<

S T
TRequest

T \
,

\ ]
	TResponse

^ g
>

g h
where 
TRequest 
: 
IRequest !
<! "
	TResponse" +
>+ ,
{ 
private 
readonly 
IEnumerable $
<$ %

IValidator% /
</ 0
TRequest0 8
>8 9
>9 :
_validators; F
;F G
public %
RequestValidationBehavior (
(( )
IEnumerable) 4
<4 5

IValidator5 ?
<? @
TRequest@ H
>H I
>I J

validatorsK U
)U V
{ 	
_validators 
= 

validators $
;$ %
} 	
public 
Task 
< 
	TResponse 
> 
Handle %
(% &
TRequest& .
request/ 6
,6 7
CancellationToken8 I
cancellationTokenJ [
,[ \"
RequestHandlerDelegate] s
<s t
	TResponset }
>} ~
next	 É
)
É Ñ
{ 	
var 
context 
= 
new 
ValidationContext /
</ 0
TRequest0 8
>8 9
(9 :
request: A
)A B
;B C
var 
failures 
= 
_validators &
. 
Select 
( 
v 
=> 
v 
. 
Validate '
(' (
context( /
)/ 0
)0 1
. 

SelectMany 
( 
result "
=># %
result& ,
., -
Errors- 3
)3 4
. 
Where 
( 
f 
=> 
f 
!=  
null! %
)% &
. 
ToList 
( 
) 
; 
if 
( 
failures 
. 
Count 
!= !
$num" #
)# $
{ 
throw   
new   
ValidationException   -
(  - .
failures  . 6
)  6 7
;  7 8
}!! 
return## 
next## 
(## 
)## 
;## 
}$$ 	
}%% 
}&& ’

ÜC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Common\GuardClauses.cs
	namespace 	
Ardalis
 
. 
GuardClauses 
{ 
public 

static 
class 
GuardClauses $
{ 
public 
static 
T 
Null 
< 
T 
> 
(  
this  $
IGuardClause% 1
guardClause2 =
,= >
T? @
inputA F
,F G
stringH N
parameterNameO \
,\ ]
ILogger^ e
loggerf l
)l m
{		 	
if

 
(

 
input

 
is

 
null

 
)

 
{ 
var 
ex 
= 
new !
ArgumentNullException 2
(2 3
parameterName3 @
)@ A
;A B
logger 
. 
LogError 
(  
ex  "
," #
ex$ &
.& '
Message' .
). /
;/ 0
throw 
ex 
; 
} 
return 
input 
; 
} 	
} 
} à
ìC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteCustomerAddress\AddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +!
DeleteCustomerAddress+ @
{ 
public 

class 

AddressDto 
{ 
public 
string 
AddressLine1 "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
AddressLine2 "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 

PostalCode  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
City 
{ 
get  
;  !
set" %
;% &
}' (
public		 
string		 
StateProvince		 #
{		$ %
get		& )
;		) *
set		+ .
;		. /
}		0 1
public

 
string

 
Country

 
{

 
get

  #
;

# $
set

% (
;

( )
}

* +
} 
} §
õC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteCustomerAddress\CustomerAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +!
DeleteCustomerAddress+ @
{ 
public 

class 
CustomerAddressDto #
{ 
public 
string 
AddressType !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 

AddressDto 
Address !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
}		 ü
•C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteCustomerAddress\DeleteCustomerAddressCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +!
DeleteCustomerAddress+ @
{ 
public 

class (
DeleteCustomerAddressCommand -
:. /
IRequest0 8
<8 9
Unit9 =
>= >
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
CustomerAddressDto !
CustomerAddress" 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
}		 
}

 ’!
¨C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteCustomerAddress\DeleteCustomerAddressCommandHandler.cs
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
 
Application

 *
.

* +!
DeleteCustomerAddress

+ @
{ 
public 

class /
#DeleteCustomerAddressCommandHandler 4
:5 6
IRequestHandler7 F
<F G(
DeleteCustomerAddressCommandG c
,c d
Unite i
>i j
{ 
private 
readonly 
ILogger  
<  !/
#DeleteCustomerAddressCommandHandler! D
>D E
loggerF L
;L M
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
public /
#DeleteCustomerAddressCommandHandler 2
(2 3
ILogger 
< /
#DeleteCustomerAddressCommandHandler 7
>7 8
logger9 ?
,? @
IRepositoryBase 
< 
Domain "
." #
Customer# +
>+ ,
customerRepository- ?
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  
customerRepository  2
)2 3
=4 5
(6 7
logger7 =
,= >
customerRepository? Q
)Q R
;R S
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '(
DeleteCustomerAddressCommand' C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
customer 
= 
await  
customerRepository! 3
.3 4
GetBySpecAsync4 B
(B C
new $
GetCustomerSpecification ,
(, -
request- 4
.4 5
AccountNumber5 B
)B C
) 
; 
Guard 
. 
Against 
. 
Null 
( 
customer '
,' (
nameof) /
(/ 0
customer0 8
)8 9
,9 :
logger; A
)A B
;B C
logger   
.   
LogInformation   !
(  ! "
$str  " B
)  B C
;  C D
var!! 
customerAddress!! 
=!!  !
customer!!" *
.!!* +
	Addresses!!+ 4
.!!4 5
FirstOrDefault!!5 C
(!!C D
a"" 
=>"" 
a"" 
."" 
AddressType"" "
==""# %
request""& -
.""- .
CustomerAddress"". =
.""= >
AddressType""> I
)## 
;## 
Guard$$ 
.$$ 
Against$$ 
.$$ 
Null$$ 
($$ 
customerAddress$$ .
,$$. /
nameof$$0 6
($$6 7
customerAddress$$7 F
)$$F G
,$$G H
logger$$I O
)$$O P
;$$P Q
customer&& 
.&& 
	Addresses&& 
.&& 
Remove&& %
(&&% &
customerAddress&&& 5
)&&5 6
;&&6 7
logger(( 
.(( 
LogInformation(( !
(((! "
$str((" A
)((A B
;((B C
await)) 
customerRepository)) $
.))$ %
UpdateAsync))% 0
())0 1
customer))1 9
)))9 :
;)): ;
return** 
Unit** 
.** 
Value** 
;** 
}++ 	
},, 
}-- ‘
óC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteCustomer\DeleteCustomerCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
DeleteCustomer+ 9
{ 
public 

class !
DeleteCustomerCommand &
:' (
IRequest) 1
<1 2
Unit2 6
>6 7
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
}		 ›
ûC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteCustomer\DeleteCustomerCommandHandler.cs
	namespace		 	
AW		
 
.		 
Services		 
.		 
Customer		 
.		 
Application		 *
.		* +
DeleteCustomer		+ 9
{

 
public 

class (
DeleteCustomerCommandHandler -
:. /
IRequestHandler0 ?
<? @!
DeleteCustomerCommand@ U
,U V
UnitW [
>[ \
{ 
private 
readonly 
ILogger  
<  !(
DeleteCustomerCommandHandler! =
>= >
logger? E
;E F
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '!
DeleteCustomerCommand' <
request= D
,D E
CancellationTokenF W
cancellationTokenX i
)i j
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
spec 
= 
new $
GetCustomerSpecification 3
(3 4
request4 ;
.; <
AccountNumber< I
)I J
;J K
var 
customer 
= 
await  
customerRepository! 3
.3 4
GetBySpecAsync4 B
(B C
specC G
)G H
;H I
Guard 
. 
Against 
. 
Null 
( 
customer '
,' (
nameof) /
(/ 0
customer0 8
)8 9
,9 :
logger; A
)A B
;B C
logger 
. 
LogInformation !
(! "
$str" C
)C D
;D E
await 
customerRepository $
.$ %
DeleteAsync% 0
(0 1
customer1 9
)9 :
;: ;
return 
Unit 
. 
Value 
; 
} 	
} 
} Ã
√C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteIndividualCustomerEmailAddress\DeleteIndividualCustomerEmailAddressCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +0
$DeleteIndividualCustomerEmailAddress+ O
{ 
public 

class 7
+DeleteIndividualCustomerEmailAddressCommand <
:= >
IRequest? G
<G H
UnitH L
>L M
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
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
 î$
 C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteIndividualCustomerEmailAddress\DeleteIndividualCustomerEmailAddressCommandHandler.cs
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
 
Application

 *
.

* +0
$DeleteIndividualCustomerEmailAddress

+ O
{ 
public 

class >
2DeleteIndividualCustomerEmailAddressCommandHandler C
:D E
IRequestHandlerF U
<U V8
+DeleteIndividualCustomerEmailAddressCommand	V Å
,
Å Ç
Unit
É á
>
á à
{ 
private 
readonly 
ILogger  
<  !>
2DeleteIndividualCustomerEmailAddressCommandHandler! S
>S T
loggerU [
;[ \
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
IndividualCustomer0 B
>B C(
individualCustomerRepositoryD `
;` a
public >
2DeleteIndividualCustomerEmailAddressCommandHandler A
(A B
ILogger 
< >
2DeleteIndividualCustomerEmailAddressCommandHandler F
>F G
loggerH N
,N O
IRepositoryBase 
< 
Domain "
." #
IndividualCustomer# 5
>5 6(
individualCustomerRepository7 S
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  (
individualCustomerRepository  <
)< =
=> ?
(@ A
loggerA G
,G H(
individualCustomerRepositoryI e
)e f
;f g
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '7
+DeleteIndividualCustomerEmailAddressCommand' R
requestS Z
,Z [
CancellationToken\ m
cancellationTokenn 
)	 Ä
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
individualCustomer "
=# $
await% *(
individualCustomerRepository+ G
.G H
GetBySpecAsyncH V
(V W
new .
"GetIndividualCustomerSpecification 6
(6 7
request7 >
.> ?
AccountNumber? L
)L M
) 
; 
Guard 
. 
Against 
. 
Null 
( 
individualCustomer 1
,1 2
nameof3 9
(9 :
individualCustomer: L
)L M
,M N
loggerO U
)U V
;V W
logger   
.   
LogInformation   !
(  ! "
$str  " B
)  B C
;  C D
var!! 
emailAddress!! 
=!! 
individualCustomer!! 1
.!!1 2
Person!!2 8
.!!8 9
EmailAddresses!!9 G
.!!G H
FirstOrDefault!!H V
(!!V W
e"" 
=>"" 
e"" 
."" 
EmailAddress"" #
==""$ &
request""' .
."". /
EmailAddress""/ ;
)## 
;## 
Guard$$ 
.$$ 
Against$$ 
.$$ 
Null$$ 
($$ 
emailAddress$$ +
,$$+ ,
nameof$$- 3
($$3 4
emailAddress$$4 @
)$$@ A
,$$A B
logger$$C I
)$$I J
;$$J K
individualCustomer&& 
.&& 
Person&& %
.&&% &
EmailAddresses&&& 4
.&&4 5
Remove&&5 ;
(&&; <
emailAddress&&< H
)&&H I
;&&I J
logger(( 
.(( 
LogInformation(( !
(((! "
$str((" A
)((A B
;((B C
await)) (
individualCustomerRepository)) .
.)). /
UpdateAsync))/ :
()): ;
individualCustomer)); M
)))M N
;))N O
return** 
Unit** 
.** 
Value** 
;** 
}++ 	
},, 
}-- ´
µC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteIndividualCustomerPhone\DeleteIndividualCustomerPhoneCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +)
DeleteIndividualCustomerPhone+ H
{ 
public 

class 0
$DeleteIndividualCustomerPhoneCommand 5
:6 7
IRequest8 @
<@ A
UnitA E
>E F
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
PhoneDto 
Phone 
{ 
get  #
;# $
set% (
;( )
}* +
}		 
}

 ã%
ºC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteIndividualCustomerPhone\DeleteIndividualCustomerPhoneCommandHandler.cs
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
 
Application

 *
.

* +)
DeleteIndividualCustomerPhone

+ H
{ 
public 

class 7
+DeleteIndividualCustomerPhoneCommandHandler <
:= >
IRequestHandler? N
<N O0
$DeleteIndividualCustomerPhoneCommandO s
,s t
Unitu y
>y z
{ 
private 
readonly 
ILogger  
<  !7
+DeleteIndividualCustomerPhoneCommandHandler! L
>L M
loggerN T
;T U
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
IndividualCustomer0 B
>B C(
individualCustomerRepositoryD `
;` a
public 7
+DeleteIndividualCustomerPhoneCommandHandler :
(: ;
ILogger 
< 7
+DeleteIndividualCustomerPhoneCommandHandler ?
>? @
loggerA G
,G H
IRepositoryBase 
< 
Domain "
." #
IndividualCustomer# 5
>5 6(
individualCustomerRepository7 S
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  (
individualCustomerRepository  <
)< =
=> ?
(@ A
loggerA G
,G H(
individualCustomerRepositoryI e
)e f
;f g
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '0
$DeleteIndividualCustomerPhoneCommand' K
requestL S
,S T
CancellationTokenU f
cancellationTokeng x
)x y
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
individualCustomer "
=# $
await% *(
individualCustomerRepository+ G
.G H
GetBySpecAsyncH V
(V W
new .
"GetIndividualCustomerSpecification 6
(6 7
request7 >
.> ?
AccountNumber? L
)L M
) 
; 
Guard 
. 
Against 
. 
Null 
( 
individualCustomer 1
,1 2
nameof3 9
(9 :
individualCustomer: L
)L M
,M N
loggerO U
)U V
;V W
logger   
.   
LogInformation   !
(  ! "
$str  " @
)  @ A
;  A B
var!! 
phone!! 
=!! 
individualCustomer!! *
.!!* +
Person!!+ 1
.!!1 2
PhoneNumbers!!2 >
.!!> ?
FirstOrDefault!!? M
(!!M N
e"" 
=>"" 
e"" 
."" 
PhoneNumberType"" &
==""' )
request""* 1
.""1 2
Phone""2 7
.""7 8
PhoneNumberType""8 G
&&""H J
e## 
.## 
PhoneNumber## !
==##" $
request##% ,
.##, -
Phone##- 2
.##2 3
PhoneNumber##3 >
)$$ 
;$$ 
Guard%% 
.%% 
Against%% 
.%% 
Null%% 
(%% 
phone%% $
,%%$ %
nameof%%& ,
(%%, -
phone%%- 2
)%%2 3
,%%3 4
logger%%5 ;
)%%; <
;%%< =
individualCustomer'' 
.'' 
Person'' %
.''% &
PhoneNumbers''& 2
.''2 3
Remove''3 9
(''9 :
phone'': ?
)''? @
;''@ A
logger)) 
.)) 
LogInformation)) !
())! "
$str))" A
)))A B
;))B C
await** (
individualCustomerRepository** .
.**. /
UpdateAsync**/ :
(**: ;
individualCustomer**; M
)**M N
;**N O
return++ 
Unit++ 
.++ 
Value++ 
;++ 
},, 	
}-- 
}.. §
ôC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteIndividualCustomerPhone\PhoneDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +)
DeleteIndividualCustomerPhone+ H
{ 
public 

class 
PhoneDto 
{ 
public 
string 
PhoneNumberType %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
PhoneNumber !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} ∏
ØC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteStoreCustomerContact\DeleteStoreCustomerContactCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
DeleteStoreCustomerContact+ E
{ 
public 

class -
!DeleteStoreCustomerContactCommand 2
:3 4
IRequest5 =
<= >
Unit> B
>B C
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public #
StoreCustomerContactDto &
CustomerContact' 6
{7 8
get9 <
;< =
set> A
;A B
}C D
}		 
}

 €(
∂C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteStoreCustomerContact\DeleteStoreCustomerContactCommandHandler.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
DeleteStoreCustomerContact+ E
{ 
public 

class 4
(DeleteStoreCustomerContactCommandHandler 9
:: ;
IRequestHandler< K
<K L-
!DeleteStoreCustomerContactCommandL m
,m n
Unito s
>s t
{ 
private 
readonly 
ILogger  
<  !4
(DeleteStoreCustomerContactCommandHandler! I
>I J
loggerK Q
;Q R
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
StoreCustomer0 =
>= >#
storeCustomerRepository? V
;V W
public 4
(DeleteStoreCustomerContactCommandHandler 7
(7 8
ILogger 
< 4
(DeleteStoreCustomerContactCommandHandler <
>< =
logger> D
,D E
IRepositoryBase 
< 
Domain "
." #
StoreCustomer# 0
>0 1#
storeCustomerRepository2 I
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  #
storeCustomerRepository  7
)7 8
=9 :
(; <
logger< B
,B C#
storeCustomerRepositoryD [
)[ \
;\ ]
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '-
!DeleteStoreCustomerContactCommand' H
requestI P
,P Q
CancellationTokenR c
cancellationTokend u
)u v
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
storeCustomer 
= 
await  %#
storeCustomerRepository& =
.= >
GetBySpecAsync> L
(L M
new )
GetStoreCustomerSpecification 1
(1 2
request2 9
.9 :
AccountNumber: G
)G H
) 
; 
Guard 
. 
Against 
. 
Null 
( 
storeCustomer ,
,, -
nameof. 4
(4 5
storeCustomer5 B
)B C
,C D
loggerE K
)K L
;L M
logger!! 
.!! 
LogInformation!! !
(!!! "
$str!!" ?
)!!? @
;!!@ A
var"" 
contact"" 
="" 
storeCustomer"" '
.""' (
Contacts""( 0
.""0 1
FirstOrDefault""1 ?
(""? @
c## 
=>## 
c## 
.## 
ContactType## "
==### %
request##& -
.##- .
CustomerContact##. =
.##= >
ContactType##> I
&&##J L
c$$ 
.$$ 
ContactPerson$$ #
.$$# $
	FirstName$$$ -
==$$. 0
request$$1 8
.$$8 9
CustomerContact$$9 H
.$$H I
ContactPerson$$I V
.$$V W
	FirstName$$W `
&&$$a c
c%% 
.%% 
ContactPerson%% #
.%%# $

MiddleName%%$ .
==%%/ 1
request%%2 9
.%%9 :
CustomerContact%%: I
.%%I J
ContactPerson%%J W
.%%W X

MiddleName%%X b
&&%%c e
c&& 
.&& 
ContactPerson&& #
.&&# $
LastName&&$ ,
==&&- /
request&&0 7
.&&7 8
CustomerContact&&8 G
.&&G H
ContactPerson&&H U
.&&U V
LastName&&V ^
)'' 
;'' 
Guard(( 
.(( 
Against(( 
.(( 
Null(( 
((( 
contact(( &
,((& '
nameof((( .
(((. /
contact((/ 6
)((6 7
,((7 8
logger((9 ?
)((? @
;((@ A
storeCustomer** 
.** 
Contacts** "
.**" #
Remove**# )
(**) *
contact*** 1
)**1 2
;**2 3
logger,, 
.,, 
LogInformation,, !
(,,! "
$str,," A
),,A B
;,,B C
await-- #
storeCustomerRepository-- )
.--) *
UpdateAsync--* 5
(--5 6
storeCustomer--6 C
)--C D
;--D E
return.. 
Unit.. 
... 
Value.. 
;.. 
}// 	
}00 
}11 Ê
óC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteStoreCustomerContact\PersonDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
DeleteStoreCustomerContact+ E
{ 
public 

class 
	PersonDto 
: 
IMapFrom %
<% &
Domain& ,
., -
Person- 3
>3 4
{ 
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public		 
string		 
	FirstName		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
string

 

MiddleName
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
string 
LastName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Suffix 
{ 
get "
;" #
set$ '
;' (
}) *
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
Person% +
,+ ,
	PersonDto- 6
>6 7
(7 8
)8 9
;9 :
} 	
} 
} Â

•C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\DeleteStoreCustomerContact\StoreCustomerContactDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
DeleteStoreCustomerContact+ E
{ 
public 

class #
StoreCustomerContactDto (
:) *
IMapFrom+ 3
<3 4
Domain4 :
.: ; 
StoreCustomerContact; O
>O P
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
public		 
	PersonDto		 
ContactPerson		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
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
< 
Domain $
.$ % 
StoreCustomerContact% 9
,9 :#
StoreCustomerContactDto; R
>R S
(S T
)T U
;U V
} 	
} 
} Å
äC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\AddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class 

AddressDto 
: 
IMapFrom &
<& '
Address' .
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
. 
	ForMember 
( 
m 
=> 
m  !
.! "
StateProvinceCode" 3
,3 4
opt5 8
=>9 ;
opt< ?
.? @
MapFrom@ G
(G H
srcH K
=>L N
srcO R
.R S
StateProvinceCodeS d
.d e
Trime i
(i j
)j k
)k l
)l m
;m n
} 	
} 
} Â	
íC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\CustomerAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class 
CustomerAddressDto #
:$ %
IMapFrom& .
<. /
CustomerAddress/ >
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
 

AddressDto

 
Address

 !
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
< 
CustomerAddress -
,- .
CustomerAddressDto/ A
>A B
(B C
)C D
;D E
} 	
} 
} è	
ãC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\CustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

abstract 
class 
CustomerDto %
{ 
public 
abstract 
CustomerType $
CustomerType% 1
{2 3
get4 7
;7 8
}9 :
public		 
string		 
AccountNumber		 #
{		$ %
get		& )
;		) *
set		+ .
;		. /
}		0 1
public

 
string

 
	Territory
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
public 
List 
< 
CustomerAddressDto &
>& '
	Addresses( 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
} 
} Å
åC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\CustomerType.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
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
} ø
èC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\GetCustomersDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class 
GetCustomersDto  
{ 
public 
List 
< 
CustomerDto 
>  
	Customers! *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 
int 
TotalCustomers !
{" #
get$ '
;' (
set) ,
;, -
}. /
}		 
}

 ÿ

ëC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\GetCustomersQuery.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class 
GetCustomersQuery "
:# $
IRequest% -
<- .
GetCustomersDto. =
>= >
{ 
public 
int 
	PageIndex 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
PageSize 
{ 
get !
;! "
set# &
;& '
}( )
public		 
CustomerType		 
?		 
CustomerType		 )
{		* +
get		, /
;		/ 0
set		1 4
;		4 5
}		6 7
public

 
string

 
	Territory
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
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} ˚%
òC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\GetCustomersQueryHandler.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class $
GetCustomersQueryHandler )
:* +
IRequestHandler, ;
<; <
GetCustomersQuery< M
,M N
GetCustomersDtoO ^
>^ _
{ 
private 
readonly 
ILogger  
<  !$
GetCustomersQueryHandler! 9
>9 :
logger; A
;A B
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9

repository: D
;D E
public $
GetCustomersQueryHandler '
(' (
ILogger 
< $
GetCustomersQueryHandler ,
>, -
logger. 4
,4 5
IMapper 
mapper 
, 
IRepositoryBase 
< 
Domain "
." #
Customer# +
>+ ,

repository- 7
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  
mapper  &
,& '
this( ,
., -

repository- 7
)7 8
=9 :
(; <
logger< B
,B C
mapperD J
,J K

repositoryL V
)V W
;W X
public 
async 
Task 
< 
GetCustomersDto )
>) *
Handle+ 1
(1 2
GetCustomersQuery2 C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" C
)C D
;D E
var 
spec 
= 
new .
"GetCustomersPaginatedSpecification =
(= >
request 
. 
	PageIndex !
,! "
request   
.   
PageSize    
,    !
mapper!! 
.!! 
Map!! 
<!! 
Domain!! !
.!!! "
CustomerType!!" .
?!!. /
>!!/ 0
(!!0 1
request!!1 8
.!!8 9
CustomerType!!9 E
)!!E F
,!!F G
request"" 
."" 
	Territory"" !
,""! "
request## 
.## 
AccountNumber## %
)$$ 
;$$ 
var%% 
	countSpec%% 
=%% 
new%% '
CountCustomersSpecification%%  ;
(%%; <
mapper&& 
.&& 
Map&& 
<&& 
Domain&& !
.&&! "
CustomerType&&" .
?&&. /
>&&/ 0
(&&0 1
request&&1 8
.&&8 9
CustomerType&&9 E
)&&E F
,&&F G
request'' 
.'' 
	Territory'' !
,''! "
request(( 
.(( 
AccountNumber(( %
))) 
;)) 
var++ 
	customers++ 
=++ 
await++ !

repository++" ,
.++, -
	ListAsync++- 6
(++6 7
spec++7 ;
)++; <
;++< =
Guard,, 
.,, 
Against,, 
.,, 
Null,, 
(,, 
	customers,, (
,,,( )
nameof,,* 0
(,,0 1
	customers,,1 :
),,: ;
),,; <
;,,< =
logger.. 
... 
LogInformation.. !
(..! "
$str.." 7
)..7 8
;..8 9
return// 
new// 
GetCustomersDto// &
{00 
	Customers11 
=11 
mapper11 "
.11" #
Map11# &
<11& '
List11' +
<11+ ,
CustomerDto11, 7
>117 8
>118 9
(119 :
	customers11: C
)11C D
,11D E
TotalCustomers22 
=22  
await22! &

repository22' 1
.221 2

CountAsync222 <
(22< =
	countSpec22= F
)22F G
}33 
;33 
}44 	
}55 
}66 ∞

öC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\GetCustomersQueryValidator.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class &
GetCustomersQueryValidator +
:, -
AbstractValidator. ?
<? @
GetCustomersQuery@ Q
>Q R
{ 
public &
GetCustomersQueryValidator )
() *
)* +
{		 	
RuleFor

 
(

 
cmd

 
=>

 
cmd

 
.

 
	PageIndex

 (
)

( )
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( @
)@ A
;A B
RuleFor 
( 
cmd 
=> 
cmd 
. 
PageSize '
)' (
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( ?
)? @
;@ A
} 	
} 
} ‹

ïC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\IndividualCustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class !
IndividualCustomerDto &
:' (
CustomerDto) 4
,4 5
IMapFrom6 >
<> ?
IndividualCustomer? Q
>Q R
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
 
	PersonDto

 
Person

 
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
< 
Domain $
.$ %
IndividualCustomer% 7
,7 8!
IndividualCustomerDto9 N
>N O
(O P
)P Q
;Q R
} 	
} 
} É
âC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\PersonDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class 
	PersonDto 
: 
IMapFrom %
<% &
Domain& ,
., -
Person- 3
>3 4
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
< !
PersonEmailAddressDto )
>) *
EmailAddresses+ 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 
List 
< 
PersonPhoneDto "
>" #
PhoneNumbers$ 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
Person% +
,+ ,
	PersonDto- 6
>6 7
(7 8
)8 9
;9 :
} 	
} 
} ú	
ïC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\PersonEmailAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class !
PersonEmailAddressDto &
:' (
IMapFrom) 1
<1 2
Domain2 8
.8 9
PersonEmailAddress9 K
>K L
{ 
public 
string 
EmailAddress "
{# $
get% (
;( )
set* -
;- .
}/ 0
public

 
void

 
Mapping

 
(

 
Profile

 #
profile

$ +
)

+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
PersonEmailAddress% 7
,7 8!
PersonEmailAddressDto9 N
>N O
(O P
)P Q
;Q R
} 	
} 
} õ

éC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\PersonPhoneDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class 
PersonPhoneDto 
:  !
IMapFrom" *
<* +
Domain+ 1
.1 2
PersonPhone2 =
>= >
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
}		. /
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
< 
Domain $
.$ %
PersonPhone% 0
,0 1
PersonPhoneDto2 @
>@ A
(A B
)B C
;C D
} 	
} 
} …

óC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\StoreCustomerContactDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class #
StoreCustomerContactDto (
:) *
IMapFrom+ 3
<3 4
Domain4 :
.: ; 
StoreCustomerContact; O
>O P
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
public		 
	PersonDto		 
ContactPerson		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
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
< 
Domain $
.$ % 
StoreCustomerContact% 9
,9 :#
StoreCustomerContactDto; R
>R S
(S T
)T U
;U V
} 	
} 
} ¥
êC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomers\StoreCustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomers+ 7
{ 
public 

class 
StoreCustomerDto !
:" #
CustomerDto$ /
,/ 0
IMapFrom1 9
<9 :
StoreCustomer: G
>G H
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
< #
StoreCustomerContactDto +
>+ ,
Contacts- 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
=D E
newF I
ListJ N
<N O#
StoreCustomerContactDtoO f
>f g
(g h
)h i
;i j
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
;@ A
} 	
} 
} ≈
âC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\AddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class 

AddressDto 
: 
IMapFrom &
<& '
Domain' -
.- .
Address. 5
>5 6
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
}4 5
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
< 
Domain $
.$ %
Address% ,
,, -

AddressDto. 8
>8 9
(9 :
): ;
. 
	ForMember 
( 
m 
=> 
m  !
.! "
StateProvinceCode" 3
,3 4
opt5 8
=>9 ;
opt< ?
.? @
MapFrom@ G
(G H
srcH K
=>L N
srcO R
.R S
StateProvinceCodeS d
.d e
Trime i
(i j
)j k
)k l
)l m
;m n
} 	
} 
} ©

ëC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\CustomerAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class 
CustomerAddressDto #
:$ %
IMapFrom& .
<. /
Domain/ 5
.5 6
CustomerAddress6 E
>E F
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
public		 

AddressDto		 
Address		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
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
< 
Domain $
.$ %
CustomerAddress% 4
,4 5
CustomerAddressDto6 H
>H I
(I J
)J K
;K L
} 	
} 
} „

äC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\CustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

abstract 
class 
CustomerDto %
{ 
public 
abstract 
CustomerType $
CustomerType% 1
{2 3
get4 7
;7 8
}9 :
public		 
string		 
AccountNumber		 #
{		$ %
get		& )
;		) *
set		+ .
;		. /
}		0 1
public

 
string

 
	Territory
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
public 
List 
< 
CustomerAddressDto &
>& '
	Addresses( 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 
List 
< 
SalesOrderDto !
>! "
SalesOrders# .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
} 
} À
èC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\GetCustomerQuery.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class 
GetCustomerQuery !
:" #
IRequest$ ,
<, -
CustomerDto- 8
>8 9
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
}		 Ë
ñC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\GetCustomerQueryHandler.cs
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
 
Application

 *
.

* +
GetCustomer

+ 6
{ 
public 

class #
GetCustomerQueryHandler (
:) *
IRequestHandler+ :
<: ;
GetCustomerQuery; K
,K L
CustomerDtoM X
>X Y
{ 
private 
readonly 
ILogger  
<  !#
GetCustomerQueryHandler! 8
>8 9
logger: @
;@ A
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9

repository: D
;D E
public #
GetCustomerQueryHandler &
(& '
ILogger 
< #
GetCustomerQueryHandler +
>+ ,
logger- 3
,3 4
IMapper 
mapper 
, 
IRepositoryBase 
< 
Domain "
." #
Customer# +
>+ ,

repository- 7
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  
mapper  &
,& '
this( ,
., -

repository- 7
)7 8
=9 :
(; <
logger< B
,B C
mapperD J
,J K

repositoryL V
)V W
;W X
public 
async 
Task 
< 
CustomerDto %
>% &
Handle' -
(- .
GetCustomerQuery. >
request? F
,F G
CancellationTokenH Y
cancellationTokenZ k
)k l
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
spec 
= 
new $
GetCustomerSpecification 3
(3 4
request 
. 
AccountNumber %
) 
; 
var!! 
customer!! 
=!! 
await!!  

repository!!! +
.!!+ ,
GetBySpecAsync!!, :
(!!: ;
spec!!; ?
)!!? @
;!!@ A
Guard"" 
."" 
Against"" 
."" 
Null"" 
("" 
customer"" '
,""' (
nameof"") /
(""/ 0
customer""0 8
)""8 9
)""9 :
;"": ;
logger$$ 
.$$ 
LogInformation$$ !
($$! "
$str$$" 6
)$$6 7
;$$7 8
return%% 
mapper%% 
.%% 
Map%% 
<%% 
CustomerDto%% )
>%%) *
(%%* +
customer%%+ 3
)%%3 4
;%%4 5
}&& 	
}'' 
}(( ∑

îC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\IndividualCustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class !
IndividualCustomerDto &
:' (
CustomerDto) 4
,4 5
IMapFrom6 >
<> ?
IndividualCustomer? Q
>Q R
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
 
	PersonDto

 
Person

 
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
;J K
} 	
} 
} Å
àC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\PersonDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class 
	PersonDto 
: 
IMapFrom %
<% &
Domain& ,
., -
Person- 3
>3 4
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
< !
PersonEmailAddressDto )
>) *
EmailAddresses+ 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 
List 
< 
PersonPhoneDto "
>" #
PhoneNumbers$ 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
Person% +
,+ ,
	PersonDto- 6
>6 7
(7 8
)8 9
;9 :
} 	
} 
} ö	
îC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\PersonEmailAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class !
PersonEmailAddressDto &
:' (
IMapFrom) 1
<1 2
Domain2 8
.8 9
PersonEmailAddress9 K
>K L
{ 
public 
string 
EmailAddress "
{# $
get% (
;( )
set* -
;- .
}/ 0
public

 
void

 
Mapping

 
(

 
Profile

 #
profile

$ +
)

+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
PersonEmailAddress% 7
,7 8!
PersonEmailAddressDto9 N
>N O
(O P
)P Q
;Q R
} 	
} 
} ô

çC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\PersonPhoneDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class 
PersonPhoneDto 
:  !
IMapFrom" *
<* +
Domain+ 1
.1 2
PersonPhone2 =
>= >
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
}		. /
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
< 
Domain $
.$ %
PersonPhone% 0
,0 1
PersonPhoneDto2 @
>@ A
(A B
)B C
;C D
} 	
} 
} â
åC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\SalesOrderDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class 
SalesOrderDto 
:  
IMapFrom! )
<) *
Domain* 0
.0 1

SalesOrder1 ;
>; <
{ 
public		 
DateTime		 
	OrderDate		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public 
DateTime 
DueDate 
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 
? 
ShipDate !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
SalesOrderStatus 
Status  &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
bool 
OnlineOrderFlag #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
SalesOrderNumber &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
PurchaseOrderNumber )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 
TotalDue 
{  !
get" %
;% &
set' *
;* +
}, -
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %

SalesOrder% /
,/ 0
SalesOrderDto1 >
>> ?
(? @
)@ A
;A B
} 	
} 
}   á
èC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\SalesOrderStatus.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
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
} «

ñC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\StoreCustomerContactDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class #
StoreCustomerContactDto (
:) *
IMapFrom+ 3
<3 4
Domain4 :
.: ; 
StoreCustomerContact; O
>O P
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
public		 
	PersonDto		 
ContactPerson		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
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
< 
Domain $
.$ % 
StoreCustomerContact% 9
,9 :#
StoreCustomerContactDto; R
>R S
(S T
)T U
;U V
} 	
} 
} ≤
èC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\GetCustomer\StoreCustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
GetCustomer+ 6
{ 
public 

class 
StoreCustomerDto !
:" #
CustomerDto$ /
,/ 0
IMapFrom1 9
<9 :
StoreCustomer: G
>G H
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
< #
StoreCustomerContactDto +
>+ ,
Contacts- 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
=D E
newF I
ListJ N
<N O#
StoreCustomerContactDtoO f
>f g
(g h
)h i
;i j
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
;@ A
} 	
} 
} °
ùC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Specifications\CountCustomersSpecification.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
Specifications+ 9
{ 
public 

class '
CountCustomersSpecification ,
:- .
Specification/ <
<< =
Domain= C
.C D
CustomerD L
>L M
{ 
public		 '
CountCustomersSpecification		 *
(		* +
CustomerType		+ 7
?		7 8
customerType		9 E
,		E F
string		G M
	territory		N W
,		W X
string		Y _
accountNumber		` m
)		m n
:		o p
base		q u
(		u v
)		v w
{

 	
Query 
. 
Where 
( 
c 
=> 
( 
string 
. 
IsNullOrEmpty )
() *
	territory* 3
)3 4
||5 7
c8 9
.9 :
	Territory: C
==D F
	territoryG P
)P Q
&&R T
( 
! 
customerType "
." #
HasValue# +
||, .
(/ 0
customerType0 <
=== ?
CustomerType@ L
.L M

IndividualM W
?X Y
c 
is 
IndividualCustomer /
:0 1
c2 3
is4 6
StoreCustomer7 D
)D E
) 
&& 
( 
string 
. 
IsNullOrEmpty )
() *
accountNumber* 7
)7 8
||9 ;
c< =
.= >
AccountNumber> K
==L N
accountNumberO \
)\ ]
) 
; 
} 	
} 
} í
ôC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Specifications\GetAddressSpecification.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
Specifications+ 9
{ 
public 

class #
GetAddressSpecification (
:) *
Specification+ 8
<8 9
Domain9 ?
.? @
Address@ G
>G H
{ 
public #
GetAddressSpecification &
(& '
string 
addressline1 
,  
string		 
addressLine2		 
,		  
string

 

postalCode

 
,

 
string 
city 
, 
string 
stateProvinceCode $
,$ %
string 
countryRegionCode $
) 	
:
 
base 
( 
) 
{ 	
Query 
. 
Where 
( 
a 
=> 
a 
. 
AddressLine1 "
==# %
addressline1& 2
&&3 5
a 
. 
AddressLine2 "
==# %
addressLine2& 2
&&3 5
a 
. 

PostalCode  
==! #

postalCode$ .
&&/ 1
a 
. 
City 
== 
city "
&&# %
a 
. 
StateProvinceCode '
==( *
stateProvinceCode+ <
&&= ?
a 
. 
CountryRegionCode '
==( *
countryRegionCode+ <
) 
; 
} 	
} 
} Ã
§C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Specifications\GetCustomersPaginatedSpecification.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
Specifications+ 9
{ 
public 

class .
"GetCustomersPaginatedSpecification 3
:4 5
Specification6 C
<C D
DomainD J
.J K
CustomerK S
>S T
{ 
public		 .
"GetCustomersPaginatedSpecification		 1
(		1 2
int		2 5
	pageIndex		6 ?
,		? @
int		A D
pageSize		E M
,		M N
CustomerType		O [
?		[ \
customerType		] i
,		i j
string		k q
	territory		r {
,		{ |
string			} É
accountNumber
		Ñ ë
)
		ë í
:
		ì î
base
		ï ô
(
		ô ö
)
		ö õ
{

 	
Query 
. 
Include 
( 
c 
=> 
c  
.  !
	Addresses! *
)* +
. 
ThenInclude 
( 
a 
=> !
a" #
.# $
Address$ +
)+ ,
;, -
Query 
. 
Include 
( 
$str "
)" #
;# $
Query 
. 
Include 
( 
$str A
)A B
;B C
Query 
. 
Include 
( 
$str ?
)? @
;@ A
Query 
. 
Where 
( 
c 
=> 
( 
string 
. 
IsNullOrEmpty )
() *
	territory* 3
)3 4
||5 7
c8 9
.9 :
	Territory: C
==D F
	territoryG P
)P Q
&&R T
( 
! 
customerType "
." #
HasValue# +
||, .
(/ 0
customerType0 <
=== ?
CustomerType@ L
.L M

IndividualM W
?X Y
c 
is 
IndividualCustomer /
:0 1
c2 3
is4 6
StoreCustomer7 D
)D E
) 
&& 
( 
string 
. 
IsNullOrEmpty )
() *
accountNumber* 7
)7 8
||9 ;
c< =
.= >
AccountNumber> K
==L N
accountNumberO \
)\ ]
) 
. 
Skip 
( 
	pageIndex 
*  !
pageSize" *
)* +
. 
Take 
( 
pageSize 
) 
. 
OrderBy 
( 
c 
=> 
c 
.  
AccountNumber  -
)- .
;. /
} 	
}   
}!! §
öC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Specifications\GetCustomerSpecification.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
Specifications+ 9
{ 
public 

class $
GetCustomerSpecification )
:* +
Specification, 9
<9 :
Domain: @
.@ A
CustomerA I
>I J
{ 
public $
GetCustomerSpecification '
(' (
string( .
accountNumber/ <
)< =
:> ?
base@ D
(D E
)E F
{		 	
Query

 
.

 
Include

 
(

 
c

 
=>

 
c

  
.

  !
	Addresses

! *
)

* +
. 
ThenInclude 
( 
a 
=> !
a" #
.# $
Address$ +
)+ ,
;, -
Query 
. 
Include 
( 
$str A
)A B
;B C
Query 
. 
Include 
( 
$str ?
)? @
;@ A
Query 
. 
Include 
( 
c 
=> 
c  
.  !
SalesOrders! ,
), -
;- .
Query 
. 
Where 
( 
c 
=> 
c 
. 
AccountNumber +
==, .
accountNumber/ <
)< =
;= >
} 	
} 
} ÷
§C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Specifications\GetIndividualCustomerSpecification.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
Specifications+ 9
{ 
public 

class .
"GetIndividualCustomerSpecification 3
:4 5
Specification6 C
<C D
DomainD J
.J K
IndividualCustomerK ]
>] ^
{ 
public .
"GetIndividualCustomerSpecification 1
(1 2
string2 8
accountNumber9 F
)F G
:H I
baseJ N
(N O
)O P
{		 	
Query

 
. 
Where 
( 
c 
=> 
c 
. 
AccountNumber +
==, .
accountNumber/ <
)< =
;= >
} 	
} 
} ¬
üC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\Specifications\GetStoreCustomerSpecification.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
Specifications+ 9
{ 
public 

class )
GetStoreCustomerSpecification .
:/ 0
Specification1 >
<> ?
Domain? E
.E F
StoreCustomerF S
>S T
{ 
public )
GetStoreCustomerSpecification ,
(, -
string- 3
accountNumber4 A
)A B
:C D
baseE I
(I J
)J K
{		 	
Query

 
. 
Where 
( 
c 
=> 
c 
. 
AccountNumber +
==, .
accountNumber/ <
)< =
;= >
} 	
} 
} ™
ìC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomerAddress\AddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +!
UpdateCustomerAddress+ @
{ 
public 

class 

AddressDto 
: 
IMapFrom &
<& '
Domain' -
.- .
Address. 5
>5 6
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
}4 5
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
< 

AddressDto (
,( )
Domain* 0
.0 1
Address1 8
>8 9
(9 :
): ;
. 
	ForMember 
( 
m 
=> 
m  !
.! "
Id" $
,$ %
opt& )
=>* ,
opt- 0
.0 1
Ignore1 7
(7 8
)8 9
)9 :
;: ;
} 	
} 
} Í
õC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomerAddress\CustomerAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +!
UpdateCustomerAddress+ @
{ 
public 

class 
CustomerAddressDto #
:$ %
IMapFrom& .
<. /
Domain/ 5
.5 6
CustomerAddress6 E
>E F
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
public		 

AddressDto		 
Address		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
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
CustomerAddressDto 0
,0 1
Domain2 8
.8 9
CustomerAddress9 H
>H I
(I J
)J K
. 
	ForMember 
( 
m 
=> 
m  !
.! "
Id" $
,$ %
opt& )
=>* ,
opt- 0
.0 1
Ignore1 7
(7 8
)8 9
)9 :
. 
	ForMember 
( 
m 
=> 
m  !
.! "
	AddressID" +
,+ ,
opt- 0
=>1 3
opt4 7
.7 8
Ignore8 >
(> ?
)? @
)@ A
;A B
} 	
} 
} ü
•C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomerAddress\UpdateCustomerAddressCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +!
UpdateCustomerAddress+ @
{ 
public 

class (
UpdateCustomerAddressCommand -
:. /
IRequest0 8
<8 9
Unit9 =
>= >
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
CustomerAddressDto !
CustomerAddress" 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
}		 
}

 Ê8
¨C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomerAddress\UpdateCustomerAddressCommandHandler.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +!
UpdateCustomerAddress+ @
{ 
public 

class /
#UpdateCustomerAddressCommandHandler 4
:5 6
IRequestHandler7 F
<F G(
UpdateCustomerAddressCommandG c
,c d
Unite i
>i j
{ 
private 
readonly 
ILogger  
<  !/
#UpdateCustomerAddressCommandHandler! D
>D E
loggerF L
;L M
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Address0 7
>7 8
addressRepository9 J
;J K
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
public /
#UpdateCustomerAddressCommandHandler 2
(2 3
ILogger 
< /
#UpdateCustomerAddressCommandHandler 7
>7 8
logger9 ?
,? @
IMapper 
mapper 
, 
IRepositoryBase 
< 
Domain "
." #
Address# *
>* +
addressRepository, =
,= >
IRepositoryBase 
< 
Domain "
." #
Customer# +
>+ ,
customerRepository- ?
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  
mapper  &
,& '
this( ,
., -
addressRepository- >
,> ?
this@ D
.D E
customerRepositoryE W
)W X
=Y Z
( 
logger 
, 
mapper 
,  
addressRepository! 2
,2 3
customerRepository4 F
)F G
;G H
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '(
UpdateCustomerAddressCommand' C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var!! 
customer!! 
=!! 
await!!  
customerRepository!!! 3
.!!3 4
GetBySpecAsync!!4 B
(!!B C
new"" $
GetCustomerSpecification"" ,
("", -
request""- 4
.""4 5
AccountNumber""5 B
)""B C
)## 
;## 
Guard$$ 
.$$ 
Against$$ 
.$$ 
Null$$ 
($$ 
customer$$ '
,$$' (
nameof$$) /
($$/ 0
customer$$0 8
)$$8 9
)$$9 :
;$$: ;
logger&& 
.&& 
LogInformation&& !
(&&! "
$str&&" A
)&&A B
;&&B C
var'' 
customerAddress'' 
=''  !
customer''" *
.''* +
	Addresses''+ 4
.''4 5
FirstOrDefault''5 C
(''C D
ca(( 
=>(( 
ca(( 
.(( 
AddressType(( $
==((% '
request((( /
.((/ 0
CustomerAddress((0 ?
.((? @
AddressType((@ K
))) 
;)) 
Guard** 
.** 
Against** 
.** 
Null** 
(** 
customerAddress** .
,**. /
nameof**0 6
(**6 7
customerAddress**7 F
)**F G
)**G H
;**H I
var-- 
existingAddress-- 
=--  !
await--" '
IsExistingAddress--( 9
(--9 :
request--: A
.--A B
CustomerAddress--B Q
.--Q R
Address--R Y
)--Y Z
;--Z [
if// 
(// 
existingAddress// 
!=//  "
null//# '
)//' (
{00 
logger11 
.11 
LogInformation11 %
(11% &
$str11& >
)11> ?
;11? @
customerAddress22 
.22  
	AddressID22  )
=22* +
existingAddress22, ;
.22; <
Id22< >
;22> ?
}33 
else44 
{55 
logger66 
.66 
LogInformation66 %
(66% &
$str66& 7
)667 8
;668 9
customerAddress77 
.77  
Address77  '
=77( )
mapper77* 0
.770 1
Map771 4
<774 5
Domain775 ;
.77; <
Address77< C
>77C D
(77D E
request77E L
.77L M
CustomerAddress77M \
.77\ ]
Address77] d
)77d e
;77e f
}88 
logger:: 
.:: 
LogInformation:: !
(::! "
$str::" ?
)::? @
;::@ A
await;; 
customerRepository;; $
.;;$ %
UpdateAsync;;% 0
(;;0 1
customer;;1 9
);;9 :
;;;: ;
return== 
Unit== 
.== 
Value== 
;== 
}>> 	
private@@ 
async@@ 
Task@@ 
<@@ 
Domain@@ !
.@@! "
Address@@" )
>@@) *
IsExistingAddress@@+ <
(@@< =

AddressDto@@= G

addressDto@@H R
)@@R S
{AA 	
varBB 
addressBB 
=BB 
awaitBB 
addressRepositoryBB  1
.BB1 2
GetBySpecAsyncBB2 @
(BB@ A
newCC #
GetAddressSpecificationCC +
(CC+ ,

addressDtoDD 
.DD 
AddressLine1DD +
,DD+ ,

addressDtoEE 
.EE 
AddressLine2EE +
,EE+ ,

addressDtoFF 
.FF 

PostalCodeFF )
,FF) *

addressDtoGG 
.GG 
CityGG #
,GG# $

addressDtoHH 
.HH 
StateProvinceCodeHH 0
,HH0 1

addressDtoII 
.II 
CountryRegionCodeII 0
)JJ 
)KK 
;KK 
ifMM 
(MM 
addressMM 
!=MM 
nullMM 
)MM  
returnNN 
addressNN 
;NN 
returnPP 
nullPP 
;PP 
}QQ 	
}RR 
}SS †[
ÆC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomerAddress\UpdateCustomerAddressCommandValidator.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +!
UpdateCustomerAddress+ @
{		 
public

 

class

 1
%UpdateCustomerAddressCommandValidator

 6
:

7 8
AbstractValidator

9 J
<

J K(
UpdateCustomerAddressCommand

K g
>

g h
{ 
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
public 1
%UpdateCustomerAddressCommandValidator 4
(4 5
IRepositoryBase 
< 
Domain "
." #
Customer# +
>+ ,
customerRepository- ?
) 	
{ 	
this 
. 
customerRepository #
=$ %
customerRepository& 8
;8 9
RuleFor 
( 
cmd 
=> 
cmd 
. 
AccountNumber ,
), -
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( D
)D E
. 
MaximumLength 
( 
$num !
)! "
." #
WithMessage# .
(. /
$str/ ]
)] ^
. 
	MustAsync 
( 
CustomerExist (
)( )
.) *
WithMessage* 5
(5 6
$str6 O
)O P
;P Q
RuleFor 
( 
cmd 
=> 
cmd 
. 
CustomerAddress .
). /
. 
NotNull 
( 
) 
. 
WithMessage &
(& '
$str' E
)E F
;F G
RuleFor 
( 
cmd 
=> 
cmd 
. 
CustomerAddress .
.. /
AddressType/ :
): ;
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( B
)B C
. 
When 
( 
cmd 
=> 
cmd  
.  !
CustomerAddress! 0
!=1 3
null4 8
)8 9
;9 :
RuleFor   
(   
cmd   
=>   
cmd   
.   
CustomerAddress   .
.  . /
Address  / 6
)  6 7
.!! 
NotNull!! 
(!! 
)!! 
.!! 
WithMessage!! &
(!!& '
$str!!' <
)!!< =
."" 
When"" 
("" 
cmd"" 
=>"" 
cmd""  
.""  !
CustomerAddress""! 0
!=""1 3
null""4 8
)""8 9
;""9 :
RuleFor$$ 
($$ 
cmd$$ 
=>$$ 
cmd$$ 
.$$ 
CustomerAddress$$ .
.$$. /
Address$$/ 6
.$$6 7
AddressLine1$$7 C
)$$C D
.%% 
NotEmpty%% 
(%% 
)%% 
.%% 
WithMessage%% '
(%%' (
$str%%( D
)%%D E
.&& 
MaximumLength&& 
(&& 
$num&& !
)&&! "
.&&" #
WithMessage&&# .
(&&. /
$str&&/ ]
)&&] ^
.'' 
When'' 
('' 
cmd'' 
=>'' 
cmd''  
.''  !
CustomerAddress''! 0
!=''1 3
null''4 8
&&''9 ;
cmd''< ?
.''? @
CustomerAddress''@ O
.''O P
Address''P W
!=''X Z
null''[ _
)''_ `
;''` a
RuleFor)) 
()) 
cmd)) 
=>)) 
cmd)) 
.)) 
CustomerAddress)) .
.)). /
Address))/ 6
.))6 7
AddressLine2))7 C
)))C D
.** 
MaximumLength** 
(** 
$num** !
)**! "
.**" #
WithMessage**# .
(**. /
$str**/ ]
)**] ^
.++ 
When++ 
(++ 
cmd++ 
=>++ 
cmd++  
.++  !
CustomerAddress++! 0
!=++1 3
null++4 8
&&++9 ;
cmd++< ?
.++? @
CustomerAddress++@ O
.++O P
Address++P W
!=++X Z
null++[ _
)++_ `
;++` a
RuleFor-- 
(-- 
cmd-- 
=>-- 
cmd-- 
.-- 
CustomerAddress-- .
.--. /
Address--/ 6
.--6 7

PostalCode--7 A
)--A B
... 
NotEmpty.. 
(.. 
).. 
... 
WithMessage.. '
(..' (
$str..( A
)..A B
.// 
MaximumLength// 
(// 
$num// !
)//! "
.//" #
WithMessage//# .
(//. /
$str/// Z
)//Z [
.00 
When00 
(00 
cmd00 
=>00 
cmd00  
.00  !
CustomerAddress00! 0
!=001 3
null004 8
&&009 ;
cmd00< ?
.00? @
CustomerAddress00@ O
.00O P
Address00P W
!=00X Z
null00[ _
)00_ `
;00` a
RuleFor22 
(22 
cmd22 
=>22 
cmd22 
.22 
CustomerAddress22 .
.22. /
Address22/ 6
.226 7
City227 ;
)22; <
.33 
NotEmpty33 
(33 
)33 
.33 
WithMessage33 '
(33' (
$str33( :
)33: ;
.44 
MaximumLength44 
(44 
$num44 !
)44! "
.44" #
WithMessage44# .
(44. /
$str44/ S
)44S T
.55 
When55 
(55 
cmd55 
=>55 
cmd55  
.55  !
CustomerAddress55! 0
!=551 3
null554 8
&&559 ;
cmd55< ?
.55? @
CustomerAddress55@ O
.55O P
Address55P W
!=55X Z
null55[ _
)55_ `
;55` a
RuleFor77 
(77 
cmd77 
=>77 
cmd77 
.77 
CustomerAddress77 .
.77. /
Address77/ 6
.776 7
StateProvinceCode777 H
)77H I
.88 
NotEmpty88 
(88 
)88 
.88 
WithMessage88 '
(88' (
$str88( D
)88D E
.99 
MaximumLength99 
(99 
$num99  
)99  !
.99! "
WithMessage99" -
(99- .
$str99. [
)99[ \
.:: 
When:: 
(:: 
cmd:: 
=>:: 
cmd::  
.::  !
CustomerAddress::! 0
!=::1 3
null::4 8
&&::9 ;
cmd::< ?
.::? @
CustomerAddress::@ O
.::O P
Address::P W
!=::X Z
null::[ _
)::_ `
;::` a
RuleFor<< 
(<< 
cmd<< 
=><< 
cmd<< 
)<< 
.== 
	MustAsync== 
(== 
UniqueAddress== (
)==( )
.==) *
WithMessage==* 5
(==5 6
$str==6 N
)==N O
.>> 
When>> 
(>> 
cmd>> 
=>>> 
cmd>>  
.>>  !
CustomerAddress>>! 0
!=>>1 3
null>>4 8
)>>8 9
;>>9 :
}?? 	
privateAA 
asyncAA 
TaskAA 
<AA 
boolAA 
>AA  
UniqueAddressAA! .
(AA. /(
UpdateCustomerAddressCommandAA/ K
commandAAL S
,AAS T
CancellationTokenAAU f
cancellationTokenAAg x
)AAx y
{BB 	
varCC 
customerCC 
=CC 
awaitCC  
customerRepositoryCC! 3
.CC3 4
GetBySpecAsyncCC4 B
(CCB C
newCCC F$
GetCustomerSpecificationCCG _
(CC_ `
commandCC` g
.CCg h
AccountNumberCCh u
)CCu v
)CCv w
;CCw x
varEE 
addressEE 
=EE 
customerEE "
.EE" #
	AddressesEE# ,
.EE, -
FirstOrDefaultEE- ;
(EE; <
aEE< =
=>EE> @
aFF 
.FF 
AddressTypeFF 
==FF  
commandFF! (
.FF( )
CustomerAddressFF) 8
.FF8 9
AddressTypeFF9 D
&&FFE G
aGG 
.GG 
AddressGG 
.GG 
AddressLine1GG &
==GG' )
commandGG* 1
.GG1 2
CustomerAddressGG2 A
.GGA B
AddressGGB I
.GGI J
AddressLine1GGJ V
&&GGW Y
aHH 
.HH 
AddressHH 
.HH 
AddressLine2HH &
==HH' )
commandHH* 1
.HH1 2
CustomerAddressHH2 A
.HHA B
AddressHHB I
.HHI J
AddressLine2HHJ V
&&HHW Y
aII 
.II 
AddressII 
.II 

PostalCodeII $
==II% '
commandII( /
.II/ 0
CustomerAddressII0 ?
.II? @
AddressII@ G
.IIG H

PostalCodeIIH R
&&IIS U
aJJ 
.JJ 
AddressJJ 
.JJ 
CityJJ 
==JJ !
commandJJ" )
.JJ) *
CustomerAddressJJ* 9
.JJ9 :
AddressJJ: A
.JJA B
CityJJB F
&&JJG I
aKK 
.KK 
AddressKK 
.KK 
StateProvinceCodeKK +
==KK, .
commandKK/ 6
.KK6 7
CustomerAddressKK7 F
.KKF G
AddressKKG N
.KKN O
StateProvinceCodeKKO `
&&KKa c
aLL 
.LL 
AddressLL 
.LL 
CountryRegionCodeLL +
==LL, .
commandLL/ 6
.LL6 7
CustomerAddressLL7 F
.LLF G
AddressLLG N
.LLN O
CountryRegionCodeLLO `
)MM 
;MM 
returnOO 
addressOO 
==OO 
nullOO "
;OO" #
}PP 	
privateRR 
asyncRR 
TaskRR 
<RR 
boolRR 
>RR  
CustomerExistRR! .
(RR. /
stringRR/ 5
accountNumberRR6 C
,RRC D
CancellationTokenRRE V
cancellationTokenRRW h
)RRh i
{SS 	
varTT 
customerTT 
=TT 
awaitTT  
customerRepositoryTT! 3
.TT3 4
GetBySpecAsyncTT4 B
(TTB C
newTTC F$
GetCustomerSpecificationTTG _
(TT_ `
accountNumberTT` m
)TTm n
)TTn o
;TTo p
returnUU 
customerUU 
!=UU 
nullUU #
;UU# $
}VV 	
}WW 
}XX à
åC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\AddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class 

AddressDto 
: 
IMapFrom &
<& '
Address' .
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
} §
îC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\CustomerAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class 
CustomerAddressDto #
:$ %
IMapFrom& .
<. /
Domain/ 5
.5 6
CustomerAddress6 E
>E F
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
 

AddressDto

 
Address

 !
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
< 
Domain $
.$ %
CustomerAddress% 4
,4 5
CustomerAddressDto6 H
>H I
(I J
)J K
. 

ReverseMap 
( 
) 
. 
EqualityComparison #
(# $
($ %
src% (
,( )
dest* .
). /
=>0 2
src3 6
.6 7
AddressType7 B
==C E
destF J
.J K
AddressTypeK V
)V W
;W X
} 	
} 
} ˜
çC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\CustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

abstract 
class 
CustomerDto %
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
	Territory 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
List		 
<		 
CustomerAddressDto		 &
>		& '
	Addresses		( 1
{		2 3
get		4 7
;		7 8
set		9 <
;		< =
}		> ?
}

 
} Ó

óC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\IndividualCustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class !
IndividualCustomerDto &
:' (
CustomerDto) 4
,4 5
IMapFrom6 >
<> ?
Domain? E
.E F
IndividualCustomerF X
>X Y
{ 
public 
	PersonDto 
Person 
{  !
get" %
;% &
set' *
;* +
}, -
=. /
new0 3
	PersonDto4 =
(= >
)> ?
;? @
public

 
void

 
Mapping

 
(

 
Profile

 #
profile

$ +
)

+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
IndividualCustomer% 7
,7 8!
IndividualCustomerDto9 N
>N O
(O P
)P Q
. 

ReverseMap 
( 
) 
; 
} 	
} 
} Ã
ãC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\PersonDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class 
	PersonDto 
: 
IMapFrom %
<% &
Domain& ,
., -
Person- 3
>3 4
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
< !
PersonEmailAddressDto )
>) *
EmailAddresses+ 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 
List 
< 
PersonPhoneDto "
>" #
PhoneNumbers$ 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
Person% +
,+ ,
	PersonDto- 6
>6 7
(7 8
)8 9
. 

ReverseMap 
( 
) 
; 
} 	
} 
} ó
óC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\PersonEmailAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class !
PersonEmailAddressDto &
:' (
IMapFrom) 1
<1 2
Domain2 8
.8 9
PersonEmailAddress9 K
>K L
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
< 
Domain $
.$ %
PersonEmailAddress% 7
,7 8!
PersonEmailAddressDto9 N
>N O
(O P
)P Q
. 

ReverseMap 
( 
) 
. 
EqualityComparison #
(# $
($ %
src% (
,( )
dest* .
). /
=>0 2
src3 6
.6 7
EmailAddress7 C
==D F
destG K
.K L
EmailAddressL X
)X Y
;Y Z
} 	
} 
} ú
êC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\PersonPhoneDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class 
PersonPhoneDto 
:  !
IMapFrom" *
<* +
Domain+ 1
.1 2
PersonPhone2 =
>= >
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
< 
Domain $
.$ %
PersonPhone% 0
,0 1
PersonPhoneDto2 @
>@ A
(A B
)B C
. 

ReverseMap 
( 
) 
. 
EqualityComparison #
(# $
($ %
src% (
,( )
dest* .
). /
=>0 2
src3 6
.6 7
PhoneNumberType7 F
==G I
destJ N
.N O
PhoneNumberTypeO ^
)^ _
;_ `
} 	
} 
} ¬
ôC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\StoreCustomerContactDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class #
StoreCustomerContactDto (
:) *
IMapFrom+ 3
<3 4
Domain4 :
.: ; 
StoreCustomerContact; O
>O P
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
 
	PersonDto

 
ContactPerson

 &
{

' (
get

) ,
;

, -
set

. 1
;

1 2
}

3 4
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
< 
Domain $
.$ % 
StoreCustomerContact% 9
,9 :#
StoreCustomerContactDto; R
>R S
(S T
)T U
. 

ReverseMap 
( 
) 
. 
EqualityComparison #
(# $
($ %
src% (
,( )
dest* .
). /
=>0 2
src3 6
.6 7
ContactType7 B
==C E
destF J
.J K
ContactTypeK V
)V W
;W X
} 	
} 
} Ë
íC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\StoreCustomerDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class 
StoreCustomerDto !
:" #
CustomerDto$ /
,/ 0
IMapFrom1 9
<9 :
Domain: @
.@ A
StoreCustomerA N
>N O
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
< #
StoreCustomerContactDto +
>+ ,
Contacts- 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
public 
void 
Mapping 
( 
Profile #
profile$ +
)+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
StoreCustomer% 2
,2 3
StoreCustomerDto4 D
>D E
(E F
)F G
. 

ReverseMap 
( 
) 
; 
} 	
} 
} €
óC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\UpdateCustomerCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public 

class !
UpdateCustomerCommand &
:' (
IRequest) 1
<1 2
CustomerDto2 =
>= >
{ 
public 
CustomerDto 
Customer #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
}		 ∆
ûC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\UpdateCustomerCommandHandler.cs
	namespace		 	
AW		
 
.		 
Services		 
.		 
Customer		 
.		 
Application		 *
.		* +
UpdateCustomer		+ 9
{

 
public 

class (
UpdateCustomerCommandHandler -
:. /
IRequestHandler0 ?
<? @!
UpdateCustomerCommand@ U
,U V
CustomerDtoW b
>b c
{ 
private 
readonly 
ILogger  
<  !(
UpdateCustomerCommandHandler! =
>= >
logger? E
;E F
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
private 
readonly 
IMapper  
mapper! '
;' (
public (
UpdateCustomerCommandHandler +
(+ ,
ILogger 
< (
UpdateCustomerCommandHandler 0
>0 1
logger2 8
,8 9
IRepositoryBase 
< 
Domain "
." #
Customer# +
>+ ,
customerRepository- ?
,? @
IMapper 
mapper 
) 
=> 
( 
this 
. 
logger 
, 
this "
." #
customerRepository# 5
,5 6
this7 ;
.; <
mapper< B
)B C
=D E
(F G
loggerG M
,M N
customerRepositoryO a
,a b
mapperc i
)i j
;j k
public 
async 
Task 
< 
CustomerDto %
>% &
Handle' -
(- .!
UpdateCustomerCommand. C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
spec 
= 
new $
GetCustomerSpecification 3
(3 4
request4 ;
.; <
Customer< D
.D E
AccountNumberE R
)R S
;S T
var 
customer 
= 
await  
customerRepository! 3
.3 4
GetBySpecAsync4 B
(B C
specC G
)G H
;H I
logger 
. 
LogInformation !
(! "
$str" 5
)5 6
;6 7
mapper   
.   
Map   
(   
request   
.   
Customer   '
,  ' (
customer  ) 1
)  1 2
;  2 3
logger"" 
."" 
LogInformation"" !
(""! "
$str""" ?
)""? @
;""@ A
await## 
customerRepository## $
.##$ %
UpdateAsync##% 0
(##0 1
customer##1 9
)##9 :
;##: ;
logger%% 
.%% 
LogInformation%% !
(%%! "
$str%%" 6
)%%6 7
;%%7 8
return&& 
mapper&& 
.&& 
Map&& 
<&& 
CustomerDto&& )
>&&) *
(&&* +
customer&&+ 3
)&&3 4
;&&4 5
}'' 	
}(( 
})) π
†C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateCustomer\UpdateCustomerCommandValidator.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +
UpdateCustomer+ 9
{ 
public		 

class		 *
UpdateCustomerCommandValidator		 /
:		0 1
AbstractValidator		2 C
<		C D!
UpdateCustomerCommand		D Y
>		Y Z
{

 
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
Customer0 8
>8 9
customerRepository: L
;L M
public *
UpdateCustomerCommandValidator -
(- .
IRepositoryBase. =
<= >
Domain> D
.D E
CustomerE M
>M N
customerRepositoryO a
)a b
{ 	
this 
. 
customerRepository #
=$ %
customerRepository& 8
;8 9
RuleFor 
( 
cmd 
=> 
cmd 
. 
Customer '
)' (
. 
NotNull 
( 
) 
. 
WithMessage &
(& '
$str' =
)= >
;> ?
RuleFor 
( 
cmd 
=> 
cmd 
. 
Customer '
.' (
AccountNumber( 5
)5 6
. 
NotEmpty 
( 
) 
. 
WithMessage '
(' (
$str( D
)D E
. 
MaximumLength 
( 
$num !
)! "
." #
WithMessage# .
(. /
$str/ ]
)] ^
. 
	MustAsync 
( 
CustomerExists )
)) *
.* +
WithMessage+ 6
(6 7
$str7 P
)P Q
. 
When 
( 
cmd 
=> 
cmd  
.  !
Customer! )
!=* ,
null- 1
)1 2
;2 3
} 	
private 
async 
Task 
< 
bool 
>  
CustomerExists! /
(/ 0
string0 6
accountNumber7 D
,D E
CancellationTokenF W
cancellationTokenX i
)i j
{ 	
var 
customer 
= 
await  
customerRepository! 3
.3 4
GetBySpecAsync4 B
(B C
newC F$
GetCustomerSpecificationG _
(_ `
accountNumber` m
)m n
)n o
;o p
return 
customer 
!= 
null #
;# $
} 	
}   
}!! ¶	
ùC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateStoreCustomerContact\EmailAddressDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
UpdateStoreCustomerContact+ E
{ 
public 

class 
EmailAddressDto  
:! "
IMapFrom# +
<+ ,
Domain, 2
.2 3
PersonEmailAddress3 E
>E F
{ 
public 
string 
EmailAddress "
{# $
get% (
;( )
set* -
;- .
}/ 0
public

 
void

 
Mapping

 
(

 
Profile

 #
profile

$ +
)

+ ,
{ 	
profile 
. 
	CreateMap 
< 
Domain $
.$ %
PersonEmailAddress% 7
,7 8
EmailAddressDto9 H
>H I
(I J
)J K
;K L
} 	
} 
} €
óC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateStoreCustomerContact\PersonDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
UpdateStoreCustomerContact+ E
{ 
public 

class 
	PersonDto 
: 
IMapFrom %
<% &
Domain& ,
., -
Person- 3
>3 4
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
< 
EmailAddressDto #
># $
EmailAddresses% 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
=B C
newD G
ListH L
<L M
EmailAddressDtoM \
>\ ]
(] ^
)^ _
;_ `
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
< 
Domain $
.$ %
Person% +
,+ ,
	PersonDto- 6
>6 7
(7 8
)8 9
;9 :
} 	
} 
} Â

•C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateStoreCustomerContact\StoreCustomerContactDto.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
UpdateStoreCustomerContact+ E
{ 
public 

class #
StoreCustomerContactDto (
:) *
IMapFrom+ 3
<3 4
Domain4 :
.: ; 
StoreCustomerContact; O
>O P
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
public		 
	PersonDto		 
ContactPerson		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
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
< 
Domain $
.$ % 
StoreCustomerContact% 9
,9 :#
StoreCustomerContactDto; R
>R S
(S T
)T U
;U V
} 	
} 
} ∏
ØC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateStoreCustomerContact\UpdateStoreCustomerContactCommand.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +&
UpdateStoreCustomerContact+ E
{ 
public 

class -
!UpdateStoreCustomerContactCommand 2
:3 4
IRequest5 =
<= >
Unit> B
>B C
{ 
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public #
StoreCustomerContactDto &
CustomerContact' 6
{7 8
get9 <
;< =
set> A
;A B
}C D
}		 
}

 ≠#
∂C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Application\UpdateStoreCustomerContact\UpdateStoreCustomerContactCommandHandler.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Application *
.* +#
AddStoreCustomerContact+ B
{ 
public 

class 4
(UpdateStoreCustomerContactCommandHandler 9
:: ;
IRequestHandler< K
<K L-
!UpdateStoreCustomerContactCommandL m
,m n
Unito s
>s t
{ 
private 
readonly 
ILogger  
<  !4
(UpdateStoreCustomerContactCommandHandler! I
>I J
loggerK Q
;Q R
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IRepositoryBase (
<( )
Domain) /
./ 0
StoreCustomer0 =
>= >
storeRepository? N
;N O
public 4
(UpdateStoreCustomerContactCommandHandler 7
(7 8
ILogger 
< 4
(UpdateStoreCustomerContactCommandHandler <
>< =
logger> D
,D E
IMapper 
mapper 
, 
IRepositoryBase 
< 
Domain "
." #
StoreCustomer# 0
>0 1
storeRepository2 A
) 	
=>
 
( 
this 
. 
logger 
, 
this 
.  
mapper  &
,& '
this( ,
., -
storeRepository- <
)< =
=> ?
(@ A
loggerA G
,G H
mapperI O
,O P
storeRepositoryQ `
)` a
;a b
public 
async 
Task 
< 
Unit 
> 
Handle  &
(& '-
!UpdateStoreCustomerContactCommand' H
requestI P
,P Q
CancellationTokenR c
cancellationTokend u
)u v
{ 	
logger 
. 
LogInformation !
(! "
$str" 1
)1 2
;2 3
logger 
. 
LogInformation !
(! "
$str" B
)B C
;C D
var 
store 
= 
await 
storeRepository -
.- .
GetBySpecAsync. <
(< =
new   )
GetStoreCustomerSpecification   1
(  1 2
request  2 9
.  9 :
AccountNumber  : G
)  G H
)!! 
;!! 
Guard"" 
."" 
Against"" 
."" 
Null"" 
("" 
store"" $
,""$ %
nameof""& ,
("", -
store""- 2
)""2 3
)""3 4
;""4 5
var$$ 
contact$$ 
=$$ 
store$$ 
.$$  
Contacts$$  (
.$$( )
FirstOrDefault$$) 7
($$7 8
c$$8 9
=>$$: <
c%% 
.%% 
ContactType%% 
==%%  
request%%! (
.%%( )
CustomerContact%%) 8
.%%8 9
ContactType%%9 D
)&& 
;&& 
Guard'' 
.'' 
Against'' 
.'' 
Null'' 
('' 
contact'' &
,''& '
nameof''( .
(''. /
contact''/ 6
)''6 7
)''7 8
;''8 9
logger)) 
.)) 
LogInformation)) !
())! "
$str))" 4
)))4 5
;))5 6
mapper** 
.** 
Map** 
(** 
request** 
.** 
CustomerContact** .
,**. /
contact**0 7
)**7 8
;**8 9
logger,, 
.,, 
LogInformation,, !
(,,! "
$str,," ?
),,? @
;,,@ A
await-- 
storeRepository-- !
.--! "
UpdateAsync--" -
(--- .
store--. 3
)--3 4
;--4 5
return// 
Unit// 
.// 
Value// 
;// 
}00 	
}11 
}22 