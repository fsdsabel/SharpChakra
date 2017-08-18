﻿using System;
using System.Runtime.InteropServices;

namespace SharpChakra
{
   /// <summary>
   ///     A JavaScript value.
   /// </summary>
   /// <remarks>
   ///     A JavaScript value is one of the following types of values: Undefined, Null, Boolean, 
   ///     String, Number, or Object.
   /// </remarks>
   public struct JavaScriptValue
   {
      /// <summary>
      /// The reference.
      /// </summary>
      private readonly IntPtr p_reference;

      /// <summary>
      ///     Initializes a new instance of the <see cref="JavaScriptValue"/> struct.
      /// </summary>
      /// <param name="_reference">The reference.</param>
      private JavaScriptValue(IntPtr _reference)
      {
         p_reference = _reference;
      }

      /// <summary>
      ///     Gets an invalid value.
      /// </summary>
      public static JavaScriptValue Invalid => new JavaScriptValue(IntPtr.Zero);
      /// <summary>
      ///     Gets the value of <c>undefined</c> in the current script context.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public static JavaScriptValue Undefined
      {
         get
         {
            Native.ThrowIfError(Native.JsGetUndefinedValue(out var value));
            return value;
         }
      }

      /// <summary>
      ///     Gets the value of <c>null</c> in the current script context.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public static JavaScriptValue Null
      {
         get
         {
            Native.ThrowIfError(Native.JsGetNullValue(out var value));
            return value;
         }
      }

      /// <summary>
      ///     Gets the value of <c>true</c> in the current script context.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public static JavaScriptValue True
      {
         get
         {
            Native.ThrowIfError(Native.JsGetTrueValue(out var value));
            return value;
         }
      }

      /// <summary>
      ///     Gets the value of <c>false</c> in the current script context.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public static JavaScriptValue False
      {
         get
         {
            Native.ThrowIfError(Native.JsGetFalseValue(out var value));
            return value;
         }
      }

      /// <summary>
      ///     Gets the global object in the current script context.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public static JavaScriptValue GlobalObject
      {
         get
         {
            Native.ThrowIfError(Native.JsGetGlobalObject(out var value));
            return value;
         }
      }

      /// <summary>
      ///     Gets a value indicating whether the value is valid.
      /// </summary>
      public bool IsValid => p_reference != IntPtr.Zero;
      /// <summary>
      ///     Gets the JavaScript type of the value.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>The type of the value.</returns>
      public JavaScriptValueType ValueType
      {
         get
         {
            Native.ThrowIfError(Native.JsGetValueType(this, out var type));
            return type;
         }
      }

      /// <summary>
      ///     Gets the length of a <c>String</c> value.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>The length of the string.</returns>
      public int StringLength
      {
         get
         {
            Native.ThrowIfError(Native.JsGetStringLength(this, out var length));
            return length;
         }
      }

      /// <summary>
      ///     Gets or sets the prototype of an object.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public JavaScriptValue Prototype
      {
         get
         {
            Native.ThrowIfError(Native.JsGetPrototype(this, out var prototypeReference));
            return prototypeReference;
         }

         set => Native.ThrowIfError(Native.JsSetPrototype(this, value));
      }

      /// <summary>
      ///     Gets a value indicating whether an object is extensible or not.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public bool IsExtensionAllowed
      {
         get
         {
            Native.ThrowIfError(Native.JsGetExtensionAllowed(this, out var allowed));
            return allowed;
         }
      }

      /// <summary>
      ///     Gets a value indicating whether an object is an external object.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public bool HasExternalData
      {
         get
         {
            Native.ThrowIfError(Native.JsHasExternalData(this, out var hasExternalData));
            return hasExternalData;
         }
      }

      /// <summary>
      ///     Gets or sets the data in an external object.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public IntPtr ExternalData
      {
         get
         {
            Native.ThrowIfError(Native.JsGetExternalData(this, out var data));
            return data;
         }

         set => Native.ThrowIfError(Native.JsSetExternalData(this, value));
      }

      /// <summary>
      ///     Creates a <c>Boolean</c> value from a <c>bool</c> value.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_value">The value to be converted.</param>
      /// <returns>The converted value.</returns>
      public static JavaScriptValue FromBoolean(bool _value)
      {
         Native.ThrowIfError(Native.JsBoolToBoolean(_value, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a <c>Number</c> value from a <c>double</c> value.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_value">The value to be converted.</param>
      /// <returns>The new <c>Number</c> value.</returns>
      public static JavaScriptValue FromDouble(double _value)
      {
         Native.ThrowIfError(Native.JsDoubleToNumber(_value, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a <c>Number</c> value from a <c>int</c> value.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_value">The value to be converted.</param>
      /// <returns>The new <c>Number</c> value.</returns>
      public static JavaScriptValue FromInt32(int _value)
      {
         Native.ThrowIfError(Native.JsIntToNumber(_value, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a <c>String</c> value from a string pointer.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_value">The string  to convert to a <c>String</c> value.</param>
      /// <returns>The new <c>String</c> value.</returns>
      public static JavaScriptValue FromString(string _value)
      {
         Native.ThrowIfError(Native.JsPointerToString(_value, new UIntPtr((uint)_value.Length), out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new <c>Object</c>.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>The new <c>Object</c>.</returns>
      public static JavaScriptValue CreateObject()
      {
         Native.ThrowIfError(Native.JsCreateObject(out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new <c>Object</c> that stores some external data.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_data">External data that the object will represent. May be null.</param>
      /// <param name="_finalizer">
      ///     A callback for when the object is finalized. May be null.
      /// </param>
      /// <returns>The new <c>Object</c>.</returns>
      public static JavaScriptValue CreateExternalObject(IntPtr _data, JavaScriptObjectFinalizeCallback _finalizer)
      {
         Native.ThrowIfError(Native.JsCreateExternalObject(_data, _finalizer, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new JavaScript function.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_function">The method to call when the function is invoked.</param>
      /// <returns>The new function object.</returns>
      public static JavaScriptValue CreateFunction(JavaScriptNativeFunction _function)
      {
         Native.ThrowIfError(Native.JsCreateFunction(_function, IntPtr.Zero, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new JavaScript function.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_function">The method to call when the function is invoked.</param>
      /// <param name="_callbackData">Data to be provided to all function callbacks.</param>
      /// <returns>The new function object.</returns>
      public static JavaScriptValue CreateFunction(JavaScriptNativeFunction _function, IntPtr _callbackData)
      {
         Native.ThrowIfError(Native.JsCreateFunction(_function, _callbackData, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a JavaScript array object.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_length">The initial length of the array.</param>
      /// <returns>The new array object.</returns>
      public static JavaScriptValue CreateArray(uint _length)
      {
         Native.ThrowIfError(Native.JsCreateArray(_length, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new JavaScript error object
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_message">Message for the error object.</param>
      /// <returns>The new error object.</returns>
      public static JavaScriptValue CreateError(JavaScriptValue _message)
      {
         Native.ThrowIfError(Native.JsCreateError(_message, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new JavaScript RangeError error object
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_message">Message for the error object.</param>
      /// <returns>The new error object.</returns>
      public static JavaScriptValue CreateRangeError(JavaScriptValue _message)
      {
         Native.ThrowIfError(Native.JsCreateRangeError(_message, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new JavaScript ReferenceError error object
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_message">Message for the error object.</param>
      /// <returns>The new error object.</returns>
      public static JavaScriptValue CreateReferenceError(JavaScriptValue _message)
      {
         Native.ThrowIfError(Native.JsCreateReferenceError(_message, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new JavaScript SyntaxError error object
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_message">Message for the error object.</param>
      /// <returns>The new error object.</returns>
      public static JavaScriptValue CreateSyntaxError(JavaScriptValue _message)
      {
         Native.ThrowIfError(Native.JsCreateSyntaxError(_message, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new JavaScript TypeError error object
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_message">Message for the error object.</param>
      /// <returns>The new error object.</returns>
      public static JavaScriptValue CreateTypeError(JavaScriptValue _message)
      {
         Native.ThrowIfError(Native.JsCreateTypeError(_message, out var reference));
         return reference;
      }

      /// <summary>
      ///     Creates a new JavaScript URIError error object
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_message">Message for the error object.</param>
      /// <returns>The new error object.</returns>
      public static JavaScriptValue CreateUriError(JavaScriptValue _message)
      {
         Native.ThrowIfError(Native.JsCreateUriError(_message, out var reference));
         return reference;
      }

      /// <summary>
      ///     Adds a reference to the object.
      /// </summary>
      /// <remarks>
      ///     This only needs to be called on objects that are not going to be stored somewhere on 
      ///     the stack. Calling AddRef ensures that the JavaScript object the value refers to will not be freed 
      ///     until Release is called
      /// </remarks>
      /// <returns>The object's new reference count.</returns>
      public uint AddRef()
      {
         Native.ThrowIfError(Native.JsAddRef(this, out var count));
         return count;
      }

      /// <summary>
      ///     Releases a reference to the object.
      /// </summary>
      /// <remarks>
      ///     Removes a reference that was created by AddRef.
      /// </remarks>
      /// <returns>The object's new reference count.</returns>
      public uint Release()
      {
         Native.ThrowIfError(Native.JsRelease(this, out var count));
         return count;
      }

      /// <summary>
      ///     Retrieves the <c>bool</c> value of a <c>Boolean</c> value.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>The converted value.</returns>
      public bool ToBoolean()
      {
         Native.ThrowIfError(Native.JsBooleanToBool(this, out var value));
         return value;
      }

      /// <summary>
      ///     Retrieves the <c>double</c> value of a <c>Number</c> value.
      /// </summary>
      /// <remarks>
      ///     <para>
      ///     This function retrieves the value of a Number value. It will fail with 
      ///     <c>InvalidArgument</c> if the type of the value is not <c>Number</c>.
      ///     </para>
      ///     <para>
      ///     Requires an active script context.
      ///     </para>
      /// </remarks>
      /// <returns>The <c>double</c> value.</returns>
      public double ToDouble()
      {
         Native.ThrowIfError(Native.JsNumberToDouble(this, out var value));
         return value;
      }

      /// <summary>
      ///     Retrieves the <c>int</c> value of a <c>Number</c> value.
      /// </summary>
      /// <remarks>
      ///     <para>
      ///     This function retrieves the value of a Number value. It will fail with
      ///     <c>InvalidArgument</c> if the type of the value is not <c>Number</c>.
      ///     </para>
      ///     <para>
      ///     Requires an active script context.
      ///     </para>
      /// </remarks>
      /// <returns>The <c>int</c> value.</returns>
      public int ToInt32()
      {
         Native.ThrowIfError(Native.JsNumberToInt(this, out var value));
         return value;
      }

      /// <summary>
      ///     Retrieves the string pointer of a <c>String</c> value.
      /// </summary>
      /// <remarks>
      ///     <para>
      ///     This function retrieves the string pointer of a <c>String</c> value. It will fail with 
      ///     <c>InvalidArgument</c> if the type of the value is not <c>String</c>.
      ///     </para>
      ///     <para>
      ///     Requires an active script context.
      ///     </para>
      /// </remarks>
      /// <returns>The string.</returns>
      public new string ToString()
      {
         Native.ThrowIfError(Native.JsStringToPointer(this, out var buffer, out var length));
         return Marshal.PtrToStringUni(buffer, (int)length);
      }

      /// <summary>
      ///     Converts the value to <c>Boolean</c> using regular JavaScript semantics.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>The converted value.</returns>
      public JavaScriptValue ConvertToBoolean()
      {
         Native.ThrowIfError(Native.JsConvertValueToBoolean(this, out var booleanReference));
         return booleanReference;
      }

      /// <summary>
      ///     Converts the value to <c>Number</c> using regular JavaScript semantics.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>The converted value.</returns>
      public JavaScriptValue ConvertToNumber()
      {
         Native.ThrowIfError(Native.JsConvertValueToNumber(this, out var numberReference));
         return numberReference;
      }

      /// <summary>
      ///     Converts the value to <c>String</c> using regular JavaScript semantics.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>The converted value.</returns>
      public JavaScriptValue ConvertToString()
      {
         Native.ThrowIfError(Native.JsConvertValueToString(this, out var stringReference));
         return stringReference;
      }

      /// <summary>
      ///     Converts the value to <c>Object</c> using regular JavaScript semantics.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>The converted value.</returns>
      public JavaScriptValue ConvertToObject()
      {
         Native.ThrowIfError(Native.JsConvertValueToObject(this, out var objectReference));
         return objectReference;
      }

      /// <summary>
      ///     Sets an object to not be extensible.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      public void PreventExtension()
      {
         Native.ThrowIfError(Native.JsPreventExtension(this));
      }

      /// <summary>
      ///     Gets a property descriptor for an object's own property.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_propertyId">The ID of the property.</param>
      /// <returns>The property descriptor.</returns>
      public JavaScriptValue GetOwnPropertyDescriptor(JavaScriptPropertyId _propertyId)
      {
         Native.ThrowIfError(Native.JsGetOwnPropertyDescriptor(this, _propertyId, out var descriptorReference));
         return descriptorReference;
      }

      /// <summary>
      ///     Gets the list of all properties on the object.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <returns>An array of property names.</returns>
      public JavaScriptValue GetOwnPropertyNames()
      {
         Native.ThrowIfError(Native.JsGetOwnPropertyNames(this, out var propertyNamesReference));
         return propertyNamesReference;
      }

      /// <summary>
      ///     Determines whether an object has a property.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_propertyId">The ID of the property.</param>
      /// <returns>Whether the object (or a prototype) has the property.</returns>
      public bool HasProperty(JavaScriptPropertyId _propertyId)
      {
         Native.ThrowIfError(Native.JsHasProperty(this, _propertyId, out var hasProperty));
         return hasProperty;
      }

      /// <summary>
      ///     Gets an object's property.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_id">The ID of the property.</param>
      /// <returns>The value of the property.</returns>
      public JavaScriptValue GetProperty(JavaScriptPropertyId _id)
      {
         Native.ThrowIfError(Native.JsGetProperty(this, _id, out var propertyReference));
         return propertyReference;
      }

      /// <summary>
      ///     Sets an object's property.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_id">The ID of the property.</param>
      /// <param name="_value">The new value of the property.</param>
      /// <param name="_useStrictRules">The property set should follow strict mode rules.</param>
      public void SetProperty(JavaScriptPropertyId _id, JavaScriptValue _value, bool _useStrictRules)
      {
         Native.ThrowIfError(Native.JsSetProperty(this, _id, _value, _useStrictRules));
      }

      /// <summary>
      ///     Deletes an object's property.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_propertyId">The ID of the property.</param>
      /// <param name="_useStrictRules">The property set should follow strict mode rules.</param>
      /// <returns>Whether the property was deleted.</returns>
      public JavaScriptValue DeleteProperty(JavaScriptPropertyId _propertyId, bool _useStrictRules)
      {
         Native.ThrowIfError(Native.JsDeleteProperty(this, _propertyId, _useStrictRules, out var returnReference));
         return returnReference;
      }

      /// <summary>
      ///     Defines a new object's own property from a property descriptor.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_propertyId">The ID of the property.</param>
      /// <param name="_propertyDescriptor">The property descriptor.</param>
      /// <returns>Whether the property was defined.</returns>
      public bool DefineProperty(JavaScriptPropertyId _propertyId, JavaScriptValue _propertyDescriptor)
      {
         Native.ThrowIfError(Native.JsDefineProperty(this, _propertyId, _propertyDescriptor, out var result));
         return result;
      }

      /// <summary>
      ///     Test if an object has a value at the specified index.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_index">The index to test.</param>
      /// <returns>Whether the object has an value at the specified index.</returns>
      public bool HasIndexedProperty(JavaScriptValue _index)
      {
         Native.ThrowIfError(Native.JsHasIndexedProperty(this, _index, out var hasProperty));
         return hasProperty;
      }

      /// <summary>
      ///     Retrieve the value at the specified index of an object.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_index">The index to retrieve.</param>
      /// <returns>The retrieved value.</returns>
      public JavaScriptValue GetIndexedProperty(JavaScriptValue _index)
      {
         Native.ThrowIfError(Native.JsGetIndexedProperty(this, _index, out var propertyReference));
         return propertyReference;
      }

      /// <summary>
      ///     Set the value at the specified index of an object.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_index">The index to set.</param>
      /// <param name="_value">The value to set.</param>
      public void SetIndexedProperty(JavaScriptValue _index, JavaScriptValue _value)
      {
         Native.ThrowIfError(Native.JsSetIndexedProperty(this, _index, _value));
      }

      /// <summary>
      ///     Delete the value at the specified index of an object.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_index">The index to delete.</param>
      public void DeleteIndexedProperty(JavaScriptValue _index)
      {
         Native.ThrowIfError(Native.JsDeleteIndexedProperty(this, _index));
      }

      /// <summary>
      ///     Compare two JavaScript values for equality.
      /// </summary>
      /// <remarks>
      ///     <para>
      ///     This function is equivalent to the "==" operator in JavaScript.
      ///     </para>
      ///     <para>
      ///     Requires an active script context.
      ///     </para>
      /// </remarks>
      /// <param name="_other">The object to compare.</param>
      /// <returns>Whether the values are equal.</returns>
      public bool Equals(JavaScriptValue _other)
      {
         Native.ThrowIfError(Native.JsEquals(this, _other, out var equals));
         return equals;
      }

      /// <summary>
      ///     Compare two JavaScript values for strict equality.
      /// </summary>
      /// <remarks>
      ///     <para>
      ///     This function is equivalent to the "===" operator in JavaScript.
      ///     </para>
      ///     <para>
      ///     Requires an active script context.
      ///     </para>
      /// </remarks>
      /// <param name="_other">The object to compare.</param>
      /// <returns>Whether the values are strictly equal.</returns>
      public bool StrictEquals(JavaScriptValue _other)
      {
         Native.ThrowIfError(Native.JsStrictEquals(this, _other, out var equals));
         return equals;
      }

      /// <summary>
      ///     Invokes a function.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_arguments">The arguments to the call.</param>
      /// <returns>The <c>Value</c> returned from the function invocation, if any.</returns>
      public JavaScriptValue CallFunction(params JavaScriptValue[] _arguments)
      {
         if (_arguments.Length > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(_arguments));

         Native.ThrowIfError(Native.JsCallFunction(this, _arguments, (ushort)_arguments.Length, out var returnReference));
         return returnReference;
      }

      /// <summary>
      ///     Invokes a function as a constructor.
      /// </summary>
      /// <remarks>
      ///     Requires an active script context.
      /// </remarks>
      /// <param name="_arguments">The arguments to the call.</param>
      /// <returns>The <c>Value</c> returned from the function invocation.</returns>
      public JavaScriptValue ConstructObject(params JavaScriptValue[] _arguments)
      {
         if (_arguments.Length > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(_arguments));

         Native.ThrowIfError(Native.JsConstructObject(this, _arguments, (ushort)_arguments.Length, out var returnReference));
         return returnReference;
      }
   }
}
