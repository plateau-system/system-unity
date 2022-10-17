using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;

public class SpotController : MonoBehaviour
{
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

        Debug.Log(json);
    }
}
