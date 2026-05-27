using AutoFoam.UI.Models.FlatChannel;
using System;
using System.Diagnostics;
using System.IO;

namespace AutoFoam.UI.Services.Shell
{
    internal class FlatChannelExecuter : IShellExecuter
    {
        private string sourcePath = Path.Combine(
                    AppContext.BaseDirectory,
                    "..",
                    "..",
                    "..",
                    "..",
                    "Tasks",
                    "FlatChannel"
                );

        public bool ExecuteChangeParams(FlatChannel flatChannel)
        {
            throw new NotImplementedException();
        }

        public bool ExecuteClean()
        {
            string cleanPath = Path.Combine(sourcePath, "Clean.sh");

            Process.Start(cleanPath);

            return true;
        }

        public bool ExecuteParaView()
        {
            throw new NotImplementedException();
        }

        public bool ExecuteRun(FlatChannel flatChannel)
        {
            throw new NotImplementedException();
        }
    }
}
