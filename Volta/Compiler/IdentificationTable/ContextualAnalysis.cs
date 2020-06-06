using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Volta.Compiler.IdentificationTable
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
                    context.decl = identifier.decl;
                }
            }
            return context;
        }

        public object VisitActParsAST([NotNull] VoltaParser.ActParsASTContext context)
        {
            
            VisitChildren(context); return null;
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

        public object VisitBolleanFactorAST([NotNull] VoltaParser.BolleanFactorASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitBracketFactorAST([NotNull] VoltaParser.BracketFactorASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitBreakStatementAST([NotNull] VoltaParser.BreakStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitCallFactorAST([NotNull] VoltaParser.CallFactorASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context)
        {
            
            VisitChildren(context); return null;
        }

        public object VisitClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context)
        {
            VoltaParser.IdentASTContext ident = (VoltaParser.IdentASTContext) Visit(context.ident());
            if (!ExistIdent(ident.GetText(), true))
            {

                List<VoltaParser.VarDeclASTContext> varDecls = new List<VoltaParser.VarDeclASTContext>(context.varDecl().ToList().Cast<VoltaParser.VarDeclASTContext>());
                ClassIdentifier classIdentifier = new ClassIdentifier(ident.IDENT().GetText(), ident.IDENT().Symbol, identificationTable.getLevel(), types[0], varDecls, context);

                identificationTable.Insert(classIdentifier);
                types.Add(ident.GetText());
            }
            else
            {
                InsertError(ident.IDENT().Symbol, ident.IDENT().Symbol.Line, ident.IDENT().Symbol.Column, "El identificador " + ident.IDENT().Symbol.Text + " ya fue declarado en este scope");
            }
            VisitChildren(context); 
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

                        if ((context.NUM() != null && type != "int") ||
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
            VisitChildren(context); return null;
        }

        public object VisitEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitExprAST([NotNull] VoltaParser.ExprASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitFormParsAST([NotNull] VoltaParser.FormParsASTContext context)
        {
            List<VoltaParser.TypeASTContext> types = new List<VoltaParser.TypeASTContext>(context.type().ToList().Cast<VoltaParser.TypeASTContext>());
            List<VoltaParser.IdentASTContext> idents = new List<VoltaParser.IdentASTContext>(context.ident().ToList().Cast<VoltaParser.IdentASTContext>());

            for(int i = 0; i < types.Count; i++)
            {
                string type = (string) Visit(types[i]);

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

            MethodIdentifier identifier = new MethodIdentifier(ident.IDENT().Symbol.Text, ident.IDENT().Symbol, identificationTable.getLevel(), type,
                    (VoltaParser.FormParsASTContext)context.formPars(), context);

            identificationTable.Insert(identifier);

            if (ident != null && !ExistIdent(ident.IDENT().Symbol.Text, true)){
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
            
            return null;
        }

        public object VisitMulop([NotNull] VoltaParser.MulopContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitNewFactorAST([NotNull] VoltaParser.NewFactorASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitNotEqualRelopAST([NotNull] VoltaParser.NotEqualRelopASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitNumFactorAST([NotNull] VoltaParser.NumFactorASTContext context)
        {
            VisitChildren(context); return null;
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
            VisitChildren(context); return null;
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
            VisitChildren(context); return null;
        }

        public object VisitTypeAST([NotNull] VoltaParser.TypeASTContext context)
        {
            VoltaParser.IdentASTContext ident = (VoltaParser.IdentASTContext)Visit(context.ident());

            if (ident.IDENT() != null)
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
                context.ident().ToList().ForEach(delegate (VoltaParser.IdentContext identC)
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
            VisitChildren(context); return null;
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
