using System.Reflection;
using System.Reflection.Emit;

namespace Junior.BuildRunBasics.BigPicture;

/// <summary>
/// Minimal IL "disassembler": bir metodun IL baytlarını (MethodBody.GetILAsByteArray) opcode adlarına çevirir.
/// ILSpy / ildasm / sharplab.io'nun yaptığının küçük bir özeti — IL'in YIĞIN (stack) tabanlı bir makine dili olduğunu görmek için.
/// </summary>
public static class IlDisassembler
{
    private static readonly Dictionary<short, OpCode> Ops = typeof(OpCodes)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Select(f => (OpCode)f.GetValue(null)!)
        .ToDictionary(o => o.Value);

    public static IReadOnlyList<string> Disassemble(MethodBase method)
    {
        var il = method.GetMethodBody()!.GetILAsByteArray()!;
        var module = method.Module;
        var typeArgs = method.DeclaringType?.IsGenericType == true ? method.DeclaringType.GetGenericArguments() : null;
        var lines = new List<string>();

        for (var i = 0; i < il.Length;)
        {
            var offset = i;
            var code = (short)il[i++];
            if (code == 0xFE) code = unchecked((short)(0xFE00 | il[i++]));       // iki baytlık opcode
            var op = Ops[code];

            string operand = op.OperandType switch
            {
                OperandType.InlineNone => "",
                OperandType.ShortInlineBrTarget => $"IL_{i + 1 + (sbyte)il[i++]:X4}",
                OperandType.InlineBrTarget => $"IL_{i + 4 + ReadInt(il, ref i):X4}",
                OperandType.ShortInlineI => ((sbyte)il[i++]).ToString(),
                OperandType.ShortInlineVar => il[i++].ToString(),
                OperandType.InlineVar => $"{BitConverter.ToInt16(il, i)}{Skip(ref i, 2)}",
                OperandType.InlineI => ReadInt(il, ref i).ToString(),
                OperandType.InlineI8 => $"{BitConverter.ToInt64(il, i)}{Skip(ref i, 8)}",
                OperandType.ShortInlineR => $"{BitConverter.ToSingle(il, i)}{Skip(ref i, 4)}",
                OperandType.InlineR => $"{BitConverter.ToDouble(il, i)}{Skip(ref i, 8)}",
                OperandType.InlineString => $"\"{module.ResolveString(ReadInt(il, ref i))}\"",
                OperandType.InlineMethod => DescribeMethod(module, ReadInt(il, ref i), typeArgs),
                OperandType.InlineField => DescribeField(module, ReadInt(il, ref i), typeArgs),
                OperandType.InlineType => module.ResolveType(ReadInt(il, ref i), typeArgs, null).Name,
                OperandType.InlineTok or OperandType.InlineSig => $"0x{ReadInt(il, ref i):X8}",
                OperandType.InlineSwitch => SkipSwitch(il, ref i),
                _ => "?",
            };

            lines.Add($"IL_{offset:X4}: {op.Name,-10} {operand}".TrimEnd());
        }
        return lines;
    }

    private static int ReadInt(byte[] il, ref int i)
    {
        var value = BitConverter.ToInt32(il, i);
        i += 4;
        return value;
    }

    private static string Skip(ref int i, int count)
    {
        i += count;
        return "";
    }

    private static string SkipSwitch(byte[] il, ref int i)
    {
        var count = ReadInt(il, ref i);
        i += count * 4;
        return $"({count} hedef)";
    }

    private static string DescribeMethod(Module module, int token, Type[]? typeArgs)
    {
        var m = module.ResolveMethod(token, typeArgs, null)!;
        return $"{m.DeclaringType?.Name}::{m.Name}";
    }

    private static string DescribeField(Module module, int token, Type[]? typeArgs)
    {
        var f = module.ResolveField(token, typeArgs, null)!;
        return $"{f.DeclaringType?.Name}::{f.Name}";
    }
}
