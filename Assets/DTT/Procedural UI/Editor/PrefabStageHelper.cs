#if UNITY_EDITOR

using UnityEditor;

using UnityEngine;

namespace DTT.UI.ProceduralUI.Editor
{
    /// <summary>
    /// A helper class for updating the canvas settings when entering a prefab scene.
    /// This is the way for rounded image components to be drawn
    /// </summary>
    [InitializeOnLoad]
    public class PrefabStageHelper
    {
        /// <summary>
        /// Subscribes to the prefab stage opened event.
        /// </summary>
        static PrefabStageHelper()
        {
            UnityEditor.SceneManagement.PrefabStage.prefabStageOpened -= OnStageOpened;
            UnityEditor.SceneManagement.PrefabStage.prefabStageOpened += OnStageOpened;
        }

        /// <summary>
        /// Called when a prefab stage has been opened to find a rounded image
        /// in the scene that can fix the canvas and forcefully update it.
        /// </summary>
        /// <param name="stage">The entered prefab stage.</param>
        private static void OnStageOpened(UnityEditor.SceneManagement.PrefabStage stage)
        {
            RoundedImage image = stage.prefabContentsRoot.GetComponentInChildren<RoundedImage>();
            if (image != null)
            {
                image.ErrorHandler.FixFixableErrors();
                Canvas.ForceUpdateCanvases();
            }
        }
    }
}

#endif
