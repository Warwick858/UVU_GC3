using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//									     ____.           .____             _____  _______   
//									    |    |           |    |    ____   /  |  | \   _  \  
//									    |    |   ______  |    |   /  _ \ /   |  |_/  /_\  \ 
//									/\__|    |  /_____/  |    |__(  <_> )    ^   /\  \_/   \
//									\________|           |_______ \____/\____   |  \_____  /
//									                             \/          |__|        \/ 
//
// ******************************************************************************************************************
//

namespace UVU_GC3
{
    /// <summary>
    /// PLAYERBOX: The main control for any given player in the gaming center.
    /// </summary>
    public sealed partial class PlayerBox : UserControl
    {
        //Declare Class Vars:
        public ObservableCollection<string> itemsList;

        /// <summary>
        /// To initialize control components
        /// </summary>
        public PlayerBox()
        {
            this.InitializeComponent();

            LoadItems();
        } // end constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">SelectionChangedEventArgs</param>
        private void PlayerFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ////Save sender as flip view
            //FlipView flipView = sender as FlipView;

            ////If flip view lands on items list (third view)
            //if (flipView.SelectedIndex.Equals(1))
            //{
            //    LoadItemsAsync();

            //    LoadItems();
            //} // end if
        } // end method PlayerFlipView_SelectionChanged()

        private void LoadItems()
        {
            itemsList = new ObservableCollection<string>();

            itemsList.Add("/Assets/Thumbnails/battleborn.jpg");
            itemsList.Add("/Assets/Thumbnails/battlefield.jpg");
            itemsList.Add("/Assets/Thumbnails/cod.jpg");

            //
            this.DataContext = itemsList;
        }

        private async Task LoadItemsAsync()
        {
            //
            itemsList = new ObservableCollection<string>();

            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");
            var files = await assets.GetFilesAsync();

            //
            foreach (StorageFile asset in files)
            {
                itemsList.Add(asset.ToString());
            }

            //
            this.DataContext = itemsList;
        } // end

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void WaitingToggle_Toggled(object sender, RoutedEventArgs e)
        {
            //Toggle is ON
            if (WaitingToggle.IsOn)
            {
                //Change waiting textbox background to red, and bring into focus 
                WaitingTxt.Background = new SolidColorBrush(Colors.Red);
                WaitingTxt.Focus(FocusState.Programmatic);
            } // end if
            else // toggle is OFF
            {
                WaitingTxt.Background = new SolidColorBrush(Colors.White);
                WaitingTxt.Text = string.Empty;
            } // end else
        } // end method WaitingToggle_Toggled()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void DecrementBtn_Click(object sender, RoutedEventArgs e)
        {
            //Save items text as int
            int count = int.Parse(ItemsTxt.Text);

            //If count can be decremented, do it
            if (count > 0)
            {
                count--;
            } // end if

            //Set new items text value
            ItemsTxt.Text = count.ToString();
        } // end method DecrementBtn_Click()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void IncrementBtn_Click(object sender, RoutedEventArgs e)
        {
            //Save items text as int
            int count = int.Parse(ItemsTxt.Text);

            //If count hasn't reached max, increment it
            if (count < 50)
            {
                count++;
            } // end if

            //Set new items text value
            ItemsTxt.Text = count.ToString();
        } // end method IncrementBtn_Click()


    } // end class PlayerBox
} // end namespace UVU_GC3
