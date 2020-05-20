//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ../Volta.g4//VoltaParser.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="VoltaParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface IVoltaParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="VoltaParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgramAST([NotNull] VoltaParser.ProgramASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="VoltaParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgramAST([NotNull] VoltaParser.ProgramASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>constDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.constDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstDeclAST([NotNull] VoltaParser.ConstDeclASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>constDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.constDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstDeclAST([NotNull] VoltaParser.ConstDeclASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>varDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.varDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>varDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.varDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>classDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>classDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>methodDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.methodDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodDeclAST([NotNull] VoltaParser.MethodDeclASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>methodDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.methodDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodDeclAST([NotNull] VoltaParser.MethodDeclASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.formPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFormParsAST([NotNull] VoltaParser.FormParsASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.formPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFormParsAST([NotNull] VoltaParser.FormParsASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="VoltaParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTypeAST([NotNull] VoltaParser.TypeASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="VoltaParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTypeAST([NotNull] VoltaParser.TypeASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>callORassignStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCallORassignStatementAST([NotNull] VoltaParser.CallORassignStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>callORassignStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCallORassignStatementAST([NotNull] VoltaParser.CallORassignStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ifStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStatementAST([NotNull] VoltaParser.IfStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ifStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStatementAST([NotNull] VoltaParser.IfStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>forStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForStatementAST([NotNull] VoltaParser.ForStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>forStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForStatementAST([NotNull] VoltaParser.ForStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>whileStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileStatementAST([NotNull] VoltaParser.WhileStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>whileStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileStatementAST([NotNull] VoltaParser.WhileStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>breakStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBreakStatementAST([NotNull] VoltaParser.BreakStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>breakStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBreakStatementAST([NotNull] VoltaParser.BreakStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>switchStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSwitchStatementAST([NotNull] VoltaParser.SwitchStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>switchStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSwitchStatementAST([NotNull] VoltaParser.SwitchStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>returnStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReturnStatementAST([NotNull] VoltaParser.ReturnStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>returnStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReturnStatementAST([NotNull] VoltaParser.ReturnStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>readStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReadStatementAST([NotNull] VoltaParser.ReadStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>readStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReadStatementAST([NotNull] VoltaParser.ReadStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>writeStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWriteStatementAST([NotNull] VoltaParser.WriteStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>writeStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWriteStatementAST([NotNull] VoltaParser.WriteStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>blockStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockStatementAST([NotNull] VoltaParser.BlockStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>blockStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockStatementAST([NotNull] VoltaParser.BlockStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>semicolonStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSemicolonStatementAST([NotNull] VoltaParser.SemicolonStatementASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>semicolonStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSemicolonStatementAST([NotNull] VoltaParser.SemicolonStatementASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="VoltaParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockAST([NotNull] VoltaParser.BlockASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="VoltaParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockAST([NotNull] VoltaParser.BlockASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.actPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActParsAST([NotNull] VoltaParser.ActParsASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.actPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActParsAST([NotNull] VoltaParser.ActParsASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="VoltaParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConditionAST([NotNull] VoltaParser.ConditionASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="VoltaParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConditionAST([NotNull] VoltaParser.ConditionASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="VoltaParser.condTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondTermAST([NotNull] VoltaParser.CondTermASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="VoltaParser.condTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondTermAST([NotNull] VoltaParser.CondTermASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="VoltaParser.condFact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondFactAST([NotNull] VoltaParser.CondFactASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="VoltaParser.condFact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondFactAST([NotNull] VoltaParser.CondFactASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="VoltaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExprAST([NotNull] VoltaParser.ExprASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="VoltaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExprAST([NotNull] VoltaParser.ExprASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="VoltaParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTermAST([NotNull] VoltaParser.TermASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="VoltaParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTermAST([NotNull] VoltaParser.TermASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>callFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCallFactorAST([NotNull] VoltaParser.CallFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>callFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCallFactorAST([NotNull] VoltaParser.CallFactorASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>numFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumFactorAST([NotNull] VoltaParser.NumFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>numFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumFactorAST([NotNull] VoltaParser.NumFactorASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>charConstFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>charConstFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>bolleanFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBolleanFactorAST([NotNull] VoltaParser.BolleanFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>bolleanFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBolleanFactorAST([NotNull] VoltaParser.BolleanFactorASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>newFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNewFactorAST([NotNull] VoltaParser.NewFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>newFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNewFactorAST([NotNull] VoltaParser.NewFactorASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>bracketFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBracketFactorAST([NotNull] VoltaParser.BracketFactorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>bracketFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBracketFactorAST([NotNull] VoltaParser.BracketFactorASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="VoltaParser.designator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDesignatorAST([NotNull] VoltaParser.DesignatorASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="VoltaParser.designator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDesignatorAST([NotNull] VoltaParser.DesignatorASTContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="VoltaParser.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMulop([NotNull] VoltaParser.MulopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="VoltaParser.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMulop([NotNull] VoltaParser.MulopContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>addopAST</c>
	/// labeled alternative in <see cref="VoltaParser.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddopAST([NotNull] VoltaParser.AddopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>addopAST</c>
	/// labeled alternative in <see cref="VoltaParser.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddopAST([NotNull] VoltaParser.AddopASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>equalEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>equalEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>notEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotEqualRelopAST([NotNull] VoltaParser.NotEqualRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>notEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotEqualRelopAST([NotNull] VoltaParser.NotEqualRelopASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>greaterEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGreaterEqualRelopAST([NotNull] VoltaParser.GreaterEqualRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>greaterEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGreaterEqualRelopAST([NotNull] VoltaParser.GreaterEqualRelopASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>lessEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLessEqualRelopAST([NotNull] VoltaParser.LessEqualRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>lessEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLessEqualRelopAST([NotNull] VoltaParser.LessEqualRelopASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>greaterRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGreaterRelopAST([NotNull] VoltaParser.GreaterRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>greaterRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGreaterRelopAST([NotNull] VoltaParser.GreaterRelopASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>lessRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLessRelopAST([NotNull] VoltaParser.LessRelopASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>lessRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLessRelopAST([NotNull] VoltaParser.LessRelopASTContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>switchAST</c>
	/// labeled alternative in <see cref="VoltaParser.switch"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSwitchAST([NotNull] VoltaParser.SwitchASTContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>switchAST</c>
	/// labeled alternative in <see cref="VoltaParser.switch"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSwitchAST([NotNull] VoltaParser.SwitchASTContext context);
}
