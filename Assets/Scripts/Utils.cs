using UnityEngine;
using Valve.VR;

namespace SocialVR {
    public class Utils : MonoBehaviour {
        public static float fadeTime = 0.1f;

        public static void InitiateTeleportFade () {
            SteamVR_Fade.Start (Color.clear, 0);
            SteamVR_Fade.Start (Color.black, fadeTime);
        }
    }
}