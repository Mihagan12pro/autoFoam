using AutoFoam.UI.Models.FlatChannel;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Shell
{
    public interface IShellExecuter
    {
        /// <summary>
        /// Executes Clean.sh
        /// </summary>
        /// <returns></returns>
        Task<bool> ExecuteClean();

        /// <summary>
        /// Executes ChangeParams.sh
        /// </summary>
        /// <param name="flatChannel"></param>
        /// <returns></returns>
        Task<bool> ExecuteChangeParams(FlatChannel flatChannel);

        /// <summary>
        /// Opens ParaView
        /// </summary>
        /// <returns></returns>
        Task<bool> ExecuteParaView();

        /// <summary>
        /// Executes Run.sh
        /// </summary>
        /// <param name="flatChannel"></param>
        /// <returns></returns>
        Task<bool> ExecuteRun(FlatChannel flatChannel);

        Task<bool> Execute(string scriptPath);
    }
}
