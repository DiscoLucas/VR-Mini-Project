using UnityEngine;
using UnityEditor;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public static class Main
    {
        public const string ParentMenuItem = "Bundle";

        [InitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            string sessionKey = typeof(WelcomeWindow).FullName + ".HasBeenShown";

            // Only show once per session
            if (SessionState.GetBool(sessionKey, false) == true)
            {
                return;
            }
            else
            {
                SessionState.SetBool(sessionKey, true);

                EditorApplication.update += InitializeWelcomeWindow;
            }
        }

        static void InitializeWelcomeWindow()
        {
            EditorApplication.update -= InitializeWelcomeWindow;

            var startupMessageAsset = ScriptableObjectUtility.Find<BundleInfo>();

            if (startupMessageAsset != null && startupMessageAsset.ShowAtStartup && !Application.isPlaying)
            {
                WelcomeWindow.Create();
            }
        }
    }
}