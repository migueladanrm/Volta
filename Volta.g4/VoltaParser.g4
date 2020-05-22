parser grammar VoltaParser;

options {
    tokenVocab = VoltaScanner;
}

program                         :   CLASS IDENT (constDecl | varDecl | classDecl)* CURLYBL methodDecl* CURLYBR EOF      #programAST;

constDecl                       :   CONST type IDENT EQUAL ( NUM | CHARCONST | STRING) SEMICOLON                        #constDeclAST;

varDecl                         :   type IDENT (COMMA IDENT)* SEMICOLON                                                 #varDeclAST;

classDecl                       :   CLASS IDENT CURLYBL varDecl CURLYBR                                                 #classDeclAST;

methodDecl                      :   (type | VOID) IDENT BL formPars? BR (varDecl)* block                                #methodDeclAST;

formPars                        :   type IDENT ((COMMA) type IDENT)*                                                    #formParsAST;

type                            :   IDENT (SQUAREBL SQUAREBR)?                                                          #typeAST;

statement                       :   designator (EQUAL expr | BL actPars? BR | ADDADD | SUBSUB) SEMICOLON                #callORassignStatementAST
                                     | IF BL condition BR statement (ELSE statement)?                                   #ifStatementAST
                                     | FOR BL expr SEMICOLON condition? SEMICOLON statement? BR statement               #forStatementAST
                                     | WHILE BL condition BR statement                                                  #whileStatementAST
                                     | BREAK SEMICOLON                                                                  #breakStatementAST
                                     | switch                                                                           #switchStatementAST
                                     | RETURN expr? SEMICOLON                                                       #returnStatementAST
                                     | READ BL designator BR SEMICOLON                                                  #readStatementAST
                                     | WRITE BL expr (COMMA NUM)? BR SEMICOLON                                          #writeStatementAST
                                     | block                                                                            #blockStatementAST
                                     | SEMICOLON                                                                        #semicolonStatementAST;

block                           :   CURLYBL (statement | constDecl | varDecl)* CURLYBR                                  #blockAST;

actPars                         :   expr (COMMA expr)*                                                                  #actParsAST;

condition                       :   condTerm (OR condTerm)*                                                             #conditionAST;

condTerm                        :   condFact (AND condFact)*                                                            #condTermAST;

condFact                        :   expr relop expr                                                                     #condFactAST;

expr                            :   SUB? term (addop term)*                                                             #exprAST;

term                            :   factor (mulop factor)*                                                              #termAST;

factor                          :   designator (BL actPars? BR)?                                                        #callFactorAST
                                      | NUM                                                                             #numFactorAST
                                      | CHARCONST                                                                       #charConstFactorAST
                                      | STRING                                                                          #stringFactorAST
                                      | (TRUE|FALSE)                                                                    #bolleanFactorAST
                                      | NEW IDENT                                                                       #newFactorAST
                                      | BL expr BR                                                                      #bracketFactorAST;

designator                      :   IDENT (DOT IDENT | SQUAREBL expr SQUAREBR)*                                         #designatorAST;

mulop                           :   MUL | DIV | MDIV                                                                    #mulopAST;

addop                           :   (ADD | SUB)                                                                         #addopAST;

relop                           :   EQUALEQUAL                                                                          #equalEqualRelopAST
                                    | NOTEQUAL                                                                          #notEqualRelopAST
                                    | GREATEREQUAL                                                                      #greaterEqualRelopAST
                                    | LESSEQUAL                                                                         #lessEqualRelopAST
                                    | GREATER                                                                           #greaterRelopAST
                                    | LESS                                                                              #lessRelopAST;

switch                          :   SWITCH BL(IDENT | NUM | CHARCONST | STRING)BR 
                                    CURLYBL 
                                        (CASE (NUM | CHARCONST | STRING) COLON 
                                            (statement (BREAK SEMICOLON)?)?
                                        )*
                                        (DEFAULT COLON
                                            (statement (BREAK SEMICOLON)?)?
                                        )?
                                    CURLYBR                                                                             #switchAST;
                                