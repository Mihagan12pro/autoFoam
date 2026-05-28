using AutoFoam.UI.Models.FlatChannel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Shell
{
    internal class FlatChannelExecuteService : IShellExecuter<FlatChannelMesh>
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
            Dictionary<string, string> namesProperties = new Dictionary<string, string>();
            foreach(var prop in mesh.GetType().GetProperties())
            {
                var displayName = ((DisplayNameAttribute)prop.GetCustomAttribute(typeof(DisplayNameAttribute)));

                namesProperties.Add(displayName.DisplayName, prop.Name);
            }

            string paramsPath = Path.Combine(sourcePath, "params.txt");

            string[] lines = await File.ReadAllLinesAsync(paramsPath);

            for(int i = 0; i < lines.Length; i+=2)
            {
                string current = lines[i].TrimEnd();

                if (namesProperties.TryGetValue(current, out string prop))
                {
                    string value = Convert.ToString(typeof(FlatChannelMesh).GetProperty(prop).GetValue(mesh));
                    lines[i + 1] = value;
                }
            }

            await File.WriteAllLinesAsync(paramsPath, lines);

            string runPath = Path.Combine(sourcePath, "Run.sh");

            var result = await Execute(runPath);

            return result;
        }
    }
}
