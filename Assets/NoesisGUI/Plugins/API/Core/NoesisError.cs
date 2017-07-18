using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public class Error
    {
        public static void RegisterCallback()
        {
            Noesis_RegisterErrorCallback(_errorCallback);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private class NoesisException: Exception
        {
            public NoesisException(string message): base(message) {}
        }

        public static void Check()
        {
            if (Pending) throw Retrieve();
        }

        private static bool Pending
        {
            get { return _pendingError.Length > 0; }
        }

        private static Exception Retrieve()
        {
            string message = _pendingError;
            _pendingError = "";
            return new NoesisException(message);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void SetNativePendingError(System.Exception exception)
        {
#if UNITY_5_3_OR_NEWER
            UnityEngine.Debug.LogException(exception);
#else
            System.Diagnostics.Debug.WriteLine(exception);
#endif
            Noesis_CppSetPendingError(exception.Message);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate void ErrorCallback([MarshalAs(UnmanagedType.LPWStr)]string desc);
        private static ErrorCallback _errorCallback = SetPendingError;

        [MonoPInvokeCallback(typeof(ErrorCallback))]
        private static void SetPendingError(string desc)
        {
            // Do not overwrite if there is already an exception pending
            if (_pendingError.Length == 0)
            {
                _pendingError = desc;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private static string _pendingError = "";

        ////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport(Library.Name)]
        private static extern void Noesis_RegisterErrorCallback(ErrorCallback errorCallback);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport(Library.Name)]
        private static extern void Noesis_CppSetPendingError([MarshalAs(UnmanagedType.LPWStr)]string message);
    }
}

