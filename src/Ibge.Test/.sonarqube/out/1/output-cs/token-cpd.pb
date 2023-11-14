ç-
NC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Configure.cs
	namespace 	
Ibge
 
. 
Infrastructure 
; 
public 
static 
class 
	Configure 
{ 
public 

static 
IServiceCollection $
ConfigureInfra% 3
(3 4
this4 8
IServiceCollection9 K
servicesL T
,T U
IConfigurationV d
configuratione r
)r s
{ 
services 
. 
AddHostedService !
<! "!
StateFromFileServices" 7
>7 8
(8 9
)9 :
;: ;
services 
. 
AddHostedService !
<! " 
CityFromFileServices" 6
>6 7
(7 8
)8 9
;9 :
return 
services 
. $
ConfigureEntityFramework %
(% &
configuration& 3
)3 4
. !
ConfigureRepositories "
(" #
)# $
. 
ConfigureWorkers 
( 
) 
;  
} 
private 
static 
IServiceCollection %$
ConfigureEntityFramework& >
(> ?
this? C
IServiceCollectionD V
servicesW _
,_ `
IConfigurationa o
configurationp }
)} ~
{ 
var 
stringConnection 
= 
configuration ,
., -
GetConnectionString- @
(@ A
$strA K
)K L
;L M!
ArgumentNullException   
.   
ThrowIfNull   )
(  ) *
stringConnection  * :
)  : ;
;  ; <
var"" 
options"" 
="" 
new"" #
DbContextOptionsBuilder"" 1
<""1 2
DatabaseContext""2 A
>""A B
(""B C
)""C D
;""D E
options## 
.## 
	UseSqlite## 
(## 
stringConnection## *
)##* +
;##+ ,
options$$ 
.$$ 
LogTo$$ 
($$ 
Console$$ 
.$$ 
Write$$ #
,$$# $
	Microsoft$$% .
.$$. /

Extensions$$/ 9
.$$9 :
Logging$$: A
.$$A B
LogLevel$$B J
.$$J K
Warning$$K R
)$$R S
;$$S T
_%% 	
=%%
 
new%% 
DatabaseContext%% 
(%%  
options%%  '
.%%' (
Options%%( /
,%%/ 0
true%%1 5
)%%5 6
;%%6 7
services'' 
.'' 
AddDbContext'' 
<'' 
DatabaseContext'' -
>''- .
(''. /
options''/ 6
=>''7 9
{(( 	
options)) 
.)) 
	UseSqlite)) 
()) 
stringConnection)) .
))). /
;))/ 0
options** 
.** 
LogTo** 
(** 
Console** !
.**! "
Write**" '
,**' (
	Microsoft**) 2
.**2 3

Extensions**3 =
.**= >
Logging**> E
.**E F
LogLevel**F N
.**N O
Warning**O V
)**V W
;**W X
}++ 	
)++	 

;++
 
return-- 
services-- 
;-- 
}.. 
private00 
static00 
IServiceCollection00 %!
ConfigureRepositories00& ;
(00; <
this00< @
IServiceCollection00A S
services00T \
)00\ ]
{11 
services22 
.22 
	AddScoped22 
<22 
IUserRepository22 *
,22* +
UserRepository22, :
>22: ;
(22; <
)22< =
;22= >
services33 
.33 
	AddScoped33 
<33 
IStateRepository33 +
,33+ ,
StateRepository33- <
>33< =
(33= >
)33> ?
.44 
Decorate44 
<44 
IStateRepository44 *
,44* +!
CachedStateRepository44, A
>44A B
(44B C
)44C D
;44D E
services66 
.66 
	AddScoped66 
<66 
ICityRepository66 *
,66* +
CityRepository66, :
>66: ;
(66; <
)66< =
;66= >
return88 
services88 
;88 
}99 
private;; 
static;; 
IServiceCollection;; %
ConfigureWorkers;;& 6
(;;6 7
this;;7 ;
IServiceCollection;;< N
services;;O W
);;W X
{<< 
services== 
.== 
AddSingleton== 
(== 
typeof== $
(==$ %
IChannelService==% 4
<==4 5
StateFromFileDto==5 E
>==E F
)==F G
,==G H
typeof==I O
(==O P
ChannelService==P ^
<==^ _
StateFromFileDto==_ o
>==o p
)==p q
)==q r
;==r s
services>> 
.>> 
AddSingleton>> 
(>> 
typeof>> $
(>>$ %
IChannelService>>% 4
<>>4 5
CityFromFileDto>>5 D
>>>D E
)>>E F
,>>F G
typeof>>H N
(>>N O
ChannelService>>O ]
<>>] ^
CityFromFileDto>>^ m
>>>m n
)>>n o
)>>o p
;>>p q
return@@ 
services@@ 
;@@ 
}AA 
}BB »
aC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Context\DatabaseContext.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #
Context# *
;* +
[ #
ExcludeFromCodeCoverage 
] 
public 
class 
DatabaseContext 
: 
	DbContext (
,( )
IContext* 2
{		 
public

 

DatabaseContext

 
(

 
DbContextOptions

 +
<

+ ,
DatabaseContext

, ;
>

; <
options

= D
,

D E
bool

F J
applyMigration

K Y
=

Z [
false

\ a
)

a b
:

c d
base

e i
(

i j
options

j q
)

q r
{ 
if 

( 
applyMigration 
&& 
Database &
.& '
IsRelational' 3
(3 4
)4 5
)5 6
Database 
. 
Migrate 
( 
) 
; 
Database 
. 
EnsureCreated 
( 
)  
;  !
} 
public 

DatabaseContext 
( 
) 
: 
base #
(# $
)$ %
{ 
} 
	protected 
override 
void 
OnModelCreating +
(+ ,
ModelBuilder, 8
modelBuilder9 E
)E F
{ 
modelBuilder 
. +
ApplyConfigurationsFromAssembly 4
(4 5
typeof5 ;
(; < 
IEFMappingEntrypoint< P
)P Q
.Q R
AssemblyR Z
)Z [
;[ \
base 
. 
OnModelCreating 
( 
modelBuilder )
)) *
;* +
} 
public 

virtual 
async 
Task 
< 
bool "
>" #
CommitAsync$ /
(/ 0
CancellationToken0 A
cancellationTokenB S
=T U
defaultV ]
)] ^
=>_ a
await 
SaveChangesAsync 
( 
cancellationToken 0
)0 1
>2 3
$num4 5
;5 6
}   ≈
ZC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Context\IContext.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #
Context# *
;* +
public 
	interface 
IContext 
{ 
Task 
< 	
bool	 
> 
CommitAsync 
( 
CancellationToken ,
cancellationToken- >
=? @
defaultA H
)H I
;I J
} Ñ
jC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Mapping\Base\EFMappingEntrypoint.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #
Mapping# *
.* +
Base+ /
;/ 0
public 
abstract 
class 
EFMappingEntrypoint )
<) *
T* +
>+ ,
:- .$
IEntityTypeConfiguration/ G
<G H
TH I
>I J
whereK P
TQ R
:S T

BaseEntityU _
{ 
public		 

void		 
	Configure		 
(		 
EntityTypeBuilder		 +
<		+ ,
T		, -
>		- .
builder		/ 6
)		6 7
{

 
builder 
. 
HasKey 
( 
x 
=> 
x 
. 
Id  
)  !
;! "
builder 
. 
Property 
( 
c 
=> 
c 
.  
	CreatedAt  )
)) *
.* +

IsRequired+ 5
(5 6
)6 7
;7 8
builder 
. 
Property 
( 
c 
=> 
c 
.  
	UpdatedAt  )
)) *
.* +

IsRequired+ 5
(5 6
false6 ;
); <
;< =
} 
public 

abstract 
void 
BuildMapping %
(% &
EntityTypeBuilder& 7
<7 8
T8 9
>9 :
builder; B
)B C
;C D
} ô
kC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Mapping\Base\IEFMappingEntrypoint.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #
Mapping# *
.* +
Base+ /
;/ 0
public 
	interface  
IEFMappingEntrypoint %
{ 
} Ù
]C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Mapping\CityMapping.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #
Mapping# *
;* +
public 
class 
CityMapping 
: 
EFMappingEntrypoint .
<. /
City/ 3
>3 4
,4 5 
IEFMappingEntrypoint6 J
{		 
public

 

override

 
void

 
BuildMapping

 %
(

% &
EntityTypeBuilder

& 7
<

7 8
City

8 <
>

< =
builder

> E
)

E F
{ 
builder 
. 
Property 
( 
c 
=> 
c 
.  
Code  $
)$ %
.% &

IsRequired& 0
(0 1
)1 2
;2 3
builder 
. 
Property 
( 
c 
=> 
c 
.  
Name  $
)$ %
.% &

IsRequired& 0
(0 1
)1 2
;2 3
builder 
. 
HasIndex 
( 
c 
=> 
c 
.  
Code  $
)$ %
. 
IsUnique 
( 
) 
; 
builder 
. 
HasOne 
( 
c 
=> 
c 
. 
State  
)  !
. 
WithMany 
( 
) 
. 
HasForeignKey 
( 
c 
=> 
c  !
.! "
StateId" )
)) *
. 
HasPrincipalKey 
( 
c 
=> !
c" #
.# $
Id$ &
)& '
;' (
builder 
. 
ToTable 
( 
$str  
)  !
;! "
} 
} ‰
^C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Mapping\StateMapping.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #
Mapping# *
;* +
public 
class 
StateMapping 
: 
EFMappingEntrypoint /
</ 0
State0 5
>5 6
,6 7 
IEFMappingEntrypoint8 L
{		 
public

 

override

 
void

 
BuildMapping

 %
(

% &
EntityTypeBuilder

& 7
<

7 8
State

8 =
>

= >
builder

? F
)

F G
{ 
builder 
. 
Property 
( 
c 
=> 
c 
.  
Code  $
)$ %
.% &

IsRequired& 0
(0 1
)1 2
;2 3
builder 
. 
Property 
( 
c 
=> 
c 
.  
Name  $
)$ %
.% &

IsRequired& 0
(0 1
)1 2
;2 3
builder 
. 
Property 
( 
c 
=> 
c 
.  
Acronym  '
)' (
.( )
HasColumnType) 6
(6 7
$str7 E
)E F
.F G

IsRequiredG Q
(Q R
)R S
;S T
builder 
. 
HasIndex 
( 
c 
=> 
c 
.  
Code  $
)$ %
. 
IsUnique 
( 
) 
; 
builder 
. 
HasIndex 
( 
c 
=> 
c 
.  
Acronym  '
)' (
. 
IsUnique 
( 
) 
; 
builder 
. 
HasMany 
( 
c 
=> 
c 
. 
Cities %
)% &
. 
WithOne 
( 
) 
. 

IsRequired 
( 
) 
. 
OnDelete 
( 
DeleteBehavior (
.( )
Cascade) 0
)0 1
;1 2
builder 
. 
ToTable 
( 
$str  
)  !
;! "
} 
} –
]C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Mapping\UserMapping.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #
Mapping# *
;* +
public 
class 
UserMapping 
: 
EFMappingEntrypoint .
<. /
User/ 3
>3 4
,4 5 
IEFMappingEntrypoint6 J
{		 
public

 

override

 
void

 
BuildMapping

 %
(

% &
EntityTypeBuilder

& 7
<

7 8
User

8 <
>

< =
builder

> E
)

E F
{ 
builder 
. 
Property 
( 
c 
=> 
c 
.  
Email  %
)% &
.& '

IsRequired' 1
(1 2
)2 3
;3 4
builder 
. 
Property 
( 
c 
=> 
c 
.  
Name  $
)$ %
.% &

IsRequired& 0
(0 1
)1 2
;2 3
builder 
. 
Property 
( 
c 
=> 
c 
.  
Password  (
)( )
.) *

IsRequired* 4
(4 5
)5 6
;6 7
builder 
. 
Property 
( 
c 
=> 
c 
.  
IsAdmin  '
)' (
.( )

IsRequired) 3
(3 4
)4 5
;5 6
builder 
. 
ToTable 
( 
$str 
)  
;  !
} 
} öR
kC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\MemoryCache\CachedStateRepository.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #
MemoryCache# .
;. /
public		 
class		 !
CachedStateRepository		 "
:		# $
IStateRepository		% 5
{

 
private 
readonly 
IStateRepository %

_decorated& 0
;0 1
private 
readonly 
IMemoryCache !
_memoryCache" .
;. /
private 
readonly #
MemoryCacheEntryOptions ,
options- 4
;4 5
public 
!
CachedStateRepository  
(  !
IStateRepository! 1
	decorated2 ;
,; <
IMemoryCache= I
memoryCacheJ U
)U V
{ 
options 
= 
new 
( 
) 
{ 	
AbsoluteExpiration 
=  
DateTime! )
.) *
UtcNow* 0
.0 1

AddMinutes1 ;
(; <
$num< >
)> ?
} 	
;	 


_decorated 
= 
	decorated 
; 
_memoryCache 
= 
memoryCache "
;" #
} 
public 

async 
Task 
Add 
( 
State 
entity  &
,& '
CancellationToken( 9
cancellationToken: K
=L M
defaultN U
)U V
{ 
await 

_decorated 
. 
Add 
( 
entity #
,# $
cancellationToken% 6
)6 7
;7 8
_memoryCache 
. 
Set 
( 
$" 
{ 
CacheKeyConstants -
.- .

StateCache. 8
}8 9
$str9 :
{: ;
entity; A
.A B
IdB D
}D E
"E F
,F G
entityH N
,N O
optionsP W
)W X
;X Y
_memoryCache   
.   
Set   
(   
$"   
{   
CacheKeyConstants   -
.  - .

StateCache  . 8
}  8 9
$str  9 :
{  : ;
entity  ; A
.  A B
Code  B F
}  F G
"  G H
,  H I
entity  J P
.  P Q
Id  Q S
,  S T
options  U \
)  \ ]
;  ] ^
}!! 
public## 

async## 
Task## 
<## 
	PagedList## 
<##  
State##  %
>##% &
>##& '
Get##( +
(##+ ,

Expression##, 6
<##6 7
Func##7 ;
<##; <
State##< A
,##A B
bool##C G
>##G H
>##H I
?##I J

expression##K U
=##V W
null##X \
,##\ ]
int##^ a
page##b f
=##g h
$num##i j
,##j k
int##l o
size##p t
=##u v
$num##w x
,##x y
CancellationToken	##z ã
cancellationToken
##å ù
=
##û ü
default
##† ß
)
##ß ®
=>
##© ´
await$$ 

_decorated$$ 
.$$ 
Get$$ 
($$ 

expression$$ '
,$$' (
page$$) -
,$$- .
size$$/ 3
,$$3 4
cancellationToken$$5 F
)$$F G
;$$G H
public&& 

async&& 
Task&& 
<&& 

IQueryable&&  
<&&  !
State&&! &
>&&& '
>&&' (
GetAll&&) /
(&&/ 0

Expression&&0 :
<&&: ;
Func&&; ?
<&&? @
State&&@ E
,&&E F
bool&&G K
>&&K L
>&&L M
?&&M N

expression&&O Y
=&&Z [
null&&\ `
,&&` a
int&&b e
page&&f j
=&&k l
$num&&m n
,&&n o
int&&p s
size&&t x
=&&y z
$num&&{ |
,&&| }
CancellationToken	&&~ è
cancellationToken
&&ê °
=
&&¢ £
default
&&§ ´
)
&&´ ¨
=>
&&≠ Ø
await'' 

_decorated'' 
.'' 
GetAll'' 
(''  

expression''  *
,''* +
page'', 0
,''0 1
size''2 6
,''6 7
cancellationToken''8 I
)''I J
;''J K
public)) 

async)) 
Task)) 
<)) 
Guid)) 
?)) 
>)) 
GetIdByCode)) (
())( )
int))) ,
code))- 1
,))1 2
CancellationToken))3 D
cancellationToken))E V
=))W X
default))Y `
)))` a
{** 
return++ 
await++ 
_memoryCache++ !
.++! "
GetOrCreateAsync++" 2
(++2 3
$"++3 5
{++5 6
CacheKeyConstants++6 G
.++G H

StateCache++H R
}++R S
$str++S T
{++T U
code++U Y
}++Y Z
"++Z [
,++[ \
async++] b
c++c d
=>++e g
{,, 	
var-- 
result-- 
=-- 
await-- 

_decorated-- )
.--) *
GetIdByCode--* 5
(--5 6
code--6 :
,--: ;
cancellationToken--< M
)--M N
;--N O
c.. 
... 

SetOptions.. 
(.. 
options..  
)..  !
;..! "
return// 
result// 
;// 
}00 	
)00	 

;00
 
}11 
public33 

async33 
Task33 
<33 
State33 
?33 
>33 
GetById33 %
(33% &
Guid33& *
id33+ -
,33- .
CancellationToken33/ @
cancellationToken33A R
=33S T
default33U \
)33\ ]
{44 
return55 
await55 
_memoryCache55 !
.55! "
GetOrCreateAsync55" 2
(552 3
$"553 5
{555 6
CacheKeyConstants556 G
.55G H

StateCache55H R
}55R S
$str55S T
{55T U
id55U W
}55W X
"55X Y
,55Y Z
async55[ `
c55a b
=>55c e
{66 	
var77 
result77 
=77 
await77 

_decorated77 )
.77) *
GetById77* 1
(771 2
id772 4
,774 5
cancellationToken776 G
)77G H
;77H I
if99 
(99 
result99 
!=99 
null99 
)99 
_memoryCache:: 
.:: 
Set::  
(::  !
$"::! #
{::# $
CacheKeyConstants::$ 5
.::5 6

StateCache::6 @
}::@ A
$str::A B
{::B C
result::C I
.::I J
Code::J N
}::N O
"::O P
,::P Q
id::R T
,::T U
options::V ]
)::] ^
;::^ _
c<< 
.<< 

SetOptions<< 
(<< 
options<<  
)<<  !
;<<! "
return>> 
result>> 
;>> 
}?? 	
)??	 

;??
 
}@@ 
publicBB 

TaskBB 
<BB 
StateBB 
?BB 
>BB 
GetByIdWithCitiesBB )
(BB) *
GuidBB* .
idBB/ 1
,BB1 2
CancellationTokenBB3 D
cancellationTokenBBE V
=BBW X
defaultBBY `
)BB` a
=>BBb d

_decoratedCC 
.CC 
GetByIdWithCitiesCC $
(CC$ %
idCC% '
,CC' (
cancellationTokenCC) :
)CC: ;
;CC; <
publicEE 

asyncEE 
TaskEE 
RemoveEE 
(EE 
StateEE "
entityEE# )
,EE) *
CancellationTokenEE+ <
cancellationTokenEE= N
=EEO P
defaultEEQ X
)EEX Y
{FF 
awaitGG 

_decoratedGG 
.GG 
RemoveGG 
(GG  
entityGG  &
,GG& '
cancellationTokenGG( 9
)GG9 :
;GG: ;
_memoryCacheII 
.II 
RemoveII 
(II 
$"II 
{II 
CacheKeyConstantsII 0
.II0 1

StateCacheII1 ;
}II; <
$strII< =
{II= >
entityII> D
.IID E
IdIIE G
}IIG H
"IIH I
)III J
;IIJ K
_memoryCacheJJ 
.JJ 
RemoveJJ 
(JJ 
$"JJ 
{JJ 
CacheKeyConstantsJJ 0
.JJ0 1

StateCacheJJ1 ;
}JJ; <
$strJJ< =
{JJ= >
entityJJ> D
.JJD E
CodeJJE I
}JJI J
"JJJ K
)JJK L
;JJL M
}KK 
publicMM 

asyncMM 
TaskMM 
UpdateMM 
(MM 
StateMM "
entityMM# )
)MM) *
{NN 
awaitOO 

_decoratedOO 
.OO 
UpdateOO 
(OO  
entityOO  &
)OO& '
;OO' (
_memoryCacheQQ 
.QQ 
SetQQ 
(QQ 
$"QQ 
{QQ 
CacheKeyConstantsQQ -
.QQ- .

StateCacheQQ. 8
}QQ8 9
$strQQ9 :
{QQ: ;
entityQQ; A
.QQA B
IdQQB D
}QQD E
"QQE F
,QQF G
entityQQH N
,QQN O
optionsQQP W
)QQW X
;QQX Y
_memoryCacheRR 
.RR 
SetRR 
(RR 
$"RR 
{RR 
CacheKeyConstantsRR -
.RR- .

StateCacheRR. 8
}RR8 9
$strRR9 :
{RR: ;
entityRR; A
.RRA B
CodeRRB F
}RRF G
"RRG H
,RRH I
entityRRJ P
.RRP Q
IdRRQ S
,RRS T
optionsRRU \
)RR\ ]
;RR] ^
}SS 
}TT ‚
cC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Repository\CityRepository.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #

Repository# -
;- .
public 
class 
CityRepository 
: 

Repository (
<( )
City) -
>- .
,. /
ICityRepository0 ?
{ 
public		 

CityRepository		 
(		 
DatabaseContext		 )
	dbContext		* 3
)		3 4
:		5 6
base		7 ;
(		; <
	dbContext		< E
)		E F
{

 
} 
} ù@
_C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Repository\Repository.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #

Repository# -
;- .
public		 
class		 

Repository		 
<		 
T		 
>		 
:		 
IRepository		 (
<		( )
T		) *
>		* +
where		, 1
T		2 3
:		4 5

BaseEntity		6 @
{

 
	protected 
readonly 
DatabaseContext &

_dbContext' 1
;1 2
	protected 
readonly 
DbSet 
< 
T 
> 
_dbSet  &
;& '
public 


Repository 
( 
DatabaseContext %
	dbContext& /
)/ 0
{ 

_dbContext 
= 
	dbContext 
; 
_dbSet 
= 
	dbContext 
. 
Set 
< 
T  
>  !
(! "
)" #
;# $
} 
public 

async 
Task 
Add 
( 
T 
entity "
," #
CancellationToken$ 5
cancellationToken6 G
=H I
defaultJ Q
)Q R
{ 
entity 
. 
SetCreatedAt 
( 
DateTimeOffset *
.* +
Now+ .
). /
;/ 0
await 
_dbSet 
. 
AddAsync 
( 
entity $
,$ %
cancellationToken& 7
)7 8
;8 9
} 
public 

async 
Task 
< 
	PagedList 
<  
T  !
>! "
>" #
Get$ '
(' (

Expression( 2
<2 3
Func3 7
<7 8
T8 9
,9 :
bool; ?
>? @
>@ A
?A B

expressionC M
=N O
nullP T
,T U
intV Y
pageZ ^
=_ `
$numa b
,b c
intd g
sizeh l
=m n
$numo p
,p q
CancellationToken	r É
cancellationToken
Ñ ï
=
ñ ó
default
ò ü
)
ü †
{ 
var 
query 
= 
_dbSet 
. 
AsNoTracking '
(' (
)( )
;) *
var 
count 
= 
await 
query 
.  

CountAsync  *
(* +
cancellationToken+ <
)< =
;= >
if   

(   

expression   
!=   
null   
)   
query!! 
=!! 
query!! 
.!! 
Where!! 
(!!  

expression!!  *
)!!* +
;!!+ ,
if## 

(## 
page## 
!=## 
default## 
&&## 
size## #
!=##$ &
default##' .
)##. /
query$$ 
=$$ 
query$$ 
.$$ 
Skip$$ 
($$ 
Math$$ #
.$$# $
Abs$$$ '
($$' (
($$( )
page$$) -
-$$. /
$num$$0 1
)$$1 2
*$$3 4
page$$5 9
)$$9 :
)$$: ;
.$$; <
Take$$< @
($$@ A
size$$A E
)$$E F
;$$F G
var&& 
items&& 
=&& 
await&& 
query&& 
.&&  
ToListAsync&&  +
(&&+ ,
cancellationToken&&, =
)&&= >
;&&> ?
return(( 
new(( 
	PagedList(( 
<(( 
T(( 
>(( 
(((  
items((  %
,((% &
count((' ,
,((, -
page((. 2
,((2 3
size((4 8
)((8 9
;((9 :
})) 
public++ 

Task++ 
<++ 

IQueryable++ 
<++ 
T++ 
>++ 
>++ 
GetAll++ %
(++% &

Expression++& 0
<++0 1
Func++1 5
<++5 6
T++6 7
,++7 8
bool++9 =
>++= >
>++> ?
?++? @

expression++A K
=++L M
null++N R
,++R S
int++T W
page++X \
=++] ^
$num++_ `
,++` a
int++b e
size++f j
=++k l
$num++m n
,++n o
CancellationToken	++p Å
cancellationToken
++Ç ì
=
++î ï
default
++ñ ù
)
++ù û
{,, 
var-- 
query-- 
=-- 
_dbSet-- 
.-- 
AsNoTracking-- '
(--' (
)--( )
;--) *
if// 

(// 

expression// 
!=// 
null// 
)// 
query00 
=00 
query00 
.00 
Where00 
(00  

expression00  *
)00* +
;00+ ,
if22 

(22 
page22 
!=22 
default22 
&&22 
size22 #
!=22$ &
default22' .
)22. /
query33 
=33 
query33 
.33 
Skip33 
(33 
Math33 #
.33# $
Abs33$ '
(33' (
(33( )
page33) -
-33. /
$num330 1
)331 2
*333 4
page335 9
)339 :
)33: ;
.33; <
Take33< @
(33@ A
size33A E
)33E F
;33F G
return55 
Task55 
.55 

FromResult55 
(55 
query55 $
)55$ %
;55% &
}66 
public88 

async88 
Task88 
<88 
T88 
?88 
>88 
GetById88 !
(88! "
Guid88" &
id88' )
,88) *
CancellationToken88+ <
cancellationToken88= N
=88O P
default88Q X
)88X Y
{99 
return:: 
await:: 
_dbSet:: 
.:: 
FirstOrDefaultAsync:: /
(::/ 0
c::0 1
=>::2 4
c::5 6
.::6 7
Id::7 9
==::: <
id::= ?
,::? @
cancellationToken::A R
)::R S
;::S T
};; 
public== 

async== 
Task== 
Remove== 
(== 
T== 
entity== %
,==% &
CancellationToken==' 8
cancellationToken==9 J
===K L
default==M T
)==T U
{>> 
var?? 
result?? 
=?? 
await?? 
_dbSet?? !
.??! "
FirstOrDefaultAsync??" 5
(??5 6
c??6 7
=>??8 :
c??; <
.??< =
Id??= ?
==??@ B
entity??C I
.??I J
Id??J L
,??L M
cancellationToken??N _
)??_ `
;??` a
ifAA 

(AA 
resultAA 
isAA 
notAA 
nullAA 
)AA 
_dbSetBB 
.BB 
RemoveBB 
(BB 
resultBB  
)BB  !
;BB! "
}CC 
publicEE 

TaskEE 
UpdateEE 
(EE 
TEE 
entityEE 
)EE  
{FF 
_dbSetGG 
.GG 
RemoveGG 
(GG 
entityGG 
)GG 
;GG 
entityHH 
.HH 
SetUpdatedAtHH 
(HH 
DateTimeOffsetHH *
.HH* +
NowHH+ .
)HH. /
;HH/ 0
returnJJ 
TaskJJ 
.JJ 
CompletedTaskJJ !
;JJ! "
}KK 
}LL ∏
dC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Repository\StateRepository.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #

Repository# -
;- .
public 
class 
StateRepository 
: 

Repository )
<) *
State* /
>/ 0
,0 1
IStateRepository2 B
{		 
public

 

StateRepository

 
(

 
DatabaseContext

 *
	dbContext

+ 4
)

4 5
:

6 7
base

8 <
(

< =
	dbContext

= F
)

F G
{ 
} 
public 

async 
Task 
< 
Guid 
? 
> 
GetIdByCode (
(( )
int) ,
code- 1
,1 2
CancellationToken3 D
cancellationTokenE V
=W X
defaultY `
)` a
{ 
var 
result 
= 
await 
_dbSet !
.! "
Where" '
(' (
c( )
=>* ,
c- .
.. /
Code/ 3
==4 6
code7 ;
); <
.< =
FirstOrDefaultAsync= P
(P Q
cancellationTokenQ b
)b c
;c d
return 
result 
? 
. 
Id 
; 
} 
public 

async 
Task 
< 
State 
? 
> 
GetByIdWithCities /
(/ 0
Guid0 4
id5 7
,7 8
CancellationToken9 J
cancellationTokenK \
=] ^
default_ f
)f g
=>h j
await 
_dbSet 
. 
AsNoTracking !
(! "
)" #
.# $
Where$ )
() *
c* +
=>, .
c/ 0
.0 1
Id1 3
==4 6
id7 9
)9 :
.: ;
Include; B
(B C
cC D
=>E G
cH I
.I J
CitiesJ P
)P Q
.Q R
FirstOrDefaultAsyncR e
(e f
cancellationTokenf w
)w x
;x y
} ‚
cC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Data\Repository\UserRepository.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Data "
." #

Repository# -
;- .
public 
class 
UserRepository 
: 

Repository (
<( )
User) -
>- .
,. /
IUserRepository0 ?
{ 
public		 

UserRepository		 
(		 
DatabaseContext		 )
	dbContext		* 3
)		3 4
:		5 6
base		7 ;
(		; <
	dbContext		< E
)		E F
{

 
} 
} ıL
fC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Migrations\20231021150854_Initial.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 

Migrations (
{ 
public 

partial 
class 
Initial  
:! "
	Migration# ,
{ 
	protected		 
override		 
void		 
Up		  "
(		" #
MigrationBuilder		# 3
migrationBuilder		4 D
)		D E
{

 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
, 
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
Code 
= 
table  
.  !
Column! '
<' (
int( +
>+ ,
(, -
type- 1
:1 2
$str3 <
,< =
nullable> F
:F G
falseH M
)M N
,N O
Name 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 <
,< =
nullable> F
:F G
falseH M
)M N
,N O
Acronym 
= 
table #
.# $
Column$ *
<* +
string+ 1
>1 2
(2 3
type3 7
:7 8
$str9 ?
,? @
nullableA I
:I J
falseK P
)P Q
,Q R
	CreatedAt 
= 
table  %
.% &
Column& ,
<, -
DateTimeOffset- ;
>; <
(< =
type= A
:A B
$strC I
,I J
nullableK S
:S T
falseU Z
)Z [
,[ \
	UpdatedAt 
= 
table  %
.% &
Column& ,
<, -
DateTimeOffset- ;
>; <
(< =
type= A
:A B
$strC I
,I J
nullableK S
:S T
trueU Y
)Y Z
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% /
,/ 0
x1 2
=>3 5
x6 7
.7 8
Id8 :
): ;
;; <
} 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
, 
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
Name   
=   
table    
.    !
Column  ! '
<  ' (
string  ( .
>  . /
(  / 0
type  0 4
:  4 5
$str  6 <
,  < =
nullable  > F
:  F G
false  H M
)  M N
,  N O
Email!! 
=!! 
table!! !
.!!! "
Column!!" (
<!!( )
string!!) /
>!!/ 0
(!!0 1
type!!1 5
:!!5 6
$str!!7 =
,!!= >
nullable!!? G
:!!G H
false!!I N
)!!N O
,!!O P
Password"" 
="" 
table"" $
.""$ %
Column""% +
<""+ ,
string"", 2
>""2 3
(""3 4
type""4 8
:""8 9
$str"": @
,""@ A
nullable""B J
:""J K
false""L Q
)""Q R
,""R S
IsAdmin## 
=## 
table## #
.### $
Column##$ *
<##* +
bool##+ /
>##/ 0
(##0 1
type##1 5
:##5 6
$str##7 @
,##@ A
nullable##B J
:##J K
false##L Q
)##Q R
,##R S
	CreatedAt$$ 
=$$ 
table$$  %
.$$% &
Column$$& ,
<$$, -
DateTimeOffset$$- ;
>$$; <
($$< =
type$$= A
:$$A B
$str$$C I
,$$I J
nullable$$K S
:$$S T
false$$U Z
)$$Z [
,$$[ \
	UpdatedAt%% 
=%% 
table%%  %
.%%% &
Column%%& ,
<%%, -
DateTimeOffset%%- ;
>%%; <
(%%< =
type%%= A
:%%A B
$str%%C I
,%%I J
nullable%%K S
:%%S T
true%%U Y
)%%Y Z
}&& 
,&& 
constraints'' 
:'' 
table'' "
=>''# %
{(( 
table)) 
.)) 

PrimaryKey)) $
())$ %
$str))% .
,)). /
x))0 1
=>))2 4
x))5 6
.))6 7
Id))7 9
)))9 :
;)): ;
}** 
)** 
;** 
migrationBuilder,, 
.,, 
CreateTable,, (
(,,( )
name-- 
:-- 
$str-- 
,-- 
columns.. 
:.. 
table.. 
=>.. !
new.." %
{// 
Id00 
=00 
table00 
.00 
Column00 %
<00% &
Guid00& *
>00* +
(00+ ,
type00, 0
:000 1
$str002 8
,008 9
nullable00: B
:00B C
false00D I
)00I J
,00J K
Code11 
=11 
table11  
.11  !
Column11! '
<11' (
int11( +
>11+ ,
(11, -
type11- 1
:111 2
$str113 <
,11< =
nullable11> F
:11F G
false11H M
)11M N
,11N O
Name22 
=22 
table22  
.22  !
Column22! '
<22' (
string22( .
>22. /
(22/ 0
type220 4
:224 5
$str226 <
,22< =
nullable22> F
:22F G
false22H M
)22M N
,22N O
StateId33 
=33 
table33 #
.33# $
Column33$ *
<33* +
Guid33+ /
>33/ 0
(330 1
type331 5
:335 6
$str337 =
,33= >
nullable33? G
:33G H
false33I N
)33N O
,33O P
	CreatedAt44 
=44 
table44  %
.44% &
Column44& ,
<44, -
DateTimeOffset44- ;
>44; <
(44< =
type44= A
:44A B
$str44C I
,44I J
nullable44K S
:44S T
false44U Z
)44Z [
,44[ \
	UpdatedAt55 
=55 
table55  %
.55% &
Column55& ,
<55, -
DateTimeOffset55- ;
>55; <
(55< =
type55= A
:55A B
$str55C I
,55I J
nullable55K S
:55S T
true55U Y
)55Y Z
}66 
,66 
constraints77 
:77 
table77 "
=>77# %
{88 
table99 
.99 

PrimaryKey99 $
(99$ %
$str99% .
,99. /
x990 1
=>992 4
x995 6
.996 7
Id997 9
)999 :
;99: ;
table:: 
.:: 

ForeignKey:: $
(::$ %
name;; 
:;; 
$str;; 5
,;;5 6
column<< 
:<< 
x<<  !
=><<" $
x<<% &
.<<& '
StateId<<' .
,<<. /
principalTable== &
:==& '
$str==( /
,==/ 0
principalColumn>> '
:>>' (
$str>>) -
,>>- .
onDelete??  
:??  !
ReferentialAction??" 3
.??3 4
Cascade??4 ;
)??; <
;??< =
}@@ 
)@@ 
;@@ 
migrationBuilderBB 
.BB 
CreateIndexBB (
(BB( )
nameCC 
:CC 
$strCC '
,CC' (
tableDD 
:DD 
$strDD 
,DD 
columnEE 
:EE 
$strEE !
)EE! "
;EE" #
}FF 	
	protectedHH 
overrideHH 
voidHH 
DownHH  $
(HH$ %
MigrationBuilderHH% 5
migrationBuilderHH6 F
)HHF G
{II 	
migrationBuilderJJ 
.JJ 
	DropTableJJ &
(JJ& '
nameKK 
:KK 
$strKK 
)KK 
;KK 
migrationBuilderMM 
.MM 
	DropTableMM &
(MM& '
nameNN 
:NN 
$strNN 
)NN 
;NN 
migrationBuilderPP 
.PP 
	DropTablePP &
(PP& '
nameQQ 
:QQ 
$strQQ 
)QQ 
;QQ 
}RR 	
}SS 
}TT è
bC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Services\CityFromFileServices.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Services &
;& '
public		 
class		  
CityFromFileServices		 !
:		" #
BackgroundService		$ 5
{

 
private 
readonly 
IChannelService $
<$ %
CityFromFileDto% 4
>4 5
_channelService6 E
;E F
private 
readonly  
IServiceScopeFactory )
_scopeFactory* 7
;7 8
public 
 
CityFromFileServices 
(  
IChannelService  /
</ 0
CityFromFileDto0 ?
>? @
channelServiceA O
,O P 
IServiceScopeFactoryQ e
scopeFactoryf r
)r s
{ 
_channelService 
= 
channelService (
;( )
_scopeFactory 
= 
scopeFactory $
;$ %
} 
	protected 
async 
override 
Task !
ExecuteAsync" .
(. /
CancellationToken/ @
stoppingTokenA N
)N O
{ 
while 
( 
! 
stoppingToken 
. #
IsCancellationRequested 5
)5 6
{ 	
var 
reader 
= 
await 
_channelService .
.. /
	GetReader/ 8
(8 9
)9 :
.: ;
	ReadAsync; D
(D E
stoppingTokenE R
)R S
;S T
using 
var 
scope 
= 
_scopeFactory +
.+ ,
CreateScope, 7
(7 8
)8 9
;9 :
var 
service 
= 
scope 
.  
ServiceProvider  /
./ 0

GetService0 :
<: ;
ICityServices; H
>H I
(I J
)J K
;K L
if 
( 
service 
is 
null 
)  
return 
; 
try!! 
{"" 
var## 
result## 
=## 
await## "
service### *
.##* +
AddFromFile##+ 6
(##6 7
reader##7 =
,##= >
stoppingToken##? L
)##L M
;##M N
await$$ 
Retry$$ 
($$ 
reader$$ "
,$$" #
result$$$ *
,$$* +
stoppingToken$$, 9
)$$9 :
;$$: ;
}%% 
catch&& 
{'' 
await(( 
Retry(( 
((( 
reader(( "
,((" #
false(($ )
,(() *
stoppingToken((+ 8
)((8 9
;((9 :
})) 
}** 	
}++ 
public-- 

async-- 
Task-- 
Retry-- 
(-- 
CityFromFileDto-- +
reader--, 2
,--2 3
bool--4 8
result--9 ?
,--? @
CancellationToken--A R
stoppingToken--S `
)--` a
{.. 
if// 

(// 
!// 
result// 
&&// 
reader// 
.// 
CountTry// &
<=//' )
$num//* +
)//+ ,
{00 	
reader11 
.11 
AddTry11 
(11 
)11 
;11 
await22 
_channelService22 !
.22! "
	GetWriter22" +
(22+ ,
)22, -
.22- .

WriteAsync22. 8
(228 9
reader229 ?
,22? @
stoppingToken22A N
)22N O
;22O P
}33 	
}55 
}66 ñ
cC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Services\StateFromFileServices.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Services &
;& '
public		 
class		 !
StateFromFileServices		 "
:		# $
BackgroundService		% 6
{

 
private 
readonly 
IChannelService $
<$ %
StateFromFileDto% 5
>5 6
_channelService7 F
;F G
private 
readonly  
IServiceScopeFactory )
_scopeFactory* 7
;7 8
public 
!
StateFromFileServices  
(  !
IChannelService! 0
<0 1
StateFromFileDto1 A
>A B
channelServiceC Q
,Q R 
IServiceScopeFactoryS g
scopeFactoryh t
)t u
{ 
_channelService 
= 
channelService (
;( )
_scopeFactory 
= 
scopeFactory $
;$ %
} 
	protected 
async 
override 
Task !
ExecuteAsync" .
(. /
CancellationToken/ @
stoppingTokenA N
)N O
{ 
while 
( 
! 
stoppingToken 
. #
IsCancellationRequested 5
)5 6
{ 	
var 
reader 
= 
await 
_channelService .
.. /
	GetReader/ 8
(8 9
)9 :
.: ;
	ReadAsync; D
(D E
stoppingTokenE R
)R S
;S T
using 
var 
scope 
= 
_scopeFactory +
.+ ,
CreateScope, 7
(7 8
)8 9
;9 :
var 
service 
= 
scope 
.  
ServiceProvider  /
./ 0

GetService0 :
<: ;
IStateServices; I
>I J
(J K
)K L
;L M
if 
( 
service 
is 
null 
)  
return 
; 
try   
{!! 
var"" 
result"" 
="" 
await"" "
service""# *
.""* +
AddFromFile""+ 6
(""6 7
reader""7 =
,""= >
stoppingToken""? L
)""L M
;""M N
await## 
Retry## 
(## 
reader## "
,##" #
result##$ *
,##* +
stoppingToken##, 9
)##9 :
;##: ;
}$$ 
catch%% 
{&& 
await'' 
Retry'' 
('' 
reader'' "
,''" #
false''$ )
,'') *
stoppingToken''+ 8
)''8 9
;''9 :
}(( 
})) 	
}** 
public++ 

async++ 
Task++ 
Retry++ 
(++ 
StateFromFileDto++ ,
reader++- 3
,++3 4
bool++5 9
result++: @
,++@ A
CancellationToken++B S
stoppingToken++T a
)++a b
{,, 
if-- 

(-- 
!-- 
result-- 
&&-- 
reader-- 
.-- 
CountTry-- &
<=--' )
$num--* +
)--+ ,
{.. 	
reader// 
.// 
AddTry// 
(// 
)// 
;// 
await00 
_channelService00 !
.00! "
	GetWriter00" +
(00+ ,
)00, -
.00- .

WriteAsync00. 8
(008 9
reader009 ?
,00? @
stoppingToken00A N
)00N O
;00O P
}11 	
}33 
}44 ≥
TC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\UoW\IUnitOfWork.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
UoW !
;! "
public 
	interface 
IUnitOfWork 
{ 
} ú
ZC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Worker\ChannelService.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Worker $
;$ %
public 
class 
ChannelService 
< 
T 
> 
:  
IChannelService! 0
<0 1
T1 2
>2 3
{ 
private 
readonly 
Channel 
< 
T 
> 
_channel  (
;( )
public		 

ChannelService		 
(		 
)		 
{

 
var 
options 
= 
new #
UnboundedChannelOptions 1
(1 2
)2 3
{ 	
SingleReader 
= 
false  
} 	
;	 

_channel 
= 
Channel 
. 
CreateUnbounded *
<* +
T+ ,
>, -
(- .
options. 5
)5 6
;6 7
} 
public 

ChannelReader 
< 
T 
> 
	GetReader %
(% &
)& '
{ 
return 
_channel 
. 
Reader 
; 
} 
public 

ChannelWriter 
< 
T 
> 
	GetWriter %
(% &
)& '
{ 
return 
_channel 
. 
Writer 
; 
} 
} Ü
[C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Infrastructure\Worker\IChannelService.cs
	namespace 	
Ibge
 
. 
Infrastructure 
. 
Worker $
;$ %
public 
	interface 
IChannelService  
<  !
T! "
>" #
{ 
ChannelWriter 
< 
T 
> 
	GetWriter 
( 
)  
;  !
ChannelReader 
< 
T 
> 
	GetReader 
( 
)  
;  !
}		 