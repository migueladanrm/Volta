using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static VoltaParser;

namespace Volta.Compiler.CodeAnalysis
{
    class ContextualAnalysis : AbstractParseTreeVisitor<object>, IVoltaParserVisitor<object>
    {
        private IdentificationTable identificationTable;
        private List<string> types;

        public ContextualAnalysis() {
            types = new List<string>() { 
                "none",
                "int",
                "char",
                "float",
                "bool",
                "string",
                "int[]",
                "char[]",
                "float[]",
                "bool[]",
                "string[]"
            };

            identificationTable = new IdentificationTable();
            Errors = new List<VoltaCompilerError>();

            AddDefaultMethods();
        }

        public List<VoltaCompilerError> Errors { get; private set; }

        #region Auxiliar

        public void AddDefaultMethods()
        {
            // CHR

            Identifier chrIdenfifier = new MethodIdentifier("chr", "char", new List<string> { "int" });
            identificationTable.Insert(chrIdenfifier);

            // ORD

            Identifier ordIdenfifier = new MethodIdentifier("ord", "int", new List<string> { "char" });
            identificationTable.Insert(ordIdenfifier);

            // LEN

            Identifier lenIdenfifier = new MethodIdentifier("len", "int", new List<string> { "array" });
            identificationTable.Insert(lenIdenfifier);
        }
        public void InsertError(IToken token, int line, int col, string message) {
            Errors.Add(new VoltaContextualError(token, line, col, message));
        }

        public void InsertError(IToken token, string message)
            => InsertError(token, token.Line, token.Column, message);

        public bool ExistIdent(string id, bool inThisLevel)
        {
            return identificationTable.Find(id, inThisLevel) != null;
        }

        #endregion

        public object VisitIdentAST([NotNull] VoltaParser.IdentASTContext context)
        {
            if(context.IDENT() != null)
            {
                Identifier identifier = identificationTable.Find(context.IDENT().Symbol.Text, false);
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
                return null;
            }

            VoltaParser.FormParsASTContext originalPars = methodIdentifier.FormPars;

            if(originalPars == null)
            {
                if (methodIdentifier.DefaultMethod)
                {
                    if (methodIdentifier.DefaulMethodParams == null)
                    {
                        InsertError(context.Start, context.Start.Line, context.Start.Column,
                            "El método '" + methodIdentifier.Id + "' no recibe parámetros, y se recibieron " + context.expr().Length);
                    }
                    else if (context.expr().Length != methodIdentifier.DefaulMethodParams.Count)
                    {
                        InsertError(context.Start, context.Start.Line, context.Start.Column,
                            "El método '" + methodIdentifier.Id + "' recibe " + methodIdentifier.DefaulMethodParams.Count + " parámetros, y se recibieron " + context.expr().Length);
                    }
                    else
                    {
                        for (int i = 0; i < methodIdentifier.DefaulMethodParams.Count; i++)
                        {
                            string exprType = (string)Visit(context.expr()[i]);
                            if (exprType == null)
                            {
                                return null;
                            }
                            else if (exprType != "none" && ((methodIdentifier.DefaulMethodParams[i] == "array" && !exprType.Contains("[]")) && exprType != methodIdentifier.DefaulMethodParams[i]))
                            {
                                InsertError(context.expr()[i].Start, context.expr()[i].Start.Line, context.expr()[i].Start.Column,

                                    $"El tipo de la expresión '{ context.expr()[i].GetText()}' es '" + exprType + "', y se esperaba el tipo '" + methodIdentifier.DefaulMethodParams[i] + "'");
                            }
                        }
                    }
                }
                else
                {
                    InsertError(context.Start, context.Start.Line, context.Start.Column, "El método '" + methodIdentifier.Id + "' no recibe parámetros, y se recibieron " + context.expr().Length);
                }
            }

            else if(context.expr().Length != originalPars.ident().Length)
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
                    else if (exprType != "none" && exprType != originalPars.type()[i].GetText())
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
            context.varDecl().ToList().ForEach(varDecl => Visit(varDecl));
            context.constDecl().ToList().ForEach(constDecl => Visit(constDecl));
            List<Pair<string, IToken>> returnedTypes= new List<Pair<string, IToken>>();
            context.statement().ToList().ForEach(statement => {
                var list = Visit(statement) as List<Pair<string, IToken>>;
                if (list != null) 
                    returnedTypes.AddRange(list); 
                });
            return returnedTypes;
            
        }

        public object VisitBlockStatementAST([NotNull] VoltaParser.BlockStatementASTContext context)
        {
            identificationTable.OpenLevel();
            List<Pair<string, IToken>> returnedTypes = Visit(context.block()) as List<Pair<string, IToken>>;
            identificationTable.CloseLevel();
            return returnedTypes;
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
            VisitChildren(context); 
            return new List<Pair<string, IToken>>();
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
                return null;
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
            if(ident != null)
            {
                if (!ExistIdent(ident.GetText(), true))
                {
                
                    List<VoltaParser.VarDeclASTContext> varDecls = new List<VoltaParser.VarDeclASTContext>(context.varDecl().ToList().Cast<VoltaParser.VarDeclASTContext>());

                    varDecls.ForEach(varDecl => Visit(varDecl));

                    ClassIdentifier classIdentifier = new ClassIdentifier(ident.IDENT().GetText(), ident.IDENT().Symbol, identificationTable.getLevel(), types[0], context, varDecls);

                    identificationTable.Insert(classIdentifier);
                    types.Add(ident.GetText());
                }
                else
                {
                    InsertError(ident.IDENT().Symbol, ident.IDENT().Symbol.Line, ident.IDENT().Symbol.Column, "El identificador " + ident.IDENT().Symbol.Text + " ya fue declarado en este scope");
                }
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
                if(type != "int" && type != "char") {
                    InsertError(context.Start, context.Start.Line, context.Start.Column, 
                        "Los tipo para una constante solo pueden ser int o char");
                    return null;
                }
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
            if (ExistIdent(context.ident()[0].GetText(), false))
            {
                Identifier identifier = identificationTable.Find(context.ident()[0].GetText(), false);
                if (context.ident().Length == 1 && context.SQUAREBL().Length == 0 && context.DOT().Length == 0)
                {
                    Visit(context.ident()[0]);
                    return identifier;
                }
                else if(identifier is ArrayIdentifier || identifier is InstanceIdentifier)
                {
                    bool arrayFound = false;
                    bool error = false;
                    Identifier currentIdentifier = identifier;
                    
                    context.GetRuleContexts<ParserRuleContext>().Skip(1).ToList().ForEach(r => {
                        if (error)
                        {
                            return;
                        }

                        if (arrayFound)
                        {
                            InsertError(r.Start, r.Start.Line, r.Start.Column, "No se puede acceder a arreglos o propiedades de un elemento de un arreglo porque solo son de tipos simples");
                            error = true;
                            return;
                        }
                        if(r is VoltaParser.ExprASTContext)
                        {
                            arrayFound = true;
                            VoltaParser.ExprASTContext expr = r as VoltaParser.ExprASTContext;

                            string type = Visit(expr) as string;

                            if(type == "int")
                            {
                                if(!(currentIdentifier is ArrayIdentifier))
                                {
                                    InsertError(r.Start, r.Start.Line, r.Start.Column, $"El identificador {currentIdentifier.Id} no es un arreglo");
                                    error = true;
                                    return;
                                }
                                else
                                {
                                    currentIdentifier = (currentIdentifier as ArrayIdentifier).Identifiers[0];
                                }
                            }
                            else
                            {
                                InsertError(expr.Start, expr.Start.Line, expr.Start.Column, "Solo se permiten números enteros cuando se accede a una posición del arreglo");
                                error = true;
                                return;
                            }
                        }
                        else if(r is VoltaParser.IdentASTContext)
                        {

                            VoltaParser.IdentASTContext ident = r as VoltaParser.IdentASTContext;
                            if (currentIdentifier is InstanceIdentifier)
                            {
                                currentIdentifier = (currentIdentifier as InstanceIdentifier).Identifiers.Find(i => ident.GetText() == i.Id);
                                if(currentIdentifier == null)
                                {
                                    InsertError(ident.Start, ident.Start.Line, ident.Start.Column, $"El identificador {ident.IDENT().Symbol.Text} no existe en la instancia");
                                    error = true;
                                    return;
                                }
                            }
                            else {
                                InsertError(ident.Start, ident.Start.Line, ident.Start.Column, $"El identificador {currentIdentifier.Id} no es una instancia de una clase");
                                error = true;
                                return;
                            }
                        }

                    });
                    if (!error)
                        return currentIdentifier;
                }
                else
                {
                    InsertError(context.Start, context.Start.Line, context.Start.Column, "No se puede acceder a posiciones o propiedades de un identificador que no es una clase ni un arreglo");
                }
            }
            else
            {
                InsertError(context.ident()[0].Start, context.ident()[0].Start.Line, context.ident()[0].Start.Column, $"El identificador {context.ident()[0].GetText()} no ha sido declarado");
            }
            return null;
        }

        public object VisitEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context) {
            return "=";
        }

        public object VisitExprAST([NotNull] VoltaParser.ExprASTContext context)
        {
            if(context.term().Length == 1)
            {
                string type = (string)Visit(context.term()[0]);
                if(type != "int")
                {
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
            var typesList = new List<VoltaParser.TypeASTContext>(context.type().ToList().Cast<VoltaParser.TypeASTContext>());
            var idents = new List<VoltaParser.IdentASTContext>(context.ident().ToList().Cast<VoltaParser.IdentASTContext>());

            for(int i = 0; i < typesList.Count; i++)
            {
                var type = Visit(typesList[i]) as string;

                if(type != null)
                {
                    if(!ExistIdent(idents[i].IDENT().Symbol.Text, true)){
                        if(types.IndexOf(type) > 10)
                        {
                            ClassIdentifier classIdentifier = identificationTable.FindClass(type);
                            List<Identifier> instanceIdentifiers = GetIdentifiersFromClass(classIdentifier);
                            InstanceIdentifier instanceIdentifier = new InstanceIdentifier(idents[i].IDENT().Symbol.Text, idents[i].IDENT().Symbol, identificationTable.getLevel(), type, context, classIdentifier, instanceIdentifiers);
                            identificationTable.Insert(instanceIdentifier);
                        }
                        else
                        {
                            var ident = idents[i].IDENT();
                            Identifier identifier = new VarIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context);
                            if (typesList[i].SQUAREBL() != null && typesList[i].SQUAREBR() != null)
                            {
                                if (types.IndexOf(type) <= 10)
                                {
                                    List<Identifier> identifiers = new List<Identifier>();
                                    identifier.Id = "0";
                                    identifiers.Add(identifier);
                                    identifier.Type = type.Replace("[]", "");
                                    ArrayIdentifier arrayIdentifier = new ArrayIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context, 1, identifiers);
                                    identificationTable.Insert(arrayIdentifier);
                                }
                            }
                            else
                            {
                                identificationTable.Insert(identifier);
                            }
                        }
                    }
                    else
                    {
                        InsertError(idents[i].IDENT().Symbol, idents[i].IDENT().Symbol.Line, idents[i].IDENT().Symbol.Column, "El identificador " + idents[i].IDENT().Symbol.Text + " ya fue declarado en los parámetros");
                    }
                }
            }
            return context;
        }

        public object VisitForStatementAST([NotNull] VoltaParser.ForStatementASTContext context)
        {
            VisitChildren(context);
            List<Pair<string, IToken>> returnedTypes = new List<Pair<string, IToken>>();
            context.statement().ToList().ForEach(statement => returnedTypes.AddRange(Visit(statement) as List<Pair<string, IToken>>));

            return returnedTypes;
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
            if(context.condition() != null)
                Visit(context.condition());
            List<Pair<string, IToken>> returnedTypes = new List<Pair<string, IToken>>();
            context.statement().ToList().ForEach(statement => returnedTypes.AddRange(Visit(statement) as List<Pair<string, IToken>>));
            return returnedTypes;
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
                    {
                        List<Pair<string, IToken>> returnedTypes = Visit(context.block()) as List<Pair<string, IToken>>;
                        returnedTypes.ForEach(returned =>
                        {
                            if(returned.a != null && (!returned.a.Equals(type) && (type == "void" || returned.a != "none")))
                            {
                                InsertError(returned.b, returned.b.Line, returned.b.Column, $"El tipo de retorno del método {identifier.Id} es {type}, pero se retorna {(returned.a == "none"? "null": returned.a)}");
                            }
                        });
                    }
                    
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
                if (context.SQUAREBL() != null && context.SQUAREBR() != null)
                {
                    if (types.Contains(context.ident().GetText() + "[]"))
                    {
                        return context.ident().GetText() + "[]";
                    }
                    else
                    {
                        InsertError(context.ident().Start, context.ident().Start.Line, context.ident().Start.Column,
                            "No se puede crear un arreglo de tipo '" + context.ident().GetText() + "' porque no es un tipo simple");
                    }
                }
                else
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
            VisitChildren(context); 
            return new List<Pair<string, IToken>>();
        }

        public object VisitReturnStatementAST([NotNull] VoltaParser.ReturnStatementASTContext context)
        {
            // Revisando que el return se encuentre en una función
            bool inMethod = false;
            RuleContext ctx = context.Parent;
            while(!(ctx is MethodDeclContext || ctx is ProgramContext))
            {
                ctx = ctx.Parent;
                if(ctx is MethodDeclContext)
                {
                    inMethod = true;
                }
            }

            if (!inMethod)
            {
                InsertError(context.Start, context.Start.Line, context.Start.Column, "No se permite utilizar un return fuera de un método");
            }

            // Retorna una lista con el tipo que está retornando
            List<Pair<string, IToken>> returnedTypes = new List<Pair<string, IToken>>();
            if(context.expr() != null)
            {
                returnedTypes.Add(new Pair<string, IToken>(Visit(context.expr()) as string, context.Start));
            }
            else
            {
                returnedTypes.Add(new Pair<string, IToken>("void", context.Start));
            }
            return returnedTypes;
        }

        public object VisitSemicolonStatementAST([NotNull] VoltaParser.SemicolonStatementASTContext context)
        {
            VisitChildren(context); return new List<Pair<string, IToken>>();
        }

        public object VisitStringFactorAST([NotNull] VoltaParser.StringFactorASTContext context)
        {
            return "string";
        }

        public object VisitSwitchAST([NotNull] VoltaParser.SwitchASTContext context)
        {
            if(context.ident() != null)
            {
                if(Visit(context.ident()) == null)
                {
                    //Error que no existe el ident
                }
            }

            List<Pair<string, IToken>> returnedTypes = new List<Pair<string, IToken>>();
            context.statement().ToList().ForEach(statement => returnedTypes.AddRange(Visit(statement) as List<Pair<string, IToken>>));
            return returnedTypes;
        }

        public object VisitSwitchStatementAST([NotNull] VoltaParser.SwitchStatementASTContext context)
        {
            
            return Visit(context.@switch());
        }

        public object VisitTermAST([NotNull] VoltaParser.TermASTContext context)
        {
            if (context.factor().Length == 1)
            {
                return Visit(context.factor()[0]);
            }
            else if (context.factor().Length > 1)
            {
                bool allInts = context.factor().ToList().All(factor => { 
                    string type = (string)Visit(factor);
                    if (type != "int"){
                        InsertError(factor.Start, factor.Start.Line, factor.Start.Column,
                            $"El factor '{factor.GetText()}' no es un número entero. Solo se permite el uso de '*', '/' o '%' con números enteros");
                    }
                    return type == "int";
                });
                return allInts ? "int": null;
            }
            return null;
        }

        public object VisitTypeAST([NotNull] VoltaParser.TypeASTContext context)
        {
            VoltaParser.IdentASTContext ident = (VoltaParser.IdentASTContext)Visit(context.ident());

            if (ident != null)
            {
                if (types.Contains(ident.GetText()))
                {
                    if (context.SQUAREBL() != null)
                    {
                        if (types.Contains(ident.GetText() + "[]"))
                            return ident.GetText() + "[]";
                        InsertError(ident.IDENT().Symbol, ident.IDENT().Symbol.Line, ident.IDENT().Symbol.Column, "No se pueden declarar arreglos de tipo" + ident.IDENT().GetText() + " porque no es un tipo simple");
                    }
                    else
                        return ident.GetText();
                }
                else
                {
                    InsertError(ident.IDENT().Symbol, ident.IDENT().Symbol.Line, ident.IDENT().Symbol.Column, "El tipo " + ident.IDENT().GetText() + " no existe");
                }
            }
            return null;
        }

        public List<Identifier> GetIdentifiersFromClass(ClassIdentifier classIdentifier)
        {
            List<Identifier> identifiers = new List<Identifier>();

            classIdentifier.VarDecl.ForEach(context =>
            {
                string type = (string)Visit(context.type());
                if (type != null)
                {
                    context.ident().ToList().ForEach((VoltaParser.IdentContext identC) =>
                    {
                        if (identC.GetText() == "")
                        {
                            return;
                        }
                        ITerminalNode ident = ((VoltaParser.IdentASTContext)identC).IDENT();
                        if (types.IndexOf(type) > 10)
                        {
                            ClassIdentifier classIdentifier1 = identificationTable.FindClass(type);
                            List<Identifier> instanceIdentifiers = GetIdentifiersFromClass(classIdentifier1);
                            InstanceIdentifier instanceIdentifier = new InstanceIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context, classIdentifier, instanceIdentifiers);
                            identifiers.Add(instanceIdentifier);
                        }
                        else
                        {
                            Identifier identifier = new VarIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context);
                            identifiers.Add(identifier);
                        }
                    });
                }
            });

            return identifiers;
        }

        public object VisitVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context)
        {
            string type = (string) Visit(context.type());
            if(type != null)
            {
                context.ident().ToList().ForEach((VoltaParser.IdentContext identC) =>
                {
                    
                    if (identC.GetText() == "")
                    {
                        return;
                    }
                    ITerminalNode ident = ((VoltaParser.IdentASTContext) identC).IDENT();
                    if (ident != null && !ExistIdent(ident.Symbol.Text, true)){
                        if(types.IndexOf(type) > 10)
                        {
                            if((context.type() as VoltaParser.TypeASTContext).SQUAREBL() == null)
                            {
                                ClassIdentifier classIdentifier = identificationTable.FindClass(type);
                                List<Identifier> instanceIdentifiers = GetIdentifiersFromClass(classIdentifier);
                                InstanceIdentifier instanceIdentifier = new InstanceIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context, classIdentifier, instanceIdentifiers);
                                identificationTable.Insert(instanceIdentifier);
                            }
                        }
                        else
                        {
                            Identifier identifier = new VarIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context);
                            if ((context.type() as VoltaParser.TypeASTContext).SQUAREBL() != null && (context.type() as VoltaParser.TypeASTContext).SQUAREBR() != null)
                            {
                                if(types.IndexOf(type) <= 10)
                                {
                                    List<Identifier> identifiers = new List<Identifier>();
                                    identifier.Id = "0";
                                    identifiers.Add(identifier);
                                    identifier.Type = type.Replace("[]", "");
                                    ArrayIdentifier arrayIdentifier = new ArrayIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context, 1, identifiers);
                                    identificationTable.Insert(arrayIdentifier);
                                }
                            }
                            else {
                                identificationTable.Insert(identifier);
                            }
                        }
                        
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
            Visit(context.condition());
            return Visit(context.statement());
        }

        public object VisitWriteStatementAST([NotNull] VoltaParser.WriteStatementASTContext context)
        {
            VisitChildren(context); 
            return new List<Pair<string, IToken>>();
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
            return new List<Pair<string, IToken>>();
        }

        public object VisitAssignStatementAST([NotNull] VoltaParser.AssignStatementASTContext context) {
            var identifier = Visit(context.designator()) as Identifier;

            if (identifier != null) {
                if (identifier is VarIdentifier) {
                    var type = Visit(context.expr()) as string;
                    if (identifier.Type.Equals(type))
                        return new List<Pair<string, IToken>>();
                    else {
                        var tmpExpr = context.expr();
                        InsertError(tmpExpr.Start, tmpExpr.Start.Line, tmpExpr.Start.Column,
                            $"No se puede asignar un valor de tipo '{type}' a una variable de tipo '{identifier.Type}'.");
                    }
                } else if (identifier is ArrayIdentifier) {
                    var arr = identifier as ArrayIdentifier;
                    var expr = context.expr() as ExprASTContext;

                    if ((expr.term()[0] as TermASTContext).factor()[0] is NewFactorASTContext) {
                        var nf = (expr.term()[0] as TermASTContext).factor()[0] as NewFactorASTContext;
                        var nfType = Visit(nf) as string;

                        if (arr.Type.Equals(nfType)) {
                            if (nf.SQUAREBL() != null && nf.SQUAREBR() != null) {
                                var arrExpr = nf.expr() as ExprASTContext;
                                if (arrExpr != null && (Visit(arrExpr) as string) == "int") {
                                    if (arrExpr.SUB() == null)
                                        return new List<Pair<string, IToken>>();
                                    else InsertError(arrExpr.SUB().Symbol, "El tamaño del arreglo no puede ser negativo.");
                                } else InsertError(arrExpr.Start, "Se esperaba un valor entero.");
                            }
                        } else {
                            InsertError(nf.Start, $"No se puede asignar un valor de tipo '{nfType}' a una variable de tipo '{identifier.Type}'.");
                        }
                    } else {
                        InsertError(expr.Start, "Sólo se puede asignar valores a arreglos usando el modificador 'new'.");
                    }
                } else if (identifier is InstanceIdentifier) {

                } else if (identifier is ConstIdentifier) {
                    InsertError(context.EQUAL().Symbol, context.EQUAL().Symbol.Line, context.EQUAL().Symbol.Column,
                        "No es posible modificar el valor de una constante después de su declaración.");
                }
            } else {
                InsertError(context.designator().Start, context.designator().Start.Line, context.designator().Start.Column,
                    $"La variable '{context.designator()}' no ha sido declarada.");
            }

            return new List<Pair<string, IToken>>(); ;
        }

        public object VisitAddsubStatementAST([NotNull] VoltaParser.AddsubStatementASTContext context)
        {
            VisitChildren(context); 
            return new List<Pair<string, IToken>>(); 
        }

        public object VisitNullFactorAST([NotNull] VoltaParser.NullFactorASTContext context)
        {
            return "none";
        }
    }
}