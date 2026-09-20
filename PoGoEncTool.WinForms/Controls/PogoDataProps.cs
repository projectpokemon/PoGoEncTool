using System.Windows.Forms;
using PoGoEncTool.Core;

namespace PoGoEncTool.WinForms;

public partial class PogoDataProps : UserControl
{
    public PogoDataProps() => InitializeComponent();
    private PogoEntry _current = PogoEntry.CreateNew();
    public void LoadEntry(PogoEntry detail) => propertyGrid1.SelectedObject = _current = detail with { };
    public void SaveEntry(PogoEntry detail) => _current.CopyTo(detail);

    private void ChangeProperty(object? sender, PropertyValueChangedEventArgs e)
    {
        if (Equals(e.OldValue, e.ChangedItem?.Value))
            return;

        switch (e.ChangedItem?.PropertyDescriptor?.Name)
        {
            case nameof(PogoEntry.Type):
                if (!_current.InitializeDefaultsForType(_current.Type))
                    return;

                // Re-read the values from _current.
                propertyGrid1.Refresh();
                break;
        }
    }
}
