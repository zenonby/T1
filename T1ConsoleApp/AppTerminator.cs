using System.Diagnostics;
using System.Threading.Tasks;
using T1.Abstractions;

namespace T1;

public class AppTerminator : IAppTerminator
{
    private readonly CancellationTokenSource _cancelTokenSrc;
    
    public AppTerminator(CancellationTokenSource cancelTokenSrc)
    {
        _cancelTokenSrc = cancelTokenSrc;
        Debug.Assert(null != _cancelTokenSrc);
    }

    public void StopApp()
    {
        _cancelTokenSrc.Cancel();
    }
}
