using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private Rigidbody2D rb2d;

	public Transform posPe;
	[HideInInspector] public bool tocaChao = false;
	[HideInInspector] public bool jump;
	[HideInInspector] public bool atirar_parado;
	[HideInInspector] public bool correr_com_arma;


	private float shootingRate = 0.1f;
	public float shootCooldown = 0f;
	public Transform spawBullet;
	public GameObject bullet;
	public GameObject bullet2;

	public float Velocidade;
	public float ForcaPulo = 1000f;
	[HideInInspector] public bool viradoDireita = true;

	public Image vida;
	private MensagemControle MC;
	public GUIText textoMensagem;

	//Imagens para tiro 1 e 2.
	public Image imagemTiro1;
	public Image imagemTiro2;



	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

		GameObject mensagemControleObject = GameObject.FindWithTag ("MensagemControle");
		if (mensagemControleObject != null) {
			MC = mensagemControleObject.GetComponent<MensagemControle> ();
		}

		bullet.gameObject.SetActive(true);
		bullet2.gameObject.SetActive(false);

		//imagemTiro1.enabled = true;
		//imagemTiro2.enabled = false;


	}

	// Update is called once per frame
	void Update () {		
		tocaChao = Physics2D.Linecast (transform.position, posPe.position, 1 << LayerMask.NameToLayer ("chao"));
		if((Input.GetKeyDown (KeyCode.Space)&& (tocaChao)))
		{
			jump = true;
		}
		if (Input.GetKeyDown("1")){
			bullet.gameObject.SetActive(true);
			bullet2.gameObject.SetActive(false);
					
		}
		if (Input.GetKeyDown("2")){
			bullet.gameObject.SetActive(false);
			bullet2.gameObject.SetActive(true);

		}
		if((Input.GetKeyDown (KeyCode.F) && (tocaChao)))
		{
			atirar_parado = true;
		}

		//se eu já atirei alguma vez, esta variavel esta maior que zero
		if(shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
		//aperte a letra F para dar o tiro
		if((Input.GetKeyDown (KeyCode.F)))
		{
			Fire ();
			shootCooldown = shootingRate;
		}
	}

	void FixedUpdate()
	{
		float translationY = 0;
		float translationX = Input.GetAxis ("Horizontal") * Velocidade;
		transform.Translate (translationX, translationY, 0);
		transform.Rotate (0, 0, 0);
		if (translationX != 0 && tocaChao) {
			anim.SetTrigger ("corre");
		} else {
			anim.SetTrigger("parado");
		}

		//Programar o pulo Aqui! 
		if (jump) {
			anim.SetTrigger ("pula");
			rb2d.AddForce (new Vector2 (0f, ForcaPulo));
			jump = false;
		}

		//Programar tiro parado e correr com arma! 
		if (atirar_parado) {
			if (translationX != 0 && tocaChao) {
				anim.SetTrigger ("correr_com_arma");
			} else {
				anim.SetTrigger ("atirar_parado");
				atirar_parado = false;
			}
		}


		if (translationX > 0 && !viradoDireita) {
			Flip ();
		} else if (translationX < 0 && viradoDireita) {
			Flip();
		}

	}

	//função do tiro
	void Fire(){
		//atirar somente se o shootCooldown <= 0f
		if(shootCooldown <= 0f)
		{
			if(bullet != null)
			{
				var cloneBullet = Instantiate (bullet, spawBullet.position, Quaternion.identity) as GameObject;
				cloneBullet.transform.localScale = this.transform.localScale;
				//som de tiro do player
				SoundEffectScript.Instance.MakeEnemyShotSound ();
			}
			if(bullet2 != null)
			{
				var cloneBullet = Instantiate (bullet2, spawBullet.position, Quaternion.identity) as GameObject;
				cloneBullet.transform.localScale = this.transform.localScale;
				//som de tiro do player
				SoundEffectScript.Instance.MakeEnemyShotSound ();
			}

		}

	}

	//colisao com agua
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "agua")
		{
			Destroy (gameObject);
			Application.LoadLevel ("Cena1");
		}
	}

	void Flip()
	{
		viradoDireita = !viradoDireita;
		Vector3 escala = transform.localScale;
		escala.x *= -1;
		transform.localScale = escala;
	}

	public void SubtraiVida()
	{
		vida.fillAmount-=0.1f;
		if (vida.fillAmount <= 0) {
			MC.GameOver();
			Destroy(gameObject);
		}
	}

}