lexer grammar VoltaScanner;

//// Symbols

NOT                                     :   '!';
fragment QMARK                          :   '"';
fragment SQMARK                         :   '\'';
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
NULL                                    :   'null';

// Numbers & identifiers

IDENT                                   :   (LETTER|[_])(LETTER|[0-9]|[_])*;
NUM                                     :   ([1-9][0-9]* | '0')(DOT[0-9]*)?;
CHARCONST                               :   (SQMARK(PRINTABLE_CHAR | '\\n' | '\\r')(SQMARK));
STRING                                  :   QMARK(~["]|'\\"')*QMARK;
COMMENT                                 :   (('//'~[\r\n]*[\n\r]) | ('/*'.*?'*/'))+ -> skip;

// Fragments & others
WS                                      :   [ \r\t\n]+ -> skip;
fragment LETTER                         :   [A-Za-z];
fragment PRINTABLE_CHAR                 :   (LETTER | [0-9] | [!"#$%&'()*+,\-./:;<=>?@ ]);


