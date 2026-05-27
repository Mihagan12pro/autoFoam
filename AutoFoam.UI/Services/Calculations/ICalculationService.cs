using AutoFoam.UI.Models.FlatChannel;
using AutoFoam.UI.Services.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFoam.UI.Services.Calculations
{
    public interface ICalculationService
    {
        Task<bool> Culc(IShellExecuter shellExecuter);
    }
}
