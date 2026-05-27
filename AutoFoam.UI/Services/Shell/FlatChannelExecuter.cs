using AutoFoam.UI.Models.FlatChannel;
using System;
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

            return File.Exists(cleanPath);
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
