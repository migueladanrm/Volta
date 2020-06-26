using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Volta.Compiler.CodeGeneration.Delta
{
    public class DeltaInterpreter : IDeltaInstructionSet
    {
        public DeltaInterpreter() {
            Instructions = new List<(string Instruction, dynamic Value)>();
            GlobalStore = new DeltaStore("global");
            LocalStores = new List<DeltaStore>();
            Stack = new DeltaStack();
            CurrentParams = new List<dynamic>();

            GlobalStore.AddValue("write", -1);
            CurrentInstruction = 0;
        }

        private List<(string Instruction, dynamic Value)> Instructions { get; set; }
        private DeltaStack Stack { get; set; }
        private DeltaStore GlobalStore { get; set; }
        private List<DeltaStore> LocalStores { get; set; }
        private int CurrentInstruction { get; set; }
        private List<dynamic> CurrentParams { get; set; }


        #region Management

        public void AddInstruction((string Instruction, dynamic Value) pair)
            => Instructions.Add(pair);

        public void AddInstruction(string instruction, dynamic param)
            => AddInstruction((instruction, param));

        #endregion

        #region Instruction set

        public void BINARY_ADD() {
            throw new NotImplementedException();
        }

        public void BINARY_AND() {
            throw new NotImplementedException();
        }

        public void BINARY_DIVIDE() {
            throw new NotImplementedException();
        }

        public void BINARY_MODULUS() {
            throw new NotImplementedException();
        }

        public void BINARY_MULTIPLY() {
            throw new NotImplementedException();
        }

        public void BINARY_OR() {
            throw new NotImplementedException();
        }

        public void BINARY_SUBSTRACT() {
            throw new NotImplementedException();
        }

        public void CALL_FUNCTION(int numParams) {
            throw new NotImplementedException();
        }

        public void COMPARE_OP(string op) {
            throw new NotImplementedException();
        }

        public void DEF(string name) {
            throw new NotImplementedException();
        }

        public void END() {
            throw new NotImplementedException();
        }

        public void JUMP_ABSOLUTE(int target) {
            throw new NotImplementedException();
        }

        public void JUMP_IF_FALSE(int target) {
            throw new NotImplementedException();
        }

        public void JUMP_IF_TRUE(int target) {
            throw new NotImplementedException();
        }

        public void LOAD_CONST(dynamic value) {
            throw new NotImplementedException();
        }

        public void LOAD_FAST(string varname) {
            throw new NotImplementedException();
        }

        public void LOAD_GLOBAL(string name) {
            throw new NotImplementedException();
        }

        public void PUSH_GLOBAL_C(string name) {
            throw new NotImplementedException();
        }

        public void PUSH_GLOBAL_I(string name) {
            throw new NotImplementedException();
        }

        public void PUSH_LOCAL_C(string name) {
            throw new NotImplementedException();
        }

        public void PUSH_LOCAL_I(string name) {
            throw new NotImplementedException();
        }

        public void RETURN_VALUE() {
            throw new NotImplementedException();
        }

        public void STORE_FAST(string varname) {
            throw new NotImplementedException();
        }

        public void STORE_GLOBAL(string varname) {
            throw new NotImplementedException();
        }

        #endregion
    }
}