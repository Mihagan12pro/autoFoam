using AutoFoam.UI.Models.FlatChannel;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

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

        public async Task<int> Execute(string scriptPath)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"\"{scriptPath}\"", 
                UseShellExecute = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processInfo))
            {
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                return process.ExitCode;
            }
        }

        public async Task<int> ExecuteChangeParams(FlatChannel flatChannel)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ExecuteClean()
        {
            string cleanPath = Path.Combine(sourcePath, "Clean.sh");

            var result = await Execute(cleanPath);

            return result;
        }

        public async Task<int> ExecuteParaView()
        {
            throw new NotImplementedException();
        }

        public async Task<int> ExecuteRun(FlatChannel flatChannel)
        {
            throw new NotImplementedException();
        }
    }
}
