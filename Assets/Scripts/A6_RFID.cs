using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A6_RFID : MonoBehaviour
{
    public string wood;
    public string log;
    public string rebar;
    public string SteelBeam;
    public bool WoodFlag = false;
    public bool LogFlag = false;
    public bool RebarFlag = false;
    public bool SteelbeamFlag = false;
    private bool RFIDToggle;
    private int WoodNumber;
    private int LogNumber;
    private int RebarNumber;
    private int SteelBeamNumber;


    // Start is called before the first frame update
    public void start()
    {
        RFIDToggle = true;

        if (LogFlag)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Log") as GameObject[];
            LogNumber = objectsWithTag.Length;
            log = "Log Number:" + LogNumber + "\n";
        }
        if (WoodFlag)
        {
            // All wood object should have a tag with "wood"
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("wood") as GameObject[];
            WoodNumber = objectsWithTag.Length;
            wood = "Wood Number:" + WoodNumber + "\n";
        }
        if (RebarFlag)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("rebar") as GameObject[];
            RebarNumber = objectsWithTag.Length;
            rebar = "Rebar Number:" + RebarNumber + "\n";
        }
        if (SteelbeamFlag)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("steelbeam") as GameObject[];
            SteelBeamNumber = objectsWithTag.Length;
            SteelBeam = "Steel Beam Number:" + SteelBeamNumber + "\n";
        }
    }

}
