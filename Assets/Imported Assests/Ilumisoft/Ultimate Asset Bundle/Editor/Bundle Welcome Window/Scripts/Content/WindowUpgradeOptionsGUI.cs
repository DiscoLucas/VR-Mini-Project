using UnityEngine;
using UnityEditor;

namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    public class WindowUpgradeOptionsGUI : WindowGUI
    {
        public override void OnGUI()
        {
            if (BundleInfo.UpgradeOptions == null || BundleInfo.UpgradeOptions.Count == 0)
            {
                return;
            }

            GUILayout.Space(14);

            DrawHeader("Upgrade Options");

            GUILayout.Label(BundleInfo.UpgradeOptionsDescription, DescriptionStyle);

            GUILayout.Space(8);

            foreach (var upgradeOption in BundleInfo.UpgradeOptions)
            {
                using (var horizontalScope = new GUILayout.HorizontalScope())
                {
                    GUILayout.Label($"{upgradeOption.Title} <color=#FF2C5A>({upgradeOption.Discount}% OFF)</color>", TitleStyle);

                    if (GUILayout.Button("Show", GUILayout.Width(100)))
                    {
                        OpenAssetStorePage((int)upgradeOption.ID);
                    }
                }

                GUILayout.Label(upgradeOption.Description, DescriptionStyle);

                GUILayout.Space(8);
            }
        }

        GUIStyle DescriptionStyle => new GUIStyle(EditorStyles.miniLabel)
        {
            wordWrap = true
        };

        GUIStyle TitleStyle => new GUIStyle(EditorStyles.boldLabel)
        {
            richText = true
        };
    }
}