using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Volta.Compiler.CodeAnalysis
{
    public class IdentificationTable
    {
        private int level = -1;
        private List<Identifier> identifiers;

        public IdentificationTable() {
            identifiers = new List<Identifier>();
        }

        public void Insert(Identifier identifier) {
            identifiers.Add(identifier);
        }

        public Identifier Find(string id, bool inThisLevel) {
            if (inThisLevel) {
                return identifiers.FindLast(delegate (Identifier identifier) {
                    return identifier.Id == id && identifier.Level == level;
                });
            }
            return identifiers.FindLast(delegate (Identifier identifier) { return identifier.Id == id; });
        }

        public ClassIdentifier FindClass(string id)
        {
            return identifiers.Find(identifier => identifier.Id == id && identifier is ClassIdentifier) as ClassIdentifier;
        }

        public void OpenLevel() {
            level++;
        }

        public void CloseLevel() {
            identifiers = identifiers.FindAll(delegate (Identifier identifier) { return identifier.Level != level; });
            level--;
        }

        public int getLevel() {
            return level;
        }
    }
}