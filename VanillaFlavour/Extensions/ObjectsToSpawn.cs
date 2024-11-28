using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VanillaFlavour.Extensions
{
    public class ObjectsToSpawnAdditionalData
    {
        public Action<GameObject[]> SpawnedObjectAction { get; set; } = default;
    }

    public static class ObjectsToSpawnExtension
    {
        public static readonly ConditionalWeakTable<ObjectsToSpawn, ObjectsToSpawnAdditionalData> data = new();

        public static ObjectsToSpawnAdditionalData GetAdditionalData(this ObjectsToSpawn objectsToSpawn)
        {
            return data.GetOrCreateValue(objectsToSpawn);
        }
    }
}