using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PhasmophobiaPotatoGUI
{
    public class HUD : MelonMod
    {
        public static void DrawHUD()
        {
            WaterMark();
            if (Main.levelController != null)
                if (Menu.ShowInfoGhost)
                {
                    ghostInfo();
                }
                if (Menu.showMissionInfo)
                {
                    missionInfo();
                }
                if (Menu.ShowInfoPlayer)
                {
                    playerInfo();
                }
            }
        }

        private static void WaterMark()
        {
            GUI.color = Color.white;
            GUI.Label(new Rect(10f, 70f, 150f, 20f), "Potato GUI (Public)");
        }

        private static void ghostInfo()
        {
            if (Main.ghostInfo != null)
            {
                GUI.Label(new Rect(10f, 400f, 150f, 20f), "Ghost Age: " + Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_Int32_0.ToString());
                GUI.Label(new Rect(10f, 415f, 850f, 20f), "Ghost Name: " + (Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_String_0.ToString()) + (Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_Boolean_1 ? " (Shy)" : ""));
                GUI.Label(new Rect(10f, 430f, 850f, 20f), "Ghost Type: " + Main.GetGhostType(Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique_0));
                GUI.Label(new Rect(10f, 460f, 150f, 20f), "Ghost Gender: " + (Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_Boolean_0 ? "Male" : "Female"));
                GUI.Label(new Rect(10f, 475f, 150f, 20f), "Favorite Room: " + Main.ghostInfo.field_Public_LevelRoom_0.field_Public_String_0);
                GUI.Label(new Rect(10f, 505f, 150f, 20f), "Can Hunt: " + Main.ghostAI.field_Public_Boolean_2.ToString());
                GUI.Label(new Rect(10f, 520f, 150f, 20f), "Hunting: " + Main.ghostAI.field_Public_Boolean_3.ToString());
            }
            if (LevelController.field_Public_Static_LevelController_0 != null)
            {
                GUI.Label(new Rect(10f, 490f, 150f, 20f), "Current Room: " + LevelController.field_Public_Static_LevelController_0.field_Public_LevelRoom_1.field_Public_String_0);
            }
        }

        private static void missionInfo()
        {
            if (Main.missionManager != null)
            {
                GUI.Label(new Rect(2010f, 450f, 850f, 20f), Main.missionManager.mainMissionText.text.ToString());
                GUI.Label(new Rect(2010f, 470f, 850f, 20f), Main.missionManager.sideMissionText.text.ToString());
                GUI.Label(new Rect(2010f, 490f, 850f, 20f), Main.missionManager.side2MissionText.text.ToString());
                GUI.Label(new Rect(2010f, 510f, 850f, 20f), Main.missionManager.hiddenMissionText.text.ToString());
            }
        }

        private static void playerInfo()
        {
            if (Main.player != null)
            {
                GUI.Label(new Rect(10f, 115f, 150f, 20f), "Hunted: " + Main.player.field_Public_Boolean_0.ToString());
                float sanity = (float)(Math.Round(Main.player.field_Public_Single_0 * 100f) / 100f);
                GUI.Label(new Rect(10f, 130f, 150f, 20f), "Sanity: " + (100 - sanity));
            }
            if (LevelController.field_Public_Static_LevelController_0 != null)
            {
                GUI.Label(new Rect(10f, 145f, 150f, 20f), "Current Room: " + LevelController.field_Public_Static_LevelController_0.field_Public_LevelRoom_2.field_Public_String_0);
            }
        }
    }
}
