using Antlr4.Runtime;
using System.Collections.Generic;

namespace Volta.Compiler.CodeAnalysis
{
    public abstract class Identifier
    {
        public Identifier() {

        }

        public Identifier(string id, IToken token, int level, string type, ParserRuleContext declaration) {
            Id = id;
            Token = token;
            Level = level;
            Type = type;
            Declaration = declaration;
        }

        public string Id { get; set; }

        public IToken Token { get; set; }

        public int Level { get; set; }

        public string Type { get; set; }

        public ParserRuleContext Declaration { get; set; }
    }

    public class ClassIdentifier : Identifier
    {
        public ClassIdentifier(string id, IToken token, int level, string type, VoltaParser.ClassDeclASTContext declaration, List<VoltaParser.VarDeclASTContext> varDecls)
            : base(id, token, level, type, declaration) {
            VarDecl = varDecls;
        }

        public List<VoltaParser.VarDeclASTContext> VarDecl { get; set; }

    }

    public class MethodIdentifier : Identifier
    {
        public MethodIdentifier(string id, IToken token, int level, string type, VoltaParser.MethodDeclASTContext declaration, VoltaParser.FormParsASTContext formPars)
            : base(id, token, level, type, declaration) {
            FormPars = formPars;
        }

        public VoltaParser.FormParsASTContext FormPars { get; set; }
    }

    public class ConstIdentifier : Identifier
    {
        public ConstIdentifier(string id, IToken token, int level, string type, VoltaParser.ConstDeclASTContext declaration)
            : base(id, token, level, type, declaration) {

        }
    }

    public class VarIdentifier : Identifier
    {
        public VarIdentifier(string id, IToken token, int level, string type, ParserRuleContext declaration)
            : base(id, token, level, type, declaration) {

        }
    }
}