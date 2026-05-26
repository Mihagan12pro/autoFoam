using AutoFoam.UI.ViewModels;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Dialog
{
    internal class DialogsService : IDialogsService
    {
        private readonly ViewModelBase _viewModel;

        public async Task SaveProject()
        {
            var topLevel = TopLevel.GetTopLevel(ViewLocator.ResolveViewFromViewModel(_viewModel));

            var storageProvider = topLevel.StorageProvider;
            var storageFolder = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions() {  AllowMultiple = false });

            var folder = storageFolder.FirstOrDefault();

            if (folder != null)
            {
                string sourcePath = Path.Combine(
                    AppContext.BaseDirectory,
                    "..",
                    "..",
                    "..",
                    "..",
                    "Tasks",
                    "FlatChannel"
                );

                sourcePath = Path.GetFullPath(sourcePath);

                string targetPath = folder.Path.LocalPath;

                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }

                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            }
        }

        public DialogsService(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
