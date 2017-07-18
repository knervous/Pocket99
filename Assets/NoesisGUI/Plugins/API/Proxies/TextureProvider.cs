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

public class TextureProvider : BaseComponent {
  internal new static TextureProvider CreateProxy(IntPtr cPtr, bool cMemoryOwn) {
    return new TextureProvider(cPtr, cMemoryOwn);
  }

  internal TextureProvider(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
  }

  internal static HandleRef getCPtr(TextureProvider obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  protected TextureProvider() {
  }

  /// <summary>
  /// Returns metadata for the given texture filename. Returns 0x0 if no texture is found.
  /// </summary>
  /// <param name="filename">Path to the texture.</param>
  public virtual void GetTextureInfo(string filename, ref uint width, ref uint height) {
  }

  /// <summary>
  /// Creates texture in the given device. Returns null if no texture is found.
  /// </summary>
  /// <param name="filename">Path to the texture being loaded.</param>
  public virtual Texture LoadTexture(string filename/*, RenderDevice device*/) {
    return null;
  }

  new internal static IntPtr GetStaticType() {
    IntPtr ret = NoesisGUI_PINVOKE.TextureProvider_GetStaticType();
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }


  internal new static IntPtr Extend(string typeName) {
    IntPtr nativeType = NoesisGUI_PINVOKE.Extend_TextureProvider(Marshal.StringToHGlobalAnsi(typeName));
    if (NoesisGUI_PINVOKE.SWIGPendingException.Pending) throw NoesisGUI_PINVOKE.SWIGPendingException.Retrieve();
    return nativeType;
  }
}

}

