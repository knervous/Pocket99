using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Globalization;
using System.Linq;

namespace Noesis
{
    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Manages Noesis Extensibility
    ////////////////////////////////////////////////////////////////////////////////////////////////
    internal partial class Extend
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private delegate IntPtr BoxDelegate(object val);
        private static Dictionary<RuntimeTypeHandle, BoxDelegate> _boxFunctions = CreateBoxFunctions();

        private static Dictionary<RuntimeTypeHandle, BoxDelegate> CreateBoxFunctions()
        {
            Dictionary<RuntimeTypeHandle, BoxDelegate> boxFunctions = new Dictionary<RuntimeTypeHandle, BoxDelegate>(100);

            boxFunctions[typeof(string).TypeHandle] = (val) => NoesisGUI_.Box_String((string)val);
            boxFunctions[typeof(bool).TypeHandle] = (val) => NoesisGUI_.Box_Bool((bool)val);
            boxFunctions[typeof(float).TypeHandle] = (val) => NoesisGUI_.Box_Float((float)val);
            boxFunctions[typeof(double).TypeHandle] = (val) => NoesisGUI_.Box_Double((double)val);
            boxFunctions[typeof(decimal).TypeHandle] = (val) => NoesisGUI_.Box_Double((double)(decimal)val);
            boxFunctions[typeof(int).TypeHandle] = (val) => NoesisGUI_.Box_Int((int)val);
            boxFunctions[typeof(long).TypeHandle] = (val) => NoesisGUI_.Box_Int((int)(long)val);
            boxFunctions[typeof(uint).TypeHandle] = (val) => NoesisGUI_.Box_UInt((uint)val);
            boxFunctions[typeof(ulong).TypeHandle] = (val) => NoesisGUI_.Box_UInt((uint)(ulong)val);
            boxFunctions[typeof(char).TypeHandle] = (val) => NoesisGUI_.Box_UInt((uint)(char)val);
            boxFunctions[typeof(short).TypeHandle] = (val) => NoesisGUI_.Box_Short((short)val);
            boxFunctions[typeof(sbyte).TypeHandle] = (val) => NoesisGUI_.Box_Short((short)(sbyte)val);
            boxFunctions[typeof(ushort).TypeHandle] = (val) => NoesisGUI_.Box_UShort((ushort)val);
            boxFunctions[typeof(byte).TypeHandle] = (val) => NoesisGUI_.Box_UShort((ushort)(byte)val);
            boxFunctions[typeof(Noesis.Color).TypeHandle] = (val) => NoesisGUI_.Box_Color((Noesis.Color)val);
            boxFunctions[typeof(Noesis.Point).TypeHandle] = (val) => NoesisGUI_.Box_Point((Noesis.Point)val);
            boxFunctions[typeof(Noesis.Rect).TypeHandle] = (val) => NoesisGUI_.Box_Rect((Noesis.Rect)val);
            boxFunctions[typeof(Noesis.Size).TypeHandle] = (val) => NoesisGUI_.Box_Size((Noesis.Size)val);
            boxFunctions[typeof(Noesis.Thickness).TypeHandle] = (val) => NoesisGUI_.Box_Thickness((Noesis.Thickness)val);
            boxFunctions[typeof(Noesis.CornerRadius).TypeHandle] = (val) => NoesisGUI_.Box_CornerRadius((Noesis.CornerRadius)val);
            boxFunctions[typeof(Noesis.Duration).TypeHandle] = (val) => NoesisGUI_.Box_Duration((Noesis.Duration)val);
            boxFunctions[typeof(Noesis.KeyTime).TypeHandle] = (val) => NoesisGUI_.Box_KeyTime((Noesis.KeyTime)val);
            boxFunctions[typeof(System.TimeSpan).TypeHandle] = (val) => NoesisGUI_.Box_TimeSpan((System.TimeSpan)val);
            boxFunctions[typeof(Noesis.VirtualizationCacheLength).TypeHandle] = (val) => NoesisGUI_.Box_VirtualizationCacheLength((Noesis.VirtualizationCacheLength)val);
            boxFunctions[typeof(bool?).TypeHandle] = (val) => NoesisGUI_.Box_NullableBool((bool?)val);
            boxFunctions[typeof(float?).TypeHandle] = (val) => NoesisGUI_.Box_NullableFloat((float?)val);
            boxFunctions[typeof(double?).TypeHandle] = (val) => NoesisGUI_.Box_NullableDouble((double?)val);
            boxFunctions[typeof(decimal?).TypeHandle] = (val) => NoesisGUI_.Box_NullableDouble((double?)(decimal?)val);
            boxFunctions[typeof(int?).TypeHandle] = (val) => NoesisGUI_.Box_NullableInt32((int?)val);
            boxFunctions[typeof(long?).TypeHandle] = (val) => NoesisGUI_.Box_NullableInt32((int?)(long?)val);
            boxFunctions[typeof(uint?).TypeHandle] = (val) => NoesisGUI_.Box_NullableUInt32((uint?)val);
            boxFunctions[typeof(ulong?).TypeHandle] = (val) => NoesisGUI_.Box_NullableUInt32((uint?)(ulong?)val);
            boxFunctions[typeof(char?).TypeHandle] = (val) => NoesisGUI_.Box_NullableUInt32((uint?)(char?)val);
            boxFunctions[typeof(short?).TypeHandle] = (val) => NoesisGUI_.Box_NullableInt16((short?)val);
            boxFunctions[typeof(sbyte?).TypeHandle] = (val) => NoesisGUI_.Box_NullableInt16((short?)(sbyte?)val);
            boxFunctions[typeof(ushort?).TypeHandle] = (val) => NoesisGUI_.Box_NullableUInt16((ushort?)val);
            boxFunctions[typeof(byte?).TypeHandle] = (val) => NoesisGUI_.Box_NullableUInt16((ushort?)(byte?)val);
            boxFunctions[typeof(Noesis.Color?).TypeHandle] = (val) => NoesisGUI_.Box_NullableColor((Noesis.Color?)val);
            boxFunctions[typeof(Noesis.Point?).TypeHandle] = (val) => NoesisGUI_.Box_NullablePoint((Noesis.Point?)val);
            boxFunctions[typeof(Noesis.Rect?).TypeHandle] = (val) => NoesisGUI_.Box_NullableRect((Noesis.Rect?)val);
            boxFunctions[typeof(Noesis.Size?).TypeHandle] = (val) => NoesisGUI_.Box_NullableSize((Noesis.Size?)val);
            boxFunctions[typeof(Noesis.Thickness?).TypeHandle] = (val) => NoesisGUI_.Box_NullableThickness((Noesis.Thickness?)val);
            boxFunctions[typeof(Noesis.CornerRadius?).TypeHandle] = (val) => NoesisGUI_.Box_NullableCornerRadius((Noesis.CornerRadius?)val);
            boxFunctions[typeof(Noesis.Duration?).TypeHandle] = (val) => NoesisGUI_.Box_NullableDuration((Noesis.Duration?)val);
            boxFunctions[typeof(Noesis.KeyTime?).TypeHandle] = (val) => NoesisGUI_.Box_NullableKeyTime((Noesis.KeyTime?)val);
            boxFunctions[typeof(System.TimeSpan?).TypeHandle] = (val) => NoesisGUI_.Box_NullableTimeSpan((System.TimeSpan?)val);
            boxFunctions[typeof(Noesis.AlignmentX).TypeHandle] = (val) => NoesisGUI_.Box_AlignmentX((Noesis.AlignmentX)val);
            boxFunctions[typeof(Noesis.AlignmentY).TypeHandle] = (val) => NoesisGUI_.Box_AlignmentY((Noesis.AlignmentY)val);
            boxFunctions[typeof(Noesis.AutoToolTipPlacement).TypeHandle] = (val) => NoesisGUI_.Box_AutoToolTipPlacement((Noesis.AutoToolTipPlacement)val);
            boxFunctions[typeof(Noesis.BindingMode).TypeHandle] = (val) => NoesisGUI_.Box_BindingMode((Noesis.BindingMode)val);
            boxFunctions[typeof(Noesis.BitmapScalingMode).TypeHandle] = (val) => NoesisGUI_.Box_BitmapScalingMode((Noesis.BitmapScalingMode)val);
            boxFunctions[typeof(Noesis.BrushMappingMode).TypeHandle] = (val) => NoesisGUI_.Box_BrushMappingMode((Noesis.BrushMappingMode)val);
            boxFunctions[typeof(Noesis.CharacterCasing).TypeHandle] = (val) => NoesisGUI_.Box_CharacterCasing((Noesis.CharacterCasing)val);
            boxFunctions[typeof(Noesis.ClickMode).TypeHandle] = (val) => NoesisGUI_.Box_ClickMode((Noesis.ClickMode)val);
            boxFunctions[typeof(Noesis.ColorInterpolationMode).TypeHandle] = (val) => NoesisGUI_.Box_ColorInterpolationMode((Noesis.ColorInterpolationMode)val);
            boxFunctions[typeof(Noesis.Dock).TypeHandle] = (val) => NoesisGUI_.Box_Dock((Noesis.Dock)val);
            boxFunctions[typeof(Noesis.ExpandDirection).TypeHandle] = (val) => NoesisGUI_.Box_ExpandDirection((Noesis.ExpandDirection)val);
            boxFunctions[typeof(Noesis.FillRule).TypeHandle] = (val) => NoesisGUI_.Box_FillRule((Noesis.FillRule)val);
            boxFunctions[typeof(Noesis.FlowDirection).TypeHandle] = (val) => NoesisGUI_.Box_FlowDirection((Noesis.FlowDirection)val);
            boxFunctions[typeof(Noesis.FontStretch).TypeHandle] = (val) => NoesisGUI_.Box_FontStretch((Noesis.FontStretch)val);
            boxFunctions[typeof(Noesis.FontStyle).TypeHandle] = (val) => NoesisGUI_.Box_FontStyle((Noesis.FontStyle)val);
            boxFunctions[typeof(Noesis.FontWeight).TypeHandle] = (val) => NoesisGUI_.Box_FontWeight((Noesis.FontWeight)val);
            boxFunctions[typeof(Noesis.GeometryCombineMode).TypeHandle] = (val) => NoesisGUI_.Box_GeometryCombineMode((Noesis.GeometryCombineMode)val);
            boxFunctions[typeof(Noesis.GradientSpreadMethod).TypeHandle] = (val) => NoesisGUI_.Box_GradientSpreadMethod((Noesis.GradientSpreadMethod)val);
            boxFunctions[typeof(Noesis.HorizontalAlignment).TypeHandle] = (val) => NoesisGUI_.Box_HorizontalAlignment((Noesis.HorizontalAlignment)val);
            boxFunctions[typeof(Noesis.KeyboardNavigationMode).TypeHandle] = (val) => NoesisGUI_.Box_KeyboardNavigationMode((Noesis.KeyboardNavigationMode)val);
            boxFunctions[typeof(Noesis.LineStackingStrategy).TypeHandle] = (val) => NoesisGUI_.Box_LineStackingStrategy((Noesis.LineStackingStrategy)val);
            boxFunctions[typeof(Noesis.ListSortDirection).TypeHandle] = (val) => NoesisGUI_.Box_ListSortDirection((Noesis.ListSortDirection)val);
            boxFunctions[typeof(Noesis.MenuItemRole).TypeHandle] = (val) => NoesisGUI_.Box_MenuItemRole((Noesis.MenuItemRole)val);
            boxFunctions[typeof(Noesis.Orientation).TypeHandle] = (val) => NoesisGUI_.Box_Orientation((Noesis.Orientation)val);
            boxFunctions[typeof(Noesis.OverflowMode).TypeHandle] = (val) => NoesisGUI_.Box_OverflowMode((Noesis.OverflowMode)val);
            boxFunctions[typeof(Noesis.PenLineCap).TypeHandle] = (val) => NoesisGUI_.Box_PenLineCap((Noesis.PenLineCap)val);
            boxFunctions[typeof(Noesis.PenLineJoin).TypeHandle] = (val) => NoesisGUI_.Box_PenLineJoin((Noesis.PenLineJoin)val);
            boxFunctions[typeof(Noesis.PlacementMode).TypeHandle] = (val) => NoesisGUI_.Box_PlacementMode((Noesis.PlacementMode)val);
            boxFunctions[typeof(Noesis.PopupAnimation).TypeHandle] = (val) => NoesisGUI_.Box_PopupAnimation((Noesis.PopupAnimation)val);
            boxFunctions[typeof(Noesis.RelativeSourceMode).TypeHandle] = (val) => NoesisGUI_.Box_RelativeSourceMode((Noesis.RelativeSourceMode)val);
            boxFunctions[typeof(Noesis.SelectionMode).TypeHandle] = (val) => NoesisGUI_.Box_SelectionMode((Noesis.SelectionMode)val);
            boxFunctions[typeof(Noesis.CornerRadius).TypeHandle] = (val) => NoesisGUI_.Box_CornerRadius((Noesis.CornerRadius)val);
            boxFunctions[typeof(Noesis.Stretch).TypeHandle] = (val) => NoesisGUI_.Box_Stretch((Noesis.Stretch)val);
            boxFunctions[typeof(Noesis.StretchDirection).TypeHandle] = (val) => NoesisGUI_.Box_StretchDirection((Noesis.StretchDirection)val);
            boxFunctions[typeof(Noesis.TextAlignment).TypeHandle] = (val) => NoesisGUI_.Box_TextAlignment((Noesis.TextAlignment)val);
            boxFunctions[typeof(Noesis.TextTrimming).TypeHandle] = (val) => NoesisGUI_.Box_TextTrimming((Noesis.TextTrimming)val);
            boxFunctions[typeof(Noesis.TextWrapping).TypeHandle] = (val) => NoesisGUI_.Box_TextWrapping((Noesis.TextWrapping)val);
            boxFunctions[typeof(Noesis.TickBarPlacement).TypeHandle] = (val) => NoesisGUI_.Box_TickBarPlacement((Noesis.TickBarPlacement)val);
            boxFunctions[typeof(Noesis.TickPlacement).TypeHandle] = (val) => NoesisGUI_.Box_TickPlacement((Noesis.TickPlacement)val);
            boxFunctions[typeof(Noesis.TileMode).TypeHandle] = (val) => NoesisGUI_.Box_TileMode((Noesis.TileMode)val);
            boxFunctions[typeof(Noesis.VerticalAlignment).TypeHandle] = (val) => NoesisGUI_.Box_VerticalAlignment((Noesis.VerticalAlignment)val);
            boxFunctions[typeof(Noesis.Visibility).TypeHandle] = (val) => NoesisGUI_.Box_Visibility((Noesis.Visibility)val);
            boxFunctions[typeof(Noesis.ClockState).TypeHandle] = (val) => NoesisGUI_.Box_ClockState((Noesis.ClockState)val);
            boxFunctions[typeof(Noesis.EasingMode).TypeHandle] = (val) => NoesisGUI_.Box_EasingMode((Noesis.EasingMode)val);
            boxFunctions[typeof(Noesis.SlipBehavior).TypeHandle] = (val) => NoesisGUI_.Box_SlipBehavior((Noesis.SlipBehavior)val);
            boxFunctions[typeof(Noesis.FillBehavior).TypeHandle] = (val) => NoesisGUI_.Box_FillBehavior((Noesis.FillBehavior)val);
            boxFunctions[typeof(Noesis.GridViewColumnHeaderRole).TypeHandle] = (val) => NoesisGUI_.Box_GridViewColumnHeaderRole((Noesis.GridViewColumnHeaderRole)val);
            boxFunctions[typeof(Noesis.HandoffBehavior).TypeHandle] = (val) => NoesisGUI_.Box_HandoffBehavior((Noesis.HandoffBehavior)val);
            boxFunctions[typeof(Noesis.PanningMode).TypeHandle] = (val) => NoesisGUI_.Box_PanningMode((Noesis.PanningMode)val);
            boxFunctions[typeof(Noesis.UpdateSourceTrigger).TypeHandle] = (val) => NoesisGUI_.Box_UpdateSourceTrigger((Noesis.UpdateSourceTrigger)val);
            boxFunctions[typeof(Noesis.ScrollUnit).TypeHandle] = (val) => NoesisGUI_.Box_ScrollUnit((Noesis.ScrollUnit)val);
            boxFunctions[typeof(Noesis.VirtualizationMode).TypeHandle] = (val) => NoesisGUI_.Box_VirtualizationMode((Noesis.VirtualizationMode)val);
            boxFunctions[typeof(Noesis.VirtualizationCacheLengthUnit).TypeHandle] = (val) => NoesisGUI_.Box_VirtualizationCacheLengthUnit((Noesis.VirtualizationCacheLengthUnit)val);

            return boxFunctions;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static IntPtr Box(object val)
        {
            BoxDelegate boxFunction;
            if (_boxFunctions.TryGetValue(val.GetType().TypeHandle, out boxFunction))
            {
                return RegisterPendingRelease(boxFunction(val));
            }
            else if (val.GetType().GetTypeInfo().IsEnum)
            {
                _boxFunctions.TryGetValue(typeof(int).TypeHandle, out boxFunction);
                return RegisterPendingRelease(boxFunction((int)Convert.ToInt64(val)));
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        private static IntPtr RegisterPendingRelease(IntPtr cPtr)
        {
            AddPendingRelease(cPtr);
            return cPtr;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        private class Boxed<T> { }
        private delegate object UnboxDelegate(IntPtr cPtr);
        private static Dictionary<RuntimeTypeHandle, UnboxDelegate> _unboxFunctions = CreateUnboxFunctions();

        private static Dictionary<RuntimeTypeHandle, UnboxDelegate> CreateUnboxFunctions()
        {
            Dictionary<RuntimeTypeHandle, UnboxDelegate> unboxFunctions = new Dictionary<RuntimeTypeHandle, UnboxDelegate>(100);

            unboxFunctions[typeof(Boxed<string>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_String(cPtr);
            unboxFunctions[typeof(Boxed<bool>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Bool(cPtr);
            unboxFunctions[typeof(Boxed<float>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Float(cPtr);
            unboxFunctions[typeof(Boxed<double>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Double(cPtr);
            unboxFunctions[typeof(Boxed<int>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Int(cPtr);
            unboxFunctions[typeof(Boxed<uint>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_UInt(cPtr);
            unboxFunctions[typeof(Boxed<short>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Short(cPtr);
            unboxFunctions[typeof(Boxed<ushort>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_UShort(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Color>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Color(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Point>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Point(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Rect>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Rect(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Size>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Size(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Thickness>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Thickness(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.CornerRadius>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_CornerRadius(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Duration>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Duration(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.KeyTime>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_KeyTime(cPtr);
            unboxFunctions[typeof(Boxed<System.TimeSpan>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_TimeSpan(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.VirtualizationCacheLength>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_VirtualizationCacheLength(cPtr);
            unboxFunctions[typeof(Boxed<bool?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableBool(cPtr);
            unboxFunctions[typeof(Boxed<float?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableFloat(cPtr);
            unboxFunctions[typeof(Boxed<double?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableDouble(cPtr);
            unboxFunctions[typeof(Boxed<int?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableInt32(cPtr);
            unboxFunctions[typeof(Boxed<uint?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableUInt32(cPtr);
            unboxFunctions[typeof(Boxed<short?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableInt16(cPtr);
            unboxFunctions[typeof(Boxed<ushort?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableUInt16(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Color?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableColor(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Point?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullablePoint(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Rect?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableRect(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Size?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableSize(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Thickness?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableThickness(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.CornerRadius?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableCornerRadius(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Duration?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableDuration(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.KeyTime?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableKeyTime(cPtr);
            unboxFunctions[typeof(Boxed<System.TimeSpan?>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_NullableTimeSpan(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.AlignmentX>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_AlignmentX(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.AlignmentY>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_AlignmentY(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.AutoToolTipPlacement>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_AutoToolTipPlacement(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.BindingMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_BindingMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.BitmapScalingMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_BitmapScalingMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.BrushMappingMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_BrushMappingMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.CharacterCasing>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_CharacterCasing(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.ClickMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_ClickMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.ColorInterpolationMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_ColorInterpolationMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Dock>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Dock(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.ExpandDirection>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_ExpandDirection(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.FillRule>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_FillRule(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.FlowDirection>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_FlowDirection(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.FontStretch>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_FontStretch(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.FontStyle>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_FontStyle(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.FontWeight>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_FontWeight(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.GeometryCombineMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_GeometryCombineMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.GradientSpreadMethod>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_GradientSpreadMethod(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.HorizontalAlignment>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_HorizontalAlignment(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.KeyboardNavigationMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_KeyboardNavigationMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.LineStackingStrategy>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_LineStackingStrategy(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.ListSortDirection>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_ListSortDirection(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.MenuItemRole>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_MenuItemRole(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Orientation>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Orientation(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.OverflowMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_OverflowMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.PenLineCap>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_PenLineCap(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.PenLineJoin>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_PenLineJoin(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.PlacementMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_PlacementMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.PopupAnimation>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_PopupAnimation(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.RelativeSourceMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_RelativeSourceMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.SelectionMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_SelectionMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.CornerRadius>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_CornerRadius(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Stretch>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Stretch(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.StretchDirection>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_StretchDirection(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.TextAlignment>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_TextAlignment(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.TextTrimming>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_TextTrimming(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.TextWrapping>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_TextWrapping(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.TickBarPlacement>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_TickBarPlacement(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.TickPlacement>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_TickPlacement(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.TileMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_TileMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.VerticalAlignment>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_VerticalAlignment(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.Visibility>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_Visibility(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.ClockState>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_ClockState(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.EasingMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_EasingMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.SlipBehavior>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_SlipBehavior(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.FillBehavior>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_FillBehavior(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.GridViewColumnHeaderRole>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_GridViewColumnHeaderRole(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.HandoffBehavior>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_HandoffBehavior(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.PanningMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_PanningMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.UpdateSourceTrigger>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_UpdateSourceTrigger(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.ScrollUnit>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_ScrollUnit(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.VirtualizationMode>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_VirtualizationMode(cPtr);
            unboxFunctions[typeof(Boxed<Noesis.VirtualizationCacheLengthUnit>).TypeHandle] = (cPtr) => NoesisGUI_.Unbox_VirtualizationCacheLengthUnit(cPtr);

            return unboxFunctions;
        }

        public static object Unbox(IntPtr cPtr, NativeTypeInfo info)
        {
            UnboxDelegate unboxFunction;
            if (_unboxFunctions.TryGetValue(info.Type.TypeHandle, out unboxFunction))
            {
                return unboxFunction(cPtr);
            }

            throw new InvalidOperationException("Can't unbox native pointer");
        }
    }
}
