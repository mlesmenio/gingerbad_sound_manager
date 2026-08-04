using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(SoundObject))]
[CanEditMultipleObjects]
public class SoundEditor : Editor
{
    SoundObject soundConfig;
    AudioSource previewSource;

    private void OnEnable()
    {
        soundConfig = (SoundObject) target;

        // Create a temporary AudioSource for previewing
        previewSource = EditorUtility.CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        // Destroy the temporary AudioSource
        DestroyImmediate(previewSource.gameObject);
    }

    public override void OnInspectorGUI()
    {
        //magic
        serializedObject.Update();

        DrawDefaultInspector();

        // Ensure the preview source has the right settings
        previewSource.clip = soundConfig.audioClip;
        previewSource.volume = soundConfig.baseVolume * (1 + Random.Range(-soundConfig.randomVolume * 0.5f, soundConfig.randomVolume * 0.5f));
        previewSource.pitch = soundConfig.basePitch * (1 + Random.Range(-soundConfig.randomPitch * 0.5f, soundConfig.randomPitch * 0.5f));

        // Play/Stop buttons
        if (GUILayout.Button("Play Preview"))
        {
            if (previewSource.isPlaying)
                previewSource.Stop();
            
            previewSource.Play();
        }

        if (GUILayout.Button("Stop Preview"))
        {
            previewSource.Stop();
        }

        //magic
        serializedObject.ApplyModifiedProperties();
    }
}

#endif