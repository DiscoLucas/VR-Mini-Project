using UnityEngine;
using UnityEditor;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public class WindowFooterGUI : WindowGUI
    {
        public override void OnGUI()
        {
            GUILayout.FlexibleSpace();

            Rect line = GUILayoutUtility.GetRect(Window.position.width, 1);

            EditorGUI.DrawRect(line, Color.black);

            using (new GUILayout.HorizontalScope())
            {
                EditorGUI.BeginChangeCheck();
                bool show = GUILayout.Toggle(BundleInfo.ShowAtStartup, " Show on startup");

                if (EditorGUI.EndChangeCheck())
                {
                    BundleInfo.ShowAtStartup = show;
                }

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Close"))
                {
                    Window.Close();
                }
            }
        }
    }
}