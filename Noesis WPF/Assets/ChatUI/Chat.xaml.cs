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
    public partial class Chat : UserControl
    {
        static Canvas _canvas;
        static Point _offset;

        Canvas _ChatContainer;
        Canvas _ChatCanvasInner;
        Canvas _ChatCanvas;
        Border _ChatHeader;
        Border _ChatDrag;
        static TextBlock _ChatText;
        static ScrollViewer _ChatTextContainer;
        Button _ChatSend;
        TextBlock _ChatSendText;
        TextBlock _ChatHeaderText;
        TextBox _ChatEntry;
        ComboBox _MessageType;
#if NOESIS
        private float chatY;
#else
        private double chatY;
#endif

        public Chat()
        {
            Initialized += OnInitialized;
            InitializeComponent();
        }


#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/NoesisGUI/Interface/ChatUI/Chat.xaml");
        }
#endif

        private void OnInitialized(object sender, EventArgs e)
        {
            _ChatContainer = FindName("ChatContainer") as Canvas;
            _ChatCanvasInner = FindName("ChatCanvasInner") as Canvas;
            _ChatHeaderText = FindName("ChatHeaderText") as TextBlock;
            _ChatCanvas = FindName("ChatCanvas") as Canvas;
            _ChatHeader = FindName("ChatHeader") as Border;
            _ChatDrag = FindName("ChatDrag") as Border;
            _ChatText = FindName("ChatText") as TextBlock;
            _ChatTextContainer = FindName("ChatTextContainer") as ScrollViewer;
            _ChatEntry = FindName("ChatEntry") as TextBox;
            _ChatSend = FindName("ChatSend") as Button;
            _ChatSendText = FindName("ChatSendText") as TextBlock;
            _MessageType = FindName("MessageType") as ComboBox;
            _canvas = (Canvas)FindName("ChatCanvas");

            _MessageType.Width = Constants.WinWidth * .125f;
            _MessageType.Height = Constants.WinHeight * .125f;
            _MessageType.FontSize = Constants.WinHyp * .02f;
            _MessageType.SelectedIndex = 0;
            Canvas.SetTop(_MessageType, Constants.WinHeight * .7f);
            Canvas.SetLeft(_MessageType, Constants.WinWidth * .025f);

            foreach (var c in _MessageType.Items)
            {
                var f = (ComboBoxItem)c;
                f.Height = Constants.WinHeight * .08f;
                f.FontSize = Constants.WinHyp * .02f;
            }

            _ChatContainer.Width = Constants.WinWidth;
            _ChatContainer.Height = Constants.WinHeight;
            Canvas.SetTop(_ChatCanvas, Constants.WinHeight * .92f);
            _ChatCanvas.Width = Constants.WinWidth;
            _ChatCanvas.Height = Constants.WinHeight * .92f;
            _ChatCanvasInner.Height = Constants.WinHeight;
            _ChatCanvasInner.Width = Constants.WinWidth;
            Canvas.SetTop(_ChatCanvasInner, Constants.WinHeight * .1f);
            _ChatHeader.Width = Constants.WinWidth * .85f;
            _ChatHeader.Height = Constants.WinHeight * .066f;
            Canvas.SetLeft(_ChatHeader, Constants.WinWidth * .075f);
            Canvas.SetTop(_ChatHeader, Constants.WinHeight * .075f);
            _ChatDrag.Width = Constants.WinHyp * .075f;
            _ChatDrag.Height = Constants.WinHyp * .04f;
            Canvas.SetTop(_ChatDrag, 0);
            Canvas.SetLeft(_ChatDrag, Constants.WinWidth * .46f);

            _ChatTextContainer.Width = Constants.WinWidth * .95f;
            _ChatTextContainer.Height = Constants.WinHeight * .55f;


            Canvas.SetLeft(_ChatTextContainer, Constants.WinWidth * .025f);
            Canvas.SetTop(_ChatTextContainer, Constants.WinHeight * .075f);

            _ChatText.FontSize = Constants.WinHyp * .02f;
            _ChatHeaderText.FontSize = Constants.WinHyp * .02f;
            _ChatText.Width = _ChatTextContainer.Width;
            Canvas.SetLeft(_ChatText, Constants.WinWidth * .025f);
            Canvas.SetTop(_ChatText, Constants.WinHeight * .075f);
            for (int x = 0; x < 30; x++)
            {
                // _ChatText.Text += "\r\n Hi";
            }

            _ChatEntry.Width = Constants.WinWidth * .6f;
            _ChatEntry.Height = Constants.WinHeight * .1f;
            _ChatEntry.FontSize = Constants.WinHyp * .02f;

            _ChatSendText.FontSize = Constants.WinHyp * .015f;

            Canvas.SetTop(_ChatEntry, Constants.WinHeight * .7f);
            Canvas.SetLeft(_ChatEntry, Constants.WinWidth * .2f);


            _ChatSend.Width = Constants.WinWidth * .12f;
            _ChatSend.Height = Constants.WinHeight * .125f;
            Canvas.SetTop(_ChatSend, Constants.WinHeight * .7f);
            Canvas.SetLeft(_ChatSend, Constants.WinWidth * .85f);

            _ChatDrag.MouseDown += DragChatWindow;

            _ChatSend.Click += SendMessage;
            _ChatText.MouseDown += ScrollChatDown;

        }


        private void SendMessage(object sender, RoutedEventArgs e)
        {
#if NOESIS
            JSONObject obj = new JSONObject();
            var sel = (ComboBoxItem)_MessageType.SelectedItem;
            obj.AddField("type", sel.Content.ToString());
            obj.AddField("message", _ChatEntry.Text);
            obj.AddField("name", MainPlayer.instance.GetComponent<PlayerAttributes>().player.name_);
            ChatNetwork.socket.Emit("message", obj);
#else
            InsertChatMessage("here's a string");
#endif
        }

        public static void InsertChatMessage(string message)
        {
            _ChatText.Text += "\r\n" + message;
            _ChatTextContainer.ScrollToEnd();

        }

        private void ScrollChatDown(object sender, MouseButtonEventArgs e)
        {
            this.chatY = e.GetPosition(_ChatCanvas).Y;
            if (_ChatText.CaptureMouse())
            {
                _ChatText.MouseMove += ScrollChatDrag;
                _ChatText.MouseUp += ScrollChatRelease;
            }
        }

        private void ScrollChatDrag(object sender, MouseEventArgs e)
        {
            var offset = chatY - e.GetPosition(_ChatCanvas).Y;
            ;
            _ChatTextContainer.ScrollToVerticalOffset(_ChatTextContainer.VerticalOffset+(offset/4));
        }

        private void ScrollChatRelease(object sender, MouseButtonEventArgs e)
        {
            _ChatText.ReleaseMouseCapture();
            _ChatText.MouseMove -= ScrollChatDrag;
            _ChatText.MouseUp -= ScrollChatRelease;

        }


        private void DragChatWindow(object sender, MouseButtonEventArgs e)
        {
            var _selected = sender as Border;
            if (_selected != null)
            {
                _offset = e.GetPosition(_ChatCanvas);
                if (_ChatDrag.CaptureMouse())
                {
                    _ChatDrag.MouseMove += OnFloatingDrag;
                    _ChatDrag.MouseUp += OnFloatingDrop;
                }
            }
        }

        private void OnFloatingDrag(object sender, MouseEventArgs e)
        {
            Point canvasPos = e.GetPosition(_ChatContainer);
            if (

                canvasPos.Y < Constants.WinHeight * .08f
                )
            {

            }
            else
            {
                Canvas.SetTop(_ChatCanvas, canvasPos.Y - _offset.Y);
            }

        }

        private void OnFloatingDrop(object sender, MouseButtonEventArgs e)
        {
            Point canvasPos = e.GetPosition(_ChatContainer);
            if (canvasPos.Y - _offset.Y > Constants.WinHeight * .75f)
            {
                Canvas.SetTop(_ChatCanvas, Constants.WinHeight * .92f);
            }
            else if (canvasPos.Y < Constants.WinHeight * .1f)
            {
                Canvas.SetTop(_ChatCanvas, Constants.WinHeight * .05f);
            }
            _ChatDrag.ReleaseMouseCapture();
            _ChatDrag.MouseMove -= OnFloatingDrag;
            _ChatDrag.MouseUp -= OnFloatingDrop;

        }

        public static void ToggleVisibility()
        {
            _canvas.Visibility = _canvas.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }


    }


}
