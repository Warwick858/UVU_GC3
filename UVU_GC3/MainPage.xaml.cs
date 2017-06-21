using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static MainPage Current;
        private PointerPoint clickPoint;

        public enum NotifyType
        {
            StatusMessage,
            ErrorMessage
        };

        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
        }


        public void NotifyUser(string strMessage, NotifyType type)
        {
            //switch (type)
            //{
            //    // Use the status message style. 
            //    case NotifyType.StatusMessage:
            //        StatusBlock.Style = Resources["StatusStyle"] as Style;
            //        break;
            //    // Use the error message style. 
            //    case NotifyType.ErrorMessage:
            //        StatusBlock.Style = Resources["ErrorStyle"] as Style;
            //        break;
            //}
            //StatusBlock.Text = strMessage;

            //// Collapse the StatusBlock if it has no text to conserve real estate. 
            //if (StatusBlock.Text != String.Empty)
            //{
            //    StatusBlock.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //}
            //else
            //{
            //    StatusBlock.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //}
        }


        private void NavCmdBar_Opening(object sender, object e)
        {
            //CommandBar cb = sender as CommandBar;
            //if (cb != null) cb.Background.Opacity = 1.0;
        }

        private void NavCmdBar_Closing(object sender, object e)
        {
            //CommandBar cb = sender as CommandBar;
            //if (cb != null) cb.Background.Opacity = 0.3;
        }

        private void NavCmdBar_GotFocus(object sender, RoutedEventArgs e)
        {
            //CommandBar cb = sender as CommandBar;
            //cb.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
        }

        private void NavCmdBar_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void AddNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SignageNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConfirmNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReceiptNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WebsiteNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DiagramNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void YouTubeNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatsNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitNavBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Stn4Rect_DragOver(object sender, DragEventArgs e)
        {
            //e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void Stn4Rect_Drop(object sender, DragEventArgs e)
        {
            //if (e.DataView.Contains(StandardDataFormats.Text))
            //{
            //    var items = await e.DataView.GetDataAsync();

            //    //var items = await e.DataView.GetDataAsync(PlayerBox.Type);
            //    if (items.Count > 0)
            //    {
            //        var storageFile = items[0] as PlayerBox;
            //        var bitmapImage = new BitmapImage();
            //        bitmapImage.SetSource(await storageFile.OpenAsync(FileAccessMode.Read));
            //        // Set the image on the main page to the dropped image
            //        Image.Source = bitmapImage;
            //    }
            //}
        }

        private void PBox2_PointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }



        private void PBox2_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void PBox2_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            composite.TranslateX += e.Delta.Translation.X;
            composite.TranslateY += e.Delta.Translation.Y;
        }

        private void PBox2_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Position.Y < 300)
            {

                var control = sender as UserControl;

                //control.SetValue(Canvas.LeftProperty, 200);
                //control.SetValue(Canvas.TopProperty, 100);

                Grid grid = MainGrid as Grid;
                Grid.SetRow(control, 1);
                Grid.SetColumn(control, 2);

                //control.SetValue(Grid.RowProperty, 0);
                //control.SetValue(Grid.ColumnProperty, 1);

                
            }
        }
    } // end class MainPage
} // end namespace UVU_GC3
