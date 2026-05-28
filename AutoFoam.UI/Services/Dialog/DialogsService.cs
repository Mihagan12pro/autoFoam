using AutoFoam.UI.ViewModels;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using Avalonia.Utilities;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Dialog
{
    internal class DialogsService : IDialogsService
    {
        private readonly ViewModelBase _viewModel;
        private readonly TopLevel _topLevel;

        private readonly string _logoPath = new FileInfo(
                Path.Combine("..", "..", "..", "Assets", "avalonia-logo.ico")
            ).FullName;

        public async Task SaveProject()
        {
            var storageProvider = _topLevel.StorageProvider;
            var storageFolder = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions() {  AllowMultiple = false, Title = "Выбор папки для сохранения" });

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

        public async Task ShowErrors(IEnumerable<string> errors)
        {
            var parent = ViewLocator.ResolveViewFromViewModel(_viewModel);

            string errorString = string.Empty;
            foreach(var error in errors)
            {
                errorString += '\n' + error + '\n';
            }

            var box = MessageBoxManager
                .GetMessageBoxCustom(new MessageBoxCustomParams()
                {
                    Topmost = true,

                    SizeToContent = SizeToContent.WidthAndHeight,

                    WindowIcon = new WindowIcon(new Bitmap((_logoPath))),

                    ContentTitle = "Список ошибок", 

                    ContentMessage = errorString,
                });

            await box.ShowWindowAsync();
        }

        public DialogsService(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
            _topLevel = TopLevel.GetTopLevel(ViewLocator.ResolveViewFromViewModel(_viewModel));
        }
    }
}
