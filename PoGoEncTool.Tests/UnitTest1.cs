using PoGoEncTool.Core;
using Xunit;

namespace PoGoEncTool.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var exeggcute = EvoUtil.GetEvoSpecForms((int)PKHeX.Core.Species.Exeggcute, 0);
        foreach (var (_, _) in exeggcute)
        {
        }
    }
}
