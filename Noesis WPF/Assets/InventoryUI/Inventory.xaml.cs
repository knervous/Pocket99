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

namespace UIInventory
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
        }

        public static void InspectItem(object sender, MouseButtonEventArgs e)
        {
            Border inspect = new Border();
            _canvas.Children.Add(inspect);
            inspect.Width = 400;
            inspect.Height = 300;
            inspect.Background = (ImageBrush)inspect.FindResource("Window_Background");
            inspect.Tag = "InspectWindow";

            Canvas inspectCanvas = new Canvas();
            inspect.Child = inspectCanvas;
            
            Button closeWindow = new Button();
            closeWindow.Height = 25;
            closeWindow.Width = 25;
            closeWindow.Background = (ImageBrush)inspect.FindResource("X_Icon");
            closeWindow.Style = (Style)inspect.FindResource("DefaultButton");
            closeWindow.Click += (object send, RoutedEventArgs args) =>
            {
               
                _canvas.Children.Remove(inspect);
                _inspected.Remove(inspect);
            };
            Canvas.SetLeft(closeWindow, inspect.Width - closeWindow.Width - 15);
            Canvas.SetTop(closeWindow, 15);
#if NOESIS
            TextBlock title = new TextBlock();
            title.Text = "Item to inspect";
            title.FontSize = 18;
            title.Width = inspect.Width;
            title.TextAlignment = TextAlignment.Center;
            title.Foreground = new SolidColorBrush(Color.FromLinearRGB(255,255,255));

            TextBlock stats = new TextBlock();

            inspectCanvas.Children.Add(title);
#endif
            inspectCanvas.Children.Add(closeWindow);
            
            

            Canvas.SetLeft(inspect, 75);
            Canvas.SetTop(inspect, 50);
            inspect.MouseDown += DragElement;

            _inspected.Add(inspect);
            
        }

        private static void DragElement(object sender, MouseButtonEventArgs e)
        {
            var win = (Border)sender;
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
            var win = (Border)sen;
            Point canvasPos2 = e.GetPosition(_canvas);
            Canvas.SetLeft(win, canvasPos2.X - _offset.X);
            Canvas.SetTop(win, canvasPos2.Y - _offset.Y);
        }
        private static void OnElementDrop(object sen, MouseButtonEventArgs e)
        {
            var win = (Border)sen;
            win.ReleaseMouseCapture();
            win.MouseMove -= OnElementDrag;
            win.MouseUp -= OnElementDrop;
        }



        public static void DragItem(object sender, MouseButtonEventArgs e)
        {
            _selected = sender as Border;
            if (_selected != null)
            {
                // Initiate drag
                _floating.Background = _selected.Background;
                _floating.BorderBrush = _selected.BorderBrush;
                _floating.Visibility = Visibility.Visible;

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

            foreach(Border win in _inspected)
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
                    // slot.BorderBrush = _floating.BorderBrush;

                   // UpdateEquip();
                    break;
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
                    // slot.BorderBrush = _floating.BorderBrush;

                    // UpdateEquip();
                    break;
                }
            }
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
            Equipment.face.Background = (ImageBrush)temp.FindResource("icon" + (inv.HeadSlot != null ? inv.FaceSlot.icon : 1723));
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

#endif
        }

    }


}
