using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour {
    public AudioClip sound;

    private Button button { get { return GetComponent<Button>(); }}
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    void Start () {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        button.onClick.AddListener(() => playsound());
	}
	
	// Update is called once per frame
	void playsound () {

        source.PlayOneShot(sound);
	}
}
