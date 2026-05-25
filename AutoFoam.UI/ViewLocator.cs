using System;
using AutoFoam.UI.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace AutoFoam.UI
{
    public class ViewLocator : IDataTemplate
    {
        public static Window ResolveViewFromViewModel<T>(T vm) where T : ViewModelBase
        {
            var name = vm.GetType().AssemblyQualifiedName!
                .Replace("ViewModels", "Views")
                .Replace("ViewModel", "");
            var type = Type.GetType(name);

            return type != null ? (Window)Activator.CreateInstance(type)! : null;
        }


        public Control? Build(object? param)
        {
            if (param is null)
                return null;

            var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
