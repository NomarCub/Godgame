using Godgame.Model.API;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
