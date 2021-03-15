using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Experimental.Utilities;
using Microsoft.MixedReality.Toolkit.Experimental.Dialog;
using Microsoft.MixedReality.Toolkit.UI;

public class ManualSelection : MonoBehaviour
{

    #region Parameters
    [SerializeField]
    [Tooltip("Assign DialogSmall_192x96.prefab")]
    private GameObject dialogPrefabSmall;
    [SerializeField] private GameObject PointingChevron;

    public GameObject RunButton;
    public GameObject ManualSelectionListPanel;
    //public GameObject UIMenuManager;
    public GameObject ManualSelectionMenuSelf;
    public GameObject ActivityManagerScript;
    public GameObject TagButton;
    public GameObject LSConfirmButton;
    public GameObject ResetButton;

    public CustomDropdown Dropdown1;
    public CustomDropdown Dropdown2;

    List<string> ActivityList = new List<string> { "Activities" };
    List<string> SensorsList;

    private int SelectedActivityIndex;
    [HideInInspector] public int SelectedSensorIndex;
    [HideInInspector] public int ActualActivityNumber;

    private bool onSensorChanged;
    private bool onActivityChanged;
    [HideInInspector] public bool ResourceTaggedBool = false;
    [HideInInspector] public string CurrentSensor;

    public GameObject A1Dozer;
    public GameObject A1Stockpile;
    public GameObject A1Roller;
    public GameObject A2Crane;
    public GameObject A2SteelBeam;
    public GameObject A3Truck;
    public GameObject A3Rebar;
    public GameObject A4Worker1;
    public GameObject A5Loader;
    public GameObject A5DumpTruck;
    public GameObject A5Stockpile;
    public GameObject A6wood;
    public GameObject A6log;
    public GameObject A6rebar;
    //public GameObject A7worker1;
    public GameObject A7worker2;
    public GameObject A7worker3;
    public GameObject A13Painter;
    public GameObject A13Laborer;
    public GameObject A13Carpenter;
    //public GameObject A13Carpenter2;
    public GameObject A5Worker;
    public GameObject A6SB;
    public GameObject A18CartWorker;
    public GameObject A19DW1;
    public GameObject A19DW2;
    public GameObject A20Masonry;

    [HideInInspector]public bool RFIDReportEnable;
    [HideInInspector] public bool GPSReportEnable;
    [HideInInspector] public bool IMUReportEnable;
    private string GPSReportString;
    private string RFIDReportString;
    private string IMUReportString;
    public TextMeshProUGUI GPSReportText;
    public TextMeshProUGUI RFIDReportText;
    public TextMeshProUGUI IMUReportText;
    [SerializeField] private GameObject IMUReportCanvas;
    [SerializeField] private GameObject GPSReportCanvas;
    [SerializeField] private GameObject RFIDReportCanvas;

    private bool showhidetoggle = false; //true means hide
    public GameObject SensorParentNode;
    //public GameObject MiscAssetNode;
    public GameObject Everything;
    public GameObject mainUICollection;
    public GameObject ActivityResourcesNode;
    public GameObject ShowHideButton;

    public GameObject IMU_P;//Panels for joint selection
    public GameObject IMU_L;
    public GameObject IMU_C;
    public GameObject IMU_Cart;
    public GameObject IMU_Drywaller;
    public GameObject IMU_Masonry;

    public GameObject IMUPainter;//Workers
    public GameObject IMULabor;
    public GameObject IMUCarpenter;
    public GameObject IMUCartWorker;
    public GameObject IMUDrywaller;
    public GameObject IMUMasonry;

    public TextMeshProUGUI Dropdown1Title;
    private bool Dropdown1TitleChangeBool;
    public TextMeshProUGUI Dropdown2Title;
    private bool Dropdown2TitleChangeBool;
    private bool onActivityDropdownCreated;

    private bool initialFlag;
    #endregion

    #region Start Update
    // Start is called before the first frame update
    public void Start()
    {
        //create sensors list
        CreateActivityInitialDropdown();
        CreateSensorsDropdown();

        SetInteractablesFalse();
        SetCubeFalse();

        LSConfirmButton.SetActive(false);
        TagButton.SetActive(false);
        ManualSelectionListPanel.SetActive(false);
        RunButton.SetActive(false);
        ResetButton.SetActive(false);
        ShowHideButton.SetActive(false);

        IMU_P.SetActive(false);
        IMU_L.SetActive(false);
        IMU_C.SetActive(false);
    }

    public void SetInteractablesFalse()
    {
        //set interactable to false
        A1Dozer.GetComponent<Interactable>().IsEnabled = false;
        A1Stockpile.GetComponent<Interactable>().IsEnabled = false;
        A2Crane.GetComponent<Interactable>().IsEnabled = false;
        A2SteelBeam.GetComponent<Interactable>().IsEnabled = false;
        A3Truck.GetComponent<Interactable>().IsEnabled = false;
        A3Rebar.GetComponent<Interactable>().IsEnabled = false;
        A4Worker1.GetComponent<Interactable>().IsEnabled = false;
        A5Stockpile.GetComponent<Interactable>().IsEnabled = false;
        A5Loader.GetComponent<Interactable>().IsEnabled = false;
        A5DumpTruck.GetComponent<Interactable>().IsEnabled = false;
        A6log.GetComponent<Interactable>().IsEnabled = false;
        A6rebar.GetComponent<Interactable>().IsEnabled = false;
        A6wood.GetComponent<Interactable>().IsEnabled = false;
        A7worker2.GetComponent<Interactable>().IsEnabled = false;
        A7worker3.GetComponent<Interactable>().IsEnabled = false;
        A13Painter.GetComponent<Interactable>().IsEnabled = false;
        A13Laborer.GetComponent<Interactable>().IsEnabled = false;
        A13Carpenter.GetComponent<Interactable>().IsEnabled = false;
        A1Roller.GetComponent<Interactable>().IsEnabled = false;
        A5Worker.GetComponent<Interactable>().IsEnabled = false;
        A6SB.GetComponent<Interactable>().IsEnabled = false;
        A18CartWorker.GetComponent<Interactable>().IsEnabled = false;
        A19DW1.GetComponent<Interactable>().IsEnabled = false;
        A19DW2.GetComponent<Interactable>().IsEnabled = false;
        A20Masonry.GetComponent<Interactable>().IsEnabled = false;
    }

    public void SetCubeFalse()
    {
        //set cube to inactive.
        A1Dozer.transform.Find("Cube").gameObject.SetActive(false);
        A1Stockpile.transform.Find("Cube").gameObject.SetActive(false);
        A2SteelBeam.transform.Find("Cube").gameObject.SetActive(false);
        A2Crane.transform.Find("Cube").gameObject.SetActive(false);
        A3Rebar.transform.Find("Cube").gameObject.SetActive(false);
        A3Truck.transform.Find("Cube").gameObject.SetActive(false);
        A4Worker1.transform.Find("Cube").gameObject.SetActive(false);
        A5DumpTruck.transform.Find("Cube").gameObject.SetActive(false);
        A5Loader.transform.Find("Cube").gameObject.SetActive(false);
        A5Stockpile.transform.Find("Cube").gameObject.SetActive(false);
        A6wood.transform.Find("Cube").gameObject.SetActive(false);
        A6rebar.transform.Find("Cube").gameObject.SetActive(false);
        A6log.transform.Find("Cube").gameObject.SetActive(false);
        A7worker2.transform.Find("Cube").gameObject.SetActive(false);
        A7worker3.transform.Find("Cube").gameObject.SetActive(false);
        A13Painter.transform.Find("Cube").gameObject.SetActive(false);
        A13Laborer.transform.Find("Cube").gameObject.SetActive(false);
        A13Carpenter.transform.Find("Cube").gameObject.SetActive(false);
        A1Roller.transform.Find("Cube").gameObject.SetActive(false);
        A5Worker.transform.Find("Cube").gameObject.SetActive(false);
        A6SB.transform.Find("Cube").gameObject.SetActive(false);
        A18CartWorker.transform.Find("Cube").gameObject.SetActive(false);
        A19DW1.transform.Find("Cube").gameObject.SetActive(false);
        A19DW2.transform.Find("Cube").gameObject.SetActive(false);
        A20Masonry.transform.Find("Cube").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSensorString();

        if (!initialFlag)
        {
            //initialize();
            initialFlag = true;
        }
        
        LUT();
        if (onSensorChanged)
        {
            UpdateActivityList();
            ClearActivitiesDropdown();
            CreateActivitiesDropdown();
            //UpdateSensorString();
            onSensorChanged = false;
        }

        //Title of dropdown 1 should be sensor until a sensor change is happened, and another boolean indicating this only happen once
        //onSensorChanged toggles on and off due to need to refresh corresponding activity list
        if (!onSensorChanged && !Dropdown1TitleChangeBool)
        {
            Dropdown1Title.GetComponent<TextMeshProUGUI>().text = "Sensors";
            Dropdown1TitleChangeBool = true;
        }

        //Title of dropdown 2 should be sensor until an item in activity list is selected
        //Mechanism is different from dropdown 1
        //
        //if (!onActivityChanged && !Dropdown2TitleChangeBool)
        if(!onActivityChanged)
        {
            Dropdown2Title.GetComponent<TextMeshProUGUI>().text = "Activities";
            Dropdown2TitleChangeBool = true;
        }

        //after selection is made, activate run button.
        if (ResourceTaggedBool == true)
        { RunButton.SetActive(true); ResourceTaggedBool = false; }
        //if (ResourceTaggedBool == false)
        //{ RunButton.SetActive(false);}

        //display results
        //Display Report Panels
        if (GPSReportEnable)
        {
            GPSReportCanvas.SetActive(true);
            //GPS reporting function
            PrepareGPSString();
            GPSReportText.GetComponent<TextMeshProUGUI>().text = GPSReportString;
        }
        else
            GPSReportCanvas.SetActive(false);

        if (RFIDReportEnable)
        {
            RFIDReportCanvas.SetActive(true);
            //RFID reporting function
            PrepareRFIDString();
            RFIDReportText.GetComponent<TextMeshProUGUI>().text = RFIDReportString;
        }
        else
            RFIDReportCanvas.SetActive(false);


        if (IMUReportEnable)
        {
            //Debug.Log("IMU report inside Update().");
            //IMU reporting function
            IMUReportCanvas.SetActive(true);
            PrepareIMUString();
            IMUReportText.GetComponent<TextMeshProUGUI>().text = IMUReportString;
        }
        else
            IMUReportCanvas.SetActive(false);
    }
    #endregion


    #region supporting functions
    private void PrepareGPSString()
    {
        // Debug.Log("TEST PASS PARAM"+ ActivityManager.GetComponent<ActivityManagerScript>().A1_Dozer_GPS);
        GPSReportString = "";
        GPSReportString = ActivityManagerScript.GetComponent<ActivityManagerScript>().A1_Dozer_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A1_Roller_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A2_Load_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A3_Truck_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A5_Loader_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A5_Dumptruck_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A5_worker_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A4_worker_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A7_w1_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A7_w2_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A7_w3_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A15_painter_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A16_Laborer_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A17_Carpenter_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A18_CartWorker_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A19_Drywaller1_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A19_Drywaller2_GPS
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A20_Masonry_GPS;
    }

    //this is mainly for A6, original RFID
    private void PrepareRFIDString()
    {
        RFIDReportString = "";
        RFIDReportString = ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_Wood_RFID
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_Log_RFID
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_Rebar_RFID
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_SteelBeam_RFID
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A3RFID;
    }

    //A13 IMU
    private void PrepareIMUString()
    {
        IMUReportString = "";
        IMUReportString = ActivityManagerScript.GetComponent<ActivityManagerScript>().A15_painter_report
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A16_Laborer_report
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A17_Carpenter_report
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A18_CartWorker_report
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A19_Drywaller1_report
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A19_Drywaller2_report
            + ActivityManagerScript.GetComponent<ActivityManagerScript>().A20_Masonry_report;
    }
    private void CreateSensorsDropdown()//Dropdown 1
    {
        SensorsList = new List<string> {"GPS", "RFID", "Laser Scanner", "Drone", "IMU" };//"Sensors",
        foreach (string option in SensorsList)
        {
            Dropdown1.CreateNewItem(option, null);
        }
        Dropdown1.dropdownEvent.AddListener(delegate { DropdownValueChanged(Dropdown1); });
        Dropdown1.SetupDropdown();
    }

    private void CreateActivityInitialDropdown()
    {
        foreach (string option in ActivityList)
        {
            Dropdown2.CreateNewItem(option, null);
        }
        //Dropdown2.dropdownEvent.AddListener(delegate { DropdownValueChanged(Dropdown2); });
        Dropdown2.SetupDropdown();
    }

    void DropdownValueChanged(CustomDropdown dropdown)
    {
        //Debug.Log("Dropdown Value Changed : " + dropdown.selectedItemIndex);
        //for (int i = 0; i < 5; ++i){ SelectedSensors[i] = false; }
        //SelectedSensors[dropdown.selectedItemIndex] = true;

        //if (dropdown.selectedItemIndex == 0) SelectedSensorIndex = 0;
        //else SelectedSensorIndex = dropdown.selectedItemIndex - 1;
        SelectedSensorIndex = dropdown.selectedItemIndex;
        onSensorChanged = true;
    }

    void DropdownValueChangedActivity(CustomDropdown dropdown)
    {

        //if (dropdown.selectedItemIndex == 0) SelectedSensorIndex = 0;
        //else SelectedActivityIndex = dropdown.selectedItemIndex - 1;
        //SelectedSensorIndex = dropdown.selectedItemIndex;
        SelectedActivityIndex = dropdown.selectedItemIndex;
        //Debug.Log("Dropdown 2 value change:"+dropdown.selectedItemIndex);
        //If LS or Drone, directly show run button.
        if (ActualActivityNumber == 8 || ActualActivityNumber == 9 || ActualActivityNumber == 10 || ActualActivityNumber == 11 || ActualActivityNumber == 12 || ActualActivityNumber == 13 || ActualActivityNumber == 14)//LS 8 9 10 11 Drone 11 12
        {
            Debug.Log("LS Drone selected.");
            LSConfirmButton.SetActive(true);
            TagButton.SetActive(false);
        }
        else
        {
            TagButton.SetActive(true);
            LSConfirmButton.SetActive(false);
        }

        onActivityChanged = true;

    }

    public void SkipConfirmButton()
    {
        ActivityIndicator();
        //Turn off all interactable
        SetInteractablesFalse();
        //Turn off all box
        SetCubeFalse();
        //execute command
        ExecuteActivity();
        //hide canvas
        ManualSelectionMenuSelf.SetActive(false);
    }

    private void LUT() //check this part with shared excel list.
    {
        if (SelectedSensorIndex == 0)//GPS
        {
            if (SelectedActivityIndex == 0) ActualActivityNumber = 1;
            if (SelectedActivityIndex == 1) ActualActivityNumber = 2;
            if (SelectedActivityIndex == 2) ActualActivityNumber = 3;
            if (SelectedActivityIndex == 3) ActualActivityNumber = 4;
            if (SelectedActivityIndex == 4) ActualActivityNumber = 5;
            if (SelectedActivityIndex == 5) ActualActivityNumber = 7;
            if (SelectedActivityIndex == 6) ActualActivityNumber = 15;
            if (SelectedActivityIndex == 7) ActualActivityNumber = 16;
            if (SelectedActivityIndex == 8) ActualActivityNumber = 17;
            if (SelectedActivityIndex == 9) ActualActivityNumber = 18;
            if (SelectedActivityIndex == 10) ActualActivityNumber = 19;
            if (SelectedActivityIndex == 11) ActualActivityNumber = 20;
        }
        if (SelectedSensorIndex == 1)//RFID
        {
            if (SelectedActivityIndex == 0) ActualActivityNumber = 1;
            if (SelectedActivityIndex == 1) ActualActivityNumber = 2;
            if (SelectedActivityIndex == 2) ActualActivityNumber = 3;
            if (SelectedActivityIndex == 3) ActualActivityNumber = 4;
            if (SelectedActivityIndex == 4) ActualActivityNumber = 5;
            if (SelectedActivityIndex == 5) ActualActivityNumber = 6;
            if (SelectedActivityIndex == 6) ActualActivityNumber = 7;
            if (SelectedActivityIndex == 7) ActualActivityNumber = 15;
            if (SelectedActivityIndex == 8) ActualActivityNumber = 16;
            if (SelectedActivityIndex == 9) ActualActivityNumber = 17;
            if (SelectedActivityIndex == 10) ActualActivityNumber = 18;
            if (SelectedActivityIndex == 11) ActualActivityNumber = 19;
            if (SelectedActivityIndex == 12) ActualActivityNumber = 20;

        }
        if (SelectedSensorIndex == 2)//LS
        {
            if (SelectedActivityIndex == 0) ActualActivityNumber = 8;
            if (SelectedActivityIndex == 1) ActualActivityNumber = 9;
            if (SelectedActivityIndex == 2) ActualActivityNumber = 10;
            if (SelectedActivityIndex == 3) ActualActivityNumber = 11;
        }
        if (SelectedSensorIndex == 3)//Drone
        {
            if (SelectedActivityIndex == 0) ActualActivityNumber = 11;
            if (SelectedActivityIndex == 1) ActualActivityNumber = 12;
            if (SelectedActivityIndex == 2) ActualActivityNumber = 13;
            if (SelectedActivityIndex == 3) ActualActivityNumber = 14;
        }
        if (SelectedSensorIndex == 4)//IMU
        {
            if (SelectedActivityIndex == 0) ActualActivityNumber = 15; //P
            if (SelectedActivityIndex == 1) ActualActivityNumber = 16;//L
            if (SelectedActivityIndex == 2) ActualActivityNumber = 17;//C
            if (SelectedActivityIndex == 3) ActualActivityNumber = 18;//Cart
            if (SelectedActivityIndex == 4) ActualActivityNumber = 19;//DW
            if (SelectedActivityIndex == 5) ActualActivityNumber = 20;//mas
        }
    }


    private void ClearActivitiesDropdown()
    {
        //To clear existing items.
        Dropdown2.dropdownItems.Clear(); 
        Dropdown2.SetupDropdown();
    }

    private void CreateActivitiesDropdown()
    {
        //Create using updated ResourcesList
        foreach (string option in ActivityList)
        {
            Dropdown2.CreateNewItem(option,null);
        }
        Dropdown2.dropdownEvent.AddListener(delegate { DropdownValueChangedActivity(Dropdown2); });
        Dropdown2.SetupDropdown();
        //onActivityDropdownCreated = true;
    }

    public void TagButtonAction()
    {
        TagButton.SetActive(false);
        //SelectedActivityIndex to ActualActivityNumber.
        LUT();

        //disable dropdown menus
        Dropdown1.GetComponent<Button>().interactable = false;
        Dropdown2.GetComponent<Button>().interactable = false;

        //pointing arrow
        ActivityIndicator();

        //activate canvas that shows a list of resources.
        UpdateResourceText();
        ManualSelectionListPanel.SetActive(true);

        Debug.Log("Actual Activity Number: "+ActualActivityNumber);

        //Show hide button will mess with LS activity.
        if (ActualActivityNumber != 8 && ActualActivityNumber != 9 && ActualActivityNumber != 10 && ActualActivityNumber != 11)
        {
            ShowHideButton.SetActive(true);
        }

        

        //Turn on interactable and box according to activity.
        if (ActualActivityNumber == 1)
        {
            A1Dozer.GetComponent<Interactable>().IsEnabled = true;
            A1Stockpile.GetComponent<Interactable>().IsEnabled = true;
            A1Roller.GetComponent<Interactable>().IsEnabled = true;
            A1Roller.transform.Find("Cube").gameObject.SetActive(true);
            A1Dozer.transform.Find("Cube").gameObject.SetActive(true);
            A1Stockpile.transform.Find("Cube").gameObject.SetActive(true);
        }
        if (ActualActivityNumber == 2)
        {
            A2Crane.transform.Find("Cube").gameObject.SetActive(true);
            A2Crane.GetComponent<Interactable>().IsEnabled = true;

            A2SteelBeam.transform.Find("Cube").gameObject.SetActive(true);
            A2SteelBeam.GetComponent<Interactable>().IsEnabled = true;
        }
        if (ActualActivityNumber == 3)
        { 
            A3Rebar.transform.Find("Cube").gameObject.SetActive(true);
            A3Rebar.GetComponent<Interactable>().IsEnabled = true;
            A3Truck.transform.Find("Cube").gameObject.SetActive(true);
            A3Truck.GetComponent<Interactable>().IsEnabled = true;
        }
        if (ActualActivityNumber == 4)
        {
            A4Worker1.transform.Find("Cube").gameObject.SetActive(true);
            A4Worker1.GetComponent<Interactable>().IsEnabled = true;
        }
        if (ActualActivityNumber == 5)
        { 
            A5DumpTruck.GetComponent<Interactable>().IsEnabled = true;
            A5DumpTruck.transform.Find("Cube").gameObject.SetActive(true);
            A5Loader.GetComponent<Interactable>().IsEnabled = true;
            A5Loader.transform.Find("Cube").gameObject.SetActive(true);
            A5Stockpile.GetComponent<Interactable>().IsEnabled = true;
            A5Stockpile.transform.Find("Cube").gameObject.SetActive(true);
            A5Worker.GetComponent<Interactable>().IsEnabled = true;
            A5Worker.transform.Find("Cube").gameObject.SetActive(true);
        }
        if (ActualActivityNumber == 6)
        {
            A6log.GetComponent<Interactable>().IsEnabled = true;
            A6log.transform.Find("Cube").gameObject.SetActive(true);
            A6rebar.GetComponent<Interactable>().IsEnabled = true;
            A6rebar.transform.Find("Cube").gameObject.SetActive(true);
            A6wood.GetComponent<Interactable>().IsEnabled = true;
            A6wood.transform.Find("Cube").gameObject.SetActive(true);
            A6SB.GetComponent<Interactable>().IsEnabled = true;
            A6SB.transform.Find("Cube").gameObject.SetActive(true);
        }
        if (ActualActivityNumber == 7)
        { 
            //A7worker1.GetComponent<Interactable>().IsEnabled = true;
            //A7worker1.transform.Find("Cube").gameObject.SetActive(true);
            A7worker2.GetComponent<Interactable>().IsEnabled = true;
            A7worker2.transform.Find("Cube").gameObject.SetActive(true);
            A7worker3.GetComponent<Interactable>().IsEnabled = true;
            A7worker3.transform.Find("Cube").gameObject.SetActive(true);
        }

        //Skip for LS and Drone
        if (ActualActivityNumber == 8) RunButton.SetActive(true);
        if (ActualActivityNumber == 9) RunButton.SetActive(true);
        if (ActualActivityNumber == 10) RunButton.SetActive(true);
        if (ActualActivityNumber == 11) RunButton.SetActive(true);
        if (ActualActivityNumber == 12) RunButton.SetActive(true);
        if (ActualActivityNumber == 13) RunButton.SetActive(true);
        if (ActualActivityNumber == 14) RunButton.SetActive(true);


        //20201208 IMU workers addition
        //GPS RFID for IMU workers
        //A15 painter
        if ( ActualActivityNumber == 15)
        {
            A13Painter.GetComponent<Interactable>().IsEnabled = true;
            A13Painter.transform.Find("Cube").gameObject.SetActive(true);
        }
        //A16 laborer
        if ( ActualActivityNumber == 16)
        {
            A13Laborer.GetComponent<Interactable>().IsEnabled = true;
            A13Laborer.transform.Find("Cube").gameObject.SetActive(true);
        }
        //A17 Carpenter
        if (ActualActivityNumber == 17)
        {
            A13Carpenter.GetComponent<Interactable>().IsEnabled = true;
            A13Carpenter.transform.Find("Cube").gameObject.SetActive(true);
        }
        //A18 cart
        if ( ActualActivityNumber == 18)
        {
            A18CartWorker.GetComponent<Interactable>().IsEnabled = true;
            A18CartWorker.transform.Find("Cube").gameObject.SetActive(true);
        }
        //A19 Drywaller
        if (ActualActivityNumber == 19)
        {
            A19DW1.GetComponent<Interactable>().IsEnabled = true;
            A19DW1.transform.Find("Cube").gameObject.SetActive(true);
            A19DW2.GetComponent<Interactable>().IsEnabled = true;
            A19DW2.transform.Find("Cube").gameObject.SetActive(true);
        }

        //A20 Masonry
        if (ActualActivityNumber == 20)
        {
            A20Masonry.GetComponent<Interactable>().IsEnabled = true;
            A20Masonry.transform.Find("Cube").gameObject.SetActive(true);
        }

        //20201207 Changed IMU

        //A15 P
        if (SelectedSensorIndex == 04 && ActualActivityNumber == 15)
        {
            ManualSelectionListPanel.SetActive(false); 
            IMU_P.SetActive(true);
        }
        //A16 L
        if (SelectedSensorIndex == 04 && ActualActivityNumber == 16)
        {
            ManualSelectionListPanel.SetActive(false);
            IMU_L.SetActive(true);
        }
        //A17 C
        if (SelectedSensorIndex == 04 && ActualActivityNumber == 17) 
        {
            ManualSelectionListPanel.SetActive(false);
            IMU_C.SetActive(true);
        }
        //A18 Cart
        if (SelectedSensorIndex == 04 && ActualActivityNumber == 18)
        {
            ManualSelectionListPanel.SetActive(false);
            IMU_Cart.SetActive(true);
        }
        //A19 Drywaller
        if (SelectedSensorIndex == 04 && ActualActivityNumber == 19)
        {
            ManualSelectionListPanel.SetActive(false);
            IMU_Drywaller.SetActive(true);
        }
        //A20 Masonry
        if (SelectedSensorIndex == 04 && ActualActivityNumber == 20)
        {
            ManualSelectionListPanel.SetActive(false);
            IMU_Masonry.SetActive(true);
        }
    }

    //List resources correspond to selected activity.
    private void UpdateResourceText()
    {
        string Text = "Dummy test resource for A1. \n A1R2. \n A1R3.";

        if (ActualActivityNumber == 1) Text = "Dozer \nRoller \nStockpile \n";
        if (ActualActivityNumber == 2) Text = "Crane \nSteel Beam \n";
        if (ActualActivityNumber == 3) Text = "Truck \nRebar \n";
        if (ActualActivityNumber == 4) Text = "Worker\n";
        if (ActualActivityNumber == 5) Text = "Backhoe \nDumptruck \nStockpile \nWorker\n";
        if (ActualActivityNumber == 6) Text = "Wood \nLog \nRebar \nSteel Beam\n";
        if (ActualActivityNumber == 7) Text = "Worker 1 \nWorker 2 \n";
        if (ActualActivityNumber == 8) Text = "Scan Building. \n";
        if (ActualActivityNumber == 9) Text = "Scan Floor. \n";
        if (ActualActivityNumber == 10) Text = "Stockpile 1 \nStockpile 2 \n";
        if (ActualActivityNumber == 11) Text = "Old Building \n";
        if (ActualActivityNumber == 12) Text = "Jobsite. \n";
        if (ActualActivityNumber == 13) Text = "Jobsite. \n";
        if (ActualActivityNumber == 14) Text = "Jobsite. \n";
        if (ActualActivityNumber == 15) Text = "Painter \n";
        if (ActualActivityNumber == 16) Text = "Laborer \n";
        if (ActualActivityNumber == 17) Text = "Carpenter \n";
        if (ActualActivityNumber == 18) Text = "Cart Worker \n";
        if (ActualActivityNumber == 19) Text = "Drywaller 1 \nDrywaller 2 \n";
        if (ActualActivityNumber == 20) Text = "Masonry \n";

        ManualSelectionListPanel.transform.Find("ResourceList").GetComponent<TextMeshProUGUI>().text = Text;

    }

    public void RunButtonFunction()
    {
        Debug.Log("Confirm selection and execute.");

        //disable canvas
        //gameObject.SetActive(false);
        ManualSelectionListPanel.SetActive(false);
        //ManualSelectionMenuSelf.SetActive(false);
        //enable reset button
        //ResetButton.SetActive(true);

        //Turn off all interactable
        SetInteractablesFalse();

        //Turn off all box
        SetCubeFalse();

        //execute command
        ExecuteActivity();
        
    }

    private void ExecuteActivity()
    {
        //Show hide button will mess with LS activity.
        if (ActualActivityNumber != 8 && ActualActivityNumber != 9 && ActualActivityNumber != 10 && ActualActivityNumber != 11)
        {
            ShowHideButton.SetActive(true);
        }
        
        //A1
        if (ActualActivityNumber == 1)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_1();
            //Dozer GPS
            if (SelectedSensorIndex == 0 && A1Dozer.GetComponent<ManualClickSelect>().TagStatus == true)
            { 
                GPSReportEnable = true; 
                ActivityManagerScript.GetComponent<ActivityManagerScript>().select_1_Dozer_GPS();
            }
            //Dozer RFID
            if (SelectedSensorIndex == 1 && A1Dozer.GetComponent<ManualClickSelect>().TagStatus == true)
            {
                ActivityManagerScript.GetComponent<ActivityManagerScript>().select_1_Dozer_RFID();
            }
            //Roller GPS
            if (SelectedSensorIndex == 0 && A1Roller.GetComponent<ManualClickSelect>().TagStatus == true)
            {
                GPSReportEnable = true;
                ActivityManagerScript.GetComponent<ActivityManagerScript>().select_1_Roller_GPS();
            }
            //Roller RFID
            if (SelectedSensorIndex == 1 && A1Roller.GetComponent<ManualClickSelect>().TagStatus == true)
            {
                ActivityManagerScript.GetComponent<ActivityManagerScript>().select_1_Roller_RFID();
            }
        }

        //A2
        if (ActualActivityNumber == 2)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_2();
            // SteelBeam GPS
            if (SelectedSensorIndex == 0 && A2SteelBeam.GetComponent<ManualClickSelect>().TagStatus == true)
            {
                GPSReportEnable = true;
                ActivityManagerScript.GetComponent<ActivityManagerScript>().select_2_CraneLoad_GPS();
            }
            // Crane RFID
            if (SelectedSensorIndex == 1 && A2Crane.GetComponent<ManualClickSelect>().TagStatus == true)
            {
                ActivityManagerScript.GetComponent<ActivityManagerScript>().select_2_crane_RFID();
            }
        }


        //A3
        if (ActualActivityNumber == 3)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_3();
            //Truck GPS
            if(A3Truck.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 0)
            { GPSReportEnable = true; ActivityManagerScript.GetComponent<ActivityManagerScript>().select_3_truck_GPS(); }
            //Truck RFID
            if (A3Truck.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 1)
            { RFIDReportEnable = true; ActivityManagerScript.GetComponent<ActivityManagerScript>().select_3_truck_RFID(); }
            //Rebar RFID
            if (A3Rebar.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 1) RFIDReportEnable = true;
        } 

        //A4
        if (ActualActivityNumber == 4)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_4(); 
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_2(); //Crane move
            //GPS
            if(A4Worker1.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 0)
            { GPSReportEnable = true; ActivityManagerScript.GetComponent<ActivityManagerScript>().select_4worker_gps(); }
            //RFID
            if (A4Worker1.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 1)
            { ActivityManagerScript.GetComponent<ActivityManagerScript>().select_4worker_RFID(); }
        } 

        //A5 Load and haul
        if (ActualActivityNumber == 5)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_5();
            //dumptruck GPS
            if (A5DumpTruck.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 0) 
            { GPSReportEnable = true; ActivityManagerScript.GetComponent<ActivityManagerScript>().select_5_dumptruck_GPS(); }
            //backhoe gps
            if(A5Loader.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 0) 
            { GPSReportEnable = true; ActivityManagerScript.GetComponent<ActivityManagerScript>().select_5_Loader_GPS(); }
            //dumptruck RFID
            if (A5DumpTruck.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 1)
            { ActivityManagerScript.GetComponent<ActivityManagerScript>().select_5_dumptruck_RFID(); Debug.Log("A5DT rfid selected."); }
            //backhoe RFID
            if (A5Loader.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 1)
            { ActivityManagerScript.GetComponent<ActivityManagerScript>().select_5_backhoe_RFID(); Debug.Log("A5backhoe rfid selected."); }
            //WOrker GPS
            if (A5Worker.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 0)
            { GPSReportEnable = true; ActivityManagerScript.GetComponent<ActivityManagerScript>().select_5_worker_GPS(); }
            //WOrker RFID
            if (A5Worker.GetComponent<ManualClickSelect>().TagStatus == true && SelectedSensorIndex == 1)
            {  ActivityManagerScript.GetComponent<ActivityManagerScript>().select_5_worker_RFID(); }
        }


        //A6. RFID
        if (ActualActivityNumber == 6)
        {
            if (A6wood.GetComponent<ManualClickSelect>().TagStatus == true) ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_wood_flag = true;
            if (A6log.GetComponent<ManualClickSelect>().TagStatus == true) ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_log_flag = true;
            if (A6rebar.GetComponent<ManualClickSelect>().TagStatus == true) ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_rebar_flag = true;
            if (A6SB.GetComponent<ManualClickSelect>().TagStatus == true) ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_steelbeam_flag = true;

            ActivityManagerScript.GetComponent<ActivityManagerScript>().A6_RFID();
            RFIDReportEnable = true;
        }

            //A7.Workers on top floor + RFID / GPS
            if (ActualActivityNumber == 7)
        {
            //if (A7worker1.GetComponent<ManualClickSelect>().TagStatus == true) ActivityManagerScript.GetComponent<ActivityManagerScript>().A7_w1_flag = true;
            if (A7worker2.GetComponent<ManualClickSelect>().TagStatus == true) ActivityManagerScript.GetComponent<ActivityManagerScript>().A7_w2_flag = true;
            if (A7worker3.GetComponent<ManualClickSelect>().TagStatus == true) ActivityManagerScript.GetComponent<ActivityManagerScript>().A7_w3_flag = true;
            //Run basic activity: danger zone red box. Based on worker selection bool.
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_7_new();
            //GPS only
            if (SelectedSensorIndex == 0) { GPSReportEnable = true; ActivityManagerScript.GetComponent<ActivityManagerScript>().select_7_GPS(); }
            //RFID only
            if (SelectedSensorIndex == 1) { ActivityManagerScript.GetComponent<ActivityManagerScript>().select_7_RFID(); }
        }

        //A8 scan part of building
        if (ActualActivityNumber == 8) ActivityManagerScript.GetComponent<ActivityManagerScript>().select_8();

        //A9 scan concrete slab
        if (ActualActivityNumber == 9) ActivityManagerScript.GetComponent<ActivityManagerScript>().select_9();

        //A10 scan stockpile, random pick between 10A 10B.
        if (ActualActivityNumber == 10) ActivityManagerScript.GetComponent<ActivityManagerScript>().select_10A();

        //A11 old bldg LS
        if (ActualActivityNumber == 11 && SelectedSensorIndex == 2) ActivityManagerScript.GetComponent<ActivityManagerScript>().select_11Laser();

        //A11 old bldg drone
        if (ActualActivityNumber == 11 && SelectedSensorIndex == 3) ActivityManagerScript.GetComponent<ActivityManagerScript>().select_11Drone();

        //A12 drone site inspection
        if (ActualActivityNumber == 12 && SelectedSensorIndex == 3) ActivityManagerScript.GetComponent<ActivityManagerScript>().select_12();

        //A13 sanitization drone
        if (ActualActivityNumber == 13 && SelectedSensorIndex == 3) ActivityManagerScript.GetComponent<ActivityManagerScript>().select_13();

        //A14 safety drone
        if (ActualActivityNumber == 14 && SelectedSensorIndex == 3) ActivityManagerScript.GetComponent<ActivityManagerScript>().select_14();

        //IMU, dont need to specify sensor, because A13 is only reachable by selecting IMU in the first place.
        //IMU will activate a separate IMU panel for each worker.

        //A15. IMU Painter GPS
        if (ActualActivityNumber == 15 && SelectedSensorIndex == 0)
        {
            GPSReportEnable = true;
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_Painter_GPS();
        }
        //A15. IMU Painter RFID
        if (ActualActivityNumber == 15 && SelectedSensorIndex == 1)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_Painter_RFID();
        }

        //A16 IMU Laborer GPS
        if (ActualActivityNumber == 16 && SelectedSensorIndex == 0)
        {
            GPSReportEnable = true;
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_Laborer_GPS();
        }
        //A16 IMU laborer RFID
        if (ActualActivityNumber == 16 && SelectedSensorIndex == 1)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_Laborer_RFID();
        }
        //A17 IMU Carpenter GPS
        if (ActualActivityNumber == 17 && SelectedSensorIndex == 0)
        {
            GPSReportEnable = true;
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_Carpenter_GPS();
        }
        //A17 IMU carpenter RFID
        if (ActualActivityNumber == 17 && SelectedSensorIndex == 1)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_Carpenter_RFID();
        }

        //A18 cart GPS
        if (ActualActivityNumber == 18 && SelectedSensorIndex == 0)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select18();
            GPSReportEnable = true;
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_Cartworker_GPS();
        }

        //A18 cart RFID
        if (ActualActivityNumber == 18 && SelectedSensorIndex == 1)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select18();
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_Cartworker_RFID();
        }

        //A19 dw gps
        if (ActualActivityNumber == 19 && SelectedSensorIndex == 0)
        {
            GPSReportEnable = true;
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_dw1_GPS();
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_dw2_GPS();
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select19();
        }

        //A19 dw rfid
        if (ActualActivityNumber == 19 && SelectedSensorIndex == 1)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_dw1_RFID();
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_dw2_RFID();
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select19();
        }

        //A20 mason GPS
        if (ActualActivityNumber == 20 && SelectedSensorIndex == 0)
        {
            GPSReportEnable = true;
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_masonry_GPS();
        }

        //A20 mason rfid
        if (ActualActivityNumber == 20 && SelectedSensorIndex == 1)
        {
            ActivityManagerScript.GetComponent<ActivityManagerScript>().select_masonry_RFID();
        }
    }


    private void ActivityIndicator()
    {
        string name = "A" + ActualActivityNumber + "POS";
        //Activate Chevron and live for 10 seconds.
        StartCoroutine(ShowAndHide(PointingChevron, name, 10.0f));
    }

    // Activate chevron, give location, and keep it active for 5 seconds.
    public IEnumerator ShowAndHide(GameObject go, string name, float delay)
    {
        go.GetComponent<DirectionalIndicator>().DirectionalTarget = GameObject.Find(name).transform;
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }

    /// <summary>
    /// Update activity list based on selected sensors.
    /// </summary>
    private void UpdateActivityList()
    {
        ActivityList.Clear();
        //Activity correspond to GPS
        if (SelectedSensorIndex == 0) { ActivityList.AddRange(new string[] { "Backfilling", "Crane Loading", "Material Delivery", "Material handling (1)", "Truck Load/Haul",
             "Material handling (2)","Painting","Labor Work","Carpentry","Cart Worker","Drywalling","Masonry" }); }
        //RFID activities
        if (SelectedSensorIndex == 1) { ActivityList.AddRange(new string[] {  "Backfilling", "Crane Loading","Material Delivery", "Material handling (1)", "Truck Load/Haul",
            "Material Inventory", "Material handling (2)" ,"Painting","Labor Work","Carpentry","Cart Worker","Drywalling","Masonry" }); }
        //LS
        if (SelectedSensorIndex == 2) { ActivityList.AddRange(new string[] {  "Cladding", "Flooring", "Stockpile unloading", "Renovation" }); }
        //Drone
        if (SelectedSensorIndex == 3) { ActivityList.AddRange(new string[] {  "Renovation", "Site Inspection" , "Site Sanitation", "Safety Inspection" }); }
        //IMU activities
        if (SelectedSensorIndex == 4) { ActivityList.AddRange(new string[] {  "Painting","Labor Work","Carpentry", "Cart Worker", "Drywalling", "Masonry" }); }
    }

    private void UpdateSensorString()
    {
        if (SelectedSensorIndex == 0) CurrentSensor = "GPS";
        if (SelectedSensorIndex == 1) CurrentSensor = "RFID";
        if (SelectedSensorIndex == 2) CurrentSensor = "Laser Scanner";
        if (SelectedSensorIndex == 3) CurrentSensor = "Drone";
        if (SelectedSensorIndex == 4) CurrentSensor = "IMU";
    }

    //For manual scene no6
    public void ReloadSceneButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }


    public void ShowHideManual()
    {
        //below is from menumanager.cs
        //GameObject building = Everything.transform.Find("SceneContent").transform.Find("Construction Site").transform.Find("buildings").transform.Find("building-6").gameObject;
        //GameObject LS = SensorParentNode.transform.Find("laserScanner").gameObject;
        //GameObject MDrone = SensorParentNode.transform.Find("Drone").gameObject;

        if (showhidetoggle)
        {
            //Currently in hidden state, now show everything.
            showhidetoggle = false;
            mainUICollection.SetActive(true);
            //building.SetActive(true);//building-6 special case shared by multiple activities
            //LS.SetActive(true);
            //MDrone.SetActive(true);
            foreach (Transform child in ActivityResourcesNode.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            //bool is false, set to true and hide everything
            //Switch to hide in-active assets.
            showhidetoggle = true;
            //mainUICollection.SetActive(false);
            //MiscAssetNode.SetActive(false);
            //building.SetActive(false); //building-6 relate to these activities:
            //LS.SetActive(false);
            //MDrone.SetActive(false);


            //if(Activity not active) {turn off;}
            if (!(ActualActivityNumber == 1)) { ActivityResourcesNode.transform.Find("Activity1").gameObject.SetActive(false); }
            if (!(ActualActivityNumber == 2)) 
            {
                if(!(ActualActivityNumber == 4))
                ActivityResourcesNode.transform.Find("Activity2").gameObject.SetActive(false); 
            }
            if (! (ActualActivityNumber == 3)) { ActivityResourcesNode.transform.Find("Activity3").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 4)) { ActivityResourcesNode.transform.Find("Activity4").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 5)) { ActivityResourcesNode.transform.Find("Activity5").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 6)) { ActivityResourcesNode.transform.Find("Activity6").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 7)) { ActivityResourcesNode.transform.Find("Activity7").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 8)) { ActivityResourcesNode.transform.Find("Activity8").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 9)) { ActivityResourcesNode.transform.Find("Activity9").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 10)) { ActivityResourcesNode.transform.Find("Activity10A").gameObject.SetActive(false); ActivityResourcesNode.transform.Find("Activity10B").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 11)) { ActivityResourcesNode.transform.Find("Activity11").gameObject.SetActive(false); ActivityResourcesNode.transform.Find("Activity11_Laser").gameObject.SetActive(false); ActivityResourcesNode.transform.Find("Activity11_Drone").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 12)) { ActivityResourcesNode.transform.Find("Activity12_Drone").gameObject.SetActive(false); }
            if (! (ActualActivityNumber == 13)) { ActivityResourcesNode.transform.Find("Activity13").gameObject.SetActive(false); }
            if (!(ActualActivityNumber == 14)) { ActivityResourcesNode.transform.Find("Activity14").gameObject.SetActive(false); }
            if (!(ActualActivityNumber == 15)) { ActivityResourcesNode.transform.Find("Activity15").gameObject.SetActive(false); }
            if (!(ActualActivityNumber == 16)) { ActivityResourcesNode.transform.Find("Activity16").gameObject.SetActive(false); }
            if (!(ActualActivityNumber == 17)) { ActivityResourcesNode.transform.Find("Activity17").gameObject.SetActive(false); }
            if (!(ActualActivityNumber == 18)) { ActivityResourcesNode.transform.Find("Activity18").gameObject.SetActive(false); }
            if (!(ActualActivityNumber == 19)) { ActivityResourcesNode.transform.Find("Activity19").gameObject.SetActive(false); }
            if (!(ActualActivityNumber == 20)) { ActivityResourcesNode.transform.Find("Activity20").gameObject.SetActive(false); }

            /*
            //building-6 check
            //if (! (ActualActivityNumber == 2) && ! (ActualActivityNumber == 4) && ! (ActualActivityNumber == 9))
            //{
            //    building.SetActive(false);
            //}

            //LS check
            if (! (ActualActivityNumber == 8) && ! (ActualActivityNumber == 8) && ! (ActualActivityNumber == 10) && ! (ActualActivityNumber == 11))
            {
                LS.SetActive(false);
            }
            //Drone check
            if (! (ActualActivityNumber == 11) && ! (ActualActivityNumber == 12))
            {
                MDrone.SetActive(false);
            }
            */
        }
        //MenuManager Finish
    }

    #endregion

    #region IMU related
    //Painter
    public void PainterConfirm()
    {
        //panel inactive
        IMU_P.SetActive(false);
        //IMU report canvas panel
        IMUReportEnable = true;
        //IMU functions
        SetInteractablesFalse();
        SetCubeFalse();
        ActivityManagerScript.GetComponent<ActivityManagerScript>().A15_Painter = true;
        ActivityManagerScript.GetComponent<ActivityManagerScript>().SelectWorkers();
    }

    public void PainterNeck()
    {
        //IMUntt.SetActive(true);//show tooltip
        IMUPainter.GetComponent<workerScript>().neckSelect();//toggle on and off
    }
    public void PainterShoulder()
    {
        //IMUstt.SetActive(true);
        IMUPainter.GetComponent<workerScript>().shoulderSelect();
    }

    public void PainterThigh()
    {
        //IMUttt.SetActive(true);
        IMUPainter.GetComponent<workerScript>().thighSelect();
    }

    public void PainterBack()
    {
        //IMUbtt.SetActive(true);
        IMUPainter.GetComponent<workerScript>().backSelect();
    }
    //Labor
    public void LabororConfirm()
    {
        //panel inactive
        IMU_L.SetActive(false);
        //IMU report canvas panel
        IMUReportEnable = true;
        //IMU functions
        SetInteractablesFalse();
        SetCubeFalse();
        ActivityManagerScript.GetComponent<ActivityManagerScript>().A16_Laborer = true;
        ActivityManagerScript.GetComponent<ActivityManagerScript>().SelectWorkers();
    }

    public void LabororNeck()
    {
        //IMUntt.SetActive(true);//show tooltip
        IMULabor.GetComponent<workerScript>().neckSelect();//toggle on and off
    }
    public void LabororShoulder()
    {
        //IMUstt.SetActive(true);
        IMULabor.GetComponent<workerScript>().shoulderSelect();
    }

    public void LabororThigh()
    {
        //IMUttt.SetActive(true);
        IMULabor.GetComponent<workerScript>().thighSelect();
    }

    public void LabororBack()
    {
        //IMUbtt.SetActive(true);
        IMULabor.GetComponent<workerScript>().backSelect();
    }
    //Carpenter
    public void CarpenterConfirm()
    {
        //panel inactive
        IMU_C.SetActive(false);
        //IMU report canvas panel
        IMUReportEnable = true;
        //IMU functions
        SetInteractablesFalse();
        SetCubeFalse();
        ActivityManagerScript.GetComponent<ActivityManagerScript>().A17_Carpenter = true;
        ActivityManagerScript.GetComponent<ActivityManagerScript>().SelectWorkers();
    }

    public void CarpenterNeck()
    {
        //IMUntt.SetActive(true);//show tooltip
        IMUCarpenter.GetComponent<workerScript>().neckSelect();//toggle on and off
    }
    public void CarpenterShoulder()
    {
        //IMUstt.SetActive(true);
        IMUCarpenter.GetComponent<workerScript>().shoulderSelect();
    }

    public void CarpenterThigh()
    {
        //IMUttt.SetActive(true);
        IMUCarpenter.GetComponent<workerScript>().thighSelect();
    }

    public void CarpenterBack()
    {
        //IMUbtt.SetActive(true);
        IMUCarpenter.GetComponent<workerScript>().backSelect();
    }

    public void IMUStop()
    {
        IMUReportEnable = false;
        IMUReportCanvas.SetActive(false);
        ActivityManagerScript.GetComponent<ActivityManagerScript>().A13_stop();
        //mainUICollection.SetActive(true);
        //ManualSelectionParent.GetComponent<ManualSelection>().IMUReportEnable = false;
    }


    //Cart
    public void CartConfirm()
    {
        //panel inactive
        IMU_Cart.SetActive(false);
        //IMU report canvas panel
        IMUReportEnable = true;
        //IMU functions
        SetInteractablesFalse();
        SetCubeFalse();
        ActivityManagerScript.GetComponent<ActivityManagerScript>().A18_CartWorker = true;
        ActivityManagerScript.GetComponent<ActivityManagerScript>().SelectWorkers();
    }

    public void CartNeck()
    {
        //IMUntt.SetActive(true);//show tooltip
        IMUCartWorker.GetComponent<workerScript>().neckSelect();//toggle on and off
    }
    public void CartShoulder()
    {
        //IMUstt.SetActive(true);
        IMUCartWorker.GetComponent<workerScript>().shoulderSelect();
    }

    public void CartThigh()
    {
        //IMUttt.SetActive(true);
        IMUCartWorker.GetComponent<workerScript>().thighSelect();
    }

    public void CartBack()
    {
        //IMUbtt.SetActive(true);
        IMUCartWorker.GetComponent<workerScript>().backSelect();
    }
    //Masonry
    public void MasonConfirm()
    {
        //panel inactive
        IMU_Masonry.SetActive(false);
        //IMU report canvas panel
        IMUReportEnable = true;
        //IMU functions
        SetInteractablesFalse();
        SetCubeFalse();
        ActivityManagerScript.GetComponent<ActivityManagerScript>().A20_Masonry = true;
        ActivityManagerScript.GetComponent<ActivityManagerScript>().SelectWorkers();
    }

    public void MasNeck()
    {
        //IMUntt.SetActive(true);//show tooltip
        IMUMasonry.GetComponent<workerScript>().neckSelect();//toggle on and off
    }
    public void MasShoulder()
    {
        //IMUstt.SetActive(true);
        IMUMasonry.GetComponent<workerScript>().shoulderSelect();
    }

    public void MasThigh()
    {
        //IMUttt.SetActive(true);
        IMUMasonry.GetComponent<workerScript>().thighSelect();
    }

    public void MasBack()
    {
        //IMUbtt.SetActive(true);
        IMUMasonry.GetComponent<workerScript>().backSelect();
    }
    //Drywaller
    public void DWConfirm()
    {
        //panel inactive
        IMU_Drywaller.SetActive(false);
        //IMU report canvas panel
        IMUReportEnable = true;
        //IMU functions
        SetInteractablesFalse();
        SetCubeFalse();
        ActivityManagerScript.GetComponent<ActivityManagerScript>().A19_Drywaller1 = true;
        ActivityManagerScript.GetComponent<ActivityManagerScript>().SelectWorkers();
        ActivityManagerScript.GetComponent<ActivityManagerScript>().select19();
    }

    public void DWNeck()
    {
        //IMUntt.SetActive(true);//show tooltip
        IMUDrywaller.GetComponent<workerScript>().neckSelect();//toggle on and off
    }
    public void DWShoulder()
    {
        //IMUstt.SetActive(true);
        IMUDrywaller.GetComponent<workerScript>().shoulderSelect();
    }

    public void DWThigh()
    {
        //IMUttt.SetActive(true);
        IMUDrywaller.GetComponent<workerScript>().thighSelect();
    }

    public void DWBack()
    {
        //IMUbtt.SetActive(true);
        IMUDrywaller.GetComponent<workerScript>().backSelect();
    }
    #endregion
}
