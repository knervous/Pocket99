#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Assets.Scripts.Data_Models;
using Noesis;
using UnityEngine;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
#endif


using System.Collections.Generic;


namespace UserInterface
{
    public partial class Target : UserControl
    {
        static Canvas _background;
        static Border _healthBackground;
        static Border _healthBar;
        static Border _healthTicks;
        static TextBlock _targetName;


        public Target()
        {
            Initialized += OnInitialized;
            InitializeComponent();
        }
#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "Assets/NoesisGUI/Interface/TopBarUI/HealthManaBar.xaml");
        }
#endif
        private void OnInitialized(object sender, EventArgs e)
        {
            _background = (Canvas)FindName("Background");
            _healthBackground = (Border)FindName("HealthBackground");
            _healthBar = (Border)FindName("HealthBar");
            _healthTicks = (Border)FindName("HealthTicks");

            _targetName = (TextBlock)FindName("TargetName");


            _background.Width = Constants.WinHyp * .16f;
            _background.Height = Constants.WinHyp * .08f;

            _firstName.Width = _background.Width;
            _firstName.Height = _background.Height / 4f;
            Canvas.SetLeft(_firstName, _background.Width * .05f);
            Canvas.SetTop(_firstName, _background.Height * .1f);
            _firstName.FontSize = Constants.WinHyp * .014f;

            _lastName.Width = _background.Width;
            _lastName.Height = _background.Height / 4;
            Canvas.SetLeft(_lastName, _background.Width * .05);
            Canvas.SetTop(_lastName, _background.Height * .3);
            _lastName.FontSize = Constants.WinHyp * .014f;

            _healthBackground.Width = _background.Width * .82f;
            _healthBackground.Height = _background.Height * .15f;
            Canvas.SetLeft(_healthBackground, _background.Width * .08f);
            Canvas.SetTop(_healthBackground, _background.Height * .55f);

            _healthBar.Width = _background.Width * .52f;
            _healthBar.Height = _background.Height * .15f;
            Canvas.SetLeft(_healthBar, _background.Width * .08f);
            Canvas.SetTop(_healthBar, _background.Height * .55f);

            _healthTicks.Width = _background.Width * .82f;
            _healthTicks.Height = _background.Height * .15f;
            Canvas.SetLeft(_healthTicks, _background.Width * .08f);
            Canvas.SetTop(_healthTicks, _background.Height * .55f);

            _manaBackground.Width = _background.Width * .82f;
            _manaBackground.Height = _background.Height * .15f;
            Canvas.SetLeft(_manaBackground, _background.Width * .08f);
            Canvas.SetTop(_manaBackground, _background.Height * .75f);

            _manaBar.Width = _background.Width * .62f;
            _manaBar.Height = _background.Height * .15f;
            Canvas.SetLeft(_manaBar, _background.Width * .08f);
            Canvas.SetTop(_manaBar, _background.Height * .75f);

            _manaTicks.Width = _background.Width * .82f;
            _manaTicks.Height = _background.Height * .15f;
            Canvas.SetLeft(_manaTicks, _background.Width * .08f);
            Canvas.SetTop(_manaTicks, _background.Height * .75f);
        }

    }
}