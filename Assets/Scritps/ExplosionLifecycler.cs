using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLifecycler : MonoBehaviour {
    private List<ParticleSystem> _particles;
	// Use this for initialization
	void Start () {
        _particles = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
    }
	
	// Update is called once per frame
	void Update () {
        var any = true;
        for (int i = _particles.Count - 1; i >= 0 && any; i--)
        {
            any &= !_particles[i].IsAlive();
        }

        if (any)
        {
            Destroy(gameObject);
        }
    }
}
