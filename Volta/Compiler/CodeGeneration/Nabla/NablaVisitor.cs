using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using static VoltaParser;
using System.Collections.Generic;

namespace Volta.Compiler.CodeGeneration.Nabla
{
    public class NablaVisitor : AbstractParseTreeVisitor<object>, IVoltaParserVisitor<object>
    {
        private ModuleBuilder moduleBuilder;
        private TypeBuilder rootType;
        private List<TypeBuilder> childTypes;

        private MethodBuilder methodBuilder;

        public NablaVisitor(ref ModuleBuilder moduleBuilder) {
            this.moduleBuilder = moduleBuilder;
        }

        public Type GetTypeOf(string typeString)
        {

            switch(typeString)
            {
                case "int":
                    return typeof(int);
                case "float":
                    return typeof(float);
                case "char":
                    return typeof(char);
                case "string":
                    return typeof(string);
                case "bool":
                    return typeof(bool);
                case "int[]":
                    return typeof(int[]);
                case "char[]":
                    return typeof(char[]);
                case "float[]":
                    return typeof(float[]);
                case "bool[]":
                    return typeof(bool[]);
                case "string[]":
                    return typeof(string[]);
                default:
                    {
                        return childTypes.Find(typeBuilder => typeBuilder.GetType().Name.Equals(typeString)).GetType();
                    }
                    
            }
        }

        public object VisitActParsAST([NotNull] ActParsASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitAddopAST([NotNull] AddopASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitAddSubStatementAST([NotNull] AddSubStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitAssignStatementAST([NotNull] AssignStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitBlockAST([NotNull] BlockASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitBlockStatementAST([NotNull] BlockStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitBoolean([NotNull] BooleanContext context)
        {
            throw new NotImplementedException();
        }

        public object VisitBooleanFactorAST([NotNull] BooleanFactorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitBracketFactorAST([NotNull] BracketFactorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitBreakStatementAST([NotNull] BreakStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitCallStatementAST([NotNull] CallStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitCaseAST([NotNull] CaseASTContext context)
        {
            throw new NotImplementedException();
        }

        public object VisitCharConstFactorAST([NotNull] CharConstFactorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitClassDeclAST([NotNull] ClassDeclASTContext context) {
            rootType = moduleBuilder.DefineType(Visit(context.ident()) as string);
            context.varDecl().ToList().ForEach(vd => Visit(vd));

            throw new NotImplementedException();
        }

        public object VisitCondFactAST([NotNull] CondFactASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitConditionAST([NotNull] ConditionASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitCondTermAST([NotNull] CondTermASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitConstDeclAST([NotNull] ConstDeclASTContext context) {
            var field = rootType.DefineField(Visit(context.ident()) as string,
                NablaHelper.ParseType(Visit(context.type()) as string),
                FieldAttributes.HasDefault);

            return null;
        }

        public object VisitDesignatorAST([NotNull] DesignatorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitEqualEqualRelopAST([NotNull] EqualEqualRelopASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitExprAST([NotNull] ExprASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitFormParsAST([NotNull] FormParsASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitForStatementAST([NotNull] ForStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitGreaterEqualRelopAST([NotNull] GreaterEqualRelopASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitGreaterRelopAST([NotNull] GreaterRelopASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitIdentAST([NotNull] IdentASTContext context) {
            return context.IDENT().Symbol.Text;
        }

        public object VisitIdentOrCallFactorAST([NotNull] IdentOrCallFactorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitIfStatementAST([NotNull] IfStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitLessEqualRelopAST([NotNull] LessEqualRelopASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitLessRelopAST([NotNull] LessRelopASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitMethodDeclAST([NotNull] MethodDeclASTContext context) {

            var name = Visit(context.ident()) as string;

            var typeString = Visit(context.type()) as string;

            var type = GetTypeOf(typeString);

            var paramTypes = Visit(context.formPars()) as Type[];

            methodBuilder = rootType.DefineMethod(name, MethodAttributes.Public, type, paramTypes )
        }

        public object VisitMulop([NotNull] MulopContext context) {
            throw new NotImplementedException();
        }

        public object VisitNewFactorAST([NotNull] NewFactorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitNotEqualRelopAST([NotNull] NotEqualRelopASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitNullFactorAST([NotNull] NullFactorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitNumFactorAST([NotNull] NumFactorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitProgramAST([NotNull] ProgramASTContext context) {
            rootType = moduleBuilder.DefineType(context.ident().GetText(), TypeAttributes.Class, typeof(object));
            VisitChildren(context);

            return null;
        }

        public object VisitReadStatementAST([NotNull] ReadStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitReturnStatementAST([NotNull] ReturnStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitSemicolonStatementAST([NotNull] SemicolonStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitStringFactorAST([NotNull] StringFactorASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitSwitchAST([NotNull] SwitchASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitSwitchStatementAST([NotNull] SwitchStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitTermAST([NotNull] TermASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitTypeAST([NotNull] TypeASTContext context) {
            return context.ident().GetText();
        }

        public object VisitVarDeclAST([NotNull] VarDeclASTContext context) {
            var varType = Visit(context.type()) as string;
            context.ident().ToList().ForEach(ident => {
                var identifier = Visit(ident) as string;
                var field = rootType.DefineField(identifier, NablaHelper.ParseType(varType), FieldAttributes.InitOnly);
            });

            return null;
        }

        public object VisitWhileStatementAST([NotNull] WhileStatementASTContext context) {
            throw new NotImplementedException();
        }

        public object VisitWriteStatementAST([NotNull] WriteStatementASTContext context) {
            throw new NotImplementedException();
        }
    }
}