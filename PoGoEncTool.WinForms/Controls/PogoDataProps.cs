using System.Windows.Forms;
using PoGoEncTool.Core;

namespace PoGoEncTool.WinForms;

public partial class PogoDataProps : UserControl
{
    public PogoDataProps() => InitializeComponent();
    private PogoEntry _current = PogoEntry.CreateNew();
    public void LoadEntry(PogoEntry detail) => propertyGrid1.SelectedObject = _current = detail with { };
    public void SaveEntry(PogoEntry detail) => _current.CopyTo(detail);
}
