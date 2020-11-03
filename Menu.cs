using MelonLoader;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

namespace PhasmophobiaPotatoGUI
{
    public class Menu : MelonMod
    {
        public static void drawMenu()
        {
            if (Main.levelController == null)
            {
                lobbyActions();
            }
            else
            {
                ghostActions();
            }
            if (showItemList)
            {
                spawnItemMenu();
            }
        }

        private static void ghostActions()
        {
            GUI.Label(new Rect(520f, 95f, 200f, 20f), "Ghost:");
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

            //    Main.ghostInteraction.GetComponent<PhotonView>().RPC("SpawnFootstepNetworked", 0, new Il2CppSystem.Object[]
            //    {
            //        ghostAI.transform.position,
            //        ghostAI.transform.rotation,
            //        new Il2CppSystem.Int32(){ m_value = UnityEngine.Random.Range(0, 3) }.BoxIl2CppObject()
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
            Il2CppSystem.Int32 appearRand = new Il2CppSystem.Int32
            {
                m_value = UnityEngine.Random.Range(0, 3)
            };

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
        }

        private static string playerNickName;
        private static Player MyPlayer;
        private static float playerReach;

        private static void anywhereActions()
        {
            if (GUI.Toggle(new Rect(1120f, 325f, 200f, 20f), showItemList, "Show Item Spawner") != showItemList)
            {
                showItemList = !showItemList;
            }
        }

        private static void lobbyActions()
        {
            GUI.SetNextControlName("changename");
            playerNickName = GUI.TextArea(new Rect(320f, 25f, 200f, 20f), playerNickName);
            if (GUI.Button(new Rect(320f, 45f, 200f, 20f), "Change Name"))
            {
                GUI.FocusControl("changename");
                PhotonNetwork.NickName = playerNickName;
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
            playerReach = GUI.HorizontalSlider(new Rect(720f, 200f, 200f, 20f), (float)((int)playerReach), 1.6f, 16f);
            GUI.Label(new Rect(520f, 175f, 200, 20f), "Reach: " + (int)playerReach);
            if (GUI.Button(new Rect(720f, 225f, 200f, 20f), "Click to change reach"))
            {
                MyPlayer = Main.GetLocalPlayer();
                MyPlayer.field_Public_PCPropGrab_0.field_Private_Single_0 = playerReach;
            }
            //GUI.Label(new Rect(920f, 0f, 200f, 20f), "Join Room:");
            //roomName = GUI.TextArea(new Rect(920f, 25f, 200f, 20f), roomName);
            //steamID = GUI.TextArea(new Rect(920f, 50f, 200f, 20f), steamID);
            //if (GUI.Button(new Rect(920f, 75f, 200f, 20f), "Join Room by Name"))
            //{
            //    PhotonNetwork.JoinRoom(roomName);
            //}
            //if (GUI.Button(new Rect(920f, 100f, 200f, 20f), "Join Room by ID"))
            //{
            //    object[] FriendIDList;

            //    bool test = PhotonNetwork.FindFriends(new string[]
            //    {
            //            steamID
            //    });
            //    MelonLogger.Log("steamID = " + steamID);
            //    //foreach (FriendInfo friendInfo in PhotonNetwork.Friends)
            //    //{
            //    //PhotonNetwork.JoinRoom(friendInfo.Room);
            //    //}
            //}
        }

        public static bool GhostESP = true;

        public static bool PlayerESP = true;

        public static bool KeyESP = true;

        public static bool OuijaESP = true;

        public static bool EvidenceESP = true;

        public static bool FuseboxESP = true;

        public static bool ShowInfoGhost = true;

        public static bool ShowInfoPlayer = true;

        public static bool showMissionInfo = true;

        public static bool SpeedHack = true;

        public static bool fullbrighttoggle = true;

        private static void hudToggles()
        {
            GUI.Label(new Rect(920f, 295f, 200f, 20f), "ESP:");
            if (GUI.Toggle(new Rect(920f, 320f, 200f, 20f), GhostESP, "Ghost") != GhostESP)
            {
                GhostESP = !GhostESP;
            }
            if (GUI.Toggle(new Rect(920f, 370f, 200f, 20f), PlayerESP, "Player") != PlayerESP)
            {
                PlayerESP = !PlayerESP;
            }
            if (GUI.Toggle(new Rect(920f, 395f, 200f, 20f), OuijaESP, "Ouija Board") != OuijaESP)
            {
                OuijaESP = !OuijaESP;
            }
            if (GUI.Toggle(new Rect(920f, 420f, 200f, 20f), KeyESP, "Key") != KeyESP)
            {
                KeyESP = !KeyESP;
            }
            if (GUI.Toggle(new Rect(920f, 445f, 200f, 20f), EvidenceESP, "Evidence") != EvidenceESP)
            {
                EvidenceESP = !EvidenceESP;
            }

            if (GUI.Toggle(new Rect(1120f, 400f, 200f, 20f), SpeedHack, "Speedhack") != SpeedHack)
            {
                SpeedHack = !SpeedHack;
            }

            if (GUI.Toggle(new Rect(1120, 415f, 200f, 20f), fullbrighttoggle, "FullBright") != fullbrighttoggle)
            {
                fullbrighttoggle = !fullbrighttoggle;
                FullBright.changeBright();
            }
            
            GUI.Label(new Rect(740f, 145f, 200f, 20f), "Lights:");
            if (GUI.Button(new Rect(740f, 175f, 200f, 20f), "All Lights On"))
            {
                foreach (LightSwitch lightSwitch1 in Main.lightSwitches)
                {
                    lightSwitch1.TurnOn(true);
                    lightSwitch1.TurnOnNetworked(true);
                }
            }
            if (GUI.Button(new Rect(740f, 195f, 200f, 20f), "All Lights Off"))
            {
                foreach (LightSwitch lightSwitch2 in Main.lightSwitches)
                {
                    lightSwitch2.TurnOff();
                    lightSwitch2.TurnOffNetworked(true);
                }
            }

            if (GUI.Toggle(new Rect(1120f, 300f, 200f, 20f), ShowInfoGhost, "Show Ghost Info") != ShowInfoGhost)
            {
                ShowInfoGhost = !ShowInfoGhost;
            }
            if (GUI.Toggle(new Rect(1120f, 250f, 200f, 20f), ShowInfoPlayer, "Show Player Info") != ShowInfoPlayer)
            {
                ShowInfoPlayer = !ShowInfoPlayer;
            }
            if (GUI.Toggle(new Rect(1120f, 200f, 200f, 20f), showMissionInfo, "Show Player Info") != showMissionInfo)
            {
                showMissionInfo = !showMissionInfo;
            }
        }

        public static Rect dropDownRect2 = new Rect(820f, 0f, 200f, 300f);
        private static bool showItemList;
        private static int selectedItem;
        private static Vector2 scrollViewVector;

        private static void spawnItemMenu()
        {
            if (showItemList)
            {
                string[] allitems = Constants.allitems;
                GUI.Label(new Rect(520f, 225f, 200f, 20f), "Item Spawner:");
                scrollViewVector = GUI.BeginScrollView(new Rect(dropDownRect2.x - 100f, dropDownRect2.y + 25f, dropDownRect2.width, dropDownRect2.height), scrollViewVector, new Rect(0f, 0f, dropDownRect2.width, Mathf.Max(dropDownRect2.height, (float)(allitems.Length * 25))));
                GUI.Box(new Rect(0f, 0f, dropDownRect2.width, Mathf.Max(dropDownRect2.height, (float)(allitems.Length * 25))), "");
                for (int l = 0; l < allitems.Length; l++)
                {
                    if (GUI.Button(new Rect(0f, (float)(l * 25), dropDownRect2.height, 25f), ""))
                    {
                        selectedItem = l;
                        if (PhotonNetwork.InRoom)
                        {
                            MyPlayer = Main.GetLocalPlayer();
                            MelonLogger.Log(allitems[selectedItem].ToString());
                            PhotonNetwork.Instantiate(allitems[selectedItem], MyPlayer.transform.position, Quaternion.identity, 0, null);
                        }
                    }
                    GUI.Label(new Rect(5f, (float)(l * 25), dropDownRect2.height, 25f), allitems[l]);
                }
                GUI.EndScrollView();
            }
        }
    }
}
