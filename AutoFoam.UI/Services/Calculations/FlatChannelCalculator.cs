using AutoFoam.UI.Models.FlatChannel;
using AutoFoam.UI.Services.Shell;
using System;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Calculations
{
    internal class FlatChannelCalculator : ICalculator<FlatChannelExecuter, FlatChannelMesh>
    {
        public Task<bool> Culc(
            FlatChannelExecuter executer, 
            FlatChannelMesh mesh)
        {
            throw new NotImplementedException();
        }
    }
}
