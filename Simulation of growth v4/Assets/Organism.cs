using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Organism : MonoBehaviour
{
    private float MovementSpeed;
    private int Sight;
    private float DetectionRange;
    private int Diet;
    private int InitPopulation;
    public int Genome;
    private Terrain terrain;
    private ContainInitialVariables Container;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    public Mesh wolfMesh;
    public Mesh goatMesh;
    public Mesh lionMesh;
    public Mesh rabbitMesh;
    public Mesh bearMesh;
    public Mesh reindeerMesh;
    public Mesh arrowMesh;
    public Material wolfMat;
    public Material goatMat;
    public Material lionMat;
    public Material rabbitMat;
    public Material bearMat;
    public Material reindeerMat;
    public Material arrowMat;

    public float hunger;
    public float thirst;
    public float lust;

    public bool moving;
    public bool isBeingEaten;

    public Vector3 searchDirection;

    private TerrainGenerator terrainGenerator;

    public List<GameObject> prey = new List<GameObject>();
    public List<GameObject> mates = new List<GameObject>();
    public List<GameObject> threats = new List<GameObject>();
    int timeCounter;

    private int[,] localPairCounter;
    //public string greatestThreat;

    // Start is called before the first frame update
    void Start()
    {
        
        //terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((timeCounter >= Container.currentSpeed) && (Container.paused != true))
        {
            removeDeadObjects();
            action();
            movement();
            checkStats();
            timeCounter = 0;
        }
        timeCounter++;


    }

    public void setOrganism(int genome, float movementSpeed, int sight, float detectionRange, int diet, int initPopulation, int herbivoreModel, int carnivoreModel, float waterLevel)
    {

        Container = GameObject.Find("Value Container").GetComponent<ContainInitialVariables>();
        terrainGenerator = GameObject.Find("Terrain").GetComponent<TerrainGenerator>();
        terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        Genome = genome;
        MovementSpeed = movementSpeed;
        Sight = sight;
        DetectionRange = detectionRange;
        Diet = diet;
        InitPopulation = initPopulation;

        searchDirection = Vector3.zero;

        localPairCounter = new int[3, 3];

        moving = true;
        isBeingEaten = false;
        timeCounter = 4;

        hunger = 0;
        thirst = 0;
        lust = 0;

        if (sight == 2)
        {
            gameObject.GetComponent<SphereCollider>().radius = 1;
        }
        else
        {
            gameObject.GetComponent<SphereCollider>().radius *= (detectionRange + 1) ;
        }

        


        bool running = true;
        float x;
        float y;
        float z;
        int counter = 0;
        while (running == true)
        {

            x = Random.Range(1f, 255f);
            z = Random.Range(1f, 255f);
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
                    Debug.Log("infinite loop in Organism.cs line 74");
                    running = false;
                }
            }
        }




        if (diet == 0)
        {
            switch (herbivoreModel)
            {
                case 0:
                    meshFilter.mesh = goatMesh;
                    meshRenderer.material = goatMat;
                    transform.localScale = transform.localScale * 15;
                    gameObject.GetComponent<SphereCollider>().radius /= 15;
                    //transform.localScale = new Vector3(15, 15, 15);
                    break;
                case 1:
                    meshFilter.mesh = rabbitMesh;
                    meshRenderer.material = rabbitMat;
                    transform.localScale = transform.localScale * 10;
                    gameObject.GetComponent<SphereCollider>().radius /= 10;
                    //transform.localScale = new Vector3(10, 10, 10);
                    break;
                case 2:
                    meshFilter.mesh = reindeerMesh;
                    meshRenderer.material = reindeerMat;
                    transform.localScale = transform.localScale * 15;
                    gameObject.GetComponent<SphereCollider>().radius /= 15;
                    //transform.localScale = new Vector3(15, 15, 15);
                    break;
            }
        }
        else
        {
            switch (carnivoreModel)
            {
                case 0:
                    meshFilter.mesh = wolfMesh;
                    meshRenderer.material = wolfMat;
                    break;
                case 1:
                    meshFilter.mesh = lionMesh;
                    meshRenderer.material = lionMat;
                    transform.localScale = transform.localScale * 3;
                    gameObject.GetComponent<SphereCollider>().radius /= 3;
                    //transform.localScale = new Vector3(3, 3, 3);
                    break;
                case 2:
                    meshFilter.mesh = bearMesh;
                    meshRenderer.material = bearMat;
                    transform.localScale = transform.localScale * 6;
                    gameObject.GetComponent<SphereCollider>().radius /= 6;
                    //transform.localScale = new Vector3(6, 6, 6);
                    break;
            }
        }
        
        GetComponent<SphereCollider>().center = new Vector3(0, 0, 0);
        /*
        switch (model)
        {
            case 0:
                meshFilter.mesh = wolfMesh;
                meshRenderer.material = wolfMat;
                break;
            case 1:
                meshFilter.mesh = goatMesh;
                meshRenderer.material = goatMat;
                transform.localScale = transform.localScale * 15;
                gameObject.GetComponent<SphereCollider>().radius /= 15;
                //transform.localScale = new Vector3(15, 15, 15);
                break;
            case 2:
                meshFilter.mesh = lionMesh;
                meshRenderer.material = lionMat;
                transform.localScale = transform.localScale * 3;
                gameObject.GetComponent<SphereCollider>().radius /= 3;
                //transform.localScale = new Vector3(3, 3, 3);
                break;
            case 3:
                meshFilter.mesh = rabbitMesh;
                meshRenderer.material = rabbitMat;
                transform.localScale = transform.localScale * 10;
                gameObject.GetComponent<SphereCollider>().radius /= 10;
                //transform.localScale = new Vector3(10, 10, 10);
                break;
            case 4:
                meshFilter.mesh = bearMesh;
                meshRenderer.material = bearMat;
                transform.localScale = transform.localScale * 6;
                gameObject.GetComponent<SphereCollider>().radius /= 6;
                //transform.localScale = new Vector3(6, 6, 6);
                break;
            case 5:
                meshFilter.mesh = reindeerMesh;
                meshRenderer.material = reindeerMat;
                transform.localScale = transform.localScale * 15;
                gameObject.GetComponent<SphereCollider>().radius /= 15;
                //transform.localScale = new Vector3(15, 15, 15);
                break;
        }
        */


    }

    private void detection()
    {

    }



    private void sight()
    {

    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision");
        Organism organism = collision.gameObject.GetComponent<Organism>();

        if ((Diet == 1) && (organism.Diet == 0)) //If our organism's diet is carnivore and collided organism is herbivore
        {
            prey.Add(collision.gameObject);
        }
        else if (Diet == organism.Diet)
        {
            mates.Add(collision.gameObject);
        }
        else if ((Diet == 0) && (organism.Diet == 1))
        {
            threats.Add(collision.gameObject);
        }
    }
    */

    private Vector3 NearestWater()
    {
        Vector3 coordinate = transform.position;
        coordinate.y = 0;
        int x = Mathf.RoundToInt(coordinate.x);
        int y = Mathf.RoundToInt(coordinate.z);
        
        int radius = 2;
        bool running = true;
        int infiniteLoop = 0;

        int distanceToWater = 0;
        int distanceToEdge = 0;

        if (terrainGenerator.WaterMap[x, y])
        {
            
            return transform.position;
        }

        

        while (running == true)
        {
            x = Mathf.RoundToInt(coordinate.x) + (radius / 2);
            y = Mathf.RoundToInt(coordinate.z) - (radius / 2);



            for (int i = 0; (i < radius) && (running == true); i++)
            {
                
                //Debug.Log(running);
                if (waterCheck(x, y))
                {
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.green, (float)0.1);
                    
                    //int height = Mathf.RoundToInt(terrain.SampleHeight(new Vector3(x, 0, y)));
                    //return new Vector3(x, height, y);
                    distanceToWater = Mathf.RoundToInt(Vector2.Distance(new Vector2(coordinate.x, coordinate.z), new Vector2(x, y)));
                    distanceToEdge = radius / 2;
                    running = false;
                    y--;
                }
                y++;

            }

            for (int i = 0; (i < radius) && (running == true); i++)
            {
                
                if (waterCheck(x, y))
                {
                    
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.green, (float)0.1);
                    //int height = Mathf.RoundToInt(terrain.SampleHeight(new Vector3(x, 0, y)));
                    //return new Vector3(x, height, y);
                    distanceToWater = Mathf.RoundToInt(Vector2.Distance(new Vector2(coordinate.x, coordinate.z), new Vector2(x, y)));
                    distanceToEdge = radius / 2;
                    running = false;
                    x++;
                }
                x--;

            }

            for (int i = 0; (i < radius) && (running == true); i++)
            {
                
                if (waterCheck(x, y))
                {
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.green, (float)0.1);
                    //int height = Mathf.RoundToInt(terrain.SampleHeight(new Vector3(x, 0, y)));
                    //return new Vector3(x, height, y);
                    distanceToWater = Mathf.RoundToInt(Vector2.Distance(new Vector2(coordinate.x, coordinate.z), new Vector2(x, y)));
                    distanceToEdge = radius / 2;
                    running = false;
                    y++;
                }
                y--;
            }

            for (int i = 0; (i < radius) && (running == true); i++)
            {
                
                if (waterCheck(x, y))
                {
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.green, (float)0.1);
                    //int height = Mathf.RoundToInt(terrain.SampleHeight(new Vector3(x, 0, y)));
                    //return new Vector3(x, height, y);
                    distanceToWater = Mathf.RoundToInt(Vector2.Distance(new Vector2(coordinate.x, coordinate.z), new Vector2(x, y)));
                    distanceToEdge = radius / 2;
                    running = false;
                    x--;
                }
                x++;
            }
            radius += 2;
            
            infiniteLoop++;
            if (infiniteLoop > 256)
            {
                running = false;
                Debug.Log("Infinite loop while finding nearest water");
                Destroy(gameObject);
            }
        }
        radius -= 2;
        
        Vector3 nearestWater = new Vector3(x, 0, y);

        //float distanceToNearestWater = ((nearestWater.x - coordinate.x) * (nearestWater.x - coordinate.x)) + ((nearestWater.z) - (coordinate.z) * (nearestWater.z) - (coordinate.z));
        float sqrDistanceToWater = Vector3.SqrMagnitude(nearestWater - coordinate);

        int extraPasses = distanceToWater - distanceToEdge;

        for (int j = 0; j < (extraPasses + 1); j++)
        {
            x = Mathf.RoundToInt(coordinate.x) + (radius / 2);
            y = Mathf.RoundToInt(coordinate.z) - (radius / 2);



            for (int i = 0; i < radius; i++)
            {
                
                if (waterCheck(x, y))
                {
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.magenta, (float)0.1);
                    Vector3 tempNearestWater = new Vector3(x, 0, y);
                    float tempSqrDistance = Vector3.SqrMagnitude(tempNearestWater - coordinate);

                    if (tempSqrDistance < sqrDistanceToWater) //If this water is closer
                    {
                        sqrDistanceToWater = tempSqrDistance;
                        nearestWater = tempNearestWater;
                    }
                }
                y++;

            }

            for (int i = 0; i < radius; i++)
            {
                
                if (waterCheck(x, y))
                {
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.magenta, (float)0.1);
                    Vector3 tempNearestWater = new Vector3(x, 0, y);
                    float tempSqrDistance = Vector3.SqrMagnitude(tempNearestWater - coordinate);

                    if (tempSqrDistance < sqrDistanceToWater) //If this water is closer
                    {
                        sqrDistanceToWater = tempSqrDistance;
                        nearestWater = tempNearestWater;
                    }


                }
                x--;

            }

            for (int i = 0; i < radius; i++)
            {
                
                if (waterCheck(x, y))
                {
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.magenta, (float)0.1);
                    Vector3 tempNearestWater = new Vector3(x, 0, y);
                    float tempSqrDistance = Vector3.SqrMagnitude(tempNearestWater - coordinate);

                    if (tempSqrDistance < sqrDistanceToWater) //If this water is closer
                    {
                        sqrDistanceToWater = tempSqrDistance;
                        nearestWater = tempNearestWater;
                    }


                }
                y--;
            }

            for (int i = 0; i < (radius - 1); i++)
            {
                
                if (waterCheck(x, y))
                {
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.magenta, (float)0.1);
                    Vector3 tempNearestWater = new Vector3(x, 0, y);
                    float tempSqrDistance = Vector3.SqrMagnitude(tempNearestWater - coordinate);

                    if (tempSqrDistance < sqrDistanceToWater) //If this water is closer
                    {
                        sqrDistanceToWater = tempSqrDistance;
                        nearestWater = tempNearestWater;
                    }


                }
                x++;
            }
            radius += 2;
        }


        float height = terrain.SampleHeight(nearestWater);
        nearestWater.y = height;

        //Debug.DrawRay(nearestWater, new Vector3(0, 200, 0), Color.cyan, (float)0.1);

        return nearestWater;



        //return Vector3.zero;
    }

    private bool waterCheck(int x, int y)
    {
        if ((x >= 256) || (x < 0) || (y >= 256) || (y < 0))
        {
            return false;
        }
        else
        {
            if (terrainGenerator.WaterMap[x, y])
            {
                return true;
            }
        }
        return false;
    }

    private GameObject evaluateThreat()
    {
        GameObject closestThreat = null;
        float sqrDistance = 1000;

        foreach (GameObject Object in threats)
        {
            float tempSqrDistance = Vector3.SqrMagnitude(transform.position - Object.transform.position);
            if (tempSqrDistance < sqrDistance)
            {
                closestThreat = Object;
                sqrDistance = tempSqrDistance;
            }
        }

        return closestThreat;
    }

    private void checkStats()
    {
        if ((hunger > 100) || (thirst > 100))
        {
            die();
        }
        else
        {
            hunger += (float)0.2;
            if (Container.WaterLevel != 0)
            {
                thirst += (float)0.2;
            }
            
            lust += (float)0.2;
        }
    }

    private void die()
    {
        meshFilter.mesh = arrowMesh;
        meshRenderer.material = arrowMat;
        
        Vector3 Position = transform.position;
        Position.y += 3;
        Position.x -= 1;
        transform.position = Position;
        //Vector3 currentScale = transform.localScale;
        //transform.localScale = currentScale * 10;
        transform.localScale = new Vector3(10, 10, 10);
        Vector3 newRotation = new Vector3(0, 0, 270);
        transform.eulerAngles = newRotation;
        gameObject.tag = "dead";

        if (Genome == 1)
        {
            Container.genome1.Population--;
        }
        else if (Genome == 2)
        {
            Container.genome2.Population--;
        }
        else
        {
            Container.genome3.Population--;
        }

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Container.pairCounter[x, y] -= localPairCounter[x, y];
            }
        }

        Destroy(gameObject.GetComponent<Organism>());
        Destroy(gameObject.GetComponent<SphereCollider>());
    }

    private void movement()
    {
        if (moving == true)
        {
            Vector3 DesiredObject = desired();
            float dangerLimit = 5;

            float distanceToThreat = 1000;

            GameObject Threat = evaluateThreat();
            if (Threat != null)
            {
                distanceToThreat = Vector3.Distance(transform.position, Threat.transform.position);
            }
        

            if (distanceToThreat < dangerLimit)//if threat is close enough that the organism must flee
            {
                Vector3 target = Threat.transform.position - transform.position;


                target.y = 0;
                target = target.normalized;


                Vector3 newPosition = transform.position - (target / (float)(1 / ((MovementSpeed / 10) + 0.1)));
                if (newPosition.x > 0.1 && newPosition.x < 255.9 && newPosition.z > 0.1 && newPosition.z < 255.9 && (terrainGenerator.WaterMap[Mathf.RoundToInt(newPosition.x), Mathf.RoundToInt(newPosition.z)] == false))
                {
                    transform.position = newPosition;
                }

                //transform.position = transform.position - (target / (float)(1 / ((MovementSpeed / 10) + 0.1)));
                
                transform.rotation = Quaternion.LookRotation(-target, -target);

            }
            else if (DesiredObject == Vector3.zero)
            {
                randomMovement();
            }
            else
            {
            
                DesiredObject.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, DesiredObject, (float)((MovementSpeed/10)+0.1));

            
                DesiredObject = DesiredObject - transform.position;
                DesiredObject.y = 0;
                DesiredObject = DesiredObject.normalized;
                if (DesiredObject != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(DesiredObject, DesiredObject);
                }
                


            }
        

            Vector3 position = transform.position;
            position.y = terrain.SampleHeight(transform.position);
            transform.position = position;
        }

        

    }

    private Vector3 desired()
    {
        float greatestDesireValue = 0;
        Vector3 greatestDesireObject = Vector3.zero;

        foreach (GameObject item in prey)
        {
            //if (item != null)
            //{
            if (item != null)
            {
                float Desire = desire(item.transform.position, "prey");

                if (Desire > greatestDesireValue)
                {
                    greatestDesireValue = Desire;
                    greatestDesireObject = item.transform.position;
                }
            }
            //}
            
        }

        foreach (GameObject item in mates)
        {
            if (item != null)
            {
                float Desire = desire(item.transform.position, "mate");

                if (Desire > greatestDesireValue)
                {
                    greatestDesireValue = Desire;
                    greatestDesireObject = item.transform.position;
                }
            }
        }

        if (Container.WaterLevel != 0)
        {
            Vector3 nearestWater = NearestWater();
            float waterDesire = desire(nearestWater, "water");

            if (waterDesire > greatestDesireValue)
            {
                greatestDesireValue = waterDesire;
                greatestDesireObject = nearestWater;
            }
        }
        

        

        return greatestDesireObject;

    }

    private float desire(Vector3 Object, string type)
    {
        if ((waterObstruction(transform.position, Object) == true) && (type != "water"))
        {
            return 0;
        }




        float desire = 0;
        
        float distance = Vector3.Distance(Object, transform.position);
        
        switch (type)
        {
            case "prey":
                if (hunger < 10)
                {
                    return 0;
                }
                desire = hunger/distance;
                break;

            case "mate":
                if (lust < 40)
                {
                    return 0;
                }
                desire = lust / distance;
                break;

            case "water":
                if (thirst < 10)
                {
                    return 0;
                }
                desire = thirst / distance;
                break;
            
        }

        return desire;
    }

    private void randomMovement()
    {
        if ((touchingBorder() == true) || (terrainGenerator.WaterMap[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)] == true) || (searchDirection == Vector3.zero))
        {
            bool running = true;
            int escape = 0;
            while (running == true)
            {
                escape++;
                searchDirection = new Vector3(Random.Range((float)-1.0, (float)1.0), 0, Random.Range((float)-1.0, (float)1.0));
                searchDirection = searchDirection.normalized;
                Vector3 tempPosition = transform.position;
                tempPosition = tempPosition + (searchDirection);
                if ((tempPosition.x < 0) || (tempPosition.x > 256) || (tempPosition.z < 0) || (tempPosition.z > 256) || (terrainGenerator.WaterMap[Mathf.RoundToInt(tempPosition.x), Mathf.RoundToInt(tempPosition.z)] == true))
                {
                    if (escape == 30)
                    {
                        running = false;
                        Debug.Log("Infinite loop in randomMovement()");
                    }
                }
                else
                {
                    running = false;
                }
            }
            
        }

        //transform.position = transform.position + (searchDirection / 5);
        transform.position = transform.position + (searchDirection / (float)(1/((MovementSpeed/10)+0.1)));
        transform.rotation = Quaternion.LookRotation(searchDirection, searchDirection);
    }

    private bool touchingBorder()
    {
        if ((transform.position.x >= 255) || (transform.position.x <= 1) || (transform.position.z >= 255) || (transform.position.z <= 1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool waterObstruction(Vector3 point1, Vector3 point2)
    {
        bool isObstructed = false;
        float deltaX = 0;
        float deltaY = 0;

        
        if (point1.x < point2.x)
        {
            /*
            deltaX = point2.x - point1.x;
            deltaY = point2.z - point1.z;
            float error = -1;
            //float slope = deltaY / deltaX;
            float slope = Mathf.Abs(deltaY / deltaX);
            int y = Mathf.RoundToInt(point1.z);



            for (int x = Mathf.RoundToInt(point1.x); x < Mathf.RoundToInt(point2.x); x++)
            {
                Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.blue, 1);
                if (terrainGenerator.WaterMap[x, y] == true)
                {
                    isObstructed = true;

                }

                error += slope;
                if ((error >= 0) && (point1.z > point2.z))
                {
                    y -= 1;
                    error -= 1;
                    Debug.Log(slope);
                }
                else if ((error >= 0) && (point2.z > point1.z))
                {
                    y += 1;
                    error -= 1;
                    Debug.Log(slope);
                }
            }
            */
            deltaX = point2.x - point1.x;
            deltaY = point2.z - point1.z;

            //deltaX = point2.z - point1.z;
            //deltaY = point2.x - point1.x;
            float error = -1;
            //float slope = deltaY / deltaX;
            float slope = Mathf.Abs(deltaY / deltaX);

            if (slope < 1)
            {
                float y = point1.z;
                //Debug.Log("5");
                for (int x = Mathf.RoundToInt(point1.x); x < Mathf.RoundToInt(point2.x); x++)
                {
                    //Debug.Log("7");
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.blue, 1);
                    if (terrainGenerator.WaterMap[x, Mathf.RoundToInt(y)] == true)
                    {
                        isObstructed = true;

                    }

                    error += slope;
                    if ((error >= 0) && (point1.z > point2.z)) //x+ z-
                    {
                        y -= 1;
                        error -= 1;
                        //Debug.Log("1");

                    }
                    else if ((error >= 0) && (point2.z > point1.z))//x+ z+
                    {
                        y += 1;
                        error -= 1;
                        //Debug.Log("2");

                    }
                }

            }
            else
            {
                float x = point1.x;
                //Debug.Log("6");

                if (point1.z < point2.z)
                {
                    for (int y = Mathf.RoundToInt(point1.z); y < Mathf.RoundToInt(point2.z); y++)
                    {
                        //Debug.Log("9");
                        //Debug.Log("test");
                        //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.blue, 1);
                        if (terrainGenerator.WaterMap[Mathf.RoundToInt(x), y] == true)
                        {
                            isObstructed = true;

                        }

                        error += 1 / slope;
                        if ((error >= 0) && (point1.x > point2.x))
                        {
                            x -= 1;
                            error -= 1;
                            //Debug.Log("10");

                        }
                        else if ((error >= 0) && (point2.x > point1.x))
                        {
                            x += 1;
                            error -= 1;
                            //Debug.Log("11");

                        }
                    }
                }
                else
                {
                    for (int y = Mathf.RoundToInt(point1.z); y > Mathf.RoundToInt(point2.z); y--)
                    {
                        //Debug.Log("8");
                        //Debug.Log("test");
                        //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.blue, 1);
                        if (terrainGenerator.WaterMap[Mathf.RoundToInt(x), y] == true)
                        {
                            isObstructed = true;

                        }

                        error += 1 / slope;
                        if ((error >= 0) && (point1.x > point2.x))
                        {
                            x -= 1;
                            error -= 1;
                            //Debug.Log("3");

                        }
                        else if ((error >= 0) && (point2.x > point1.x))
                        {
                            x += 1;
                            error -= 1;
                            //Debug.Log("4");

                        }
                    }
                }

                
            }
        }
        else
        {
            deltaX = point2.x - point1.x;
            deltaY = point2.z - point1.z;

            //deltaX = point2.z - point1.z;
            //deltaY = point2.x - point1.x;
            float error = -1;
            //float slope = deltaY / deltaX;
            float slope = Mathf.Abs(deltaY / deltaX);
            
            if (slope < 1)
            {
                float y = point1.z;

                for (int x = Mathf.RoundToInt(point1.x); x > Mathf.RoundToInt(point2.x); x--)
                {
                    //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.blue, 1);
                    if (terrainGenerator.WaterMap[x, Mathf.RoundToInt(y)] == true)
                    {
                        isObstructed = true;

                    }

                    error += slope;
                    if ((error >= 0) && (point1.z > point2.z))
                    {
                        y -= 1;
                        error -= 1;
                        //Debug.Log(slope);
                        
                    }
                    else if ((error >= 0) && (point2.z > point1.z))
                    {
                        y += 1;
                        error -= 1;
                        //Debug.Log(slope);
                        
                    }
                }
                
            }
            else
            {
                float x = point1.x;

                if (point1.z < point2.z)
                {
                    for (int y = Mathf.RoundToInt(point1.z); y < Mathf.RoundToInt(point2.z); y++)
                    {
                        //Debug.Log("test");
                        //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.blue, 1);
                        if (terrainGenerator.WaterMap[Mathf.RoundToInt(x), y] == true)
                        {
                            isObstructed = true;

                        }

                        error += 1 / slope;
                        if ((error >= 0) && (point1.x > point2.x))
                        {
                            x -= 1;
                            error -= 1;
                            //Debug.Log(slope);

                        }
                        else if ((error >= 0) && (point2.x > point1.x))
                        {
                            x += 1;
                            error -= 1;
                            //Debug.Log(slope);

                        }
                    }
                }
                else
                {
                    for (int y = Mathf.RoundToInt(point1.z); y > Mathf.RoundToInt(point2.z); y--)
                    {
                        //Debug.Log("test");
                        //Debug.DrawRay(new Vector3(x, 0, y), new Vector3(0, 20, 0), Color.blue, 1);
                        if (terrainGenerator.WaterMap[Mathf.RoundToInt(x), y] == true)
                        {
                            isObstructed = true;

                        }

                        error += 1 / slope;
                        if ((error >= 0) && (point1.x > point2.x))
                        {
                            x -= 1;
                            error -= 1;
                            //Debug.Log(slope);

                        }
                        else if ((error >= 0) && (point2.x > point1.x))
                        {
                            x += 1;
                            error -= 1;
                            //Debug.Log(slope);

                        }
                    }
                }

                
            }
            
        }
        
        

        return isObstructed;
    }

    private void action()
    {
        
        if (moving == true)
        {
            if (hunger >= 10)
            {
                List<GameObject> tempPreyList = prey;
                for (int i = 0; i < prey.Count; i++)
                {
                    /*
                    bool active;
                    try
                    {
                        active = prey[i].activeInHierarchy;
                    }
                    catch (System.Exception)
                    {
                        active = false;
                        throw;
                    }
                    */
                    if (prey[i] != null)
                    {
                    
                        Vector3 preyPosition = prey[i].transform.position;

                        if (((transform.position.x - 2 < preyPosition.x) && (preyPosition.x <= transform.position.x + 2)) && ((transform.position.z - 2 < preyPosition.z) && (preyPosition.z <= transform.position.z + 2)))
                        {
                            Organism organism;
                            if ((prey[i].TryGetComponent<Organism>(out organism) == true))
                            {
                                if (organism.isBeingEaten == false)
                                {
                                    StartCoroutine(consumeOrganism(prey[i]));
                                }
                                

                            }
                            else
                            {
                                StartCoroutine(consumePlant(prey[i]));
                            }
                            tempPreyList.Remove(prey[i]);
                        }
                    }






                }
                prey = tempPreyList;
            }

            /*
            List<GameObject> tempPreyList = prey;
            foreach (GameObject Prey in prey)
            {
                
                MeshFilter meshFilter = GetComponent<MeshFilter>();
                if (bounds.Intersects(meshFilter.mesh.bounds) == true)
                {
                    Organism organism;
                    if (Prey.TryGetComponent<Organism>(out organism) == true)
                    {
                        StartCoroutine(consumeOrganism(Prey));
                        
                    }
                    else
                    {
                        StartCoroutine(consumePlant(Prey));
                    }
                    tempPreyList.Remove(Prey);
                }
            }
            prey = tempPreyList;
            */
            if (lust >= 40)
            {
                List<GameObject> tempMatesList = mates;

                for (int i = 0; i < mates.Count; i++)
                {
                    if (mates[i] != null)
                    {
                        Vector3 matePosition = mates[i].transform.position;

                        if (((transform.position.x - 1 <= matePosition.x) && (matePosition.x <= transform.position.x + 1)) && ((transform.position.z - 1 <= matePosition.z) && (matePosition.z <= transform.position.z + 1)))
                        {
                            StartCoroutine(reproduce(mates[i]));
                            tempMatesList.Remove(mates[i]);
                        }
                    }
                }
                mates = tempMatesList;
            }


            /*
            foreach (GameObject Mate in mates)
            {
                //Organism mateOrganism = Mate.GetComponent<Organism>();
                Debug.Log("reproduce2");

                Vector3 matePosition = Mate.transform.position;

                if (((transform.position.x - 1 <= matePosition.x) && (matePosition.x <= transform.position.x + 1)) && ((transform.position.z - 1 <= matePosition.z) && (matePosition.z <= transform.position.z + 1)))
                {
                    StartCoroutine(reproduce(Mate));
                }
            }
            */
            if (thirst >= 10)
            {
                if (terrainGenerator.WaterMap[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)] == true)
                {

                    StartCoroutine(drink());
                }
            }
            
        }
        
    }

    private IEnumerator drink()
    {
        moving = false;
        
        for (int i = 0; i < (180 / (4 / Container.currentSpeed)); i++)//wait for 3 seconds while drinking
        {

            yield return null;

        }
        
        moving = true;

        if (thirst <= 100)
        {
            thirst = 0;
        }
        else
        {
            thirst -= 100;
        }
    }

    private IEnumerator reproduce(GameObject mate)
    {
        

        Organism mateOrganism = mate.GetComponent<Organism>();

        if (Genome > mateOrganism.Genome)
        {
            Container.pairCounter[Genome - 1, mateOrganism.Genome - 1]++;
            localPairCounter[Genome - 1, mateOrganism.Genome - 1]++;
        }
        else
        {
            Container.pairCounter[mateOrganism.Genome - 1, Genome - 1]++;
            localPairCounter[mateOrganism.Genome - 1, Genome - 1]++;
        }

        if (lust <= 100)
        {
            lust = 0;
        }
        else
        {
            lust -= 100;
        }

        moving = false;

        for (int i = 0; i < (180 / (4 / Container.currentSpeed)); i++)//wait for 3 seconds while drinking
        {

            yield return null;

        }

        moving = true;
    }

    private IEnumerator consumeOrganism(GameObject prey)
    {
        Debug.Log("consume org");
        Organism preyOrganism = prey.GetComponent<Organism>();
        preyOrganism.moving = false;
        moving = false;
        preyOrganism.isBeingEaten = true;

        for (int i = 0; i < (180 / (4 / Container.currentSpeed)); i++)//wait for 3 seconds while eating
        {

            yield return null;

        }
        
        preyOrganism.die();
        moving = true;

        if (hunger <= 100)
        {
            hunger = 0;
        }
        else
        {
            hunger -= 100;
        }
    }

    private IEnumerator consumePlant(GameObject plant)
    {
        moving = false;
        for (int i = 0; i < (180 / (4 / Container.currentSpeed)); i++)//wait for 3 seconds while eating
        {
            
            yield return null;

        }
        Destroy(plant);
        moving = true;
        
        if (hunger <= 100)
        {
            hunger = 0;
        }
        else
        {
            hunger -= 100;
        }

    }

    private void removeDeadObjects()
    {
        List<GameObject> tempPrey = prey;
        for (int i = 0; i < prey.Count; i++)
        {
            if (prey[i] == null)
            {
                tempPrey.Remove(prey[i]);
            }
            else if (prey[i].tag == "dead")
            {
                tempPrey.Remove(prey[i]);
            }
        }
        prey = tempPrey;

        List<GameObject> tempMates = mates;
        for (int i = 0; i < mates.Count; i++)
        {
            if (mates[i].tag == "dead")
            {
                tempMates.Remove(mates[i]);
            }
        }
        mates = tempMates;

        List<GameObject> tempThreats = threats;
        for (int i = 0; i < threats.Count; i++)
        {
            if (threats[i].tag == "dead")
            {
                tempThreats.Remove(threats[i]);
            }
        }
        threats = tempThreats;
    }

    private void resetDetected()
    {
        threats.Clear();
        mates.Clear();
        prey.Clear();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(collision);
        Organism organism;
        Plant plant;




        if (collision.gameObject.TryGetComponent<Organism>(out organism) == true)
        {
            //Organism organism = collision.gameObject.GetComponent<Organism>();

            if ((Diet == 1) && (organism.Diet == 0)) //If our organism's diet is carnivore and collided organism is herbivore
            {
                if (Sight != 1)
                {
                    prey.Add(collision.gameObject);
                }
                else
                {
                    RaycastHit hitInfo = new RaycastHit();

                    LayerMask mask = ~3;

                    float maxDistance = 30;

                    Vector3 origin = transform.position;
                    origin.y += (float)0.5;
                    float distanceToObject = Vector3.Distance(transform.position, collision.transform.position);

                    Physics.Raycast(origin, collision.transform.position - transform.position, out hitInfo, maxDistance, mask);
                    //Debug.DrawRay(origin, collision.transform.position - transform.position, Color.red, 3000);

                    if (hitInfo.distance == 0)
                    {
                        hitInfo.distance = 1000;
                    }


                    if (distanceToObject > hitInfo.distance)
                    {

                    }
                    else
                    {

                        prey.Add(collision.gameObject);
                    }
                }

            }
            else if (Diet == organism.Diet)
            {
                if (Sight != 1)
                {
                    mates.Add(collision.gameObject);
                }
                else
                {
                    //collision.GetComponent<SphereCollider>().enabled = false;
                    RaycastHit hitInfo = new RaycastHit();

                    LayerMask mask = ~3;

                    float maxDistance = 30;

                    Vector3 origin = transform.position;
                    origin.y += (float)0.5;
                    float distanceToObject = Vector3.Distance(transform.position, collision.transform.position);

                    Physics.Raycast(origin, collision.transform.position - transform.position, out hitInfo, maxDistance, mask);

                    //Debug.DrawRay(origin, collision.transform.position - transform.position, Color.red, 3000);
                    /*
                    Debug.Log("Obama" + collision.transform.position);
                    
                    Debug.Log("");
                    Debug.Log(gameObject.name);
                    Debug.Log(collision.gameObject.name);
                    
                    Debug.Log(distanceToObject);
                    Debug.Log(hitInfo.distance);
                    
                    try
                    {
                        Debug.Log(hitInfo.collider.gameObject.name);
                    }
                    catch (System.Exception)
                    {
                        Debug.Log("no collision");
                        
                    }
                    */

                    if (hitInfo.distance == 0)
                    {
                        hitInfo.distance = 1000;
                    }


                    if (distanceToObject > hitInfo.distance)
                    {
                        //Debug.Log("Test1");
                    }
                    else
                    {
                        //Debug.Log("Test2");
                        mates.Add(collision.gameObject);
                    }

                    /*
                    if (hitInfo.collider == collision)
                    {
                        mates.Add(collision.gameObject);
                    }
                    */

                    //collision.GetComponent<SphereCollider>().enabled = true;
                }

            }
            else if ((Diet == 0) && (organism.Diet == 1))
            {
                if (Sight != 1)
                {
                    threats.Add(collision.gameObject);
                }
                else
                {

                    RaycastHit hitInfo = new RaycastHit();

                    LayerMask mask = ~3;

                    float maxDistance = 30;

                    Vector3 origin = transform.position;
                    origin.y += (float)0.5;
                    float distanceToObject = Vector3.Distance(transform.position, collision.transform.position);

                    Physics.Raycast(origin, collision.transform.position - transform.position, out hitInfo, maxDistance, mask);
                    //Debug.DrawRay(origin, collision.transform.position - transform.position, Color.red, 3000);

                    if (hitInfo.distance == 0)
                    {
                        hitInfo.distance = 1000;
                    }


                    if (distanceToObject > hitInfo.distance)
                    {

                    }
                    else
                    {

                        threats.Add(collision.gameObject);
                    }


                }
            }
        }
        else if (collision.gameObject.TryGetComponent<Plant>(out plant) == true)
        {

            if (Diet == 0)
            {

                if (Sight != 1)
                {
                    prey.Add(collision.gameObject);
                }
                else
                {

                    RaycastHit hitInfo = new RaycastHit();

                    LayerMask mask = ~3;

                    float maxDistance = 30;

                    Vector3 origin = transform.position;
                    origin.y += (float)0.5;
                    float distanceToObject = Vector3.Distance(transform.position, collision.transform.position);

                    Physics.Raycast(origin, collision.transform.position - transform.position, out hitInfo, maxDistance, mask);
                    //Debug.DrawRay(origin, collision.transform.position - transform.position, Color.red, 3000);

                    if (hitInfo.distance == 0)
                    {
                        hitInfo.distance = 1000;
                    }


                    if (distanceToObject > hitInfo.distance)
                    {

                    }
                    else
                    {

                        prey.Add(collision.gameObject);
                    }


                }
            }


        }
        
    }
}
