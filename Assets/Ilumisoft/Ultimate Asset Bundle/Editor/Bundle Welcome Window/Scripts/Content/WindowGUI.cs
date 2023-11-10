using UnityEngine;
using UnityEditor;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public abstract class WindowGUI
    {
        public WelcomeWindow Window { get; set; }

        public BundleInfo BundleInfo => Window.BundleInfo;

        public virtual void Initialize(WelcomeWindow window)
        {
            this.Window = window;
        }

        public abstract void OnGUI();

        protected void DrawHeader(string text)
        {
            Rect line = GUILayoutUtility.GetRect(Window.position.width, 0);

            line.height = 22;

            EditorGUI.DrawRect(line, new Color(0.2f, 0.2f, 0.2f));

            GUILayout.Space(1);
            GUILayout.Label(text, EditorStyles.boldLabel);

            GUILayout.Space(8);
        }

        protected void OpenAssetStorePage(int assetID)
        {
            Application.OpenURL($"https://assetstore.unity.com/packages/slug/{assetID}?aid=1100l9P3D");
        }
    }
}