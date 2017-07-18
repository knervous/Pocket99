//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Noesis
{

public class InputBinding : Freezable {
  internal new static InputBinding CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new InputBinding(cPtr, cMemoryOwn);
  }

  internal InputBinding(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(InputBinding obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public ICommand Command {
    get {
      return GetCommandHelper() as ICommand;
    }
    set {
      SetCommandHelper(value);
    }
  }

  public InputBinding() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_InputBinding__SWIG_0();
  }

  public static DependencyProperty CommandProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_CommandProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty CommandParameterProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_CommandParameterProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public static DependencyProperty CommandTargetProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_CommandTargetProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public object CommandParameter {
    set {
      NoesisGUI_PINVOKE.InputBinding_CommandParameter_set(swigCPtr, Noesis.Extend.GetInstanceHandle(value));
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    }
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_CommandParameter_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public UIElement CommandTarget {
    set {
      NoesisGUI_PINVOKE.InputBinding_CommandTarget_set(swigCPtr, UIElement.getCPtr(value));
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_CommandTarget_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (UIElement)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public InputGesture Gesture {
    set {
      NoesisGUI_PINVOKE.InputBinding_Gesture_set(swigCPtr, InputGesture.getCPtr(value));
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_Gesture_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (InputGesture)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  private object GetCommandHelper() {
    IntPtr cPtr = NoesisGUI_PINVOKE.InputBinding_GetCommandHelper(swigCPtr);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return Noesis.Extend.GetProxy(cPtr, false);
  }

  private void SetCommandHelper(object command) {
    NoesisGUI_PINVOKE.InputBinding_SetCommandHelper(swigCPtr, Noesis.Extend.GetInstanceHandle(command));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.InputBinding_GetStaticType();
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
