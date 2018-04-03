using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSenha : MonoBehaviour {

	// Use this for initialization
	public GameObject porta;
	public GameObject sinal;
	public GameObject portafechada;
	private bool Aberto = false;
	private MudaCor pc1;
	private MudaCor pc2;
	private MudaCor pc3;

	private AudioSource aSource;

	void Start () {
		GameObject pedraCorObject = GameObject.FindWithTag ("Pedra1");
		if (pedraCorObject != null) {
			pc1 = pedraCorObject.GetComponent<MudaCor> ();
		}
		pedraCorObject = GameObject.FindWithTag ("Pedra2");
		if (pedraCorObject != null) {
			pc2 = pedraCorObject.GetComponent<MudaCor> ();
		}
		pedraCorObject = GameObject.FindWithTag ("Pedra3");
		if (pedraCorObject != null) {
			pc3 = pedraCorObject.GetComponent<MudaCor> ();
		}

		aSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {		
		if(pc1.valPedra ==1 && pc2.valPedra ==2 && pc3.valPedra ==0 && !Aberto)
		{
			
			Vector3 spawnPos = new Vector3 (7.32f,-3.41f,-0.5f);
			Quaternion spawnRot = Quaternion.identity;
			Instantiate (porta, spawnPos, spawnRot);

			if(aSource.isPlaying){
				aSource.Stop ();
			}

			spawnPos = new Vector3 (6f,-4f,-0.5f);
			spawnRot = Quaternion.identity;
			Instantiate (sinal, spawnPos, spawnRot);

			Aberto = true;

			if(portafechada != null){
				portafechada.SetActive (false);
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pc1.valPedra == 1 && pc2.valPedra == 2 && pc3.valPedra == 0 && !Aberto) {
			aSource.Play ();
		}
	}
}
