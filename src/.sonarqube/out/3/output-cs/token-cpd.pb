î
C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\AWContext.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Persistence *
.* +
EntityFrameworkCore+ >
{ 
public 

class 
	AWContext 
: 
	DbContext &
{ 
public 
	AWContext 
( 
) 
:		 
base		 
(		 
)		 
{

 	
} 	
public 
	AWContext 
( 
DbContextOptions )
<) *
	AWContext* 3
>3 4
options5 <
)< =
:> ?
base@ D
(D E
optionsE L
)L M
{ 	
} 	
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
modelBuilder= I
)I J
{ 	
modelBuilder 
. +
ApplyConfigurationsFromAssembly 8
(8 9
Assembly9 A
.A B 
GetExecutingAssemblyB V
(V W
)W X
)X Y
;Y Z
} 	
public 
virtual 
void 
SetModified '
(' (
object( .
entity/ 5
)5 6
{ 	
Entry 
( 
entity 
) 
. 
State 
=  !
EntityState" -
.- .
Modified. 6
;6 7
} 	
} 
} ¯
ªC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\AddressConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class  
AddressConfiguration %
:& '$
IEntityTypeConfiguration( @
<@ A
CustomerA I
.I J
DomainJ P
.P Q
AddressQ X
>X Y
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @
Address@ G
>G H
builderI P
)P Q
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 %
)

% &
;

& '
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
Id$ &
)& '
. 
HasColumnName 
( 
$str *
)* +
;+ ,
} 	
} 
} Ï
²C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\CustomerAddressConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class (
CustomerAddressConfiguration -
:. /$
IEntityTypeConfiguration0 H
<H I
CustomerI Q
.Q R
DomainR X
.X Y
CustomerAddressY h
>h i
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @
CustomerAddress@ O
>O P
builderQ X
)X Y
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 -
)

- .
;

. /
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
Id$ &
)& '
. 
HasColumnName 
( 
$str 2
)2 3
;3 4
} 	
} 
} 
«C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\CustomerConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class !
CustomerConfiguration &
:' ($
IEntityTypeConfiguration) A
<A B
CustomerB J
.J K
DomainK Q
.Q R
CustomerR Z
>Z [
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @
Customer@ H
>H I
builderJ Q
)Q R
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 &
)

& '
;

' (
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
Id$ &
)& '
. 
HasColumnName 
( 
$str +
)+ ,
;, -
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
AccountNumber$ 1
)1 2
. 

IsRequired 
( 
) 
. 
HasMaxLength 
( 
$num  
)  !
;! "
} 	
} 
} ›	
µC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\IndividualCustomerConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class +
IndividualCustomerConfiguration 0
:1 2$
IEntityTypeConfiguration3 K
<K L
CustomerL T
.T U
DomainU [
.[ \
IndividualCustomer\ n
>n o
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @
IndividualCustomer@ R
>R S
builderT [
)[ \
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 0
)

0 1
;

1 2
} 	
} 
} «
©C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\PersonConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class 
PersonConfiguration $
:% &$
IEntityTypeConfiguration' ?
<? @
Customer@ H
.H I
DomainI O
.O P
PersonP V
>V W
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @
Person@ F
>F G
builderH O
)O P
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 $
)

$ %
;

% &
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder 
. 
Property 
( 
p 
=> !
p" #
.# $
Id$ &
)& '
. 
HasColumnName 
( 
$str )
)) *
;* +
} 	
} 
} Û
µC:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\PersonEmailAddressConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class +
PersonEmailAddressConfiguration 0
:1 2$
IEntityTypeConfiguration3 K
<K L
CustomerL T
.T U
DomainU [
.[ \
PersonEmailAddress\ n
>n o
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @
PersonEmailAddress@ R
>R S
builderT [
)[ \
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 0
)

0 1
;

1 2
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder 
. 
Property 
( 
p 
=> !
p" #
.# $
Id$ &
)& '
. 
HasColumnName 
( 
$str 5
)5 6
;6 7
} 	
} 
} ¿
®C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\PersonPhoneConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class $
PersonPhoneConfiguration )
:* +$
IEntityTypeConfiguration, D
<D E
CustomerE M
.M N
DomainN T
.T U
PersonPhoneU `
>` a
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @
PersonPhone@ K
>K L
builderM T
)T U
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 )
)

) *
;

* +
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
Id$ &
)& '
. 
HasColumnName 
( 
$str .
). /
;/ 0
} 	
} 
} ò
­C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\SalesOrderConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Persistence *
.* +
EntityFrameworkCore+ >
.> ?
Configurations? M
{ 
public 

class #
SalesOrderConfiguration (
:) *$
IEntityTypeConfiguration+ C
<C D
DomainD J
.J K

SalesOrderK U
>U V
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Domain0 6
.6 7

SalesOrder7 A
>A B
builderC J
)J K
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 (
)

( )
;

) *
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
Id$ &
)& '
. 
HasColumnName 
( 
$str -
)- .
;. /
} 	
} 
} ‡	
°C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\StoreCustomerConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class &
StoreCustomerConfiguration +
:, -$
IEntityTypeConfiguration. F
<F G
CustomerG O
.O P
DomainP V
.V W
StoreCustomerW d
>d e
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @
StoreCustomer@ M
>M N
builderO V
)V W
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 +
)

+ ,
;

, -
} 	
} 
} ™
·C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\Configurations\StoreCustomerContactConfiguration.cs
	namespace 	
AW
 
. 
Services 
. 
Product 
. 
Persistence )
.) *
EntityFrameworkCore* =
.= >
Configurations> L
{ 
public 

class -
!StoreCustomerContactConfiguration 2
:3 4$
IEntityTypeConfiguration5 M
<M N
CustomerN V
.V W
DomainW ]
.] ^ 
StoreCustomerContact^ r
>r s
{ 
public 
void 
	Configure 
( 
EntityTypeBuilder /
</ 0
Customer0 8
.8 9
Domain9 ?
.? @ 
StoreCustomerContact@ T
>T U
builderV ]
)] ^
{		 	
builder

 
.

 
ToTable

 
(

 
$str

 2
)

2 3
;

3 4
builder 
. 
HasKey 
( 
p 
=> 
p  !
.! "
Id" $
)$ %
;% &
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
Id$ &
)& '
. 
HasColumnName 
( 
$str 7
)7 8
;8 9
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
StoreCustomerId$ 3
)3 4
. 
HasColumnName 
( 
$str +
)+ ,
;, -
builder 
. 
Property 
( 
c 
=> !
c" #
.# $
ContactPersonId$ 3
)3 4
. 
HasColumnName 
( 
$str )
)) *
;* +
} 	
} 
} 
“C:\Users\ngrus\Google Drive\Repos\ngruson\AdventureWorks\src\Services\Customer\AW.Services.Customer.Persistence.EntityFrameworkCore\EfRepository.cs
	namespace 	
AW
 
. 
Services 
. 
Customer 
. 
Persistence *
.* +
EntityFrameworkCore+ >
{ 
public 

class 
EfRepository 
< 
T 
>  
:! "
RepositoryBase# 1
<1 2
T2 3
>3 4
where5 :
T; <
:= >
class? D
{ 
public 
EfRepository 
( 
	AWContext %
	dbContext& /
)/ 0
:1 2
base3 7
(7 8
	dbContext8 A
)A B
{ 	
}

 	
} 
} 