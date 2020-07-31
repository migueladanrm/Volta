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
            
            var opCode = OpCodes.Add;

            if(context.SUBSUB() != null)
            {
                opCode = OpCodes.Sub;
            }

            var tuple = (Tuple<object, object>)Visit(context.designator());

            var typeString = tuple.Item1 as string;

            var baseType = GetTypeOf(typeString.Replace("[]", ""));

            if (tuple.Item2 is FieldInfo)
            {
                var fieldInfo = tuple.Item2 as FieldInfo;

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    emitter.Emit(OpCodes.Ldfld, fieldInfo); //Lee el arreglo
                    Visit((context.designator() as DesignatorASTContext).expr(0)); // Lee el index

                    emitter.Emit(OpCodes.Ldfld, fieldInfo); // Lee el arreglo

                    Visit((context.designator() as DesignatorASTContext).expr(0)); // Lee el index

                    emitter.Emit(OpCodes.Ldelem, baseType); // Obtiene el elemento
                    emitter.Emit(OpCodes.Ldc_I4_1); // Lee 1
                    emitter.Emit(opCode); // Guarda en la pila la suma o resta del 1

                    emitter.Emit(OpCodes.Ldelem, baseType); // Guarda, usando la primera leída del arreglo y el index, el valor de la suma o resta de 1
                }
                else
                {
                    emitter.Emit(OpCodes.Ldfld, fieldInfo);
                    emitter.Emit(OpCodes.Ldc_I4_1);
                    emitter.Emit(opCode);

                    emitter.Emit(OpCodes.Stfld, fieldInfo);
                }
            }
            else if (tuple.Item2 is ParameterBuilder)
            {
                var paramInfo = tuple.Item2 as ParameterBuilder;



                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {

                    emitter.Emit(OpCodes.Ldarg, paramInfo.Position - 1);
                    Visit((context.designator() as DesignatorASTContext).expr(0));


                    emitter.Emit(OpCodes.Ldarg, paramInfo.Position - 1);
                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Ldelem, baseType); // Obtiene el elemento
                    emitter.Emit(OpCodes.Ldc_I4_1); // Lee 1
                    emitter.Emit(opCode); // Guarda en la pila la suma o resta del 1

                    emitter.Emit(OpCodes.Stelem, baseType);
                }
                else
                {
                    emitter.Emit(OpCodes.Ldarg, paramInfo.Position - 1);
                    emitter.Emit(OpCodes.Ldc_I4_1);
                    emitter.Emit(opCode);

                    emitter.Emit(OpCodes.Starg, paramInfo.Position - 1);
                }
            }
            else if (tuple.Item2 is LocalBuilder)
            {
                var localBuilder = tuple.Item2 as LocalBuilder;

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    emitter.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);
                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);
                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Ldelem, baseType); // Obtiene el elemento
                    emitter.Emit(OpCodes.Ldc_I4_1); // Lee 1
                    emitter.Emit(opCode); // Guarda en la pila la suma o resta del 1

                    emitter.Emit(OpCodes.Stelem, baseType);
                }
                else
                {
                    emitter.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);
                    emitter.Emit(OpCodes.Ldc_I4_1);
                    emitter.Emit(opCode);

                    emitter.Emit(OpCodes.Stloc, localBuilder.LocalIndex);
                }
            }
            return null;
        }

        public object VisitAssignStatementAST([NotNull] AssignStatementASTContext context) {
            

            var tuple = (Tuple<object, object>)Visit(context.designator());

            var typeString = tuple.Item1 as string;

            var baseType = GetTypeOf(typeString.Replace("[]", ""));

            if (tuple.Item2 is FieldInfo)
            {
                var fieldInfo = tuple.Item2 as FieldInfo;

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    emitter.Emit(OpCodes.Ldfld, fieldInfo);

                    Visit((context.designator() as DesignatorASTContext).expr(0));


                    emitter.Emit(OpCodes.Stelem, baseType);
                }
                else
                {
                    emitter.Emit(OpCodes.Stfld, fieldInfo);
                }
            }
            else if (tuple.Item2 is ParameterBuilder)
            {
                var paramInfo = tuple.Item2 as ParameterBuilder;



                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    emitter.Emit(OpCodes.Ldarg, paramInfo.Position - 1);

                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    Visit(context.expr());

                    emitter.Emit(OpCodes.Stelem, baseType);
                }
                else
                {
                    emitter.Emit(OpCodes.Starg, paramInfo.Position - 1);
                }
            }
            else if (tuple.Item2 is LocalBuilder)
            {
                var localBuilder = tuple.Item2 as LocalBuilder;

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    emitter.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);

                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Stelem, baseType);
                }
                else
                {
                    emitter.Emit(OpCodes.Stloc, localBuilder.LocalIndex);
                }
            }
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
            if (context.TRUE() != null)
                emitter.Emit(OpCodes.Ldc_I4_1);
            else
                emitter.Emit(OpCodes.Ldc_I4_0);
            return null;
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
            if(context.NUM() != null)
            {
                if (context.NUM().GetText().Split('.').Length > 1)
                {
                    emitter.Emit(OpCodes.Ldc_R4, float.Parse(context.NUM().GetText(), System.Globalization.CultureInfo.InvariantCulture));
                }
                emitter.Emit(OpCodes.Ldc_I4, Int32.Parse(context.NUM().GetText()));
            }
            else if(context.CHARCONST() != null)
            {
                var charInt = Convert.ToInt32(context.CHARCONST().GetText().ToCharArray()[1]);

                emitter.Emit(OpCodes.Ldc_I4, charInt);
            }
            else if (context.STRING() != null)
            {
                emitter.Emit(OpCodes.Ldstr, context.STRING().GetText().Substring(1, context.STRING().GetText().Length - 2));
            }
            else if(context.boolean() != null)
            {
                Visit(context.boolean());
            }
            return null;
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
            var relop = context.relop().GetText();
            if (relop.Equals(">=") || relop.Equals("<="))
            {
                Visit(context.expr(1));
                Visit(context.expr(0));
                relop = relop.Equals(">=") ? "<" : relop.Equals("<=") ? ">" : relop;  
            }
            else
            {
                Visit(context.expr(0));
                Visit(context.expr(1));
            }


            OpCode opCode;
            switch (context.relop().GetText())
            {
                case "<":
                    opCode = OpCodes.Clt;
                    break;
                case ">":
                    opCode = OpCodes.Cgt;
                    break;
                default:
                    opCode = OpCodes.Ceq;
                    break;
            }

            emitter.Emit(opCode);

            if (relop == "!=")
            {
                emitter.Emit(OpCodes.Ldc_I4_1);
                emitter.Emit(OpCodes.Sub);
                emitter.Emit(OpCodes.Neg);

            }
            return null;
        }

        public object VisitConditionAST([NotNull] ConditionASTContext context) {
            Visit(context.condTerm(0));

            for (int i = 1; i < context.condTerm().Length; i++)
            {
                Visit(context.condTerm(i));

                emitter.Emit(OpCodes.Or);
            }
            return null;
        }

        public object VisitCondTermAST([NotNull] CondTermASTContext context) {
            Visit(context.condFact(0));

            for (int i = 1; i < context.condFact().Length; i++)
            {
                Visit(context.condFact(i));

                emitter.Emit(OpCodes.And);
            }
            return null;
        }

        public object VisitConstDeclAST([NotNull] ConstDeclASTContext context) {
            var field = rootType.DefineField(Visit(context.ident()) as string,
                NablaHelper.ParseType(Visit(context.type()) as string),
                FieldAttributes.HasDefault);

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
                var field = GetField(Visit(context.ident(0)) as string);

                string typeString;
                if (decl is ConstDeclASTContext)
                {
                    typeString = Visit((decl as ConstDeclASTContext).type()) as string;

                }
                else
                {
                    //Falta revisión de instancias
                    typeString = Visit((decl as VarDeclASTContext).type()) as string;

                }
                return new Tuple<object, object>(typeString, field);
            }
            else
            {
                object field = GetFirstVariable(Visit(context.ident(0)) as string);

                string typeString;
                if(!(decl is FormParsASTContext)){

                    if (decl is ConstDeclASTContext)
                    {
                        typeString = Visit((decl as ConstDeclASTContext).type()) as string;

                    }
                    else
                    {
                        //Falta revisión de instancias

                        typeString = Visit((decl as VarDeclASTContext).type()) as string;
                    }

                    return new Tuple<object, object>(typeString, (((string name, LocalBuilder localBuilder))field).localBuilder);
                }
                else
                {
                    typeString = (((string type, ParameterBuilder pb)) field).type;

                    return new Tuple<object, object>(typeString, (((string type, ParameterBuilder pb))field).pb);
                }

            }
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
            if (context.condition() != null && context.statement(0) != null && context.statement(1) != null)
            {
                Visit(context.condition());

                var labelToBegin = emitter.DefineLabel();
                var labelToEnd = emitter.DefineLabel();

                emitter.Emit(OpCodes.Brfalse, labelToEnd);

                emitter.MarkLabel(labelToBegin);

                Visit(context.statement(1));
                Visit(context.statement(0));
                Visit(context.condition());

                emitter.Emit(OpCodes.Brtrue, labelToBegin);

                emitter.MarkLabel(labelToEnd);
            }
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

            var tuple = (Tuple<object, object>) Visit(context.designator());

            var typeString = tuple.Item1 as string;

            var baseType = GetTypeOf(typeString.Replace("[]", ""));

            if (tuple.Item2 is MethodInfo)
            {
                if (context.actPars() != null)
                {
                    Visit(context.actPars());
                }
                var methodInfo = tuple.Item2 as MethodInfo;

                emitter.Emit(OpCodes.Call, methodInfo);
            }
            else if(tuple.Item2 is FieldInfo)
            {
                var fieldInfo = tuple.Item2 as FieldInfo;

                emitter.Emit(OpCodes.Ldfld, fieldInfo);

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    Visit((context.designator() as DesignatorASTContext).expr(0));


                    emitter.Emit(OpCodes.Ldelem, baseType);

                    typeString = typeString.Replace("[]", "");
                }
            }
            else if(tuple.Item2 is ParameterBuilder)
            {
                var paramInfo = tuple.Item2 as ParameterBuilder;

                emitter.Emit(OpCodes.Ldarg, paramInfo.Position - 1);
                
                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Ldelem, baseType);

                    typeString = typeString.Replace("[]", "");
                }
            }
            else if(tuple.Item2 is LocalBuilder)
            {
                var localBuilder = tuple.Item2 as LocalBuilder;

                emitter.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Ldelem, baseType);

                    typeString = typeString.Replace("[]", "");
                }
            }

            return typeString;
        }

        public object VisitIfStatementAST([NotNull] IfStatementASTContext context) {

            Visit(context.condition());
            var toElseLabel = emitter.DefineLabel();
            emitter.Emit(OpCodes.Brfalse, toElseLabel);

            Visit(context.statement(0));

            if (context.ELSE() != null)
            {
                var toEndLabel = emitter.DefineLabel();

                emitter.Emit(OpCodes.Br, toEndLabel);

                emitter.MarkLabel(toElseLabel);

                Visit(context.statement(1));

                emitter.MarkLabel(toEndLabel);
            }
            else
            {
                emitter.MarkLabel(toElseLabel);
            }

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
                var index = emitter.DeclareLocal(GetTypeOf(typeString+ "[]")).LocalIndex;
                emitter.Emit(OpCodes.Stloc, index);
                emitter.Emit(OpCodes.Ldloc, index);
            }
            
            return typeString;
        }

        public object VisitNotEqualRelopAST([NotNull] NotEqualRelopASTContext context) {
            return null;
        }

        public object VisitNullFactorAST([NotNull] NullFactorASTContext context) {
            emitter.Emit(OpCodes.Ldnull);
            return "none";
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

            var tuple = (Tuple<object, object>)Visit(context.designator());

            var typeString = tuple.Item1 as string;

            var baseType = GetTypeOf(typeString.Replace("[]", ""));

            if (tuple.Item2 is FieldInfo)
            {
                var fieldInfo = tuple.Item2 as FieldInfo;

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    emitter.Emit(OpCodes.Ldfld, fieldInfo);

                    Visit((context.designator() as DesignatorASTContext).expr(0));


                    emitter.Emit(OpCodes.Stelem, baseType);
                }
                else
                {
                    emitter.Emit(OpCodes.Stfld, fieldInfo);
                }
            }
            else if (tuple.Item2 is ParameterBuilder)
            {
                var paramInfo = tuple.Item2 as ParameterBuilder;

                

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    emitter.Emit(OpCodes.Ldarg, paramInfo.Position - 1);

                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Stelem, baseType);
                }
                else
                {
                    emitter.Emit(OpCodes.Starg, paramInfo.Position - 1);
                }
            }
            else if (tuple.Item2 is LocalBuilder)
            {
                var localBuilder = tuple.Item2 as LocalBuilder;

                if ((tuple.Item1 as string).Contains("[]") && (context.designator() as DesignatorASTContext).expr().Length != 0)
                {
                    emitter.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);

                    Visit((context.designator() as DesignatorASTContext).expr(0));

                    emitter.Emit(OpCodes.Stelem, baseType);
                }
                else
                {
                    emitter.Emit(OpCodes.Stloc, localBuilder.LocalIndex);
                }
            }
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

            var end = emitter.DefineLabel();
            var defaultCase = emitter.DefineLabel();

            Label[] casesL = new Label[context.@case().Length];

            for (int i = 0; i < casesL.Length; i++)
            {
                casesL[i] = emitter.DefineLabel();
            }

            var type = Visit(context.expr()) as string;
            var expr = emitter.DeclareLocal(GetTypeOf(type));

            emitter.Emit(OpCodes.Stloc, expr);



            for (int i = 0; i < context.@case().Length; i++)
            {
                
                emitter.Emit(OpCodes.Ldloc, expr);
                Visit(context.@case(i));
                emitter.Emit(OpCodes.Ceq);
                emitter.Emit(OpCodes.Brtrue_S, casesL[i]);
            }
            emitter.Emit(OpCodes.Br_S, defaultCase);

            for (int i = 0; i < context.@case().Length; i++)
            {
                emitter.MarkLabel(casesL[i]);
                if((context.@case(i) as CaseASTContext).statement() != null)
                    Visit((context.@case(i) as CaseASTContext).statement());
                emitter.Emit(OpCodes.Br_S, end);
            }

            emitter.MarkLabel(defaultCase);
            if(context.statement() != null)
            {
                Visit(context.statement());
            }

            emitter.MarkLabel(end);

            return null;
        }

        public object VisitSwitchStatementAST([NotNull] SwitchStatementASTContext context) {
            Visit(context.@switch());
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
            if (context.condition() != null && context.statement() != null)
            {
                Visit(context.condition());

                var labelToBegin = emitter.DefineLabel();
                var labelToEnd = emitter.DefineLabel();

                emitter.Emit(OpCodes.Brfalse, labelToEnd);

                emitter.MarkLabel(labelToBegin);
                Visit(context.statement());
                Visit(context.condition());

                emitter.Emit(OpCodes.Brtrue, labelToBegin);

                emitter.MarkLabel(labelToEnd);
            }
            return null;
        }

        public object VisitWriteStatementAST([NotNull] WriteStatementASTContext context) {

            var type = Visit(context.expr()) as string;

            MethodInfo write = typeof(Console).GetMethod(
                         "WriteLine",
                         new Type[] { GetTypeOf(type.Replace("[]", "")) });

            emitter.Emit(OpCodes.Call, write);

            return null;
        }
    }
}