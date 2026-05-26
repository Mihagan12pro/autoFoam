using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Dialog
{
    public interface IDialogsService
    {
        Task SaveProject();

        Task ShowErrors(IEnumerable<string> errors);
    }
}
