£
UC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Adapter\CityAdapter.cs
	namespace 	
Ibge
 
. 
Application 
. 
Adapter "
;" #
public 
static 
class 
CityAdapter 
{ 
public		 

static		 
City		 
Create		 
(		 
CreateCityCommand		 /
param		0 5
)		5 6
=>		7 9
new

 
(

 
param

 
.

 
Code

 
,

 
param

 
.

 
Name

 "
,

" #
param

$ )
.

) *
StateId

* 1
)

1 2
;

2 3
public 

static 
CityResponseDto !
?! "

FromDomain# -
(- .
City. 2
?2 3
param4 9
)9 :
=>; =
param 
== 
null 
? 
null 
: 
new "
(" #
)# $
{ 	
Code 
= 
param 
. 
Code 
, 
	CreatedAt 
= 
param 
. 
	CreatedAt '
,' (
Id 
= 
param 
. 
Id 
, 
Name 
= 
param 
. 
Name 
, 
StateId 
= 
param 
. 
StateId #
,# $
	UpdatedAt 
= 
param 
. 
	UpdatedAt '
} 	
;	 

public 

static 
City 
Update 
( 
UpdateCityCommand /
param0 5
)5 6
=>7 9
new 
( 
param 
. 
Id 
, 
param 
. 
Code  
,  !
param" '
.' (
Name( ,
,, -
param. 3
.3 4
StateId4 ;
); <
;< =
public 

static 
CreateCityCommand #
FromFile$ ,
(, -
CityFromFileDto- <
param= B
,B C
GuidD H
StateIdI P
)P Q
=>R T
new 
( 
) 
{ 	
Code 
= 
param 
. 
Code 
, 
Name 
= 
param 
. 
Name 
, 
StateId 
= 
StateId 
, 
}   	
;  	 

}!! »
VC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Adapter\StateAdapter.cs
	namespace 	
Ibge
 
. 
Application 
. 
Adapter "
;" #
public 
static 
class 
StateAdapter  
{ 
public		 

static		 
State		 
Create		 
(		 
CreateStateCommand		 1
param		2 7
)		7 8
=>		9 ;
new

 
(

 
param

 
.

 
Code

 
,

 
param

 
.

 
Name

 "
,

" #
param

$ )
.

) *
Acronym

* 1
)

1 2
;

2 3
public 

static 
StateResponseDto "
?" #

FromDomain$ .
(. /
State/ 4
?4 5
param6 ;
); <
=>= ?
param@ E
isF H
nullI M
?N O
nullP T
:U V
new 
StateResponseDto 
( 
) 
{ 	
Acronym 
= 
param 
. 
Acronym #
,# $
Code 
= 
param 
. 
Code 
, 
Name 
= 
param 
. 
Name 
, 
	CreatedAt 
= 
param 
. 
	CreatedAt '
,' (
Id 
= 
param 
. 
Id 
, 
	UpdatedAt 
= 
param 
. 
	UpdatedAt '
} 	
;	 

public 

static 
State 
Update 
( 
UpdateStateCommand 1
param2 7
)7 8
=>9 ;
new 
( 
param 
. 
Id 
, 
param 
. 
Code  
,  !
param" '
.' (
Name( ,
,, -
param. 3
.3 4
Acronym4 ;
); <
;< =
public 

static 
CreateStateCommand $
FromFile% -
(- .
StateFromFileDto. >
param? D
)D E
=>F H
new 
CreateStateCommand 
( 
)  
{ 	
Acronym 
= 
param 
. 
Acronym #
,# $
Code 
= 
param 
. 
Code 
, 
Name 
= 
param 
. 
Name 
}   	
;  	 

}!! „
UC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Adapter\UserAdapter.cs
	namespace 	
Ibge
 
. 
Domain 
. 
Adapter 
; 
public 
static 
class 
UserAdapter 
{ 
public 

static 
User 
CreateNewUser $
($ %
CreateUserCommand% 6
user7 ;
); <
=>= ?
new		 
User		 
(		 
user		 
.		 
Name		 
,		 
user		  
.		  !
Email		! &
,		& '
BCrypt		( .
.		. /
Net		/ 2
.		2 3
BCrypt		3 9
.		9 :
HashPassword		: F
(		F G
user		G K
.		K L
Password		L T
)		T U
,		U V
user		W [
.		[ \
IsAdmin		\ c
)		c d
;		d e
}

 Õ
ZC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Configuration\JwtOptions.cs
	namespace 	
Ibge
 
. 
Application 
. 
Configuration (
;( )
public 
class 

JwtOptions 
{ 
public 

string 
Key 
{ 
get 
; 
set  
;  !
}" #
=$ %
string& ,
., -
Empty- 2
;2 3
public 

int "
TimeToExpiresInMinutes %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} √>
KC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Configure.cs
	namespace 	
Ibge
 
. 
Application 
; 
public 
static 
class 
	Configure 
{ 
public 

static 
IServiceCollection $ 
ConfigureApplication% 9
(9 :
this: >
IServiceCollection? Q
servicesR Z
,Z [
IConfiguration\ j
configurationk x
)x y
{ 
services 
. 
	AddScoped 
<  
GenerateTokenUseCase /
>/ 0
(0 1
)1 2
;2 3
services 
. 
	Configure 
< 

JwtOptions %
>% &
(& '
configuration' 4
.4 5

GetSection5 ?
(? @
$str@ L
)L M
)M N
;N O
services 
. 

AddMediatR 
( 
cfg 
=>  "
cfg# &
.& '(
RegisterServicesFromAssembly' C
(C D
AssemblyD L
.L M 
GetExecutingAssemblyM a
(a b
)b c
)c d
)d e
;e f
services 
. 
ConfigureValidators $
($ %
)% &
.   
ConfigureHandlers   "
(  " #
)  # $
.!! 
ConfigureServices!! "
(!!" #
)!!# $
."" 
AddMemoryCache"" 
(""  
)""  !
;""! "
return$$ 
services$$ 
;$$ 
}%% 
public'' 

static'' 
IServiceCollection'' $
ConfigureValidators''% 8
(''8 9
this''9 =
IServiceCollection''> P
services''Q Y
)''Y Z
{(( 
services)) 
.)) 
	AddScoped)) 
<)) 

IValidator)) %
<))% &
CreateUserCommand))& 7
>))7 8
,))8 9
CreateUserValidator)): M
>))M N
())N O
)))O P
;))P Q
services** 
.** 
	AddScoped** 
<** 

IValidator** %
<**% &
AuthUserCommand**& 5
>**5 6
,**6 7
AuthUserValidator**8 I
>**I J
(**J K
)**K L
;**L M
services,, 
.,, 
	AddScoped,, 
<,, 

IValidator,, %
<,,% &
CreateStateCommand,,& 8
>,,8 9
,,,9 : 
CreateStateValidator,,; O
>,,O P
(,,P Q
),,Q R
;,,R S
services-- 
.-- 
	AddScoped-- 
<-- 

IValidator-- %
<--% &
UpdateStateCommand--& 8
>--8 9
,--9 : 
UpdateStateValidator--; O
>--O P
(--P Q
)--Q R
;--R S
services.. 
... 
	AddScoped.. 
<.. 

IValidator.. %
<..% &
RemoveStateCommand..& 8
>..8 9
,..9 : 
RemoveStateValidator..; O
>..O P
(..P Q
)..Q R
;..R S
services00 
.00 
	AddScoped00 
<00 

IValidator00 %
<00% &
CreateCityCommand00& 7
>007 8
,008 9
CreateCityValidator00: M
>00M N
(00N O
)00O P
;00P Q
services11 
.11 
	AddScoped11 
<11 

IValidator11 %
<11% &
UpdateCityCommand11& 7
>117 8
,118 9
UpdateCityValidator11: M
>11M N
(11N O
)11O P
;11P Q
return33 
services33 
;33 
}44 
public66 

static66 
IServiceCollection66 $
ConfigureHandlers66% 6
(666 7
this667 ;
IServiceCollection66< N
services66O W
)66W X
{77 
services88 
.88 
	AddScoped88 
<88 
IRequestHandler88 *
<88* +
CreateUserCommand88+ <
,88< =
Result88> D
<88D E
Guid88E I
>88I J
>88J K
,88K L
UserCommandHandler88M _
>88_ `
(88` a
)88a b
;88b c
services99 
.99 
	AddScoped99 
<99 
IRequestHandler99 *
<99* +
AuthUserCommand99+ :
,99: ;
Result99< B
<99B C
TokenResponse99C P
>99P Q
>99Q R
,99R S
UserCommandHandler99T f
>99f g
(99g h
)99h i
;99i j
services;; 
.;; 
	AddScoped;; 
<;; 
IRequestHandler;; *
<;;* +
CreateStateCommand;;+ =
,;;= >
Result;;? E
<;;E F
Guid;;F J
>;;J K
>;;K L
,;;L M
StateCommandHandler;;N a
>;;a b
(;;b c
);;c d
;;;d e
services<< 
.<< 
	AddScoped<< 
<<< 
IRequestHandler<< *
<<<* +
UpdateStateCommand<<+ =
,<<= >
Result<<? E
><<E F
,<<F G
StateCommandHandler<<H [
><<[ \
(<<\ ]
)<<] ^
;<<^ _
services== 
.== 
	AddScoped== 
<== 
IRequestHandler== *
<==* +
RemoveStateCommand==+ =
,=== >
Result==? E
>==E F
,==F G
StateCommandHandler==H [
>==[ \
(==\ ]
)==] ^
;==^ _
services?? 
.?? 
	AddScoped?? 
<?? 
IRequestHandler?? *
<??* +
CreateCityCommand??+ <
,??< =
Result??> D
<??D E
Guid??E I
>??I J
>??J K
,??K L
CityCommandHandler??M _
>??_ `
(??` a
)??a b
;??b c
services@@ 
.@@ 
	AddScoped@@ 
<@@ 
IRequestHandler@@ *
<@@* +
UpdateCityCommand@@+ <
,@@< =
Result@@> D
>@@D E
,@@E F
CityCommandHandler@@G Y
>@@Y Z
(@@Z [
)@@[ \
;@@\ ]
servicesAA 
.AA 
	AddScopedAA 
<AA 
IRequestHandlerAA *
<AA* +
RemoveCityCommandAA+ <
,AA< =
ResultAA> D
>AAD E
,AAE F
CityCommandHandlerAAG Y
>AAY Z
(AAZ [
)AA[ \
;AA\ ]
returnCC 
servicesCC 
;CC 
}DD 
publicFF 

staticFF 
IServiceCollectionFF $
ConfigureServicesFF% 6
(FF6 7
thisFF7 ;
IServiceCollectionFF< N
servicesFFO W
)FFW X
{GG 
servicesHH 
.HH 
	AddScopedHH 
<HH 
IStateServicesHH )
,HH) *
StateServicesHH+ 8
>HH8 9
(HH9 :
)HH: ;
;HH; <
servicesII 
.II 
	AddScopedII 
<II 
ICityServicesII (
,II( )
CityServicesII* 6
>II6 7
(II7 8
)II8 9
;II9 :
servicesJJ 
.JJ 
	AddScopedJJ 
<JJ 
IImportServicesJJ *
,JJ* +
ImportServicesJJ, :
>JJ: ;
(JJ; <
)JJ< =
;JJ= >
servicesKK 
.KK 
	AddScopedKK 
<KK 
IUserServicesKK (
,KK( )
UserServicesKK* 6
>KK6 7
(KK7 8
)KK8 9
;KK9 :
returnMM 
servicesMM 
;MM 
}NN 
}OO ¶
fC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Extensions\FluentValidationExtension.cs
	namespace 	
Ibge
 
. 
Application 
. 

Extensions %
;% &
public 
static 
class %
FluentValidationExtension -
<- .
T. /
,/ 0
Instance1 9
>9 :
where; @
TA B
:C D

IValidatorE O
<O P
InstanceP X
>X Y
whereZ _
Instance` h
:i j
classk p
{ 
public		 

async		 
static		 
Task		 
<		 
List		 !
<		! "
ValidationError		" 1
>		1 2
>		2 3
GetValidationErrors		4 G
(		G H
Instance		H P
request		Q X
,		X Y
CancellationToken		Z k
cancellationToken		l }
)		} ~
{

 
var 
	validator 
= 
	Activator !
.! "
CreateInstance" 0
<0 1
T1 2
>2 3
(3 4
)4 5
;5 6
var 

validation 
= 
await 
	validator (
.( )
ValidateAsync) 6
(6 7
request7 >
,> ?
cancellationToken@ Q
)Q R
;R S
if 

( 
! 

validation 
. 
IsValid 
)  
return 

validation 
. 
AsErrors &
(& '
)' (
;( )
return 
new 
( 
) 
; 
} 
} €]
\C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Handler\CityCommandHandler.cs
	namespace 	
Ibge
 
. 
Application 
. 
Handler "
;" #
public 
class 
CityCommandHandler 
:  !
CommandHandler" 0
,0 1
IRequestHandler 
< 
CreateCityCommand %
,% &
Result' -
<- .
Guid. 2
>2 3
>3 4
,4 5
IRequestHandler 
< 
UpdateCityCommand %
,% &
Result' -
>- .
,. /
IRequestHandler 
< 
RemoveCityCommand %
,% &
Result' -
>- .
{ 
private 
readonly 
ICityRepository $
_cityRepository% 4
;4 5
private 
readonly 
IStateRepository %
_stateRepository& 6
;6 7
public 

CityCommandHandler 
( 
DatabaseContext 
context 
,  
ICityRepository 
cityRepository &
,& '
IStateRepository 
stateRepository (
)( )
:* +
base, 0
(0 1
context1 8
)8 9
{ 
_cityRepository 
= 
cityRepository (
;( )
_stateRepository 
= 
stateRepository *
;* +
} 
public 

async 
Task 
< 
Result 
< 
Guid !
>! "
>" #
Handle$ *
(* +
CreateCityCommand+ <
request= D
,D E
CancellationTokenF W
cancellationTokenX i
)i j
{   
var!! 
errors!! 
=!! 
await!! %
FluentValidationExtension!! 4
<!!4 5
CreateCityValidator!!5 H
,!!H I
CreateCityCommand!!J [
>!![ \
.!!\ ]
GetValidationErrors!!] p
(!!p q
request!!q x
,!!x y
cancellationToken	!!z ã
)
!!ã å
;
!!å ç
if## 

(## 
errors## 
.## 
Any## 
(## 
)## 
)## 
return$$ 
Result$$ 
.$$ 
Invalid$$ !
($$! "
errors$$" (
)$$( )
;$$) *
var&& 
state&& 
=&& 
await&& 
_stateRepository&& *
.&&* +
GetById&&+ 2
(&&2 3
request&&3 :
.&&: ;
StateId&&; B
,&&B C
cancellationToken&&D U
)&&U V
;&&V W
if(( 

((( 
state(( 
==(( 
null(( 
)(( 
{)) 	
errors** 
=** 
new** 
List** 
<** 
ValidationError** -
>**- .
{**/ 0
new++ 
ValidationError++ '
{,, 

Identifier-- "
=--# $
$str--% 5
,--5 6
ErrorMessage.. $
=..% &
$str..' 8
}..9 :
}// 
;// 
return11 
Result11 
.11 
Invalid11 !
(11! "
errors11" (
)11( )
;11) *
}22 	
var44 
	existCity44 
=44 
await44 
_cityRepository44 -
.44- .
GetAll44. 4
(444 5
c445 6
=>447 9
c44: ;
.44; <
Code44< @
==44A C
request44D K
.44K L
Code44L P
,44P Q
cancellationToken44R c
:44c d
cancellationToken44e v
)44v w
;44w x
if66 

(66 
	existCity66 
.66 
Any66 
(66 
)66 
)66 
{77 	
errors88 
=88 
new88 
List88 
<88 
ValidationError88 -
>88- .
{88/ 0
new99 
ValidationError99 '
{:: 

Identifier;; "
=;;# $
$str;;% /
,;;/ 0
ErrorMessage<< $
=<<% &
$str<<' V
}<<W X
}== 
;== 
return?? 
Result?? 
.?? 
Invalid?? !
(??! "
errors??" (
)??( )
;??) *
}@@ 	
varBB 
cityBB 
=BB 
CityAdapterBB 
.BB 
CreateBB %
(BB% &
requestBB& -
)BB- .
;BB. /
awaitDD 
_cityRepositoryDD 
.DD 
AddDD !
(DD! "
cityDD" &
,DD& '
cancellationTokenDD( 9
)DD9 :
;DD: ;
awaitFF 
CommitAsyncFF 
(FF 
cancellationTokenFF +
)FF+ ,
;FF, -
returnGG 
ResultGG 
.GG 
SuccessGG 
(GG 
cityGG "
.GG" #
IdGG# %
)GG% &
;GG& '
}HH 
publicJJ 

asyncJJ 
TaskJJ 
<JJ 
ResultJJ 
>JJ 
HandleJJ $
(JJ$ %
UpdateCityCommandJJ% 6
requestJJ7 >
,JJ> ?
CancellationTokenJJ@ Q
cancellationTokenJJR c
)JJc d
{KK 
varLL 
errorsLL 
=LL 
awaitLL %
FluentValidationExtensionLL 4
<LL4 5
UpdateCityValidatorLL5 H
,LLH I
UpdateCityCommandLLJ [
>LL[ \
.LL\ ]
GetValidationErrorsLL] p
(LLp q
requestLLq x
,LLx y
cancellationToken	LLz ã
)
LLã å
;
LLå ç
ifNN 

(NN 
errorsNN 
.NN 
AnyNN 
(NN 
)NN 
)NN 
returnOO 
ResultOO 
.OO 
InvalidOO !
(OO! "
errorsOO" (
)OO( )
;OO) *
varQQ 
stateQQ 
=QQ 
awaitQQ 
_stateRepositoryQQ *
.QQ* +
GetByIdQQ+ 2
(QQ2 3
requestQQ3 :
.QQ: ;
StateIdQQ; B
,QQB C
cancellationTokenQQD U
)QQU V
;QQV W
ifSS 

(SS 
stateSS 
==SS 
nullSS 
)SS 
{TT 	
errorsUU 
=UU 
newUU 
ListUU 
<UU 
ValidationErrorUU -
>UU- .
{UU/ 0
newVV 
ValidationErrorVV '
{WW 

IdentifierXX "
=XX# $
$strXX% 5
,XX5 6
ErrorMessageYY $
=YY% &
$strYY' 8
}YY9 :
}ZZ 
;ZZ 
return\\ 
Result\\ 
.\\ 
Invalid\\ !
(\\! "
errors\\" (
)\\( )
;\\) *
}]] 	
var__ 
exist__ 
=__ 
await__ 
_cityRepository__ )
.__) *
GetById__* 1
(__1 2
request__2 9
.__9 :
Id__: <
,__< =
cancellationToken__> O
)__O P
;__P Q
ifaa 

(aa 
existaa 
==aa 
nullaa 
)aa 
returnbb 
Resultbb 
.bb 
NotFoundbb "
(bb" #
)bb# $
;bb$ %

Expressiondd 
<dd 
Funcdd 
<dd 
Citydd 
,dd 
booldd "
>dd" #
>dd# $

expressiondd% /
=dd0 1
cdd2 3
=>dd4 6
(dd7 8
cdd8 9
.dd9 :
Codedd: >
==dd? A
requestddB I
.ddI J
CodeddJ N
&&ddO Q
cddR S
.ddS T
StateIdddT [
==dd\ ^
requestdd_ f
.ddf g
StateIdddg n
)ddn o
&&ddp r
cdds t
.ddt u
Idddu w
!=ddx z
request	dd{ Ç
.
ddÇ É
Id
ddÉ Ö
;
ddÖ Ü
varff 
conflictff 
=ff 
awaitff 
_cityRepositoryff ,
.ff, -
GetAllff- 3
(ff3 4

expressionff4 >
,ff> ?
cancellationTokenff@ Q
:ffQ R
cancellationTokenffS d
)ffd e
;ffe f
ifhh 

(hh 
conflicthh 
.hh 
Anyhh 
(hh 
)hh 
)hh 
{ii 	
errorsjj 
=jj 
newjj 
Listjj 
<jj 
ValidationErrorjj -
>jj- .
{jj/ 0
newkk 
ValidationErrorkk '
{ll 

Identifiermm "
=mm# $
$strmm% /
,mm/ 0
ErrorMessagenn $
=nn% &
$strnn' V
}nnW X
}oo 
;oo 
returnqq 
Resultqq 
.qq 
Invalidqq !
(qq! "
errorsqq" (
)qq( )
;qq) *
}rr 	
vartt 
citytt 
=tt 
CityAdaptertt 
.tt 
Updatett %
(tt% &
requesttt& -
)tt- .
;tt. /
awaitvv 
_cityRepositoryvv 
.vv 
Updatevv $
(vv$ %
cityvv% )
)vv) *
;vv* +
awaitxx 
CommitAsyncxx 
(xx 
cancellationTokenxx +
)xx+ ,
;xx, -
returnzz 
Resultzz 
.zz 
Successzz 
(zz 
)zz 
;zz  
}{{ 
public}} 

async}} 
Task}} 
<}} 
Result}} 
>}} 
Handle}} $
(}}$ %
RemoveCityCommand}}% 6
request}}7 >
,}}> ?
CancellationToken}}@ Q
cancellationToken}}R c
)}}c d
{~~ 
var 
errors 
= 
await %
FluentValidationExtension 4
<4 5
RemoveCityValidator5 H
,H I
RemoveCityCommandJ [
>[ \
.\ ]
GetValidationErrors] p
(p q
requestq x
,x y
cancellationToken	z ã
)
ã å
;
å ç
if
ÅÅ 

(
ÅÅ 
errors
ÅÅ 
.
ÅÅ 
Any
ÅÅ 
(
ÅÅ 
)
ÅÅ 
)
ÅÅ 
return
ÇÇ 
Result
ÇÇ 
.
ÇÇ 
Invalid
ÇÇ !
(
ÇÇ! "
errors
ÇÇ" (
)
ÇÇ( )
;
ÇÇ) *
var
ÑÑ 
exist
ÑÑ 
=
ÑÑ 
await
ÑÑ 
_cityRepository
ÑÑ )
.
ÑÑ) *
GetById
ÑÑ* 1
(
ÑÑ1 2
request
ÑÑ2 9
.
ÑÑ9 :
Id
ÑÑ: <
,
ÑÑ< =
cancellationToken
ÑÑ> O
)
ÑÑO P
;
ÑÑP Q
if
ÜÜ 

(
ÜÜ 
exist
ÜÜ 
==
ÜÜ 
null
ÜÜ 
)
ÜÜ 
return
áá 
Result
áá 
.
áá 
NotFound
áá "
(
áá" #
)
áá# $
;
áá$ %
await
ââ 
_cityRepository
ââ 
.
ââ 
Remove
ââ $
(
ââ$ %
exist
ââ% *
,
ââ* +
cancellationToken
ââ, =
)
ââ= >
;
ââ> ?
await
ãã 
CommitAsync
ãã 
(
ãã 
cancellationToken
ãã +
)
ãã+ ,
;
ãã, -
return
çç 
Result
çç 
.
çç 
Success
çç 
(
çç 
)
çç 
;
çç  
}
éé 
}èè Í
XC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Handler\CommandHandler.cs
	namespace 	
Ibge
 
. 
Application 
. 
Handler "
;" #
public 
abstract 
class 
CommandHandler $
{ 
private 
readonly 
IContext 
_context &
;& '
	protected		 
CommandHandler		 
(		 
IContext		 %
context		& -
)		- .
{

 
_context 
= 
context 
; 
} 
	protected 
async 
Task 
< 
bool 
> 
CommitAsync *
(* +
CancellationToken+ <
cancellationToken= N
=O P
defaultQ X
)X Y
{ 
return 
await 
_context 
. 
CommitAsync )
() *
cancellationToken* ;
); <
;< =
} 
} ∏V
]C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Handler\StateCommandHandler.cs
	namespace 	
Ibge
 
. 
Application 
. 
Handler "
;" #
public 
class 
StateCommandHandler  
:! "
CommandHandler# 1
,1 2
IRequestHandler 
< 
CreateStateCommand &
,& '
Result( .
<. /
Guid/ 3
>3 4
>4 5
,5 6
IRequestHandler 
< 
UpdateStateCommand &
,& '
Result( .
>. /
,/ 0
IRequestHandler 
< 
RemoveStateCommand &
,& '
Result( .
>. /
{ 
private 
readonly 
IStateRepository %
_repository& 1
;1 2
public 

StateCommandHandler 
( 
DatabaseContext .
context/ 6
,6 7
IStateRepository8 H

repositoryI S
)S T
:U V
baseW [
([ \
context\ c
)c d
{ 
_repository 
= 

repository  
;  !
} 
public 

async 
Task 
< 
Result 
< 
Guid !
>! "
>" #
Handle$ *
(* +
CreateStateCommand+ =
request> E
,E F
CancellationTokenG X
cancellationTokenY j
)j k
{ 
var 
errors 
= 
await %
FluentValidationExtension 4
<4 5 
CreateStateValidator5 I
,I J
CreateStateCommandK ]
>] ^
.^ _
GetValidationErrors_ r
(r s
requests z
,z {
cancellationToken	| ç
)
ç é
;
é è
if 

( 
errors 
. 
Any 
( 
) 
) 
return 
Result 
. 
Invalid !
(! "
errors" (
)( )
;) *

Expression!! 
<!! 
Func!! 
<!! 
State!! 
,!! 
bool!! #
>!!# $
>!!$ %

expression!!& 0
=!!1 2
c!!3 4
=>!!5 7
c!!8 9
.!!9 :
Code!!: >
==!!? A
request!!B I
.!!I J
Code!!J N
||!!O Q
c!!R S
.!!S T
Acronym!!T [
.!![ \
ToLower!!\ c
(!!c d
)!!d e
==!!f h
request!!i p
.!!p q
Acronym!!q x
.!!x y
ToLower	!!y Ä
(
!!Ä Å
)
!!Å Ç
;
!!Ç É
var## 
query## 
=## 
await## 
_repository## %
.##% &
GetAll##& ,
(##, -

expression##- 7
,##7 8
cancellationToken##9 J
:##J K
cancellationToken##L ]
)##] ^
;##^ _
if%% 

(%% 
query%% 
.%% 
Any%% 
(%% 
)%% 
)%% 
{&& 	
errors'' 
='' 
new'' 
List'' 
<'' 
ValidationError'' -
>''- .
{''/ 0
new(( 
ValidationError(( '
{)) 

Identifier** "
=**# $
$str**% /
,**/ 0
ErrorMessage++ $
=++% &
$str++' V
}++W X
},, 
;,, 
return.. 
Result.. 
... 
Invalid.. !
(..! "
errors.." (
)..( )
;..) *
}// 	
var11 
state11 
=11 
StateAdapter11  
.11  !
Create11! '
(11' (
request11( /
)11/ 0
;110 1
await33 
_repository33 
.33 
Add33 
(33 
state33 #
,33# $
cancellationToken33% 6
)336 7
;337 8
await55 
CommitAsync55 
(55 
cancellationToken55 +
)55+ ,
;55, -
return77 
Result77 
.77 
Success77 
(77 
state77 #
.77# $
Id77$ &
)77& '
;77' (
}88 
public:: 

async:: 
Task:: 
<:: 
Result:: 
>:: 
Handle:: $
(::$ %
UpdateStateCommand::% 7
request::8 ?
,::? @
CancellationToken::A R
cancellationToken::S d
)::d e
{;; 
var<< 
errors<< 
=<< 
await<< %
FluentValidationExtension<< 4
<<<4 5 
UpdateStateValidator<<5 I
,<<I J
UpdateStateCommand<<K ]
><<] ^
.<<^ _
GetValidationErrors<<_ r
(<<r s
request<<s z
,<<z {
cancellationToken	<<| ç
)
<<ç é
;
<<é è
if>> 

(>> 
errors>> 
.>> 
Any>> 
(>> 
)>> 
)>> 
return?? 
Result?? 
.?? 
Invalid?? !
(??! "
errors??" (
)??( )
;??) *
varAA 
existAA 
=AA 
awaitAA 
_repositoryAA %
.AA% &
GetByIdAA& -
(AA- .
requestAA. 5
.AA5 6
IdAA6 8
,AA8 9
cancellationTokenAA: K
)AAK L
;AAL M
ifCC 

(CC 
existCC 
==CC 
nullCC 
)CC 
returnDD 
ResultDD 
.DD 
NotFoundDD "
(DD" #
)DD# $
;DD$ %

ExpressionFF 
<FF 
FuncFF 
<FF 
StateFF 
,FF 
boolFF #
>FF# $
>FF$ %

expressionFF& 0
=FF1 2
cFF3 4
=>FF5 7
(FF8 9
cFF9 :
.FF: ;
CodeFF; ?
==FF@ B
requestFFC J
.FFJ K
CodeFFK O
||FFP R
cFFS T
.FFT U
AcronymFFU \
.FF\ ]
ToLowerFF] d
(FFd e
)FFe f
==FFg i
requestFFj q
.FFq r
AcronymFFr y
.FFy z
ToLower	FFz Å
(
FFÅ Ç
)
FFÇ É
)
FFÉ Ñ
&&
FFÖ á
c
FFà â
.
FFâ ä
Id
FFä å
!=
FFç è
request
FFê ó
.
FFó ò
Id
FFò ö
;
FFö õ
varHH 
conflictHH 
=HH 
awaitHH 
_repositoryHH (
.HH( )
GetAllHH) /
(HH/ 0

expressionHH0 :
,HH: ;
cancellationTokenHH< M
:HHM N
cancellationTokenHHO `
)HH` a
;HHa b
ifJJ 

(JJ 
conflictJJ 
.JJ 
AnyJJ 
(JJ 
)JJ 
)JJ 
{KK 	
errorsLL 
=LL 
newLL 
ListLL 
<LL 
ValidationErrorLL -
>LL- .
{LL/ 0
newMM 
ValidationErrorMM '
{NN 

IdentifierOO "
=OO# $
$strOO% /
,OO/ 0
ErrorMessagePP $
=PP% &
$strPP' V
}PPW X
}QQ 
;QQ 
returnSS 
ResultSS 
.SS 
InvalidSS !
(SS! "
errorsSS" (
)SS( )
;SS) *
}TT 	
varVV 
stateVV 
=VV 
StateAdapterVV  
.VV  !
UpdateVV! '
(VV' (
requestVV( /
)VV/ 0
;VV0 1
awaitXX 
_repositoryXX 
.XX 
UpdateXX  
(XX  !
stateXX! &
)XX& '
;XX' (
awaitZZ 
CommitAsyncZZ 
(ZZ 
cancellationTokenZZ +
)ZZ+ ,
;ZZ, -
return[[ 
Result[[ 
.[[ 
Success[[ 
([[ 
)[[ 
;[[  
}\\ 
public^^ 

async^^ 
Task^^ 
<^^ 
Result^^ 
>^^ 
Handle^^ $
(^^$ %
RemoveStateCommand^^% 7
request^^8 ?
,^^? @
CancellationToken^^A R
cancellationToken^^S d
)^^d e
{__ 
var`` 
errors`` 
=`` 
await`` %
FluentValidationExtension`` 4
<``4 5 
RemoveStateValidator``5 I
,``I J
RemoveStateCommand``K ]
>``] ^
.``^ _
GetValidationErrors``_ r
(``r s
request``s z
,``z {
cancellationToken	``| ç
)
``ç é
;
``é è
ifbb 

(bb 
errorsbb 
.bb 
Anybb 
(bb 
)bb 
)bb 
returncc 
Resultcc 
.cc 
Invalidcc !
(cc! "
errorscc" (
)cc( )
;cc) *
varee 
existee 
=ee 
awaitee 
_repositoryee %
.ee% &
GetByIdWithCitiesee& 7
(ee7 8
requestee8 ?
.ee? @
Idee@ B
,eeB C
cancellationTokeneeD U
)eeU V
;eeV W
ifgg 

(gg 
existgg 
==gg 
nullgg 
)gg 
returnhh 
Resulthh 
.hh 
NotFoundhh "
(hh" #
)hh# $
;hh$ %
ifjj 

(jj 
existjj 
.jj 
Citiesjj 
.jj 
Anyjj 
(jj 
)jj 
)jj 
{kk 	
errorsll 
=ll 
newll 
Listll 
<ll 
ValidationErrorll -
>ll- .
{ll/ 0
newmm 
ValidationErrormm '
{nn 

Identifieroo "
=oo# $
$stroo% 5
,oo5 6
ErrorMessagepp $
=pp% &
$strpp' m
}ppn o
}qq 
;qq 
returnss 
Resultss 
.ss 
Invalidss !
(ss! "
errorsss" (
)ss( )
;ss) *
}tt 	
awaituu 
_repositoryuu 
.uu 
Removeuu  
(uu  !
existuu! &
,uu& '
cancellationTokenuu( 9
)uu9 :
;uu: ;
awaitww 
CommitAsyncww 
(ww 
cancellationTokenww +
)ww+ ,
;ww, -
returnyy 
Resultyy 
.yy 
Successyy 
(yy 
)yy 
;yy  
}zz 
}{{ Ï8
\C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Handler\UserCommandHandler.cs
	namespace 	
Ibge
 
. 
Application 
. 
Handler "
;" #
public 
class 
UserCommandHandler 
:  !
CommandHandler" 0
,0 1
IRequestHandler 
< 
AuthUserCommand '
,' (
Result) /
</ 0
TokenResponse0 =
>= >
>> ?
,? @
IRequestHandler 
< 
CreateUserCommand )
,) *
Result+ 1
<1 2
Guid2 6
>6 7
>7 8
{ 
private 
readonly 
IUserRepository $
_repository% 0
;0 1
private 
readonly  
GenerateTokenUseCase )!
_generateTokenUseCase* ?
;? @
public 

UserCommandHandler 
( 
DatabaseContext -
context. 5
,5 6
IUserRepository7 F

repositoryG Q
,Q R 
GenerateTokenUseCaseS g 
generateTokenUseCaseh |
)| }
:~ 
base
Ä Ñ
(
Ñ Ö
context
Ö å
)
å ç
{ 
_repository 
= 

repository  
;  !!
_generateTokenUseCase 
=  
generateTokenUseCase  4
;4 5
} 
public 

async 
Task 
< 
Result 
< 
TokenResponse *
>* +
>+ ,
Handle- 3
(3 4
AuthUserCommand4 C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 
var 
errors 
= 
await %
FluentValidationExtension 4
<4 5
AuthUserValidator5 F
,F G
AuthUserCommandH W
>W X
.X Y
GetValidationErrorsY l
(l m
requestm t
,t u
cancellationToken	v á
)
á à
;
à â
if!! 

(!! 
errors!! 
.!! 
Any!! 
(!! 
)!! 
)!! 
return"" 
Result"" 
."" 
Invalid"" !
(""! "
errors""" (
)""( )
;"") *
var$$ 
query$$ 
=$$ 
await$$ 
_repository$$ %
.$$% &
GetAll$$& ,
($$, -
c$$- .
=>$$/ 1
c$$2 3
.$$3 4
Email$$4 9
==$$: <
request$$= D
.$$D E
Email$$E J
,$$J K
cancellationToken$$L ]
:$$] ^
cancellationToken$$_ p
)$$p q
;$$q r
var&& 
user&& 
=&& 
query&& 
.&& 
FirstOrDefault&& '
(&&' (
)&&( )
;&&) *
if(( 

((( 
user(( 
==(( 
null(( 
||(( 
!(( 
Bcrypt(( #
.((# $
Verify(($ *
(((* +
request((+ 2
.((2 3
Password((3 ;
,((; <
user((= A
.((A B
Password((B J
)((J K
)((K L
{)) 	
errors** 
=** 
new** 
List** 
<** 
ValidationError** -
>**- .
{**/ 0
new++ 
ValidationError++ '
{,, 

Identifier-- "
=--# $
$str--% ;
,--; <
ErrorMessage.. $
=..% &
$str..' <
}..= >
}// 
;// 
return11 
Result11 
.11 
Invalid11 !
(11! "
errors11" (
)11( )
;11) *
}22 	
var44 
tokenResponse44 
=44 
new44 
TokenResponse44  -
{55 	
Token66 
=66 !
_generateTokenUseCase66 )
.66) *
Action66* 0
(660 1
user661 5
)665 6
}77 	
;77	 

return99 
Result99 
.99 
Success99 
(99 
tokenResponse99 +
)99+ ,
;99, -
}:: 
public<< 

async<< 
Task<< 
<<< 
Result<< 
<<< 
Guid<< !
><<! "
><<" #
Handle<<$ *
(<<* +
CreateUserCommand<<+ <
request<<= D
,<<D E
CancellationToken<<F W
cancellationToken<<X i
)<<i j
{== 
var>> 
errors>> 
=>> 
await>> %
FluentValidationExtension>> 4
<>>4 5
CreateUserValidator>>5 H
,>>H I
CreateUserCommand>>J [
>>>[ \
.>>\ ]
GetValidationErrors>>] p
(>>p q
request>>q x
,>>x y
cancellationToken	>>z ã
)
>>ã å
;
>>å ç
if@@ 

(@@ 
errors@@ 
.@@ 
Any@@ 
(@@ 
)@@ 
)@@ 
returnAA 
ResultAA 
.AA 
InvalidAA !
(AA! "
errorsAA" (
)AA( )
;AA) *
varCC 
existCC 
=CC 
awaitCC 
_repositoryCC %
.CC% &
GetAllCC& ,
(CC, -
cCC- .
=>CC/ 1
cCC2 3
.CC3 4
EmailCC4 9
==CC: <
requestCC= D
.CCD E
EmailCCE J
,CCJ K
cancellationTokenCCL ]
:CC] ^
cancellationTokenCC_ p
)CCp q
;CCq r
ifEE 

(EE 
existEE 
.EE 
AnyEE 
(EE 
)EE 
)EE 
{FF 	
errorsGG 
=GG 
newGG 
ListGG 
<GG 
ValidationErrorGG -
>GG- .
{GG/ 0
newHH 
ValidationErrorHH '
{II 

IdentifierJJ "
=JJ# $
$strJJ% /
,JJ/ 0
ErrorMessageKK $
=KK% &
$strKK' V
}KKW X
}LL 
;LL 
returnNN 
ResultNN 
.NN 
InvalidNN !
(NN! "
errorsNN" (
)NN( )
;NN) *
}OO 	
varQQ 
userQQ 
=QQ 
UserAdapterQQ 
.QQ 
CreateNewUserQQ ,
(QQ, -
requestQQ- 4
)QQ4 5
;QQ5 6
awaitSS 
_repositorySS 
.SS 
AddSS 
(SS 
userSS "
,SS" #
cancellationTokenSS$ 5
)SS5 6
;SS6 7
awaitUU 
CommitAsyncUU 
(UU 
cancellationTokenUU +
)UU+ ,
;UU, -
returnWW 
ResultWW 
.WW 
SuccessWW 
(WW 
userWW "
.WW" #
IdWW# %
)WW% &
;WW& '
}XX 
}YY òU
WC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Services\CityServices.cs
	namespace 	
Ibge
 
. 
Application 
. 
Services #
;# $
public 
class 
CityServices 
: 
ICityServices )
{ 
private 
readonly 
ICityRepository $
_cityRepository% 4
;4 5
private 
readonly 
IStateServices #
_stateServices$ 2
;2 3
private 
readonly 
ILogger 
< 
CityServices )
>) *
_logger+ 2
;2 3
private 
readonly 
	IMediator 
	_mediator (
;( )
public 

CityServices 
( 
ICityRepository '
cityRepository( 6
,6 7
IStateServices8 F
stateServicesG T
,T U
ILoggerV ]
<] ^
CityServices^ j
>j k
loggerl r
,r s
	IMediatort }
mediator	~ Ü
)
Ü á
{ 
_cityRepository 
= 
cityRepository (
;( )
_stateServices 
= 
stateServices &
;& '
_logger 
= 
logger 
; 
	_mediator 
= 
mediator 
; 
} 
public 

async 
Task 
< 
Result 
< 
CityResponseDto ,
?, -
>- .
>. /
GetById0 7
(7 8
Guid8 <
id= ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 
var 
city 
= 
await 
_cityRepository (
.( )
GetById) 0
(0 1
id1 3
,3 4
cancellationToken5 F
)F G
;G H
if!! 

(!! 
city!! 
==!! 
null!! 
)!! 
return"" 
Result"" 
."" 
NotFound"" "
(""" #
)""# $
;""$ %
return$$ 
Result$$ 
.$$ 
Success$$ 
($$ 
CityAdapter$$ )
.$$) *

FromDomain$$* 4
($$4 5
city$$5 9
)$$9 :
)$$: ;
;$$; <
}%% 
public'' 

async'' 
Task'' 
<'' 
Result'' 
<'' 
PagedResponseDto'' -
<''- .
CityResponseDto''. =
?''= >
>''> ?
>''? @
>''@ A
Get''B E
(''E F
CityQueryParamsDto''F X
param''Y ^
,''^ _
CancellationToken''` q
cancellationToken	''r É
)
''É Ñ
{(( 

Expression)) 
<)) 
Func)) 
<)) 
City)) 
,)) 
bool)) "
>))" #
>))# $

expression))% /
=))0 1
c))2 3
=>))4 6
(** 
param** 
.** 
Id** 
==** 
null** 
||**  
c**! "
.**" #
Id**# %
==**& (
param**) .
.**. /
Id**/ 1
)**1 2
&&**3 5
(++ 
param++ 
.++ 
Code++ 
==++ 
null++ 
||++  "
c++# $
.++$ %
Code++% )
==++* ,
param++- 2
.++2 3
Code++3 7
)++7 8
&&++9 ;
(,, 
string,, 
.,, 
IsNullOrWhiteSpace,, &
(,,& '
param,,' ,
.,,, -
Name,,- 1
),,1 2
||,,3 5
c,,6 7
.,,7 8
Name,,8 <
.,,< =
Contains,,= E
(,,E F
param,,F K
.,,K L
Name,,L P
),,P Q
),,Q R
&&,,S U
(-- 
param-- 
.-- 
StateId-- 
==-- 
null-- "
||--# %
c--& '
.--' (
StateId--( /
==--0 2
param--3 8
.--8 9
StateId--9 @
)--@ A
;--A B
var// 
	paginated// 
=// 
await// 
_cityRepository// -
.//- .
Get//. 1
(//1 2

expression//2 <
,//< =
param//> C
.//C D
Page//D H
,//H I
param//J O
.//O P
Size//P T
,//T U
cancellationToken//V g
)//g h
;//h i
var11 
parse11 
=11 
	paginated11 
.11 
Select11 $
(11$ %
CityAdapter11% 0
.110 1

FromDomain111 ;
)11; <
;11< =
var33 
result33 
=33 
new33 
PagedResponseDto33 )
<33) *
CityResponseDto33* 9
?339 :
>33: ;
(33; <
parse33< A
,33A B
	paginated33C L
.33L M
CurrentPage33M X
,33X Y
	paginated33Z c
.33c d

TotalPages33d n
,33n o
	paginated33p y
.33y z
PageSize	33z Ç
,
33Ç É
	paginated
33Ñ ç
.
33ç é

TotalCount
33é ò
)
33ò ô
;
33ô ö
return55 
result55 
;55 
}66 
public88 

async88 
Task88 
<88 
bool88 
>88 
AddFromFile88 '
(88' (
CityFromFileDto88( 7
item888 <
,88< =
CancellationToken88> O
cancellationToken88P a
)88a b
{99 
var:: 
state:: 
=:: 
await:: 
_stateServices:: (
.::( )
GetIdByCode::) 4
(::4 5
item::5 9
.::9 :
	StateCode::: C
,::C D
cancellationToken::E V
)::V W
;::W X
if<< 

(<< 
!<< 
state<< 
.<< 
	IsSuccess<< 
||<< 
state<<  %
.<<% &
Value<<& +
==<<, .
null<</ 3
)<<3 4
{== 	
var>> 
error>> 
=>> 
$">> 
$str>> 7
{>>7 8
item>>8 <
.>>< =
	StateCode>>= F
}>>F G
">>G H
;>>H I
_logger?? 
.?? 

LogWarning?? 
(?? 
$str	?? à
,
??à â
item
??ä é
.
??é è
ToString
??è ó
(
??ó ò
)
??ò ô
,
??ô ö
item
??õ ü
.
??ü †
Id
??† ¢
,
??¢ £
error
??§ ©
)
??© ™
;
??™ ´
returnAA 
falseAA 
;AA 
}BB 	
varDD 
dataDD 
=DD 
CityAdapterDD 
.DD 
FromFileDD '
(DD' (
itemDD( ,
,DD, -
stateDD. 3
.DD3 4
ValueDD4 9
.DD9 :
ValueDD: ?
)DD? @
;DD@ A
varFF 
resultFF 
=FF 
awaitFF 
	_mediatorFF $
.FF$ %
SendFF% )
(FF) *
dataFF* .
,FF. /
cancellationTokenFF0 A
)FFA B
;FFB C
ifHH 

(HH 
!HH 
resultHH 
.HH 
	IsSuccessHH 
)HH 
{II 	
varJJ 
errorsJJ 
=JJ 
resultJJ 
.JJ  
ValidationErrorsJJ  0
.JJ0 1
SelectJJ1 7
(JJ7 8
cJJ8 9
=>JJ: <
newJJ= @
{KK 
errorLL 
=LL 
$"LL 
{LL 
cLL 
.LL 

IdentifierLL '
}LL' (
$strLL( +
{LL+ ,
cLL, -
.LL- .
ErrorMessageLL. :
}LL: ;
"LL; <
}MM 
)MM 
;MM 
_loggerOO 
.OO 

LogWarningOO 
(OO 
$str	OO à
,
OOà â
item
OOä é
.
OOé è
ToString
OOè ó
(
OOó ò
)
OOò ô
,
OOô ö
item
OOõ ü
.
OOü †
Id
OO† ¢
,
OO¢ £
string
OO§ ™
.
OO™ ´
Join
OO´ Ø
(
OOØ ∞
$char
OO∞ ≥
,
OO≥ ¥
errors
OOµ ª
.
OOª º
Select
OOº ¬
(
OO¬ √
c
OO√ ƒ
=>
OO≈ «
c
OO» …
.
OO…  
error
OO  œ
)
OOœ –
)
OO– —
)
OO— “
;
OO“ ”
returnPP 
falsePP 
;PP 
}QQ 	
_loggerSS 
.SS 
LogInformationSS 
(SS 
$strSS i
,SSi j
itemSSk o
.SSo p
ToStringSSp x
(SSx y
)SSy z
,SSz {
item	SS| Ä
.
SSÄ Å
Id
SSÅ É
)
SSÉ Ñ
;
SSÑ Ö
returnTT 
trueTT 
;TT 
}UU 
publicWW 

asyncWW 
TaskWW 
<WW 
ResultWW 
<WW 
GuidWW !
>WW! "
>WW" #
CreateWW$ *
(WW* +
CreateCityCommandWW+ <
requestWW= D
,WWD E
CancellationTokenWWF W
cancellationTokenWWX i
)WWi j
=>WWk m
awaitXX 
	_mediatorXX 
.XX 
SendXX 
(XX 
requestXX $
,XX$ %
cancellationTokenXX& 7
)XX7 8
;XX8 9
publicZZ 

asyncZZ 
TaskZZ 
<ZZ 
ResultZZ 
>ZZ 
UpdateZZ $
(ZZ$ %
UpdateCityCommandZZ% 6
requestZZ7 >
,ZZ> ?
CancellationTokenZZ@ Q
cancellationTokenZZR c
)ZZc d
=>ZZe g
await[[ 
	_mediator[[ 
.[[ 
Send[[ 
([[ 
request[[ $
,[[$ %
cancellationToken[[& 7
)[[7 8
;[[8 9
public]] 

async]] 
Task]] 
<]] 
Result]] 
>]] 
Remove]] $
(]]$ %
RemoveCityCommand]]% 6
request]]7 >
,]]> ?
CancellationToken]]@ Q
cancellationToken]]R c
)]]c d
=>]]e g
await^^ 
	_mediator^^ 
.^^ 
Send^^ 
(^^ 
request^^ $
,^^$ %
cancellationToken^^& 7
)^^7 8
;^^8 9
}__ ˚<
YC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Services\ImportServices.cs
	namespace 	
Ibge
 
. 
Application 
. 
Services #
;# $
public

 
class

 
ImportServices

 
:

 
IImportServices

 -
{ 
private 
readonly 
IChannelService $
<$ %
StateFromFileDto% 5
>5 6
_channelState7 D
;D E
private 
readonly 
IChannelService $
<$ %
CityFromFileDto% 4
>4 5
_channelCity6 B
;B C
public 

ImportServices 
( 
IChannelService )
<) *
StateFromFileDto* :
>: ;
channelState< H
,H I
IChannelServiceJ Y
<Y Z
CityFromFileDtoZ i
>i j
channelCityk v
)v w
{ 
_channelState 
= 
channelState $
;$ %
_channelCity 
= 
channelCity "
;" #
} 
public 

async 
Task 
ProccessFile "
(" #
Guid# '
id( *
,* +
	IFormFile, 5
file6 :
,: ;
CancellationToken< M
cancellationTokenN _
)_ `
{ 
using 
var 
fs 
= 
file 
. 
OpenReadStream *
(* +
)+ ,
;, -
using 
var 
wb 
= 
new 
XSSFWorkbook '
(' (
fs( *
)* +
;+ ,
var 
sheets 
= 
wb 
. 
NumberOfSheets &
;& '
if 

( 
sheets 
<= 
$num 
) 
return 
; 
var 
sheet 
= 
( 
	XSSFSheet 
) 
wb !
.! "

GetSheetAt" ,
(, -
$num- .
). /
;/ 0
var 
citiesSheet 
= 
( 
	XSSFSheet $
)$ %
wb% '
.' (

GetSheetAt( 2
(2 3
$num3 4
)4 5
;5 6
GetStateRows!! 
(!! 
id!! 
,!! 
sheet!! 
,!! 
cancellationToken!!  1
)!!1 2
.!!2 3

GetAwaiter!!3 =
(!!= >
)!!> ?
;!!? @
await## 
GetCitiesRows## 
(## 
id## 
,## 
citiesSheet##  +
,##+ ,
cancellationToken##- >
)##> ?
;##? @
}$$ 
public&& 

async&& 
Task&& 
GetStateRows&& "
(&&" #
Guid&&# '
id&&( *
,&&* +
	XSSFSheet&&, 5
sheet&&6 ;
,&&; <
CancellationToken&&= N
cancellationToken&&O `
)&&` a
{'' 
for(( 
((( 
int(( 
rowIndex(( 
=(( 
$num(( 
;(( 
rowIndex(( '
<=((( *
sheet((+ 0
.((0 1

LastRowNum((1 ;
;((; <
rowIndex((= E
++((E G
)((G H
{)) 	
var** 
row** 
=** 
sheet** 
.** 
GetRow** "
(**" #
rowIndex**# +
)**+ ,
;**, -
if,, 
(,, 
row,, 
!=,, 
null,, 
),, 
{-- 
var.. 
code.. 
=.. 
(.. 
int.. 
)..  
(..  !
row..! $
...$ %
GetCell..% ,
(.., -
$num..- .
)... /
?../ 0
...0 1
NumericCellValue..1 A
??..B D
$num..E F
)..F G
;..G H
var// 
acronym// 
=// 
row// !
.//! "
GetCell//" )
(//) *
$num//* +
)//+ ,
?//, -
.//- .
StringCellValue//. =
??//> @
string//A G
.//G H
Empty//H M
;//M N
var00 
name00 
=00 
row00 
.00 
GetCell00 &
(00& '
$num00' (
)00( )
?00) *
.00* +
StringCellValue00+ :
??00; =
string00> D
.00D E
Empty00E J
;00J K
var22 
data22 
=22 
new22 
StateFromFileDto22 /
(22/ 0
id220 2
,222 3
code224 8
,228 9
name22: >
,22> ?
acronym22@ G
)22G H
;22H I
await44 
_channelState44 #
.44# $
	GetWriter44$ -
(44- .
)44. /
.44/ 0

WriteAsync440 :
(44: ;
data44; ?
,44? @
cancellationToken44A R
)44R S
;44S T
}55 
}66 	
}77 
public99 

async99 
Task99 
GetCitiesRows99 #
(99# $
Guid99$ (
id99) +
,99+ ,
	XSSFSheet99- 6
sheet997 <
,99< =
CancellationToken99> O
cancellationToken99P a
)99a b
{:: 
for;; 
(;; 
int;; 
rowIndex;; 
=;; 
$num;; 
;;; 
rowIndex;; '
<=;;( *
sheet;;+ 0
.;;0 1

LastRowNum;;1 ;
;;;; <
rowIndex;;= E
++;;E G
);;G H
{<< 	
var== 
row== 
=== 
sheet== 
.== 
GetRow== "
(==" #
rowIndex==# +
)==+ ,
;==, -
if?? 
(?? 
row?? 
!=?? 
null?? 
)?? 
{@@ 
varAA 
codeAA 
=AA 
(AA 
intAA 
)AA  
(AA  !
rowAA! $
.AA$ %
GetCellAA% ,
(AA, -
$numAA- .
)AA. /
?AA/ 0
.AA0 1
NumericCellValueAA1 A
??AAB D
$numAAE F
)AAF G
;AAG H
varBB 
nameBB 
=BB 
rowBB 
.BB 
GetCellBB &
(BB& '
$numBB' (
)BB( )
?BB) *
.BB* +
StringCellValueBB+ :
??BB; =
stringBB> D
.BBD E
EmptyBBE J
;BBJ K
varCC 
	stateCodeCC 
=CC 
(CC  !
intCC! $
)CC$ %
(CC% &
rowCC& )
.CC) *
GetCellCC* 1
(CC1 2
$numCC2 3
)CC3 4
?CC4 5
.CC5 6
NumericCellValueCC6 F
??CCG I
$numCCJ K
)CCK L
;CCL M
varEE 
dataEE 
=EE 
newEE 
CityFromFileDtoEE .
(EE. /
idEE/ 1
,EE1 2
codeEE3 7
,EE7 8
nameEE9 =
,EE= >
	stateCodeEE? H
)EEH I
;EEI J
awaitGG 
_channelCityGG "
.GG" #
	GetWriterGG# ,
(GG, -
)GG- .
.GG. /

WriteAsyncGG/ 9
(GG9 :
dataGG: >
,GG> ?
cancellationTokenGG@ Q
)GGQ R
;GGR S
}HH 
}II 	
}JJ 
}KK ÎR
XC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Services\StateServices.cs
	namespace 	
Ibge
 
. 
Application 
. 
Services #
;# $
public 
class 
StateServices 
: 
IStateServices +
{ 
private 
readonly 
IStateRepository %
_stateRepository& 6
;6 7
private 
readonly 
	IMediator 
	_mediator (
;( )
private 
readonly 
ILogger 
< 
StateServices *
>* +
_logger, 3
;3 4
public 

StateServices 
( 
IStateRepository )
stateRepository* 9
,9 :
	IMediator; D
mediatorE M
,M N
ILoggerO V
<V W
StateServicesW d
>d e
loggerf l
)l m
{ 
_stateRepository 
= 
stateRepository *
;* +
	_mediator 
= 
mediator 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
bool 
> 
AddFromFile '
(' (
StateFromFileDto( 8
item9 =
,= >
CancellationToken? P
cancellationTokenQ b
)b c
{ 
var 
param 
= 
StateAdapter  
.  !
FromFile! )
() *
item* .
). /
;/ 0
var   
result   
=   
await   
	_mediator   $
.  $ %
Send  % )
(  ) *
param  * /
,  / 0
cancellationToken  1 B
)  B C
;  C D
if"" 

("" 
!"" 
result"" 
."" 
	IsSuccess"" 
)"" 
{## 	
var$$ 
errors$$ 
=$$ 
result$$ 
.$$  
ValidationErrors$$  0
.$$0 1
Select$$1 7
($$7 8
c$$8 9
=>$$: <
new$$= @
{%% 
error&& 
=&& 
$"&& 
{&& 
c&& 
.&& 

Identifier&& '
}&&' (
$str&&( +
{&&+ ,
c&&, -
.&&- .
ErrorMessage&&. :
}&&: ;
"&&; <
}'' 
)'' 
;'' 
_logger)) 
.)) 

LogWarning)) 
()) 
$str	)) â
,
))â ä
item
))ã è
.
))è ê
ToString
))ê ò
(
))ò ô
)
))ô ö
,
))ö õ
item
))ú †
.
))† °
Id
))° £
,
))£ §
string
))• ´
.
))´ ¨
Join
))¨ ∞
(
))∞ ±
$char
))± ¥
,
))¥ µ
errors
))∂ º
.
))º Ω
Select
))Ω √
(
))√ ƒ
c
))ƒ ≈
=>
))∆ »
c
))…  
.
))  À
error
))À –
)
))– —
)
))— “
)
))“ ”
;
))” ‘
return** 
false** 
;** 
}++ 	
_logger-- 
.-- 
LogInformation-- 
(-- 
$str-- k
,--k l
item--m q
.--q r
ToString--r z
(--z {
)--{ |
,--| }
item	--~ Ç
.
--Ç É
Id
--É Ö
)
--Ö Ü
;
--Ü á
return.. 
true.. 
;.. 
}// 
public11 

async11 
Task11 
<11 
Result11 
<11 
PagedResponseDto11 -
<11- .
StateResponseDto11. >
?11> ?
>11? @
>11@ A
>11A B
Get11C F
(11F G
StateQueryParamsDto11G Z
param11[ `
,11` a
CancellationToken11b s
cancellationToken	11t Ö
)
11Ö Ü
{22 

Expression33 
<33 
Func33 
<33 
State33 
,33 
bool33 #
>33# $
>33$ %

expression33& 0
=331 2
c333 4
=>335 7
(44 
string44 
.44 
IsNullOrWhiteSpace44 *
(44* +
param44+ 0
.440 1
Name441 5
)445 6
||447 9
c44: ;
.44; <
Name44< @
.44@ A
Contains44A I
(44I J
param44J O
.44O P
Name44P T
)44T U
)44U V
&&44W Y
(55 
string55 
.55 
IsNullOrWhiteSpace55 *
(55* +
param55+ 0
.550 1
Acronym551 8
)558 9
||55: <
c55= >
.55> ?
Acronym55? F
.55F G
Contains55G O
(55O P
param55P U
.55U V
Acronym55V ]
)55] ^
)55^ _
&&55` b
(66 
param66 
.66 
Id66 
==66 
null66 !
||66" $
c66% &
.66& '
Id66' )
==66* ,
param66- 2
.662 3
Id663 5
)665 6
&&667 9
(77 
param77 
.77 
Code77 
==77 
null77 #
||77$ &
c77' (
.77( )
Code77) -
==77. 0
param771 6
.776 7
Code777 ;
)77; <
;77< =
var99 
	paginated99 
=99 
await99 
_stateRepository99 .
.99. /
Get99/ 2
(992 3

expression993 =
,99= >
param99? D
.99D E
Page99E I
,99I J
param99K P
.99P Q
Size99Q U
,99U V
cancellationToken99W h
)99h i
;99i j
var;; 
parse;; 
=;; 
	paginated;; 
.;; 
Select;; $
(;;$ %
StateAdapter;;% 1
.;;1 2

FromDomain;;2 <
);;< =
;;;= >
var== 
result== 
=== 
new== 
PagedResponseDto== )
<==) *
StateResponseDto==* :
?==: ;
>==; <
(==< =
parse=== B
,==B C
	paginated==D M
.==M N
CurrentPage==N Y
,==Y Z
	paginated==[ d
.==d e

TotalPages==e o
,==o p
	paginated==q z
.==z {
PageSize	=={ É
,
==É Ñ
	paginated
==Ö é
.
==é è

TotalCount
==è ô
)
==ô ö
;
==ö õ
return?? 
Result?? 
.?? 
Success?? 
(?? 
result?? $
)??$ %
;??% &
}@@ 
publicBB 

asyncBB 
TaskBB 
<BB 
ResultBB 
<BB 
GuidBB !
?BB! "
>BB" #
>BB# $
GetIdByCodeBB% 0
(BB0 1
intBB1 4
codeBB5 9
,BB9 :
CancellationTokenBB; L
cancellationTokenBBM ^
)BB^ _
{CC 
varDD 
resultDD 
=DD 
awaitDD 
_stateRepositoryDD +
.DD+ ,
GetIdByCodeDD, 7
(DD7 8
codeDD8 <
,DD< =
cancellationTokenDD> O
)DDO P
;DDP Q
ifFF 

(FF 
resultFF 
isFF 
nullFF 
)FF 
returnGG 
ResultGG 
.GG 
NotFoundGG "
(GG" #
)GG# $
;GG$ %
returnII 
ResultII 
.II 
SuccessII 
(II 
resultII $
)II$ %
;II% &
}JJ 
publicMM 

asyncMM 
TaskMM 
<MM 
ResultMM 
<MM 
StateResponseDtoMM -
?MM- .
>MM. /
>MM/ 0
GetByIdMM1 8
(MM8 9
GuidMM9 =
idMM> @
,MM@ A
CancellationTokenMMB S
cancellationTokenMMT e
)MMe f
{NN 
varOO 
resultOO 
=OO 
awaitOO 
_stateRepositoryOO +
.OO+ ,
GetByIdOO, 3
(OO3 4
idOO4 6
,OO6 7
cancellationTokenOO8 I
)OOI J
;OOJ K
ifQQ 

(QQ 
resultQQ 
isQQ 
nullQQ 
)QQ 
returnRR 
ResultRR 
.RR 
NotFoundRR "
(RR" #
)RR# $
;RR$ %
returnTT 
ResultTT 
.TT 
SuccessTT 
(TT 
StateAdapterTT *
.TT* +

FromDomainTT+ 5
(TT5 6
resultTT6 <
)TT< =
)TT= >
;TT> ?
}UU 
publicWW 

asyncWW 
TaskWW 
<WW 
ResultWW 
<WW 
GuidWW !
>WW! "
>WW" #
CreateWW$ *
(WW* +
CreateStateCommandWW+ =
requestWW> E
,WWE F
CancellationTokenWWG X
cancellationTokenWWY j
)WWj k
=>WWl n
awaitXX 
	_mediatorXX 
.XX 
SendXX 
(XX 
requestXX $
,XX$ %
cancellationTokenXX& 7
)XX7 8
;XX8 9
publicZZ 

asyncZZ 
TaskZZ 
<ZZ 
ResultZZ 
>ZZ 
UpdateZZ $
(ZZ$ %
UpdateStateCommandZZ% 7
requestZZ8 ?
,ZZ? @
CancellationTokenZZA R
cancellationTokenZZS d
)ZZd e
=>ZZf h
await[[ 
	_mediator[[ 
.[[ 
Send[[ 
([[ 
request[[ $
,[[$ %
cancellationToken[[& 7
)[[7 8
;[[8 9
public]] 

async]] 
Task]] 
<]] 
Result]] 
>]] 
Remove]] $
(]]$ %
RemoveStateCommand]]% 7
request]]8 ?
,]]? @
CancellationToken]]A R
cancellationToken]]S d
)]]d e
=>]]f h
await^^ 
	_mediator^^ 
.^^ 
Send^^ 
(^^ 
request^^ $
,^^$ %
cancellationToken^^& 7
)^^7 8
;^^8 9
}__ ‡
WC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Services\UserServices.cs
	namespace 	
Ibge
 
. 
Application 
. 
Services #
;# $
public		 
class		 
UserServices		 
:		 
IUserServices		 )
{

 
private 
readonly 
	IMediator 
	_mediator (
;( )
public 

UserServices 
( 
	IMediator !
mediator" *
)* +
{ 
	_mediator 
= 
mediator 
; 
} 
public 

async 
Task 
< 
Result 
< 
TokenResponse *
>* +
>+ ,
Auth- 1
(1 2
AuthUserCommand2 A
requestB I
,I J
CancellationTokenK \
cancellationToken] n
)n o
=>p r
await 
	_mediator 
. 
Send 
( 
request $
,$ %
cancellationToken& 7
)7 8
;8 9
public 

async 
Task 
< 
Result 
< 
Guid !
>! "
>" #
Create$ *
(* +
CreateUserCommand+ <
request= D
,D E
CancellationTokenF W
cancellationTokenX i
)i j
=>k m
await 
	_mediator 
. 
Send 
( 
request $
,$ %
cancellationToken& 7
)7 8
;8 9
} Ù
_C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\UseCases\GenerateTokenUseCase.cs
	namespace		 	
Ibge		
 
.		 
Application		 
.		 
UseCases		 #
;		# $
public 
class  
GenerateTokenUseCase !
{ 
private 
readonly 

JwtOptions 
_options  (
;( )
public 
 
GenerateTokenUseCase 
(  
IOptions  (
<( )

JwtOptions) 3
>3 4
options5 <
)< =
{ 
_options 
= 
options 
. 
Value  
;  !
} 
public 

virtual 
string 
Action  
(  !
User! %
user& *
)* +
{ 
var 
tokenHandler 
= 
new #
JwtSecurityTokenHandler 6
(6 7
)7 8
;8 9!
ArgumentNullException 
. 
ThrowIfNull )
() *
_options* 2
.2 3
Key3 6
)6 7
;7 8
var 
key 
= 
Encoding 
. 
ASCII  
.  !
GetBytes! )
() *
_options* 2
.2 3
Key3 6
)6 7
;7 8
var 
tokenDescriptor 
= 
new !#
SecurityTokenDescriptor" 9
{ 	
Subject 
= 
new 
ClaimsIdentity (
(( )
new) ,
Claim- 2
[2 3
]3 4
{ 
new   
Claim   
(   

ClaimTypes   (
.  ( )
Name  ) -
,  - .
user  / 3
.  3 4
Name  4 8
.  8 9
ToString  9 A
(  A B
)  B C
)  C D
,  D E
new!! 
Claim!! 
(!! 

ClaimTypes!! (
.!!( )
Role!!) -
,!!- .
user!!/ 3
.!!3 4
IsAdmin!!4 ;
?!!< =
$str!!> E
:!!F G
$str!!H N
)!!N O
}"" 
)"" 
,"" 
Expires$$ 
=$$ 
DateTime$$ 
.$$ 
UtcNow$$ %
.$$% &

AddMinutes$$& 0
($$0 1
_options$$1 9
.$$9 :"
TimeToExpiresInMinutes$$: P
)$$P Q
,$$Q R
SigningCredentials%% 
=%%  
new%%! $
SigningCredentials%%% 7
(%%7 8
new%%8 ; 
SymmetricSecurityKey%%< P
(%%P Q
key%%Q T
)%%T U
,%%U V
SecurityAlgorithms%%W i
.%%i j
HmacSha256Signature%%j }
)%%} ~
}'' 	
;''	 

var(( 
token(( 
=(( 
tokenHandler((  
.((  !
CreateToken((! ,
(((, -
tokenDescriptor((- <
)((< =
;((= >
return)) 
tokenHandler)) 
.)) 

WriteToken)) &
())& '
token))' ,
))), -
;))- .
}** 
}++ à
_C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\City\CityValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
City& *
;* +
public 
abstract 
class 
CityValidator #
<# $
T$ %
>% &
:' (
AbstractValidator) :
<: ;
T; <
>< =
where> C
TD E
:F G
CityCommandH S
{ 
public 

void 
ValidateName 
( 
) 
{		 
RuleFor

 
(

 
c

 
=>

 
c

 
.

 
Name

 
)

 
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
. 
MinimumLength 
( 
$num 
) 
. 
MaximumLength 
( 
$num 
) 
; 
} 
public 

void 
ValidateCode 
( 
) 
{ 
RuleFor 
( 
c 
=> 
c 
. 
Code 
) 
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
. 
GreaterThan 
( 
$num 
) 
; 
} 
public 

void 
ValidateStateId 
(  
)  !
{ 
RuleFor 
( 
c 
=> 
c 
. 
StateId 
) 
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
. 
NotEqual 
( 
Guid 
. 
Empty 
)  
;  !
} 
}   ù
eC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\City\CreateCityValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
City& *
;* +
public 
class 
CreateCityValidator  
:! "
CityValidator# 0
<0 1
CreateCityCommand1 B
>B C
{ 
public 

CreateCityValidator 
( 
)  
{ 
ValidateCode		 
(		 
)		 
;		 
ValidateName

 
(

 
)

 
;

 
ValidateStateId 
( 
) 
; 
} 
} œ
eC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\City\RemoveCityValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
City& *
;* +
public 
class 
RemoveCityValidator  
:! "
AbstractValidator# 4
<4 5
RemoveCityCommand5 F
>F G
{ 
public 

RemoveCityValidator 
( 
)  
{		 
RuleFor

 
(

 
c

 
=>

 
c

 
.

 
Id

 
)

 
. 
NotEqual 
( 
Guid 
. 
Empty  
)  !
;! "
} 
} ®	
eC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\City\UpdateCityValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
City& *
;* +
public 
class 
UpdateCityValidator  
:! "
CityValidator# 0
<0 1
UpdateCityCommand1 B
>B C
{ 
public 

UpdateCityValidator 
( 
)  
{		 
ValidateCode

 
(

 
)

 
;

 
ValidateName 
( 
) 
; 
ValidateStateId 
( 
) 
; 
RuleFor 
( 
c 
=> 
c 
. 
Id 
) 
.
 
NotEmpty 
( 
) 
.
 
NotNull 
( 
) 
.
 
NotEqual 
( 
Guid 
. 
Empty 
) 
;  
} 
} §
gC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\State\CreateStateValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
State& +
;+ ,
public 
class  
CreateStateValidator !
:" #
StateValidator$ 2
<2 3
CreateStateCommand3 E
>E F
{ 
public 
 
CreateStateValidator 
(  
)  !
{ 
ValidateCode		 
(		 
)		 
;		 
ValidateName

 
(

 
)

 
;

 
ValidateAcronym 
( 
) 
; 
} 
} ’
gC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\State\RemoveStateValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
State& +
;+ ,
public 
class  
RemoveStateValidator !
:" #
AbstractValidator$ 5
<5 6
RemoveStateCommand6 H
>H I
{ 
public 
 
RemoveStateValidator 
(  
)  !
{		 
RuleFor

 
(

 
c

 
=>

 
c

 
.

 
Id

 
)

 
. 
NotEqual 
( 
Guid 
. 
Empty  
)  !
;! "
} 
} ©
aC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\State\StateValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
State& +
;+ ,
public 
abstract 
class 
StateValidator $
<$ %
T% &
>& '
:( )
AbstractValidator* ;
<; <
T< =
>= >
where? D
TE F
:G H
StateCommandI U
{ 
	protected 
void 
ValidateCode 
(  
)  !
{		 
RuleFor

 
(

 
c

 
=>

 
c

 
.

 
Code

 
)

 
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
. 
GreaterThan 
( 
$num 
) 
; 
} 
	protected 
void 
ValidateName 
(  
)  !
{ 
RuleFor 
( 
c 
=> 
c 
. 
Name 
) 
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
. 
MinimumLength 
( 
$num 
) 
. 
MaximumLength 
( 
$num 
) 
; 
} 
	protected 
void 
ValidateAcronym "
(" #
)# $
{ 
RuleFor 
( 
c 
=> 
c 
. 
Acronym 
) 
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
. 
Must 
( 
c 
=> 
c 
. 
Length 
== !
$num" #
)# $
. 
WithMessage 
( 
$str 9
)9 :
;: ;
}   
}!! Ï	
gC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\State\UpdateStateValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
State& +
;+ ,
public 
class  
UpdateStateValidator !
:" #
StateValidator$ 2
<2 3
UpdateStateCommand3 E
>E F
{ 
public 
 
UpdateStateValidator 
(  
)  !
{		 
ValidateCode

 
(

 
)

 
;

 
ValidateName 
( 
) 
; 
ValidateAcronym 
( 
) 
; 

ValidateId 
( 
) 
; 
} 
	protected 
void 

ValidateId 
( 
) 
{ 
RuleFor 
( 
c 
=> 
c 
. 
Id 
) 
. 
NotEqual 
( 
Guid 
. 
Empty $
)$ %
;% &
} 
}  
cC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\User\AuthUserValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
User& *
;* +
public 
class 
AuthUserValidator 
:  
UserValidator! .
<. /
AuthUserCommand/ >
>> ?
{ 
public 

AuthUserValidator 
( 
) 
{ 

ValidEmail		 
(		 
)		 
;		 
ValidPassword

 
(

 
)

 
;

 
} 
} ú	
eC:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\User\CreateUserValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
User& *
;* +
public 
class 
CreateUserValidator  
:! "
UserValidator# 0
<0 1
CreateUserCommand1 B
>B C
{ 
public 

CreateUserValidator 
( 
)  
{		 

ValidEmail

 
(

 
)

 
;

 
ValidPassword 
( 
) 
; 
RuleFor 
( 
c 
=> 
c 
. 
Name 
) 
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
. 
MinimumLength 
( 
$num 
) 
. 
MaximumLength 
( 
$num 
) 
; 
} 
} ç
_C:\Users\patrick.amorim\source\repos\ibge-api\Ibge.Application\Validators\User\UserValidator.cs
	namespace 	
Ibge
 
. 
Application 
. 

Validators %
.% &
User& *
{ 
public 

class 
UserValidator 
< 
T  
>  !
:" #
AbstractValidator$ 5
<5 6
T6 7
>7 8
where9 >
T? @
:A B
UserCommandC N
{ 
public 
void 

ValidEmail 
( 
)  
{		 	
RuleFor

 
(

 
c

 
=>

 
c

 
.

 
Email

  
)

  !
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
. 
EmailAddress 
( 
)  
. 
MaximumLength 
(  
$num  #
)# $
;$ %
} 	
public 
void 
ValidPassword !
(! "
)" #
{ 	
RuleFor 
( 
c 
=> 
c 
. 
Password #
)# $
. 
NotEmpty 
( 
) 
. 
NotNull 
( 
) 
; 
} 	
} 
} 