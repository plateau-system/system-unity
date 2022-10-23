using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;

public class SpotController : MonoBehaviour
{
    public GameObject SpotPrefab;

    // Start is called before the first frame update
    void Start()
    {   
        spotAllGet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spotAllGet()
    {
        string url = "http://localhost:8000/api/spot_all/1";

        WebRequest request = WebRequest.Create(url);
        Stream responseStream = request.GetResponse().GetResponseStream();
        StreamReader reader = new StreamReader(responseStream);
        var json = JArray.Parse(reader.ReadToEnd());

        var meshPointUnity = new [,]{{30144.6, 18509.98}, {-15070.48, 18509.98}, {-15070.48, 55391.62}, {30144.6, 55391.62}};
        var meshPointReal = new [,]{{139.5, 35.83}, {140, 35.83}, {140, 35.5}, {139.5,35.5}};

        for (int i=0; i < json.Count; i++) {
            // ※緯度経度の順番がDBと逆
            double longitude = double.Parse((string)json[i]["spots_longitude"]);
            double latitude = double.Parse((string)json[i]["spots_latitude"]);

            // 移動距離を増加量で決める(PLATEAUのメッシュの誤差は後で修正する必要がある)
            double longitudeRate = ((longitude - 139.5) * 100 / 0.5);
            double latitudeRate = ((latitude - 35.5) * 100 / 0.33);

            Debug.Log(longitudeRate);
            Debug.Log(latitudeRate);

            double longitudeSize = -45215.08 * longitudeRate / 100;
            double latitudeSize =  -36881.64 * latitudeRate / 100;

            double longitudeLast =  30144.6 + longitudeSize;
            double latitudeLast =  55391.62 + latitudeSize;

            float longitudeUnity = (float)longitudeLast;
            float latitudeUnity = (float)latitudeLast;

            Vector3 pos = new Vector3(longitudeUnity, 10.0f, latitudeUnity);
            var spot = Instantiate(SpotPrefab, pos, Quaternion.identity);
            spot.name  = (string)json[i]["spots_name"];
        }
    }
}