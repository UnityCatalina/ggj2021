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
        float skinRandom = Random.Range(-0.7f, 1f);
        Color hairRandom = new Color(skinRandom, skinRandom, 0f);
        Color SuitRandom = new Color(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Color bagRandom = new Color(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        //color the hair and suit
        Material SkinTone = gameObject.GetComponent<MeshRenderer>().materials[0];
        Material SuitColor = gameObject.GetComponent<MeshRenderer>().materials[1];
        SkinTone.color += new Color(skinRandom, skinRandom, skinRandom);
        SuitColor.color += SuitRandom;

        //attach a hat, bag or backpack
        int hasHat = Random.Range(0, hats.Length+1);
        int hasBag = Random.Range(0, bags.Length+1);
        int hasBackpack = Random.Range(0, backpacks.Length+1);

        Transform[] items = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform item in items)
        {
            //check if i have a bag and spawn bag at position
            if (item.name == "Bag" && hasBag!= bags.Length)
            {
                Instantiate(bags[hasBag], item.transform, false);
                item.transform.GetComponentInChildren<MeshRenderer>().material.color += bagRandom;
            }
            //check if i have a hat and spawn bag at position
            if (item.name == "Hat" && hasHat!=hats.Length)
            {
                Debug.Log(hasHat);
                Instantiate(hats[hasHat], item.transform, false);
                item.transform.GetComponentInChildren<MeshRenderer>().material.color += hairRandom;
            }
            //check if i have a backpack and spawn bag at position
            if (item.name == "Backpack" && hasBackpack!=backpacks.Length)
            {
                Instantiate(backpacks[hasBackpack], item.transform, false);
                item.transform.GetComponentInChildren<MeshRenderer>().material.color +=  (SuitRandom - hairRandom);
            }
        }
    }
}
