lexer grammar Scanner1;


//Símbolos
PyCOMA      : ';';
ASSIGN   : ':=';
PIZQ    : '(';
PDER    : ')';
VIR      : '~';
DOSPUN     : ':';
SUM     : '+';
SUB     : '-';
MUL     : '*';
DIV     : '/';


//Palabras reservadas
IF      : 'if';
THEN    : 'then';
ELSE    : 'else';
WHILE   : 'while';
DO      : 'do';
LET     : 'let';
IN      : 'in';
BEGIN   : 'begin';
END     : 'end';
CONST   : 'const';
VAR     : 'var';


//Números
NUM: [1-9][0-9]* | '0';

//Identificadores
IDENT: [a-z]([a-z]|[0-9])*;

WS : [ \r\t\n]+ -> skip;
