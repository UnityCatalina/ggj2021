using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobAccessoeries
{
    public Mesh[] body;
    public GameObject[] hats;
    public GameObject[] bags;
    public GameObject[] backpacks;
    public GameObject[] companions;
}


public class Mob_Randomizer : MonoBehaviour
{
    public MobAccessoeries[] parts;
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
        if (parts.Length > 0)
        {
            int partIndex = Random.Range(0, parts.Length);
            int bodyIndex = Random.Range(0, parts[partIndex].body.Length);
            int hatIndex = Random.Range(-1, parts[partIndex].hats.Length);
            int bagIndex = Random.Range(-1, parts[partIndex].bags.Length);
            int backpackIndex = Random.Range(-1, parts[partIndex].backpacks.Length);

            MeshFilter meshfilter = GetComponent<MeshFilter>();
            meshfilter.mesh = parts[partIndex].body[bodyIndex];

            Transform[] items = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform item in items)
            {
                //check if i have a bag and spawn bag at position
                if (item.name == "Bag" && bagIndex >= 0)
                {
                    Instantiate(parts[partIndex].bags[bagIndex], item.transform, false);
                    item.transform.GetComponentInChildren<MeshRenderer>().material.color += bagRandom;
                }
                //check if i have a hat and spawn bag at position
                if (item.name == "Hat" && hatIndex >= 0)
                {
                    Instantiate(parts[partIndex].hats[hatIndex], item.transform, false);
                    item.transform.GetComponentInChildren<MeshRenderer>().material.color += hairRandom;
                }
                //check if i have a backpack and spawn bag at position
                if (item.name == "Backpack" && backpackIndex >= 0)
                {
                    Instantiate(parts[partIndex].backpacks[backpackIndex], item.transform, false);
                    item.transform.GetComponentInChildren<MeshRenderer>().material.color += (SuitRandom - hairRandom);
                }
            }
        }
    }
}
