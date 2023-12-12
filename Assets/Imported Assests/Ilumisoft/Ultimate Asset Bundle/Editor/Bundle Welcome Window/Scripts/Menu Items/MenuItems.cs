using UnityEditor;
using UnityEngine;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public static class MenuItems
    {
        [MenuItem(Main.ParentMenuItem+"/Download Assets")]
        static void OpenDownloadUtility()
        {
            WelcomeWindow.Create();
        }

        [MenuItem(Main.ParentMenuItem + "/Rate")]
        static void Rate()
        {
            var bundleInfo = ScriptableObjectUtility.Find<BundleInfo>();

            if(bundleInfo != null)
            {
                Application.OpenURL(bundleInfo.BundleURL);
            }
        }
    }
}