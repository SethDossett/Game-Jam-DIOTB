using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    
    
    public void PlaySound(string clipName)
    {
        foreach (var clip in clips)
        {
            if (clip.name == clipName)
            {
                audio.PlayOneShot(clip);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
