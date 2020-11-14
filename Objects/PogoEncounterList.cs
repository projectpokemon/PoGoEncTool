using System.Collections.Generic;

namespace PoGoEncTool
{
    public class PogoEncounterList : List<PogoPoke>
    {
        public PogoPoke GetDetails(int species, int form)
        {
            var exist = Find(z => z.Species == species && z.Form == form);
            if (exist != null)
                return exist;

            var created = PogoPoke.CreateNew(species, form);
            Add(created);
            return created;
        }

        public void Clean()
        {
            foreach (var d in this)
                d.Available.Clean();
            RemoveAll(z => z.Available.Count == 0);
        }
    }
}
