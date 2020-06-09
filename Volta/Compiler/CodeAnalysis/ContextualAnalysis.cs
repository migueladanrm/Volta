﻿using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Volta.Compiler.CodeAnalysis
{
    class ContextualAnalysis : AbstractParseTreeVisitor<object>, IVoltaParserVisitor<object>
    {
        List<string> types;

        IdentificationTable identificationTable;

        public ContextualAnalysis() {
            types = new List<string>() { 
                "none",
                "int",
                "char",
                "float",
                "bool",
                "string"
            };

            identificationTable = new IdentificationTable();
            Errors = new List<VoltaCompilerError>();
        }

        public void InsertError(IToken token, int line, int charPositionInLine, string msg)
        {
            VoltaContextualError error = new VoltaContextualError(token, line, charPositionInLine, msg);
            Errors.Add(error);
        }

        public bool ExistIdent(string id, bool inThisLevel)
        {
            return identificationTable.Find(id, inThisLevel) != null;
        }

        public List<VoltaCompilerError> Errors { get; private set; }

        public object VisitIdentAST([NotNull] VoltaParser.IdentASTContext context)
        {
            if(context.IDENT() != null)
            {
                Identifier identifier = identificationTable.Find(context.IDENT().GetText(), false);
                if(identifier != null){
                    context.decl = identifier.Declaration;
                }
                return context;
            }
            return null;
        }

        public object VisitActParsAST([NotNull] VoltaParser.ActParsASTContext context)
        {
            var callFactor = context.Parent is VoltaParser.IdentOrCallFactorASTContext ? (VoltaParser.IdentOrCallFactorASTContext)context.Parent : null;
            var callStatement = context.Parent is VoltaParser.CallStatementASTContext ? (VoltaParser.CallStatementASTContext)context.Parent : null;

            MethodIdentifier methodIdentifier = null;
            if (callFactor != null)
                methodIdentifier = (MethodIdentifier)Visit(callFactor.designator());
            else if (callStatement != null)
                methodIdentifier = (MethodIdentifier)Visit(callStatement.designator());
            
            if(methodIdentifier == null)
            {
                Debug.WriteLine("Es nulo");
                return null;
            }

            VoltaParser.FormParsASTContext originalPars = methodIdentifier.FormPars;

            if(context.expr().Length != originalPars.ident().Length)
            {
                InsertError(context.Start, context.Start.Line, context.Start.Column, "El método '" + methodIdentifier.Id + "' recibe " + originalPars.ident().Length + " parámetros, y se recibieron " + context.expr().Length);
            }
            else
            {
                for (int i = 0; i < originalPars.ident().Length; i++)
                {
                    string exprType = (string)Visit(context.expr()[i]);
                    if(exprType == null)
                    {
                        return null;
                    }
                    else if (exprType != originalPars.type()[i].GetText())
                    {
                        InsertError(context.expr()[i].Start, context.expr()[i].Start.Line, context.expr()[i].Start.Column,
                            
                            $"El tipo de la expresión '{ context.expr()[i].GetText()}' es '" + exprType + "', y se esperaba el tipo '" + originalPars.type()[i].GetText() + "'");
                    }
                }
            }
            
            

            return null;
        }

        public object VisitAddopAST([NotNull] VoltaParser.AddopASTContext context)
        {
            VisitChildren(context); return null;
            
        }

        public object VisitBlockAST([NotNull] VoltaParser.BlockASTContext context)
        {
            VisitChildren(context); return null;
            
        }

        public object VisitBlockStatementAST([NotNull] VoltaParser.BlockStatementASTContext context)
        {
            identificationTable.OpenLevel();
            VisitChildren(context);
            identificationTable.CloseLevel();
            return null;
        }

        public object VisitBooleanFactorAST([NotNull] VoltaParser.BooleanFactorASTContext context)
        {
            //return context.TRUE() != null;
            return "bool";
        }

        public object VisitBracketFactorAST([NotNull] VoltaParser.BracketFactorASTContext context)
        {
            return Visit(context.expr());
        }

        public object VisitBreakStatementAST([NotNull] VoltaParser.BreakStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitIdentOrCallFactorAST([NotNull] VoltaParser.IdentOrCallFactorASTContext context)
        {
            Identifier identifier = (Identifier)Visit(context.designator());
            if (identifier != null && identifier is MethodIdentifier && context.BL() != null)
            {
                MethodIdentifier methodId = identifier as MethodIdentifier;
                if (context.actPars() != null)
                {
                    Visit(context.actPars());
                    return methodId.Type;
                }
                else if (methodId.FormPars != null)
                {
                    InsertError(context.BL().Symbol, context.BL().Symbol.Line, context.BL().Symbol.Column,
                        "El método '" + methodId.Id + "' recibe " + methodId.FormPars.ident().Length +
                        (methodId.FormPars.ident().Length > 1 ? " parámetros" : " parámetro") + ", y no se recibió ninguno");
                }
            }
            else if(identifier != null)
            {
                return identifier.Type;
            }
            else if(identifier == null)
            {
                IToken token = context.BL() != null ? context.BL().Symbol : ((VoltaParser.IdentASTContext)((VoltaParser.DesignatorASTContext)context.designator()).ident()[0]).IDENT().Symbol;
                InsertError(token, token.Line, token.Column, "El identificador '" + ((VoltaParser.DesignatorASTContext)context.designator()).ident()[0].GetText() + "' no está definido");
            }
            else
            {
                IToken token = context.BL() != null ? context.BL().Symbol : ((VoltaParser.IdentASTContext)((VoltaParser.DesignatorASTContext)context.designator()).ident()[0]).IDENT().Symbol;
                InsertError(token, token.Line, token.Column, "El identificador '" + ((VoltaParser.DesignatorASTContext)context.designator()).ident()[0].GetText() + "' no corresponde a un método");
            }
            return null;
            
        }

        public object VisitCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context)
        {
            
            return "char";
        }

        public object VisitClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context)
        {
            VoltaParser.IdentASTContext ident = (VoltaParser.IdentASTContext) Visit(context.ident());
            if (!ExistIdent(ident.GetText(), true))
            {
                
                List<VoltaParser.VarDeclASTContext> varDecls = new List<VoltaParser.VarDeclASTContext>(context.varDecl().ToList().Cast<VoltaParser.VarDeclASTContext>());

                ClassIdentifier classIdentifier = new ClassIdentifier(ident.IDENT().GetText(), ident.IDENT().Symbol, identificationTable.getLevel(), types[0], context, varDecls);

                identificationTable.Insert(classIdentifier);
                types.Add(ident.GetText());
            }
            else
            {
                InsertError(ident.IDENT().Symbol, ident.IDENT().Symbol.Line, ident.IDENT().Symbol.Column, "El identificador " + ident.IDENT().Symbol.Text + " ya fue declarado en este scope");
            }
            return null;
        }

        public object VisitCondFactAST([NotNull] VoltaParser.CondFactASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitConditionAST([NotNull] VoltaParser.ConditionASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitCondTermAST([NotNull] VoltaParser.CondTermASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitConstDeclAST([NotNull] VoltaParser.ConstDeclASTContext context)
        {
            string type = (string)Visit(context.type());
            if (type != null)
            {
                VoltaParser.IdentASTContext ident = (VoltaParser.IdentASTContext) Visit(context.ident());
                if (ident != null)
                {
                    if (!ExistIdent(ident.IDENT().Symbol.Text, true))
                    {
                        Identifier identifier = new ConstIdentifier(ident.IDENT().GetText(), ident.IDENT().Symbol, identificationTable.getLevel(), type, context);
                        identificationTable.Insert(identifier);



                        if ((context.NUM() != null && 
                                ((context.NUM().GetText().Split('.').Length > 1 && type != "float") || 
                                (context.NUM().GetText().Split('.').Length == 1 && type != "int"))) 
                            ||
                            (context.STRING() != null && type != "string") ||
                            (context.CHARCONST() != null && type != "char"))
                        {
                            InsertError(context.EQUAL().Symbol, context.EQUAL().Symbol.Line, context.EQUAL().Symbol.Column, "El tipo para la constante " + ident.GetText() + " no coincide con el tipo de " + context.GetText().Split('=').Last());
                        }
                    }
                    else
                    {
                        InsertError(ident.IDENT().Symbol, ident.IDENT().Symbol.Line, ident.IDENT().Symbol.Column, "El identificador " + ident.IDENT().Symbol.Text + " ya fue declarado en este scope");
                    }
                }
            }
            return null;
        }

        public object VisitDesignatorAST([NotNull] VoltaParser.DesignatorASTContext context)
        {
            if (context.ident().Length == 1 && context.SQUAREBL().Length == 0 && context.DOT().Length == 0)
            {
                if (ExistIdent(context.ident()[0].GetText(), false))
                {
                    Visit(context.ident()[0]);
                    return identificationTable.Find(context.ident()[0].GetText(), false);
                }
            }
            return null;
        }

        public object VisitEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitExprAST([NotNull] VoltaParser.ExprASTContext context)
        {
            if(context.term().Length == 1)
            {
                string type = (string)Visit(context.term()[0]);
                Debug.WriteLine("El tipo del término es " + type);
                if(type != "int" && type != "float")
                {
                    Debug.WriteLine(context.SUB());
                    if(context.SUB() == null)
                    {
                        return type;
                    }
                    else if(type != null)
                    {
                        InsertError(context.SUB().Symbol, context.SUB().Symbol.Line, context.SUB().Symbol.Column, "La expresión '" + context.term()[0].GetText() + "'no es un número y no se puede negar");
                        return null;
                    }
                }
                else
                {
                    return type;
                }
            }
            return null;
        }

        public object VisitFormParsAST([NotNull] VoltaParser.FormParsASTContext context)
        {
            var types = new List<VoltaParser.TypeASTContext>(context.type().ToList().Cast<VoltaParser.TypeASTContext>());
            var idents = new List<VoltaParser.IdentASTContext>(context.ident().ToList().Cast<VoltaParser.IdentASTContext>());

            for(int i = 0; i < types.Count; i++)
            {
                var type = Visit(types[i]) as string;

                if(!ExistIdent(idents[i].IDENT().Symbol.Text, true)){
                    Identifier identifier = new VarIdentifier(idents[i].IDENT().Symbol.Text, idents[i].IDENT().Symbol, identificationTable.getLevel(), type, context);
                    identificationTable.Insert(identifier);
                }
                else
                {
                    InsertError(idents[i].IDENT().Symbol, idents[i].IDENT().Symbol.Line, idents[i].IDENT().Symbol.Column, "El identificador " + idents[i].IDENT().Symbol.Text + " ya fue declarado en los parámetros");
                }
            }
            return context;
        }

        public object VisitForStatementAST([NotNull] VoltaParser.ForStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitGreaterEqualRelopAST([NotNull] VoltaParser.GreaterEqualRelopASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitGreaterRelopAST([NotNull] VoltaParser.GreaterRelopASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitIfStatementAST([NotNull] VoltaParser.IfStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitLessEqualRelopAST([NotNull] VoltaParser.LessEqualRelopASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitLessRelopAST([NotNull] VoltaParser.LessRelopASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitMethodDeclAST([NotNull] VoltaParser.MethodDeclASTContext context)
        {
            string type = "void";
            if (context.type() != null)
                type = (string) Visit(context.type());

            VoltaParser.IdentASTContext ident = (VoltaParser.IdentASTContext)Visit(context.ident());

            if(ident != null)
            {
                if (!ExistIdent(ident.IDENT().Symbol.Text, true)){
                    MethodIdentifier identifier = new MethodIdentifier(ident.IDENT().Symbol.Text, ident.IDENT().Symbol, identificationTable.getLevel(), type,
                            context, (VoltaParser.FormParsASTContext)context.formPars());

                    identificationTable.Insert(identifier);

                    identificationTable.OpenLevel(); // Para los parámetros ya existe un scope nuevo
                    if(context.formPars() != null)
                        Visit(context.formPars()); //Cuando se visitan los parámetros y se encuentra un error, ellos lo reportan
                    

                    if(context.varDecl() != null)
                        context.varDecl().ToList().ForEach(varDecl => Visit(varDecl));

                    if (context.block() != null)
                        Visit(context.block());
                    
                    identificationTable.CloseLevel();
                
                }
                else
                {
                    InsertError(ident.IDENT().Symbol, ident.IDENT().Symbol.Line, ident.IDENT().Symbol.Column, "El identificador " + ident.IDENT().Symbol.Text + " ya fue declarado en este scope");
                }
            }

            
            return null;
        }

        public object VisitMulop([NotNull] VoltaParser.MulopContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitNewFactorAST([NotNull] VoltaParser.NewFactorASTContext context)
        {
            if (types.Contains(context.ident().GetText()))
            {
                return context.ident().GetText();
            }
            InsertError(context.ident().Start, context.ident().Start.Line, context.ident().Start.Column,
                "No se puede crear una instancia de '" + context.ident().GetText() + "' porque no es un tipo");
            return null;
        }

        public object VisitNotEqualRelopAST([NotNull] VoltaParser.NotEqualRelopASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitNumFactorAST([NotNull] VoltaParser.NumFactorASTContext context)
        {
            return context.NUM().GetText().Split('.').Length > 1 ? "float" : "int";
        }

        public object VisitProgramAST([NotNull] VoltaParser.ProgramASTContext context)
        {
            identificationTable.OpenLevel();
            VisitChildren(context); 
            identificationTable.CloseLevel();
            return null;
        }

        public object VisitReadStatementAST([NotNull] VoltaParser.ReadStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitReturnStatementAST([NotNull] VoltaParser.ReturnStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitSemicolonStatementAST([NotNull] VoltaParser.SemicolonStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitStringFactorAST([NotNull] VoltaParser.StringFactorASTContext context)
        {
            return "string";
        }

        public object VisitSwitchAST([NotNull] VoltaParser.SwitchASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitSwitchStatementAST([NotNull] VoltaParser.SwitchStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitTermAST([NotNull] VoltaParser.TermASTContext context)
        {
            if (context.factor().Length == 1)
            {
                return Visit(context.factor()[0]);
            }
            else if (context.factor().Length > 1)
            {
                bool allInts = true;
                bool allNumbers = context.factor().ToList().All(factor => { 
                    string type = (string)Visit(factor);
                    if (type != "int" && type != "float"){
                        InsertError(factor.Start, factor.Start.Line, factor.Start.Column,
                            "El factor '" + factor.GetText() + "' no es un número. Solo se permite el uso de '*', '/' o '%' con floats o ints");
                    }
                    allInts = allInts && type == "int";
                    return type == "int" || type == "float";
                });

                if (allNumbers)
                {
                    return allInts ? "int" : "float";
                }
            }
            return null;
        }

        public object VisitTypeAST([NotNull] VoltaParser.TypeASTContext context)
        {
            VoltaParser.IdentASTContext ident = (VoltaParser.IdentASTContext)Visit(context.ident());

            if (ident != null)
            {
                if (types.Contains(ident.IDENT().GetText()))
                {
                    return context.ident().GetText();
                }
                else
                {
                    InsertError(ident.IDENT().Symbol, ident.IDENT().Symbol.Line, ident.IDENT().Symbol.Column, "El tipo " + ident.IDENT().GetText() + " no existe");

                }
            }
            return null;
        }

        public object VisitVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context)
        {


            string type = (string) Visit(context.type());
            if(type != null)
            {
                context.ident().ToList().ForEach((VoltaParser.IdentContext identC) =>
                {
                    ITerminalNode ident = ((VoltaParser.IdentASTContext) identC).IDENT();
                    if (!ExistIdent(ident.Symbol.Text, true)){
                        Identifier identifier = new VarIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context);
                        identificationTable.Insert(identifier);
                    }
                    else
                    {
                        InsertError(ident.Symbol, ident.Symbol.Line, ident.Symbol.Column, "El identificador " + ident.Symbol.Text + " ya fue declarado en este scope");
                    }
                });
            }
            return null;
        }

        public object VisitWhileStatementAST([NotNull] VoltaParser.WhileStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitWriteStatementAST([NotNull] VoltaParser.WriteStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitCallStatementAST([NotNull] VoltaParser.CallStatementASTContext context)
        {
            Identifier identifier = (Identifier) Visit(context.designator());
            if (identifier != null && identifier is MethodIdentifier)
            {
                MethodIdentifier methodId = (MethodIdentifier) identifier;
                if(context.actPars() != null)
                {
                    Visit(context.actPars());
                }
                else if(methodId.FormPars != null)
                {
                    InsertError(context.BL().Symbol, context.BL().Symbol.Line, context.BL().Symbol.Column,
                        "El método '" + methodId.Id + "' recibe " + methodId.FormPars.ident().Length + 
                        (methodId.FormPars.ident().Length > 1?" parámetros": " parámetro") + ", y no se recibió ninguno");
                }
            }
            else
            {
                IToken token = context.BL() != null ? context.BL().Symbol : ((VoltaParser.IdentASTContext)((VoltaParser.DesignatorASTContext)context.designator()).ident()[0]).IDENT().Symbol;
                InsertError(token, token.Line, token.Column, "El identificador " + ((VoltaParser.DesignatorASTContext)context.designator()).ident()[0].GetText() + " no corresponde a un método");   
            }
            return identifier.Type;
        }

        public object VisitAssignStatementAST([NotNull] VoltaParser.AssignStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitAddsubStatementAST([NotNull] VoltaParser.AddsubStatementASTContext context)
        {
            VisitChildren(context); return null;
        }
    }
}