using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System;

public abstract class USerializerHandler
{
	public static USerializerHandler GetHandler(Type serializingType)
	{
		Type baseType = serializingType;

		if(handlers == null)
		{
			Type[] handlerTypes = ReflectionHelper.GetAttributeTypes(typeof(USerializerHandlerType));//get all handler types

			foreach(Type handlerType in handlerTypes)//iterate over handler types
			{
				USerializerHandlerType info = handlerType.GetCustomAttributes(typeof(USerializerHandlerType), false)[0] as USerializerHandlerType;//get the attribute
				handlers.Add(info.HandlerType, handlerType);//add the handler type to the dictionary
			}
		}

		do
		{
			Type handlerType = null;
			handlers.TryGetValue(baseType, out handlerType);

			if(handlerType != null)
			{
				return handlerType.GetConstructor(null).Invoke(null) as USerializerHandler;
			}

			baseType = baseType.BaseType;
		}
		while(baseType != null);

		return null;
	}
	private static Dictionary<Type, Type> handlers;

	/// <summary>
	/// The value that this handler is serializing
	/// </summary>
	/// <value>The value being serialized</value>
	public System.Object Value
	{
		get{return value;}
		set{this.value = value;}
	}

	public void Serialize(BinaryWriter writer)
	{
		writer.Write (GetType().AssemblyQualifiedName);
		writer.Write(Value.GetType().AssemblyQualifiedName);
		
		OnSerialize(writer);
	}
	
	/// <summary>
	/// Deserializes an object from a binary stream
	/// </summary>
	/// <param name="reader">The reader to read the binary data.</param>
	/// <param name="outer">The object that owns this object.</param>
	/// <returns>The deserialized object, or null if an error occured</returns>
	public object Deserialize (BinaryReader reader, object outer)
	{
		object defaultObj = CreateAbstractTemplate(reader.ReadString(), outer, reader);
		if(defaultObj == null)
			return null;
		
		Value = defaultObj;
		
		OnDeserialize(reader, outer);
		
		return Value;
	}

	protected abstract void OnSerialize(BinaryWriter writer);

	protected abstract void OnDeserialize(BinaryReader reader, object outer);

	/// <summary>
	/// Creates the default object that is used to deserialize into
	/// </summary>
	/// <returns>The abstract version of this object</returns>
	/// <param name="typeName">The typename that was used when serializing this object.</param>
	/// <param name="outer">The object that owns this object.</param>
	/// <param name="reader">The reader that is reading the binary stream.</param>
	protected abstract object CreateAbstractTemplate(string typeName, object outer, BinaryReader reader);

	private System.Object value;
}

public sealed class USerializerHandlerType : System.Attribute
{
	public System.Type HandlerType
	{
		get{return type;}
	}

	public USerializerHandlerType(System.Type type)
	{
		this.type = type;
	}

	private System.Type type;
}
