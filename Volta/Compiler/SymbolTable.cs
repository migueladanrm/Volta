using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Volta.Compiler
{
    public class SymbolTable
    {
        private int scope = 0;
        private List<Symbol> index;

        public SymbolTable() {
            index = new List<Symbol>();
        }

        public void Insert(string name, object attrs) {
            throw new NotImplementedException();
        }

        public Symbol Find(string name)
            => index.Where(s => s.Name.Equals(name)).OrderByDescending(s => s.Level).FirstOrDefault();


        public int LevelDown()
            => scope++;

        public int LevelUp() {
            if (0 < scope) {
                index.RemoveAll(s => s.Level == scope);
                scope--;
            }

            return scope;
        }

    }
}