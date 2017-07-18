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

namespace Noesis
{

public class Matrix3DProjection : Projection {
  internal new static Matrix3DProjection CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new Matrix3DProjection(cPtr, cMemoryOwn);
  }

  internal Matrix3DProjection(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(Matrix3DProjection obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  public Matrix3DProjection() {
  }

  protected override IntPtr CreateCPtr(Type type, out bool registerExtend) {
    registerExtend = false;
    return NoesisGUI_PINVOKE.new_Matrix3DProjection();
  }

  public override bool IsIdentity() {
    bool ret = NoesisGUI_PINVOKE.Matrix3DProjection_IsIdentity(swigCPtr);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override Matrix4 GetProjection(Size surface, Size size) {
    IntPtr ret = NoesisGUI_PINVOKE.Matrix3DProjection_GetProjection(swigCPtr, ref surface, ref size);
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    if (ret != IntPtr.Zero) {
      return Marshal.PtrToStructure<Matrix4>(ret);
    }
    else {
      return new Matrix4();
    }
  }

  public static DependencyProperty ProjectionMatrixProperty {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.Matrix3DProjection_ProjectionMatrixProperty_get();
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      return (DependencyProperty)Noesis.Extend.GetProxy(cPtr, false);
    }
  }

  public Matrix4 ProjectionMatrix {
    set {
      NoesisGUI_PINVOKE.Matrix3DProjection_ProjectionMatrix_set(swigCPtr, ref value);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    }

    get {
      IntPtr ret = NoesisGUI_PINVOKE.Matrix3DProjection_ProjectionMatrix_get(swigCPtr);
      if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<Matrix4>(ret);
      }
      else {
        return new Matrix4();
      }
    }

  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.Matrix3DProjection_GetStaticType();
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
