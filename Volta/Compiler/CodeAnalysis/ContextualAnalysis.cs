using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using static VoltaParser;

namespace Volta.Compiler.CodeAnalysis
{
    /// <summary>
    /// Analizador contextual.
    /// </summary>
    class ContextualAnalysis : AbstractParseTreeVisitor<object>, IVoltaParserVisitor<object>
    {
        private IdentificationTable identificationTable;
        private readonly List<string> types = new List<string>() {
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

        public ContextualAnalysis() {
            identificationTable = new IdentificationTable();
            Errors = new List<VoltaCompilerError>();

            AddDefaultMethods();
        }

        public List<VoltaCompilerError> Errors { get; private set; }

        #region Auxiliar methods

        public void AddDefaultMethods() {
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

        public bool ExistIdent(string id, bool inThisLevel) {
            return identificationTable.Find(id, inThisLevel) != null;
        }

        public void InsertError(IToken token, string message)
            => Errors.Add(new VoltaContextualError(token, token.Line, token.Column, message));

        #endregion

        public object VisitIdentAST([NotNull] IdentASTContext context) {
            if (context.IDENT() != null) {
                Identifier identifier = identificationTable.Find(context.IDENT().Symbol.Text, false);
                if (identifier != null) {
                    context.decl = identifier.Declaration;
                }
                return context;
            }
            return null;
        }

        public object VisitActParsAST([NotNull] ActParsASTContext context) {
            var callFactor = context.Parent is IdentOrCallFactorASTContext ? (IdentOrCallFactorASTContext)context.Parent : null;
            var callStatement = context.Parent is CallStatementASTContext ? (CallStatementASTContext)context.Parent : null;

            MethodIdentifier methodIdentifier = null;
            if (callFactor != null)
                methodIdentifier = (MethodIdentifier)Visit(callFactor.designator());
            else if (callStatement != null)
                methodIdentifier = (MethodIdentifier)Visit(callStatement.designator());

            if (methodIdentifier == null) {
                return null;
            }

            FormParsASTContext originalPars = methodIdentifier.FormPars;

            if (originalPars == null) {
                if (methodIdentifier.DefaultMethod) {
                    if (methodIdentifier.DefaulMethodParams == null) {
                        InsertError(context.Start,
                            "El método '" + methodIdentifier.Id + "' no recibe parámetros, y se recibieron " + context.expr().Length);
                    } else if (context.expr().Length != methodIdentifier.DefaulMethodParams.Count) {
                        InsertError(context.Start,
                            "El método '" + methodIdentifier.Id + "' recibe " + methodIdentifier.DefaulMethodParams.Count + " parámetros, y se recibieron " + context.expr().Length);
                    } else {
                        for (int i = 0; i < methodIdentifier.DefaulMethodParams.Count; i++) {
                            string exprType = (string)Visit(context.expr()[i]);
                            if (exprType == null) {
                                return null;
                            } else if (exprType != "none" && ((methodIdentifier.DefaulMethodParams[i] == "array" && !exprType.Contains("[]")) && exprType != methodIdentifier.DefaulMethodParams[i])) {
                                InsertError(context.expr()[i].Start,
                                    $"El tipo de la expresión '{ context.expr()[i].GetText()}' es '" + exprType + "', y se esperaba el tipo '" + methodIdentifier.DefaulMethodParams[i] + "'");
                            }
                        }
                    }
                } else {
                    InsertError(context.Start, "El método '" + methodIdentifier.Id + "' no recibe parámetros, y se recibieron " + context.expr().Length);
                }
            } else if (context.expr().Length != originalPars.ident().Length) {
                InsertError(context.Start, "El método '" + methodIdentifier.Id + "' recibe " + originalPars.ident().Length + " parámetros, y se recibieron " + context.expr().Length);
            } else {
                for (int i = 0; i < originalPars.ident().Length; i++) {
                    string exprType = (string)Visit(context.expr()[i]);
                    if (exprType == null) {
                        return null;
                    } else if (exprType != "none" && exprType != originalPars.type()[i].GetText()) {
                        InsertError(context.expr()[i].Start,
                            $"El tipo de la expresión '{ context.expr()[i].GetText()}' es '" + exprType + "', y se esperaba el tipo '" + originalPars.type()[i].GetText() + "'");
                    }
                }
            }



            return null;
        }

        public object VisitBlockAST([NotNull] BlockASTContext context) {
            context.varDecl().ToList().ForEach(varDecl => Visit(varDecl));
            context.constDecl().ToList().ForEach(constDecl => Visit(constDecl));
            List<Pair<string, IToken>> returnedTypes = new List<Pair<string, IToken>>();
            context.statement().ToList().ForEach(statement => {
                var list = Visit(statement) as List<Pair<string, IToken>>;
                if (list != null)
                    returnedTypes.AddRange(list);
            });
            return returnedTypes;

        }

        public object VisitBlockStatementAST([NotNull] BlockStatementASTContext context) {
            identificationTable.OpenLevel();
            List<Pair<string, IToken>> returnedTypes = Visit(context.block()) as List<Pair<string, IToken>>;
            identificationTable.CloseLevel();
            return returnedTypes;
        }

        public object VisitBooleanFactorAST([NotNull] BooleanFactorASTContext context) {
            return "bool";
        }

        public object VisitBracketFactorAST([NotNull] BracketFactorASTContext context) {
            return Visit(context.expr());
        }

        public object VisitBreakStatementAST([NotNull] BreakStatementASTContext context) {
            VisitChildren(context);
            return new List<Pair<string, IToken>>();
        }

        public object VisitIdentOrCallFactorAST([NotNull] IdentOrCallFactorASTContext context) {
            Identifier identifier = (Identifier)Visit(context.designator());
            if (identifier != null && identifier is MethodIdentifier && context.BL() != null) {
                MethodIdentifier methodId = identifier as MethodIdentifier;
                if (context.actPars() != null) {
                    Visit(context.actPars());
                    return methodId.Type;
                } else if (methodId.FormPars != null) {
                    InsertError(context.BL().Symbol,
                        "El método '" + methodId.Id + "' recibe " + methodId.FormPars.ident().Length +
                        (methodId.FormPars.ident().Length > 1 ? " parámetros" : " parámetro") + ", y no se recibió ninguno");
                }
            } else if (identifier != null) {
                return identifier.Type;
            } else if (identifier == null) {
                return null;
            } else {
                IToken token = context.BL() != null ? context.BL().Symbol : ((IdentASTContext)((DesignatorASTContext)context.designator()).ident()[0]).IDENT().Symbol;
                InsertError(token, "El identificador '" + ((DesignatorASTContext)context.designator()).ident()[0].GetText() + "' no corresponde a un método");
            }
            return null;

        }

        public object VisitCharConstFactorAST([NotNull] CharConstFactorASTContext context) {
            return "char";
        }

        public object VisitClassDeclAST([NotNull] ClassDeclASTContext context) {
            IdentASTContext ident = (IdentASTContext)Visit(context.ident());
            if (ident != null) {
                if (!ExistIdent(ident.GetText(), true)) {
                    List<VarDeclASTContext> varDecls = new List<VarDeclASTContext>(context.varDecl().ToList().Cast<VarDeclASTContext>());
                    varDecls.ForEach(varDecl => Visit(varDecl));
                    ClassIdentifier classIdentifier = new ClassIdentifier(ident.IDENT().GetText(), ident.IDENT().Symbol, identificationTable.getLevel(), types[0], context, varDecls);

                    identificationTable.Insert(classIdentifier);
                    types.Add(ident.GetText());
                } else {
                    InsertError(ident.IDENT().Symbol, "El identificador " + ident.IDENT().Symbol.Text + " ya fue declarado en este scope");
                }
            }
            return null;
        }

        #region Conditions

        public object VisitConditionAST([NotNull] ConditionASTContext context) {
            var relops = new List<List<string>>();
            context.condTerm().ToList().ForEach(condTerm => relops.Add(Visit(condTerm) as List<string>));
            return relops;
        }

        public object VisitCondTermAST([NotNull] CondTermASTContext context) {
            var relops = new List<string>();
            context.condFact().ToList().ForEach(condFact => relops.Add(Visit(condFact) as string ?? "none"));
            return relops;
        }

        public object VisitCondFactAST([NotNull] CondFactASTContext context) {
            var a = Visit(context.expr()[0]) as string;
            var b = Visit(context.expr()[1]) as string;
            var relop = Visit(context.relop()) as string;
            var sameType = a == b;

            if (relop == "<" || relop == "<=" || relop == ">" || relop == ">=") {
                if (sameType) {
                    if (a == "int" || a == "float") {
                        return a;
                    } else InsertError(context.relop().Start, $"El operador '{relop}' solo se puede usar con tipos int o float.");
                }
            } else if (relop == "==" || relop == "!=") {
                if (sameType)
                    return a;
                else {
                    InsertError(context.expr()[1].Start,
                        $"El operador '{relop}' no se puede aplicar a los tipos '{a}' y '{b}'.");
                }
            }

            return null;
        }

        #endregion

        public object VisitConstDeclAST([NotNull] ConstDeclASTContext context) {
            string type = (string)Visit(context.type());
            if (type != null) {
                if (type != "int" && type != "char") {
                    InsertError(context.Start,
                        "Los tipo para una constante solo pueden ser int o char");
                    return null;
                }
                IdentASTContext ident = (IdentASTContext)Visit(context.ident());
                if (ident != null) {
                    if (!ExistIdent(ident.IDENT().Symbol.Text, true)) {
                        Identifier identifier = new ConstIdentifier(ident.IDENT().GetText(), ident.IDENT().Symbol, identificationTable.getLevel(), type, context);
                        identificationTable.Insert(identifier);



                        if ((context.NUM() != null &&
                                ((context.NUM().GetText().Split('.').Length > 1 && type != "float") ||
                                (context.NUM().GetText().Split('.').Length == 1 && type != "int")))
                            ||
                            (context.STRING() != null && type != "string") ||
                            (context.CHARCONST() != null && type != "char")) {
                            InsertError(context.EQUAL().Symbol,
                                "El tipo para la constante " + ident.GetText() + " no coincide con el tipo de " + context.GetText().Split('=').Last());
                        }
                    } else {
                        InsertError(ident.IDENT().Symbol, "El identificador " + ident.IDENT().Symbol.Text + " ya fue declarado en este scope");
                    }
                }
            }
            return null;
        }

        public object VisitDesignatorAST([NotNull] DesignatorASTContext context) {
            if (ExistIdent(context.ident()[0].GetText(), false)) {
                Identifier identifier = identificationTable.Find(context.ident()[0].GetText(), false);
                if (context.ident().Length == 1 && context.SQUAREBL().Length == 0 && context.DOT().Length == 0) {
                    Visit(context.ident()[0]);
                    return identifier;
                } else if (identifier is ArrayIdentifier || identifier is InstanceIdentifier) {
                    bool arrayFound = false;
                    bool error = false;
                    Identifier currentIdentifier = identifier;

                    context.GetRuleContexts<ParserRuleContext>().Skip(1).ToList().ForEach(r => {
                        if (error) {
                            return;
                        }

                        if (arrayFound) {
                            InsertError(r.Start, "No se puede acceder a arreglos o propiedades de un elemento de un arreglo porque solo son de tipos simples");
                            error = true;
                            return;
                        }
                        if (r is ExprASTContext) {
                            arrayFound = true;
                            ExprASTContext expr = r as ExprASTContext;

                            string type = Visit(expr) as string;

                            if (type == "int") {
                                if (!(currentIdentifier is ArrayIdentifier)) {
                                    InsertError(r.Start, $"El identificador {currentIdentifier.Id} no es un arreglo");
                                    error = true;
                                    return;
                                } else {
                                    currentIdentifier = (currentIdentifier as ArrayIdentifier).Identifiers[0];
                                }
                            } else {
                                InsertError(expr.Start, "Solo se permiten números enteros cuando se accede a una posición del arreglo");
                                error = true;
                                return;
                            }
                        } else if (r is IdentASTContext) {

                            IdentASTContext ident = r as IdentASTContext;
                            if (currentIdentifier is InstanceIdentifier) {
                                currentIdentifier = (currentIdentifier as InstanceIdentifier).Identifiers.Find(i => ident.GetText() == i.Id);
                                if (currentIdentifier == null) {
                                    InsertError(ident.Start, $"El identificador {ident.IDENT().Symbol.Text} no existe en la instancia");
                                    error = true;
                                    return;
                                }
                            } else {
                                InsertError(ident.Start, $"El identificador {currentIdentifier.Id} no es una instancia de una clase");
                                error = true;
                                return;
                            }
                        }

                    });
                    if (!error)
                        return currentIdentifier;
                } else {
                    InsertError(context.Start, "No se puede acceder a posiciones o propiedades de un identificador que no es una clase ni un arreglo");
                }
            } else {
                InsertError(context.ident()[0].Start, $"El identificador {context.ident()[0].GetText()} no ha sido declarado");
            }
            return null;
        }

        public object VisitExprAST([NotNull] ExprASTContext context) {
            if (context.term().Length == 1) {
                string type = (string)Visit(context.term(0));
                if (type != "int") {
                    if (context.SUB() == null) {
                        return type;
                    } else if (type != null) {
                        InsertError(context.SUB().Symbol, "La expresión '" + context.term()[0].GetText() + "'no es un número y no se puede negar");
                        return null;
                    }
                } else {
                    return type;
                }
            }
            else if (context.term().Length > 1)
            {
                bool allInts = context.term().ToList().All(term => {
                    string type = (string)Visit(term);
                    if (type != "int")
                    {
                        InsertError(term.Start,
                            $"El término '{term.GetText()}' no es un número entero. Solo se permite el uso de '+' o '-' con números enteros");
                    }
                    return type == "int";
                });
                return allInts ? "int" : null;
            }

            return null;
        }

        public object VisitFormParsAST([NotNull] FormParsASTContext context) {
            var typesList = new List<TypeASTContext>(context.type().ToList().Cast<TypeASTContext>());
            var idents = new List<IdentASTContext>(context.ident().ToList().Cast<IdentASTContext>());

            for (int i = 0; i < typesList.Count; i++) {
                var type = Visit(typesList[i]) as string;

                if (type != null) {
                    if (idents[i].IDENT() != null)
                    {
                        if (!ExistIdent(idents[i].IDENT().Symbol.Text, true))
                        {
                            if (types.IndexOf(type) > 10)
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
                            InsertError(idents[i].IDENT().Symbol, "El identificador " + idents[i].IDENT().Symbol.Text + " ya fue declarado en los parámetros");
                        }
                    }
                }
            }
            return context;
        }

        #region Relops

        public object VisitEqualEqualRelopAST([NotNull] EqualEqualRelopASTContext context) {
            return "==";
        }

        public object VisitNotEqualRelopAST([NotNull] NotEqualRelopASTContext context) {
            return "!=";
        }

        public object VisitGreaterEqualRelopAST([NotNull] GreaterEqualRelopASTContext context) {
            return ">=";
        }

        public object VisitGreaterRelopAST([NotNull] GreaterRelopASTContext context) {
            return ">";
        }

        public object VisitLessEqualRelopAST([NotNull] LessEqualRelopASTContext context) {
            return "<=";
        }

        public object VisitLessRelopAST([NotNull] LessRelopASTContext context) {
            return "<";
        }

        #endregion

        public object VisitForStatementAST([NotNull] ForStatementASTContext context) {
            var iterator = Visit(context.expr()) as string ?? "none";
            if (context.condition() != null && context.statement(0) != null && context.statement(1) != null)
            {
                Visit(context.condition());
                Visit(context.statement()[0]);

                if (iterator == "int")
                {
                    if (context.statement().Length > 1)
                        Visit(context.statement()[1]);
                }
                else
                    InsertError(context.expr().Start, $"El iterador debe ser de tipo 'int' pero se obtuvo '{iterator}'.");

                VisitChildren(context);
                List<Pair<string, IToken>> returnedTypes = new List<Pair<string, IToken>>();
                context.statement().ToList().ForEach(statement =>
                {
                    var list = Visit(statement) as List<Pair<string, IToken>>;
                    if (list != null)
                        returnedTypes.AddRange(list);
                });
                return returnedTypes;
            }
            return new List<Pair<string, IToken>>();
        }

        public object VisitIfStatementAST([NotNull] IfStatementASTContext context) {
            if (context.condition() != null)
                Visit(context.condition());

            var returnedTypes = new List<Pair<string, IToken>>();
            context.statement().ToList().ForEach(statement => {
                var list = Visit(statement) as List<Pair<string, IToken>>;
                if (list != null)
                    returnedTypes.AddRange(list);
            });

            return returnedTypes;
        }

        public object VisitMethodDeclAST([NotNull] MethodDeclASTContext context) {
            string type = "void";
            if (context.type() != null)
                type = (string)Visit(context.type());

            IdentASTContext ident = (IdentASTContext)Visit(context.ident());

            if (ident != null) {
                if (!ExistIdent(ident.IDENT().Symbol.Text, true)) {
                    MethodIdentifier identifier = new MethodIdentifier(ident.IDENT().Symbol.Text, ident.IDENT().Symbol, identificationTable.getLevel(), type,
                            context, (FormParsASTContext)context.formPars());

                    identificationTable.Insert(identifier);

                    identificationTable.OpenLevel(); // Para los parámetros ya existe un scope nuevo
                    if (context.formPars() != null)
                        Visit(context.formPars()); //Cuando se visitan los parámetros y se encuentra un error, ellos lo reportan


                    if (context.varDecl() != null)
                        context.varDecl().ToList().ForEach(varDecl => Visit(varDecl));

                    if (context.block() != null) {
                        List<Pair<string, IToken>> returnedTypes = Visit(context.block()) as List<Pair<string, IToken>>;
                        returnedTypes.ForEach(returned => {
                            if (returned.a != null && (!returned.a.Equals(type) && (type == "void" || returned.a != "none"))) {
                                InsertError(returned.b, $"El tipo de retorno del método {identifier.Id} es {type}, pero se retorna {(returned.a == "none" ? "null" : returned.a)}");
                            }
                        });
                    }

                    identificationTable.CloseLevel();

                } else {
                    InsertError(ident.IDENT().Symbol, "El identificador " + ident.IDENT().Symbol.Text + " ya fue declarado en este scope");
                }
            }


            return null;
        }

        public object VisitMulop([NotNull] MulopContext context) {
            VisitChildren(context); return null;
        }

        public object VisitNewFactorAST([NotNull] NewFactorASTContext context) {

            if (types.Contains(context.ident().GetText())) {
                if (context.SQUAREBL() != null && context.SQUAREBR() != null) {
                    if (types.Contains(context.ident().GetText() + "[]")) {
                        return context.ident().GetText() + "[]";
                    } else {
                        InsertError(context.ident().Start,
                            "No se puede crear un arreglo de tipo '" + context.ident().GetText() + "' porque no es un tipo simple");
                    }
                } else
                    return context.ident().GetText();
            }

            InsertError(context.ident().Start,
                "No se puede crear una instancia de '" + context.ident().GetText() + "' porque no es un tipo");
            return null;
        }

        public object VisitNumFactorAST([NotNull] NumFactorASTContext context) {
            return context.NUM().GetText().Split('.').Length > 1 ? "float" : "int";
        }

        public object VisitProgramAST([NotNull] ProgramASTContext context) {
            identificationTable.OpenLevel();
            VisitChildren(context);
            identificationTable.CloseLevel();
            return null;
        }

        public object VisitReadStatementAST([NotNull] ReadStatementASTContext context) {
            VisitChildren(context);
            return new List<Pair<string, IToken>>();
        }

        public object VisitReturnStatementAST([NotNull] ReturnStatementASTContext context) {
            // Revisando que el return se encuentre en una función
            bool inMethod = false;
            RuleContext ctx = context.Parent;
            while (!(ctx is MethodDeclContext || ctx is ProgramContext)) {
                ctx = ctx.Parent;
                if (ctx is MethodDeclContext) {
                    inMethod = true;
                }
            }

            if (!inMethod) {
                InsertError(context.Start, "No se permite utilizar un return fuera de un método");
            }

            // Retorna una lista con el tipo que está retornando
            List<Pair<string, IToken>> returnedTypes = new List<Pair<string, IToken>>();
            if (context.expr() != null) {
                returnedTypes.Add(new Pair<string, IToken>(Visit(context.expr()) as string, context.Start));
            } else {
                returnedTypes.Add(new Pair<string, IToken>("void", context.Start));
            }
            return returnedTypes;
        }

        public object VisitSemicolonStatementAST([NotNull] SemicolonStatementASTContext context) {
            VisitChildren(context); return new List<Pair<string, IToken>>();
        }

        public object VisitStringFactorAST([NotNull] StringFactorASTContext context) {
            return "string";
        }

        public object VisitSwitchAST([NotNull] SwitchASTContext context) {
            var nums = context.NUM();
            var strings = context.STRING();
            var chars = context.CHARCONST();

            var trues = context.TRUE();
            var falses = context.FALSE();

            string type = Visit(context.expr()) as string;
            if (type != null && types.IndexOf(type) < 4) {
                if (type == "int" || type == "float") {
                    if (nums.Length != context.CASE().Length) {
                        InsertError(context.Start, $"El tipo de todos los case deben coincidir con el tipo {type}");
                    }
                } else if (type == "char") {
                    if (chars.Length != context.CASE().Length) {
                        InsertError(context.Start, $"El tipo de todos los case deben coincidir con el tipo {type}");
                    }
                } else if (type == "string") {
                    if (strings.Length != context.CASE().Length) {
                        InsertError(context.Start, $"El tipo de todos los case deben coincidir con el tipo {type}");
                    }
                } else if (type == "bool") {
                    if (trues.Length + falses.Length != context.CASE().Length) {
                        InsertError(context.Start, $"El tipo de todos los case deben coincidir con el tipo {type}");
                    }
                } else if (type == "none") {
                    InsertError(context.expr().Start, $"La expresión no puede ser la constante 'null'");
                }
            } else {
                InsertError(context.expr().Start, $"El tipo de la expresión para el switch debe ser simple");
            }


            List<Pair<string, IToken>> returnedTypes = new List<Pair<string, IToken>>();
            context.statement().ToList().ForEach(statement => {
                var list = Visit(statement) as List<Pair<string, IToken>>;
                if (list != null)
                    returnedTypes.AddRange(list);
            });
            return returnedTypes;
        }

        public object VisitSwitchStatementAST([NotNull] SwitchStatementASTContext context) {

            return Visit(context.@switch());
        }

        public object VisitTermAST([NotNull] TermASTContext context) {
            if (context.factor().Length == 1) {
                return Visit(context.factor()[0]);
            } else if (context.factor().Length > 1) {
                bool allInts = context.factor().ToList().All(factor => {
                    string type = (string)Visit(factor);
                    if (type != "int") {
                        InsertError(factor.Start,
                            $"El factor '{factor.GetText()}' no es un número entero. Solo se permite el uso de '*', '/' o '%' con números enteros");
                    }
                    return type == "int";
                });
                return allInts ? "int" : null;
            }
            return null;
        }

        public object VisitTypeAST([NotNull] TypeASTContext context) {
            IdentASTContext ident = (IdentASTContext)Visit(context.ident());

            if (ident != null) {
                if (types.Contains(ident.GetText())) {
                    if (context.SQUAREBL() != null) {
                        if (types.Contains(ident.GetText() + "[]"))
                            return ident.GetText() + "[]";
                        InsertError(ident.IDENT().Symbol, "No se pueden declarar arreglos de tipo" + ident.IDENT().GetText() + " porque no es un tipo simple");
                    } else
                        return ident.GetText();
                } else {
                    InsertError(ident.IDENT().Symbol, "El tipo " + ident.IDENT().GetText() + " no existe");
                }
            }
            return null;
        }

        public List<Identifier> GetIdentifiersFromClass(ClassIdentifier classIdentifier) {
            List<Identifier> identifiers = new List<Identifier>();

            classIdentifier.VarDecl.ForEach(context => {
                string type = (string)Visit(context.type());
                if (type != null) {
                    context.ident().ToList().ForEach((IdentContext identC) => {
                        if (identC.GetText() == "") {
                            return;
                        }
                        ITerminalNode ident = ((IdentASTContext)identC).IDENT();
                        if (types.IndexOf(type) > 10) {
                            ClassIdentifier classIdentifier1 = identificationTable.FindClass(type);
                            List<Identifier> instanceIdentifiers = GetIdentifiersFromClass(classIdentifier1);
                            InstanceIdentifier instanceIdentifier = new InstanceIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context, classIdentifier, instanceIdentifiers);
                            identifiers.Add(instanceIdentifier);
                        } else {
                            Identifier identifier = new VarIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context);
                            identifiers.Add(identifier);
                        }
                    });
                }
            });

            return identifiers;
        }

        public object VisitVarDeclAST([NotNull] VarDeclASTContext context) {
            string type = (string)Visit(context.type());
            if (type != null) {
                context.ident().ToList().ForEach((IdentContext identC) => {

                    if (identC.GetText() == "") {
                        return;
                    }
                    ITerminalNode ident = ((IdentASTContext)identC).IDENT();
                    if (ident != null && !ExistIdent(ident.Symbol.Text, true)) {
                        if (types.IndexOf(type) > 10) {
                            if ((context.type() as TypeASTContext).SQUAREBL() == null) {
                                ClassIdentifier classIdentifier = identificationTable.FindClass(type);
                                List<Identifier> instanceIdentifiers = GetIdentifiersFromClass(classIdentifier);
                                InstanceIdentifier instanceIdentifier = new InstanceIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context, classIdentifier, instanceIdentifiers);
                                identificationTable.Insert(instanceIdentifier);
                            }
                        } else {
                            Identifier identifier = new VarIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context);
                            if ((context.type() as TypeASTContext).SQUAREBL() != null && (context.type() as TypeASTContext).SQUAREBR() != null) {
                                if (types.IndexOf(type) <= 10) {
                                    List<Identifier> identifiers = new List<Identifier>();
                                    identifier.Id = "0";
                                    identifiers.Add(identifier);
                                    identifier.Type = type.Replace("[]", "");
                                    ArrayIdentifier arrayIdentifier = new ArrayIdentifier(ident.Symbol.Text, ident.Symbol, identificationTable.getLevel(), type, context, 1, identifiers);
                                    identificationTable.Insert(arrayIdentifier);
                                }
                            } else {
                                identificationTable.Insert(identifier);
                            }
                        }

                    } else {
                        InsertError(ident.Symbol, "El identificador " + ident.Symbol.Text + " ya fue declarado en este scope");
                    }
                });
            }
            return null;
        }

        public object VisitWhileStatementAST([NotNull] WhileStatementASTContext context) {
            Visit(context.condition());
            return Visit(context.statement());
        }

        public object VisitWriteStatementAST([NotNull] WriteStatementASTContext context) {
            VisitChildren(context);
            return new List<Pair<string, IToken>>();
        }

        public object VisitCallStatementAST([NotNull] CallStatementASTContext context) {
            Identifier identifier = (Identifier)Visit(context.designator());
            if (identifier != null && identifier is MethodIdentifier) {
                MethodIdentifier methodId = (MethodIdentifier)identifier;
                if (context.actPars() != null) {
                    Visit(context.actPars());
                } else if (methodId.FormPars != null) {
                    InsertError(context.BL().Symbol,
                        "El método '" + methodId.Id + "' recibe " + methodId.FormPars.ident().Length +
                        (methodId.FormPars.ident().Length > 1 ? " parámetros" : " parámetro") + ", y no se recibió ninguno");
                }
            } else {
                IToken token = context.BL() != null ? context.BL().Symbol : ((IdentASTContext)((DesignatorASTContext)context.designator()).ident()[0]).IDENT().Symbol;
                InsertError(token, "El identificador " + ((DesignatorASTContext)context.designator()).ident()[0].GetText() + " no corresponde a un método");
            }
            return new List<Pair<string, IToken>>();
        }

        public object VisitAssignStatementAST([NotNull] AssignStatementASTContext context) {
            var identifier = Visit(context.designator()) as Identifier;

            if (identifier != null) {
                if (identifier is VarIdentifier || identifier is InstanceIdentifier) {
                    var type = Visit(context.expr()) as string;
                    if (identifier.Type.Equals(type))
                        return new List<Pair<string, IToken>>();
                    else {
                        var tmpExpr = context.expr();
                        InsertError(tmpExpr.Start,
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
                } else if (identifier is ConstIdentifier) {
                    InsertError(context.EQUAL().Symbol,
                        "No es posible modificar el valor de una constante después de su declaración.");
                }
            } else {
                InsertError(context.designator().Start,
                    $"La variable '{context.designator()}' no ha sido declarada.");
            }

            return new List<Pair<string, IToken>>(); ;
        }

        public object VisitAddSubStatementAST([NotNull] AddSubStatementASTContext context) {
            Identifier identifier = Visit(context.designator()) as Identifier;
            if (identifier != null) {
                if (identifier is VarIdentifier) {
                    if (identifier.Type != "int") {
                        InsertError(context.Start,
                        "No se puede aplicar la operación a identificadores que no sean de tipo 'int', y el tipo de la variable es " + identifier.Type);
                    }
                } else {
                    InsertError(context.Start,
                        "No se puede aplicar la operación a identificadores de arreglos, métodos, constantes o clases. Solo variables de tipo int");
                }
            }
            return new List<Pair<string, IToken>>();
        }

        public object VisitNullFactorAST([NotNull] NullFactorASTContext context) {
            return "none";
        }

        public object VisitAddopAST([NotNull] AddopASTContext context) {
            VisitChildren(context); return null;
        }
    }
}