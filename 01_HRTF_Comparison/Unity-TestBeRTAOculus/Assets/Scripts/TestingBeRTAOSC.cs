using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBeRTAOSC : MonoBehaviour
{

    private OSCManagerBeRTA oscManager;
    bool osConnectionReady;
    bool hrtfLoaded;
    string currentHRTF;
    string[] HRTF;
    int i = 0;
    int alreadyTried = 0;

    private void Awake()
    {
        oscManager = this.GetComponent<OSCManagerBeRTA>();
        osConnectionReady = false;
        hrtfLoaded = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        oscManager.Setup();

        InitAudioRenderConnection();
        currentHRTF = "hrtf1";
        HRTF = new string[] { "hrtf1", "hrtf2", "hrtf3", "hrtf4", "hrtf5" };
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.F5))
        //{
        //    oscManager.PlaySoundSource("source1");
        //}
        //else if (Input.GetKeyUp(KeyCode.F6))
        //{
        //    oscManager.PauseSoundSource("source1");
        //}
        if(Input.GetKeyUp(KeyCode.F7))
        {
            oscManager.StopSoundSource("source1");
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            oscManager.PlaySoundSource("source1");
            oscManager.SetListenerHRTF("DefaultListener", currentHRTF);
            alreadyTried = 1;
        }
        //get current hrtf
        else if (OVRInput.GetDown(OVRInput.Button.Two) && alreadyTried > 0)
        {
            if(alreadyTried == 1) oscManager.StopSoundSource("source1");
            alreadyTried++;
            i++;
           if (i > 4) { i = 0;}
            oscManager.PlaySoundSource("source1");
            oscManager.SetListenerHRTF("DefaultListener", HRTF[i]);
        }
        else if (OVRInput.GetDown(OVRInput.Button.One) && alreadyTried > 0)
        {
            if (alreadyTried == 1) oscManager.StopSoundSource("source1");
            i--;
            alreadyTried++;
            if (i < 0) { i = 4; }
            oscManager.PlaySoundSource("source1");
            oscManager.SetListenerHRTF("DefaultListener", HRTF[i]);
        }       

    }



    private void InitAudioRenderConnection()
    {

        StartCoroutine(CoroutineInitAudioRenderConnection());
    }


    private IEnumerator CoroutineInitAudioRenderConnection()
    {
        // Config render
        oscManager.ControlConnect();        // Connect to render        
        float safetyTimer = 10;

        ArrayList _returnParameters;
        while (!oscManager.isReceivedControlConnect(out _returnParameters))
        {
            yield return new WaitForSeconds(0.1f);
            safetyTimer -= 0.1f;
            if (safetyTimer < 0)
            {
                Debug.LogError("ERROR trying to connect to Audio Render");
                break;
            }
        }
        Debug.Log("Connection established");
        osConnectionReady = true;
        AfterStartConnectionWithBeRTA();
    }

    private void AfterStartConnectionWithBeRTA() {

        if (!osConnectionReady) return;

        oscManager.ControlVersion();
        oscManager.ControlFrameSize();
        oscManager.ControlSampleRate();

        CleanBeRTAScene();
        LoadHRTF("hrtf1", "P0001_Raw_48kHz.sofa");
        LoadHRTF("hrtf2", "P0205_Raw_48kHz.sofa");
        LoadHRTF("hrtf3", "P0007_Raw_48kHz.sofa");
        LoadHRTF("hrtf4", "P0006_Raw_48kHz.sofa");
        LoadHRTF("hrtf5", "3DTI_HRTF_IRC1008_256s_48000Hz.sofa");
    }


    public void LoadHRTF(string _hrtfID, string _hrtfFileName)
    {
        if (!osConnectionReady) return;
        if (_hrtfFileName != "")
        {
            StartCoroutine(CoroutineLoadHRTF(_hrtfID, _hrtfFileName));
        }        
    }

    private IEnumerator CoroutineLoadHRTF(string _hrtfID, string _hrtfFileName)
    {
        oscManager.LoadHRTF(_hrtfID, GetFullPathToResources(_hrtfFileName), 5.0f);

        ArrayList _returnParameters;
        while (!oscManager.isReceivedLoadHRTF(out _returnParameters))
        {
            yield return new WaitForSeconds(1f);
        }

        if (_returnParameters[0].ToString() == _hrtfID && _returnParameters[1].ToString() == "true")
        {
            AfterHRTFLoaded(_hrtfID);
            oscManager.SetListenerHRTF("DefaultListener", _hrtfID);            
        }
        else
        {
            Debug.LogError("ERROR trying to load the HRTF SOFA file");
        }
    }

    private void AfterHRTFLoaded(string _hrtfID)
    {
        
        Debug.Log("HRTFLoaded : " + _hrtfID);
        hrtfLoaded = true;

        oscManager.LoadSoundSource("source1", GetFullPathToResources("MusArch_Sample_48kHz_Anechoic_FemaleSpeech.wav"));        
    }    



    private void CleanBeRTAScene()
    {
        oscManager.GeneralStop();           // Stop render, just in case
        oscManager.RemoveAllSources();      // Remove all sound sources
    }


    private string GetFullPathToResources(string _filePath)
    {
        if (!System.IO.Path.IsPathRooted(_filePath))
        {
            string appPath = Application.dataPath;
            //string temp = System.IO.Directory.GetParent(System.IO.Directory.GetParent(appPath).ToString()).ToString();
            return (appPath + "/Resources/" + _filePath);
        }
        else
        {
            return _filePath;
        }
    }
}
