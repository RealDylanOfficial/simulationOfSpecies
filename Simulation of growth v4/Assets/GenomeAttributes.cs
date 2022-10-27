using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenomeAttributes : MonoBehaviour
{
    //initialises the values of each genome
    public int Genome1Value { get; private set; }
    public int Genome2Value { get; private set; }
    public int Genome3Value { get; private set; }
    private int CurrentGenome;
    //initialises a GenomeSelector class component
    private GenomeSelector selector;
    //initialises a dropdown component
    private Dropdown dropdown;
    //initialises an inputfield component
    private InputField inputField;
    //initialises the variables required for the parse validation
    private bool parseSuccess;
    private bool rangeSuccess;
    private int result;

    private ContainInitialVariables container;

    // Start is called before the first frame update
    void Start()
    {
        Genome1Value = 0;
        Genome2Value = 0;
        Genome3Value = 0;
        CurrentGenome = 0;
        //sets dropdown as the dropdown component of the object the script is attached to. Is null if doesn't exist.
        dropdown = GetComponent<Dropdown>();
        //sets dropdown as the dropdown component of the object the script is attached to. Is null if doesn't exist.
        inputField = GetComponent<InputField>();
        //sets selector as the GenomeSelector component of the genome dropdown
        selector = GameObject.Find("Genome Dropdown").GetComponent<GenomeSelector>();
        container = GameObject.Find("Value Container").GetComponent<ContainInitialVariables>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void ChangeGenome()
    {
        //Sets newGenome as the 
        int newGenome = selector.GetCurrentGenome();

        //saves the value of the dropdown in the variable for the relevant genome
        Outline outline = GetComponent<Outline>();
        switch (CurrentGenome)
        {
            //in the case that the first genome was previously being edited
            
            case 0:
                if (inputField == null) //if the object that the script is attached to is a dropdown menu
                {
                    Genome1Value = dropdown.value;
                }
                else
                {
                    //Trys to parse the contents of the input field into an integer. Defaults to 0 if the parse fails.
                    //success is true if parse succeeds, false if it doesn't. result stores the parsed integer,
                    parseSuccess = int.TryParse(inputField.text, out result);
                    rangeSuccess = false;
                    if (0 <= result && result <= 9999)
                    {

                        rangeSuccess = true;
                    }

                    if (parseSuccess == true && rangeSuccess == true)
                    {
                        //stores the value of the input field to the relevent genome
                        Genome1Value = result;
                        outline.effectDistance = new Vector2(0, 0);

                    }
                    else
                    {
                        //defaults value to 0
                        Genome1Value = 0;
                        outline.effectDistance = new Vector2(4, 4);
                        container.success = false;


                    }
                }
                break;

            //in the case that the second genome was previously being edited
            case 1:
                if (inputField == null)
                {
                    Genome2Value = dropdown.value;
                }
                else
                {
                    rangeSuccess = false;
                    parseSuccess = int.TryParse(inputField.text, out result);
                    if (0 <= result && result <= 9999)
                    {
                        rangeSuccess = true;
                    }


                    if (parseSuccess == true && rangeSuccess == true)
                    {
                        Genome2Value = result;
                        outline.effectDistance = new Vector2(0, 0);
                    }
                    else
                    {
                        Genome2Value = 0;
                        outline.effectDistance = new Vector2(4, 4);
                        container.success = false;
                    }
                }
                break;

            //in the case that the third genome was previously being edited
            case 2:
                if (inputField == null)
                {
                    Genome3Value = dropdown.value;
                }
                else
                {
                    rangeSuccess = false;
                    parseSuccess = int.TryParse(inputField.text, out result);
                    if (0 <= result && result <= 9999)
                    {
                        rangeSuccess = true;
                    }


                    if (parseSuccess == true && rangeSuccess == true)
                    {
                        Genome3Value = result;
                        outline.effectDistance = new Vector2(0, 0);
                    }
                    else
                    {
                        Genome3Value = 0;
                        outline.effectDistance = new Vector2(4, 4);
                        container.success = false;
                    }
                }

                break;
        }

        //loads the value of the relevant genome and sets it as the value of the dropdown
        switch (newGenome)
        {
            //in the case that the first genome is now being edited
            case 0:
                if (inputField == null)
                {
                    //sets the value of the dropdown to the value of the relevant genome
                    dropdown.value = Genome1Value;
                }
                else
                {
                    //sets the value of the input field to the value of the relevant genome
                    inputField.text = Genome1Value.ToString();
                }
                CurrentGenome = 0;
                break;

            //in the case that the second genome is now being edited
            case 1:
                if (inputField == null)
                {
                    dropdown.value = Genome2Value;
                }
                else
                {
                    inputField.text = Genome2Value.ToString();
                }
                CurrentGenome = 1;
                break;

            //in the case that the third genome is now being edited
            case 2:
                if (inputField == null)
                {
                    dropdown.value = Genome3Value;
                }
                else
                {
                    inputField.text = Genome3Value.ToString();
                }
                CurrentGenome = 2;
                break;
        }

    }

}
