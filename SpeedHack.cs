using MelonLoader;
using UnityEngine;

namespace PhasmophobiaPotatoGUI
{
    class SpeedHack
    {
        public static void playerSpeed()
        {
            if (Main.player != null)
            {
                GUI.Label(new Rect(10f, 100f, 150f, 20f), "SpeedHack: " + Menu.SpeedHack);
                if (Menu.SpeedHack)
                {
                    Main.player.field_Public_FirstPersonController_0.m_WalkSpeed = 4.5f;
                    Main.player.field_Public_FirstPersonController_0.m_RunSpeed = 5.0f;
                }
                else if (!Menu.SpeedHack)
                {
                    Main.player.field_Public_FirstPersonController_0.m_WalkSpeed = 1.2f;
                    Main.player.field_Public_FirstPersonController_0.m_RunSpeed = 1.5f;
                }
            }
        }
    }
}
