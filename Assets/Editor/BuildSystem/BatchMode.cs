using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSystem;

namespace Assets.Editor.BuildSystem
{
    public static class BatchMode
    {
        public static void Build()
        {
            var args = Environment.GetCommandLineArgs().Concat(new string[] { "" }).ToArray(); // オプションのみと　そもそも入っていないの識別
            var options = new string[] { "-parameter",};

            var name = options.ToDictionary(p => p.Substring(1), p => args.SkipWhile(a => a != p).Skip(1).FirstOrDefault())["parameter"];

            if (name == null)
            {
                BuildProcess.Build();
            }
            else
            {
                BuildProcess.Build(name);
            }

        }
    }
}
