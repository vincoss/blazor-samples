using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlazorWebAssemblyApp_GameCanvas_Samples.Code
{
    public interface IGameLoop : IDisposable
    {
        Action<int> Render { get; set; }
    }
}
