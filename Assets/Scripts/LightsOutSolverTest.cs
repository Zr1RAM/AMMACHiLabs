using System;
using System.Collections;
using System.Collections.Generic;

public struct Coordinate
{
    public int x,y;

    public Coordinate(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class LightsOutSolverTest
{
    private bool[,] lights;
    private int matrixSize;

    public LightsOutSolverTest(bool[,] lights, int matrixSize)
    {
        this.lights = lights;
        this.matrixSize = matrixSize;
    }

    public List<Coordinate> Solve()
    {
        //List<Tuple<int, int>> solution = new List<Tuple<int, int>>();
        List<Coordinate> solution = new List<Coordinate>();
        Solve(lights, 0, 0, solution);
        return solution;
    }
    private bool Solve(bool[,] grid, int x, int y, List<Coordinate> solution)
    {
        // Check if all lights are off
        if (IsAllLightsOff(grid))
        {
            return true;
        }

        // Flip current light and add to solution
        grid[x, y] = !grid[x, y];
        solution.Add(new Coordinate(x,y));

        bool foundSolution = false;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int newX = x + dx;
                int newY = y + dy;
                if (IsWithinGrid(newX, newY))
                {
                    // Recursively explore neighbors
                    if (Solve(grid, newX, newY, solution))
                    {
                        foundSolution = true;
                        break; // Stop exploring if solution found
                    }
                }
            }
        }

        // Backtrack if no neighbor leads to a solution
        if (!foundSolution)
        {
            grid[x, y] = !grid[x, y];
            int indextoRemove = solution.FindIndex(structItem => (structItem.x == x && structItem.y == y));
            solution.RemoveAt(indextoRemove);
        }
        return foundSolution;
    }

    private bool IsAllLightsOff(bool[,] grid)
    {
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                if (grid[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsWithinGrid(int x, int y)
    {
        return x >= 0 && x < matrixSize && y >= 0 && y < matrixSize;
    }
}
