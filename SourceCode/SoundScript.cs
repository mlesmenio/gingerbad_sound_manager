using System;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    
    internal FloatVariable dynamicVolume;
    internal float sourceVolume;
    Action updateVolume;
    
    internal bool ignoreTimescale;
    internal float sourcePitch;
    float lastTimescale;
    
    AudioSource selfAudioSource;
    
    void Start(){

        selfAudioSource = GetComponent<AudioSource>();

        if(dynamicVolume){

            updateVolume = () => selfAudioSource.volume = sourceVolume * dynamicVolume.currentValue;
            updateVolume();
            dynamicVolume.onValueChanged += updateVolume;
        }

        else{

            selfAudioSource.volume = sourceVolume;
        } 

        if(!ignoreTimescale){

            lastTimescale = Time.timeScale;
            selfAudioSource.pitch = sourcePitch * lastTimescale;
        } 
        
        else{

            selfAudioSource.pitch = sourcePitch;
        } 

        
    }

    void OnDisable(){

        if(dynamicVolume) dynamicVolume.onValueChanged -= updateVolume;
    }

    void Update(){

        if (!selfAudioSource.isPlaying && !AudioListener.pause) Destroy(gameObject);

        if(!ignoreTimescale && Time.timeScale != lastTimescale){

            lastTimescale = Time.timeScale;
            selfAudioSource.pitch = sourcePitch * lastTimescale;
        }
    }
}