using UnityEngine;
using System.Collections.Generic;
using System;

public class BuildingGenerator : MonoBehaviour {
    
    public GameObject[] buildingFloors;
    public GameObject[] buildingRooms;
    public GameObject[] buildingFirstWalls;
    public GameObject[] buildingOtherWalls;
    public Vector3 startingPosition;
    public int numberOfBuildings;
    public int maxFloors;
    public int spaceBetween;

    private Building[] buildings;

    public struct Building
    {
        public GameObject floor;
        public float floorWidth;
        public float floorHeight;
        public GameObject room;
        public float roomWidth;
        public float roomHeight;

        public GameObject firstWall;
        public float firstWallWidth;
        public float firstWallHeight;
        public GameObject otherWall;
        public float otherWallWidth;
        public float otherWallHeight;
    }

    void createBuildingArray()
    {
        buildings = new Building[buildingFloors.Length];
        if (buildingFloors.Length != buildingRooms.Length)
        {
            throw new UnityException("BuildingGenerator: Number of rooms and number of floors do not match");
        }

        for (int i = 0; i < buildingFloors.Length; i++)
        {
            buildings[i] = new Building();
            buildings[i].floor = buildingFloors[i];
            buildings[i].floorWidth = buildingFloors[i].GetComponent<SpriteRenderer>().bounds.size.x;
            buildings[i].floorHeight = buildingFloors[i].GetComponent<SpriteRenderer>().bounds.size.y;
            buildings[i].room = buildingRooms[i];
            buildings[i].roomWidth = buildingRooms[i].GetComponent<SpriteRenderer>().bounds.size.x;
            buildings[i].roomHeight = buildingRooms[i].GetComponent<SpriteRenderer>().bounds.size.y;


            buildings[i].firstWall = buildingFirstWalls[i];
            buildings[i].firstWallWidth = buildingFirstWalls[i].GetComponent<SpriteRenderer>().bounds.size.x;
            buildings[i].firstWallHeight = buildingFirstWalls[i].GetComponent<SpriteRenderer>().bounds.size.y;
            buildings[i].otherWall = buildingOtherWalls[i];
            buildings[i].otherWallWidth = buildingOtherWalls[i].GetComponent<SpriteRenderer>().bounds.size.x;
            buildings[i].otherWallHeight = buildingOtherWalls[i].GetComponent<SpriteRenderer>().bounds.size.y;

            logCompareBuildingPart(buildings[i].firstWallWidth, buildings[i].otherWallWidth, i, "first wall width", "other wall width");
            logCompareBuildingPart(buildings[i].firstWallHeight, buildings[i].otherWallHeight, i, "first wall height", "other wall height");
            logCompareBuildingPart(buildings[i].roomHeight, buildings[i].otherWallHeight, i, "room height", "other wall height");
            logCompareBuildingPart(buildings[i].floorWidth, buildings[i].roomWidth + buildings[i].firstWallWidth * 2, i, "floor width", "room plus walls width");
        }
    }

    void logCompareBuildingPart(float num1, float num2, int buildingNumber, string part1, string part2)
    {
        if(Math.Round(num1, 2) != Math.Round(num2, 2))
        {
            Debug.LogError("BuildingGenerator: Building number " + buildingNumber + " " + part1 + " does not match " + part2 + ": " + num1 + ", " + num2);
        }
    }

    void generateAll()
    {
        Vector3 currentPosition = startingPosition;

        // Generate buildings
        for (int i = 0; i < numberOfBuildings; i++)
        {
            // TODO implement a way to chooseBuildingType()
            int buildingNumber = 0;

            // Generate floors
            for (int j = 0; j < findNumberOfFloors(i + 1) ; j++)
            { 
                // Generate room
                GameObject.Instantiate(buildings[buildingNumber].room, currentPosition, Quaternion.identity);

                // Generate Walls, different wall for first floor
                if (j == 0)
                {
                    generateFirstWalls(currentPosition, buildingNumber);
                } else
                {
                    generateOtherWalls(currentPosition, buildingNumber);
                }

                // Generate next floor/ceiling
                // Need to add half the room height and half the floor height to get to the CENTER of the next object
                currentPosition.y += buildings[buildingNumber].roomHeight / 2 + buildings[buildingNumber].floorHeight / 2;
                GameObject.Instantiate(buildings[buildingNumber].floor, currentPosition, Quaternion.identity);

                // Prepare for next room generation
                currentPosition.y += buildings[buildingNumber].floorHeight / 2 + buildings[buildingNumber].roomHeight / 2;
            }

            currentPosition.x += buildings[buildingNumber].floorWidth + spaceBetween;
            currentPosition.y = startingPosition.y;
        }
    }

    void generateFirstWalls(Vector3 currentPosition, int buildingNumber)
    {
        currentPosition.x -= buildings[buildingNumber].roomWidth / 2 + buildings[buildingNumber].firstWallWidth / 2;
        GameObject.Instantiate(buildings[buildingNumber].firstWall, currentPosition, Quaternion.identity);

        currentPosition.x += buildings[buildingNumber].roomWidth + buildings[buildingNumber].firstWallWidth;
        GameObject.Instantiate(buildings[buildingNumber].firstWall, currentPosition, Quaternion.identity);
    }

    void generateOtherWalls(Vector3 currentPosition, int buildingNumber)
    {
        currentPosition.x -= buildings[buildingNumber].roomWidth / 2 + buildings[buildingNumber].otherWallWidth / 2;
        GameObject.Instantiate(buildings[buildingNumber].otherWall, currentPosition, Quaternion.identity);

        currentPosition.x += buildings[buildingNumber].roomWidth + buildings[buildingNumber].otherWallWidth;
        GameObject.Instantiate(buildings[buildingNumber].otherWall, currentPosition, Quaternion.identity);
    }

    // Function to find the number of floors to generate based on the current building number, number of max floors, and the number of buildings
    // buildingNumber should be non-zero indexed (i.e. start at 1)
    int findNumberOfFloors(int buildingNumber)
    {
        return maxFloors - (numberOfBuildings - (buildingNumber)) * (maxFloors / numberOfBuildings);
    }

    void Start()
    {
        createBuildingArray();
        generateAll();
    }
}
