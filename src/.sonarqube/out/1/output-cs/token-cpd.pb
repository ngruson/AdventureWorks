Ð
uC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\Address.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class 
Address 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
AddressLine1 "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
AddressLine2 "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 

PostalCode  
{! "
get# &
;& '
set( +
;+ ,
}- .
public		 
string		 
City		 
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
 
StateProvinceCode

 '
{

( )
get

* -
;

- .
set

/ 2
;

2 3
}

4 5
public 
string 
CountryRegionCode '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} «
vC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\Customer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

abstract 
class 
Customer "
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
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
< 
CustomerAddress #
># $
	Addresses% .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
List 
< 

SalesOrder 
> 
SalesOrders  +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
} 
} õ
}C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\CustomerAddress.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class 
CustomerAddress  
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
AddressType !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
Address 
Address 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
	AddressID 
{ 
get "
;" #
set$ '
;' (
}) *
}		 
}

 À
zC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\CustomerType.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
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
} ò
€C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\IndividualCustomer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class 
IndividualCustomer #
:$ %
Customer& .
{ 
public 
Person 
Person 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
new- 0
Person1 7
(7 8
)8 9
;9 :
public 
override 
CustomerType $
CustomerType% 1
=>2 4
CustomerType5 A
.A B

IndividualB L
;L M
}		 
}

 Ë
tC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\Person.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class 
Person 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
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
public 
List 
< 
PersonEmailAddress &
>& '
EmailAddresses( 6
{7 8
get9 <
;< =
set> A
;A B
}C D
public 
List 
< 
PersonPhone 
>  
PhoneNumbers! -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} 
} Ç
€C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\PersonEmailAddress.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class 
PersonEmailAddress #
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
EmailAddress "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
} Ú
yC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\PersonPhone.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class 
PersonPhone 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
PhoneNumberType %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
PhoneNumber !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
}		 Í
xC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\SalesOrder.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class 

SalesOrder 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
DateTime 
	OrderDate !
{" #
get$ '
;' (
set) ,
;, -
}. /
public

 
DateTime

 
DueDate

 
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
public 
DateTime 
? 
ShipDate !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
SalesOrderStatus 
Status  &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
bool 
OnlineOrderFlag #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
SalesOrderNumber &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
PurchaseOrderNumber )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public 
string 
AccountNumber #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 
TotalDue 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} È
~C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\SalesOrderStatus.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
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
} —

{C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\StoreCustomer.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class 
StoreCustomer 
:  
Customer! )
{ 
public 
override 
CustomerType $
CustomerType% 1
=>2 4
CustomerType5 A
.A B
StoreB G
;G H
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public		 
string		 
SalesPerson		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public

 
List

 
<

  
StoreCustomerContact

 (
>

( )
Contacts

* 2
{

3 4
get

5 8
;

8 9
set

: =
;

= >
}

? @
=

A B
new

C F
List

G K
<

K L 
StoreCustomerContact

L `
>

` a
(

a b
)

b c
;

c d
} 
} Ó

‚C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Domain\StoreCustomerContact.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Domain %
{ 
public 

class  
StoreCustomerContact %
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
ContactType !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
StoreCustomer 
StoreCustomer *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public		 
int		 
StoreCustomerId		 "
{		# $
get		% (
;		( )
set		* -
;		- .
}		/ 0
public 
Person 
ContactPerson #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
int 
ContactPersonId "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
} 