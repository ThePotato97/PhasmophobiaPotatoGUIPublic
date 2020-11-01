using MelonLoader;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnhollowerBaseLib;
using System.Reflection;
using Harmony;
using UnityEngine.AI;
using UnhollowerRuntimeLib;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Threading.Tasks;

[assembly: MelonInfo(typeof(PhasmophobiaPotatoGUI.Main), "PhasmophobiaPotatoGUI", "1.0", "github.com/ThePotato97")]
[assembly: MelonGame("Kinetic Games", "Phasmophobia")]

namespace PhasmophobiaPotatoGUI
{
    public class Main : MelonMod
    {
        public string luigiBoardName;
        public bool guiEnabled = true;
        public bool menuEnabled;

        public static ServerManager serverManager;

        // Token: 0x04000039 RID: 57
        public static GhostAI ghostAI;

        // Token: 0x0400003A RID: 58
        public static List<GhostAI> ghosts;

        // Token: 0x0400003B RID: 59
        public static ExitLevel exitLevel;

        // Token: 0x0400003C RID: 60
        public static GhostInfo ghostInfo;

        // Token: 0x0400003D RID: 61
        public static HandCamera handCamera;

        // Token: 0x0400003E RID: 62
        public static InventoryManager inventoryManager;

        // Token: 0x0400003F RID: 63
        public static LiftButton liftButton;

        // Token: 0x04000040 RID: 64
        public static GameController gameController;

        // Token: 0x04000041 RID: 65
        public static ItemSpawner itemSpawner;

        // Token: 0x04000042 RID: 66
        public static Player player;

        // Token: 0x04000043 RID: 67
        public static OuijaBoard ouijaBoard;

        // Token: 0x04000044 RID: 68
        public static List<OuijaBoard> ouijaBoards;

        // Token: 0x04000045 RID: 69
        public static ObjectPublicGhNaPhSiPlBoPlSiSiUnique huntingstate;

        // Token: 0x04000046 RID: 70
        public static GhostInteraction ghostInteraction;

        // Token: 0x04000048 RID: 72
        public static PCMenu pCMenu;

        // Token: 0x04000049 RID: 73
        public static WalkieTalkie walkieTalkie;

        // Token: 0x0400004A RID: 74
        public static PhotonView photonView;

        // Token: 0x0400004B RID: 75
        public static Light light;

        // Token: 0x0400004C RID: 76
        public static GhostAudio ghostAudio;

        // Token: 0x0400004E RID: 78
        public static Contract contract;

        // Token: 0x0400004F RID: 79
        public static LevelSelectionManager levelSelectionManager;

        // Token: 0x04000050 RID: 80
        public static List<LightSwitch> lightSwitches;

        // Token: 0x04000051 RID: 81
        public static LightSwitch lightSwitch;

        // Token: 0x04000052 RID: 82
        public static Rigidbody rigidbody;

        // Token: 0x04000053 RID: 83

        // Token: 0x04000054 RID: 84
        public static GhostEventPlayer ghostEventPlayer;

        // Token: 0x04000055 RID: 85
        public static GhostController ghostController;

        // Token: 0x04000056 RID: 86
        public static DeadPlayer deadPlayer;

        // Token: 0x04000057 RID: 87
        public static ValueTypePublicSealedObInBoStInBoInInInUnique ghostTraits;

        // Token: 0x04000058 RID: 88
        public static LevelController levelController;

        // Token: 0x04000059 RID: 89
        public static FuseBox fuseBox;

        // Token: 0x0400005A RID: 90
        public static List<Torch> torches;

        // Token: 0x0400005B RID: 91
        public static List<Door> doors;

        // Token: 0x0400005C RID: 92
        public static List<Light> lights;

        // Token: 0x0400005D RID: 93
        public static List<Contract> contracts;

        // Token: 0x0400005E RID: 94
        public static List<InventoryItem> items;

        public static List<Player> players;

        // Token: 0x0400005F RID: 95
        public static List<FriendInfo> friends;

        // Token: 0x04000060 RID: 96
        public static List<Key> keys;

        // Token: 0x04000061 RID: 97
        public static List<DNAEvidence> dNAEvidences;

        // Token: 0x0400001D RID: 29
        private bool ShowInfoPlayer = true;

        // Token: 0x0400001E RID: 30
        private bool ShowInfoGhost = true;

        public override void OnApplicationStart()
        {
            string curSceneName = SceneManager.GetActiveScene().name.ToLower();
            while (!curSceneName.Contains("menu") && !curSceneName.Contains("new")) //check if main menu scene is fully loaded
            {
                new Thread(delegate ()
                {
                    for (; ; )
                    {
                        LoadObjects();
                        Thread.Sleep(5000);
                    }
                }).Start();
                break; //break
            }
            var list = new List<string> { "Luigi", "Weegee", "Ouija", "Lauigi", "Wega", "Weegi", "Oiji", "Ooija", "Weggy", "Lauigi" };
            int randomInt = new System.Random().Next(list.Count);
            luigiBoardName = list[randomInt];
        }

        //[HarmonyPatch(typeof(PhotonNetwork), "CreateRoom")]
        //private class CreateRoomPatch
        //{
        //    private static bool Prefix(ref string roomName, RoomOptions roomOptions)
        //    {
        //        MelonLogger.Log("created room: " + roomName);

        //        roomOptions.MaxPlayers = 0;
        //        MelonLogger.Log("Max Players: " + roomOptions.MaxPlayers);
        //        roomOptions.IsVisible = false;
        //        return true;
        //    }
        //}

        //[HarmonyPatch(typeof(LobbyManager), "JoinServer")]
        //private class JoinServerPatch
        //{
        //    private static bool Prefix(RoomInfo info, LobbyManager __instance)
        //    {
        //        //if (XRDevice.isPresent)
        //        //{
        //        //    __instance.SaveVRPlayerPositions();
        //        //}
        //        //else
        //        //{
        //        //    __instance.SavePCPlayerPositions();
        //        //}
        //        MelonLogger.Log("Joining Room: " + info.Name);
        //        PhotonNetwork.JoinRoom(info.Name, null);
        //        return false;
        //    }
        //}

        //[HarmonyPatch(typeof(LobbyManager), "JoinServerByName")]
        //private class JoinServerByNamePatch
        //{
        //    private static bool Prefix(string serverName, LobbyManager __instance)
        //    {
        //        //if (XRDevice.isPresent)
        //        //{
        //        //    __instance.SaveVRPlayerPositions();
        //        //}
        //        //else
        //        //{
        //        //    __instance.SavePCPlayerPositions();
        //        //}
        //        MelonLogger.Log("Joining Room: " + serverName);
        //        PhotonNetwork.JoinRoom(serverName);
        //        return false;
        //    }
        //}

        //[HarmonyPatch(typeof(RoomInfo), "PlayerCount", MethodType.Getter)]
        //private class PlayerCountPatch
        //{
        //    [HarmonyPostfix]
        //    public static void Postfix(ref int __result)
        //    {
        //        __result = 1;
        //    }
        //}

        public static Player GetLocalPlayer()
        {
            if (players == null || players.Count == 0)
            {
                return null;
            }
            else if (players.Count == 1)
            {
                return players[0];
            }
            else
            {
                foreach (Player player in players)
                {
                    if (player != null)
                    {
                        if (player.field_Public_PhotonView_0 != null)
                        {
                            if (player.field_Public_PhotonView_0.AmOwner)
                            {
                                return player;
                            }
                        }
                    }
                }

                return null;
            }
        }

        //public static List<Player> GetAllPlayers()
        //{
        //    if (players == null || players.Length == 0)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return players.ToList();
        //    }
        //}

        public override void OnUpdate()
        {
            var kb = Keyboard.current;
            bool insert = kb.insertKey.wasPressedThisFrame;
            if (insert)
            {
                menuEnabled = !menuEnabled;
                if (!menuEnabled)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = false;
                }
                if (menuEnabled)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
            bool delete = kb.deleteKey.wasPressedThisFrame;
            if (delete)
            {
                guiEnabled = !guiEnabled;
            }
        }

        // ghosttraits.type

        private string GetGhostType(ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique type)
        {
            switch (type)
            {
                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.none:
                    return "None";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Spirit:
                    return " <color=red>Spirit</color>: <color=green>Fingerprint</color> <color=white>|</color> <color=green>Writing</color> <color=white>|</color> <color=green>Box</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Wraith:
                    return " <color=red>Wraith</color>: <color=green>Fingerprint</color> <color=white>|</color> <color=green>Temps</color> <color=white>|</color> <color=green>Box</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Phantom:
                    return " <color=red>Phantom</color>: <color=green>EMF5</color> <color=white>|</color> <color=green>Temps</color> <color=white>|</color> <color=green>Orbs</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Poltergeist:
                    return " <color=red>Poltergeist</color>: <color=green>Fingerprint</color> <color=white>|</color> <color=green>Orbs</color> <color=white>|</color> <color=green>Box</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Banshee:
                    return " <color=red>Banshee</color>: <color=green>EMF5</color> <color=white>|</color> <color=green>Fingerprint</color> <color=white>|</color> <color=green>Temps</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Jinn:
                    return " <color=red>Jinn</color>: <color=green>EMF5</color> <color=white>|</color> <color=green>Orbs</color> <color=white>|</color> <color=green>Box</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Mare:
                    return " <color=red>Mare</color>: <color=green>Temps</color> <color=white>|</color> <color=green>Orbs</color> <color=white>|</color> <color=green>Box</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Revenant:
                    return " <color=red>Revenant</color>: <color=green>EMF5</color> <color=white>|</color> <color=green>Fingerprints</color> <color=white>|</color> <color=green>Writing</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Shade:
                    return " <color=red>Shade</color>: <color=green>EMF5</color> <color=white>|</color> <color=green>Orbs</color> <color=white>|</color> <color=green>Writing</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Demon:
                    return " <color=red>Demon</color>: <color=green>Temps</color> <color=white>|</color> <color=green>Writing</color> <color=white>|</color> <color=green>Box</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Yurei:
                    return " <color=red>Yurei</color>: <color=green>Temps</color> <color=white>|</color> <color=green>Orbs</color> <color=white>|</color> <color=green>Writing</color>";

                case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Oni:
                    return " <color=red>Oni</color>: <color=green>EMF5</color> <color=white>|</color> <color=green>Writing</color> <color=white>|</color> <color=green>Box</color>";

                default:
                    return "";
            }
        }

        private bool GhostESP = true;

        // Token: 0x04000012 RID: 18
        private bool PlayerESP = true;

        // Token: 0x04000013 RID: 19
        private bool KeyESP = true;

        // Token: 0x04000014 RID: 20
        private bool OuijaESP = true;

        // Token: 0x04000015 RID: 21
        private bool EvidenceESP = true;

        //private string roomName;
        //private string steamID;
        private bool isPrivateServer;

        private string serverName;
        private float serverSlots;
        //private bool antikick;

        private void GhostESPFunc()
        {
            if (gameController != null && ghostAI != null)
            {
                if (this.GhostESP)
                {
                    foreach (GhostAI ghostAI in ghosts)
                    {
                        Vector3 vector = Camera.main.WorldToScreenPoint(ghostAI.transform.position);
                        if (vector.z > 0f)
                        {
                            vector.y = (float)Screen.height - (vector.y + 1f);
                            GUI.color = Color.red;
                            GUI.DrawTexture(new Rect(new Vector2(vector.x, vector.y), new Vector2(5f, 5f)), Texture2D.whiteTexture, 0);
                            GUI.Label(new Rect(new Vector2(vector.x, vector.y), new Vector2(100f, 100f)), ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique_0.ToString());
                        }
                    }
                }
            }
        }

        private void PlayerESPFunc()
        {
            if (this.PlayerESP)
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

        private void KeyESPFunc()
        {
            if (keys != null && this.KeyESP)
            {
                foreach (Key key in keys)
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

        private void OuijaESPFunc()
        {
            if (ouijaBoard != null && this.OuijaESP)
            {
                foreach (OuijaBoard ouijaBoard in ouijaBoards)
                {
                    Vector3 vector5 = Camera.main.WorldToScreenPoint(ouijaBoard.transform.position);
                    if (vector5.z > 0f)
                    {
                        vector5.y = (float)Screen.height - (vector5.y + 1f);
                        GUI.color = Color.yellow;
                        GUI.Label(new Rect(new Vector2(vector5.x, vector5.y), new Vector2(100f, 100f)), luigiBoardName + " Board");
                    }
                }
            }
        }

        private void BoneESP()
        {
            if (dNAEvidences != null && this.EvidenceESP)
            {
                foreach (DNAEvidence dnaevidence in dNAEvidences)
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

        private void LoadObjects()
        {
            if (SceneManager.sceneLoaded != null)
            {
                Main.gameController = GameObject.FindObjectOfType<GameController>();

                Main.levelController = GameObject.FindObjectOfType<LevelController>();

                Main.levelSelectionManager = GameObject.FindObjectOfType<LevelSelectionManager>();

                Main.walkieTalkie = GameObject.FindObjectOfType<WalkieTalkie>();

                Main.handCamera = GameObject.FindObjectOfType<HandCamera>();

                Main.inventoryManager = GameObject.FindObjectOfType<InventoryManager>();

                Main.liftButton = GameObject.FindObjectOfType<LiftButton>();

                Main.contract = GameObject.FindObjectOfType<Contract>();

                Main.pCMenu = GameObject.FindObjectOfType<PCMenu>();

                Main.exitLevel = GameObject.FindObjectOfType<ExitLevel>();

                Main.ghostAI = GameObject.FindObjectOfType<GhostAI>();

                Main.lightSwitch = GameObject.FindObjectOfType<LightSwitch>();

                Main.light = GameObject.FindObjectOfType<Light>();

                Main.dNAEvidences = Enumerable.ToList<DNAEvidence>(GameObject.FindObjectsOfType<DNAEvidence>());

                Main.contracts = Enumerable.ToList<Contract>(GameObject.FindObjectsOfType<Contract>());

                Main.items = Enumerable.ToList<InventoryItem>(GameObject.FindObjectsOfType<InventoryItem>());

                Main.players = Enumerable.ToList<Player>(GameObject.FindObjectsOfType<Player>());

                Main.photonView = GameObject.FindObjectOfType<PhotonView>();

                Main.ghostInfo = GameObject.FindObjectOfType<GhostInfo>();

                Main.deadPlayer = GameObject.FindObjectOfType<DeadPlayer>();

                Main.player = GameObject.FindObjectOfType<Player>();

                Main.rigidbody = GameObject.FindObjectOfType<Rigidbody>();

                Main.itemSpawner = GameObject.FindObjectOfType<ItemSpawner>();

                Main.ghostInteraction = GameObject.FindObjectOfType<GhostInteraction>();

                //Main.baseController = GameObject.FindObjectOfType<BaseController>();

                Main.ouijaBoard = GameObject.FindObjectOfType<OuijaBoard>();

                Main.ouijaBoards = Enumerable.ToList<OuijaBoard>(GameObject.FindObjectsOfType<OuijaBoard>());

                Main.keys = Enumerable.ToList<Key>(GameObject.FindObjectsOfType<Key>());

                Main.ghosts = Enumerable.ToList<GhostAI>(GameObject.FindObjectsOfType<GhostAI>());

                Main.serverManager = GameObject.FindObjectOfType<ServerManager>();

                Main.torches = Enumerable.ToList<Torch>(GameObject.FindObjectsOfType<Torch>());

                Main.ghostAudio = GameObject.FindObjectOfType<GhostAudio>();

                Main.fuseBox = GameObject.FindObjectOfType<FuseBox>();

                Main.doors = Enumerable.ToList<Door>(GameObject.FindObjectsOfType<Door>());

                Main.lightSwitches = Enumerable.ToList<LightSwitch>(GameObject.FindObjectsOfType<LightSwitch>());
            }
        }

        private void HUD()
        {
            GUI.color = Color.white;
            GUI.Label(new Rect(10f, 70f, 150f, 20f), "Potato GUI (Public)");
            if (ShowInfoPlayer)
            {
                //GUI.Label(new Rect(10f, 100f, 150f, 20f), "Name: " + PhotonNetwork.NickName.ToString());
                //if (!PhotonNetwork.IsMasterClient)
                //{
                //    GUI.Label(new Rect(10f, 85f, 150f, 20f), "Master: " + PhotonNetwork.MasterClient.NickName.ToString());
                //}
                //if (PhotonNetwork.IsMasterClient)
                //{
                //    GUI.Label(new Rect(10f, 85f, 150f, 20f), "Master: You");
                //}
                if (Main.gameController != null)
                {
                    GUI.Label(new Rect(10f, 115f, 150f, 20f), "Hunted: " + Main.player.field_Public_Boolean_0.ToString());
                    float sanity = (float)(Math.Round(Main.player.field_Public_Single_0 * 100f) / 100f);
                    GUI.Label(new Rect(10f, 130f, 150f, 20f), "Sanity: " + (100 - sanity));
                    GUI.Label(new Rect(10f, 145f, 150f, 20f), "Current Room: " + LevelController.field_Public_Static_LevelController_0.field_Public_LevelRoom_2.field_Public_String_0);
                }
            }
            if (ShowInfoGhost)
            {
                //-------------------------------------------------------------------------------------------\\
                if (ghostAI != null)
                {
                    GUI.Label(new Rect(10f, 400f, 150f, 20f), "Ghost Age: " + Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_Int32_0.ToString());
                    GUI.Label(new Rect(10f, 415f, 850f, 20f), "Ghost Name: " + (Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_String_0.ToString()) + (Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_Boolean_1 ? " (Shy)" : ""));
                    GUI.Label(new Rect(10f, 430f, 850f, 20f), "Ghost Type: " + GetGhostType(Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique_0));
                    GUI.Label(new Rect(10f, 460f, 150f, 20f), "Ghost Gender: " + (Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_Boolean_0 ? "Male" : "Female"));
                    GUI.Label(new Rect(10f, 475f, 150f, 20f), "Favorite Room: " + Main.ghostInfo.field_Public_LevelRoom_0.field_Public_String_0);
                    GUI.Label(new Rect(10f, 490f, 150f, 20f), "Current Room: " + LevelController.field_Public_Static_LevelController_0.field_Public_LevelRoom_1.field_Public_String_0);
                    GUI.Label(new Rect(10f, 505f, 150f, 20f), "Can Hunt: " + Main.ghostAI.field_Public_Boolean_2.ToString());
                    GUI.Label(new Rect(10f, 520f, 150f, 20f), "Hunting: " + Main.ghostAI.field_Public_Boolean_3.ToString());
                }
            }
            //--------------------------------------------------------------------------------------------\\
        }

        // Token: 0x04000033 RID: 51

        // Token: 0x04000034 RID: 52
        public Rect dropDownRect = new Rect(1420f, 0f, 200f, 300f);

        // Token: 0x04000035 RID: 53
        public Rect dropDownRect2 = new Rect(820f, 0f, 200f, 300f);

        // Token: 0x04000036 RID: 54
        //private GUIStyle guiStyle = new GUIStyle();

        //private int selecteditem;
        //private bool showItemList;

        private void ShitIStoleFromYude2000()
        {
            //    string[] allitems = new string[]
            //{
            //    "BasementKey",
            //    "CarKey",
            //    "GarageKey",
            //    "MainKey",
            //    "Blacklight Flashlight",
            //    "Glowstick",
            //    "Flashlight",
            //    "StrongFlashlight",
            //    "EMF Reader",
            //    "EVP Recorder",
            //    "Thermometer",
            //    "IR Light Sensor",
            //    "Motion Sensor",
            //    "SoundSensor",
            //    "Ghost Writing Book",
            //    "Parabolic Microphone",
            //    "DSLRCamera",
            //    "Tripod",
            //    "Head Mounted Camera",
            //    "Candle",
            //    "Crucifix",
            //    "Lighter",
            //    "SaltShaker",
            //    "Bone",
            //    "Ouija board",
            //    "PainKillers",
            //    "WhiteSage",
            //    "Hellephant",
            //    "Hanging Body",
            //    "monsterprefab",
            //    "WiccanAltar",
            //    "ZomBear",
            //    "ZomBunny",
            //    "ButcherKnife",
            //    "HumanSkull",
            //    "VHS Tape",
            //    "TarotCardBox",
            //    "VoodooDoll",
            //    "My Robot Kyle",
            //    "Robot Kyle 2D",
            //    "Robot Kyle Mecanim",
            //    "Robot Kyle RPG",
            //    "Bug",
            //    "GhostOrb",
            //    "EMF Spot",
            //    "Footstep",
            //    "PhotoPaper",
            //    "Noise Spot",
            //    "SaltSpot",
            //    "SanitySoundSpot",
            //    "TornCloth",
            //    "Camera",
            //    "Walkie Talkie",
            //    "BoxFlashPrefab",
            //    "Boy",
            //    "muktargame",
            //    "BoxPrefab",
            //    "Environment"
            //};

            if (!PhotonNetwork.InRoom)
            {
                GUI.Label(new Rect(720f, 0f, 200f, 20f), "Custom Room Creator:");
                this.serverName = GUI.TextArea(new Rect(720f, 25f, 200f, 20f), this.serverName);
                this.serverSlots = GUI.HorizontalSlider(new Rect(720f, 50f, 200f, 20f), (float)((int)this.serverSlots), 4f, 90f);
                GUI.Label(new Rect(720f, 65f, 200f, 20f), "Slots: " + ((int)this.serverSlots).ToString());
                if (GUI.Toggle(new Rect(720f, 80f, 200f, 20f), this.isPrivateServer, "Private Room") != this.isPrivateServer)
                {
                    this.isPrivateServer = !isPrivateServer;
                }
                if (GUI.Button(new Rect(720f, 105f, 200f, 20f), "Create Custom Room"))
                {
                    if (this.isPrivateServer)
                    {
                        PlayerPrefs.SetInt("isPublicServer", 0);
                        RoomOptions roomOptions = new RoomOptions
                        {
                            IsOpen = true,
                            IsVisible = false,
                            MaxPlayers = Convert.ToByte((int)this.serverSlots),
                            PlayerTtl = 2000
                        };
                        PhotonNetwork.CreateRoom(UnityEngine.Random.Range(0, 999999).ToString("000000"), roomOptions, TypedLobby.Default);
                    }
                    if (!this.isPrivateServer)
                    {
                        PlayerPrefs.SetInt("isPublicServer", 1);
                        RoomOptions roomOptions2 = new RoomOptions
                        {
                            IsOpen = true,
                            IsVisible = true,
                            MaxPlayers = Convert.ToByte((int)this.serverSlots),
                            PlayerTtl = 2000
                        };
                        PhotonNetwork.CreateRoom(this.serverName + "#" + UnityEngine.Random.Range(0, 999999).ToString("000000"), roomOptions2, TypedLobby.Default);
                    }
                }
            }
            if (Main.levelController != null)
            {
                if (GUI.Button(new Rect(520f, 120f, 200f, 20f), "Random Event") && Main.ghostAI != null)
                {
                    Main.ghostAI.field_Public_GhostActivity_0.InteractWithARandomDoor();
                    Main.ghostAI.field_Public_GhostActivity_0.InteractWithARandomProp();
                    Main.ghostAI.field_Public_GhostActivity_0.Interact();
                    Main.ghostAI.RandomEvent();
                }
                //if (GUI.Button(new Rect(520f, 120f, 200f, 20f), "Sound"))
                //{
                //    Main.ghostAudio.PlaySound(1, false, false);
                //    Main.ghostAudio.PlaySound(0, false, false);
                //    Main.ghostInteraction.GetComponent<PhotonView>().RPC("SpawnFootstepNetworked", 0, new object[]
                //    {
                //        this.MyPlayer.transform.position,
                //        this.MyPlayer.transform.rotation,
                //        Random.Range(0, 2)
                //    });
                //}

                if (GUI.Button(new Rect(520f, 145f, 200f, 20f), "Wander"))
                {
                    Main.ghostAI.field_Public_Boolean_6 = true;
                    Main.ghostAI.field_Public_Animator_0.SetBool("isIdle", false);
                    Vector3 destination = Vector3.zero;
                    NavMeshHit navMeshHit;
                    if (NavMesh.SamplePosition(UnityEngine.Random.insideUnitSphere * 3f + Main.ghostAI.transform.position, out navMeshHit, 3f, 1))
                    {
                        destination = navMeshHit.position;
                    }
                    Main.ghostAI.field_Public_NavMeshAgent_0.destination = destination;
                    Main.ghostAI.ChangeState((GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique)1, null, null);
                    Main.ghostAI.field_Public_PhotonView_0.RPC("Hunting", 0, new Il2CppSystem.Object[]
                    {
                        new Il2CppSystem.Boolean().BoxIl2CppObject()
                    });
                    Main.ghostAI.field_Public_PhotonView_0.RPC("SyncChasingPlayer", 0, new Il2CppSystem.Object[]
                    {
                        new Il2CppSystem.Boolean().BoxIl2CppObject()
                    });
                }
                if (GUI.Button(new Rect(520f, 170f, 200f, 20f), "Hunt"))
                {
                    SetupPhaseController.field_Public_Static_SetupPhaseController_0.field_Public_Boolean_0 = false;
                    Main.ghostAI.field_Public_Boolean_4 = true;
                    Main.ghostAI.field_Public_Boolean_2 = true;
                    Main.ghostAI.field_Public_Animator_0.SetBool("isIdle", false);
                    Main.ghostAI.field_Public_Animator_0.SetInteger("WalkType", 1);
                    Main.ghostAI.field_Public_NavMeshAgent_0.speed = Main.ghostAI.field_Public_Single_0;
                    Main.ghostAI.field_Public_GhostInteraction_0.CreateAppearedEMF(Main.ghostAI.transform.position);
                    Vector3 destination2 = Vector3.zero;
                    float num = UnityEngine.Random.Range(2f, 15f);

                    NavMeshHit navMeshHit2;

                    if (NavMesh.SamplePosition(UnityEngine.Random.insideUnitSphere * num + Main.ghostAI.transform.position, out navMeshHit2, num, 1))
                    {
                        destination2 = navMeshHit2.position;
                    }
                    else
                    {
                        destination2 = Vector3.zero;
                    }
                    Main.ghostAI.field_Public_NavMeshAgent_0.SetDestination(destination2);
                    SetupPhaseController.field_Public_Static_SetupPhaseController_0.ForceEnterHuntingPhase();
                    Main.ghostAI.ChangeState((GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique)2, null, null);
                    Main.ghostAI.field_Public_PhotonView_0.RPC("Hunting", 0, new Il2CppSystem.Object[]
                    {
                        new Il2CppSystem.Boolean(){ m_value = true}.BoxIl2CppObject()
                    });
                    Main.ghostAI.field_Public_PhotonView_0.RPC("SyncChasingPlayer", 0, new Il2CppSystem.Object[]
                    {
                        new Il2CppSystem.Boolean(){ m_value = true}.BoxIl2CppObject()
                    });
                }
                if (GUI.Button(new Rect(520f, 195f, 200f, 20f), "Idle"))
                {
                    Main.ghostAI.field_Public_Animator_0.SetInteger("IdleNumber", UnityEngine.Random.Range(0, 2));
                    Main.ghostAI.field_Public_Animator_0.SetBool("isIdle", true);
                    Main.ghostAI.UnAppear(false);
                    Main.ghostAI.field_Public_GhostAudio_0.TurnOnOrOffAppearSource(false);
                    Main.ghostAI.field_Public_GhostAudio_0.PlayOrStopAppearSource(false);
                    Main.ghostAI.ChangeState(0, null, null);
                    Main.ghostAI.field_Public_PhotonView_0.RPC("Hunting", 0, new Il2CppSystem.Object[]
                    {
                        new Il2CppSystem.Boolean().BoxIl2CppObject()
                    });
                    Main.ghostAI.field_Public_PhotonView_0.RPC("SyncChasingPlayer", 0, new Il2CppSystem.Object[]
                    {
                        new Il2CppSystem.Boolean().BoxIl2CppObject()
                    });
                }
                Il2CppSystem.Int32 appearRand = new Il2CppSystem.Int32();

                appearRand.m_value = UnityEngine.Random.Range(0, 3);

                if (GUI.Button(new Rect(520f, 220f, 200f, 20f), "Appear"))
                {
                    Main.ghostAI.field_Public_PhotonView_0.RPC("MakeGhostAppear", 0, new Il2CppSystem.Object[]
                    {
                        new Il2CppSystem.Boolean(){ m_value = true}.BoxIl2CppObject(),
                        new Il2CppSystem.Int32(){ m_value = UnityEngine.Random.Range(0, 3) }.BoxIl2CppObject()
                    });
                }
                if (GUI.Button(new Rect(520f, 245f, 200f, 20f), "Unappear"))
                {
                    Main.ghostAI.field_Public_PhotonView_0.RPC("MakeGhostAppear", 0, new Il2CppSystem.Object[]
                    {
                        new Il2CppSystem.Boolean().BoxIl2CppObject(),
                        new Il2CppSystem.Int32(){ m_value = UnityEngine.Random.Range(0, 3) }.BoxIl2CppObject()
                    });
                }
                //if (GUI.Toggle(new Rect(1120f, 325f, 200f, 20f), this.showItemList, "Show Item Spawner") != this.showItemList)
                //{
                //    this.showItemList = !this.showItemList;
                //}

                //if (this.showItemList)
                //{
                //    GUI.Label(new Rect(520f, 225f, 200f, 20f), "Item Spawner:");
                //    this.scrollViewVector = GUI.BeginScrollView(new Rect(this.dropDownRect2.x - 100f, this.dropDownRect2.y + 25f, this.dropDownRect2.width, this.dropDownRect2.height), this.scrollViewVector, new Rect(0f, 0f, this.dropDownRect2.width, Mathf.Max(this.dropDownRect2.height, (float)(allitems.Length * 25))));
                //    GUI.Box(new Rect(0f, 0f, this.dropDownRect2.width, Mathf.Max(this.dropDownRect2.height, (float)(allitems.Length * 25))), "");
                //    for (int l = 0; l < allitems.Length; l++)
                //    {
                //        if (GUI.Button(new Rect(0f, (float)(l * 25), this.dropDownRect2.height, 25f), ""))
                //        {
                //            this.selecteditem = l;
                //            if (PhotonNetwork.InRoom)
                //            {
                //                MelonLogger.Log(allitems[this.selecteditem].ToString());
                //                PhotonNetwork.Instantiate(allitems[this.selecteditem], MainManager.field_Public_Static_MainManager_0.field_Public_Player_0.transform.position, Quaternion.identity, 0, null);
                //            }
                //        }
                //        GUI.Label(new Rect(5f, (float)(l * 25), this.dropDownRect2.height, 25f), allitems[l]);
                //    }
                //    GUI.EndScrollView();
                //}

                GUI.Label(new Rect(920f, 295f, 200f, 20f), "ESP:");
                if (GUI.Toggle(new Rect(920f, 320f, 200f, 20f), this.GhostESP, "Ghost") != this.GhostESP)
                {
                    this.GhostESP = !this.GhostESP;
                }
                if (GUI.Toggle(new Rect(920f, 370f, 200f, 20f), this.PlayerESP, "Player") != this.PlayerESP)
                {
                    this.PlayerESP = !this.PlayerESP;
                }
                if (GUI.Toggle(new Rect(920f, 395f, 200f, 20f), this.OuijaESP, "Ouija Board") != this.OuijaESP)
                {
                    this.OuijaESP = !this.OuijaESP;
                }
                if (GUI.Toggle(new Rect(920f, 420f, 200f, 20f), this.KeyESP, "Key") != this.KeyESP)
                {
                    this.KeyESP = !this.KeyESP;
                }
                if (GUI.Toggle(new Rect(920f, 445f, 200f, 20f), this.EvidenceESP, "Evidence") != this.EvidenceESP)
                {
                    this.EvidenceESP = !this.EvidenceESP;
                }

                if (GUI.Toggle(new Rect(1120f, 300f, 200f, 20f), this.ShowInfoGhost, "Show Ghost Info") != this.ShowInfoGhost)
                {
                    this.ShowInfoGhost = !this.ShowInfoGhost;
                }
                if (GUI.Toggle(new Rect(1120f, 250f, 200f, 20f), this.ShowInfoPlayer, "Show Player Info") != this.ShowInfoPlayer)
                {
                    this.ShowInfoPlayer = !this.ShowInfoPlayer;
                }
            }
            if (Main.levelController == null)
            {
                GUI.SetNextControlName("changename");
                this.Name = GUI.TextArea(new Rect(320f, 25f, 200f, 20f), this.Name);
                if (GUI.Button(new Rect(320f, 45f, 200f, 20f), "Change Name"))
                {
                    GUI.FocusControl("changename");
                    PhotonNetwork.NickName = this.Name;
                }

                if (GUI.Button(new Rect(320f, 145f, 200f, 20f), "Force Start"))
                {
                    Main.serverManager.StartGame();
                }

                if (GUI.Button(new Rect(520f, 50f, 200f, 20f), "Add 100$"))
                {
                    FileBasedPrefs.SetInt("PlayersMoney", FileBasedPrefs.GetInt("PlayersMoney", 0) + 100);
                }
                if (GUI.Button(new Rect(520f, 75f, 200f, 20f), "Add 1 Level"))
                {
                    FileBasedPrefs.SetInt("myTotalExp", FileBasedPrefs.GetInt("myTotalExp", 0) + 100);
                }

                //GUI.Label(new Rect(920f, 0f, 200f, 20f), "Join Room:");
                //this.roomName = GUI.TextArea(new Rect(920f, 25f, 200f, 20f), this.roomName);
                //this.steamID = GUI.TextArea(new Rect(920f, 50f, 200f, 20f), this.steamID);
                //if (GUI.Button(new Rect(920f, 75f, 200f, 20f), "Join Room by Name"))
                //{
                //    PhotonNetwork.JoinRoom(this.roomName);
                //}
                //if (GUI.Button(new Rect(920f, 100f, 200f, 20f), "Join Room by ID"))
                //{
                //    object[] FriendIDList;

                //    bool test = PhotonNetwork.FindFriends(new string[]
                //    {
                //            this.steamID
                //    });
                //    MelonLogger.Log("steamID = " + steamID);
                //    //foreach (FriendInfo friendInfo in PhotonNetwork.Friends)
                //    //{
                //    //PhotonNetwork.JoinRoom(friendInfo.Room);
                //    //}
                //}
            }
        }

        private string Name;

        public override void OnGUI()
        {
            if (guiEnabled)
            {
                HUD();
                BoneESP();
                OuijaESPFunc();
                GhostESPFunc();
                KeyESPFunc();
                //PlayerESPFunc();

                if (menuEnabled)
                {
                    ShitIStoleFromYude2000();
                }
            }
        }
    }
}