using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreInitialVariables : MonoBehaviour
{
    private ContainInitialVariables container;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the container for the variables
        container = GameObject.Find("Value Container").GetComponent<ContainInitialVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Stores the value of the dropdown menus in the container
    public void StoreDropdown(int Case)
    {
        //gets the dropdown component of the dropdown menu
        Dropdown dropdown = GetComponent<Dropdown>();
        switch (Case)
        {
            case 0:
                container.GroundTexture = dropdown.value;
                break;

            case 1:
                container.CarnivoreModel = dropdown.value;
                break;

            case 2:
                container.PlantModel = dropdown.value;
                break;

            case 3:
                container.HerbivoreModel = dropdown.value;
                break;
        }
        
    }
    
    public void StoreInputField(int Case)
    {
        //gets the input field component of the input field
        InputField inputField = GetComponent<InputField>();
        //gets the outline component of the input field
        Outline outline = GetComponent<Outline>();
        bool parseSuccess;
        bool rangeSuccess = false;
        int result;
        //tries to parse the contents of the input field. Returns true or false whether it was successful
        parseSuccess = int.TryParse(inputField.text, out result);
        //checks that the contents of the input field is within the acceptable range of values
        if (0 <= result && result <= 9999)
        {
            rangeSuccess = true;
        }
        //checks if the contents of the input field failed validation
        if (parseSuccess == false || rangeSuccess == false)
        {
            //if validation fails, success is made false so that the program doesn't progress and a red outline appears
            container.success = false;
            outline.effectDistance = new Vector2(4, 4);

        }
        else
        {
            //if validation succeeds, the contents of the input fields are stored in the container and any red outline is removed
            switch (Case)
            {
                case 0:
                    container.NumOfCycles = result;
                    outline.effectDistance = new Vector2(0, 0);
                    break;

                case 1:
                    container.DaysPerCycle = result;
                    outline.effectDistance = new Vector2(0, 0);
                    break;

                case 2:
                    container.PlantsPerDay = result;
                    outline.effectDistance = new Vector2(0, 0);
                    break;
            }
        }

    }
    
    public void StoreSlider(int Case)
    {
        //gets the silder component of the slider
        Slider slider = GetComponent<Slider>();
        switch (Case)
        {
            case 0:
                container.HillHeight = slider.value;
                break;

            case 1:
                container.HillDensity = slider.value;
                break;

            case 2:
                container.WaterLevel = slider.value;
                break;
        }

    }

    public void StoreGenome(int Case)
    {
        //gets the GenomeAttributes component of the genome dropdown or input field
        GenomeAttributes genomeAttributes = GetComponent<GenomeAttributes>();
        
        //stores the values for each genome in the container
        switch (Case)
        {
            case 0:
                container.genome1.movementSpeed = genomeAttributes.Genome1Value;
                container.genome2.movementSpeed = genomeAttributes.Genome2Value;
                container.genome3.movementSpeed = genomeAttributes.Genome3Value;
                break;

            case 1:
                container.genome1.Sight = genomeAttributes.Genome1Value;
                container.genome2.Sight = genomeAttributes.Genome2Value;
                container.genome3.Sight = genomeAttributes.Genome3Value;
                break;

            case 2:
                container.genome1.detectionRange = genomeAttributes.Genome1Value;
                container.genome2.detectionRange = genomeAttributes.Genome2Value;
                container.genome3.detectionRange = genomeAttributes.Genome3Value;
                break;
            case 3:
                container.genome1.Diet = genomeAttributes.Genome1Value;
                container.genome2.Diet = genomeAttributes.Genome2Value;
                container.genome3.Diet = genomeAttributes.Genome3Value;
                break;
            case 4:
                container.genome1.InitPopulation = genomeAttributes.Genome1Value;
                container.genome2.InitPopulation = genomeAttributes.Genome2Value;
                container.genome3.InitPopulation = genomeAttributes.Genome3Value;
                break;
        }
    }
}
