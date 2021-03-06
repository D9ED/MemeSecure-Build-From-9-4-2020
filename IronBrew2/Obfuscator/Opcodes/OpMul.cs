using IronBrew2.Bytecode_Library.Bytecode;
using IronBrew2.Bytecode_Library.IR;

namespace IronBrew2.Obfuscator.Opcodes
{
	public class OpMul : VOpcode
	{
		public override bool IsInstruction(Instruction instruction) =>
			instruction.OpCode == Opcode.Mul && instruction.B <= 255 && instruction.C <= 255;

		public override string GetObfuscated(ObfuscationContext context) =>
			"Stk[Inst[D9_OP_A]]=Stk[Inst[D9_OP_B]]*Stk[Inst[D9_OP_C]];";
	}
	
	public class OpMulB : VOpcode
	{
		public override bool IsInstruction(Instruction instruction) =>
			instruction.OpCode == Opcode.Mul && instruction.B > 255 && instruction.C <= 255;

		public override string GetObfuscated(ObfuscationContext context) =>
			"Stk[Inst[D9_OP_A]]=Inst[D9_OP_B]*Stk[Inst[D9_OP_C]];";

		public override void Mutate(Instruction instruction)
		{
			instruction.B -= 255;
			instruction.ConstantMask |= InstructionConstantMask.RB;
		}
	}
	
	public class OpMulC : VOpcode
	{
		public override bool IsInstruction(Instruction instruction) =>
			instruction.OpCode == Opcode.Mul && instruction.B <= 255 && instruction.C > 255;

		public override string GetObfuscated(ObfuscationContext context) =>
			"Stk[Inst[D9_OP_A]] = Stk[Inst[D9_OP_B]] * Inst[D9_OP_C];";

		public override void Mutate(Instruction instruction)
		{
			instruction.C -= 255;
			instruction.ConstantMask |= InstructionConstantMask.RC;
		}
	}
	
	public class OpMulBC : VOpcode
	{
		public override bool IsInstruction(Instruction instruction) =>
			instruction.OpCode == Opcode.Mul && instruction.B > 255 && instruction.C > 255;

		public override string GetObfuscated(ObfuscationContext context) =>
			"Stk[Inst[D9_OP_A]]=Inst[D9_OP_B] * Inst[D9_OP_C];";

		public override void Mutate(Instruction instruction)
		{
			instruction.B -= 255;
			instruction.C -= 255;
			instruction.ConstantMask |= InstructionConstantMask.RB | InstructionConstantMask.RC;
		}
	}
}