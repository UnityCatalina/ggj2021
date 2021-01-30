using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Randomizer : MonoBehaviour
{
    string type;
    public GameObject[] hats;
    public GameObject[] bags;
    public GameObject[] backpacks;
    public GameObject[] companions;
    
    // Start is called before the first frame update
    void Start()
    {
        float skinRandom = Random.Range(-0.7f,1f);
        Color SuitRandom = new Color(Random.Range(-2f, 7f), Random.Range(-2f,7f), Random.Range(-2f, 7f));


        Material SkinTone = gameObject.GetComponent<MeshRenderer>().materials[0];
        Material SuitColor = gameObject.GetComponent<MeshRenderer>().materials[1];
        SkinTone.color += new Color(skinRandom, skinRandom, skinRandom);
        SuitColor.color += SuitRandom;
        
    }

}
