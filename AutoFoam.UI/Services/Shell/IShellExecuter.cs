using AutoFoam.UI.Models.FlatChannel;

namespace AutoFoam.UI.Services.Shell
{
    public interface IShellExecuter
    {
        /// <summary>
        /// Executes Clean.sh
        /// </summary>
        /// <returns></returns>
        bool ExecuteClean();

        /// <summary>
        /// Executes ChangeParams.sh
        /// </summary>
        /// <param name="flatChannel"></param>
        /// <returns></returns>
        bool ExecuteChangeParams(FlatChannel flatChannel);

        /// <summary>
        /// Opens ParaView
        /// </summary>
        /// <returns></returns>
        bool ExecuteParaView();

        /// <summary>
        /// Executes Run.sh
        /// </summary>
        /// <param name="flatChannel"></param>
        /// <returns></returns>
        bool ExecuteRun(FlatChannel flatChannel);
    }
}
