using Godgame.Model.API;
using Windows.UI.Xaml.Controls;

namespace Godgame
{
    public sealed partial class ContainerContentDialog : ContentDialog
    {
        private Inventory PlayerInventory;
        private Inventory ContainerInventory;

        public ContainerContentDialog(Inventory playerInventory, Inventory containerInventory)
        {
            this.InitializeComponent();
            PlayerInventory = playerInventory;
            ContainerInventory = containerInventory;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void PlayerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (sender as ListView).SelectedItem as ItemAmount;
            if (selected == null) return;
            PlayerInventory.Remove(selected);
            ContainerInventory.Add(selected);
        }
        private void ContainerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (sender as ListView).SelectedItem as ItemAmount;
            if (selected == null) return;
            ContainerInventory.Remove(selected);
            PlayerInventory.Add(selected);
        }
    }
}
