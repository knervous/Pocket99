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
        ScrollViewer _ChatTextScroll;
        TextBlock _ChatText;
        Button _ChatSend;
        TextBlock _ChatSendText;
        TextBox _ChatEntry;


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
            _ChatCanvas = FindName("ChatCanvas") as Canvas;
            _ChatHeader = FindName("ChatHeader") as Border;
            _ChatDrag = FindName("ChatDrag") as Border;
            _ChatTextScroll = FindName("ChatTextScroll") as ScrollViewer;
            _ChatText = FindName("ChatText") as TextBlock;
            _ChatEntry = FindName("ChatEntry") as TextBox;
            _ChatSend = FindName("ChatSend") as Button;
            _ChatSendText = FindName("ChatSendText") as TextBlock;
            _canvas = (Canvas)FindName("ChatCanvas");
            _ChatContainer.Width = Constants.WinWidth;
            _ChatContainer.Height = Constants.WinHeight;
            Canvas.SetTop(_ChatCanvas, Constants.WinHeight * .5f);
            _ChatCanvas.Width = Constants.WinWidth;
            _ChatCanvas.Height = Constants.WinHeight * .75f;
            _ChatCanvasInner.Height = Constants.WinHeight * .75f;
            _ChatCanvasInner.Width = Constants.WinWidth;
            Canvas.SetTop(_ChatCanvasInner, Constants.WinHeight * .1f);
            _ChatHeader.Width = Constants.WinWidth * .85f;
            _ChatHeader.Height = Constants.WinHeight * .066f;
            Canvas.SetLeft(_ChatHeader, Constants.WinWidth * .075f);
            Canvas.SetTop(_ChatHeader, Constants.WinHeight * .075f);
            _ChatDrag.Width = Constants.WinHyp * .075f;
            _ChatDrag.Height = Constants.WinHyp * .04f;
            Canvas.SetTop(_ChatDrag, 0);

            _ChatTextScroll.Width = Constants.WinWidth * .95f;
            _ChatTextScroll.Height = Constants.WinHeight * .55f;

            Canvas.SetLeft(_ChatTextScroll, Constants.WinWidth * .025f);
            Canvas.SetTop(_ChatTextScroll, Constants.WinHeight * .075f);

            _ChatText.Width = Constants.WinWidth * .95f;
            //ChatText.Height = Constants.WinHeight * .55;
            _ChatText.FontSize = _ChatSendText.FontSize = Constants.WinHyp * .015f;

            Canvas.SetLeft(_ChatText, Constants.WinWidth * .025f);
            Canvas.SetTop(_ChatText, Constants.WinHeight * .075f);
            for (int x = 0; x < 30; x++)
            {
                _ChatText.Text += "\r\n Hi";
            }

            _ChatEntry.Width = Constants.WinWidth * .6f;
            _ChatEntry.Height = Constants.WinHeight * .07f;
            _ChatEntry.FontSize = Constants.WinHyp * .015f;
            _ChatSendText.FontSize = Constants.WinHyp * .015f;
          
            Canvas.SetTop(_ChatEntry, Constants.WinHeight * .65f);
            Canvas.SetLeft(_ChatEntry, Constants.WinWidth * .2f);

            _ChatSend.Width = Constants.WinWidth * .06f;
            _ChatSend.Height = Constants.WinHeight * .065f;
            Canvas.SetTop(_ChatSend, Constants.WinHeight * .65f);
            Canvas.SetLeft(_ChatSend, Constants.WinWidth * .8f);

            _ChatDrag.MouseDown += DragChatWindow;
            
        }

        private void DragChatWindow(object sender, MouseButtonEventArgs e)
        {
            var _selected = sender as Border;
            if (_selected != null)
            {
                _offset = e.GetPosition(_selected);
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
            if(canvasPos.Y - _offset.Y > Constants.WinHeight * .92f ||
                canvasPos.Y - _offset.Y < Constants.WinHeight * .15f)
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
            }else if (canvasPos.Y - _offset.Y < Constants.WinHeight * .3f)
            {
                Canvas.SetTop(_ChatCanvas, Constants.WinHeight * .15f);
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
