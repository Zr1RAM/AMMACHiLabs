using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLogic : MonoBehaviour
{
    [SerializeField]
    int matrixSize = 3;

 
    [SerializeField]
    bool[,] lightsBool;
    void CreateLights(int size)
    {
        print("Lights size: " + size.ToString() + "x" + size.ToString());
        lightsBool = new bool[size,size];
        for (int i = 0; i < size; i++) 
        {
            for(int j = 0; j < size; j++)
            {
                lightsBool[i,j] = false;
            }        
        }
        onGameInitialized(size);

    }
    void onGameInitialized(int size)
    {
        List<int> randomIndexes = new List<int>();
        int randomNumberOfLightsToTurnOn = Random.Range(0, size*size);
        print("Random number of lights to turn on: " + randomNumberOfLightsToTurnOn.ToString());
        randomIndexes.Add(Random.Range(0, size));
        print("random indexes selected to turn on: \n" + randomIndexes[0].ToString());
        lightsBool[randomIndexes[0]/size, randomIndexes[0] % size] = true;
        for (int i = 1; i < randomNumberOfLightsToTurnOn; i++)
        {
            int randomValue = Random.Range(0, size*size);
            while (randomIndexes.Contains(randomValue))
            {
                randomValue = Random.Range(0, size*size);
            }
            randomIndexes.Add(randomValue);
            print(randomIndexes[i].ToString());
            lightsBool[randomIndexes[i] / size, randomIndexes[i] % size] = true;
        }
#if UNITY_EDITOR
        testLightBools();
#endif
    }

    void updateLightsAroundLightAt(int[] index) 
    {
        int x = index[0], y = index[1];
        lightsBool[x, y] = !lightsBool[x, y];
        if ((y - 1) >= 0)
        {
            lightsBool[x, y - 1] = !lightsBool[x, y - 1];
        }
        if ((y + 1) < matrixSize)
        {
            lightsBool[x, y + 1] = !lightsBool[x, y + 1];
        }
        if ((x - 1) >= 0)
        {
            lightsBool[x - 1, y] = !lightsBool[x - 1, y];
        }
        if ((x + 1) < matrixSize)
        {
            lightsBool[x + 1, y] = !lightsBool[x + 1, y];
        }
#if UNITY_EDITOR
        testLightBools();
        print("Lights index selected: " + index[0].ToString() + ", " + index[1].ToString());
#endif

    }


#if UNITY_EDITOR
    // testing variables
    [SerializeField]
    int[] _arrayIndex;

    // testing methods
    void testLightBools()
    {
        string output = "";
        for (int i = 0;i < matrixSize; i++)
        {
            output += "\n";
            for(int j = 0; j < matrixSize; j++)
            {
                output += lightsBool[i, j].ToString() + " ";
            }
        }
        print(output);
    }
#endif
    void Start()
    {
        CreateLights(matrixSize);
        LightsOutSolverTest testingSolver = new LightsOutSolverTest(lightsBool, matrixSize);
        testingSolver.Solve();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //CreateLights(matrixSize);
            testLightBools();
            updateLightsAroundLightAt(_arrayIndex);
        }
#endif
    }
}
