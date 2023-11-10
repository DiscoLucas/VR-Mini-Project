namespace Ilumisoft.Editor.BundleWelcomeWindow
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class BundleInfo : ScriptableObject
    {
        [SerializeField]
        public Texture BundleImage = null;

        [SerializeField]
        public AssetID BundleID = 0;

        [SerializeField]
        public bool ShowAtStartup;

        public string BundleTitle => ObjectNames.NicifyVariableName(BundleID.ToString());

        public string BundleURL => $"https://assetstore.unity.com/packages/slug/{BundleID}?aid=1100l9P3D#reviews";

        public Vector2 WindowDimensions = new Vector2(400, 730);

        public List<Package> Packages;

        [TextArea]
        public string UpgradeOptionsDescription = string.Empty;

        public List<UpgradeOption> UpgradeOptions;
    }

    [System.Serializable]
    public struct Package
    {
        [SerializeField]
        public AssetID ID;

        public string Title => ObjectNames.NicifyVariableName(ID.ToString());
    }

    [System.Serializable]
    public struct UpgradeOption
    {
        [SerializeField]
        public AssetID ID;

        [SerializeField, Range(0, 100)]
        public int Discount;

        [SerializeField, TextArea]
        public string Description;

        public string Title => ObjectNames.NicifyVariableName(ID.ToString());
    }

    [CustomPropertyDrawer(typeof(Package))]
    public class PackageDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("ID"), GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}

