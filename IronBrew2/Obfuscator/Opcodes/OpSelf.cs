using IronBrew2.Bytecode_Library.Bytecode;
using IronBrew2.Bytecode_Library.IR;

namespace IronBrew2.Obfuscator.Opcodes
{
	public class OpSelf : VOpcode
	{
		public override bool IsInstruction(Instruction instruction) =>
			instruction.OpCode == Opcode.Self && instruction.C <= 255;

		public override string GetObfuscated(ObfuscationContext context) =>
			"local A=Inst[D9_OP_A];local B=Stk[Inst[D9_OP_B]];Stk[A+1]=B;Stk[A]=B[Stk[Inst[D9_OP_C]]];";
	}
	
	public class OpSelfC : VOpcode
	{
		public override bool IsInstruction(Instruction instruction)=>
			instruction.OpCode == Opcode.Self && instruction.C > 255;
		
		public override string GetObfuscated(ObfuscationContext context) =>
			"local A=Inst[D9_OP_A];local B=Stk[Inst[D9_OP_B]];Stk[A+1]=B;Stk[A]=B[Inst[D9_OP_C]];";

		public override void Mutate(Instruction instruction)
		{
			instruction.C -= 255;
			instruction.ConstantMask |= InstructionConstantMask.RC;
		}
	}
}