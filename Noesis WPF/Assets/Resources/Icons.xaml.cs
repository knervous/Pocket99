#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif


namespace UIResources
{
    public partial class Icons : ResourceDictionary
    {
        private const float _scale = .156f;
        public Icons()
        {
            InitializeComponent();
            int count = 500;
            for (int file = 1; file < 35; file++)
            {
                for (int col = 0; col < 6; col++)
                {
                    for (int row = 0; row < 6; row++)
                    {
#if NOESIS
                       // ImageBrush newBrush = new ImageBrush();
                       // newBrush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
                       // newBrush.Stretch = Stretch.Fill;
                       // Texture2D texture = Resources.Load(System.String.Format("Images/icons/dragitem{0}.png", file)) as Texture2D;
                       // TextureSource source = new TextureSource(texture);
                       // newBrush.ImageSource = source;
                       // newBrush.Viewbox = new Rect(col * _scale, row * _scale, _scale, _scale);
                       // this.RegisterName("icon" + (int)(count), newBrush); 
                        //count++;
                        //UnityEngine.Debug.Log("FINISHED LOADING INTO R DICT");
                        
#else
                        ImageBrush newBrush = new ImageBrush();
                        newBrush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
                        newBrush.Stretch = Stretch.Fill;
                        newBrush.ImageSource = new BitmapImage(new Uri(String.Format("../../Assets/Resources/icons/dragitem{0}.png", file), UriKind.RelativeOrAbsolute));
                        newBrush.Viewbox = new Rect(col * _scale, row * _scale, _scale, _scale);
                       // Add("icon" + (int)(count), newBrush);
                        count++;
#endif
                    }
                }
            }

            ;

        }


#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/NoesisGUI/Interface/Resources/Icons.xaml");
        }
#endif
    }

}