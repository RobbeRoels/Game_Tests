using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

    
    public Light componentLight;

    private float _LightTime = 0.0f;
    private float _LightTimeCounter = 0.0f;

    public float minLightTime = 0.5f;
    public float maxLightTime = 0.7f;
    public float minIntensity = 1.5f;
    public float maxIntensity = 2.5f;
    bool dimming = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_LightTimeCounter < _LightTime)
        {
            _LightTimeCounter += Time.deltaTime;
            if (componentLight.intensity < minIntensity && dimming)
            {
                _LightTimeCounter = 0;
                _LightTime = Random.Range(minLightTime, maxLightTime);
                dimming = false;
            }
            if (componentLight.intensity > maxIntensity && !dimming) {
                _LightTimeCounter = 0;
                _LightTime = Random.Range(minLightTime, maxLightTime);
                dimming = true;
            }
            if (dimming)
            {
                componentLight.intensity -= Time.deltaTime;
            }
            else
            {
                componentLight.intensity += Time.deltaTime;
            }
        }
        else {
            _LightTimeCounter = 0;
            _LightTime = Random.Range(minLightTime, maxLightTime);
            dimming = !dimming;
        }

    }
}
