using AutoFoam.UI.Models;
using AutoFoam.UI.Models.FlatChannel;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Shell
{
    public interface IShellExecuter
    {
        Task<int> Execute(string scriptPath);
    }

    public interface IShellExecuter<TMesh>  : IShellExecuter
        where TMesh : MeshBase
    {
        /// <summary>
        /// Executes Clean.sh
        /// </summary>
        /// <returns></returns>
        Task<int> ExecuteClean();

        /// <summary>
        /// Executes ChangeParams.sh
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        Task<int> ExecuteChangeParams(TMesh mesh);

        /// <summary>
        /// Opens ParaView
        /// </summary>
        /// <returns></returns>
        Task<int> ExecuteParaView();

        /// <summary>
        /// Executes Run.sh
        /// </summary>
        /// <param name="flatChannel"></param>
        /// <returns></returns>
        Task<int> ExecuteRun(TMesh mesh);
    }
}
