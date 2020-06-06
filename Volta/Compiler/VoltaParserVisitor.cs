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
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="VoltaParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface IVoltaParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="VoltaParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgramAST([NotNull] VoltaParser.ProgramASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>constDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.constDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstDeclAST([NotNull] VoltaParser.ConstDeclASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>varDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.varDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>classDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>methodDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.methodDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodDeclAST([NotNull] VoltaParser.MethodDeclASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.formPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFormParsAST([NotNull] VoltaParser.FormParsASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="VoltaParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeAST([NotNull] VoltaParser.TypeASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>callORassignStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCallORassignStatementAST([NotNull] VoltaParser.CallORassignStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ifStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatementAST([NotNull] VoltaParser.IfStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>forStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatementAST([NotNull] VoltaParser.ForStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>whileStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatementAST([NotNull] VoltaParser.WhileStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>breakStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBreakStatementAST([NotNull] VoltaParser.BreakStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>switchStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSwitchStatementAST([NotNull] VoltaParser.SwitchStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>returnStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStatementAST([NotNull] VoltaParser.ReturnStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>readStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReadStatementAST([NotNull] VoltaParser.ReadStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>writeStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWriteStatementAST([NotNull] VoltaParser.WriteStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>blockStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockStatementAST([NotNull] VoltaParser.BlockStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>semicolonStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSemicolonStatementAST([NotNull] VoltaParser.SemicolonStatementASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="VoltaParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockAST([NotNull] VoltaParser.BlockASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.actPars"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActParsAST([NotNull] VoltaParser.ActParsASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="VoltaParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConditionAST([NotNull] VoltaParser.ConditionASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="VoltaParser.condTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondTermAST([NotNull] VoltaParser.CondTermASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="VoltaParser.condFact"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondFactAST([NotNull] VoltaParser.CondFactASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="VoltaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprAST([NotNull] VoltaParser.ExprASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="VoltaParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTermAST([NotNull] VoltaParser.TermASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>callFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCallFactorAST([NotNull] VoltaParser.CallFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>numFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumFactorAST([NotNull] VoltaParser.NumFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>charConstFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>stringFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringFactorAST([NotNull] VoltaParser.StringFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>bolleanFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBolleanFactorAST([NotNull] VoltaParser.BolleanFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>newFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNewFactorAST([NotNull] VoltaParser.NewFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>bracketFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBracketFactorAST([NotNull] VoltaParser.BracketFactorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="VoltaParser.designator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDesignatorAST([NotNull] VoltaParser.DesignatorASTContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="VoltaParser.mulop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMulop([NotNull] VoltaParser.MulopContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>addopAST</c>
	/// labeled alternative in <see cref="VoltaParser.addop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddopAST([NotNull] VoltaParser.AddopASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>equalEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>notEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotEqualRelopAST([NotNull] VoltaParser.NotEqualRelopASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>greaterEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGreaterEqualRelopAST([NotNull] VoltaParser.GreaterEqualRelopASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>lessEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLessEqualRelopAST([NotNull] VoltaParser.LessEqualRelopASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>greaterRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGreaterRelopAST([NotNull] VoltaParser.GreaterRelopASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>lessRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLessRelopAST([NotNull] VoltaParser.LessRelopASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>switchAST</c>
	/// labeled alternative in <see cref="VoltaParser.switch"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSwitchAST([NotNull] VoltaParser.SwitchASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>identAST</c>
	/// labeled alternative in <see cref="VoltaParser.ident"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentAST([NotNull] VoltaParser.IdentASTContext context);
}
