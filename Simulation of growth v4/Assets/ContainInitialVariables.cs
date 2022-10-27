using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainInitialVariables : MonoBehaviour
{
    public bool success { private get; set; }
    private HideCanvas Canvas;
    public int GroundTexture { get; set; }
    public int HerbivoreModel { get; set; }
    public int CarnivoreModel { get; set; }
    public int PlantModel { get; set; }
    public int NumOfCycles { get; set; }
    public int DaysPerCycle { get; set; }
    public int PlantsPerDay { get; set; }
    public float HillHeight { get; set; }
    public float HillDensity { get; set; }
    public float WaterLevel { get; set; }
    public int currentSpeed { get; set; }

    public bool paused;

    public int[,] pairCounter;

    /*
    public int Movement1 { get; set; }
    public int Sight1 { get; set; }
    public int Reproduction1 { get; set; }
    public int Diet1 { get; set; }
    public int InitPopulation1 { get; set; }
    public int Movement2 { get; set; }
    public int Sight2 { get; set; }
    public int Reproduction2 { get; set; }
    public int Diet2 { get; set; }
    public int InitPopulation2 { get; set; }
    public int Movement3 { get; set; }
    public int Sight3 { get; set; }
    public int Reproduction3 { get; set; }
    public int Diet3 { get; set; }
    public int InitPopulation3 { get; set; }
    */
    
    public struct Genome
    {
        public float movementSpeed { get; set; }
        public int Sight { get; set; }
        public float detectionRange { get; set; }
        public int Diet { get; set; }
        public int InitPopulation { get; set; }
        public int Population { get; set; }
    }
    public Genome genome1;
    public Genome genome2;
    public Genome genome3;
    // Start is called before the first frame update
    void Start()
    {
        pairCounter = new int[3, 3];
        success = true;
        Canvas = GameObject.Find("Canvas initial").GetComponent<HideCanvas>();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreEverything()
    {
        if (success == true)
        {
            Canvas.hideCanvas();

            Simulation simulation = GetComponent<Simulation>();
            StartCoroutine(simulation.runSimulation());
            //EditorCoroutineUtility.StartCoroutine(simulation.runSimulation(), this);
        }
        else
        {
            success = true;
        }
    }

    public void updatePopulation()
    {

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int i = 0; i < pairCounter[x, y]/2; i++)
                {
                    int selectedParent = 0;
                    int randInt = Random.Range(0, 2);
                    if (randInt == 0)
                    {
                        selectedParent = x;
                    }
                    else
                    {
                        selectedParent = y;
                    }

                    switch (selectedParent)
                    {
                        case 0:
                            genome1.Population++;
                            break;
                        case 1:
                            genome2.Population++;
                            break;
                        case 2:
                            genome3.Population++;
                            break;
                    }
                }
            }
        }



        pairCounter = new int[3, 3];//clear the array
    }


}
