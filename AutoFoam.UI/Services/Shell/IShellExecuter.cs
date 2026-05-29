using AutoFoam.UI.Models;
using AutoFoam.UI.Models.FlatChannel;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Shell
{
    public interface IShellExecuter
    {
        /// <summary>
        /// Executes every script
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <returns></returns>
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
