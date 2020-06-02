using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Volta.Compiler.IdentificationTable
{
    public class IdentificationTable
    {
        private int level = 0;
        private List<Identifier> identifiers;

        

        public IdentificationTable() {
            identifiers = new List<Identifier>();
        }

        public void Insert(Identifier identifier) {
            identifiers.Add(identifier);
        }

        public Identifier Find(string id, Boolean inThisLevel) {
            if (inThisLevel)
            {
                return identifiers.FindLast(delegate (Identifier identifier) { return identifier.id == id && identifier.level == level; });
            }
            return identifiers.FindLast(delegate (Identifier identifier) { return identifier.id == id; });
        }

        public void OpenLevel()
        {
            level++;
        }

        public void CloseLevel()
        {
            identifiers = identifiers.FindAll(delegate (Identifier identifier) { return identifier.level != level; });
            level--;
        }

        public int getLevel()
        {
            return level;
        }
    }
}
