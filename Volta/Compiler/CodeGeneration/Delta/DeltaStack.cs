using System.Collections;


namespace Volta.Compiler.CodeGeneration.Delta
{
    public class DeltaStack
    {
        private Stack stack;

        public DeltaStack() {
            stack = new Stack();
        }

        //public void Push(dynamic value)
        //    => stack.Push(value);

        public dynamic Pop()
            => stack.Pop();

    }
}
