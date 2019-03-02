#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Assets.Scripts.Data_Models;
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
using System.Diagnostics;
using System.Timers;

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
        static Stopwatch ClickTimer;
        
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


            ClickTimer = new Stopwatch();
           
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

            Canvas inspectCanvas = new Canvas() {
                Width = inspect.Width,
                Height = inspect.Height
            };
            inspect.Children.Add(inspectCanvas);

            Button closeWindow = new Button() {
                Height = inspectCanvas.Height * .1f,
                Width = inspectCanvas.Height * .1f,
                Background = (ImageBrush)inspect.FindResource("X_Icon"),
                Style = (Style)inspect.FindResource("DefaultButton"),
            };
            closeWindow.Click += (object send, RoutedEventArgs args) =>
            {
                _canvas.Children.Remove(inspect);
                //_inspected.Remove(inspect);
            };
            Canvas.SetRight(closeWindow, inspectCanvas.Width * .02f);
            Canvas.SetTop(closeWindow, inspectCanvas.Height * .02f);

#if NOESIS
            Border icon = new Border()
            {
                Background = (ImageBrush)inspect.FindResource("icon" + item.icon),
                Width = Constants.WinHyp * .07f,
                Height = Constants.WinHyp * .07f
            };
            inspectCanvas.Children.Add(icon);
            Canvas.SetLeft(icon, inspectCanvas.Width * .75f);
            Canvas.SetTop(icon, inspectCanvas.Height * .01f);

            TextBlock text = new TextBlock() {
                Text = item.Name,
                FontSize = Constants.WinHyp * .02f,
                FontWeight = FontWeight.Light,
                Foreground = new SolidColorBrush(Color.FromArgb(0, 0, 0, 200)),
                FontFamily = (FontFamily)icon.FindResource("ArialFont"),
                Width = inspectCanvas.Width - inspectCanvas.Width * .1f,
                TextWrapping = TextWrapping.WrapWithOverflow
            };

            inspectCanvas.Children.Add(text);
            Canvas.SetLeft(text, inspectCanvas.Width * .03f);
            Canvas.SetTop(text, inspectCanvas.Height * .04f);
            string temp = string.Empty;
            text.Text += "\r\n";
            text.Text += (item.magic != 0 ? "MAGIC ITEM" : "") + (item.nodrop != 1 ? " NO DROP" : "") + (item.norent != 1 ? " NO RENT" : "");
            for(int x = 0; x < item.slots.Count; x++)
            {
                temp += " " + item.slots[x] + (x == item.slots.Count - 1 ? "" : ",");
            }
            text.Text += "\r\nSlot: " + temp;
            temp = string.Empty;
            if((item.itemtype >= 0 && item.itemtype < 8) ||
                (item.itemtype == 23) || (item.itemtype == 24) || (item.itemtype == 25) || (item.itemtype == 26)
                || (item.itemtype == 35) || (item.itemtype == 45)
                )
            {
                text.Text += "\r\nSkill: " + Constants.ItemTypes[item.itemtype] + "  Atk Delay: " + item.delay;
                text.Text += "\r\nDMG: " + item.damage;
            }
            if(item.ac != 0)
            {
                text.Text += "\r\nAC: " + item.ac;
            }
            if (item.hp != 0 || item.mana != 0   )
            {
                text.Text += "\r\n";
            }
            text.Text += item.hp != 0 ? "HP: " + (item.hp > 0 ? "+" : "-") + item.hp + "  " : "";
            text.Text += item.mana != 0 ? "Mana: " + (item.mana > 0 ? "+" : "-") + item.mana + "  " : "";
            if ( item.astr != 0 || item.aagi != 0 || item.asta != 0 || item.adex != 0 || item.awis != 0 || item.aint != 0 || item.acha != 00
                || item.fr != 0 || item.cr != 0 || item.dr != 0 || item.dr != 0 || item.pr != 0 || item.mr != 0
                )
            {
                text.Text += "\r\n";
            }
            text.Text += item.astr != 0 ? "STR: " + (item.astr > 0 ? "+" : "-") + item.astr + "  " : "";
            text.Text += item.asta != 0 ? "STA: " + (item.asta > 0 ? "+" : "-") + item.asta + "  " : "";
            text.Text += item.aagi != 0 ? "AGI: " + (item.aagi > 0 ? "+" : "-") + item.aagi + "  " : "";
            text.Text += item.adex != 0 ? "DEX: " + (item.adex > 0 ? "+" : "-") + item.adex + "  " : "";
            
            text.Text += item.awis != 0 ? "WIS: " + (item.awis > 0 ? "+" : "-") + item.awis + "  " : "";
            text.Text += item.aint != 0 ? "INT: " + (item.aint > 0 ? "+" : "-") + item.aint + "  " : "";
            text.Text += item.acha != 0 ? "CHA: " + (item.acha > 0 ? "+" : "-") + item.acha + "  " : "";
            text.Text += item.fr != 0 ? "SV FIRE: " + (item.fr > 0 ? "+" : "-") + item.fr + "  " : "";
            text.Text += item.cr != 0 ? "SV COLD: " + (item.cr > 0 ? "+" : "-") + item.cr + "  " : "";
            text.Text += item.dr != 0 ? "SV DISEASE: " + (item.dr > 0 ? "+" : "-") + item.dr + "  " : "";
            text.Text += item.pr != 0 ? "SV POISON: " + (item.pr > 0 ? "+" : "-") + item.pr + "  " : "";
            text.Text += item.mr != 0 ? "SV MAGIC: " + (item.mr > 0 ? "+" : "-") + item.mr + "  " : "";
            text.Text += item.worneffect != -1 ? "\r\nEffect: " + item.worneffect : "";
            text.Text += "\r\nWT: " + (item.weight / 10).ToString("0.0");
            if (item.classes.Count < 16)
            {
                item.classes.ForEach(s => {
                    temp += s + " ";
                });
            }
            else
            {
                temp += "ALL";
            }
            text.Text += "\r\nClass: " + temp;
            temp = string.Empty;
            if (item.races.Count < 16)
            {
                item.races.ForEach(s => {
                    temp += s + " ";
                });
            }
            else
            {
                temp += "ALL";
            }

            text.Text += "\r\nRace: " + temp;



#endif
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
        private static void OnElementDrag(object sen, MouseEventArgs e)
        {
            
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
            ClickTimer.Start();
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
                    _floating.MouseMove += OnItemDrag;
                    _floating.MouseUp += OnItemDrop;
                }
            }
        }

        private static void OnItemDrag(object sender, MouseEventArgs e)
        {
            //UnityEngine.Debug.Log(Stop.ElapsedMilliseconds);
            Point canvasPos = e.GetPosition(_canvas);
            Canvas.SetLeft(_floating, canvasPos.X - _offset.X);
            Canvas.SetTop(_floating, canvasPos.Y - _offset.Y);
        }

        private static void OnItemDrop(object sender, MouseButtonEventArgs e)
        {
            if(ClickTimer.ElapsedMilliseconds < 300)
            {
                Point originItem = e.GetPosition(_selected);
                Size originSize = _selected.RenderSize;
                if (originItem.X >= 0.0f && originItem.X < originSize.Width &&
                originItem.Y >= 0.0f && originItem.Y < originSize.Height)
                {
                    Item item = Constants.ItemFromXamlName(_selected.Name);
                    InspectItem(item);
                }
            }
            ClickTimer.Stop();
            ClickTimer.Reset();
            _floating.ReleaseMouseCapture();
            _floating.MouseMove -= OnItemDrag;
            _floating.MouseUp -= OnItemDrop;

            _floating.Visibility = Visibility.Collapsed;

            //Check to see if item inspect
            Point p = e.GetPosition(_rightMag);
            Size s = _rightMag.RenderSize;
            if (p.X >= 0.0f && p.X < s.Width &&
                p.Y >= 0.0f && p.Y < s.Height)
            {
                Border i = (Border)sender;
                Item item = Constants.ItemFromXamlName(_selected.Name);
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
            
            CheckSlots(Equipment.equip_, e);
            CheckSlots(InventorySlots.slots_, e);
        }

        private static void CheckSlots(List<Border> slots, MouseButtonEventArgs e)
        {
            foreach (Border slot in slots)
            {
                Point pos = e.GetPosition(slot);
                Size size = slot.RenderSize;
                if (pos.X >= 0.0f && pos.X < size.Width &&
                    pos.Y >= 0.0f && pos.Y < size.Height)
                {
                    //_selected.Background = slot.Background;
                    //slot.Background = _floating.Background;
                    ItemMoveRequest(_selected, slot);
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
