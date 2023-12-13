using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLogic : MonoBehaviour
{
    [SerializeField]
    int matrixSize = 3;

    bool[] lightsBool;
    void CreateLights(int size)
    {
        print("Lights size: " + size.ToString());
        lightsBool = new bool[size];
        for (int i = 0; i < size; i++) 
        {
            lightsBool[i] = false;        
        }
        onGameInitialized(size);

    }
    void onGameInitialized(int size)
    {
        List<int> randomIndexes = new List<int>();
        int randomNumberOfLightsToTurnOn = Random.Range(0, size);
        print("Random number of lights to turn on: " + randomNumberOfLightsToTurnOn.ToString());
        randomIndexes.Add(Random.Range(0, size));
        print("random indexes selected to turn on: \n" + randomIndexes[0].ToString());
        lightsBool[randomIndexes[0]] = true;
        for (int i = 1; i < randomNumberOfLightsToTurnOn; i++)
        {
            int randomValue = Random.Range(0, size);
            while (randomIndexes.Contains(randomValue))
            {
                randomValue = Random.Range(0, size);
            }
            randomIndexes.Add(randomValue);
            print(randomIndexes[i].ToString());
            lightsBool[randomIndexes[i]] = true;
        }
        testLightBools();
    }
    void testLightBools()
    {
        string output = "";
        for (int i = 0;i < matrixSize * matrixSize; i++)
        {
            if (i % matrixSize == 0)
            {
                output += "\n" + lightsBool[i].ToString() + " ";
            }
            else
            {
                output += lightsBool[i].ToString() + " ";
            }
        }
        print(output);
    }
    void Start()
    {
        CreateLights(matrixSize * matrixSize);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateLights(matrixSize * matrixSize);
        }
    }
}
