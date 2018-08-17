using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Exund.AdvancedBuilding
{
    class AdvancedBuildingMod
    {
        private static GameObject _holder;
        public static void Load()
        {
            _holder = new GameObject();
            _holder.AddComponent<RotateBlocks>();
            _holder.AddComponent<TranslateBlocks>();
            _holder.AddComponent<ScaleBlocks>();
            _holder.AddComponent<BlocksInfo>();
            UnityEngine.Object.DontDestroyOnLoad(_holder);
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
