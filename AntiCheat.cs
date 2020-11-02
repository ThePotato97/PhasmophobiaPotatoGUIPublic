//#define BETA_VERSION

using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhasmophobiaPotatoGUI
{
    internal class AntiCheat
    {
#if BETA_VERSION

        // bypass stupid MelonLoader checks
        [HarmonyPatch(typeof(MLDetection.MLDetectionCheck), "Check")]
        private class MLCheckPatch
        {
            private static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(MLDetection.MLDetectionCheck), "Start")]
        private class MLStartPatch
        {
            private static bool Prefix()
            {
                return false;
            }
        }

#endif
    }
}