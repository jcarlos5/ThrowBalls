using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Text txtScore;
	public Text txtBalls;
	public AudioSource impact_sound;
	public AudioSource break_sound;

	public Rigidbody pelota;
	public float fuerza = 1500f;
	public bool touchScreen = false;

	private float velMov = 2f;
	private float velRot = 20f;

	private bool dispara = false;
	private bool inidispara = false;

	private int score = 0;
	private int pelotasLanzadas = 0;

	void Awake()
    {
        LoadData();
    }

	void Start() {

		if (touchScreen) {
			velMov = 4f;
			velRot = 5f;
		}
		else {
			velMov = 2;
			velRot = 20f;
		}

		UpdateTextScore();
		UpdateTextBalls();
	}
	
	// Update is called once per frame
	void Update () {
		float h, v;
		if ( touchScreen ) {
			if (Input.touchCount > 0 && 
		    	Input.GetTouch (0).phase == TouchPhase.Moved) {
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				h = touchDeltaPosition.x * Time.deltaTime * velRot;
				v = touchDeltaPosition.y * Time.deltaTime * velMov;
			} 
			else
				h = v = 0;
		}
		else {
		   h = -Input.GetAxis("Horizontal") * Time.deltaTime* velRot;
		   v = -Input.GetAxis ("Vertical") * Time.deltaTime * velMov;
		}

		if (h != 0 || v != 0) {
			inidispara = dispara = false;
			if ( v != 0 )
				transform.Translate(new Vector3(0,-v,0) );
			if ( h != 0 )
				transform.Rotate(new Vector3(0,-h,0) );
		}
		else if ( touchScreen ) {
				for (int  i = 0; i < Input.touchCount; i++) {
					if (Input.GetTouch (i).phase == TouchPhase.Began) {
						inidispara = true;
						return;
					}
					if (Input.GetTouch (i).phase == TouchPhase.Ended) {
						if ( inidispara ) 
							dispara = true;
					}
				}

	    }
		 
		if(Input.GetButtonUp("Fire1")) {
			dispara = true;
		}
		
		if ( dispara ) {
			pelotasLanzadas++;
			UpdateTextBalls();
			Rigidbody lapelota = Instantiate (pelota, transform.position,
			                                new Quaternion (0, 0, 0, 1)) as Rigidbody;
			
			Vector3 fwd = transform.TransformDirection (Vector3.forward);

			//lapelota.GetComponent<Rigidbody>().AddForce(fwd * fuerza);
			lapelota.AddForce (fwd * fuerza);
			inidispara = dispara = false;
		}
		
		// if (Input.GetKey (KeyCode.Escape))
		// 	//Application.LoadLevel("e0");
		// 	SceneManager.LoadScene ("e0");
	}

	private void UpdateTextScore()
	{
		txtScore.text = "Puntos: "+score;
	}

	private void UpdateTextBalls()
	{
		txtBalls.text = "Pelotas lanzadas: "+pelotasLanzadas;
	}

	public void addPoint(int points)
	{
		score+=points;
		UpdateTextScore();
	}

	public void PlaySound(bool isBreak)
	{
		if(isBreak)
		{
			break_sound.Play();
		}
		else
		{
			impact_sound.Play();
		}
	}

	void OnDestroy()
    {
        SaveData();
    }

	private void SaveData()
    {
        PlayerPrefs.SetInt("Balls", pelotasLanzadas);
        PlayerPrefs.SetInt("Score", score);
    }

    private void LoadData()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        pelotasLanzadas = PlayerPrefs.GetInt("Balls", 0);
    }

	public void GoScene1()
	{
		pelotasLanzadas--;
		SceneManager.LoadScene("Scene1");
	}

	public void GoScene3()
	{
		pelotasLanzadas--;
		SceneManager.LoadScene("Scene3");
	}
}

