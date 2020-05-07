lexer grammar VoltaScanner;

//// Symbols

NOT                                     :   '!';
QMARK                                   :   '"';
SQMARK                                  :   '\'';
HASH                                    :   '#';
DOLAR                                   :   '$';
AND                                     :   '&&';
OR                                      :   '||';
BL                                      :   '(';
BR                                      :   ')';
SEMICOLON                               :   ';';
ADD                                     :   '+';
ADDADD                                  :   '++';
SUB                                     :   '-';
SUBSUB                                  :   '--';
MUL                                     :   '*';
DIV                                     :   '/';
MDIV                                    :   '%';
EQUALEQUAL                              :   '==';
NOTEQUAL                                :   '!=';
GREATEREQUAL                            :   '>=';
LESSEQUAL                               :   '<=';
EQUAL                                   :   '=';
GREATER                                 :   '>';
LESS                                    :   '<';
CURLYBL                                 :   '{';
CURLYBR                                 :   '}';
SQUAREBL                                :   '[';
SQUAREBR                                :   ']';
COLON                                   :   ':';
DOT                                     :   '.';
COMMA                                   :   ',';
QUESTIONMARK                            :   '?';



//// Keywords
CLASS                                   :   'class';
CONST                                   :   'const';
ELSE                                    :   'else';
IF                                      :   'if';
NEW                                     :   'new';
READ                                    :   'read';
RETURN                                  :   'return';
VOID                                    :   'void';
WHILE                                   :   'while';
WRITE                                   :   'write';
TRUE                                    :   'true';
FALSE                                   :   'false';
FOR                                     :   'for';
SWITCH                                  :   'switch';
CASE                                    :   'case';
BREAK                                   :   'break';
DEFAULT                                 :   'default';

// Numbers & identifiers

IDENT                                   :   (LETTER|[_])(LETTER|NUM|[_])*;
NUM                                     :   [1-9][0-9]* | '0';
CHARCONST                               :   (SQMARK(PRINTABLE_CHAR | '\\n' | '\\r')(SQMARK));
STRING                                  :   QMARK(~["]|'\\"')*QMARK;
COMMENT                                 :   (('//'~[\r\n]*[\n\r]) | ('/*'[.]*'*/'))+ -> skip;

// Fragments & others
WS                                      :   [ \r\t\n]+ -> skip;
fragment LETTER                         :   [A-Za-z];
fragment PRINTABLE_CHAR                 :   (LETTER | [0-9] | [!"#$%&'()*+,\-./:;<=>?@]);


