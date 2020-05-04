parser grammar VoltaParser;

options {
    tokenVocab = VoltaScanner;
}

program                         :   CLASS IDENT (constDecl | varDecl | classDecl)* CURLYBL methodDecl* CURLYBR EOF;

constDecl                       :   CONST type IDENT EQUAL ( NUM | CHARCONST) SEMICOLON;

varDecl                         :   type IDENT (COMMA IDENT)* SEMICOLON;

classDecl                       :   CLASS IDENT CURLYBL varDecl CURLYBR;

methodDecl                      :   (type | VOID) IDENT BL formPars? BR (varDecl)* block;

formPars                        :   type IDENT ((COMMA) type IDENT)*;

type                            :   IDENT (SQUAREBL SQUAREBR)?;

statement                       :   designator (EQUAL expr | BL actPars? BR | ADDADD | SUBSUB) SEMICOLON
                                     | IF BL condition BR statement (ELSE statement)?
                                     | FOR BL expr SEMICOLON condition? SEMICOLON statement? BR statement
                                     | WHILE BL condition BR statement
                                     | BREAK SEMICOLON
                                     | RETURN expr?
                                     | READ BL designator BR SEMICOLON
                                     | WRITE BL expr (COMMA NUM)? BR SEMICOLON
                                     | block
                                     | SEMICOLON;

block                           :   CURLYBL statement* CURLYBR;

actPars                         :   expr (COMMA expr)*;

condition                       :   condTerm (OR condTerm)*;

condTerm                        :   condFact (AND condFact)*;

condFact                        :   expr relop expr;

expr                            :   SUB? term (addop term)*;

term                            :   factor (mulop factor)*;

factor                          :   designator (BL actPars? BR)?
                                      | NUM
                                      | CHARCONST
                                      | (TRUE|FALSE)
                                      | NEW IDENT
                                      | BL expr BR;

designator                      :   IDENT (DOT IDENT | SQUAREBL expr SQUAREBR)*;

mulop                           :   MUL | DIV | MDIV;

addop                           :   (ADD | SUB);

relop                           :   EQUALEQUAL
                                    | NOTEQUAL
                                    | GREATEREQUAL
                                    | LESSEQUAL
                                    | GREATER
                                    | LESS;