using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Dialog
{
    public interface IDialogsService
    {
        /// <summary>
        /// Saves current project to selected folder
        /// </summary>
        /// <returns></returns>
        Task SaveProject();

        /// <summary>
        /// Shows input errors
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        Task ShowErrors(IEnumerable<string> errors);
    }
}
