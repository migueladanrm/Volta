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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IVoltaParserListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class VoltaParserBaseListener : IVoltaParserListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="VoltaParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProgramAST([NotNull] VoltaParser.ProgramASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>programAST</c>
	/// labeled alternative in <see cref="VoltaParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProgramAST([NotNull] VoltaParser.ProgramASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>constDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.constDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterConstDeclAST([NotNull] VoltaParser.ConstDeclASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>constDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.constDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitConstDeclAST([NotNull] VoltaParser.ConstDeclASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>varDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.varDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>varDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.varDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitVarDeclAST([NotNull] VoltaParser.VarDeclASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>classDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.classDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>classDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.classDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitClassDeclAST([NotNull] VoltaParser.ClassDeclASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>methodDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.methodDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterMethodDeclAST([NotNull] VoltaParser.MethodDeclASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>methodDeclAST</c>
	/// labeled alternative in <see cref="VoltaParser.methodDecl"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitMethodDeclAST([NotNull] VoltaParser.MethodDeclASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.formPars"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFormParsAST([NotNull] VoltaParser.FormParsASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>formParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.formPars"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFormParsAST([NotNull] VoltaParser.FormParsASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="VoltaParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTypeAST([NotNull] VoltaParser.TypeASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>typeAST</c>
	/// labeled alternative in <see cref="VoltaParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTypeAST([NotNull] VoltaParser.TypeASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>callStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCallStatementAST([NotNull] VoltaParser.CallStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>callStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCallStatementAST([NotNull] VoltaParser.CallStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>assignStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssignStatementAST([NotNull] VoltaParser.AssignStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>assignStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssignStatementAST([NotNull] VoltaParser.AssignStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>addSubStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAddSubStatementAST([NotNull] VoltaParser.AddSubStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>addSubStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAddSubStatementAST([NotNull] VoltaParser.AddSubStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>ifStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIfStatementAST([NotNull] VoltaParser.IfStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>ifStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIfStatementAST([NotNull] VoltaParser.IfStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>forStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterForStatementAST([NotNull] VoltaParser.ForStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>forStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitForStatementAST([NotNull] VoltaParser.ForStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>whileStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWhileStatementAST([NotNull] VoltaParser.WhileStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>whileStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWhileStatementAST([NotNull] VoltaParser.WhileStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>breakStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBreakStatementAST([NotNull] VoltaParser.BreakStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>breakStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBreakStatementAST([NotNull] VoltaParser.BreakStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>switchStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSwitchStatementAST([NotNull] VoltaParser.SwitchStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>switchStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSwitchStatementAST([NotNull] VoltaParser.SwitchStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>returnStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReturnStatementAST([NotNull] VoltaParser.ReturnStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>returnStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReturnStatementAST([NotNull] VoltaParser.ReturnStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>readStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReadStatementAST([NotNull] VoltaParser.ReadStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>readStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReadStatementAST([NotNull] VoltaParser.ReadStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>writeStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWriteStatementAST([NotNull] VoltaParser.WriteStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>writeStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWriteStatementAST([NotNull] VoltaParser.WriteStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>blockStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlockStatementAST([NotNull] VoltaParser.BlockStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>blockStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlockStatementAST([NotNull] VoltaParser.BlockStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>semicolonStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSemicolonStatementAST([NotNull] VoltaParser.SemicolonStatementASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>semicolonStatementAST</c>
	/// labeled alternative in <see cref="VoltaParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSemicolonStatementAST([NotNull] VoltaParser.SemicolonStatementASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="VoltaParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBlockAST([NotNull] VoltaParser.BlockASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>blockAST</c>
	/// labeled alternative in <see cref="VoltaParser.block"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBlockAST([NotNull] VoltaParser.BlockASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.actPars"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterActParsAST([NotNull] VoltaParser.ActParsASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>actParsAST</c>
	/// labeled alternative in <see cref="VoltaParser.actPars"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitActParsAST([NotNull] VoltaParser.ActParsASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="VoltaParser.condition"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterConditionAST([NotNull] VoltaParser.ConditionASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>conditionAST</c>
	/// labeled alternative in <see cref="VoltaParser.condition"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitConditionAST([NotNull] VoltaParser.ConditionASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="VoltaParser.condTerm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCondTermAST([NotNull] VoltaParser.CondTermASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>condTermAST</c>
	/// labeled alternative in <see cref="VoltaParser.condTerm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCondTermAST([NotNull] VoltaParser.CondTermASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="VoltaParser.condFact"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCondFactAST([NotNull] VoltaParser.CondFactASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>condFactAST</c>
	/// labeled alternative in <see cref="VoltaParser.condFact"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCondFactAST([NotNull] VoltaParser.CondFactASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="VoltaParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExprAST([NotNull] VoltaParser.ExprASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>exprAST</c>
	/// labeled alternative in <see cref="VoltaParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExprAST([NotNull] VoltaParser.ExprASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="VoltaParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTermAST([NotNull] VoltaParser.TermASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>termAST</c>
	/// labeled alternative in <see cref="VoltaParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTermAST([NotNull] VoltaParser.TermASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>identOrCallFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIdentOrCallFactorAST([NotNull] VoltaParser.IdentOrCallFactorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>identOrCallFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIdentOrCallFactorAST([NotNull] VoltaParser.IdentOrCallFactorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>numFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNumFactorAST([NotNull] VoltaParser.NumFactorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>numFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNumFactorAST([NotNull] VoltaParser.NumFactorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>charConstFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>charConstFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCharConstFactorAST([NotNull] VoltaParser.CharConstFactorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>stringFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStringFactorAST([NotNull] VoltaParser.StringFactorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>stringFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStringFactorAST([NotNull] VoltaParser.StringFactorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>booleanFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBooleanFactorAST([NotNull] VoltaParser.BooleanFactorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>booleanFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBooleanFactorAST([NotNull] VoltaParser.BooleanFactorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>newFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNewFactorAST([NotNull] VoltaParser.NewFactorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>newFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNewFactorAST([NotNull] VoltaParser.NewFactorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>bracketFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBracketFactorAST([NotNull] VoltaParser.BracketFactorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>bracketFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBracketFactorAST([NotNull] VoltaParser.BracketFactorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>nullFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNullFactorAST([NotNull] VoltaParser.NullFactorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>nullFactorAST</c>
	/// labeled alternative in <see cref="VoltaParser.factor"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNullFactorAST([NotNull] VoltaParser.NullFactorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="VoltaParser.designator"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDesignatorAST([NotNull] VoltaParser.DesignatorASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>designatorAST</c>
	/// labeled alternative in <see cref="VoltaParser.designator"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDesignatorAST([NotNull] VoltaParser.DesignatorASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="VoltaParser.mulop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterMulop([NotNull] VoltaParser.MulopContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="VoltaParser.mulop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitMulop([NotNull] VoltaParser.MulopContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>addopAST</c>
	/// labeled alternative in <see cref="VoltaParser.addop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAddopAST([NotNull] VoltaParser.AddopASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>addopAST</c>
	/// labeled alternative in <see cref="VoltaParser.addop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAddopAST([NotNull] VoltaParser.AddopASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>equalEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>equalEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEqualEqualRelopAST([NotNull] VoltaParser.EqualEqualRelopASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>notEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNotEqualRelopAST([NotNull] VoltaParser.NotEqualRelopASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>notEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNotEqualRelopAST([NotNull] VoltaParser.NotEqualRelopASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>greaterEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGreaterEqualRelopAST([NotNull] VoltaParser.GreaterEqualRelopASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>greaterEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGreaterEqualRelopAST([NotNull] VoltaParser.GreaterEqualRelopASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>lessEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLessEqualRelopAST([NotNull] VoltaParser.LessEqualRelopASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>lessEqualRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLessEqualRelopAST([NotNull] VoltaParser.LessEqualRelopASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>greaterRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGreaterRelopAST([NotNull] VoltaParser.GreaterRelopASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>greaterRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGreaterRelopAST([NotNull] VoltaParser.GreaterRelopASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>lessRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLessRelopAST([NotNull] VoltaParser.LessRelopASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>lessRelopAST</c>
	/// labeled alternative in <see cref="VoltaParser.relop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLessRelopAST([NotNull] VoltaParser.LessRelopASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="VoltaParser.boolean"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBoolean([NotNull] VoltaParser.BooleanContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="VoltaParser.boolean"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBoolean([NotNull] VoltaParser.BooleanContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>switchAST</c>
	/// labeled alternative in <see cref="VoltaParser.switch"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSwitchAST([NotNull] VoltaParser.SwitchASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>switchAST</c>
	/// labeled alternative in <see cref="VoltaParser.switch"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSwitchAST([NotNull] VoltaParser.SwitchASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>caseAST</c>
	/// labeled alternative in <see cref="VoltaParser.case"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCaseAST([NotNull] VoltaParser.CaseASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>caseAST</c>
	/// labeled alternative in <see cref="VoltaParser.case"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCaseAST([NotNull] VoltaParser.CaseASTContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>identAST</c>
	/// labeled alternative in <see cref="VoltaParser.ident"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIdentAST([NotNull] VoltaParser.IdentASTContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>identAST</c>
	/// labeled alternative in <see cref="VoltaParser.ident"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIdentAST([NotNull] VoltaParser.IdentASTContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
