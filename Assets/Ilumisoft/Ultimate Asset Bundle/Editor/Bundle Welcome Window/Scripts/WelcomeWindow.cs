using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public class WelcomeWindow : EditorWindow
    {
        List<WindowGUI> elements = new List<WindowGUI>();

        public BundleInfo BundleInfo { get; set; }

        public static void Create()
        {
            var config = ScriptableObjectUtility.Find<BundleInfo>();

            var window = GetWindow<WelcomeWindow>(utility:true);

            window.titleContent = new GUIContent("Welcome");
            window.maxSize = config.WindowDimensions;
            window.minSize = window.maxSize;
        }

        private void OnEnable()
        {
            BundleInfo = ScriptableObjectUtility.Find<BundleInfo>();

            elements.Clear();

            elements.AddRange(new List<WindowGUI>()
            {
                new WindowHeaderGUI(),
                new WindowPackageGUI(),
                new WindowUpgradeOptionsGUI(),
                new WindowSupportGUI(),
                new WindowFooterGUI()
            });

            elements.ForEach(element => element.Initialize(this));
        }

        void OnGUI()
        {
            foreach(var element in elements)
            {
                element.OnGUI();
            }
        }
    }
}