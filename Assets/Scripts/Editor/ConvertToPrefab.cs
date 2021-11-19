using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Editor
{

    class ConvertToPrefab
    {
        [MenuItem("Tools/Link Prefabs")]
        public static void NewMenuOption()
        {
            // get the selected transforms
            Transform[] selection = Selection.transforms;

            // remember their positions
            List<Vector3> positions = new List<Vector3>();
            for (int i=0; i<selection.Length; i++)
                positions.Add(selection[i].position);

            Transform prefab = AssetDatabase.LoadAssetAtPath<Transform>("Assets/Prefabs/Ledge.prefab");

            // make a prefab at each location
            for (int i = 0; i < selection.Length; i++) {
                var obj = PrefabUtility.InstantiatePrefab(prefab, selection[i].parent) as Transform;
                obj.position = selection[i].position;
                obj.rotation = selection[i].rotation;
                obj.localScale = selection[i].localScale;
            }

            // clean up old objects
            for (int i = 0; i < selection.Length; i++)
                Object.DestroyImmediate(selection[i].gameObject);
        }
    }
}
