using AutoFoam.UI.Models;
using AutoFoam.UI.Services.Shell;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Calculations
{
    public interface ICalculator<TExecuter, TMesh>
        where TExecuter : IShellExecuter 
        where TMesh : MeshBase
    {
        Task<bool> Culc(TExecuter executer, TMesh mesh);
    }
}
