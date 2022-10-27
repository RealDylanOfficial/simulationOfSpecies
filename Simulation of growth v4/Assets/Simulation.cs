using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Simulation : MonoBehaviour
{
    private ContainInitialVariables Container;
    private TerrainGenerator terrainGenerator;
    public GameObject organismPrefab;
    public GameObject plantPrefab;
    private Water water;
    //private bool simulating;
    GameObject[] organisms;
    GameObject[] plants;
    //public int timeCounter;
    public int currentDay;
    public int currentCycle;
    public GameObject canvas;
    public Text cycleText;
    public Text dayText;
    int remainingTime;

    public Text timeText;
    //int counter3;
    // Start is called before the first frame update
    void Start()
    {
        //timeCounter = 0;
        Application.targetFrameRate = 60;
        Container = GetComponent<ContainInitialVariables>();
        terrainGenerator = GameObject.Find("Terrain").GetComponent<TerrainGenerator>();
        water = GameObject.Find("Water plane").GetComponent<Water>();
        //simulating = false;
        organisms = new GameObject[0];
        plants = new GameObject[0];
        

        //canvas = GameObject.Find("Canvas simulation");
}

    // Update is called once per frame
    void Update()
    {
        
        //if (counter3 == (7200 / (Container.currentSpeed / 4)))
        //{
            //Debug.Log("update");
        //}


        //if (simulating == true)
        //{

            //counter3++;

            //if (timeCounter == Container.currentSpeed)
            //{
                //timeCounter = 1;
            //}
            //else
                //timeCounter++;
        //}

        
    }

    public IEnumerator runSimulation()
    {
        currentCycle = 0;
        currentDay = 0;
        Debug.Log("Simulation");
        terrainGenerator.setTerrainVariables(Container.HillDensity, Container.HillHeight, Container.GroundTexture);
        water.setWater(Container.WaterLevel);
        canvas.SetActive(true);
        Container.currentSpeed = 4;
        //simulating = true;
        for (int i = 0; i < Container.NumOfCycles; i++)
        {
            yield return StartCoroutine(runCycle());
            //yield return EditorCoroutineUtility.StartCoroutine(runCycle(), this);
        }
 
    }

    private void runDay()
    {
        
        Debug.Log("Day");
        currentDay++;
        dayText.text = currentDay.ToString();
        for (int i = 0; i < organisms.Length; i++)
        {
            Destroy(organisms[i]);
        }

        for (int i = 0; i < plants.Length; i++)
        {
            Destroy(plants[i]);
        }
        
        createOrganisms();
        createPlants();
        //int tempCounter = 0;
        //bool running = true;
        //int counter = 0;

        /*
        while (running == true)
        {

            
            counter++;
            //With default speed, will count a fifteenth of a second, 1800 times for a total of 2 minutes
            if (timeCounter == 1)
            {
                tempCounter++;
            }
            if (tempCounter == 450)
            {

                running = false;
            }
            

            //if (counter == 1000)
            //{
                //running = false;
            //}
        }
        */
        


    }


    private IEnumerator runCycle()
    {
        //int counter = 0;
        Debug.Log("cycle");
        currentCycle++;
        cycleText.text = currentCycle.ToString();
        terrainGenerator.newTerrain(Container.WaterLevel);
        
        Container.genome1.Population = Container.genome1.InitPopulation;
        Container.genome2.Population = Container.genome2.InitPopulation;
        Container.genome3.Population = Container.genome3.InitPopulation;
        for (int i = 0; i < Container.DaysPerCycle; i++)
        {
            //StartCoroutine(runDay());
            runDay();
            //remainingTime = 7200 / (4 / Container.currentSpeed);
            remainingTime = 3600;
            //for (int j = 0; j < (remainingTime / (4 / Container.currentSpeed));)
            for (int j = 0; j < (remainingTime);)
            {
                
                if (Container.paused != true)
                {

                    remainingTime -= (4/Container.currentSpeed);
                    timeText.text = (((float)remainingTime / 3600) * 100).ToString();
                    
                }


                //remainingTime--;
                //counter++;
                //Debug.Log(counter);
                //yield return new WaitForEndOfFrame();

                //yield return EditorCoroutineUtility.StartCoroutine(oneFrameDelay(), this);
                yield return null;
                
            }
            Debug.Log(Container.genome1.Population);
            Container.updatePopulation();

        }
        currentDay = 0;

        //createOrganisms();
        //createPlants();

    }

    IEnumerator oneFrameDelay() 
    {
        yield return null;
        /*int count = 0;
        while (count == 0)
        {
            count++;
            yield return null;
            Debug.Log("coggers");
        }*/
        
    }

    private void createOrganisms()
    {



        //GameObject[] organisms1 = new GameObject[Container.genome1.InitPopulation];
        //GameObject[] organisms2 = new GameObject[Container.genome2.InitPopulation];
        //GameObject[] organisms3 = new GameObject[Container.genome3.InitPopulation];
        //GameObject[] organisms = new GameObject[Container.genome1.InitPopulation + Container.genome2.InitPopulation + Container.genome3.InitPopulation];
        organisms = new GameObject[Container.genome1.Population + Container.genome2.Population + Container.genome3.Population];

        int counter = 0;
        for (int i = 0; i < Container.genome1.Population; i++)
        {
            
            organisms[counter] = Instantiate(organismPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            organisms[counter].name = "organism" + counter;
            organisms[counter].GetComponent<Organism>().setOrganism(1, Container.genome1.movementSpeed, Container.genome1.Sight, Container.genome1.detectionRange, Container.genome1.Diet, Container.genome1.InitPopulation, Container.HerbivoreModel, Container.CarnivoreModel, Container.WaterLevel);
            counter += 1;

            //organism = new Organism(1, Container.genome1.Movement, Container.genome1.Sight, Container.genome1.Reproduction, Container.genome1.Diet, Container.genome1.InitPopulation);


            /*
            organisms[counter] = new GameObject("organism"+counter);
            organisms[counter].AddComponent<Organism>();
            organisms[counter].GetComponent<Organism>().setOrganism(1, Container.genome1.Movement, Container.genome1.Sight, Container.genome1.Reproduction, Container.genome1.Diet, Container.genome1.InitPopulation);
            counter += 1; */
        }
        for (int i = 0; i < Container.genome2.Population; i++)
        {
            organisms[counter] = Instantiate(organismPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            organisms[counter].name = "organism" + counter;
            organisms[counter].GetComponent<Organism>().setOrganism(2, Container.genome2.movementSpeed, Container.genome2.Sight, Container.genome2.detectionRange, Container.genome2.Diet, Container.genome2.InitPopulation, Container.HerbivoreModel, Container.CarnivoreModel, Container.WaterLevel);
            counter += 1;

            /*
            organisms[counter] = new GameObject("organism" + counter);
            organisms[counter].AddComponent<Organism>();
            organisms[counter].GetComponent<Organism>().setOrganism(2, Container.genome2.Movement, Container.genome2.Sight, Container.genome2.Reproduction, Container.genome2.Diet, Container.genome2.InitPopulation);
            counter += 1; */
        }
        for (int i = 0; i < Container.genome3.Population; i++)
        {
            organisms[counter] = Instantiate(organismPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            organisms[counter].name = "organism" + counter;
            organisms[counter].GetComponent<Organism>().setOrganism(3, Container.genome3.movementSpeed, Container.genome3.Sight, Container.genome3.detectionRange, Container.genome3.Diet, Container.genome3.InitPopulation, Container.HerbivoreModel, Container.CarnivoreModel, Container.WaterLevel);
            counter += 1;

            /*
            organisms[counter] = new GameObject("organism" + counter);
            organisms[counter].AddComponent<Organism>();
            organisms[counter].GetComponent<Organism>().setOrganism(3, Container.genome3.Movement, Container.genome3.Sight, Container.genome3.Reproduction, Container.genome3.Diet, Container.genome3.InitPopulation);
            counter += 1; */
        }
    }

    private void createPlants()
    {
        plants = new GameObject[Container.PlantsPerDay];

        for (int i = 0; i < Container.PlantsPerDay; i++)
        {
            plants[i] = Instantiate(plantPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            plants[i].name = "plant" + i;
            plants[i].GetComponent<Plant>().setPlant(Container.PlantsPerDay, Container.PlantModel, Container.WaterLevel);
        }
    }
}
