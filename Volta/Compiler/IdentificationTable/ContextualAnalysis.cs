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

        public List<VoltaCompilerError> Errors { get; private set; }

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
            VisitChildren(context); return null;
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

        public object VisitCallORassignStatementAST([NotNull] VoltaParser.CallORassignStatementASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context)
        {
            VisitChildren(context); return null;
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
                ITerminalNode ident = context.IDENT();
                if (ident != null)
                {
                    if (identificationTable.Find(ident.Symbol.Text, true) == null)
                    {
                        Identifier identifier = new VarCostIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type);
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
                        InsertError(context.IDENT().Symbol, context.IDENT().Symbol.Line, context.IDENT().Symbol.Column, "El identificador " + ident.Symbol.Text + " ya fue declarado en este scope");
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
            VisitChildren(context); return null;
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
            VisitChildren(context); return null;
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
            VisitChildren(context); return null;
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
            if (context.IDENT() != null)
            {
                if (types.Contains(context.IDENT().GetText()))
                {
                    return context.IDENT().GetText();
                }
                else
                {
                    InsertError(context.IDENT().Symbol, context.IDENT().Symbol.Line, context.IDENT().Symbol.Column, "El tipo " + context.IDENT().GetText() + " no existe");

                }
            }
            VisitChildren(context); 
            return null;
        }

        public object VisitVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context)
        {
            string type = (string) Visit(context.type());
            if(type != null)
            {
                context.IDENT().ToList().ForEach(delegate (ITerminalNode ident)
                {
                    if (identificationTable.Find(ident.Symbol.Text, true) == null){
                        Identifier identifier = new VarCostIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type);
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

        
    }
}
