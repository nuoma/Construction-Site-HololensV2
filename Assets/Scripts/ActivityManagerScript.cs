using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI.BoundsControl;

public class ActivityManagerScript : MonoBehaviour
{
    #region Parameters and declaration

    //main camera
    [SerializeField] private GameObject mainCamera;

    //legacy objects
    //[SerializeField] private GameObject worker;
    [SerializeField] private GameObject tripod;
    [SerializeField] private GameObject scanner;
    [SerializeField] private GameObject scannerParentNode;
    [SerializeField] private GameObject scannerCanvas;
    [SerializeField] private GameObject drone;

    [SerializeField] private GameObject RFIDWorkerReader1_tooltip;
    [SerializeField] private GameObject RFIDWorkerReader2_tooltip;
    //activities main actuator
    [SerializeField] private GameObject Activity1Bulldozer;
    [SerializeField] private GameObject Activity1Roller;
    [SerializeField] private GameObject A1RollerRFID;
    [SerializeField] private GameObject A1DozerRFID;

    [SerializeField] private GameObject Activity2Crane;
    [SerializeField] private GameObject Activity2CraneLoad;
    [SerializeField] private GameObject A2CraneRFID;

    [SerializeField] private GameObject Activity3Truck;
    [SerializeField] private GameObject A3TruckRFID;

    [SerializeField] private GameObject Activity4Worker;
    [SerializeField] private GameObject A4worker_Tooltip;

    [SerializeField] private GameObject Activity5;
    [SerializeField] private GameObject Activity5Loader;
    [SerializeField] private GameObject Activity5dumptruck;
    [SerializeField] private GameObject A5_Worker;
    [SerializeField] private GameObject A5BackhoeRFID;
    [SerializeField] private GameObject A5TruckRFID;
    [SerializeField] private GameObject A5worker_Tooltip;

    [SerializeField] private GameObject Activity6;
    [SerializeField] private GameObject Activity6_ResourcesCanvas;

    [SerializeField] private GameObject Activity7Arrow;
    [SerializeField] private GameObject A7Worker1;
    [SerializeField] private GameObject A7Worker2;
    [SerializeField] private GameObject A7w1_Tooltip;
    [SerializeField] private GameObject A7w2_Tooltip;

    [SerializeField] private GameObject Activity8;

    [SerializeField] private GameObject Activity9;

    [SerializeField] private GameObject Activity10A;
    [SerializeField] private GameObject Activity10B;

    [SerializeField] private GameObject Activity11Laser;
    [SerializeField] private GameObject Activity11Drone;
    [SerializeField] private GameObject Activity11DroneCanvas;
    
    [SerializeField] private GameObject Activity12_DroneLocator;
    [SerializeField] private GameObject Activity12_MDroneCameraLocator;
    [SerializeField] private GameObject Activity12_DroneCanvas;
    [SerializeField] private GameObject Activity12Drone;

    [SerializeField] private GameObject Activity13_DroneCanvas;
    [SerializeField] private GameObject Activity14_DroneCanvas;

    [SerializeField] private GameObject A19DW1;
    [SerializeField] private GameObject A19DW2;

    public GameObject IMUPainterWorker;
    public GameObject IMULaborerWorker;
    public GameObject IMUCarpenterWorker;
    public GameObject IMUCartWorker;
    public GameObject IMUDrywaller1;
    public GameObject IMUDrywaller2;
    public GameObject IMUMasonry;
    [SerializeField] private GameObject Painter_Tooltip;
    [SerializeField] private GameObject Laborer_Tooltip;
    [SerializeField] private GameObject Carpenter_Tooltip;
    [SerializeField] private GameObject CartWorker_Tooltip;
    [SerializeField] private GameObject Drywaller2_Tooltip;
    [SerializeField] private GameObject Drywaller1_Tooltip;
    [SerializeField] private GameObject Masonry_Tooltip;
    [SerializeField] private GameObject A11roofer_Tooltip;

    [SerializeField] private GameObject WorkerManagerNode;
    [SerializeField] private GameObject LSResetButton;


    //menus and canvas
    [SerializeField] private GameObject mainMenu; //added for main menu
    [SerializeField] private GameObject activityMenu; //added for activity menu
    [SerializeField] private GameObject scannerMenu;
    [SerializeField] private GameObject resourceMenu;
    [SerializeField] private GameObject tripodMenu;
    [SerializeField] private GameObject imuMenu;
    [SerializeField] private GameObject rfidMenu;
    [SerializeField] private GameObject droneCanvas;
    [SerializeField] private GameObject MDroneFlightCanvas;
    [SerializeField] private GameObject MDroneParentCanvas;
    
    [SerializeField] private GameObject LaserScannerBackButton;
    [SerializeField] private GameObject ManualDroneBackButton;
    [SerializeField] private GameObject flightCanvas;
    [SerializeField] private Slider rotateSlider;
    [SerializeField] private Slider horizontalSlider;
    [SerializeField] private Slider verticalSlider;
    [SerializeField] private float[] droneMove = new float[3];
    [SerializeField] private float[] backMove = new float[3];
    Vector3 droneResetPosition;
    Vector3 MainCameraResetPosition;
    Quaternion MainCameraResetRotation;

    //others
    bool move = false;
    bool sensors = false;

    [HideInInspector] public string A1_Dozer_GPS;
    [HideInInspector] public string A1_Roller_GPS;
    [HideInInspector] public string A2_Load_GPS;
    [HideInInspector] public string A3_Truck_GPS;
    [HideInInspector] public string A5_Loader_GPS;
    [HideInInspector] public string A5_Dumptruck_GPS;
    [HideInInspector] public string A5_worker_GPS;
    [HideInInspector] public string A4_worker_GPS;
    [HideInInspector] public string A6_Wood_RFID;
    [HideInInspector] public string A6_Log_RFID;
    [HideInInspector] public string A6_Rebar_RFID;
    [HideInInspector] public string A6_SteelBeam_RFID;
    [HideInInspector] public bool A6_wood_flag;
    [HideInInspector] public bool A6_log_flag;
    [HideInInspector] public bool A6_rebar_flag;
    [HideInInspector] public bool A6_steelbeam_flag;
    [HideInInspector] public string A3RFID;
    [HideInInspector] public bool A16_Laborer;
    [HideInInspector] public bool A15_Painter;
    [HideInInspector] public bool A17_Carpenter;
    [HideInInspector] public bool A18_CartWorker;
    [HideInInspector] public bool A19_Drywaller1;
    [HideInInspector] public bool A19_Drywaller2;
    [HideInInspector] public bool A20_Masonry;
    [HideInInspector] public bool A14_c2;
    [HideInInspector] public bool A4_worker_GPSRFID;
    [HideInInspector] public bool A7_worker_GPSRFID;
    [HideInInspector] public string A16_Laborer_report;
    [HideInInspector] public string A15_painter_report;
    [HideInInspector] public string A17_Carpenter_report;
    [HideInInspector] public string A18_CartWorker_report;
    [HideInInspector] public string A19_Drywaller1_report;
    [HideInInspector] public string A19_Drywaller2_report;
    [HideInInspector] public string A20_Masonry_report;
    [HideInInspector] public bool A7_w1_flag;
    [HideInInspector] public bool A7_w2_flag;
    [HideInInspector] public bool A7_w3_flag;
    [HideInInspector] public string A7_w1_GPS;
    [HideInInspector] public string A7_w2_GPS;
    [HideInInspector] public string A7_w3_GPS;
    [HideInInspector] public string A15_painter_GPS;
    [HideInInspector] public string A16_Laborer_GPS;
    [HideInInspector] public string A17_Carpenter_GPS;
    [HideInInspector] public string A18_CartWorker_GPS;
    [HideInInspector] public string A19_Drywaller1_GPS;
    [HideInInspector] public string A19_Drywaller2_GPS;
    [HideInInspector] public string A20_Masonry_GPS;
    [HideInInspector] public bool A7_w1_GPSdisplay;
    [HideInInspector] public bool A7_w2_GPSdisplay;
    [HideInInspector] public bool A7_w3_GPSdisplay;
    [HideInInspector] public bool ConcurencySuspension = false;

    public GameObject LSConstraint;
    public GameObject AutoUI;
    public GameObject ManualUI;

    public GameObject LSAreset;
    public GameObject LSMreset;

    public GameObject LStarget1;
    public GameObject LStarget2;
    public GameObject LStarget3;
    public GameObject LStripod;
    public GameObject BubbleLeveler;
    public GameObject LaserScannerParentNode;
    #endregion

    #region main menu functionalities
    void Start()
    {
        MainCameraResetPosition = mainCamera.transform.position;
        MainCameraResetRotation = mainCamera.transform.rotation;
        droneResetPosition = drone.transform.position;

        ManualDroneBackButton.SetActive(false);
        mainMenu.SetActive(false);
        activityMenu.SetActive(false);
        Activity4Worker.transform.Find("Canvas").gameObject.SetActive(false);
        //Activity4Worker_GPSTMP.SetActive(false);

        //A7Worker1.transform.Find("Canvas").gameObject.SetActive(false);
        //A7w1_GPSTMP.SetActive(false);

        //A7Worker2.transform.Find("Canvas").gameObject.SetActive(false);
        //A7w2_GPSTMP.SetActive(false);



        //Addition vehicle RFID
        A1DozerRFID.SetActive(false);
        A2CraneRFID.SetActive(false);
        A3TruckRFID.SetActive(false);
        A5TruckRFID.SetActive(false);
        A5BackhoeRFID.SetActive(false);

        LStarget1.GetComponent<BoundsControl>().gameObject.SetActive(false);
        LStarget2.GetComponent<BoundsControl>().gameObject.SetActive(false);
        LStarget3.GetComponent<BoundsControl>().gameObject.SetActive(false);
        LStripod.GetComponent<BoundsControl>().gameObject.SetActive(false);

        drone.SetActive(false);
        LaserScannerParentNode.SetActive(false);
    }

    private void LSboxon()
    {
        LStarget1.GetComponent<BoundsControl>().gameObject.SetActive(true);
        LStarget2.GetComponent<BoundsControl>().gameObject.SetActive(true);
        LStarget3.GetComponent<BoundsControl>().gameObject.SetActive(true);
        LStripod.GetComponent<BoundsControl>().gameObject.SetActive(true);
    }

    //Update GPS and RFID reports from other components in real time.
    private void Update()
    {
        A1_Dozer_GPS = Activity1Bulldozer.GetComponent<GenericGPS>().GGPSConent;
        A1_Roller_GPS = Activity1Roller.GetComponent<GenericGPS>().GGPSConent;

        A17_Carpenter_report = WorkerManagerNode.GetComponent<WorkerManager>().c1_string;
        

        A2_Load_GPS = Activity2CraneLoad.GetComponent<GenericGPS>().GGPSConent;
        A3_Truck_GPS = Activity3Truck.GetComponent<GenericGPS>().GGPSConent;
        A4_worker_GPS = Activity4Worker.GetComponent<GenericGPS>().GGPSConent;
        A5_Loader_GPS = Activity5Loader.GetComponent<GenericGPS>().GGPSConent;
        A5_Dumptruck_GPS = Activity5dumptruck.GetComponent<GenericGPS>().GGPSConent;
        A5_worker_GPS = A5_Worker.GetComponent<GenericGPS>().GGPSConent;
        A3RFID = Activity3Truck.GetComponent<Activity3Truck>().A3RFID;
        A6_Wood_RFID = Activity6.GetComponent<A6_RFID>().wood;
        A6_Log_RFID = Activity6.GetComponent<A6_RFID>().log;
        A6_Rebar_RFID = Activity6.GetComponent<A6_RFID>().rebar;
        A6_SteelBeam_RFID = Activity6.GetComponent<A6_RFID>().SteelBeam;
        A7_w1_GPS = A7Worker1.GetComponent<GenericGPS>().GGPSConent;
        A7_w2_GPS = A7Worker2.GetComponent<GenericGPS>().GGPSConent;
        A15_painter_GPS = IMUPainterWorker.GetComponent<GenericGPS>().GGPSConent;
        A16_Laborer_GPS = IMULaborerWorker.GetComponent<GenericGPS>().GGPSConent;
        A17_Carpenter_GPS = IMUCarpenterWorker.GetComponent<GenericGPS>().GGPSConent;
        A18_CartWorker_GPS = IMUCartWorker.GetComponent<GenericGPS>().GGPSConent;
        A19_Drywaller1_GPS = IMUDrywaller1.GetComponent<GenericGPS>().GGPSConent;
        A19_Drywaller2_GPS = IMUDrywaller2.GetComponent<GenericGPS>().GGPSConent;
        A20_Masonry_GPS = IMUMasonry.GetComponent<GenericGPS>().GGPSConent;

        A16_Laborer_report = WorkerManagerNode.GetComponent<WorkerManager>().l_string;
        A15_painter_report = WorkerManagerNode.GetComponent<WorkerManager>().p_string;
        A18_CartWorker_report = WorkerManagerNode.GetComponent<WorkerManager>().cart_string;
        A19_Drywaller1_report = WorkerManagerNode.GetComponent<WorkerManager>().dw1_string;
        A19_Drywaller2_report = WorkerManagerNode.GetComponent<WorkerManager>().dw2_string;
        A20_Masonry_report = WorkerManagerNode.GetComponent<WorkerManager>().ma_string;
    }

    //Deprecated after using new scene system.
    //Functions to reset and quit scene.
    public void resetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    // Entrance interface, select sensors, which is default menu for V1.0
    public void SensorsEnabled()
    {
        activityMenu.SetActive(false); //should clear activity menu if possible double selection
        mainMenu.SetActive(true);

    }

    // Entrance interface, select activity menu
    public void ActivityEnabled()
    {
        mainMenu.SetActive(false); //should clear main menu if possible double selection
        activityMenu.SetActive(true);
    }

    //this has been re-written as:
    //Activity1Bulldozer.transform.Find("Arrow").gameObject.SetActive(false);
    private void switchTag(GameObject Tag)
    {
        if (Tag.transform.GetChild(0).gameObject.activeSelf)
            Tag.transform.GetChild(0).gameObject.SetActive(false);
        else
            Tag.transform.GetChild(0).gameObject.SetActive(true);
    }


    //stop all activities
    public void stopALL()
    {
        stop_7();//Stop activity 7
        stop_3();
        stop_4();
        stop_1();
        stop_2();
        stop_5();
        stop_10();
        stop_11_a();
        stop_12_a();
    }
    #endregion

    #region Activity button actions
    //Added 20210228 for new activity list and added assets.
    //Activity Functions
    public void select_1()
    {
        Activity1Bulldozer.transform.Find("Arrow").gameObject.SetActive(true);
        Activity1Bulldozer.GetComponent<BullldozerActivity1>().start();
    }

    public void TutorialGPS()
    {
        //Activity1Bulldozer.transform.Find("Arrow").gameObject.SetActive(true);
        Activity1Bulldozer.GetComponent<BullldozerActivity1>().start();
        Activity1Bulldozer.GetComponent<GenericGPS>().start();
    }
    public void select_1_Dozer_GPS()
    {
        Activity1Bulldozer.GetComponent<GenericGPS>().start();
    }

    public void select_1_Dozer_RFID()
    {
        A1DozerRFID.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }

    public void select_1_Roller_GPS()
    {
        Activity1Roller.GetComponent<GenericGPS>().start();
    }

    public void select_1_Roller_RFID()
    {
        A1RollerRFID.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }

    public void stop_1()
    {
        Activity1Bulldozer.transform.Find("Arrow").gameObject.SetActive(false);
        Activity1Bulldozer.GetComponent<BullldozerActivity1>().stop();
    }

    public void select_2()
    {
        Activity2Crane.GetComponent<Crane>().start();
    }

    public void select_2_crane_RFID()
    {
        A2CraneRFID.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }

    public void select_2_CraneLoad_GPS()
    {
        Activity2CraneLoad.GetComponent<GenericGPS>().start();
    }


    public void stop_2()
    {
        Activity2Crane.GetComponent<Crane>().stop();
    }

    public void select_3()
    {
        Activity3Truck.GetComponent<Activity3Truck>().start();
    }

    public void select_3_truck_RFID()
    {
        A3TruckRFID.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }

    public void select_3_truck_GPS()
    {
        Activity3Truck.GetComponent<GenericGPS>().start();
    }
    public void stop_3()
    {
        Activity3Truck.GetComponent<Activity3Truck>().stop_3();
    }

    public void select_4()
    {
        Activity2Crane.GetComponent<Crane>().start();
        Activity4Worker.GetComponent<workerMove>().start();
        Activity4Worker.transform.Find("Arrow").gameObject.SetActive(true);
    }

    public void select_4worker_gps()
    {
        Activity4Worker.GetComponent<GenericGPS>().start();
    }

    public void select_4worker_RFID()
    {
        //Activity4Worker.transform.Find("Canvas").gameObject.SetActive(true);
        A4worker_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }


    public void stop_4()
    {
        Activity4Worker.GetComponent<workerMove>().stop();
        Activity4Worker.transform.Find("Arrow").gameObject.SetActive(false);
    }

    public void select_5()
    {
        Activity5.GetComponent<Activity5>().start();
        A5_Worker.GetComponent<workerMove>().start();
    }

    public void select_5_Loader_GPS()
    {
        Activity5Loader.GetComponent<GenericGPS>().start();
    }

    public void select_5_dumptruck_GPS()
    {
        Activity5dumptruck.GetComponent<GenericGPS>().start();
    }

    public void select_5_dumptruck_RFID()
    {
        A5TruckRFID.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void select_5_backhoe_RFID()
    {
        A5BackhoeRFID.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void select_5_worker_GPS()
    {
        A5_Worker.GetComponent<GenericGPS>().start();
    }

    public void select_5_worker_RFID()
    {
        A5worker_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void stop_5()
    {
        Activity5.GetComponent<Activity5>().stop_5();
    }

    //Old canvas style method
    public void select_6()
    {
        Activity6_ResourcesCanvas.SetActive(true);
    }

    public void A6_RFID()
    {
        if (A6_wood_flag) Activity6.GetComponent<A6_RFID>().WoodFlag = true;
        if (A6_log_flag) Activity6.GetComponent<A6_RFID>().LogFlag = true;
        if (A6_rebar_flag) Activity6.GetComponent<A6_RFID>().RebarFlag = true;
        if (A6_steelbeam_flag) Activity6.GetComponent<A6_RFID>().SteelbeamFlag = true;
        Activity6.GetComponent<A6_RFID>().start();
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }

    public void select_7()
    {
        A7Worker1.GetComponent<workerMove>().start();
        A7Worker2.GetComponent<workerMove>().start();
        //A7Worker3.GetComponent<workerMove>().start();
        Activity7Arrow.transform.Find("Arrow").gameObject.SetActive(true);
    }

    public void select_7_new()
    {
        if (A7_w1_flag) A7Worker1.GetComponent<workerMove>().start();
        if (A7_w2_flag) A7Worker2.GetComponent<workerMove>().start();
        //if (A7_w3_flag) A7Worker3.GetComponent<workerMove>().start();
    }

    public void select_7_GPS()
    {
        if (A7_w1_flag) A7Worker1.GetComponent<GenericGPS>().start();
        if (A7_w2_flag) A7Worker2.GetComponent<GenericGPS>().start();
        //if (A7_w3_flag) A7Worker3.GetComponent<GenericGPS>().start();
    }

    public void select_7_RFID()
    {
        if (A7_w1_flag) A7w1_Tooltip.SetActive(true);// A7Worker1.transform.Find("Canvas").gameObject.SetActive(true);
        if (A7_w2_flag) A7w2_Tooltip.SetActive(true);//A7Worker2.transform.Find("Canvas").gameObject.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }

    public void stop_7()
    {
        A7Worker1.GetComponent<workerMove>().stop();
        A7Worker2.GetComponent<workerMove>().stop();
        //A7Worker3.GetComponent<workerMove>().stop();
        Activity7Arrow.transform.Find("Arrow").gameObject.SetActive(false);
    }

    //8.cladding
    public void select_8()
    {
        LaserScannerParentNode.SetActive(true);
        //switchTag(Activity8Arrow);
        //Activity8.transform.Find("Arrow").gameObject.SetActive(true);
        //sensorSelected();
        AutoUI.SetActive(false);
        LSboxon();
        scannerMenu.SetActive(true);
        //Vector3 ScannerPosition= Activity8.transform.position;
        //scannerParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        scannerParentNode.transform.position = Activity8.transform.position;
        //LSConstraint.GetComponent<LSConstraint>().BeginSignal();
        //AutoUI.SetActive(false);
    }

    //9.flooring
    public void select_9()
    {
        LaserScannerParentNode.SetActive(true);
        //switchTag(Activity9Arrow);
        //Activity9.transform.Find("Arrow").gameObject.SetActive(true);
        //sensorSelected();
        AutoUI.SetActive(false);
        LSboxon();
        Vector3 ScannerPosition = Activity9.transform.position;
        scannerParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        LSConstraint.GetComponent<LSConstraint>().BeginSignal();
        scannerMenu.SetActive(true);
    }

    //A8, only move
    public Vector3 Explore_A8_MoveLSOnly()
    {
        Vector3 ScannerPosition = Activity8.transform.position;
        //DummyLSParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        return ScannerPosition;
    }
    //A9 scan floor, only move
    public Vector3 Explore_A9_MoveLSOnly()
    {
        Vector3 ScannerPosition = Activity9.transform.position;
        //DummyLSParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        return ScannerPosition;
    }

    //A10. stockpile unloading
    //only move
    public Vector3 Explore_A10_MoveLSOnly()
    {
        Vector3 ScannerPosition = Activity10A.transform.position;
        //DummyLSParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        return ScannerPosition;
    }



    //A10. laser scan stockpile 1
    public void select_10A()
    {
        LaserScannerParentNode.SetActive(true);
        //switchTag(Activity10AArrow);
        //Activity10A.transform.Find("Arrow").gameObject.SetActive(true);
        LSboxon();
        AutoUI.SetActive(false);
        //sensorSelected();
        Vector3 ScannerPosition = Activity10A.transform.position;
        scannerParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        LSConstraint.GetComponent<LSConstraint>().BeginSignal();
        scannerMenu.SetActive(true);
    }

    //A10. laser scan stockpile 2
    public void select_10B()
    {
        LaserScannerParentNode.SetActive(true);
        //switchTag(Activity10BArrow);
        //Activity10B.transform.Find("Arrow").gameObject.SetActive(true);
        LSboxon();
        AutoUI.SetActive(false);
        //sensorSelected();
        Vector3 ScannerPosition = Activity10B.transform.position;
        scannerParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        LSConstraint.GetComponent<LSConstraint>().BeginSignal();
        scannerMenu.SetActive(true);
    }

    public void stop_10()
    {
        //sensorSelected();
        Activity10A.transform.Find("Arrow").gameObject.SetActive(false);
        Activity10B.transform.Find("Arrow").gameObject.SetActive(false);
    }

    //A11 renovation
    public void select_11Laser()
    {
        LaserScannerParentNode.SetActive(true);
        //switchTag(Activity11Laser); //Activate laser position arrow
        //Activity12Canvas.SetActive(false);
        //sensorSelected();
        //mainCamera.transform.position = Activity12_MDroneCameraLocator.transform.position;//4, move camera.
        LSboxon();
        AutoUI.SetActive(false);
        Vector3 ScannerPosition = Activity11Laser.transform.position;
        scannerParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        LSConstraint.GetComponent<LSConstraint>().BeginSignal();
        scannerMenu.SetActive(true);
    }

    public void select_11Drone()
    {
        //switchTag(Activity11Drone);
        //Activity12Canvas.SetActive(false);
        //sensorSelected();
        //mainCamera.transform.position = Activity12_MDroneCameraLocator.transform.position;//4, move camera.
        AutoUI.SetActive(false);
        Activity11DroneCanvas.SetActive(true);
        //Start the drone and automatically fly around building


    }

    public void stop_11_a()
    {
        Activity11Drone.GetComponent<Drone12>().stop();
        Activity11DroneCanvas.SetActive(false);
    }

    //A12 site inspection use drone
    public void select_12()
    {
        //drone around jobsite
        //switchTag(Activity13Drone);
        //sensorSelected();
        AutoUI.SetActive(false);
        Activity12_DroneCanvas.SetActive(true);
        //Activity12Canvas.SetActive(false);
        Activity11DroneCanvas.SetActive(false);
        //need to get rid of all canvas
        DroneAllActivitiesRun();
        //other  activities on site.
        /*
        select_1();
        select_2();
        select_3();
        select_4();
        select_5();
        A7_w1_flag = true;
        A7_w2_flag = true;
        A7_w3_flag = true;
        select_7_new();
        */
    }

    public void stop_12_a()
    {
        Activity12Drone.GetComponent<Drone13>().stop();
    }

    //A13.site sanitation use drone
    public void select_13()
    {
        DroneAllActivitiesRun();
        AutoUI.SetActive(false);
        ManualUI.SetActive(false);
        Activity13_DroneCanvas.SetActive(true);
       
    }

    //A14.safety inspection use drone
    public void select_14()
    {
        DroneAllActivitiesRun();
        AutoUI.SetActive(false);
        ManualUI.SetActive(false);
        Activity14_DroneCanvas.SetActive(true);

        
    }

    public void DroneAllActivitiesRun()
    {
        select_1();
        select_2();
        select_3();
        select_4();
        select_5();
        A7_w1_flag = true;
        A7_w2_flag = true;
        A7_w3_flag = true;
        select_7_new();
    }

    //A15.painting
    public void select15()
    {
        A15_Painter = true;
    }
    //A16.labor work
    public void select16()
    {
        A16_Laborer = true;
    }
    //A17.carpentry
    public void select17()
    {
        A17_Carpenter = true;
    }
    //A18.cart worker
    public void select18()
    {
        A18_CartWorker = true;
        IMUCartWorker.GetComponent<workerMove>().start();
    }

    //A19.drywall
    public void select19()
    {
        A19_Drywaller1 = true;
        A19_Drywaller2 = true;
        //A19DW1.GetComponent<workerMove>().start();
        //A19DW2.GetComponent<workerMove>().start();
    }
    //A20.masonry
    public void select20()
    {
        A20_Masonry = true;
    }



    public void SelectWorkers()
    {
        //initialize
        //sensorSelected();
        WorkerManagerNode.GetComponent<WorkerManager>().Start();
        //select worker
        if (A15_Painter) WorkerManagerNode.GetComponent<WorkerManager>().painterSelect();
        if (A16_Laborer) WorkerManagerNode.GetComponent<WorkerManager>().laborerSelect();
        if (A17_Carpenter) WorkerManagerNode.GetComponent<WorkerManager>().carpenter1Select();
        if (A18_CartWorker) WorkerManagerNode.GetComponent<WorkerManager>().cartSelect();
        if (A19_Drywaller1) WorkerManagerNode.GetComponent<WorkerManager>().dw1Select();
        if (A19_Drywaller2) WorkerManagerNode.GetComponent<WorkerManager>().dw2Select();
        if (A20_Masonry) WorkerManagerNode.GetComponent<WorkerManager>().masonrySelect();

        //execute IMU function
        WorkerManagerNode.GetComponent<WorkerManager>().done();
    }

    public void TutorialIMU()
    {
        WorkerManagerNode.GetComponent<WorkerManager>().Start();
        WorkerManagerNode.GetComponent<WorkerManager>().carpenter1Select();
        WorkerManagerNode.GetComponent<WorkerManager>().done();

    }
    //A15 painter
    public void select_Painter_GPS()
    {
        IMUPainterWorker.GetComponent<GenericGPS>().start();
    }

    //A16 laborer
    public void select_Laborer_GPS()
    {
        IMULaborerWorker.GetComponent<GenericGPS>().start();
    }

    //A17 carpentry
    public void select_Carpenter_GPS()
    {
        IMUCarpenterWorker.GetComponent<GenericGPS>().start();
    }

    //Just show the tooltips
    public void select_Painter_RFID()
    {
        Painter_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void select_Laborer_RFID()
    {
        Laborer_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void select_Carpenter_RFID()
    {
        Carpenter_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void select_Cartworker_GPS()
    {
        IMUCartWorker.GetComponent<GenericGPS>().start();
    }
    public void select_Cartworker_RFID()
    {
        CartWorker_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void select_dw1_GPS()
    {
        IMUDrywaller1.GetComponent<GenericGPS>().start();
    }
    public void select_dw1_RFID()
    {
        Drywaller1_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void select_dw2_GPS()
    {
        IMUDrywaller2.GetComponent<GenericGPS>().start();
    }
    public void select_dw2_RFID()
    {
        Drywaller2_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }
    public void select_masonry_GPS()
    {
        IMUMasonry.GetComponent<GenericGPS>().start();
    }
    public void select_masonry_RFID()
    {
        Masonry_Tooltip.SetActive(true);
        RFIDWorkerReader1_tooltip.SetActive(true);
        RFIDWorkerReader2_tooltip.SetActive(true);
    }



    public void A13_stop()
    {
        WorkerManagerNode.GetComponent<WorkerManager>().backSelected();
    }
    #endregion

    #region Sensor Related Functions


    //Legacy method to use manually controlled drone
    public void droneSelected()
    {
        //sensorSelected();
        ManualDroneBackButton.SetActive(true);
        drone.SetActive(true);
        droneCanvas.SetActive(true);
        //Vector3 newPosition = droneCanvas.transform.position;
        //mainCamera.transform.position = newPosition + new Vector3(droneMove[0], droneMove[1], droneMove[2]);
        GetComponent<Canvas>().enabled = false;
    }

    //For activity 12: Job site inspection. 
    //Mods include: Skip task, Disable camera movement.
    public void ManualDrone12JobSite()
    {
        //sensorSelected();
        Activity12_DroneCanvas.SetActive(false);
        Activity13_DroneCanvas.SetActive(false);
        Activity14_DroneCanvas.SetActive(false);

        drone.SetActive(true);
        //mainCamera.transform.position = newPosition + new Vector3(droneMove[0], droneMove[1], droneMove[2]);
        //GetComponent<Canvas>().enabled = false;
        //ManualDroneBackButton.SetActive(true);
        droneCanvas.SetActive(true);//here should be drone canvas instead of task canvas
        drone.transform.Find("Arrow").gameObject.SetActive(true);

        //MDroneParentCanvas.transform.position = Activity12_MDroneCameraLocator.transform.position;
        //drone.transform.position = Activity12_DroneLocator.transform.position;
        //drone.GetComponent<droneScript>().start();
    }

    //For activity 11: old house. 
    //Jump drone location and camera location to old house.
    public void ManualDrone11OldHouse()
    {
        //sensorSelected(); //initialization
        Activity11DroneCanvas.SetActive(false);
        drone.SetActive(true);//2, activate drone.
        drone.transform.position = Activity12_DroneLocator.transform.position; //3, move drone to locator position.
        //mainCamera.transform.position = Activity12_MDroneCameraLocator.transform.position;//4, move camera.
        //GetComponent<Canvas>().enabled = false; //1, Disable main menu canvas.
        //ManualDroneBackButton.SetActive(true);//Backbutton active
        droneCanvas.SetActive(true);//Drone canvas active

        //drone canvas move location and rotate
        //droneCanvas.transform.eulerAngles = new Vector3(droneCanvas.transform.eulerAngles.x, droneCanvas.transform.eulerAngles.y + 50, droneCanvas.transform.eulerAngles.z);
        //move flight canvas as well
        //MDroneFlightCanvas.transform.eulerAngles = new Vector3(MDroneFlightCanvas.transform.eulerAngles.x, MDroneFlightCanvas.transform.eulerAngles.y + 50, MDroneFlightCanvas.transform.eulerAngles.z); ;

        //Move Drone Parent Node
        MDroneParentCanvas.transform.position = Activity12_MDroneCameraLocator.transform.position;
        //MDroneParentCanvas.transform.eulerAngles = new Vector3(MDroneParentCanvas.transform.eulerAngles.x, MDroneParentCanvas.transform.eulerAngles.y + 50, MDroneParentCanvas.transform.eulerAngles.z);

        drone.transform.Find("Arrow").gameObject.SetActive(true);
        //drone.GetComponent<droneScript>().start();
    }

    public void TutorialDrone()
    {
        Debug.Log("Tutorial drone executed");
        drone.SetActive(true);//activate drone.
        //drone.transform.position = Activity12_DroneLocator.transform.position; //3, move drone to locator position.
        ManualDroneBackButton.SetActive(true);//Backbutton active
        droneCanvas.SetActive(true);//Drone canvas active
        //Move Drone Parent Node
        //MDroneParentCanvas.transform.position = Activity12_MDroneCameraLocator.transform.position;
        //MDroneParentCanvas.transform.eulerAngles = new Vector3(MDroneParentCanvas.transform.eulerAngles.x, MDroneParentCanvas.transform.eulerAngles.y + 50, MDroneParentCanvas.transform.eulerAngles.z);
        //drone.transform.Find("Arrow").gameObject.SetActive(true);
    }

    public void TutorialLS()
    {
        //LSboxon();
        //LSConstraint.GetComponent<LSConstraint>().BeginSignal();
        scannerMenu.SetActive(true);


        //Ref
        LaserScannerParentNode.SetActive(true);
        //switchTag(Activity11Laser); //Activate laser position arrow
        //Activity12Canvas.SetActive(false);
        //sensorSelected();
        //mainCamera.transform.position = Activity12_MDroneCameraLocator.transform.position;//4, move camera.
        LSboxon();
        //AutoUI.SetActive(false);
        //Vector3 ScannerPosition = Activity11Laser.transform.position;
        //scannerParentNode.transform.position = ScannerPosition;//new Vector3(droneMove[0], droneMove[1], droneMove[2]); ;// change scanner parent node position to building 2.
        LSConstraint.GetComponent<LSConstraint>().BeginSignal();
        scannerMenu.SetActive(true);
    }
    public void GenericBackButton() //currently for all Back menus.
    {
        //resetMainCam();
        LaserScannerBackButton.SetActive(false);
        ManualDroneBackButton.SetActive(false);
        GetComponent<Canvas>().enabled = true;
        gameObject.SetActive(true);
        resetScanner();
        resetDrone();
        Activity11DroneCanvas.SetActive(false);
        Activity12_DroneCanvas.SetActive(false);
        SensorReset();
        AutoUI.SetActive(true);
        ManualUI.SetActive(false);
        LSResetButton.SetActive(false);
        ConcurencySuspension = true;
        LaserScannerBackButton.SetActive(false);
    }

    public void ManualDroneReset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }
    public void AutoDroneReset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void ManualDroneManualModeBackButton()
    {
        //resetMainCam();
        LaserScannerBackButton.SetActive(false);
        ManualDroneBackButton.SetActive(false);
        GetComponent<Canvas>().enabled = true;
        gameObject.SetActive(true);
        resetScanner();
        resetDrone();
        Activity11DroneCanvas.SetActive(false);
        Activity12_DroneCanvas.SetActive(false);
        SensorReset();
        AutoUI.SetActive(false);
        ManualUI.SetActive(true);
        LSResetButton.SetActive(false);
        ConcurencySuspension = true;
        LaserScannerBackButton.SetActive(false);
    }

    public void LSBackButton()
    {
        //resetMainCam();
        LaserScannerBackButton.SetActive(false);
        GetComponent<Canvas>().enabled = true;
        gameObject.SetActive(true);
    }

    public void BubbleLevelerBackButton()
    {
        //resetMainCam();
        BubbleLeveler.SetActive(false);
        GetComponent<Canvas>().enabled = true;//re-show hidden scanner initial canvas.
        gameObject.SetActive(true);
        //re-enable scaner menu
        scannerMenu.SetActive(true);
    }

    public void DroneBackButton()
    {
        //resetMainCam();
        ManualDroneBackButton.SetActive(false);
        GetComponent<Canvas>().enabled = true;
        gameObject.SetActive(true);
    }

    private void resetScanner()
    {
        scannerCanvas.SetActive(false);
        scannerCanvas.GetComponent<scanScript>().resolution = 0;
        scannerCanvas.GetComponent<scanScript>().quality = 0;
        scannerCanvas.GetComponent<scanScript>().color = 0;
        scannerCanvas.GetComponent<scanScript>().profile = 0;
        scannerCanvas.GetComponent<scanScript>().coverage = false;
        scanner.GetComponent<Animator>().SetBool("spin", false);
        scannerMenu.SetActive(false);
        LSResetButton.SetActive(false);
    }
    private void resetDrone()
    {
        drone.SetActive(false);
        drone.transform.Find("Arrow").gameObject.SetActive(false);//For activity 12 back button deactive drone arrow.
        //droneCanvas.SetActive(true);
        drone.GetComponent<droneScript>().taskSelected = false;
        drone.GetComponent<droneScript>().power = false;
        drone.GetComponent<droneScript>().motor = false;
        drone.transform.position = droneResetPosition;
        drone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        flightCanvas.SetActive(false);
        droneCanvas.SetActive(false);
        rotateSlider.value = 0;
        horizontalSlider.value = 0;
        verticalSlider.value = 0;
    }
    public void resetMainCam() //currently for all Back menus.
    {
        //reset camera location to 0,0,0
        mainCamera.transform.position = MainCameraResetPosition;
        mainCamera.transform.rotation = MainCameraResetRotation;
    }
    /*
    //Function to select worker
    public void workerFunction()
    {
        if (move)
        {
            worker.GetComponent<workerMove>().start();
        }

        if (sensors)
        {
            switchTag(worker);
            //worker.GetComponent<workerMove>().switchTag();
        }
    }
    */

    /*
    public void sensorSelected()
    {
        resetDrone();
        resetScanner();
        resourceMenu.GetComponent<gpsScript>().back();
        rfidMenu.GetComponent<rfidScript>().back();
        imuMenu.GetComponent<imuScript>().backSelected();
        scanner.GetComponent<Animator>().SetBool("spin", false);
        scannerMenu.SetActive(false);
        //resourceMenu.SetActive(false);//GPS report, can disable.
        //imuMenu.SetActive(false);//can discart
        //rfidMenu.SetActive(false);//can discart
    }
    */

    public void SensorReset()
    {
        resetDrone();
        resetScanner();
        //resetMainCam();
        AutoUI.SetActive(true);
        ConcurencySuspension = true;
    }

    public void ResetForLS()
    {
        resetScanner();
        //resetMainCam();
        AutoUI.SetActive(true);
        ConcurencySuspension = true;
    }

    public void ResetForLSManualMode()
    {
        resetScanner();
        //resetMainCam();
        AutoUI.SetActive(false);
        ManualUI.SetActive(true);
        ConcurencySuspension = true;
    }

    public void Auto_LS_Reset()
    {
        resetScanner();
        //resetMainCam();
        AutoUI.SetActive(true);
        ManualUI.SetActive(false);
        LSAreset.SetActive(false);
    }

    public void Manual_LS_Reset()
    {
        resetScanner();
        //resetMainCam();
        AutoUI.SetActive(false);
        ManualUI.SetActive(true);
        LSMreset.SetActive(false);
    }
    #endregion


}
