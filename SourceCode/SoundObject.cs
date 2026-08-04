using UnityEngine;
using System;
using UnityEngine.Audio;


[CreateAssetMenu(fileName = "SoundObject", menuName = "Gingerbad/SoundObject", order = 2)]
public class SoundObject : ScriptableObject
{

    public AudioClip audioClip;

    public AudioMixerGroup outputMixer;

    [Tooltip("This allows to instantiate the audio source as a copy of a pre-existing object instead of as an new object.")]
    public GameObject settings3D;

    [Tooltip("This allows dynamic updates for the volume of the audio source.")]
    public FloatVariable dynamicVolume;
        
    [Tooltip("Integer between 0 and 255 where 0 = high priority and 255 = low priority.")]
    [Range(0, 255)] public int audioPriority;
    
    [Range(0f, 1f)] public float baseVolume;
    [Range(0f, 1f)] public float randomVolume;
    [Range(0f, 2f)] public float basePitch; 
    [Range(0f, 1f)] public float randomPitch;

    [Tooltip("True to continue playing until stopped by external source, False to destroy the audio source after it finishes playing once.")]
    public bool loopAudio;

    [Tooltip("True to have the audio source scale its pitch with the in-game timescale, False to have it keep its pitch after instatiated.")]
    public bool ignoreTimescale;

    public GameObject Play(Transform parent = null, Vector3? spawnPosition = null){

        if(!audioClip){

            Debug.Log("Sound " + this.name + " has no audio clip attached.");
            return null;
        }

        GameObject go;
        
        if (settings3D){

            go = Instantiate(settings3D);
            go.AddComponent<SoundScript>();
            go.name = this.name;
        }

        else{

            go = new GameObject(name, typeof(AudioSource), typeof(SoundScript));
        }

        if (parent){
            
            go.transform.position = spawnPosition ?? parent.position;
            go.transform.SetParent(parent);
        }

        else{

            go.transform.position = spawnPosition ?? Vector3.zero;
        }
          
        SoundScript script = go.GetComponent<SoundScript>();
        AudioSource audio = go.GetComponent<AudioSource>();

        //volume is clipped in between 0 and 1
        script.dynamicVolume = dynamicVolume;
        script.sourceVolume = baseVolume;

        if(randomVolume != 0){

            script.sourceVolume *= 1 + UnityEngine.Random.Range(-randomVolume * 0.5f, randomVolume * 0.5f);  
        }

        script.ignoreTimescale = ignoreTimescale;
        script.sourcePitch = basePitch;

        if(randomPitch != 0){

            script.sourcePitch *= 1 + UnityEngine.Random.Range(-randomPitch * 0.5f, randomPitch * 0.5f);
        }
        
        
        audio.clip = audioClip;
        audio.loop = loopAudio;
        audio.priority = audioPriority;
        audio.outputAudioMixerGroup = outputMixer;
        
        audio.Play(); 

        return go;
    }

    public void PlayFromUI(){

        Play();
    }
}
