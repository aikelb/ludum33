﻿using UnityEngine;
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
	public GameObject bloodParticles;
	
	public AudioClip chickenAudio;
	public AudioClip chickAudio;
	public AudioClip peck;
	public AudioClip coc;
	public AudioClip attack;
	
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
		AddPool(bloodParticles);
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
		Life.OnDamage += Life_OnDamage;
		Chicken.OnAttack += OnAttack;
		Player.OnAttack += OnAttack;
    }

    void OnDisable() {
        Chick.OnDeath -= Chick_OnDeath;
		Chicken.OnDeath -= Chicken_OnDeath;
		Player.OnDeath -= Player_OnDeath;
		Life.OnDamage -= Life_OnDamage;
		Chicken.OnAttack -= OnAttack;
		Player.OnAttack -= OnAttack;
    }

    void Chick_OnDeath(Vector3 position) {
		PlayParticle(ChickExplosion, position);
		PlayAudio(chickAudio);
    }
	
	void Chicken_OnDeath (Vector3 position) {
		PlayParticle(BrownChickenExplosion, position);
		PlayAudio(chickenAudio);
	}
	
	void Player_OnDeath (Vector3 position) {
		PlayParticle(WhiteChickenExplosion, position);
		PlayAudio(chickenAudio);
	}
	
	void Life_OnDamage (Vector3 position) {
		PlayParticle(bloodParticles, position);
	}
	
	void OnAttack (Vector3 position) {
		PlayAudio(attack);
	}

}
