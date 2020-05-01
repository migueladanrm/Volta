lexer grammar Scanner;

//// Symbols

NOT : '!';
QMARK : '"';
SQMARK : '\'';
HASH: '#';
DOLAR: '$';
AND: '&&';
OR: '||';
BL       : '(';
BR     : ')';
SEMICOLON   : ';';
ADD         : '+';
ADDADD      : '++';
SUB         : '-';
SUBSUB      : '--';
MUL         : '*';
DIV         : '/';
MDIV:       '%';
EQUALEQUAL: '==';
NOTEQUAL: '!=';
GREATEREQUAL: '>=';
LESSEQUAL: '<=';
EQUAL      : '=';
GREATER     : '>';
LESS        : '<';
CURLYBL      : '{';
CURLYBR    : '}';
SQUAREBL    : '[';
SQUAREBR : ']';
COLON       : ':';
DOT : '.';
COMMA : ',';
QUESTIONMARK: '?';



//// Reserved Words

BREAK : 'break';
CLASS : 'class';
CONST : 'const';
ELSE : 'else';
IF: 'if';
NEW: 'new';
READ: 'read';
RETURN : 'return';
VOID : 'void';
WHILE: 'while';
WRITE: 'write';
TRUE: 'true';
FALSE: 'false';

//// NUMBERS AND INDENTIFIERS

NUM: [1-9][0-9]* | '0';

fragment LETTER : [A-Za-z];

IDENT: (LETTER|[_])(LETTER|NUM|[_])*;

//// Char Const

fragment PRINTABLE_CHAR: (LETTER | [0-9] | [!"#$%&'()*+,\-./:;<=>?@]);

CHARCONST : (SQMARK(PRINTABLE_CHAR | '\\n' | '\\r')(SQMARK));

COMMENT : (('//'~[\r\n]*[\n\r]) | ('/*'[.]*'*/'))+ -> skip;

WS : [ \r\t\n]+ -> skip;


