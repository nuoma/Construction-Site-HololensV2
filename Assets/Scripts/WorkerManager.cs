using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class WorkerManager : MonoBehaviour
{

	#region parameters
	[SerializeField] private GameObject laborer;
	[SerializeField] private GameObject painter;
	[SerializeField] private GameObject carpenter1;
	[SerializeField] private GameObject Drywaller1;
    [SerializeField] private GameObject CartWorker;
    [SerializeField] private GameObject Drywaller2;
    [SerializeField] private GameObject Masonry;

	private bool laborerSelected;
	private bool painterSelected;
	private bool carpenter1Selected;
    private bool cartSelected;
    private bool dw1Selected;
    private bool dw2Selected;
    private bool masonrySelected;

    string filePath;
    string LFileName;
    string PFileName;
    string C1FileName;
    string CartFileName;
    string Dw1FileName;
    string Dw2FileName;
    string MaFileName;

    [HideInInspector] public string p_string;
    [HideInInspector]public string l_string;
    [HideInInspector] public string c1_string;
    [HideInInspector] public string cart_string;
    [HideInInspector] public string dw1_string;
    [HideInInspector] public string dw2_string;
    [HideInInspector] public string ma_string;

    #endregion

    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("IMU persistent data path:"+Application.persistentDataPath);
        //Debug.Log("IMU app data path:" + Application.dataPath);

        filePath = Application.persistentDataPath + "/imuReports";

        //filePath = Application.dataPath + "/imuReports";

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        //fileName = string.Format("{0}/imuReport_{1}.txt",filePath,System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

        PFileName = string.Format("{0}/imuReport_Painter_{1}.txt",filePath,System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")); 
        LFileName = string.Format("{0}/imuReport_Laborer_{1}.txt",filePath,System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        C1FileName = string.Format("{0}/imuReport_Carpenter1_{1}.txt",filePath,System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        CartFileName = string.Format("{0}/imuReport_CartWorker_{1}.txt", filePath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        Dw1FileName = string.Format("{0}/imuReport_Drywaller1_{1}.txt", filePath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        Dw2FileName = string.Format("{0}/imuReport_Drywaller2_{1}.txt", filePath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        MaFileName = string.Format("{0}/imuReport_Masonry_{1}.txt", filePath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void laborerSelect()
    {
    	laborerSelected = true;
        File.WriteAllText(LFileName, "IMU Report: ");
        File.AppendAllText(LFileName, "Laborer\n");    
    }

    public void painterSelect()
    {
		painterSelected = true;
        File.WriteAllText(PFileName, "IMU Report: ");
        File.AppendAllText(PFileName, "Painter\n");    
    }
    public void carpenter1Select()
    {
    	carpenter1Selected = true;
        File.WriteAllText(C1FileName, "IMU Report: ");
        File.AppendAllText(C1FileName, "Carpenter 1\n");
    }
    public void cartSelect()
    {
        cartSelected = true;
        File.WriteAllText(CartFileName, "IMU Report: ");
        File.AppendAllText(CartFileName, "Cart Worker\n");
    }
    public void dw1Select()
    {
        dw1Selected = true;
        File.WriteAllText(Dw1FileName, "IMU Report: ");
        File.AppendAllText(Dw1FileName, "Drywaller 1\n");
    }
    public void dw2Select()
    {
        dw2Selected = true;
        File.WriteAllText(Dw2FileName, "IMU Report: ");
        File.AppendAllText(Dw2FileName, "Drywaller 2\n");
    }
    public void masonrySelect()
    {
        masonrySelected = true;
        File.WriteAllText(MaFileName, "IMU Report: ");
        File.AppendAllText(MaFileName, "Masonry\n");
    }


    //rewrite. Back button on report canvas. End all file write.
    public void backSelected()
    {
    	//reset workers
        laborer.GetComponent<workerScript>().reset();
        painter.GetComponent<workerScript>().reset();
        carpenter1.GetComponent<workerScript>().reset();
        CartWorker.GetComponent<workerScript>().reset();
        Drywaller1.GetComponent<workerScript>().reset();
        Drywaller2.GetComponent<workerScript>().reset();
        Masonry.GetComponent<workerScript>().reset();

        //reset file name using new date and time.
        //fileName = string.Format("{0}/imuReport_{1}.txt",filePath,System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        //Close all canvas
        //WorkerSelectPage.SetActive(false);
        //ReportPage.SetActive(false);
        //SelectedWorkerText.GetComponent<TextMeshProUGUI>().text = "";
    }

    //Finish selection of worker, proceed to report
    public void done()
    {
    	//activate report canvas
    	//ReportPage.SetActive(true);

    	//activate workers selected
    	if(laborerSelected)
    	{
			laborer.GetComponent<workerScript>().active = true;
        	laborer.GetComponent<workerScript>().fileName = LFileName;
    	}
    	if(painterSelected)
    	{
			painter.GetComponent<workerScript>().active = true;
        	painter.GetComponent<workerScript>().fileName = PFileName;
    	}
    	if(carpenter1Selected)
    	{
			carpenter1.GetComponent<workerScript>().active = true;
        	carpenter1.GetComponent<workerScript>().fileName = C1FileName;
    	}
        if (cartSelected)
        {
            CartWorker.GetComponent<workerScript>().active = true;
            CartWorker.GetComponent<workerScript>().fileName = CartFileName;
        }
        if (dw1Selected)
        {
            Drywaller1.GetComponent<workerScript>().active = true;
            Drywaller1.GetComponent<workerScript>().fileName = Dw1FileName;
        }
        if (dw2Selected)
        {
            Drywaller2.GetComponent<workerScript>().active = true;
            Drywaller2.GetComponent<workerScript>().fileName = Dw2FileName;
        }
        if (masonrySelected)
        {
            Masonry.GetComponent<workerScript>().active = true;
            Masonry.GetComponent<workerScript>().fileName = MaFileName;
        }

    }

    
    void Update()
    {
        l_string = laborer.GetComponent<workerScript>().ReportString;
        p_string = painter.GetComponent<workerScript>().ReportString;
        c1_string = carpenter1.GetComponent<workerScript>().ReportString;
        cart_string = CartWorker.GetComponent<workerScript>().ReportString;
        dw1_string = Drywaller1.GetComponent<workerScript>().ReportString;
        dw2_string = Drywaller2.GetComponent<workerScript>().ReportString;
        ma_string = Masonry.GetComponent<workerScript>().ReportString;
    }
    
}
