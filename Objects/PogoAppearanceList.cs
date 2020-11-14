using System.Collections.Generic;

namespace PoGoEncTool
{
    public class PogoAppearanceList : List<PogoEntry>
    {
        public void Clean() => RemoveAll(z => z.Type == 0);
    }
}
