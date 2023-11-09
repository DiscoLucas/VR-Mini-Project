using UnityEditor;
using UnityEngine;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public class WindowPackageGUI : WindowGUI
    {
        Vector2 scrollPos = Vector2.zero;

        public override void OnGUI()
        {
            GUILayout.Space(14);

            DrawHeader("Packages");

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            foreach (var package in BundleInfo.Packages)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label(new GUIContent(package.Title));

                if (GUILayout.Button("Download", GUILayout.Width(100)))
                {
                    OpenAssetStorePage((int)package.ID);
                }

                GUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }
    }
}