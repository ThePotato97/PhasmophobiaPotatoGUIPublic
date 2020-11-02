using MelonLoader;
using UnityEngine;

namespace PhasmophobiaPotatoGUI
{
    public class ESPMethods
    {
        public static void drawESP()
        {
            if (Menu.GhostESP)
            {
                GhostESPFunc();
            }
            if (Menu.KeyESP)
            {
                KeyESPFunc();
            }
            if (Menu.OuijaESP)
            {
                OuijaESPFunc();
            }
            if (Menu.EvidenceESP)
            {
                BoneESP();
            }
            if (Menu.PlayerESP)
            {
                PlayerESPFunc();
            }
            if (Menu.FuseboxESP)
            {
                FuseBoxESPFunc();
            }
        }

        private static void GhostESPFunc()
        {
            if (Main.gameController != null && Main.ghostAI != null)
            {
                foreach (GhostAI ghostAI in Main.ghosts)
                {
                    Vector3 vector = Camera.main.WorldToScreenPoint(ghostAI.transform.position);
                    if (vector.z > 0f)
                    {
                        vector.y = (float)Screen.height - (vector.y + 1f);
                        GUI.color = Color.red;
                        GUI.DrawTexture(new Rect(new Vector2(vector.x, vector.y), new Vector2(5f, 5f)), Texture2D.whiteTexture, 0);
                        GUI.Label(new Rect(new Vector2(vector.x, vector.y), new Vector2(100f, 100f)), Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique_0.ToString());
                    }
                }
            }
        }

        private static void PlayerESPFunc()
        {
            if (Menu.PlayerESP)
            {
                Player[] players = UnityEngine.Object.FindObjectsOfType<Player>();

                foreach (Player player in players)
                {
                    UnityEngine.Object playerObject = player;

                    MelonLogger.Log("Drawing ESP for " + player.field_Public_PhotonView_0.owner.NickName);

                    Vector3 vector2 = Camera.main.WorldToScreenPoint(player.transform.position);
                    if (vector2.z > 0f)
                    {
                        vector2.y = (float)Screen.height - (vector2.y + 1f);
                        GUI.color = Color.green;
                        GUI.DrawTexture(new Rect(new Vector2(vector2.x, vector2.y), new Vector2(5f, 5f)), Texture2D.whiteTexture, 0);
                        GUI.Label(new Rect(new Vector2(vector2.x, vector2.y), new Vector2(100f, 100f)), player.field_Public_PhotonView_0.owner.NickName);
                    }
                }
            }
        }

        private static void KeyESPFunc()
        {
            if (Main.keys != null && Menu.KeyESP)
            {
                foreach (Key key in Main.keys)
                {
                    Vector3 vector3 = Camera.main.WorldToScreenPoint(key.transform.position);
                    if (vector3.z > 0f)
                    {
                        vector3.y = (float)Screen.height - (vector3.y + 1f);
                        GUI.color = Color.cyan;
                        GUI.Label(new Rect(new Vector2(vector3.x, vector3.y), new Vector2(100f, 100f)), key.field_Public_EnumNPublicSealedvamabagaCano6vUnique_0.ToString() + " Key");
                    }
                }
            }
        }

        private static void OuijaESPFunc()
        {
            if (Main.ouijaBoard != null && Menu.OuijaESP)
            {
                foreach (OuijaBoard ouijaBoard in Main.ouijaBoards)
                {
                    Vector3 vector5 = Camera.main.WorldToScreenPoint(ouijaBoard.transform.position);
                    if (vector5.z > 0f)
                    {
                        vector5.y = (float)Screen.height - (vector5.y + 1f);
                        GUI.color = Color.yellow;
                        GUI.Label(new Rect(new Vector2(vector5.x, vector5.y), new Vector2(100f, 100f)), Main.luigiBoardName + " Board");
                    }
                }
            }
        }

        private static void BoneESP()
        {
            if (Main.dNAEvidences != null)
            {
                foreach (DNAEvidence dnaevidence in Main.dNAEvidences)
                {
                    Vector3 vector4 = Camera.main.WorldToScreenPoint(dnaevidence.transform.position);
                    if (vector4.z > 0f)
                    {
                        vector4.y = (float)Screen.height - (vector4.y + 1f);
                        GUI.color = Color.magenta;
                        GUI.Label(new Rect(new Vector2(vector4.x, vector4.y), new Vector2(100f, 100f)), "Evidence");
                    }
                }
            }
        }
        private static void FuseBoxESPFunc()
        {
            if (Main.fuseBox != null)
            {

                Vector3 fuseBoxPos = Camera.main.WorldToScreenPoint(Main.fuseBox.transform.position);
                if (fuseBoxPos.z > 0f)
                {
                    fuseBoxPos.y = (float)Screen.height - (fuseBoxPos.y + 1f);
                    GUI.color = Color.green;
                    GUI.Label(new Rect(new Vector2(fuseBoxPos.x, fuseBoxPos.y), new Vector2(100f, 100f)), "Fusebox");
                }

            }
        }

    }
}