using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FXManager : MonoBehaviour {

	public delegate void FxEvent(Vector3 position);

    	
	int length = 10;
	Dictionary<GameObject, ParticleSystem[]> pool;
	Dictionary<GameObject, int> poolIndex;

	public GameObject WhiteChickenExplosion;
	public GameObject BrownChickenExplosion;
	public GameObject ChickExplosion;
	
	public AudioClip chickenpop;
	public AudioClip chickpop;
	public AudioClip peck;



	[SerializeField]
	AudioSource aSource;
	
	void OnValidate () {
		aSource = GetComponent<AudioSource>();
	}
	
	void Start () {
		pool = new Dictionary<GameObject, ParticleSystem[]>();
		poolIndex = new Dictionary<GameObject, int>();
		
		AddPool(WhiteChickenExplosion);
		AddPool(BrownChickenExplosion);
		AddPool(ChickExplosion);
	}
	
	void AddPool (GameObject prefab) {
		pool.Add(prefab, new ParticleSystem[length]);
		poolIndex.Add(prefab, 0);
		for (int i = 0; i < length; i++) {
			pool[prefab][i] = GameObject.Instantiate(prefab).GetComponent<ParticleSystem>();
		}
	}
	
	void PlayParticle (GameObject prefab, Vector3 position) {
		int i = poolIndex[prefab];
		poolIndex[prefab] = (poolIndex[prefab] + 1) % length;
		pool[prefab][i].transform.position = position;
		pool[prefab][i].Play();
	}
	
	void PlayAudio (AudioClip a) {
		if (a != null)
			aSource.PlayOneShot(a);
	}

    void OnEnable() {
        Chick.OnDeath += Chick_OnDeath;
		Chicken.OnDeath += Chicken_OnDeath;
		Player.OnDeath += Player_OnDeath;
    }

    void OnDisable() {
        Chick.OnDeath -= Chick_OnDeath;
		Chicken.OnDeath -= Chicken_OnDeath;
		Player.OnDeath -= Player_OnDeath;
    }

    void Chick_OnDeath(Vector3 position) {
		PlayParticle(ChickExplosion, position);
		PlayAudio(chickpop);
    }
	
	void Chicken_OnDeath (Vector3 position) {
		PlayParticle(BrownChickenExplosion, position);
		PlayAudio(chickenpop);
	}
	
	void Player_OnDeath (Vector3 position) {
		PlayParticle(WhiteChickenExplosion, position);
		PlayAudio(chickenpop);
	}

}
