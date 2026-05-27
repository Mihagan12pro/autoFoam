using AutoFoam.UI.Models.FlatChannel;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Shell
{
    internal class FlatChannelExecuter : IShellExecuter<FlatChannelMesh>
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

        public async Task<int> ExecuteChangeParams(FlatChannelMesh mesh)
        {
            string paramsPath = Path.Combine(sourcePath, "params.txt");

            var a = File.Exists(paramsPath);

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
            string paraViewPath = Path.Combine(sourcePath, "ParaView.sh");

            var result = await Execute(paraViewPath);

            return result;
        }

        public async Task<int> ExecuteRun(FlatChannelMesh mesh)
        {
            throw new NotImplementedException();
        }
    }
}
