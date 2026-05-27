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

        public async Task<bool> Execute(string scriptPath)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"\"{scriptPath}\"", 
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processInfo))
            {
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    Console.WriteLine($"Успех:\n{output}");

                    return true;
                }
                else
                {
                    Console.WriteLine($"Ошибка (код {process.ExitCode}):\n{error}");

                    return false;
                }
            }
        }

        public async Task<bool> ExecuteChangeParams(FlatChannel flatChannel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExecuteClean()
        {
            string cleanPath = Path.Combine(sourcePath, "Clean.sh");

            Process.Start(cleanPath);

            return true;
        }

        public async Task<bool> ExecuteParaView()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExecuteRun(FlatChannel flatChannel)
        {
            throw new NotImplementedException();
        }
    }
}
