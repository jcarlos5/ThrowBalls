using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Color[] colors = new Color[] {  new Color (2262f/255f, 38f/255f, 89f/255f) , //fucsia
                                            new Color (0f/255f, 132f/255f, 255f/255f) , //celeste
                                            new Color (245f/255f, 193f/255f, 11f/255f), //amarillo
                                            new Color (178f/255f, 178f/255f, 6f/255f)}; //verde limón

    private int life = 3;
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerController>();
        GetComponent<Renderer>().material.color = colors[life-1];
    }

    void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            life--;
            player.addPoint(1);
            if(life <= 0)
            {
                player.PlaySound(true);
                Destroy(gameObject);
            }
            else
            {
                player.PlaySound(false);
                GetComponent<Renderer>().material.color = colors[life-1];
            }
        }
    }

    void Update()
    {
        if(gameObject.transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
}
