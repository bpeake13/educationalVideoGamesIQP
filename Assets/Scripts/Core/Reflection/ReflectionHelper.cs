using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class ReflectionHelper
{
	/// <summary>
	/// Gets all classes with the attribute T defined
	/// </summary>
	/// <returns>An array of all types with attribute T.</returns>
	/// <typeparam name="T">The attribute type to look for</typeparam>
	/// <remarks>This method is slow, do not use in time critical functions</remarks>
	public static System.Type[] GetAttributeTypes(Type attributeType, bool inherited = false)
	{
		getAttributeTypesCache.Clear();

		if(assembliesCache == null)
			assembliesCache = AppDomain.CurrentDomain.GetAssemblies();//gets all loaded assemblies

		foreach(Assembly a in assembliesCache)
		{
			Type[] types = a.GetTypes ();
			foreach(Type t in types)
			{
				if(t.IsDefined(attributeType, inherited))
					getAttributeTypesCache.Add(t);
			}
		}

		return getAttributeTypesCache.ToArray();
	}
	private static List<Type> getAttributeTypesCache = new List<Type>();

	public static Type[] GetSubclasses(Type baseClassType)
	{
		getSubclassesCache.Clear();
		
		if(assembliesCache == null)
			assembliesCache = AppDomain.CurrentDomain.GetAssemblies();//gets all loaded assemblies

		foreach(Assembly a in assembliesCache)
		{
			Type[] types = a.GetTypes ();
			foreach(Type t in types)
			{
				if(t.IsSubclassOf(baseClassType))
					getSubclassesCache.Add(t);
			}
		}
		
		return getSubclassesCache.ToArray();
	}
	private static List<Type> getSubclassesCache = new List<Type>();

	public static FieldInfo[] GetFields(object obj, BindingFlags flags)
	{
		getFieldsCache.Clear();

		Type baseType = obj.GetType();

		do
		{
			FieldInfo[] classFields = baseType.GetFields(flags);
			getFieldsCache.UnionWith(classFields);
		}
		while(baseType != null);

		FieldInfo[] fields = new FieldInfo[getFieldsCache.Count];
		getFieldsCache.CopyTo(fields);

		return fields;
	}
	private static HashSet<FieldInfo> getFieldsCache = new HashSet<FieldInfo>();

	private static Assembly[] assembliesCache;
}
