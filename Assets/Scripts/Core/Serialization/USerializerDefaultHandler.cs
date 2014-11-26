using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;

public class USerializerDefaultHandler : USerializerHandler
{

	protected override void OnSerialize (BinaryWriter writer)
	{
		serializePrivateFields(writer);//serialize all private fields with flags
		serializePublicFields(writer);//serialize all public fields
	}

	protected void serializePrivateFields(BinaryWriter writer)
	{
		object o = Value as object;
		FieldInfo[] fields = ReflectionHelper.GetFields(o, BindingFlags.Instance | BindingFlags.NonPublic);//get all non-public fields for this object and write them to the writer

		foreach(FieldInfo field in fields)
		{
			if(field.IsDefined(typeof(SerializeField), true))
				serializeField(field, writer);
		}
	}

	protected void serializePublicFields(BinaryWriter writer)
	{
		object o = Value as object;
		FieldInfo[] fields = ReflectionHelper.GetFields(o, BindingFlags.Instance | BindingFlags.Public);//get all public fields for this object and write them to the writer
		
		foreach(FieldInfo field in fields)
		{
			if(field.IsDefined(typeof(SerializeField), true))
				serializeField(field, writer);
		}
	}

	protected void serializeField(FieldInfo field, BinaryWriter writer)
	{
		writer.Write (string.Format("{//START:{0}}", field.Name));//signify the start of a value

		serializeValue(field.GetValue(Value), writer);//serialize the field value 

		writer.Write(string.Format("{//END:{0}}", field.Name));//signify the end of a value
	}

	protected void serializeValue(object value, BinaryWriter writer)
	{
		if(value == null)//write if a valid value type was written, or if null was written
		{
			writer.Write (false);
			return;
		}
		else
			writer.Write (true);
		
		
		Type valueType = value.GetType();//get the value type of the field value
		
		if(valueType.IsPrimitive)
		{
			serializePrimitive(value, writer);
		}
		else if(valueType.IsEnum)
		{
			serializeEnum(value, writer);
		}
		else
		{
			USerializer.Serialize(value, writer);
		}
	}

	protected void serializePrimitive(object value, BinaryWriter writer)
	{
		//determine the primitive type and write it to the writer

		Type valueType = value.GetType();
		if(valueType == typeof(byte))
			writer.Write ((byte)value);
		else if(valueType == typeof(bool))
			writer.Write((bool)value);
		else if(valueType == typeof(char))
			writer.Write((char)value);
		else if(valueType == typeof(short))
			writer.Write((short)value);
		else if(valueType == typeof(ushort))
			writer.Write((ushort)value);
		else if(valueType == typeof(int))
			writer.Write((int)value);
		else if(valueType == typeof(uint))
			writer.Write((uint)value);
		else if(valueType == typeof(long))
			writer.Write((long)value);
		else if(valueType == typeof(ulong))
			writer.Write((ulong)value);
		else if(valueType == typeof(Single))
			writer.Write((Single)value);
		else if(valueType == typeof(float))
			writer.Write((float)value);
		else if(valueType == typeof(double))
			writer.Write((double)value);
		else if(valueType == typeof(string))
			writer.Write((string)value);
	}

	//write the enum name to the writer
	protected void serializeEnum(object value, BinaryWriter writer)
	{
		string enumName = Enum.GetName(value.GetType(), value);
		writer.Write (enumName);
	}

	protected override object CreateAbstractTemplate (string typeName, object outer, BinaryReader reader)
	{
		Type type = Type.GetType(typeName);//get the full type
		if(type == null)//if no type was found then return null
			return null;

		ConstructorInfo constructor = type.GetConstructor(null);//get the public constructor if one exists
		if(constructor == null)//if no valid constructor is found then return null
			return null;

		return constructor.Invoke (null);//invoke the constructor to create the default object
	}

	protected override void OnDeserialize (BinaryReader reader, object outer)
	{

	}
}
