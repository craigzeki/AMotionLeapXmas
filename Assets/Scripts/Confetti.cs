using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    private ParticleSystem _myPS;
    // Start is called before the first frame update
    void Start()
    {
        _myPS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_myPS.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
