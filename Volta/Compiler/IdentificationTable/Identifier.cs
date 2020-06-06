using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;

namespace Volta.Compiler.IdentificationTable
{
    public abstract class Identifier
    {
        public string id;
        public IToken token;
        public int level;
        public string type;
        public ParserRuleContext decl;
    }

    public class ClassIdentifier: Identifier
    {
        List<VoltaParser.VarDeclASTContext> varDecls;

        VoltaParser.ClassDeclASTContext classDecl;

        public ClassIdentifier(string id, IToken token, int level, string type,  List<VoltaParser.VarDeclASTContext> varDecls, VoltaParser.ClassDeclASTContext classDecl)
        {
            this.id = id;
            this.token = token;
            this.level = level;
            this.type = type;
            this.varDecls = varDecls;
            this.decl = classDecl;
        }
    }

    public class MethodIdentifier : Identifier
    {
        List<VoltaParser.FormParsASTContext> formPars;

        VoltaParser.MethodDeclASTContext methodDecl;
        public MethodIdentifier(string id, IToken token, int level, string type, List<VoltaParser.FormParsASTContext> formPars, VoltaParser.MethodDeclASTContext methodDecl)
        {
            this.id = id;
            this.token = token;
            this.level = level;
            this.type = type;
            this.formPars = formPars;
            this.decl = methodDecl;
        }
    }

    public class ConstIdentifier: Identifier
    {
        VoltaParser.ConstDeclASTContext constDecl;
        public ConstIdentifier(string id, IToken token, int level, string type, VoltaParser.ConstDeclASTContext constDecl)
        {
            this.id = id;
            this.token = token;
            this.level = level;
            this.type = type;
            this.decl = constDecl;
        }

    }

    public class VarIdentifier : Identifier
    {
        VoltaParser.VarDeclASTContext varDecl;
        public VarIdentifier(string id, IToken token, int level, string type, VoltaParser.VarDeclASTContext varDecl)
        {
            this.id = id;
            this.token = token;
            this.level = level;
            this.type = type;
            this.decl = varDecl;
        }

    }
}
