using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GenerationEnvironment : MonoBehaviour
{
    public static GenerationEnvironment GE;
    public Text seed;

    public float elevation = 0.2f;
    public const int width = 10;
    public const int height = 10;
    public const int percentFill = 75;

    private static int current = 0;
    private bool ready = false;

    int[] map = null;

    private void Awake()
    {

        if (GE == null)
        {
            GE = this;
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            if (current < width * height - 1)
            {
                current++;
                SetEnvironment();
            }
            
        }
    }

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

    private void SetEnvironment()
    {
        float beginX = 4.2f;
        float beginZ = 3.5f;
        float offsetX = -0.83f;
        float offsetZ = -0.84f;


        if (map[current] == 1)
        {
            if (checkForPlayers())
            {
                Instantiate(GameObject.FindGameObjectWithTag("Destructible"), new Vector3(offsetX * (current % width) + beginX, elevation, offsetZ * (current / height) + beginZ), Quaternion.identity);
            }
        }
    }

    private void initMap()
    {

        map = new int[width * height];
        for (int i = 0; i < width * height; i++)
            map[i] = 0;

    }


    public void GenereateEnvironment()
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
        ready = true;
    }
}
