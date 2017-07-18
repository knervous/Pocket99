using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public class UnitySoftwareKeyboard: SoftwareKeyboard
    {
        static bool IsTouchKeyboardSupported = UnityEngine.TouchScreenKeyboard.isSupported;
        public override bool Show(UIElement focusedElement)
        {
            if (focusedElement != null)
            {
                _textBox = focusedElement as Noesis.TextBox;
                if (_textBox != null)
                {
                    _keyboard = ShowOnTextBox(_textBox);
                    return IsTouchKeyboardSupported;
                }

                _passwordBox = focusedElement as Noesis.PasswordBox;
                if (_passwordBox != null)
                {
                    _keyboard = ShowOnPasswordBox(_passwordBox);
                    return IsTouchKeyboardSupported;
                }
            }

            return false;
        }

        /// <summary>
        /// Override if you want to open the software keyboard for a TextBox with your own options.
        /// </summary>
        protected virtual UnityEngine.TouchScreenKeyboard ShowOnTextBox(TextBox textBox)
        {
            return UnityEngine.TouchScreenKeyboard.Open(textBox.Text);
        }

        /// <summary>
        /// Override if you want to open the software keyboard for a PasswordBox with your own options.
        /// </summary>
        protected virtual UnityEngine.TouchScreenKeyboard ShowOnPasswordBox(PasswordBox passwordBox)
        {
            return UnityEngine.TouchScreenKeyboard.Open(passwordBox.Password,
                UnityEngine.TouchScreenKeyboardType.Default, false, false, true);
        }

        public override void Hide()
        {
            if (_keyboard != null)
            {
                // TODO: Force software keyboard to hide

                _textBox = null;
                _passwordBox = null;
                _keyboard = null;
            }
        }

        public override void Update()
        {
            if (_keyboard != null)
            {
                if (_keyboard.active)
                {
                    _isActive = true;

                    if (_textBox != null)
                    {
                        _textBox.Text = _keyboard.text;
                    }
                    else
                    {
                        _passwordBox.Password = _keyboard.text;
                    }
                }

                if (_isActive)
                {
                    if (_keyboard.done || _keyboard.wasCanceled)
                    {
                        _isActive = false;

                        // Remove focus from the UI element
                        if (_textBox != null)
                        {
                            _textBox.Keyboard.Focus(null);
                        }
                        else
                        {
                            _passwordBox.Keyboard.Focus(null);
                        }
                    }
                }
            }
        }

        #region Private members
        protected TextBox _textBox = null;
        protected PasswordBox _passwordBox = null;
        protected UnityEngine.TouchScreenKeyboard _keyboard = null;
        protected bool _isActive = false;
        #endregion
    }
}
