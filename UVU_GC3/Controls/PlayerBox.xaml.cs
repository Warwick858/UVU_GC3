using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Devices.Enumeration;
using Windows.Devices.Scanners;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI;
using Windows.UI.Input;
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
        private DeviceWatcher scannerWatcher;
        //public StorageFolder folder = ApplicationData.Current.LocalFolder;

        private bool isDragging;
        private PointerPoint clickPoint;
        private Pointer pointer;

        /// <summary>
        /// To initialize control components
        /// </summary>
        public PlayerBox()
        {
            this.InitializeComponent();
            //LoadItems();
            LoadItemsAsync();

        } // end constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">SelectionChangedEventArgs</param>
        private void PlayerFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Save sender as flip view
            FlipView flipView = sender as FlipView;

            //If flip view lands on items list (third view)
            if (flipView.SelectedIndex.Equals(1))
            {
                //LoadItemsAsync();
                //LoadItems();
            } // end if
        } // end method PlayerFlipView_SelectionChanged()

        private void LoadItems()
        {
            itemsList = new ObservableCollection<string>();
            itemsList.Add("/Assets/Thumbnails/battleborn.jpg");
            itemsList.Add("/Assets/Thumbnails/battlefield.jpg");
            this.DataContext = itemsList;
        }

        private async Task LoadItemsAsync()
        {
            //
            itemsList = new ObservableCollection<string>();

            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");

            StorageFolder thumbnails = await assets.GetFolderAsync("Thumbnails");
            var files = await thumbnails.GetFilesAsync();

            //
            foreach (StorageFile thumb in files)
            {
                itemsList.Add(thumb.Path);
            }

            //
            this.DataContext = itemsList;
        } // end

        /// <summary>
        /// To clear all data associated with the current player
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            //
            WaitingTxt.Text = string.Empty;
            NotesTxt.Text = string.Empty;
            ItemCountLbl.Text = "0";
            GuestCountLbl.Text = "0";

            //CLEAR GUEST FORM
            //CLEAR ID IMAGE
        } // end method ClearBtn_Click()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void DecrementBtn_Click(object sender, RoutedEventArgs e)
        {
            //Save items text as int
            int count = int.Parse(ItemCountLbl.Text);

            //If count can be decremented, do it
            if (count > 0)
            {
                count--;
            } // end if

            //Set new items text value
            ItemCountLbl.Text = count.ToString();
        } // end method DecrementBtn_Click()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void IncrementBtn_Click(object sender, RoutedEventArgs e)
        {
            //Save items text as int
            int count = int.Parse(ItemCountLbl.Text);

            //If count hasn't reached max, increment it
            if (count < 50)
            {
                count++;
            } // end if

            //Set new items text value
            ItemCountLbl.Text = count.ToString();
        } // end method IncrementBtn_Click()

        //*********************OWNER***********************

        private void IDPnl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            IDCmdBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
        }

        private void IDPnl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            IDCmdBar.ClosedDisplayMode = AppBarClosedDisplayMode.Hidden;
        }

        private void ScanIDBtn_Click(object sender, RoutedEventArgs e)
        {
            InitDeviceWatcher();
        }

        void InitDeviceWatcher()
        {
            // Create a Device Watcher class for type Image Scanner for enumerating scanners
            scannerWatcher = DeviceInformation.CreateWatcher(DeviceClass.ImageScanner);

            //StorageFolder testFolder = await StorageFolder.GetFolderFromPathAsync(@"D:\Pictures\Test");
            //ImageScanner myScanner = await ImageScanner.FromIdAsync("{4D36E967-E325-11CE-BFC1-08002BE10318}");
            //var result = await myScanner.ScanFilesToFolderAsync(ImageScannerScanSource.Default, testFolder);

            scannerWatcher.Added += OnScannerAdded;
            //scannerWatcher.Removed += OnScannerRemoved;
            //scannerWatcher.EnumerationCompleted += OnScannerEnumerationComplete;
        }

        private async void OnScannerAdded(DeviceWatcher sender, DeviceInformation deviceInfo)
        {
            await
            MainPage.Current.Dispatcher.RunAsync(
                  Windows.UI.Core.CoreDispatcherPriority.Normal,
                  () =>
                  {
                      string stuff = "test";

                      //MainPage.Current.NotifyUser(String.Format("Scanner with device id {0} has been added", deviceInfo.Id), NotifyType.StatusMessage);

                      //// search the device list for a device with a matching device id
                      //ScannerDataItem match = FindInList(deviceInfo.Id);

                      //// If we found a match then mark it as verified and return
                      //if (match != null)
                      //{
                      //    match.Matched = true;
                      //    return;
                      //}

                      //// Add the new element to the end of the list of devices
                      //AppendToList(deviceInfo);
                  }
            );

            //var library = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            //var folders = library.Folders;

            StorageFolder testFolder = await StorageFolder.GetFolderFromPathAsync(@"D:\Pictures\Test");

            //ImageScanner myScanner = await ImageScanner.FromIdAsync(deviceId);

            //4D36E967-E325-11CE-BFC1-08002BE10318
            ImageScanner myScanner = await ImageScanner.FromIdAsync(deviceInfo.Id);
            var result = await myScanner.ScanFilesToFolderAsync(ImageScannerScanSource.Default, testFolder);
        }

        private void LoadIDBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveIDBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        //*********************GUEST***********************

        private void GuestIDPnl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void GuestIDPnl_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void DeleteGuestBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveGuestBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PromoteGuestBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        //*********************END GUEST***********************

        private void PBox_DragOver(object sender, DragEventArgs e)
        {
            //e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void PBox_Drop(object sender, DragEventArgs e)
        {
            //if (e.DataView.Contains(StandardDataFormats.StorageItems))
            //{
            //    var items = await e.DataView.GetStorageItemsAsync();
            //    if (items.Count > 0)
            //    {
            //        //var storageFile = items[0] as StorageFile;
            //        //var bitmapImage = new BitmapImage();
            //        //bitmapImage.SetSource(await storageFile.OpenAsync(FileAccessMode.Read));
            //        //// Set the image on the main page to the dropped image
            //        //Image.Source = bitmapImage;
            //    }
            //}
        }

        //private void PBox_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        //{
        //    //Opacity = 0.5;
        //}

        //private void PBox_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        //{
        //    //var ct = (CompositeTransform)RenderTransform ?? new CompositeTransform { CenterX = 0.5, CenterY = 0.5 };
        //    //ct.TranslateX += e.Delta.Translation.X;
        //    //ct.TranslateY += e.Delta.Translation.Y;
        //}

        //private void PBox_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        //{
        //    //Opacity = 1;
        //}

        //SILVERLIGHT ATTEMPT

        //private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    isDragging = true;
        //    var draggableControl = sender as UserControl;
        //    clickPoint = e.GetPosition(this);
        //    draggableControl.CaptureMouse();
        //}

        //private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    isDragging = false;
        //    var draggable = sender as UserControl;
        //    draggable.ReleaseMouseCapture();
        //}

        //private void Control_MouseMove(object sender, MouseEventArgs e)
        //{
        //    var draggableControl = sender as UserControl;

        //    if (isDragging && draggableControl != null)
        //    {
        //        Point currentPosition = e.GetPosition(this.Parent as UIElement);

        //        var transform = draggableControl.RenderTransform as TranslateTransform;
        //        if (transform == null)
        //        {
        //            transform = new TranslateTransform();
        //            draggableControl.RenderTransform = transform;
        //        }

        //        transform.X = currentPosition.X - clickPoint.X;
        //        transform.Y = currentPosition.Y - clickPoint.Y;
        //    }
        //}

        //*******************************

        private void PBox_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.CanDrag = true;
            isDragging = true;
            var draggedBox = sender as PlayerBox; // UserControl

            clickPoint = e.GetCurrentPoint(this.Parent as UIElement);
            draggedBox.CapturePointer(e.Pointer);
        }

        private void PBox_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var draggedBox = sender as PlayerBox; // UserControl

            if (isDragging && draggedBox != null)
            {
                //Point currentPosition = e.GetPosition(this.Parent as UIElement);
                PointerPoint currentPosition = e.GetCurrentPoint(this.Parent as UIElement);
                //PointerPoint currentPosition = e.GetCurrentPoint(draggableControl);
                
                var transform = draggedBox.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggedBox.RenderTransform = transform;
                }

                //if (PBoxTranslate == null)
                //{
                //    PBoxTranslate = new TranslateTransform();
                //    draggedBox.RenderTransform = PBoxTranslate;
                //}

                //draggableControl.RenderTransformOrigin = new Point(0.5, 0.5);
                ////RotateTransform rt = new RotateTransform();
                //TranslateTransform tt = new TranslateTransform();
                //draggableControl.RenderTransform = tt;
                //draggableControl.RenderTransform.

                //transform.X = currentPosition.X - clickPoint.X;
                //transform.X = (clickPoint.Position.X - currentPosition.Position.X);
                //transform.X = currentPosition.Position.X;
                transform.X = currentPosition.Position.X - clickPoint.Position.X;
                //PBoxTranslate.X = (currentPosition.Position.X - clickPoint.Position.X);


                //transform.Y = currentPosition.Y - clickPoint.Y;
                //transform.Y = (clickPoint.Position.Y - currentPosition.Position.Y);
                //transform.Y = currentPosition.Position.Y;
                transform.Y = currentPosition.Position.Y - clickPoint.Position.Y;
                //PBoxTranslate.Y = (currentPosition.Position.Y - clickPoint.Position.Y);


                //MUST BE DONE IN MAINPAGE???

            } // end if
        } // end method PBox_PointerMoved()

        private void PBox_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            this.CanDrag = false;
            isDragging = false;
            var draggedBox = sender as PlayerBox; // UserControl

            //PointerPoint currentPosition = e.GetCurrentPoint(this.Parent as UIElement);
            //var transform = draggedBox.RenderTransform as TranslateTransform;
            //transform.X = (currentPosition.Position.X - clickPoint.Position.X);
            //transform.Y = (currentPosition.Position.Y - clickPoint.Position.Y);

            draggedBox.ReleasePointerCapture(e.Pointer);
            //Point relativePoint = TransformToVisual(Parent as UIElement);
        }
    } // end class PlayerBox
} // end namespace UVU_GC3
