using UnityEngine;
using System.Collections;

public class EngineSound : MonoBehaviour
{
    //ficheiro de audio
    private AudioSource carSound;
    private const float lowPtich = 0.5f;
    private const float highPitch = 5f;
    private const float reductionFactor = .001f;
    private float userInput;
    WheelJoint2D wj;

    void Awake()
    {
        carSound = GetComponent<AudioSource>();
        wj = GetComponent<WheelJoint2D>();
    }

    void FixedUpdate()
    {
        userInput = Input.GetAxisRaw("Horizontal");
        float forwardSpeed = Mathf.Abs(wj.jointSpeed);
        float pitchFactor = Mathf.Abs(forwardSpeed * reductionFactor * userInput);
        carSound.pitch = Mathf.Clamp(pitchFactor, lowPtich, highPitch);
    }

}