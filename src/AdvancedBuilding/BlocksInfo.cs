using System;
using System.Linq;
using UnityEngine;

namespace Exund.AdvancedBuilding
{
    class BlocksInfo : MonoBehaviour
    {
        private int ID = 7777;

        private bool visible = false;

        private TankBlock block;

        private float posX, posY;

        private Rect win;

        private void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                win = new Rect(Input.mousePosition.x - 400f, Screen.height - Input.mousePosition.y, 200f, 200f);
                try
                {
                    block = Singleton.Manager<ManPointer>.inst.targetVisible.block;
                }
                catch (Exception ex)
                {
                    block = null;
                }
                visible = block;
            }
        }

        private void OnGUI()
        {
            if (!visible || !block) return;
            if (!AdvancedBuildingMod.Nuterra && AdvancedBuildingMod.ModExists("Nuterra.UI"))
            {
                foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.FullName.StartsWith("Nuterra.UI"))
                    {
                        var type = assembly.GetTypes().First(t => t.Name.Contains("NuterraGUI"));
                        AdvancedBuildingMod.Nuterra = (GUISkin)type.GetProperty("Skin").GetValue(null, null);
                        break;
                    }
                }
            }
            if (AdvancedBuildingMod.Nuterra)
            {
                GUI.skin = AdvancedBuildingMod.Nuterra;
            }
            /*GUI.skin = NuterraGUI.Skin;/*.window = new GUIStyle(GUI.skin.window)
            {
                normal =
            {
                background = NuterraGUI.LoadImage("Border_BG.png"),
                textColor = Color.white
            },
                border = new RectOffset(16, 16, 16, 16),
            }; 
            GUI.skin.label.margin.top = 5;
            GUI.skin.label.margin.bottom = 0;*/
            try
            {
                win = GUI.Window(ID, win, new GUI.WindowFunction(DoWindow), "Block Infos");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void DoWindow(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Type");
            GUILayout.Label(block.BlockType.ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Category");
            GUILayout.Label(block.BlockCategory.ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Rarity");
            GUILayout.Label(block.BlockRarity.ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Centre of Mass");
            GUILayout.Label(block.CentreOfMass.ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Mass");
            GUILayout.Label(block.CurrentMass.ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Health");
            GUILayout.Label(block.visible.damageable.Health.ToString());
            GUILayout.EndHorizontal();


            if (GUILayout.Button("Close"))
            {
                visible = false;
                block = null;
            }
            GUI.DragWindow();
        }
    }
}
