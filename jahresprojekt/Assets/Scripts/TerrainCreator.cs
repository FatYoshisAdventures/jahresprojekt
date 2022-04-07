using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using UnityEngine.U2D;


public class TerrainCreator : NetworkBehaviour
{
    [SerializeField] SpriteShapeController shape;

    [SerializeField] int numOfPoints = 30;

    [SerializeField] float height = 5f;

    [SerializeField] float deviation = 5f;

    [SerializeField] float mapSize = 30;

    [SerializeField] float roundness = 2f;

    private List<float> yValues = new List<float>();




    [SerializeField] private NetworkManager manager;

    void Start()
    {
        //manager = this.GetComponent<NetworkManager>();
        StartHostorServer();

        ChangeMapSize();

        GenerateTerrain();
    }

    void StartHostorServer()
    {
        switch (PlayerPrefs.GetInt("host"))
        {
            case 0:
                string ip = PlayerPrefs.GetString("ip");
                if (ip == "") ip = "127.0.0.1";
                manager.gameObject.GetComponent<UNetTransport>().ConnectAddress = ip;
                
                manager.StartClient();
                StartCoroutine(wait());
                //GetShapeYValuesServerRpc();
                break;
            case 1:
                manager.StartHost();
                StartCoroutine(wait());
                break;
            default:
                break;
        }
        PlayerPrefs.DeleteKey("host");
    }
    
    /// <summary>
    /// waits for .5 seconds then executes serverRPC
    /// </summary>
    /// <returns></returns>
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        GetShapeYValuesServerRpc();
    }

    void GenerateTerrain()
    {
        if (manager.IsServer)
        {
            // calculate exact horizontal size of map
            float scale = shape.spline.GetPosition(2).x - shape.spline.GetPosition(1).x;

            // calculate what the distance between the points should be
            float distanceBtwnPoints = scale / numOfPoints;

            for (int i = 0; i < numOfPoints - 1; i++)
            {
                float xPos = shape.spline.GetPosition(i + 1).x + distanceBtwnPoints;

                Vector3 temp = new Vector3(xPos, height + deviation * Mathf.PerlinNoise(i * Random.Range(0f, 1f), 0));
                yValues.Add(temp.y);
                shape.spline.InsertPointAt(i + 2, temp);

            }

            for (int i = 2; i < numOfPoints + 1; i++)
            {
                shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                shape.spline.SetLeftTangent(i, new Vector3(-roundness, 0, 0));
                shape.spline.SetRightTangent(i, new Vector3(+roundness, 0, 0));
            }
        }
    }

    void GenerateTerrain(List<float> data)
    {
        // calculate exact horizontal size of map
        float scale = shape.spline.GetPosition(2).x - shape.spline.GetPosition(1).x;

        // calculate what the distance between the points should be
        float distanceBtwnPoints = scale / numOfPoints;

        for (int i = 0; i < numOfPoints - 1; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBtwnPoints;

            Vector3 temp = new Vector3(xPos, data[i], 0);           
            shape.spline.InsertPointAt(i + 2, temp);
        }

        for (int i = 2; i < numOfPoints + 1; i++)
        {
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-roundness, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(+roundness, 0, 0));
        }
    }

    /// <summary>
    /// sends yValues of map through a client rpc to client
    /// </summary>
    [ServerRpc(Delivery = RpcDelivery.Unreliable, RequireOwnership = false)]
    void GetShapeYValuesServerRpc()
    {
        string list = string.Join(' ', yValues.ToArray());
        SendShapeYValuesClientRpc(list);
    }
    
    /// <summary>
    /// starts terrain creation with data previously requested and sent from server, gets exectued from server
    /// </summary>
    /// <param name="data"></param>
    [ClientRpc]
    void SendShapeYValuesClientRpc(string data)
    {
        List<string> list = new List<string>();
        list.AddRange(data.Split(' '));
        yValues = list.ConvertAll(float.Parse);
        GenerateTerrain(yValues);
    }

    void ChangeMapSize()
    {
        //move points outwards to left
        shape.spline.SetPosition(0, shape.spline.GetPosition(0) - Vector3.right * mapSize);
        shape.spline.SetPosition(1, shape.spline.GetPosition(1) - Vector3.right * mapSize);

        //move points outwards to right
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * mapSize);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * mapSize);
    }
}
