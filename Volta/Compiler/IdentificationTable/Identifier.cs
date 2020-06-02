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
    }

    public class ClassIdentifier: Identifier
    {
        List<VoltaParser.VarDeclASTContext> varDecls;

        public ClassIdentifier(string id, IToken token, int level, string type,  List<VoltaParser.VarDeclASTContext> varDecls)
        {
            this.id = id;
            this.token = token;
            this.level = level;
            this.type = type;
            this.varDecls = varDecls;
        }
    }

    public class MethodIdentifier : Identifier
    {
        List<VoltaParser.FormParsASTContext> formPars;

        public MethodIdentifier(string id, IToken token, int level, string type, List<VoltaParser.FormParsASTContext> formPars)
        {
            this.id = id;
            this.token = token;
            this.level = level;
            this.type = type;
            this.formPars = formPars;
        }
    }

    public class VarCostIdentifier: Identifier
    {

        public VarCostIdentifier(string id, IToken token, int level, string type)
        {
            this.id = id;
            this.token = token;
            this.level = level;
            this.type = type;
        }

    }
}
