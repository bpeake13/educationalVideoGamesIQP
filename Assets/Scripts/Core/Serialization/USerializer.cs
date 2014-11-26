using UnityEngine;
using System.Collections;
using System.IO;
using System;

/// <summary>
/// Provides serialization for unity data
/// </summary>
public class USerializer
{
	public static bool Serialize(System.Object obj, BinaryWriter writer)
	{
		if(obj == null)
			return false;//serializing a null value is considered an error

		Type objType = obj.GetType();

		USerializerHandler handler = USerializerHandler.GetHandler(objType);
		if(handler == null)
			return false;//return an error if a handler is not found for this type

		string fullTypeName = objType.AssemblyQualifiedName;

		writer.Write (fullTypeName);//write the type name to the file

		handler.Value = obj;
		handler.Serialize(writer);

		return true;
	}


}
