using System;
using UnityEngine;

namespace Exund.AdvancedBuilding
{
    class ScaleBlocks : MonoBehaviour
    {
        private int ID = 7779;

        private bool visible = false;

        private TankBlock block;

        private float x = 0, y = 0, z = 0, posX, posY;

        private Rect win;

        private void Start()
        {
            // Singleton.Manager<ManPointer>.inst.MouseEvent += Inst_MouseEvent;

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                posX = Input.mousePosition.x;
                posY = Screen.height - Input.mousePosition.y;
                win = new Rect(posX + 200f, posY, 200f, 200f);
                try
                {
                    block = Singleton.Manager<ManPointer>.inst.targetVisible.block;
                    x = block.trans.localScale.x;
                    y = block.trans.localScale.y;
                    z = block.trans.localScale.z;
                    //Console.WriteLine(block.trans.rotation.eulerAngles);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    block = null;
                }
                visible = block;
            }
        }

        /*private void Inst_MouseEvent(ManPointer.Event arg1, bool arg2, bool arg3)
        {
            if (arg1 == ManPointer.Event.MMB && arg2 && Singleton.Manager<ManPointer>.inst.targetVisible.block)
            {
                
            }
        }*/

        private void OnGUI()
        {
            if (!visible || !block) return;
            if (AdvancedBuildingMod.ModExists("Nuterra.UI"))
            {
                GUI.skin = Nuterra.UI.NuterraGUI.Skin;
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
            GUI.skin.label.margin.bottom = 5;*/
            try
            {
                win = GUI.Window(ID, win, new GUI.WindowFunction(DoWindow), "Block scale");
                block.trans.localScale = new Vector3(x, y, z);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void DoWindow(int id)
        {
            GUILayout.Label("X scale");
            x = AdvancedBuildingMod.NumberField(x, 0.1f);
            //float.TryParse(GUILayout.TextField(x.ToString()), out x);

            GUILayout.Label("Y scale");
            y = AdvancedBuildingMod.NumberField(y, 0.1f);
            //float.TryParse(GUILayout.TextField(y.ToString()), out y);

            GUILayout.Label("Z scale");
            z = AdvancedBuildingMod.NumberField(z, 0.1f);
            //float.TryParse(GUILayout.TextField(z.ToString()), out z);
            if (GUILayout.Button("Close"))
            {
                visible = false;
                block = null;
            }
            GUI.DragWindow();
        }
    }
}
