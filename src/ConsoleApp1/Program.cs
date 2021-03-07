using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<string, string> types = new Dictionary<string, string>()
            {
                { "Strength", "int" },
                { "Intelligence","int" },
                { "Vitality","int" },
            };

            Generate("Stat", types);
        }

        static void Generate(string name, Dictionary<string, string> dictionary)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"public partial class {name}");
            sb.AppendLine("{");
            foreach (var member in dictionary)
            {
                sb.AppendLine($"\tpublic {member.Value} {member.Key} {{ get; set; }}");
            }

            foreach (var op in new[] { "+", "-" })
            {
                sb.Append($"\tpublic static {name} operator {op}({name} a, {name} b) =>\n");
                sb.Append($"\t\tnew {name}() {{\n");
                foreach (var member in dictionary)
                {
                    var fieldName = member.Key;
                    sb.Append("\t\t\t");
                    sb.Append($"{fieldName} = a.{fieldName} {op} b.{fieldName},");
                    sb.Append('\n');
                }
                sb.Append("\t\t};\n\n");
            }

            sb.AppendLine("}");
        }

    }
}
