#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Diagnostics;

#endif
using System.Collections.Generic;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class Inv : UserControl
    {
        static Canvas _canvas;
        static Point _offset;
        static Border _floating;
        static Border _selected;
        static Border _rightMag;
        static Border _leftMag;
        const int NumSlots = 12;
        static List<Border> _inspected = new List<Border>();

        public Inv()
        {
            Initialized += OnInitialized;
            InitializeComponent();
        }



#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/NoesisGUI/Interface/InventoryUI/Inventory.xaml");
        }
#endif

        private void OnInitialized(object sender, EventArgs e)
        {
            _canvas = (Canvas)FindName("InventoryCanvas");
            _floating = (Border)FindName("InventoryFloating");
            _rightMag = (Border)FindName("RightMag");

            _rightMag.Width = Constants.WinHyp * .1f;
            _rightMag.Height = Constants.WinHyp * .1f;
            Canvas.SetLeft(_rightMag, Constants.WinWidth * .85f);
            Canvas.SetTop(_rightMag, Constants.WinHeight * .78f);

            _canvas.Width = Constants.WinWidth;
            _canvas.Height = Constants.WinHeight;
            _floating.Width = Constants.WinHyp / 12;
            _floating.Height = Constants.WinHyp / 12;
            InventorySlots inventorySlots = FindName("InventorySlots") as InventorySlots;
            Canvas.SetLeft(inventorySlots, Constants.WinWidth * .7f);
            Canvas.SetTop(inventorySlots, Constants.WinHeight * .1f);

        }

        public static void InspectItem(Item item)
        {
            Canvas inspect = new Canvas();
            
            _canvas.Children.Add(inspect);
            inspect.Width = Constants.WinHyp * .6f;
            inspect.Height = Constants.WinHyp * .4f;
            //inspect.Background = (ImageBrush)inspect.FindResource("Window_Background");
            inspect.Tag = "InspectWindow";

            Border bgTopLeft = new Border()
            {
                Width = inspect.Width * .5f,
                Height = inspect.Height * .5f,
                Background = (ImageBrush)inspect.FindResource("Inspect_TopLeft"),
                BorderThickness = new Thickness(0),
                ClipToBounds = true
            };
            
            Border bgTopRight = new Border()
            {
                Width = inspect.Width * .5f,
                Height = inspect.Height * .5f,
                Background = (ImageBrush)inspect.FindResource("Inspect_TopRight"),
                BorderThickness = new Thickness(0),
                ClipToBounds = true
            };

            Border bgBottomLeft = new Border()
            {
                Width = inspect.Width * .5f,
                Height = inspect.Height * .5f,
                Background = (ImageBrush)inspect.FindResource("Inspect_BottomLeft"),
                BorderThickness = new Thickness(0),
                ClipToBounds = true
            };

            Border bgBottomRight = new Border()
            {
                Width = inspect.Width * .5f,
                Height = inspect.Height * .5f,
                Background = (ImageBrush)inspect.FindResource("Inspect_BottomRight"),
                BorderThickness = new Thickness(0),
                ClipToBounds = true
            };

            inspect.Children.Add(bgTopLeft);
            inspect.Children.Add(bgTopRight);
            inspect.Children.Add(bgBottomLeft);
            inspect.Children.Add(bgBottomRight);
            Canvas.SetLeft(bgTopRight, inspect.Width * .49f);
            Canvas.SetLeft(bgBottomRight, inspect.Width * .49f);
            Canvas.SetTop(bgBottomLeft, inspect.Height * .49f);
            Canvas.SetTop(bgBottomRight, inspect.Height * .49f);

            Canvas inspectCanvas = new Canvas();
            inspect.Children.Add(inspectCanvas);
            
            Button closeWindow = new Button();
            closeWindow.Height = 25;
            closeWindow.Width = 25;
            closeWindow.Background = (ImageBrush)inspect.FindResource("X_Icon");
            closeWindow.Style = (Style)inspect.FindResource("DefaultButton");
            closeWindow.Click += (object send, RoutedEventArgs args) =>
            {
                _canvas.Children.Remove(inspect);
                //_inspected.Remove(inspect);
            };
            Canvas.SetLeft(closeWindow, inspect.Width - closeWindow.Width - 15);
            Canvas.SetTop(closeWindow, 15);




            Border icon = new Border() {
                Background = (ImageBrush)inspect.FindResource("icon500"),
                Width = Constants.WinHyp * .07,
                Height = Constants.WinHeight * .07
            };
            Canvas.SetLeft(icon, inspectCanvas.Width * .7);
            Canvas.SetTop(icon, inspectCanvas.Height * .05);
            inspectCanvas.Children.Add(icon);
            TextBlock title = new TextBlock();
            title.Text = "Item to inspect";
            title.FontSize = 18;
            title.Width = inspect.Width;
            title.TextAlignment = TextAlignment.Center;
            title.Foreground = new SolidColorBrush(Colors.Black);
            title.FontFamily = (FontFamily) title.FindResource("ArialFont");

            TextBlock stats = new TextBlock();

            inspectCanvas.Children.Add(title);

            inspectCanvas.Children.Add(closeWindow);
            
            

            Canvas.SetLeft(inspect, 75);
            Canvas.SetTop(inspect, 50);
            inspect.MouseDown += DragElement;

            //_inspected.Add(inspect);
            
        }

        private static void DragElement(object sender, MouseButtonEventArgs e)
        {
            var win = (Canvas)sender;
            _offset = e.GetPosition(win);
            Point canvasPos = e.GetPosition(_canvas);
            Canvas.SetLeft(_floating, canvasPos.X - _offset.X);
            Canvas.SetTop(_floating, canvasPos.Y - _offset.Y);
            if (win.CaptureMouse())
            {
                win.MouseMove += OnElementDrag;
                win.MouseUp += OnElementDrop;
            }
        }
        private static void OnElementDrag(object sen, MouseEventArgs e) {
            var win = (Canvas)sen;
            Point canvasPos2 = e.GetPosition(_canvas);
            Canvas.SetLeft(win, canvasPos2.X - _offset.X);
            Canvas.SetTop(win, canvasPos2.Y - _offset.Y);
        }
        private static void OnElementDrop(object sen, MouseButtonEventArgs e)
        {
            var win = (Canvas)sen;
            win.ReleaseMouseCapture();
            win.MouseMove -= OnElementDrag;
            win.MouseUp -= OnElementDrop;
        }



        public static void DragItem(object sender, MouseButtonEventArgs e)
        {
            _selected = sender as Border;
            if (_selected != null)
            {
                _floating.Background = _selected.Background;
                _floating.BorderBrush = _selected.BorderBrush;
                _floating.Visibility = Visibility.Visible;
                _rightMag.Visibility = Visibility.Visible;

                _offset = e.GetPosition(_selected);
                Point canvasPos = e.GetPosition(_canvas);
                Canvas.SetLeft(_floating, canvasPos.X - _offset.X);
                Canvas.SetTop(_floating, canvasPos.Y - _offset.Y);

                if (_floating.CaptureMouse())
                {
                    _floating.MouseMove += OnFloatingDrag;
                    _floating.MouseUp += OnFloatingDrop;
                }
            }
        }

        private static void OnFloatingDrag(object sender, MouseEventArgs e)
        {
            Point canvasPos = e.GetPosition(_canvas);
            Canvas.SetLeft(_floating, canvasPos.X - _offset.X);
            Canvas.SetTop(_floating, canvasPos.Y - _offset.Y);
        }

        private static void OnFloatingDrop(object sender, MouseButtonEventArgs e)
        {
            _floating.ReleaseMouseCapture();
            _floating.MouseMove -= OnFloatingDrag;
            _floating.MouseUp -= OnFloatingDrop;

            _floating.Visibility = Visibility.Collapsed;

            //Check to see if item inspect
            Point p = e.GetPosition(_rightMag);
            Size s = _rightMag.RenderSize;
            if (p.X >= 0.0f && p.X < s.Width &&
                p.Y >= 0.0f && p.Y < s.Height)
            {
                Border i = (Border)sender;
                Item item = Constants.ItemFromXamlName(i.Name);
                InspectItem(item);
                _rightMag.Visibility = Visibility.Collapsed;
                return;
            }

            _rightMag.Visibility = Visibility.Collapsed;

            foreach (Border win in _inspected)
            {
                Point pos = e.GetPosition(win);
                Size size = win.RenderSize;
                if (pos.X >= 0.0f && pos.X < size.Width &&
                    pos.Y >= 0.0f && pos.Y < size.Height)
                {
                    return;
                }
            }

            foreach (Border slot in Equipment.equip_)
            {
                Point pos = e.GetPosition(slot);
                Size size = slot.RenderSize;
                if (pos.X >= 0.0f && pos.X < size.Width &&
                    pos.Y >= 0.0f && pos.Y < size.Height)
                {
                    _selected.Background = slot.Background;
                    slot.Background = _floating.Background;
                    return;
                }
            }

            foreach (Border slot in InventorySlots.slots_)
            {
                Point pos = e.GetPosition(slot);
                Size size = slot.RenderSize;
                if (pos.X >= 0.0f && pos.X < size.Width &&
                    pos.Y >= 0.0f && pos.Y < size.Height)
                {
                    _selected.Background = slot.Background;
                    slot.Background = _floating.Background;
                    return;
                }
            }
        }

        private static void ItemMoveRequest(Border SelectedFrom, Border SlotTo)
        {
#if NOESIS
            JSONObject obj = new JSONObject();
            obj.AddField("from", SelectedFrom.Name);
            obj.AddField("to", SlotTo.Name);
            obj.AddField("char", MainPlayer.instance.GetComponent<PlayerAttributes>().player.char_id_);
            Network.socket.Emit("item_swap", obj);
#endif
        }

        public static void ToggleVisibility()
        {
            _canvas.Visibility = _canvas.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        public static void UpdateEquip()
        {
#if NOESIS
            var inv = MainPlayer.instance.GetComponent<PlayerAttributes>().player.inventory_;
            Border temp = Equipment.leftEar;

            Equipment.leftEar.Background = (ImageBrush)temp.FindResource("icon" + (inv.LeftEarSlot != null ? inv.LeftEarSlot.icon : 1723));
            Equipment.neck.Background = (ImageBrush)temp.FindResource("icon" + (inv.NeckSlot != null ? inv.NeckSlot.icon : 1723));
            Equipment.head.Background = (ImageBrush)temp.FindResource("icon" + (inv.HeadSlot != null ? inv.HeadSlot.icon : 1723));
            Equipment.face.Background = (ImageBrush)temp.FindResource("icon" + (inv.FaceSlot != null ? inv.FaceSlot.icon : 1723));
            Equipment.rightEar.Background = (ImageBrush)temp.FindResource("icon" + (inv.RightEarSlot != null ? inv.RightEarSlot.icon : 1723));
            Equipment.leftFinger.Background = (ImageBrush)temp.FindResource("icon" + (inv.LeftFingerSlot != null ? inv.LeftFingerSlot.icon : 1723));
            Equipment.leftWrist.Background = (ImageBrush)temp.FindResource("icon" + (inv.LeftWristSlot != null ? inv.LeftWristSlot.icon : 1723));
            Equipment.arms.Background = (ImageBrush)temp.FindResource("icon" + (inv.ArmSlot != null ? inv.ArmSlot.icon : 1723));
            Equipment.hands.Background = (ImageBrush)temp.FindResource("icon" + (inv.GloveSlot != null ? inv.GloveSlot.icon : 1723));
            Equipment.rightWrist.Background = (ImageBrush)temp.FindResource("icon" + (inv.RightWristSlot != null ? inv.RightWristSlot.icon : 1723));
            Equipment.rightFinger.Background = (ImageBrush)temp.FindResource("icon" + (inv.RightFingerSlot != null ? inv.RightFingerSlot.icon : 1723));
            Equipment.shoulders.Background = (ImageBrush)temp.FindResource("icon" + (inv.ShoulderSlot != null ? inv.ShoulderSlot.icon : 1723));
            Equipment.chest.Background = (ImageBrush)temp.FindResource("icon" + (inv.ChestSlot != null ? inv.ChestSlot.icon : 1723));
            Equipment.back.Background = (ImageBrush)temp.FindResource("icon" + (inv.BackSlot != null ? inv.BackSlot.icon : 1723));
            Equipment.belt.Background = (ImageBrush)temp.FindResource("icon" + (inv.WaistSlot != null ? inv.WaistSlot.icon : 1723));
            Equipment.legs.Background = (ImageBrush)temp.FindResource("icon" + (inv.LegSlot != null ? inv.LegSlot.icon : 1723));
            Equipment.boots.Background = (ImageBrush)temp.FindResource("icon" + (inv.FeetSlot != null ? inv.FeetSlot.icon : 1723));
            Equipment.primary.Background = (ImageBrush)temp.FindResource("icon" + (inv.PrimarySlot != null ? inv.PrimarySlot.icon : 1723));
            Equipment.offhand.Background = (ImageBrush)temp.FindResource("icon" + (inv.SecondarySlot != null ? inv.SecondarySlot.icon : 1723));
            Equipment.ranged.Background = (ImageBrush)temp.FindResource("icon" + (inv.RangedSlot != null ? inv.RangedSlot.icon : 1723));
            Equipment.ammo.Background = (ImageBrush)temp.FindResource("icon" + (inv.RangedSlot != null ? inv.RangedSlot.icon : 1723));
            if (inv.Slot1 != null)
                InventorySlots.slot1.Background = (ImageBrush)temp.FindResource("icon" + inv.Slot1.icon) != null ? (ImageBrush)temp.FindResource("icon" + inv.Slot1.icon) : (ImageBrush)temp.FindResource("icon1693");
            else
                InventorySlots.slot1.Background = null;
            if (inv.Slot2 != null)
                InventorySlots.slot2.Background = (ImageBrush)temp.FindResource("icon" + inv.Slot2.icon) != null ? (ImageBrush)temp.FindResource("icon" + inv.Slot2.icon) : (ImageBrush)temp.FindResource("icon1693");
            else
                InventorySlots.slot2.Background = null;
            if (inv.Slot3 != null)
                InventorySlots.slot3.Background = (ImageBrush)temp.FindResource("icon" + inv.Slot3.icon) != null ? (ImageBrush)temp.FindResource("icon" + inv.Slot3.icon) : (ImageBrush)temp.FindResource("icon1693");
            else
                InventorySlots.slot3.Background = null;
            if (inv.Slot4 != null)
                InventorySlots.slot4.Background = (ImageBrush)temp.FindResource("icon" + inv.Slot4.icon) != null ? (ImageBrush)temp.FindResource("icon" + inv.Slot4.icon) : (ImageBrush)temp.FindResource("icon1693");
            else
                InventorySlots.slot4.Background = null;
            if (inv.Slot5 != null)
                InventorySlots.slot5.Background = (ImageBrush)temp.FindResource("icon" + inv.Slot5.icon) != null ? (ImageBrush)temp.FindResource("icon" + inv.Slot5.icon) : (ImageBrush)temp.FindResource("icon1693");
            else
                InventorySlots.slot5.Background = null;
            if (inv.Slot6 != null)
                InventorySlots.slot6.Background = (ImageBrush)temp.FindResource("icon" + inv.Slot6.icon) != null ? (ImageBrush)temp.FindResource("icon" + inv.Slot6.icon) : (ImageBrush)temp.FindResource("icon1693");
            else
                InventorySlots.slot6.Background = null;
            if (inv.Slot7 != null)
                InventorySlots.slot7.Background = (ImageBrush)temp.FindResource("icon" + inv.Slot7.icon) != null ? (ImageBrush)temp.FindResource("icon" + inv.Slot7.icon) : (ImageBrush)temp.FindResource("icon1693");
            else
                InventorySlots.slot7.Background = null;
            if (inv.Slot8 != null)
                InventorySlots.slot8.Background = (ImageBrush)temp.FindResource("icon" + inv.Slot8.icon) != null ? (ImageBrush)temp.FindResource("icon" + inv.Slot8.icon) : (ImageBrush)temp.FindResource("icon1693");
            else
                InventorySlots.slot8.Background = null;

#endif
        }

    }


}
