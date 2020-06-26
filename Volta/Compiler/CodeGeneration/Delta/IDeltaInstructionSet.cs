namespace Volta.Compiler.CodeGeneration.Delta
{
    /// <summary>
    /// Conjunto de instrucciones del intérprete Delta.
    /// </summary>
    interface IDeltaInstructionSet
    {
        void BINARY_ADD();
        void BINARY_AND();
        void BINARY_DIVIDE();
        void BINARY_MODULUS();
        void BINARY_MULTIPLY();
        void BINARY_OR();
        void BINARY_SUBSTRACT();
        void CALL_FUNCTION(int numParams);
        void COMPARE_OP(string op);
        void DEF(string name);
        void END();
        void JUMP_ABSOLUTE(int target);
        void JUMP_IF_FALSE(int target);
        void JUMP_IF_TRUE(int target);
        void LOAD_CONST(dynamic value);
        void LOAD_FAST(string varname);
        void LOAD_GLOBAL(string name);
        void PUSH_GLOBAL_C(string name);
        void PUSH_GLOBAL_I(string name);
        void PUSH_LOCAL_C(string name);
        void PUSH_LOCAL_I(string name);
        void RETURN_VALUE();
        void STORE_FAST(string varname);
        void STORE_GLOBAL(string varname);

    }
}