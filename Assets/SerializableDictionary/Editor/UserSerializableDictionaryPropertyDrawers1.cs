using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [CustomPropertyDrawer(typeof(StringStringDictionary))]

[CustomPropertyDrawer(typeof(StringStringStringBoolDictionary))]
[CustomPropertyDrawer(typeof(StringStringBoolDictionary))]
[CustomPropertyDrawer(typeof(StringBoolDictionary))]

// [CustomPropertyDrawer(typeof(ObjectColorDictionary))]
// [CustomPropertyDrawer(typeof(StringColorArrayDictionary))]
public class AnySerializableDictionaryPropertyDraweraaa : SerializableDictionaryPropertyDrawer {}

// [CustomPropertyDrawer(typeof(StringStringBoolDictionary))]
// public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
