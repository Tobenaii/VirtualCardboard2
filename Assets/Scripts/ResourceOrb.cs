using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceOrb : MonoBehaviour
{
    public List<Material> materials = new List<Material>();

    public AnimationCurve curve = new AnimationCurve();

    public bool consumed = false;
    public float progression = 0;
    public float progTime = 2;
    public Vector3 basePos;

    public GameObject player;
    public void Start()
    {
        basePos = transform.position;
        progTime = Random.Range(.25f, .75f);
    }
    public void SetUp(int matType)
    {
        GetComponent<MeshRenderer>().sharedMaterial = materials[matType];
    }

    public void Update()
    {
        if(consumed)
        {
            float newY = curve.Evaluate(progression / progTime);

            progression += Time.deltaTime;

            transform.position = Vector3.Lerp(basePos, player.transform.position, progression / progTime);
            Vector3 pos = transform.position;
            pos.y += newY;
            transform.position = pos;

            if (progression >= progTime)
                Destroy(gameObject);
            
        }
    }



    public void Consume(GameObject newPlayer)
    {
        player = newPlayer;
        consumed = true;
       // Destroy(this);
    }
}
