internal class NoesisKeyCodes
{
    private static System.Collections.Generic.Dictionary<UnityEngine.KeyCode, Noesis.Key> _unityToNoesis;
    
    static NoesisKeyCodes()
    {
        _unityToNoesis = new System.Collections.Generic.Dictionary<UnityEngine.KeyCode, Noesis.Key>();

        // https://docs.unity3d.com/ScriptReference/KeyCode.html
        _unityToNoesis.Add(UnityEngine.KeyCode.None, Noesis.Key.None);

        _unityToNoesis.Add(UnityEngine.KeyCode.Backspace, Noesis.Key.Back);
        _unityToNoesis.Add(UnityEngine.KeyCode.Delete, Noesis.Key.Delete);
        _unityToNoesis.Add(UnityEngine.KeyCode.Tab, Noesis.Key.Tab);
        _unityToNoesis.Add(UnityEngine.KeyCode.Clear, Noesis.Key.Clear);
        _unityToNoesis.Add(UnityEngine.KeyCode.Return, Noesis.Key.Return);
        _unityToNoesis.Add(UnityEngine.KeyCode.Pause, Noesis.Key.Pause);
        _unityToNoesis.Add(UnityEngine.KeyCode.Escape, Noesis.Key.Escape);
        _unityToNoesis.Add(UnityEngine.KeyCode.Space, Noesis.Key.Space);

        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad0, Noesis.Key.D0);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad1, Noesis.Key.D1);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad2, Noesis.Key.D2);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad3, Noesis.Key.D3);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad4, Noesis.Key.D4);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad5, Noesis.Key.D5);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad6, Noesis.Key.D6);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad7, Noesis.Key.D7);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad8, Noesis.Key.D8);
        _unityToNoesis.Add(UnityEngine.KeyCode.Keypad9, Noesis.Key.D9);
        _unityToNoesis.Add(UnityEngine.KeyCode.KeypadPeriod, Noesis.Key.Decimal);
        _unityToNoesis.Add(UnityEngine.KeyCode.KeypadDivide, Noesis.Key.Divide);
        _unityToNoesis.Add(UnityEngine.KeyCode.KeypadMultiply, Noesis.Key.Multiply);
        _unityToNoesis.Add(UnityEngine.KeyCode.KeypadMinus, Noesis.Key.Subtract);
        _unityToNoesis.Add(UnityEngine.KeyCode.KeypadPlus, Noesis.Key.Add);
        _unityToNoesis.Add(UnityEngine.KeyCode.KeypadEnter, Noesis.Key.Return);
        _unityToNoesis.Add(UnityEngine.KeyCode.Equals, Noesis.Key.OemPlus);

        _unityToNoesis.Add(UnityEngine.KeyCode.UpArrow, Noesis.Key.Up);
        _unityToNoesis.Add(UnityEngine.KeyCode.DownArrow, Noesis.Key.Down);
        _unityToNoesis.Add(UnityEngine.KeyCode.RightArrow, Noesis.Key.Right);
        _unityToNoesis.Add(UnityEngine.KeyCode.LeftArrow, Noesis.Key.Left);

        _unityToNoesis.Add(UnityEngine.KeyCode.Insert, Noesis.Key.Insert);
        _unityToNoesis.Add(UnityEngine.KeyCode.Home, Noesis.Key.Home);
        _unityToNoesis.Add(UnityEngine.KeyCode.End, Noesis.Key.End);
        _unityToNoesis.Add(UnityEngine.KeyCode.PageUp, Noesis.Key.PageUp);
        _unityToNoesis.Add(UnityEngine.KeyCode.PageDown, Noesis.Key.PageDown);

        _unityToNoesis.Add(UnityEngine.KeyCode.F1, Noesis.Key.F1);
        _unityToNoesis.Add(UnityEngine.KeyCode.F2, Noesis.Key.F2);
        _unityToNoesis.Add(UnityEngine.KeyCode.F3, Noesis.Key.F3);
        _unityToNoesis.Add(UnityEngine.KeyCode.F4, Noesis.Key.F4);
        _unityToNoesis.Add(UnityEngine.KeyCode.F5, Noesis.Key.F5);
        _unityToNoesis.Add(UnityEngine.KeyCode.F6, Noesis.Key.F6);
        _unityToNoesis.Add(UnityEngine.KeyCode.F7, Noesis.Key.F7);
        _unityToNoesis.Add(UnityEngine.KeyCode.F8, Noesis.Key.F8);
        _unityToNoesis.Add(UnityEngine.KeyCode.F9, Noesis.Key.F9);
        _unityToNoesis.Add(UnityEngine.KeyCode.F10, Noesis.Key.F10);
        _unityToNoesis.Add(UnityEngine.KeyCode.F11, Noesis.Key.F11);
        _unityToNoesis.Add(UnityEngine.KeyCode.F12, Noesis.Key.F12);
        _unityToNoesis.Add(UnityEngine.KeyCode.F13, Noesis.Key.F13);
        _unityToNoesis.Add(UnityEngine.KeyCode.F14, Noesis.Key.F14);
        _unityToNoesis.Add(UnityEngine.KeyCode.F15, Noesis.Key.F15);


        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha0, Noesis.Key.NumPad0);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha1, Noesis.Key.NumPad1);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha2, Noesis.Key.NumPad2);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha3, Noesis.Key.NumPad3);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha4, Noesis.Key.NumPad4);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha5, Noesis.Key.NumPad5);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha6, Noesis.Key.NumPad6);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha7, Noesis.Key.NumPad7);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha8, Noesis.Key.NumPad8);
        _unityToNoesis.Add(UnityEngine.KeyCode.Alpha9, Noesis.Key.NumPad9);

        // UnityEngine.KeyCode.Exclaim
        // UnityEngine.KeyCode.DoubleQuote
        // UnityEngine.KeyCode.Hash
        // UnityEngine.KeyCode.Dollar
        // UnityEngine.KeyCode.Ampersand
        _unityToNoesis.Add(UnityEngine.KeyCode.Quote, Noesis.Key.OemQuotes);
        // UnityEngine.KeyCode.LeftParen
        // UnityEngine.KeyCode.RightParen
        // UnityEngine.KeyCode.Asterisk

        _unityToNoesis.Add(UnityEngine.KeyCode.Plus, Noesis.Key.OemPlus);
        _unityToNoesis.Add(UnityEngine.KeyCode.Comma, Noesis.Key.OemComma);
        _unityToNoesis.Add(UnityEngine.KeyCode.Minus, Noesis.Key.OemMinus);
        _unityToNoesis.Add(UnityEngine.KeyCode.Period, Noesis.Key.OemPeriod);
        _unityToNoesis.Add(UnityEngine.KeyCode.Slash, Noesis.Key.OemQuestion);

        // UnityEngine.KeyCode.Colon
        _unityToNoesis.Add(UnityEngine.KeyCode.Semicolon, Noesis.Key.OemSemicolon);

        // UnityEngine.KeyCode.Less
        // UnityEngine.KeyCode.Equals
        // UnityEngine.KeyCode.Greater
        // UnityEngine.KeyCode.Question
        // UnityEngine.KeyCode.At
        _unityToNoesis.Add(UnityEngine.KeyCode.LeftBracket, Noesis.Key.OemOpenBrackets);
        _unityToNoesis.Add(UnityEngine.KeyCode.Backslash, Noesis.Key.OemBackslash);
        _unityToNoesis.Add(UnityEngine.KeyCode.RightBracket, Noesis.Key.OemCloseBrackets);
        // UnityEngine.KeyCode.Caret
        // UnityEngine.KeyCode.Underscore
        // UnityEngine.KeyCode.Backquote

        _unityToNoesis.Add(UnityEngine.KeyCode.A, Noesis.Key.A);
        _unityToNoesis.Add(UnityEngine.KeyCode.B, Noesis.Key.B);
        _unityToNoesis.Add(UnityEngine.KeyCode.C, Noesis.Key.C);
        _unityToNoesis.Add(UnityEngine.KeyCode.D, Noesis.Key.D);
        _unityToNoesis.Add(UnityEngine.KeyCode.E, Noesis.Key.E);
        _unityToNoesis.Add(UnityEngine.KeyCode.F, Noesis.Key.F);
        _unityToNoesis.Add(UnityEngine.KeyCode.G, Noesis.Key.G);
        _unityToNoesis.Add(UnityEngine.KeyCode.H, Noesis.Key.H);
        _unityToNoesis.Add(UnityEngine.KeyCode.I, Noesis.Key.I);
        _unityToNoesis.Add(UnityEngine.KeyCode.J, Noesis.Key.J);
        _unityToNoesis.Add(UnityEngine.KeyCode.K, Noesis.Key.K);
        _unityToNoesis.Add(UnityEngine.KeyCode.L, Noesis.Key.L);
        _unityToNoesis.Add(UnityEngine.KeyCode.M, Noesis.Key.M);
        _unityToNoesis.Add(UnityEngine.KeyCode.N, Noesis.Key.N);
        _unityToNoesis.Add(UnityEngine.KeyCode.O, Noesis.Key.O);
        _unityToNoesis.Add(UnityEngine.KeyCode.P, Noesis.Key.P);
        _unityToNoesis.Add(UnityEngine.KeyCode.Q, Noesis.Key.Q);
        _unityToNoesis.Add(UnityEngine.KeyCode.R, Noesis.Key.R);
        _unityToNoesis.Add(UnityEngine.KeyCode.S, Noesis.Key.S);
        _unityToNoesis.Add(UnityEngine.KeyCode.T, Noesis.Key.T);
        _unityToNoesis.Add(UnityEngine.KeyCode.U, Noesis.Key.U);
        _unityToNoesis.Add(UnityEngine.KeyCode.V, Noesis.Key.V);
        _unityToNoesis.Add(UnityEngine.KeyCode.W, Noesis.Key.W);
        _unityToNoesis.Add(UnityEngine.KeyCode.X, Noesis.Key.X);
        _unityToNoesis.Add(UnityEngine.KeyCode.Y, Noesis.Key.Y);
        _unityToNoesis.Add(UnityEngine.KeyCode.Z, Noesis.Key.Z);

        _unityToNoesis.Add(UnityEngine.KeyCode.Numlock, Noesis.Key.NumLock);
        _unityToNoesis.Add(UnityEngine.KeyCode.CapsLock, Noesis.Key.CapsLock);
        _unityToNoesis.Add(UnityEngine.KeyCode.ScrollLock, Noesis.Key.Scroll);
        _unityToNoesis.Add(UnityEngine.KeyCode.RightShift, Noesis.Key.RightShift);
        _unityToNoesis.Add(UnityEngine.KeyCode.LeftShift, Noesis.Key.LeftShift);
        _unityToNoesis.Add(UnityEngine.KeyCode.RightControl, Noesis.Key.RightCtrl);
        _unityToNoesis.Add(UnityEngine.KeyCode.LeftControl, Noesis.Key.LeftCtrl);
        _unityToNoesis.Add(UnityEngine.KeyCode.RightAlt, Noesis.Key.RightAlt);
        _unityToNoesis.Add(UnityEngine.KeyCode.LeftAlt, Noesis.Key.LeftAlt);
        // UnityEngine.KeyCode.LeftCommand
        // UnityEngine.KeyCode.LeftApple
        _unityToNoesis.Add(UnityEngine.KeyCode.LeftWindows, Noesis.Key.LWin);
        // UnityEngine.KeyCode.RightCommand
        // UnityEngine.KeyCode.RightApple
        _unityToNoesis.Add(UnityEngine.KeyCode.RightWindows, Noesis.Key.RWin);
        // UnityEngine.KeyCode.AltGr
        _unityToNoesis.Add(UnityEngine.KeyCode.Help, Noesis.Key.Help);
        _unityToNoesis.Add(UnityEngine.KeyCode.Print, Noesis.Key.Print);
        // UnityEngine.KeyCode.SysReq
        _unityToNoesis.Add(UnityEngine.KeyCode.Break, Noesis.Key.Pause);
        // UnityEngine.KeyCode.Menu
    }

    public static Noesis.Key Convert(UnityEngine.KeyCode key)
    {
        Noesis.Key noesisKey = Noesis.Key.None;
        _unityToNoesis.TryGetValue(key, out noesisKey);
        return noesisKey;
    }

#if UNITY_STANDALONE_LINUX
    public static char KeySymToUnicode(char ch)
    {
        // http://www.cl.cam.ac.uk/~mgk25/ucs/keysyms.txt
        if (ch >= '\ufd01' && ch <= '\ufefd') return '\u0000';
        if (ch >= '\uff20' && ch <= '\uff7f') return '\u0000';
        if (ch >= '\uff91' && ch <= '\uff9f') return '\u0000';
        if (ch >= '\uffbe' && ch <= '\uffff') return '\u0000';

        switch (ch)
        {
            case '\uff08': return '\u0008'; // BackSpace : back space, back char
            case '\uff09': return '\u0009'; // Tab
            case '\uff0a': return '\u000a'; // Linefeed : Linefeed, LF
            case '\uff0b': return '\u000b'; // Clear
            case '\uff0d': return '\u000d'; // Return : Return, enter
            case '\uff13': return '\u0013'; // Pause /* Pause, hold */
            case '\uff14': return '\u0014'; // Scroll_Lock
            case '\uff15': return '\u0015'; // Sys_Req
            case '\uff1b': return '\u001b'; // Escape
            case '\uff80': return '\u0020'; // Space
            case '\uff89': return '\u0009'; // Tab
            case '\uff8d': return '\u000d'; // Return : Return, enter
            case '\uffaa': return '\u002a'; // KP_Multiply
            case '\uffab': return '\u002b'; // KP_Add
            case '\uffac': return '\u002c'; // KP_Separator
            case '\uffad': return '\u002d'; // KP_Subtract
            case '\uffae': return '\u002e'; // KP_Decimal
            case '\uffaf': return '\u002f'; // KP_Divide
            case '\uffb0': return '\u0030'; // KP_0
            case '\uffb1': return '\u0031'; // KP_1
            case '\uffb2': return '\u0032'; // KP_2
            case '\uffb3': return '\u0033'; // KP_3
            case '\uffb4': return '\u0034'; // KP_4
            case '\uffb5': return '\u0035'; // KP_5
            case '\uffb6': return '\u0036'; // KP_6
            case '\uffb7': return '\u0037'; // KP_7
            case '\uffb8': return '\u0038'; // KP_8
            case '\uffb9': return '\u0039'; // KP_9
            case '\uffbd': return '\u003d'; // KP_Equal
            default: return ch;
        }
    }
#endif
}