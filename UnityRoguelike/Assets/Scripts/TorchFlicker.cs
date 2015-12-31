using UnityEngine;
using System.Collections;

public class TorchFlicker : MonoBehaviour {
    // Properties
    public string waveFunction = "sin"; // possible values: sin, tri(angle), sqr(square), saw(tooth), inv(verted sawtooth), noise (random)
    public float _base  = 0.85f; // start
    public float amplitude  = 0.15f; // amplitude of the wave
    public float phase  = 0.0f; // start point inside on wave cycle
    public float frequency  = 3f; // cycle frequency per second
 
    // Keep a copy of the original color
    private Color originalColor;
    private Light lightComp;
    // Store the original color
    void Start () {
        lightComp = GetComponent<Light>();
        originalColor = lightComp.color;
    }
 
    void Update () {
        lightComp.color = originalColor * (EvalWave());
    }

    private float EvalWave()
    {
        float x = (Time.time + phase)*frequency;
        float y;

        x = x - Mathf.Floor(x); // normalized value (0..1)

        switch (waveFunction)
        {
            case "sin":
                y = Mathf.Sin(x*2*Mathf.PI);
                break;
            case "tri":
                if (x < 0.5f)
                    y = 4.0f*x - 1.0f;
                else
                    y = -4.0f*x + 3.0f;
                break;
            case "sqr":
                if (x < 0.5f)
                    y = 1.0f;
                else
                    y = -1.0f;
                break;
            case "saw":
                y = x;
                break;
            case "inv":
                y = 1.0f - x;
                break;
            case "noise":
                y = 1 - (Random.value*2);   
                break;
            default:
                y = 1.0f;
                break;
        }
        return (y*amplitude) + _base;
    }
}
