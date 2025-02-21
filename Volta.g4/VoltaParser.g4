parser grammar VoltaParser;

options {
    tokenVocab = VoltaScanner;
}

program                         :   CLASS ident (constDecl | varDecl | classDecl)* CURLYBL methodDecl* CURLYBR EOF      #programAST;

constDecl                       :   CONST type ident EQUAL ( NUM | CHARCONST | STRING) SEMICOLON                        #constDeclAST;

varDecl                         :   type ident (COMMA ident)* SEMICOLON                                                 #varDeclAST;

classDecl                       :   CLASS ident CURLYBL (varDecl)* CURLYBR                                                 #classDeclAST;

methodDecl                      :   (type | VOID) ident BL formPars? BR (varDecl)* block                                #methodDeclAST;

formPars                        :   type ident ((COMMA) type ident)*                                                    #formParsAST;

type                            :   ident (SQUAREBL SQUAREBR)?                                                          #typeAST;

statement                       :   designator (BL actPars? BR) SEMICOLON                                               #callStatementAST
                                     | designator (EQUAL expr) SEMICOLON                                                #assignStatementAST
                                     | designator (ADDADD | SUBSUB) SEMICOLON                                           #addSubStatementAST
                                     | IF BL condition BR statement (ELSE statement)?                                   #ifStatementAST
                                     | FOR BL expr SEMICOLON condition SEMICOLON statement? BR statement               #forStatementAST
                                     | WHILE BL condition BR statement                                                  #whileStatementAST
                                     | BREAK SEMICOLON                                                                  #breakStatementAST
                                     | switch                                                                           #switchStatementAST
                                     | RETURN expr? SEMICOLON                                                           #returnStatementAST
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

factor                          :   designator (BL actPars? BR)?                                                        #identOrCallFactorAST
                                      | NUM                                                                             #numFactorAST
                                      | CHARCONST                                                                       #charConstFactorAST
                                      | STRING                                                                          #stringFactorAST
                                      | (TRUE|FALSE)                                                                    #booleanFactorAST
                                      | NEW ident (SQUAREBL expr SQUAREBR)?                                             #newFactorAST
                                      | BL expr BR                                                                      #bracketFactorAST
                                      | NULL                                                                            #nullFactorAST;

designator                      :   ident (DOT ident | SQUAREBL expr SQUAREBR)*                                         #designatorAST;

mulop                           :   MUL | DIV | MDIV                                                                    #mulopAST;

addop                           :   (ADD | SUB)                                                                         #addopAST;

relop                           :   EQUALEQUAL                                                                          #equalEqualRelopAST
                                    | NOTEQUAL                                                                          #notEqualRelopAST
                                    | GREATEREQUAL                                                                      #greaterEqualRelopAST
                                    | LESSEQUAL                                                                         #lessEqualRelopAST
                                    | GREATER                                                                           #greaterRelopAST
                                    | LESS                                                                              #lessRelopAST;

boolean
locals [bool value=false] 
                            : TRUE | FALSE                                                                                                  #booleanAST;

switch
locals [string typeString=null]                          
                            :   SWITCH BL(expr)BR 
                                    CURLYBL 
                                        (case)*
                                        (DEFAULT COLON
                                            (statement)?
                                        )?
                                    CURLYBR                                                                             #switchAST;

case
locals [string typeString=null]
                        : CASE (NUM | CHARCONST | STRING | boolean) COLON 
                                            (statement)?                                                                #caseAST;

ident
locals [ParserRuleContext decl=null]
                    : IDENT                                                                                             #identAST;
                                