using System;
using System.Text;
using Npgg.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        public class Stat
        {
            public int Attack { get; set; }
            public int MaxHP { get; set; }

            public static Stat operator +(Stat a, Stat b)
            {
                return null;
            }
        }

        static void Main(string[] args)
        {
            Generate<Stat>();
        }

        static void Generate<T>()
        {
            var stats = MemberAccessor.GetAccessors<T>();
            var name = typeof(T).Name;
            var sb = new StringBuilder();

            foreach (var op in new[] { "+", "-"})
            {
                sb.Append($"public static {name} operator {op}({name} a, {name} b) =>\n");
                sb.Append($"new {name}() {{\n");
                foreach (var member in stats)
                {
                    var fieldName = member.Key;
                    sb.Append('\t');
                    sb.Append($"{fieldName} = a.{fieldName} {op} b.{fieldName},");
                    sb.Append('\n');
                }
                sb.Append("};\n\n");
            }
        }

    }
}
