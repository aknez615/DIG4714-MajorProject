using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnvironment : MonoBehaviour
{
    float XPos, ZPos;
    float xScale, yScale, zScale;

    // Start is called before the first frame update
    void Start()
    {
        CreateRandomEnvironment();
    }

    void CreateRandomEnvironment()
    {
        for (int i = 0; i < 200; i++)
        {
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            xScale = Random.Range(0.2f, 1.2f);
            yScale = Random.Range(0.5f, 1.5f);
            zScale = Random.Range(0.2f, 1.2f);
            cylinder.transform.localScale = new UnityEngine.Vector3(xScale, yScale, zScale);

            XPos = Random.Range(-50,50);
            ZPos = Random.Range(-50,50);
            cylinder.transform.position = new UnityEngine.Vector3(XPos, yScale, ZPos);

            Renderer renderer = cylinder.GetComponent<Renderer>();
            renderer.material = new Material(Shader.Find("Standard"));
            renderer.material.color = new Color(Random.Range(0 , 0.5f),Random.Range(0.5f , 1),Random.Range(0 , 0.5f));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
