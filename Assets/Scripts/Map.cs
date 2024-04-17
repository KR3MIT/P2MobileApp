//MIT License
//Copyright (c) 2023 DA LAB (https://www.youtube.com/@DA-LAB)
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Globalization;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    [SerializeField] private string accessToken = "pk.eyJ1IjoibWFkc2JlbiIsImEiOiJjbHRrMXA1a3UwdWdsMnBxdmx3anRjOHB5In0.WcbYEU3wnJ8jnCQHvyy21A";
    [SerializeField] private enum style { Light, Dark, Streets, Outdoors, Satellite, SatelliteStreets };
    [SerializeField] private style mapStyle = style.Streets;
    [SerializeField] private enum resolution { low = 1, high = 2 };
    [SerializeField] private resolution mapResolution = resolution.low;
    [HideInInspector] public double[] BoundingBox = new double[4]; //[lon(min), lat(min), lon(max), lat(max)]

    private string[] styleStr = new string[] { "light-v10", "dark-v10", "streets-v11", "outdoors-v11", "satellite-v9", "satellite-streets-v11" };
    private string url = "";
    private Material mapMaterial;
    private int mapWidthPx = 1280;

    private int mapHeightPx = 1280;
    private double planeWidth;
    private double planeHeight;

    public static Map instance;

    void Start()
    {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

       // MatchPlaneToScreenSize();
        if (gameObject.GetComponent<MeshRenderer>() == null)
        {
            gameObject.AddComponent<MeshRenderer>();
        }
        mapMaterial = new Material(Shader.Find("Unlit/Texture"));
        gameObject.GetComponent<MeshRenderer>().material = mapMaterial;
        StartCoroutine(GetMapbox());
    }

    public void UpdateBoundingBox(double userLongitude, double userLatitude)
    {
        // Define a desired radius around the user location (adjust as needed)
        double radiusInMeters = 1000; // Adjust for desired zoom level

        // Calculate bounding box based on user location and radius
        double deltaLatitude = radiusInMeters / (111132 * Mathf.Cos((float)(Mathf.Deg2Rad * userLatitude)));
        double deltaLongitude = radiusInMeters / (111132 * Mathf.Cos((float)(Mathf.Deg2Rad * userLatitude)));

        BoundingBox[0] = userLongitude - deltaLongitude;
        BoundingBox[1] = userLatitude - deltaLatitude;
        BoundingBox[2] = userLongitude + deltaLongitude;
        BoundingBox[3] = userLatitude + deltaLatitude;

        // Regenerate map with updated bounding box
        StartCoroutine(GetMapbox());
      //  MatchPlaneToScreenSize();
    }

    IEnumerator GetMapbox()
    {
        CultureInfo invariantCulture = CultureInfo.InvariantCulture;

        url = "https://api.mapbox.com/styles/v1/mapbox/" + styleStr[(int)mapStyle] + "/static/["
            + BoundingBox[0].ToString(invariantCulture) + "," + BoundingBox[1].ToString(invariantCulture) + ","
            + BoundingBox[2].ToString(invariantCulture) + "," + BoundingBox[3].ToString(invariantCulture) + "]/"
            + mapWidthPx + "x" + mapHeightPx + "?" + "access_token=" + accessToken;

        Debug.Log(url);
        Debug.Log("that was the url");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + www.error);
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", ((DownloadHandlerTexture)www.downloadHandler).texture);
        }
    }



    //Set the scale of plane to match the screen size
    private void MatchPlaneToScreenSize()
    {
        double planeToCameraDistance = Vector3.Distance(gameObject.transform.position, Camera.main.transform.position);
        double planeHeightScale = (2.0 * Math.Tan(0.5f * Camera.main.fieldOfView * (Math.PI / 180)) * planeToCameraDistance) / 10.0; //Radians = (Math.PI / 180) * degrees. Default plane is 10 units in x and z
        double planeWidthScale = planeHeightScale * Camera.main.aspect;
        gameObject.transform.localScale = new Vector3((float)planeWidthScale, 1, (float)planeHeightScale);
        //Set map width and height in pixel based on view aspec ratio
        if (Camera.main.aspect > 1) //Width is bigger than height
        {
            mapWidthPx = 1280; //Mapbox width should be a number between 1 and 1280 pixels.
            mapHeightPx = (int)Math.Round(1280 / Camera.main.aspect); //Height is proportional to to view aspect ratio
        }
        else //Height is bigger than width
        {
            mapHeightPx = 1280; //Mapbox height should be a number between 1 and 1280 pixels.
            mapWidthPx = (int)Math.Round(1280 / Camera.main.aspect); //Width is proportional to to view aspect ratio
        }
        // the following code will force the map tp have a width between 1 and 1280 pixels
        if (mapWidthPx < 1)
        {
            mapWidthPx = 1;
        }
        else if (mapWidthPx > 1280)
        {
            mapWidthPx = 1280;
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "CombatEncounterTest")
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

    }
}