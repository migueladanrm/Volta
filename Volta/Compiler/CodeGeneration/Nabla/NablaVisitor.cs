using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using static VoltaParser;

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
            return null;
        }

        public object VisitAddopAST([NotNull] AddopASTContext context) {
            return null;
        }

        public object VisitAddSubStatementAST([NotNull] AddSubStatementASTContext context) {
            return null;
        }

        public object VisitAssignStatementAST([NotNull] AssignStatementASTContext context) {
            return null;
        }

        public object VisitBlockAST([NotNull] BlockASTContext context) {
            return null;
        }

        public object VisitBlockStatementAST([NotNull] BlockStatementASTContext context) {
            return null;
        }

        public object VisitBoolean([NotNull] BooleanContext context)
        {
            throw new NotImplementedException();
        }

        public object VisitBooleanFactorAST([NotNull] BooleanFactorASTContext context) {
            return null;
        }

        public object VisitBracketFactorAST([NotNull] BracketFactorASTContext context) {
            return null;
        }

        public object VisitBreakStatementAST([NotNull] BreakStatementASTContext context) {
            return null;
        }

        public object VisitCallStatementAST([NotNull] CallStatementASTContext context) {
            return null;
        }

        public object VisitCaseAST([NotNull] CaseASTContext context)
        {
            throw new NotImplementedException();
        }

        public object VisitCharConstFactorAST([NotNull] CharConstFactorASTContext context) {
            return null;
        }

        public object VisitClassDeclAST([NotNull] ClassDeclASTContext context) {
            rootType = moduleBuilder.DefineType(Visit(context.ident()) as string);
            context.varDecl().ToList().ForEach(vd => Visit(vd));

            return null;
        }

        public object VisitCondFactAST([NotNull] CondFactASTContext context) {
            return null;
        }

        public object VisitConditionAST([NotNull] ConditionASTContext context) {
            return null;
        }

        public object VisitCondTermAST([NotNull] CondTermASTContext context) {
            return null;
        }

        public object VisitConstDeclAST([NotNull] ConstDeclASTContext context) {
            var field = rootType.DefineField(Visit(context.ident()) as string,
                NablaHelper.ParseType(Visit(context.type()) as string),
                FieldAttributes.HasDefault);

            return null;
        }

        public object VisitDesignatorAST([NotNull] DesignatorASTContext context) {
            return null;
        }

        public object VisitEqualEqualRelopAST([NotNull] EqualEqualRelopASTContext context) {
            return null;
        }

        public object VisitExprAST([NotNull] ExprASTContext context) {
            return null;
        }

        public object VisitFormParsAST([NotNull] FormParsASTContext context) {
            return null;
        }

        public object VisitForStatementAST([NotNull] ForStatementASTContext context) {
            return null;
        }

        public object VisitGreaterEqualRelopAST([NotNull] GreaterEqualRelopASTContext context) {
            return null;
        }

        public object VisitGreaterRelopAST([NotNull] GreaterRelopASTContext context) {
            return null;
        }

        public object VisitIdentAST([NotNull] IdentASTContext context) {
            return context.IDENT().Symbol.Text;
        }

        public object VisitIdentOrCallFactorAST([NotNull] IdentOrCallFactorASTContext context) {
            return null;
        }

        public object VisitIfStatementAST([NotNull] IfStatementASTContext context) {
            return null;
        }

        public object VisitLessEqualRelopAST([NotNull] LessEqualRelopASTContext context) {
            return null;
        }

        public object VisitLessRelopAST([NotNull] LessRelopASTContext context) {
            return null;
        }

        public object VisitMethodDeclAST([NotNull] MethodDeclASTContext context) {
<<<<<<< HEAD

            var name = Visit(context.ident()) as string;

            var typeString = Visit(context.type()) as string;

            var type = GetTypeOf(typeString);

            var paramTypes = Visit(context.formPars()) as Type[];

            methodBuilder = rootType.DefineMethod(name, MethodAttributes.Public, type, paramTypes);

=======
>>>>>>> dev
            return null;
        }

        public object VisitMulop([NotNull] MulopContext context) {
            return null;
        }

        public object VisitNewFactorAST([NotNull] NewFactorASTContext context) {
            return null;
        }

        public object VisitNotEqualRelopAST([NotNull] NotEqualRelopASTContext context) {
            return null;
        }

        public object VisitNullFactorAST([NotNull] NullFactorASTContext context) {
            return null;
        }

        public object VisitNumFactorAST([NotNull] NumFactorASTContext context) {
            return null;
        }

        public object VisitProgramAST([NotNull] ProgramASTContext context) {
            rootType = moduleBuilder.DefineType(context.ident().GetText(), TypeAttributes.Class | TypeAttributes.Public);
            //VisitChildren(context);

            rootType.CreateType();

            return null;
        }

        public object VisitReadStatementAST([NotNull] ReadStatementASTContext context) {
            return null;
        }

        public object VisitReturnStatementAST([NotNull] ReturnStatementASTContext context) {
            return null;
        }

        public object VisitSemicolonStatementAST([NotNull] SemicolonStatementASTContext context) {
            return null;
        }

        public object VisitStringFactorAST([NotNull] StringFactorASTContext context) {
            return null;
        }

        public object VisitSwitchAST([NotNull] SwitchASTContext context) {
            return null;
        }

        public object VisitSwitchStatementAST([NotNull] SwitchStatementASTContext context) {
            return null;
        }

        public object VisitTermAST([NotNull] TermASTContext context) {
            return null;
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
            return null;
        }

        public object VisitWriteStatementAST([NotNull] WriteStatementASTContext context) {
            return null;
        }
    }
}