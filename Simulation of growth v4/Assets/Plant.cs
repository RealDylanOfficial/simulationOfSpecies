using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private int PlantNum;
    private Terrain terrain;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    public Mesh GrassMesh;
    public Mesh BushMesh;
    public Mesh DaisyMesh;

    public Material GrassMaterial;
    public Material BushMaterial;
    public Material DaisyMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setPlant(int plantNum, int model, float waterLevel)
    {

        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        PlantNum = plantNum;
        //float x = Random.Range(0f, 256f);
        //float z = Random.Range(0f, 256f);
        //float y = terrain.SampleHeight(new Vector3(x, 0, z));
        //gameObject.transform.position = new Vector3(x, y, z);
        bool running = true;
        float x;
        float y;
        float z;
        int counter = 0;
        while (running == true)
        {

            x = Random.Range(0f, 256f);
            z = Random.Range(0f, 256f);
            if (terrain.SampleHeight(new Vector3(x, 0, z)) >= waterLevel)
            {
                y = terrain.SampleHeight(new Vector3(x, 0, z));
                gameObject.transform.position = new Vector3(x, y, z);
                running = false;
            }
            else
            {
                counter += 1;
                if (counter == 1000)
                {
                    Debug.Log("infinite loop in Plant.cs line 64");
                    running = false;
                }
            }
        }

        

        switch (model)
        {
            case 0:
                meshFilter.mesh = GrassMesh;
                meshRenderer.material = GrassMaterial;
                transform.localScale = transform.localScale * 3;
                gameObject.GetComponent<SphereCollider>().radius /= 3;

                break;
            case 1:
                meshFilter.mesh = BushMesh;
                Material[] materials = new Material[2];
                materials[0] = BushMaterial;
                materials[1] = BushMaterial;
                meshRenderer.materials = materials;
                
                
                break;
            case 2:
                meshFilter.mesh = DaisyMesh;
                meshRenderer.material = DaisyMaterial;
                transform.localScale = transform.localScale * 20;
                gameObject.GetComponent<SphereCollider>().radius /= 20;

                break;

        }
    }
}
