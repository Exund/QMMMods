using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace Exund.AdvancedBuilding
{
    public class AdvancedBuildingMod
    {
        public static string PreciseSnapshotsFolder = Path.Combine(Application.dataPath, "../PreciseSnapshots");
        private static GameObject _holder;
        internal static GUISkin Nuterra;

        public static bool ModExists(string name)
        {
            foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith(name))
                {
                    return true;
                }
            }
            return false;
        }

        public static void Load()
        {
            try
            {
                _holder = new GameObject();
                _holder.AddComponent<RotateBlocks>();
                _holder.AddComponent<TranslateBlocks>();
                _holder.AddComponent<ScaleBlocks>();
                _holder.AddComponent<BlocksInfo>();
                _holder.AddComponent<SaveWindow>();
                _holder.AddComponent<LoadWindow>();
                UnityEngine.Object.DontDestroyOnLoad(_holder);

                if (!Directory.Exists(PreciseSnapshotsFolder))
                {
                    Directory.CreateDirectory(PreciseSnapshotsFolder);
                }
            } catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static float NumberField(float value, float interval)
        {
            GUILayout.BeginHorizontal(GUILayout.Height(30));
            float.TryParse(GUILayout.TextField(value.ToString()), out float val);
            if (GUILayout.Button("+")) val += interval;
            if (GUILayout.Button("-")) val -= interval;
            GUILayout.EndHorizontal();
            return val;
        }
    }
}
