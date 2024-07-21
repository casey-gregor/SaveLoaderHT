using UnityEditor;
using UnityEngine;

namespace SaveLoaderProject
{

    [CustomEditor(typeof(ActionHelper))]
    public class CustomUIElements : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var actionHelper = (ActionHelper)target;

            if(GUILayout.Button("Save Game"))
            {
                actionHelper.SaveGame();
            }
            if (GUILayout.Button("Load Game"))
            {
                actionHelper.LoadGame();
            }
        }
    }
}

