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
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IVoltaParserVisitor{Result}"/>,
/// which can be extended to create a visitor which only needs to handle a subset
/// of the available methods.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class VoltaParserBaseVisitor<Result> : AbstractParseTreeVisitor<Result>, IVoltaParserVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="VoltaParser.program"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitProgramAST([NotNull] VoltaParser.ProgramASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>constDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.constDecl"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitConstDeclAST([NotNull] VoltaParser.ConstDeclASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>varDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.varDecl"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>classDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.classDecl"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>methodDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.methodDecl"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitMethodDeclAST([NotNull] VoltaParser.MethodDeclASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.formPars"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitFormParsAST([NotNull] VoltaParser.FormParsASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="VoltaParser.type"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitTypeAST([NotNull] VoltaParser.TypeASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>callStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCallStatementAST([NotNull] VoltaParser.CallStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>assignStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitAssignStatementAST([NotNull] VoltaParser.AssignStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>addsubStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitAddsubStatementAST([NotNull] VoltaParser.AddsubStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>ifStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitIfStatementAST([NotNull] VoltaParser.IfStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>forStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitForStatementAST([NotNull] VoltaParser.ForStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>whileStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitWhileStatementAST([NotNull] VoltaParser.WhileStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>breakStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitBreakStatementAST([NotNull] VoltaParser.BreakStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>switchStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitSwitchStatementAST([NotNull] VoltaParser.SwitchStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>returnStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitReturnStatementAST([NotNull] VoltaParser.ReturnStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>readStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitReadStatementAST([NotNull] VoltaParser.ReadStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>writeStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitWriteStatementAST([NotNull] VoltaParser.WriteStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>blockStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitBlockStatementAST([NotNull] VoltaParser.BlockStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>semicolonStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitSemicolonStatementAST([NotNull] VoltaParser.SemicolonStatementASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="VoltaParser.block"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitBlockAST([NotNull] VoltaParser.BlockASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.actPars"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitActParsAST([NotNull] VoltaParser.ActParsASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="VoltaParser.condition"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitConditionAST([NotNull] VoltaParser.ConditionASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="VoltaParser.condTerm"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCondTermAST([NotNull] VoltaParser.CondTermASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="VoltaParser.condFact"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCondFactAST([NotNull] VoltaParser.CondFactASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="VoltaParser.expr"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitExprAST([NotNull] VoltaParser.ExprASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="VoltaParser.term"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitTermAST([NotNull] VoltaParser.TermASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>identOrCallFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitIdentOrCallFactorAST([NotNull] VoltaParser.IdentOrCallFactorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>numFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitNumFactorAST([NotNull] VoltaParser.NumFactorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>charConstFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>stringFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitStringFactorAST([NotNull] VoltaParser.StringFactorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>booleanFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitBooleanFactorAST([NotNull] VoltaParser.BooleanFactorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>newFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitNewFactorAST([NotNull] VoltaParser.NewFactorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>bracketFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitBracketFactorAST([NotNull] VoltaParser.BracketFactorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>nullFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitNullFactorAST([NotNull] VoltaParser.NullFactorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="VoltaParser.designator"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitDesignatorAST([NotNull] VoltaParser.DesignatorASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="VoltaParser.mulop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitMulop([NotNull] VoltaParser.MulopContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>addopAST</c>
	/// labeled alternative in <see cref="VoltaParser.addop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitAddopAST([NotNull] VoltaParser.AddopASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>equalEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>notEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitNotEqualRelopAST([NotNull] VoltaParser.NotEqualRelopASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>greaterEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitGreaterEqualRelopAST([NotNull] VoltaParser.GreaterEqualRelopASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>lessEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLessEqualRelopAST([NotNull] VoltaParser.LessEqualRelopASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>greaterRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitGreaterRelopAST([NotNull] VoltaParser.GreaterRelopASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>lessRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLessRelopAST([NotNull] VoltaParser.LessRelopASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>switchAST</c>
	/// labeled alternative in <see cref="VoltaParser.switch"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitSwitchAST([NotNull] VoltaParser.SwitchASTContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by the <c>identAST</c>
	/// labeled alternative in <see cref="VoltaParser.ident"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitIdentAST([NotNull] VoltaParser.IdentASTContext context) { return VisitChildren(context); }
}
