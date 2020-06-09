//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ../Volta.g4//VoltaScanner.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class VoltaScanner : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		NOT=1, HASH=2, DOLAR=3, AND=4, OR=5, BL=6, BR=7, SEMICOLON=8, ADD=9, ADDADD=10, 
		SUB=11, SUBSUB=12, MUL=13, DIV=14, MDIV=15, EQUALEQUAL=16, NOTEQUAL=17, 
		GREATEREQUAL=18, LESSEQUAL=19, EQUAL=20, GREATER=21, LESS=22, CURLYBL=23, 
		CURLYBR=24, SQUAREBL=25, SQUAREBR=26, COLON=27, DOT=28, COMMA=29, QUESTIONMARK=30, 
		CLASS=31, CONST=32, ELSE=33, IF=34, NEW=35, READ=36, RETURN=37, VOID=38, 
		WHILE=39, WRITE=40, TRUE=41, FALSE=42, FOR=43, SWITCH=44, CASE=45, BREAK=46, 
		DEFAULT=47, IDENT=48, NUM=49, CHARCONST=50, STRING=51, COMMENT=52, WS=53;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"NOT", "QMARK", "SQMARK", "HASH", "DOLAR", "AND", "OR", "BL", "BR", "SEMICOLON", 
		"ADD", "ADDADD", "SUB", "SUBSUB", "MUL", "DIV", "MDIV", "EQUALEQUAL", 
		"NOTEQUAL", "GREATEREQUAL", "LESSEQUAL", "EQUAL", "GREATER", "LESS", "CURLYBL", 
		"CURLYBR", "SQUAREBL", "SQUAREBR", "COLON", "DOT", "COMMA", "QUESTIONMARK", 
		"CLASS", "CONST", "ELSE", "IF", "NEW", "READ", "RETURN", "VOID", "WHILE", 
		"WRITE", "TRUE", "FALSE", "FOR", "SWITCH", "CASE", "BREAK", "DEFAULT", 
		"IDENT", "NUM", "CHARCONST", "STRING", "COMMENT", "WS", "LETTER", "PRINTABLE_CHAR"
	};


	public VoltaScanner(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public VoltaScanner(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'!'", "'#'", "'$'", "'&&'", "'||'", "'('", "')'", "';'", "'+'", 
		"'++'", "'-'", "'--'", "'*'", "'/'", "'%'", "'=='", "'!='", "'>='", "'<='", 
		"'='", "'>'", "'<'", "'{'", "'}'", "'['", "']'", "':'", "'.'", "','", 
		"'?'", "'class'", "'const'", "'else'", "'if'", "'new'", "'read'", "'return'", 
		"'void'", "'while'", "'write'", "'true'", "'false'", "'for'", "'switch'", 
		"'case'", "'break'", "'default'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "NOT", "HASH", "DOLAR", "AND", "OR", "BL", "BR", "SEMICOLON", "ADD", 
		"ADDADD", "SUB", "SUBSUB", "MUL", "DIV", "MDIV", "EQUALEQUAL", "NOTEQUAL", 
		"GREATEREQUAL", "LESSEQUAL", "EQUAL", "GREATER", "LESS", "CURLYBL", "CURLYBR", 
		"SQUAREBL", "SQUAREBR", "COLON", "DOT", "COMMA", "QUESTIONMARK", "CLASS", 
		"CONST", "ELSE", "IF", "NEW", "READ", "RETURN", "VOID", "WHILE", "WRITE", 
		"TRUE", "FALSE", "FOR", "SWITCH", "CASE", "BREAK", "DEFAULT", "IDENT", 
		"NUM", "CHARCONST", "STRING", "COMMENT", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "VoltaScanner.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static VoltaScanner() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\x37', '\x177', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', '\x4', 
		'#', '\t', '#', '\x4', '$', '\t', '$', '\x4', '%', '\t', '%', '\x4', '&', 
		'\t', '&', '\x4', '\'', '\t', '\'', '\x4', '(', '\t', '(', '\x4', ')', 
		'\t', ')', '\x4', '*', '\t', '*', '\x4', '+', '\t', '+', '\x4', ',', '\t', 
		',', '\x4', '-', '\t', '-', '\x4', '.', '\t', '.', '\x4', '/', '\t', '/', 
		'\x4', '\x30', '\t', '\x30', '\x4', '\x31', '\t', '\x31', '\x4', '\x32', 
		'\t', '\x32', '\x4', '\x33', '\t', '\x33', '\x4', '\x34', '\t', '\x34', 
		'\x4', '\x35', '\t', '\x35', '\x4', '\x36', '\t', '\x36', '\x4', '\x37', 
		'\t', '\x37', '\x4', '\x38', '\t', '\x38', '\x4', '\x39', '\t', '\x39', 
		'\x4', ':', '\t', ':', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\b', 
		'\x3', '\b', '\x3', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\n', '\x3', 
		'\n', '\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', '\r', 
		'\x3', '\r', '\x3', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', 
		'\xF', '\x3', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', '\x11', '\x3', 
		'\x11', '\x3', '\x12', '\x3', '\x12', '\x3', '\x13', '\x3', '\x13', '\x3', 
		'\x13', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', '\x15', '\x3', 
		'\x15', '\x3', '\x15', '\x3', '\x16', '\x3', '\x16', '\x3', '\x16', '\x3', 
		'\x17', '\x3', '\x17', '\x3', '\x18', '\x3', '\x18', '\x3', '\x19', '\x3', 
		'\x19', '\x3', '\x1A', '\x3', '\x1A', '\x3', '\x1B', '\x3', '\x1B', '\x3', 
		'\x1C', '\x3', '\x1C', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1E', '\x3', 
		'\x1E', '\x3', '\x1F', '\x3', '\x1F', '\x3', ' ', '\x3', ' ', '\x3', '!', 
		'\x3', '!', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', 
		'\"', '\x3', '\"', '\x3', '#', '\x3', '#', '\x3', '#', '\x3', '#', '\x3', 
		'#', '\x3', '#', '\x3', '$', '\x3', '$', '\x3', '$', '\x3', '$', '\x3', 
		'$', '\x3', '%', '\x3', '%', '\x3', '%', '\x3', '&', '\x3', '&', '\x3', 
		'&', '\x3', '&', '\x3', '\'', '\x3', '\'', '\x3', '\'', '\x3', '\'', '\x3', 
		'\'', '\x3', '(', '\x3', '(', '\x3', '(', '\x3', '(', '\x3', '(', '\x3', 
		'(', '\x3', '(', '\x3', ')', '\x3', ')', '\x3', ')', '\x3', ')', '\x3', 
		')', '\x3', '*', '\x3', '*', '\x3', '*', '\x3', '*', '\x3', '*', '\x3', 
		'*', '\x3', '+', '\x3', '+', '\x3', '+', '\x3', '+', '\x3', '+', '\x3', 
		'+', '\x3', ',', '\x3', ',', '\x3', ',', '\x3', ',', '\x3', ',', '\x3', 
		'-', '\x3', '-', '\x3', '-', '\x3', '-', '\x3', '-', '\x3', '-', '\x3', 
		'.', '\x3', '.', '\x3', '.', '\x3', '.', '\x3', '/', '\x3', '/', '\x3', 
		'/', '\x3', '/', '\x3', '/', '\x3', '/', '\x3', '/', '\x3', '\x30', '\x3', 
		'\x30', '\x3', '\x30', '\x3', '\x30', '\x3', '\x30', '\x3', '\x31', '\x3', 
		'\x31', '\x3', '\x31', '\x3', '\x31', '\x3', '\x31', '\x3', '\x31', '\x3', 
		'\x32', '\x3', '\x32', '\x3', '\x32', '\x3', '\x32', '\x3', '\x32', '\x3', 
		'\x32', '\x3', '\x32', '\x3', '\x32', '\x3', '\x33', '\x3', '\x33', '\x5', 
		'\x33', '\x11E', '\n', '\x33', '\x3', '\x33', '\x3', '\x33', '\x3', '\x33', 
		'\a', '\x33', '\x123', '\n', '\x33', '\f', '\x33', '\xE', '\x33', '\x126', 
		'\v', '\x33', '\x3', '\x34', '\x3', '\x34', '\a', '\x34', '\x12A', '\n', 
		'\x34', '\f', '\x34', '\xE', '\x34', '\x12D', '\v', '\x34', '\x3', '\x34', 
		'\x5', '\x34', '\x130', '\n', '\x34', '\x3', '\x34', '\x3', '\x34', '\a', 
		'\x34', '\x134', '\n', '\x34', '\f', '\x34', '\xE', '\x34', '\x137', '\v', 
		'\x34', '\x5', '\x34', '\x139', '\n', '\x34', '\x3', '\x35', '\x3', '\x35', 
		'\x3', '\x35', '\x3', '\x35', '\x3', '\x35', '\x3', '\x35', '\x5', '\x35', 
		'\x141', '\n', '\x35', '\x3', '\x35', '\x3', '\x35', '\x3', '\x36', '\x3', 
		'\x36', '\x3', '\x36', '\x3', '\x36', '\a', '\x36', '\x149', '\n', '\x36', 
		'\f', '\x36', '\xE', '\x36', '\x14C', '\v', '\x36', '\x3', '\x36', '\x3', 
		'\x36', '\x3', '\x37', '\x3', '\x37', '\x3', '\x37', '\x3', '\x37', '\a', 
		'\x37', '\x154', '\n', '\x37', '\f', '\x37', '\xE', '\x37', '\x157', '\v', 
		'\x37', '\x3', '\x37', '\x3', '\x37', '\x3', '\x37', '\x3', '\x37', '\x3', 
		'\x37', '\a', '\x37', '\x15E', '\n', '\x37', '\f', '\x37', '\xE', '\x37', 
		'\x161', '\v', '\x37', '\x3', '\x37', '\x3', '\x37', '\x6', '\x37', '\x165', 
		'\n', '\x37', '\r', '\x37', '\xE', '\x37', '\x166', '\x3', '\x37', '\x3', 
		'\x37', '\x3', '\x38', '\x6', '\x38', '\x16C', '\n', '\x38', '\r', '\x38', 
		'\xE', '\x38', '\x16D', '\x3', '\x38', '\x3', '\x38', '\x3', '\x39', '\x3', 
		'\x39', '\x3', ':', '\x3', ':', '\x5', ':', '\x176', '\n', ':', '\x3', 
		'\x15F', '\x2', ';', '\x3', '\x3', '\x5', '\x2', '\a', '\x2', '\t', '\x4', 
		'\v', '\x5', '\r', '\x6', '\xF', '\a', '\x11', '\b', '\x13', '\t', '\x15', 
		'\n', '\x17', '\v', '\x19', '\f', '\x1B', '\r', '\x1D', '\xE', '\x1F', 
		'\xF', '!', '\x10', '#', '\x11', '%', '\x12', '\'', '\x13', ')', '\x14', 
		'+', '\x15', '-', '\x16', '/', '\x17', '\x31', '\x18', '\x33', '\x19', 
		'\x35', '\x1A', '\x37', '\x1B', '\x39', '\x1C', ';', '\x1D', '=', '\x1E', 
		'?', '\x1F', '\x41', ' ', '\x43', '!', '\x45', '\"', 'G', '#', 'I', '$', 
		'K', '%', 'M', '&', 'O', '\'', 'Q', '(', 'S', ')', 'U', '*', 'W', '+', 
		'Y', ',', '[', '-', ']', '.', '_', '/', '\x61', '\x30', '\x63', '\x31', 
		'\x65', '\x32', 'g', '\x33', 'i', '\x34', 'k', '\x35', 'm', '\x36', 'o', 
		'\x37', 'q', '\x2', 's', '\x2', '\x3', '\x2', '\t', '\x3', '\x2', '\x61', 
		'\x61', '\x3', '\x2', '\x33', ';', '\x3', '\x2', '\x32', ';', '\x3', '\x2', 
		'$', '$', '\x4', '\x2', '\f', '\f', '\xF', '\xF', '\x5', '\x2', '\v', 
		'\f', '\xF', '\xF', '\"', '\"', '\x4', '\x2', '\x43', '\\', '\x63', '|', 
		'\x2', '\x184', '\x2', '\x3', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\r', '\x3', '\x2', '\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', 
		')', '\x3', '\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '-', '\x3', '\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x31', '\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x37', '\x3', '\x2', '\x2', '\x2', '\x2', '\x39', '\x3', '\x2', '\x2', 
		'\x2', '\x2', ';', '\x3', '\x2', '\x2', '\x2', '\x2', '=', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '?', '\x3', '\x2', '\x2', '\x2', '\x2', '\x41', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x43', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x45', '\x3', '\x2', '\x2', '\x2', '\x2', 'G', '\x3', '\x2', '\x2', '\x2', 
		'\x2', 'I', '\x3', '\x2', '\x2', '\x2', '\x2', 'K', '\x3', '\x2', '\x2', 
		'\x2', '\x2', 'M', '\x3', '\x2', '\x2', '\x2', '\x2', 'O', '\x3', '\x2', 
		'\x2', '\x2', '\x2', 'Q', '\x3', '\x2', '\x2', '\x2', '\x2', 'S', '\x3', 
		'\x2', '\x2', '\x2', '\x2', 'U', '\x3', '\x2', '\x2', '\x2', '\x2', 'W', 
		'\x3', '\x2', '\x2', '\x2', '\x2', 'Y', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'[', '\x3', '\x2', '\x2', '\x2', '\x2', ']', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '_', '\x3', '\x2', '\x2', '\x2', '\x2', '\x61', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x63', '\x3', '\x2', '\x2', '\x2', '\x2', '\x65', '\x3', 
		'\x2', '\x2', '\x2', '\x2', 'g', '\x3', '\x2', '\x2', '\x2', '\x2', 'i', 
		'\x3', '\x2', '\x2', '\x2', '\x2', 'k', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'm', '\x3', '\x2', '\x2', '\x2', '\x2', 'o', '\x3', '\x2', '\x2', '\x2', 
		'\x3', 'u', '\x3', '\x2', '\x2', '\x2', '\x5', 'w', '\x3', '\x2', '\x2', 
		'\x2', '\a', 'y', '\x3', '\x2', '\x2', '\x2', '\t', '{', '\x3', '\x2', 
		'\x2', '\x2', '\v', '}', '\x3', '\x2', '\x2', '\x2', '\r', '\x7F', '\x3', 
		'\x2', '\x2', '\x2', '\xF', '\x82', '\x3', '\x2', '\x2', '\x2', '\x11', 
		'\x85', '\x3', '\x2', '\x2', '\x2', '\x13', '\x87', '\x3', '\x2', '\x2', 
		'\x2', '\x15', '\x89', '\x3', '\x2', '\x2', '\x2', '\x17', '\x8B', '\x3', 
		'\x2', '\x2', '\x2', '\x19', '\x8D', '\x3', '\x2', '\x2', '\x2', '\x1B', 
		'\x90', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x92', '\x3', '\x2', '\x2', 
		'\x2', '\x1F', '\x95', '\x3', '\x2', '\x2', '\x2', '!', '\x97', '\x3', 
		'\x2', '\x2', '\x2', '#', '\x99', '\x3', '\x2', '\x2', '\x2', '%', '\x9B', 
		'\x3', '\x2', '\x2', '\x2', '\'', '\x9E', '\x3', '\x2', '\x2', '\x2', 
		')', '\xA1', '\x3', '\x2', '\x2', '\x2', '+', '\xA4', '\x3', '\x2', '\x2', 
		'\x2', '-', '\xA7', '\x3', '\x2', '\x2', '\x2', '/', '\xA9', '\x3', '\x2', 
		'\x2', '\x2', '\x31', '\xAB', '\x3', '\x2', '\x2', '\x2', '\x33', '\xAD', 
		'\x3', '\x2', '\x2', '\x2', '\x35', '\xAF', '\x3', '\x2', '\x2', '\x2', 
		'\x37', '\xB1', '\x3', '\x2', '\x2', '\x2', '\x39', '\xB3', '\x3', '\x2', 
		'\x2', '\x2', ';', '\xB5', '\x3', '\x2', '\x2', '\x2', '=', '\xB7', '\x3', 
		'\x2', '\x2', '\x2', '?', '\xB9', '\x3', '\x2', '\x2', '\x2', '\x41', 
		'\xBB', '\x3', '\x2', '\x2', '\x2', '\x43', '\xBD', '\x3', '\x2', '\x2', 
		'\x2', '\x45', '\xC3', '\x3', '\x2', '\x2', '\x2', 'G', '\xC9', '\x3', 
		'\x2', '\x2', '\x2', 'I', '\xCE', '\x3', '\x2', '\x2', '\x2', 'K', '\xD1', 
		'\x3', '\x2', '\x2', '\x2', 'M', '\xD5', '\x3', '\x2', '\x2', '\x2', 'O', 
		'\xDA', '\x3', '\x2', '\x2', '\x2', 'Q', '\xE1', '\x3', '\x2', '\x2', 
		'\x2', 'S', '\xE6', '\x3', '\x2', '\x2', '\x2', 'U', '\xEC', '\x3', '\x2', 
		'\x2', '\x2', 'W', '\xF2', '\x3', '\x2', '\x2', '\x2', 'Y', '\xF7', '\x3', 
		'\x2', '\x2', '\x2', '[', '\xFD', '\x3', '\x2', '\x2', '\x2', ']', '\x101', 
		'\x3', '\x2', '\x2', '\x2', '_', '\x108', '\x3', '\x2', '\x2', '\x2', 
		'\x61', '\x10D', '\x3', '\x2', '\x2', '\x2', '\x63', '\x113', '\x3', '\x2', 
		'\x2', '\x2', '\x65', '\x11D', '\x3', '\x2', '\x2', '\x2', 'g', '\x12F', 
		'\x3', '\x2', '\x2', '\x2', 'i', '\x13A', '\x3', '\x2', '\x2', '\x2', 
		'k', '\x144', '\x3', '\x2', '\x2', '\x2', 'm', '\x164', '\x3', '\x2', 
		'\x2', '\x2', 'o', '\x16B', '\x3', '\x2', '\x2', '\x2', 'q', '\x171', 
		'\x3', '\x2', '\x2', '\x2', 's', '\x175', '\x3', '\x2', '\x2', '\x2', 
		'u', 'v', '\a', '#', '\x2', '\x2', 'v', '\x4', '\x3', '\x2', '\x2', '\x2', 
		'w', 'x', '\a', '$', '\x2', '\x2', 'x', '\x6', '\x3', '\x2', '\x2', '\x2', 
		'y', 'z', '\a', ')', '\x2', '\x2', 'z', '\b', '\x3', '\x2', '\x2', '\x2', 
		'{', '|', '\a', '%', '\x2', '\x2', '|', '\n', '\x3', '\x2', '\x2', '\x2', 
		'}', '~', '\a', '&', '\x2', '\x2', '~', '\f', '\x3', '\x2', '\x2', '\x2', 
		'\x7F', '\x80', '\a', '(', '\x2', '\x2', '\x80', '\x81', '\a', '(', '\x2', 
		'\x2', '\x81', '\xE', '\x3', '\x2', '\x2', '\x2', '\x82', '\x83', '\a', 
		'~', '\x2', '\x2', '\x83', '\x84', '\a', '~', '\x2', '\x2', '\x84', '\x10', 
		'\x3', '\x2', '\x2', '\x2', '\x85', '\x86', '\a', '*', '\x2', '\x2', '\x86', 
		'\x12', '\x3', '\x2', '\x2', '\x2', '\x87', '\x88', '\a', '+', '\x2', 
		'\x2', '\x88', '\x14', '\x3', '\x2', '\x2', '\x2', '\x89', '\x8A', '\a', 
		'=', '\x2', '\x2', '\x8A', '\x16', '\x3', '\x2', '\x2', '\x2', '\x8B', 
		'\x8C', '\a', '-', '\x2', '\x2', '\x8C', '\x18', '\x3', '\x2', '\x2', 
		'\x2', '\x8D', '\x8E', '\a', '-', '\x2', '\x2', '\x8E', '\x8F', '\a', 
		'-', '\x2', '\x2', '\x8F', '\x1A', '\x3', '\x2', '\x2', '\x2', '\x90', 
		'\x91', '\a', '/', '\x2', '\x2', '\x91', '\x1C', '\x3', '\x2', '\x2', 
		'\x2', '\x92', '\x93', '\a', '/', '\x2', '\x2', '\x93', '\x94', '\a', 
		'/', '\x2', '\x2', '\x94', '\x1E', '\x3', '\x2', '\x2', '\x2', '\x95', 
		'\x96', '\a', ',', '\x2', '\x2', '\x96', ' ', '\x3', '\x2', '\x2', '\x2', 
		'\x97', '\x98', '\a', '\x31', '\x2', '\x2', '\x98', '\"', '\x3', '\x2', 
		'\x2', '\x2', '\x99', '\x9A', '\a', '\'', '\x2', '\x2', '\x9A', '$', '\x3', 
		'\x2', '\x2', '\x2', '\x9B', '\x9C', '\a', '?', '\x2', '\x2', '\x9C', 
		'\x9D', '\a', '?', '\x2', '\x2', '\x9D', '&', '\x3', '\x2', '\x2', '\x2', 
		'\x9E', '\x9F', '\a', '#', '\x2', '\x2', '\x9F', '\xA0', '\a', '?', '\x2', 
		'\x2', '\xA0', '(', '\x3', '\x2', '\x2', '\x2', '\xA1', '\xA2', '\a', 
		'@', '\x2', '\x2', '\xA2', '\xA3', '\a', '?', '\x2', '\x2', '\xA3', '*', 
		'\x3', '\x2', '\x2', '\x2', '\xA4', '\xA5', '\a', '>', '\x2', '\x2', '\xA5', 
		'\xA6', '\a', '?', '\x2', '\x2', '\xA6', ',', '\x3', '\x2', '\x2', '\x2', 
		'\xA7', '\xA8', '\a', '?', '\x2', '\x2', '\xA8', '.', '\x3', '\x2', '\x2', 
		'\x2', '\xA9', '\xAA', '\a', '@', '\x2', '\x2', '\xAA', '\x30', '\x3', 
		'\x2', '\x2', '\x2', '\xAB', '\xAC', '\a', '>', '\x2', '\x2', '\xAC', 
		'\x32', '\x3', '\x2', '\x2', '\x2', '\xAD', '\xAE', '\a', '}', '\x2', 
		'\x2', '\xAE', '\x34', '\x3', '\x2', '\x2', '\x2', '\xAF', '\xB0', '\a', 
		'\x7F', '\x2', '\x2', '\xB0', '\x36', '\x3', '\x2', '\x2', '\x2', '\xB1', 
		'\xB2', '\a', ']', '\x2', '\x2', '\xB2', '\x38', '\x3', '\x2', '\x2', 
		'\x2', '\xB3', '\xB4', '\a', '_', '\x2', '\x2', '\xB4', ':', '\x3', '\x2', 
		'\x2', '\x2', '\xB5', '\xB6', '\a', '<', '\x2', '\x2', '\xB6', '<', '\x3', 
		'\x2', '\x2', '\x2', '\xB7', '\xB8', '\a', '\x30', '\x2', '\x2', '\xB8', 
		'>', '\x3', '\x2', '\x2', '\x2', '\xB9', '\xBA', '\a', '.', '\x2', '\x2', 
		'\xBA', '@', '\x3', '\x2', '\x2', '\x2', '\xBB', '\xBC', '\a', '\x41', 
		'\x2', '\x2', '\xBC', '\x42', '\x3', '\x2', '\x2', '\x2', '\xBD', '\xBE', 
		'\a', '\x65', '\x2', '\x2', '\xBE', '\xBF', '\a', 'n', '\x2', '\x2', '\xBF', 
		'\xC0', '\a', '\x63', '\x2', '\x2', '\xC0', '\xC1', '\a', 'u', '\x2', 
		'\x2', '\xC1', '\xC2', '\a', 'u', '\x2', '\x2', '\xC2', '\x44', '\x3', 
		'\x2', '\x2', '\x2', '\xC3', '\xC4', '\a', '\x65', '\x2', '\x2', '\xC4', 
		'\xC5', '\a', 'q', '\x2', '\x2', '\xC5', '\xC6', '\a', 'p', '\x2', '\x2', 
		'\xC6', '\xC7', '\a', 'u', '\x2', '\x2', '\xC7', '\xC8', '\a', 'v', '\x2', 
		'\x2', '\xC8', '\x46', '\x3', '\x2', '\x2', '\x2', '\xC9', '\xCA', '\a', 
		'g', '\x2', '\x2', '\xCA', '\xCB', '\a', 'n', '\x2', '\x2', '\xCB', '\xCC', 
		'\a', 'u', '\x2', '\x2', '\xCC', '\xCD', '\a', 'g', '\x2', '\x2', '\xCD', 
		'H', '\x3', '\x2', '\x2', '\x2', '\xCE', '\xCF', '\a', 'k', '\x2', '\x2', 
		'\xCF', '\xD0', '\a', 'h', '\x2', '\x2', '\xD0', 'J', '\x3', '\x2', '\x2', 
		'\x2', '\xD1', '\xD2', '\a', 'p', '\x2', '\x2', '\xD2', '\xD3', '\a', 
		'g', '\x2', '\x2', '\xD3', '\xD4', '\a', 'y', '\x2', '\x2', '\xD4', 'L', 
		'\x3', '\x2', '\x2', '\x2', '\xD5', '\xD6', '\a', 't', '\x2', '\x2', '\xD6', 
		'\xD7', '\a', 'g', '\x2', '\x2', '\xD7', '\xD8', '\a', '\x63', '\x2', 
		'\x2', '\xD8', '\xD9', '\a', '\x66', '\x2', '\x2', '\xD9', 'N', '\x3', 
		'\x2', '\x2', '\x2', '\xDA', '\xDB', '\a', 't', '\x2', '\x2', '\xDB', 
		'\xDC', '\a', 'g', '\x2', '\x2', '\xDC', '\xDD', '\a', 'v', '\x2', '\x2', 
		'\xDD', '\xDE', '\a', 'w', '\x2', '\x2', '\xDE', '\xDF', '\a', 't', '\x2', 
		'\x2', '\xDF', '\xE0', '\a', 'p', '\x2', '\x2', '\xE0', 'P', '\x3', '\x2', 
		'\x2', '\x2', '\xE1', '\xE2', '\a', 'x', '\x2', '\x2', '\xE2', '\xE3', 
		'\a', 'q', '\x2', '\x2', '\xE3', '\xE4', '\a', 'k', '\x2', '\x2', '\xE4', 
		'\xE5', '\a', '\x66', '\x2', '\x2', '\xE5', 'R', '\x3', '\x2', '\x2', 
		'\x2', '\xE6', '\xE7', '\a', 'y', '\x2', '\x2', '\xE7', '\xE8', '\a', 
		'j', '\x2', '\x2', '\xE8', '\xE9', '\a', 'k', '\x2', '\x2', '\xE9', '\xEA', 
		'\a', 'n', '\x2', '\x2', '\xEA', '\xEB', '\a', 'g', '\x2', '\x2', '\xEB', 
		'T', '\x3', '\x2', '\x2', '\x2', '\xEC', '\xED', '\a', 'y', '\x2', '\x2', 
		'\xED', '\xEE', '\a', 't', '\x2', '\x2', '\xEE', '\xEF', '\a', 'k', '\x2', 
		'\x2', '\xEF', '\xF0', '\a', 'v', '\x2', '\x2', '\xF0', '\xF1', '\a', 
		'g', '\x2', '\x2', '\xF1', 'V', '\x3', '\x2', '\x2', '\x2', '\xF2', '\xF3', 
		'\a', 'v', '\x2', '\x2', '\xF3', '\xF4', '\a', 't', '\x2', '\x2', '\xF4', 
		'\xF5', '\a', 'w', '\x2', '\x2', '\xF5', '\xF6', '\a', 'g', '\x2', '\x2', 
		'\xF6', 'X', '\x3', '\x2', '\x2', '\x2', '\xF7', '\xF8', '\a', 'h', '\x2', 
		'\x2', '\xF8', '\xF9', '\a', '\x63', '\x2', '\x2', '\xF9', '\xFA', '\a', 
		'n', '\x2', '\x2', '\xFA', '\xFB', '\a', 'u', '\x2', '\x2', '\xFB', '\xFC', 
		'\a', 'g', '\x2', '\x2', '\xFC', 'Z', '\x3', '\x2', '\x2', '\x2', '\xFD', 
		'\xFE', '\a', 'h', '\x2', '\x2', '\xFE', '\xFF', '\a', 'q', '\x2', '\x2', 
		'\xFF', '\x100', '\a', 't', '\x2', '\x2', '\x100', '\\', '\x3', '\x2', 
		'\x2', '\x2', '\x101', '\x102', '\a', 'u', '\x2', '\x2', '\x102', '\x103', 
		'\a', 'y', '\x2', '\x2', '\x103', '\x104', '\a', 'k', '\x2', '\x2', '\x104', 
		'\x105', '\a', 'v', '\x2', '\x2', '\x105', '\x106', '\a', '\x65', '\x2', 
		'\x2', '\x106', '\x107', '\a', 'j', '\x2', '\x2', '\x107', '^', '\x3', 
		'\x2', '\x2', '\x2', '\x108', '\x109', '\a', '\x65', '\x2', '\x2', '\x109', 
		'\x10A', '\a', '\x63', '\x2', '\x2', '\x10A', '\x10B', '\a', 'u', '\x2', 
		'\x2', '\x10B', '\x10C', '\a', 'g', '\x2', '\x2', '\x10C', '`', '\x3', 
		'\x2', '\x2', '\x2', '\x10D', '\x10E', '\a', '\x64', '\x2', '\x2', '\x10E', 
		'\x10F', '\a', 't', '\x2', '\x2', '\x10F', '\x110', '\a', 'g', '\x2', 
		'\x2', '\x110', '\x111', '\a', '\x63', '\x2', '\x2', '\x111', '\x112', 
		'\a', 'm', '\x2', '\x2', '\x112', '\x62', '\x3', '\x2', '\x2', '\x2', 
		'\x113', '\x114', '\a', '\x66', '\x2', '\x2', '\x114', '\x115', '\a', 
		'g', '\x2', '\x2', '\x115', '\x116', '\a', 'h', '\x2', '\x2', '\x116', 
		'\x117', '\a', '\x63', '\x2', '\x2', '\x117', '\x118', '\a', 'w', '\x2', 
		'\x2', '\x118', '\x119', '\a', 'n', '\x2', '\x2', '\x119', '\x11A', '\a', 
		'v', '\x2', '\x2', '\x11A', '\x64', '\x3', '\x2', '\x2', '\x2', '\x11B', 
		'\x11E', '\x5', 'q', '\x39', '\x2', '\x11C', '\x11E', '\t', '\x2', '\x2', 
		'\x2', '\x11D', '\x11B', '\x3', '\x2', '\x2', '\x2', '\x11D', '\x11C', 
		'\x3', '\x2', '\x2', '\x2', '\x11E', '\x124', '\x3', '\x2', '\x2', '\x2', 
		'\x11F', '\x123', '\x5', 'q', '\x39', '\x2', '\x120', '\x123', '\x5', 
		'g', '\x34', '\x2', '\x121', '\x123', '\t', '\x2', '\x2', '\x2', '\x122', 
		'\x11F', '\x3', '\x2', '\x2', '\x2', '\x122', '\x120', '\x3', '\x2', '\x2', 
		'\x2', '\x122', '\x121', '\x3', '\x2', '\x2', '\x2', '\x123', '\x126', 
		'\x3', '\x2', '\x2', '\x2', '\x124', '\x122', '\x3', '\x2', '\x2', '\x2', 
		'\x124', '\x125', '\x3', '\x2', '\x2', '\x2', '\x125', '\x66', '\x3', 
		'\x2', '\x2', '\x2', '\x126', '\x124', '\x3', '\x2', '\x2', '\x2', '\x127', 
		'\x12B', '\t', '\x3', '\x2', '\x2', '\x128', '\x12A', '\t', '\x4', '\x2', 
		'\x2', '\x129', '\x128', '\x3', '\x2', '\x2', '\x2', '\x12A', '\x12D', 
		'\x3', '\x2', '\x2', '\x2', '\x12B', '\x129', '\x3', '\x2', '\x2', '\x2', 
		'\x12B', '\x12C', '\x3', '\x2', '\x2', '\x2', '\x12C', '\x130', '\x3', 
		'\x2', '\x2', '\x2', '\x12D', '\x12B', '\x3', '\x2', '\x2', '\x2', '\x12E', 
		'\x130', '\a', '\x32', '\x2', '\x2', '\x12F', '\x127', '\x3', '\x2', '\x2', 
		'\x2', '\x12F', '\x12E', '\x3', '\x2', '\x2', '\x2', '\x130', '\x138', 
		'\x3', '\x2', '\x2', '\x2', '\x131', '\x135', '\x5', '=', '\x1F', '\x2', 
		'\x132', '\x134', '\t', '\x4', '\x2', '\x2', '\x133', '\x132', '\x3', 
		'\x2', '\x2', '\x2', '\x134', '\x137', '\x3', '\x2', '\x2', '\x2', '\x135', 
		'\x133', '\x3', '\x2', '\x2', '\x2', '\x135', '\x136', '\x3', '\x2', '\x2', 
		'\x2', '\x136', '\x139', '\x3', '\x2', '\x2', '\x2', '\x137', '\x135', 
		'\x3', '\x2', '\x2', '\x2', '\x138', '\x131', '\x3', '\x2', '\x2', '\x2', 
		'\x138', '\x139', '\x3', '\x2', '\x2', '\x2', '\x139', 'h', '\x3', '\x2', 
		'\x2', '\x2', '\x13A', '\x140', '\x5', '\a', '\x4', '\x2', '\x13B', '\x141', 
		'\x5', 's', ':', '\x2', '\x13C', '\x13D', '\a', '^', '\x2', '\x2', '\x13D', 
		'\x141', '\a', 'p', '\x2', '\x2', '\x13E', '\x13F', '\a', '^', '\x2', 
		'\x2', '\x13F', '\x141', '\a', 't', '\x2', '\x2', '\x140', '\x13B', '\x3', 
		'\x2', '\x2', '\x2', '\x140', '\x13C', '\x3', '\x2', '\x2', '\x2', '\x140', 
		'\x13E', '\x3', '\x2', '\x2', '\x2', '\x141', '\x142', '\x3', '\x2', '\x2', 
		'\x2', '\x142', '\x143', '\x5', '\a', '\x4', '\x2', '\x143', 'j', '\x3', 
		'\x2', '\x2', '\x2', '\x144', '\x14A', '\x5', '\x5', '\x3', '\x2', '\x145', 
		'\x149', '\n', '\x5', '\x2', '\x2', '\x146', '\x147', '\a', '^', '\x2', 
		'\x2', '\x147', '\x149', '\a', '$', '\x2', '\x2', '\x148', '\x145', '\x3', 
		'\x2', '\x2', '\x2', '\x148', '\x146', '\x3', '\x2', '\x2', '\x2', '\x149', 
		'\x14C', '\x3', '\x2', '\x2', '\x2', '\x14A', '\x148', '\x3', '\x2', '\x2', 
		'\x2', '\x14A', '\x14B', '\x3', '\x2', '\x2', '\x2', '\x14B', '\x14D', 
		'\x3', '\x2', '\x2', '\x2', '\x14C', '\x14A', '\x3', '\x2', '\x2', '\x2', 
		'\x14D', '\x14E', '\x5', '\x5', '\x3', '\x2', '\x14E', 'l', '\x3', '\x2', 
		'\x2', '\x2', '\x14F', '\x150', '\a', '\x31', '\x2', '\x2', '\x150', '\x151', 
		'\a', '\x31', '\x2', '\x2', '\x151', '\x155', '\x3', '\x2', '\x2', '\x2', 
		'\x152', '\x154', '\n', '\x6', '\x2', '\x2', '\x153', '\x152', '\x3', 
		'\x2', '\x2', '\x2', '\x154', '\x157', '\x3', '\x2', '\x2', '\x2', '\x155', 
		'\x153', '\x3', '\x2', '\x2', '\x2', '\x155', '\x156', '\x3', '\x2', '\x2', 
		'\x2', '\x156', '\x158', '\x3', '\x2', '\x2', '\x2', '\x157', '\x155', 
		'\x3', '\x2', '\x2', '\x2', '\x158', '\x165', '\t', '\x6', '\x2', '\x2', 
		'\x159', '\x15A', '\a', '\x31', '\x2', '\x2', '\x15A', '\x15B', '\a', 
		',', '\x2', '\x2', '\x15B', '\x15F', '\x3', '\x2', '\x2', '\x2', '\x15C', 
		'\x15E', '\v', '\x2', '\x2', '\x2', '\x15D', '\x15C', '\x3', '\x2', '\x2', 
		'\x2', '\x15E', '\x161', '\x3', '\x2', '\x2', '\x2', '\x15F', '\x160', 
		'\x3', '\x2', '\x2', '\x2', '\x15F', '\x15D', '\x3', '\x2', '\x2', '\x2', 
		'\x160', '\x162', '\x3', '\x2', '\x2', '\x2', '\x161', '\x15F', '\x3', 
		'\x2', '\x2', '\x2', '\x162', '\x163', '\a', ',', '\x2', '\x2', '\x163', 
		'\x165', '\a', '\x31', '\x2', '\x2', '\x164', '\x14F', '\x3', '\x2', '\x2', 
		'\x2', '\x164', '\x159', '\x3', '\x2', '\x2', '\x2', '\x165', '\x166', 
		'\x3', '\x2', '\x2', '\x2', '\x166', '\x164', '\x3', '\x2', '\x2', '\x2', 
		'\x166', '\x167', '\x3', '\x2', '\x2', '\x2', '\x167', '\x168', '\x3', 
		'\x2', '\x2', '\x2', '\x168', '\x169', '\b', '\x37', '\x2', '\x2', '\x169', 
		'n', '\x3', '\x2', '\x2', '\x2', '\x16A', '\x16C', '\t', '\a', '\x2', 
		'\x2', '\x16B', '\x16A', '\x3', '\x2', '\x2', '\x2', '\x16C', '\x16D', 
		'\x3', '\x2', '\x2', '\x2', '\x16D', '\x16B', '\x3', '\x2', '\x2', '\x2', 
		'\x16D', '\x16E', '\x3', '\x2', '\x2', '\x2', '\x16E', '\x16F', '\x3', 
		'\x2', '\x2', '\x2', '\x16F', '\x170', '\b', '\x38', '\x2', '\x2', '\x170', 
		'p', '\x3', '\x2', '\x2', '\x2', '\x171', '\x172', '\t', '\b', '\x2', 
		'\x2', '\x172', 'r', '\x3', '\x2', '\x2', '\x2', '\x173', '\x176', '\x5', 
		'q', '\x39', '\x2', '\x174', '\x176', '\x4', '#', '\x42', '\x2', '\x175', 
		'\x173', '\x3', '\x2', '\x2', '\x2', '\x175', '\x174', '\x3', '\x2', '\x2', 
		'\x2', '\x176', 't', '\x3', '\x2', '\x2', '\x2', '\x13', '\x2', '\x11D', 
		'\x122', '\x124', '\x12B', '\x12F', '\x135', '\x138', '\x140', '\x148', 
		'\x14A', '\x155', '\x15F', '\x164', '\x166', '\x16D', '\x175', '\x3', 
		'\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
