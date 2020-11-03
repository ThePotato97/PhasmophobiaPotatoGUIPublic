using MelonLoader;
using UnityEngine;

namespace PhasmophobiaPotatoGUI
{
    class FullBright
    {
        public static void changeBright()
        {
            
            GUI.Label(new Rect(10f, 160f, 150f, 20f), "Fullbright: " + Menu.fullbright);
            if (Menu.fullbright)
            {
                Main.light = Main.boneTransform.gameObject.AddComponent<Light>();
                Main.light.color = Color.white;
                Main.light.type = LightType.Spot;
                Main.light.shadows = LightShadows.None;
                Main.light.range = 99f;
                Main.light.spotAngle = 9999f;
                Main.light.intensity = 0.3f;
                return;
            }
            else
            {
                Object.Destroy(Main.light);
            }
        }
    }
}
