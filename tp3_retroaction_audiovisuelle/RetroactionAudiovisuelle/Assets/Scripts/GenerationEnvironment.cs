using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GenerationEnvironment : MonoBehaviour
{
    public GameObject boxDestroyPrefab;
    public Text seed;

    public float elevation = 0.2f;
    public const int width = 10;
    public const int height = 10;
    public const int percentFill = 75;

    private static int current = 0;

    int[] map = null;

    private bool checkForPlayers()
    {
        int j = current % width;
        int i = current / height;

        if (i == 0 && (j == 0 || j == 1 || j == width - 1 || j == width - 2))
            return false;
        if (i == 1 && ((j == 0 || j == width - 1)))
            return false;
        if (i == height - 1 && (j == 0 || j == 1 || j == width - 1 || j == width - 2))
            return false;
        if (i == height - 2 && ((j == 0 || j == width - 1)))
            return false;
        return true;
    }

    public GameObject SetEnvironment()
    {
        float beginX = 4.2f;
        float beginZ = 3.5f;
        float offsetX = -0.83f;
        float offsetZ = -0.84f;

        if (map[current] == 1)
        {
            if (checkForPlayers())
            {
                Vector3 pos = new Vector3(offsetX * (current % width) + beginX, elevation, offsetZ * (current / height) + beginZ);
                Quaternion rotation = Quaternion.identity;
                GameObject gameobject = Instantiate(this.boxDestroyPrefab, pos, rotation);
                return gameobject;
            }
        }
        return null;
    }

    private void initMap()
    {
        map = new int[width * height];
        for (int i = 0; i < width * height; i++)
            map[i] = 0;
    }

    public List<GameObject> GenerateEnvironment()
    {
        initMap();
        int maxValue = 1000;

        Random.InitState(int.Parse(seed.text));
        for (int i = 0; i < width * height; i++)
        {
            int rdm = Random.Range(0, maxValue);
            if (rdm >= 0 && rdm <= 10 * percentFill)
            {
                map[i] = 1;
            }
        }

        List<GameObject> objects = new List<GameObject>();
        while (current < map.Length - 1)
        {
            if (current < width * height - 1)
            {
                current++;
                objects.Add(SetEnvironment());
            }
        }
        current = 0;
        return objects;
    }
}
