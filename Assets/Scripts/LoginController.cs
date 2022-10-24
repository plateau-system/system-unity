using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoginController : MonoBehaviour
{
    public InputField emailAddress;
    public InputField password;
    public static string userId;

    void Start ()
    {
        emailAddress = emailAddress.GetComponent<InputField> ();
        password = password.GetComponent<InputField> ();
        Debug.Log(emailAddress);
        Debug.Log(password);
    }

    public void GetInput()
    {
        string emailAddressPost = emailAddress.text;
        string passwordPost = password.text;   

        var wb = new WebClient();
        var data = new NameValueCollection();
        string url = "http://localhost:8000/api/unity_login";
        data["email"] = emailAddressPost;
        data["password"] = passwordPost;
        var response = wb.UploadValues(url, "POST", data);
        string responseStr = Encoding.GetEncoding("Shift_JIS").GetString(response);
        JObject json = JObject.Parse(responseStr);

        if ((string)json["judge"] == "True"){
           Debug.Log("ログイン成功"); 
           Debug.Log(json["user"]["id"]);
           userId = (string)json["user"]["id"];

           LoadScene();
        } else {
            Debug.Log("ログイン失敗"); 
        }
     }

    void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
