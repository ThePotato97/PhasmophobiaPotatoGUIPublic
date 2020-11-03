using Harmony;
using MelonLoader;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[assembly: MelonInfo(typeof(PhasmophobiaPotatoGUI.Main), "PhasmophobiaPotatoGUI", "1.0", "github.com/ThePotato97")]
[assembly: MelonGame("Kinetic Games", "Phasmophobia")]

namespace PhasmophobiaPotatoGUI
{
    public class Main : MelonMod
    {
        public static string luigiBoardName;
        public bool guiEnabled = true;
        public bool menuEnabled;
        public float reach;

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
        public static bool ShowInfoPlayer = true;

        public static MissionManager missionManager;
        
        public static Transform boneTransform;

        public static Animator playerAnim;

        private static object coRoutine;

        private static bool canRun = true;

        private static bool isRunning = true;
        
        public override void OnLevelWasInitialized(int level)
        {
            if (level == 1 && Main.canRun)
            {
                Main.canRun = false;
                new Thread(delegate ()
                {
                    while (Main.isRunning)
                    {
                        Main.coRoutine = MelonCoroutines.Start(LoadObjectsTask());
                        Thread.Sleep(5000);
                    }
                }).Start();
            }
            Main.initializedScene = level;
        }

        public IEnumerator LoadObjectsTask()
        {
            yield return new WaitForSeconds(0.15f);
            Main.missionManager = GameObject.FindObjectOfType<MissionManager>();
            yield return new WaitForSeconds(0.15f); ;
            Main.gameController = GameObject.FindObjectOfType<GameController>();
            yield return new WaitForSeconds(0.15f);
            Main.levelController = GameObject.FindObjectOfType<LevelController>();
            yield return new WaitForSeconds(0.15f); ;
            Main.levelSelectionManager = GameObject.FindObjectOfType<LevelSelectionManager>();
            yield return new WaitForSeconds(0.15f);
            Main.walkieTalkie = GameObject.FindObjectOfType<WalkieTalkie>();
            yield return new WaitForSeconds(0.15f);
            Main.handCamera = GameObject.FindObjectOfType<HandCamera>();
            yield return new WaitForSeconds(0.15f);
            Main.inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
            yield return new WaitForSeconds(0.15f);
            Main.liftButton = GameObject.FindObjectOfType<LiftButton>();
            yield return new WaitForSeconds(0.15f);
            Main.contract = GameObject.FindObjectOfType<Contract>();
            yield return new WaitForSeconds(0.15f);
            Main.pCMenu = GameObject.FindObjectOfType<PCMenu>();
            yield return new WaitForSeconds(0.15f);
            Main.exitLevel = GameObject.FindObjectOfType<ExitLevel>();
            yield return new WaitForSeconds(0.15f);
            Main.ghostAI = UnityEngine.Object.FindObjectOfType<GhostAI>();
            yield return new WaitForSeconds(0.15f);
            Main.lightSwitch = GameObject.FindObjectOfType<LightSwitch>();
            yield return new WaitForSeconds(0.15f);
            Main.light = GameObject.FindObjectOfType<Light>();
            yield return new WaitForSeconds(0.15f);
            Main.dNAEvidences = Enumerable.ToList<DNAEvidence>(GameObject.FindObjectsOfType<DNAEvidence>());
            yield return new WaitForSeconds(0.15f);
            Main.contracts = Enumerable.ToList<Contract>(GameObject.FindObjectsOfType<Contract>());
            yield return new WaitForSeconds(0.15f);
            Main.items = Enumerable.ToList<InventoryItem>(GameObject.FindObjectsOfType<InventoryItem>());
            yield return new WaitForSeconds(0.15f);
            Main.players = Enumerable.ToList<Player>(GameObject.FindObjectsOfType<Player>());
            yield return new WaitForSeconds(0.15f);
            if (Main.levelController != null)
            {
                Main.photonView = (Main.ghostAI.field_Public_PhotonView_0 ?? null);
                yield return new WaitForSeconds(0.15f);
            }
            Main.ghostInfo = GameObject.FindObjectOfType<GhostInfo>();
            yield return new WaitForSeconds(0.15f);
            Main.deadPlayer = GameObject.FindObjectOfType<DeadPlayer>();
            yield return new WaitForSeconds(0.15f);
            Main.player = GameObject.FindObjectOfType<Player>();
            yield return new WaitForSeconds(0.15f);
            Main.rigidbody = GameObject.FindObjectOfType<Rigidbody>();
            yield return new WaitForSeconds(0.15f);
            Main.itemSpawner = GameObject.FindObjectOfType<ItemSpawner>();
            yield return new WaitForSeconds(0.15f);
            Main.ghostInteraction = GameObject.FindObjectOfType<GhostInteraction>();
            yield return new WaitForSeconds(0.15f);
            //Main.baseController = GameObject.FindObjectOfType<BaseController>();
            yield return new WaitForSeconds(0.15f);
            Main.ouijaBoard = GameObject.FindObjectOfType<OuijaBoard>();
            yield return new WaitForSeconds(0.15f);
            Main.ouijaBoards = Enumerable.ToList<OuijaBoard>(GameObject.FindObjectsOfType<OuijaBoard>());
            yield return new WaitForSeconds(0.15f);
            Main.keys = Enumerable.ToList<Key>(GameObject.FindObjectsOfType<Key>());
            yield return new WaitForSeconds(0.15f);
            Main.ghosts = Enumerable.ToList<GhostAI>(GameObject.FindObjectsOfType<GhostAI>());
            yield return new WaitForSeconds(0.15f);
            Main.serverManager = GameObject.FindObjectOfType<ServerManager>();
            yield return new WaitForSeconds(0.15f);
            Main.torches = Enumerable.ToList<Torch>(GameObject.FindObjectsOfType<Torch>());
            yield return new WaitForSeconds(0.15f);
            Main.ghostAudio = GameObject.FindObjectOfType<GhostAudio>();
            yield return new WaitForSeconds(0.15f);
            Main.fuseBox = GameObject.FindObjectOfType<FuseBox>();
            yield return new WaitForSeconds(0.15f);
            Main.doors = Enumerable.ToList<Door>(GameObject.FindObjectsOfType<Door>());
            yield return new WaitForSeconds(0.15f);
            Main.lightSwitches = Enumerable.ToList<LightSwitch>(GameObject.FindObjectsOfType<LightSwitch>());

            if (UnityEngine.Object.FindObjectOfType<Player>() != null)
            {
                Main.player = (UnityEngine.Object.FindObjectOfType<Player>() ?? null);
                yield return new WaitForSeconds(0.15f);
                Main.playerAnim = (Main.player.field_Public_Animator_0 ?? null);
                yield return new WaitForSeconds(0.15f);
                if (Main.playerAnim != null)
                {
                    Main.boneTransform = (Main.playerAnim.GetBoneTransform((HumanBodyBones)10) ?? null);
                    yield return new WaitForSeconds(0.15f);
                    if (Main.boneTransform != null)
                    {
                        Main.light = (Main.boneTransform.GetComponent<Light>() ?? null);
                        yield return new WaitForSeconds(0.15f);
                    }
                }
            }
            yield return null;
            yield break;
        }

        public override void OnApplicationStart()
        {
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
                    GameObject.FindObjectOfType<Player>();
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = false;
                    if (myPlayer != null)
                    {
                        myPlayer.field_Public_FirstPersonController_0.enabled = true;
                        myPlayer.field_Public_Animator_0.SetFloat("speed", 0f);
                    }
                }
                if (menuEnabled)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (myPlayer != null)
                    {
                        myPlayer.field_Public_FirstPersonController_0.enabled = false;
                        myPlayer.field_Public_Animator_0.SetFloat("speed", 0f);
                    }
                }
            }
            bool delete = kb.deleteKey.wasPressedThisFrame;
            if (delete)
            {
                guiEnabled = !guiEnabled;
            }

            bool print = kb.numpad0Key.wasPressedThisFrame;
            if (print)
            {
                //insert print all assets here
            }
        }

        // ghosttraits.type

        public static string GetGhostType(ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique type)
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

        //private string roomName;
        //private string steamID;
        private bool isPrivateServer;

        private string serverName;
        private float serverSlots;
        //private bool antikick;

        // Token: 0x04000033 RID: 51

        // Token: 0x04000034 RID: 52
        public Rect dropDownRect = new Rect(1420f, 0f, 200f, 300f);

        // Token: 0x04000035 RID: 53

        // Token: 0x04000036 RID: 54
        //private GUIStyle guiStyle = new GUIStyle();

        private int selecteditem;
        private bool showItemList;

        public static Player myPlayer;

        private void RoomGUI()
        {
            if (!PhotonNetwork.InRoom)
            {
                GUI.Label(new Rect(720f, 0f, 200f, 20f), "Custom Room Creator:");
                serverName = GUI.TextArea(new Rect(720f, 25f, 200f, 20f), serverName);
                serverSlots = GUI.HorizontalSlider(new Rect(720f, 50f, 200f, 20f), (float)((int)serverSlots), 4f, 90f);
                GUI.Label(new Rect(720f, 65f, 200f, 20f), "Slots: " + ((int)serverSlots).ToString());
                if (GUI.Toggle(new Rect(720f, 80f, 200f, 20f), isPrivateServer, "Private Room") != isPrivateServer)
                {
                    isPrivateServer = !isPrivateServer;
                }
                if (GUI.Button(new Rect(720f, 105f, 200f, 20f), "Create Custom Room"))
                {
                    if (isPrivateServer)
                    {
                        PlayerPrefs.SetInt("isPublicServer", 0);
                        RoomOptions roomOptions = new RoomOptions
                        {
                            IsOpen = true,
                            IsVisible = false,
                            MaxPlayers = Convert.ToByte((int)serverSlots),
                            PlayerTtl = 2000
                        };
                        PhotonNetwork.CreateRoom(UnityEngine.Random.Range(0, 999999).ToString("000000"), roomOptions, TypedLobby.Default);
                    }
                    if (!isPrivateServer)
                    {
                        PlayerPrefs.SetInt("isPublicServer", 1);
                        RoomOptions roomOptions2 = new RoomOptions
                        {
                            IsOpen = true,
                            IsVisible = true,
                            MaxPlayers = Convert.ToByte((int)serverSlots),
                            PlayerTtl = 2000
                        };
                        PhotonNetwork.CreateRoom(serverName + "#" + UnityEngine.Random.Range(0, 999999).ToString("000000"), roomOptions2, TypedLobby.Default);
                    }
                }
            }
        }

        private Vector2 scrollViewVector;

        public override void OnGUI()
        {
            if (guiEnabled)
            {
                HUD.DrawHUD();
                if (Main.levelController != null)
                {
                    SpeedHack.playerSpeed();
                    ESPMethods.drawESP();
                    FullBright.changeBright();
                }
                if (menuEnabled && Main.levelController != null)
                {
                    Menu.hudToggles();
                }
                //PlayerESPFunc();
                if (menuEnabled)
                {
                    if (!PhotonNetwork.InRoom)
                    {
                        RoomGUI();
                    }
                    Menu.drawMenu();
                }
            }
        }
    }
}
