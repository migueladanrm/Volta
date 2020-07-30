using Antlr4.Runtime;
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
        public TypeBuilder rootType;
        private List<TypeBuilder> childTypes;

        private MethodBuilder methodBuilder;


        private ILGenerator emitter = null;

        private int scope = 0;

        private List<(string name, LocalBuilder localBuilder)> localVariables = new List<(string name, LocalBuilder localBuilder)>();

        private List<MethodInfo> methods = new List<MethodInfo>();
        private List<FieldInfo> fields = new List<FieldInfo>();
        private List<(string type, ParameterBuilder pb)> currentParameters = new List<(string type, ParameterBuilder pb)>();

        private TypeBuilder tmpType = null;

        public MethodBuilder mainMethod;

        public NablaVisitor(ref ModuleBuilder moduleBuilder) {
            this.moduleBuilder = moduleBuilder;
        }

        public Type GetTypeOf(string typeString)
        {

            switch(typeString)
            {
                case "none":
                    return typeof(string);
                case "void":
                    return typeof(void);
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

        public MethodInfo GetMethod(string name)
        {
            return methods.Find(method => method.Name.Equals(name));
        }

        public FieldInfo GetField(string name)
        {
            return fields.Find(field => field.Name.Equals(name));
        }

        public (string type, ParameterBuilder pb) GetParameter(string name)
        {
            return currentParameters.Find(param => param.pb.Name.Equals(name));
        }

        public LocalVariableInfo GetLocal(string name)
        {
            return localVariables.FindLast(local => local.name.Equals(name)).localBuilder;
        }

        public object GetFirstVariable(string name)
        {
            return GetLocal(name) as object ?? GetParameter(name) as object ?? GetField(name) as object;
        }

        public object VisitActParsAST([NotNull] ActParsASTContext context) {

            context.expr().ToList().ForEach(expr => Visit(expr));
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
            emitter.BeginScope();
            VisitChildren(context);
            emitter.EndScope();
            return null;
        }

        public object VisitBlockStatementAST([NotNull] BlockStatementASTContext context) {
            Visit(context.block());
            
            return null;
        }

        public object VisitBoolean([NotNull] BooleanContext context)
        {
            throw new NotImplementedException();
        }

        public object VisitBooleanFactorAST([NotNull] BooleanFactorASTContext context) {
            if (context.TRUE() != null)
                emitter.Emit(OpCodes.Ldc_I4_1);
            else
                emitter.Emit(OpCodes.Ldc_I4_0);
            return "bool";
        }

        public object VisitBracketFactorAST([NotNull] BracketFactorASTContext context) {
            return Visit(context.expr());
        }

        public object VisitBreakStatementAST([NotNull] BreakStatementASTContext context) {
            return null;
        }

        public object VisitCallStatementAST([NotNull] CallStatementASTContext context)
        {
            var tuple = (Tuple<object, object>) Visit(context.designator());

            var methodInfo = tuple.Item2 as MethodInfo;
            var typeString = tuple.Item1 as string;

            if (context.actPars() != null)
                Visit(context.actPars());

            emitter.Emit(OpCodes.Call, methodInfo);

            if (!typeString.Equals("void")){
                emitter.Emit(OpCodes.Pop);
            }
            return null;
        }

        public object VisitCaseAST([NotNull] CaseASTContext context)
        {
            throw new NotImplementedException();
        }

        public object VisitCharConstFactorAST([NotNull] CharConstFactorASTContext context) {
            var charInt = Convert.ToInt32(context.CHARCONST().GetText().ToCharArray()[1]);

            emitter.Emit(OpCodes.Ldc_I4, charInt);

            return "char";
        }

        public object VisitClassDeclAST([NotNull] ClassDeclASTContext context) {
            tmpType = rootType.DefineNestedType(context.ident().GetText(),
                TypeAttributes.Class | TypeAttributes.NestedPublic,
                typeof(object));
            context.varDecl().ToList().ForEach(vd => Visit(vd));

            tmpType.CreateType();

            tmpType = null;

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
            var type = Visit(context.type()) as string;
            FieldBuilder field;

            ConstructorBuilder rootConstructor=null;

            if (tmpType != null)
                field = tmpType.DefineField(Visit(context.ident()) as string,
                    NablaHelper.ParseType(type), FieldAttributes.HasDefault);
            else {
                rootConstructor = rootType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
                emitter = rootConstructor.GetILGenerator();
                field = rootType.DefineField(Visit(context.ident()) as string,
                    NablaHelper.ParseType(type), FieldAttributes.HasDefault);
            }

            switch (type) {
                case "char":
                    emitter.Emit(OpCodes.Ldc_I4_S, char.Parse(context.CHARCONST().GetText()));
                    break;
                case "int":
                    emitter.Emit(OpCodes.Ldc_I4, int.Parse(context.NUM().GetText()));
                    break;
                case "float":
                    emitter.Emit(OpCodes.Ldind_R4, float.Parse(context.CHARCONST().GetText()));
                    break;
                case "bool":
                    // no está en el parser.
                    break;
            }

            emitter.Emit(OpCodes.Ldfld, field);

            if (rootConstructor != null) {
                emitter.Emit(OpCodes.Ret);
                //rootConstructor.crea
            }

            return null;
        }

        public object VisitDesignatorAST([NotNull] DesignatorASTContext context) {
            ParserRuleContext decl = context.ident(0).decl;

           

            if (decl is MethodDeclASTContext)
            {

                var methodName = Visit(context.ident(0)) as string;

                var method = GetMethod(methodName);

                var typeString = "void";
                if((decl as MethodDeclASTContext).type() != null)
                    typeString = Visit((decl as MethodDeclASTContext).type()) as string;

                return new Tuple<object, object>(typeString, method);

            }
            else if (decl.Parent is ProgramASTContext)
            {
                if (decl is ConstDeclASTContext)
                {
                    var field = GetField(Visit(context.ident(0)) as string);

                    var typeString = Visit((decl as ConstDeclASTContext).type());

                    return new Tuple<object, object>(typeString, field);
                }
                else if(decl is VarDeclASTContext)
                {
                    //Si fueran de un clase interna
                    var field = GetField(Visit(context.ident(0)) as string);

                    var typeString = Visit((decl as VarDeclASTContext).type()) as string;

                    return new Tuple<object, object>(typeString, field);
                }
            }
            else
            {
                if (decl is ConstDeclASTContext)
                {
                    var field = GetFirstVariable(Visit(context.ident(0)) as string);

                    var typeString = Visit((decl as ConstDeclASTContext).type());

                    return new Tuple<object, object>(typeString, field);
                }
                else if(decl is VarDeclASTContext)
                {
                    //Si fueran de algún tipo clase
                    var field = GetFirstVariable(Visit(context.ident(0)) as string);

                    var typeString = Visit((decl as VarDeclASTContext).type()) as string;

                    return new Tuple<object, object>(typeString, field);
                }
                else
                {
                    var field = GetParameter(Visit(context.ident(0)) as string);

                    var typeString = (field.type);

                    return new Tuple<object, object>(typeString, field.pb);
                }


            }
            return null;
        }

        public object VisitEqualEqualRelopAST([NotNull] EqualEqualRelopASTContext context) {
            return null;
        }

        public object VisitExprAST([NotNull] ExprASTContext context) {
            var typeString = Visit(context.term(0)) as string;
            if (context.addop().Length != 0)
            {
                for(int i = 1; i < context.term().Length; i++)
                {
                    Visit(context.term(i));

                    if (context.addop(i - 1).GetText() == "+")
                        emitter.Emit(OpCodes.Add);
                    else
                        emitter.Emit(OpCodes.Sub);
                }
                return "int";
            }
            else
            {
                return typeString;
            }
            
        }

        public object VisitFormParsAST([NotNull] FormParsASTContext context) {

            var types = new Type[context.type().Length];

            
            for (int i = 0; i < context.type().Length; i++)
            {
                var typeString = context.type(i).GetText();
                var type = GetTypeOf(typeString);

                types[i] = type;
            }

            methodBuilder.SetParameters(types);

            for (int i = 0; i < context.ident().Length; i++)
            {
                var paramName = context.ident(i).GetText();

                currentParameters.Add((context.type(i).GetText(), methodBuilder.DefineParameter(i + 1, ParameterAttributes.None, paramName)));
                
            }

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

            if(context.actPars() != null)
            {
                Visit(context.actPars());
            }
            

            var tuple = (Tuple<object, object>) Visit(context.designator());

            if(tuple.Item2 is MethodInfo)
            {
                var methodInfo = tuple.Item2 as MethodInfo;

                emitter.Emit(OpCodes.Call, methodInfo);
            }
            else if(tuple.Item2 is FieldInfo)
            {
                var fieldInfo = tuple.Item2 as FieldInfo;

                emitter.Emit(OpCodes.Ldfld, fieldInfo);

                if ((tuple.Item1 as string).Contains("[]"))
                {
                    Visit((context.designator() as DesignatorASTContext).expr(0));


                    emitter.Emit(OpCodes.Ldelem);
                }
            }
            else if(tuple.Item2 is ParameterBuilder)
            {
                var paramInfo = tuple.Item2 as ParameterBuilder;

                emitter.Emit(OpCodes.Ldarg, paramInfo.Position - 1);
                
                if ((tuple.Item1 as string).Contains("[]"))
                {
                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Ldelem);
                }
            }

            return tuple.Item1;
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


            var name = Visit(context.ident()) as string;

            var typeString = "void";
            if(context.type() != null)
                typeString=Visit(context.type()) as string;

            
            var type = GetTypeOf(typeString);


            methodBuilder = rootType.DefineMethod(name, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig, type, null);


            methodBuilder.InitLocals = true;


            currentParameters = new List<(string type, ParameterBuilder pb)>();
            if(context.formPars() != null)
                Visit(context.formPars());
            

            
            var baseEmitter = emitter;

            

            emitter = methodBuilder.GetILGenerator();

            Visit(context.block());

            if(name == "Main" || typeString == "void")
            {
                emitter.Emit(OpCodes.Ret);
            }

            if(name == "Main")
            {
                mainMethod = methodBuilder;
            }
            else
            {
                methods.Add(methodBuilder);
            }
            
            emitter = baseEmitter;
            
            return null;
        }

        public object VisitMulop([NotNull] MulopContext context) {
            return null;
        }

        public object VisitNewFactorAST([NotNull] NewFactorASTContext context) {
            var typeString = Visit(context.ident()) as string;
            if(context.SQUAREBL() != null)
            {
                Visit(context.expr());
                emitter.Emit(OpCodes.Newarr, GetTypeOf(typeString));
            }
            
            return null;
        }

        public object VisitNotEqualRelopAST([NotNull] NotEqualRelopASTContext context) {
            return null;
        }

        public object VisitNullFactorAST([NotNull] NullFactorASTContext context) {
            return null;
        }

        public object VisitNumFactorAST([NotNull] NumFactorASTContext context) {
            if(context.NUM().GetText().Split('.').Length > 1)
            {
                emitter.Emit(OpCodes.Ldc_R4, float.Parse(context.NUM().GetText(), System.Globalization.CultureInfo.InvariantCulture));
                return "float";
            }
            emitter.Emit(OpCodes.Ldc_I4, Int32.Parse(context.NUM().GetText()));
            return "int";
        }

        public object VisitProgramAST([NotNull] ProgramASTContext context) {

            rootType = moduleBuilder.DefineType(context.ident().GetText(), TypeAttributes.Public | TypeAttributes.Class);
            VisitChildren(context);
            return null;
        }

        public object VisitReadStatementAST([NotNull] ReadStatementASTContext context) {
            MethodInfo read = typeof(Console).GetMethod(
                         "ReadLine");

            emitter.Emit(OpCodes.Call, read);
            return null;
        }

        public object VisitReturnStatementAST([NotNull] ReturnStatementASTContext context) {
            if(context.expr() != null)
            {
                Visit(context.expr());
            }
            emitter.Emit(OpCodes.Ret);
            return null;
        }

        public object VisitSemicolonStatementAST([NotNull] SemicolonStatementASTContext context) {
            return null;
        }

        public object VisitStringFactorAST([NotNull] StringFactorASTContext context) {
            emitter.Emit(OpCodes.Ldstr, context.GetText().Substring(1, context.GetText().Length - 2));
            return "string";
        }

        public object VisitSwitchAST([NotNull] SwitchASTContext context) {
            return null;
        }

        public object VisitSwitchStatementAST([NotNull] SwitchStatementASTContext context) {
            return null;
        }

        public object VisitTermAST([NotNull] TermASTContext context) {
            var typeString = Visit(context.factor(0)) as string;

            if (context.mulop().Length > 0)
            {
                for (int i = 1; i < context.factor().Length; i++)
                {
                    Visit(context.factor(i));

                    if (context.mulop(i - 1).GetText() == "*")
                        emitter.Emit(OpCodes.Mul);
                    else if (context.mulop(i - 1).GetText() == "/")
                        emitter.Emit(OpCodes.Div);
                }

                return "num";
            }
            else
            {
                return typeString;
            }
        }

        public object VisitTypeAST([NotNull] TypeASTContext context) {
            return context.ident().GetText();
        }

        public object VisitVarDeclAST([NotNull] VarDeclASTContext context) {
            var varType = Visit(context.type()) as string;
            context.ident().ToList().ForEach(ident => {
                var identifier = Visit(ident) as string;
                FieldBuilder field;
                if (tmpType != null)
                    field = tmpType.DefineField(identifier, NablaHelper.ParseType(varType), FieldAttributes.Public);
                else
                    field = rootType.DefineField(identifier, NablaHelper.ParseType(varType), FieldAttributes.Public);
            });

            return null;
        }

        public object VisitWhileStatementAST([NotNull] WhileStatementASTContext context) {
            return null;
        }

        public object VisitWriteStatementAST([NotNull] WriteStatementASTContext context) {

            var type = Visit(context.expr()) as string;

            MethodInfo write = typeof(Console).GetMethod(
                         "WriteLine",
                         new Type[] { GetTypeOf(type) });

            emitter.Emit(OpCodes.Call, write);

            return null;
        }
    }
}