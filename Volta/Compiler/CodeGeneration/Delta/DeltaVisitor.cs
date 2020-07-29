using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static VoltaParser;

namespace Volta.Compiler.CodeGeneration.Delta
{
    public class DeltaVisitor : AbstractParseTreeVisitor<object>, IVoltaParserVisitor<object>
    {


        public List<string> CodeLines { get; set; }

        public int LineCount { get; set; }

        public DeltaVisitor(IParseTree tree)
        {
            CodeLines = new List<string>();
            LineCount = 0;

            Visit(tree);
        }

        public void PrintCode()
        {
            CodeLines.ForEach(line =>
            {
                Debug.WriteLine(line);
            });
        }

        public List<string> GetCode()
        {
            return CodeLines;
        }

        public string CreateTempFile()
        {
            var directoryName = @".\temp";

            

            var fileName = @".\temp\running.VMCS";

            try
            {
                if(!Directory.Exists(directoryName))
                    Directory.CreateDirectory(@".\temp");


                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file   
                    CodeLines.ForEach(line => {
                        var lineBytes = new UTF8Encoding(true).GetBytes(line + "\n");
                        fs.Write(lineBytes, 0, lineBytes.Length);
                    });
                    
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

            return fileName;
        }

        

        public void AddLine(string code)
        {
            CodeLines.Add($"{LineCount} {code}");
            LineCount++;
        }

        public void WhiteLine()
        {
            //CodeLines.Add("");
        }

        public int RealIndexOf(int i)
        {
#if NETCOREAPP3_1
            return CodeLines.FindIndex(line => line.Split(" ")[0] == i.ToString());
#else
            return 0;
#endif
        }

        public void SetLineOnRealIndexOf(int i, string code)
        {
#if NETCOREAPP3_1
            CodeLines[CodeLines.FindIndex(line => line.Split(" ")[0] == i.ToString())] = $"{i} {code}";
#endif
        }

        public object VisitActParsAST([NotNull] ActParsASTContext context)
        {
            context.expr().ToList().ForEach(expr => Visit(expr));
            return $"{context.expr().Length}";
        }

        public object VisitAddopAST([NotNull] AddopASTContext context)
        {
            return null;
        }

        public object VisitAddSubStatementAST([NotNull] AddSubStatementASTContext context)
        {
            string scope = Visit(context.designator()) as string;
            string ident = context.designator().GetText();

            AddLine($"LOAD_{scope} {ident}");
            AddLine("LOAD_CONST 1");
            if (context.ADDADD() != null)
                AddLine($"BINARY_ADD");
            else
                AddLine($"BINARY_SUBSTRACT");
            AddLine($"STORE_{scope} {ident}");
            WhiteLine();
            return null;
        }

        public object VisitAssignStatementAST([NotNull] AssignStatementASTContext context)
        {
            Visit(context.expr());

            string scope = Visit(context.designator()) as string;
            string ident = context.designator().GetText();

            AddLine($"STORE_{scope} {ident}");
            WhiteLine();
            return null;
        }

        public object VisitBlockAST([NotNull] BlockASTContext context)
        {
            context.varDecl().ToList().ForEach(varDecl => Visit(varDecl));
            context.constDecl().ToList().ForEach(constDecl => Visit(constDecl));
            context.statement().ToList().ForEach(statement => Visit(statement));
            return null;
        }

        public object VisitBlockStatementAST([NotNull] BlockStatementASTContext context)
        {
            Visit(context.block());
            return null;
        }

        public object VisitBooleanFactorAST([NotNull] BooleanFactorASTContext context)
        {
            if(context.TRUE() != null)
                AddLine($"LOAD_CONST {context.TRUE().GetText()}");
            else
                AddLine($"LOAD_CONST {context.FALSE().GetText()}");
            return null;
        }

        public object VisitBracketFactorAST([NotNull] BracketFactorASTContext context)
        {
            Visit(context.expr());
            return null;
        }

        public object VisitBreakStatementAST([NotNull] BreakStatementASTContext context)
        {
            AddLine("BREAK");
            return null;
        }

        public object VisitCallStatementAST([NotNull] CallStatementASTContext context)
        {

            string scope = Visit(context.designator()) as string;
            string ident = context.designator().GetText();

            AddLine($"LOAD_{scope} {ident}");
            string numParams = Visit(context.actPars()) as string;
            AddLine($"CALL_FUNCTION {numParams}");
 
            return null;
        }

        public object VisitCharConstFactorAST([NotNull] CharConstFactorASTContext context)
        {
            AddLine($"LOAD_CONST {context.CHARCONST().GetText()}");
            return null;
        }

        public object VisitClassDeclAST([NotNull] ClassDeclASTContext context)
        {
            return null;
        }

        public object VisitCondFactAST([NotNull] CondFactASTContext context)
        {
            Visit(context.expr(0));
            Visit(context.expr(1));

            AddLine($"COMPARE_OP {context.relop().GetText()}");
            
            return null;
        }

        public object VisitConditionAST([NotNull] ConditionASTContext context)
        {
            Visit(context.condTerm(0));

            for (int i = 1; i < context.condTerm().Length; i++)
            {
                Visit(context.condTerm(i));

                AddLine("BINARY_OR");
            }
            return null;
        }

        public object VisitCondTermAST([NotNull] CondTermASTContext context)
        {
            Visit(context.condFact(0));

            for (int i = 1; i < context.condFact().Length; i++)
            {
                Visit(context.condFact(i));

                AddLine("BINARY_AND");
            }
            return null;
        }

        public object VisitConstDeclAST([NotNull] ConstDeclASTContext context)
        {
            string varName = context.ident().GetText();

            string scope = "LOCAL";
            if (context.Parent is ProgramASTContext)
                scope = "GLOBAL";
            
            string type = Visit(context.type()) as string;

            
            string declCode = $"PUSH_{scope}_{type} {varName}";
            AddLine(declCode);

            string constT;
            if(context.NUM() != null)
                constT = context.NUM().GetText();
            else if(context.CHARCONST() != null)
                constT = context.CHARCONST().GetText();
            else
                constT = context.STRING().GetText();

            string loadCode = $"LOAD_CONST {constT}";
            AddLine(loadCode);

            string assignCode = $"STORE_{scope} {varName}";
            AddLine(assignCode);

            WhiteLine();
            return null;
        }

        public object VisitDesignatorAST([NotNull] DesignatorASTContext context)
        {
            if(context.ident(0).decl.Parent is ProgramASTContext)
            {
                return "GLOBAL";
            }
            else
            {
                return "FAST";
            }
        }

        public object VisitEqualEqualRelopAST([NotNull] EqualEqualRelopASTContext context)
        {
            return null;
        }

        public object VisitExprAST([NotNull] ExprASTContext context)
        {
            Visit(context.term(0));

            for(int i = 1; i < context.term().Length; i++)
            {
                Visit(context.term(i));

                string instruction = "BINARY_";
                if (context.addop(i - 1).GetText() == "+")
                    instruction += "ADD";
                else
                    instruction += "SUBSTRACT";

                AddLine(instruction);
            }
            return null;
        }

        public object VisitFormParsAST([NotNull] FormParsASTContext context)
        {
            WhiteLine();
            string instruction = "PUSH_";
            if (context.Parent is ProgramASTContext)
                instruction += "GLOBAL";
            else
                instruction += "LOCAL";


            for(int i = 0; i < context.type().Length; i++)
            {
                string type = Visit(context.type(i)) as string;

                AddLine($"{instruction}_{type} {context.ident(i).GetText()}");
            }

            WhiteLine();
            return null;
        }

        public object VisitForStatementAST([NotNull] ForStatementASTContext context)
        {
            if(context.condition() != null && context.statement(0) != null && context.statement(1) != null)
            {
                Visit(context.condition());

                int jumpIfFalse = LineCount;
                AddLine("JUMP_IF_FALSE");

                int jumpIfNext = LineCount;
                Visit(context.statement(1));
                Visit(context.statement(0));
                Visit(context.condition());
                AddLine($"JUMP_IF_TRUE {jumpIfNext}");

                SetLineOnRealIndexOf(jumpIfFalse, $"JUMP_IF_FALSE {LineCount}");
            }
            return null;
        }

        public object VisitGreaterEqualRelopAST([NotNull] GreaterEqualRelopASTContext context)
        {
            return null;
        }

        public object VisitGreaterRelopAST([NotNull] GreaterRelopASTContext context)
        {
            return null;
        }

        public object VisitIdentAST([NotNull] IdentASTContext context)
        {
            return null;
        }

        public object VisitIdentOrCallFactorAST([NotNull] IdentOrCallFactorASTContext context)
        {
            string scope = Visit(context.designator()) as string;
            string ident = context.designator().GetText();


            if (context.BL() != null)
            {
                string numParams = Visit(context.actPars()) as string;
                AddLine($"LOAD_{scope} {ident}");
                AddLine($"CALL_FUNCTION {numParams}");
            }
            else
            {
                AddLine($"LOAD_{scope} {ident}");
            }
            
            return null;

        }

        public object VisitIfStatementAST([NotNull] IfStatementASTContext context)
        {
            Visit(context.condition());
            int jumpIfFalsePosition = LineCount;
            AddLine("JUMP_IF_FALSE");

            Visit(context.statement(0));

            if(context.ELSE() != null)
            {
                int jumpAbsolutePosition = LineCount;
                AddLine("JUMP_ABSOLUTE");

                SetLineOnRealIndexOf(jumpIfFalsePosition, $"JUMP_IF_FALSE {LineCount}");

                Visit(context.statement(1));

                SetLineOnRealIndexOf(jumpAbsolutePosition, $"JUMP_ABSOLUTE {LineCount}");
            }
            else
            {
                SetLineOnRealIndexOf(jumpIfFalsePosition, $"JMP_IF_FALSE {LineCount}");
            }

            return null;
        }

        public object VisitLessEqualRelopAST([NotNull] LessEqualRelopASTContext context)
        {
            return null;
        }

        public object VisitLessRelopAST([NotNull] LessRelopASTContext context)
        {
            return null;
        }

        public object VisitMethodDeclAST([NotNull] MethodDeclASTContext context)
        {
            if(context.ident().GetText() != "Main")
            {
                AddLine($"DEF {context.ident().GetText()}");
                Visit(context.formPars());
                Visit(context.block());
                AddLine("RETURN");
                WhiteLine();
                WhiteLine();
            }
            else
            {
                AddLine("DEF Main");
                Visit(context.block());
                AddLine("END");
                WhiteLine();
            }
            return null;
        }

        public object VisitMulop([NotNull] MulopContext context)
        {
            return null;
        }

        public object VisitNewFactorAST([NotNull] NewFactorASTContext context)
        {
            return null;
        }

        public object VisitNotEqualRelopAST([NotNull] NotEqualRelopASTContext context)
        {
            return null;
        }

        public object VisitNullFactorAST([NotNull] NullFactorASTContext context)
        {
            AddLine($"LOAD_CONST {null}");
            return null;
        }

        public object VisitNumFactorAST([NotNull] NumFactorASTContext context)
        {
            AddLine($"LOAD_CONST {context.NUM().GetText()}");
            return null;
        }

        public object VisitProgramAST([NotNull] ProgramASTContext context)
        {
            VisitChildren(context); return null;
        }

        public object VisitReadStatementAST([NotNull] ReadStatementASTContext context)
        {
            AddLine("LOAD_GLOBAL read");
            AddLine("CALL_FUNCTION 0");
            var scope = Visit(context.designator()) as string;
            var name = context.designator().GetText();

            AddLine($"STORE_{scope} {name}");

            return null;
        }

        public object VisitReturnStatementAST([NotNull] ReturnStatementASTContext context)
        {
            if(context.expr() == null)
            {
                AddLine("RETURN");
            }
            else
            {
                Visit(context.expr());

                AddLine("RETURN_VALUE");
            }
            return null;
        }

        public object VisitSemicolonStatementAST([NotNull] SemicolonStatementASTContext context)
        {
            return null;
        }

        public object VisitStringFactorAST([NotNull] StringFactorASTContext context)
        {
            AddLine($"LOAD_CONST {context.STRING().GetText()}");
            return null;
        }

        public object VisitSwitchAST([NotNull] SwitchASTContext context)
        {
            var cases = context.@case().ToList();

            int firstLine = LineCount;

            cases.ForEach(@case =>
            {
                Visit(context.expr());
                Visit(@case);
            });

            if(context.statement() != null)
            {
                Visit(context.statement());
            }

            int lastLine = LineCount - 1;

            var breaksIndex = CodeLines.Select((s, i) => (s.Split(" ")[1].Equals("BREAK") && i >= firstLine && i <= lastLine)? i : -1).Where(i => i != -1);


            
            breaksIndex.ToList().ForEach(i =>
            {
                SetLineOnRealIndexOf(i, $"JUMP_ABSOLUTE {LineCount}");
            });

            return null;
        }

        public object VisitSwitchStatementAST([NotNull] SwitchStatementASTContext context)
        {
            VisitChildren(context); 
            return null;
        }

        public object VisitTermAST([NotNull] TermASTContext context)
        {
            Visit(context.factor(0));

            for (int i = 1; i < context.factor().Length; i++)
            {
                Visit(context.factor(i));

                string instruction = "BINARY_";
                if (context.mulop(i - 1).GetText() == "*")
                    instruction += "MULTIPLY";
                else if(context.mulop(i - 1).GetText() == "/")
                    instruction += "DIVIDE";

                AddLine(instruction);
            }
            return null;
        }

        public object VisitTypeAST([NotNull] TypeASTContext context)
        {
            string type = context.GetText();
            if (type == "int" || type == "bool" || type == "float")
            {
                return "I";
            }
            else
            {
                return "C";
            }
        }

        public object VisitVarDeclAST([NotNull] VarDeclASTContext context)
        {

            string instruction = "PUSH_";
            if (context.Parent is ProgramASTContext)
                instruction += "GLOBAL_";
            else
                instruction += "LOCAL_";

            string type = Visit(context.type()) as string;
            instruction += type;

            context.ident().ToList().ForEach(ident =>
            {
                AddLine($"{instruction} {ident.GetText()}");
            });

            WhiteLine();
            return null;

        }

        public object VisitWhileStatementAST([NotNull] WhileStatementASTContext context)
        {
            if (context.condition() != null && context.statement() != null)
            {
                Visit(context.condition());

                int jumpIfFalse = LineCount;
                AddLine("JUMP_IF_FALSE");

                int jumpIfNext = LineCount;
                Visit(context.statement());
                Visit(context.condition());
                AddLine($"JUMP_IF_TRUE {jumpIfNext}");

                SetLineOnRealIndexOf(jumpIfFalse, $"JUMP_IF_FALSE {LineCount}");
            }
            return null;
        }

        public object VisitWriteStatementAST([NotNull] WriteStatementASTContext context)
        {
            Visit(context.expr());
            AddLine("LOAD_GLOBAL write");
            AddLine("CALL_FUNCTION 1");
            return null;
        }

        public object VisitBoolean([NotNull] BooleanContext context)
        {

            return context.value ? "true" : "false";
        }

        public object VisitCaseAST([NotNull] CaseASTContext context)
        {
            if(context.typeString != "bool")
            {
                var value = context.NUM() ?? context.CHARCONST() ?? context.STRING(); 
                AddLine($"LOAD_CONST {value.GetText()}");
            }
            else
            {
                var value = Visit(context.boolean()) as string;
                AddLine($"LOAD_CONST {value}");
            }

            AddLine("COMPARE_OP ==");
            int jumpIfFalsePosition = LineCount;
            AddLine("JUMP_IF_FALSE");
            if(context.statement() != null)
                Visit(context.statement());

            SetLineOnRealIndexOf(jumpIfFalsePosition, $"JUMP_IF_FALSE {LineCount}");

            return null;
        }
    }
}
