parser grammar Parser1;

options {
    tokenVocab = Scanner1;
}

program             : singleCommand EOF;
command             : singleCommand (PyCOMA singleCommand)* ;
singleCommand       : IDENT ASSIGN expression
                    | IDENT PIZQ  expression PDER
                    | IF expression THEN singleCommand ELSE singleCommand
                    | WHILE expression DO singleCommand
                    | LET declaration IN singleCommand
                    | BEGIN command END ;
declaration         : singleDeclaration (PyCOMA singleDeclaration)* ;
singleDeclaration   : CONST IDENT VIR expression
    	            | VAR IDENT DOSPUN typeDenoter ;
typeDenoter         : IDENT ;
expression          : primaryExpression (operator primaryExpression)* ;
primaryExpression   : NUM | IDENT | PIZQ expression PDER ;
operator            : SUM | SUB | MUL | DIV ;
