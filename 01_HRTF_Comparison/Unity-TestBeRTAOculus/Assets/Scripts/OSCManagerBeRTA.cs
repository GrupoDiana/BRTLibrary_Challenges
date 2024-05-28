using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OSCManagerBeRTA : MonoBehaviour
{
    //private enum TAudioRenderApplication { bita, berta };    
    private OSC oscLibrary;
    private OSCMessageBeRTA appMessage;
    private OscMessage lastMessageReceived;
    private string myIP;

    private void Awake()
    {
        oscLibrary = this.GetComponent<OSC>();
        appMessage = new OSCMessageBeRTA();
        lastMessageReceived = new OscMessage();
        myIP = GetLocalIPAddress();
    }
    public void Setup()
    {
        oscLibrary.SetAllMessageHandler(OnReceive);
    }


    // RECEIVER

    void OnReceive(OscMessage message)
    {
        lastMessageReceived = message;
        //if (message.address == "/control/connect")
        //{

        //    float data = message.GetFloat(0);            

        //}
        //else if (message.address == "/control/disconnect")
        //{

        //    float data = message.GetFloat(0);


        //}
        Debug.Log("The following OSC message has been received: " + message.address);
    }

    private bool isReceivedMessage(string _message, out ArrayList _parameters)
    {
        if (_message == lastMessageReceived.address)
        {
            _parameters = lastMessageReceived.values;
            return true;
        }
        _parameters = new ArrayList();
        return false;
    }

    public bool isReceivedControlConnect(out ArrayList _parameters)
    {
        return isReceivedMessage("/control/connect", out _parameters);
    }

    public bool isReceivedLoadHRTF(out ArrayList _parameters)
    {
        return isReceivedMessage("/loadHRTF", out _parameters);
    }


    ///////////////////
    /// SENDER        
    ///////////////////    
    public void ControlConnect()
    {
        int inPort = oscLibrary.inPort;
        myIP = "127.0.0.1";
        oscLibrary.Send(appMessage.ControlConnect(myIP, inPort));
    }
    public void ControlDisconnect()
    {
        oscLibrary.Send(appMessage.ControlDisconnect());
    }
    public void ControlPing()
    {
        oscLibrary.Send(appMessage.ControlPing());
    }
    public void ControlVersion()
    {
        oscLibrary.Send(appMessage.ControlVersion());
    }
    public void ControlSampleRate()
    {
        oscLibrary.Send(appMessage.ControlSampleRate());
    }
    public void ControlFrameSize()
    {
        oscLibrary.Send(appMessage.ControlFrameSize());
    }

    ///// PlayBack Control
    public void GeneralStart()
    {
        oscLibrary.Send(appMessage.Start());
    }
    public void GeneralPause()
    {
        oscLibrary.Send(appMessage.Pause());
    }
    public void GeneralStop()
    {
        oscLibrary.Send(appMessage.Stop());
    }        
    public void RemoveAllSources()
    {
        oscLibrary.Send(appMessage.RemoveAllSources());
    }

    ////// Source

    public void LoadSoundSource(string soundSourceID, string wavfile) {
        oscLibrary.Send(appMessage.LoadSoundSource(soundSourceID, wavfile));
    }
    public void RemoveSoundSource(string soundSourceID)
    {
        oscLibrary.Send(appMessage.RemoveSoundSource(soundSourceID));
    }
    public void PlaySoundSource(string soundSourceID) {
        oscLibrary.Send(appMessage.PlaySoundSource(soundSourceID));
    }
    public void PauseSoundSource(string soundSourceID)
    {
        oscLibrary.Send(appMessage.PauseSoundSource(soundSourceID));
    }
    public void StopSoundSource(string soundSourceID)
    {
        oscLibrary.Send(appMessage.StopSoundSource(soundSourceID));
    }
    public void PlayAndRecordSoundSource(string soundSourceID, string filename)
    {        
        oscLibrary.Send(appMessage.PlayAndRecordSoundSource(soundSourceID, filename));
    }
   
    public void LoopSoundSource(string soundSourceID, bool _enable) {
        oscLibrary.Send(appMessage.LoopSoundSource(soundSourceID, _enable));
    }
    public void MuteSoundSource(string soundSourceID)
    {
        oscLibrary.Send(appMessage.MuteSoundSource(soundSourceID));
    }
    public void UnmuteSoundSource(string soundSourceID)
    {
        oscLibrary.Send(appMessage.UnmuteSoundSource(soundSourceID));
    }

    public void SoundSourceLocation(string soundSourceID, Vector3 _location) {
        oscLibrary.Send(appMessage.SoundSourceLocation(soundSourceID, _location));
    }
    public void SoundSourceOrientation(string soundSourceID, Vector3 _orientation)
    {
        oscLibrary.Send(appMessage.SoundSourceOrientation(soundSourceID, _orientation));
    }
    public void SetSoundSourceGain(string soundSourceID, float _gain) {
        oscLibrary.Send(appMessage.SoundSourceGain(soundSourceID, _gain));
    }

    
   
    //// Resources
    public void LoadHRTF(string _hrtfID, string _hrtfFilePath, float _samplingStep)
    {
        oscLibrary.Send(appMessage.LoadHRTF(_hrtfID, _hrtfFilePath, _samplingStep));
    }

    public void LoadDirectivityTF(string _ID, string _filePath, float _samplingStep)
    {
        oscLibrary.Send(appMessage.LoadDirectivity(_ID, _filePath, _samplingStep));
    }

    public void LoadNFCFilters(string _ID, string _filePath)
    {
        oscLibrary.Send(appMessage.LoadNFCFilters(_ID, _filePath));
    }

    ////LISTENER
    public void SendListenerLocation(Vector3 _location)
    {
        oscLibrary.Send(appMessage.ListenerLocation(_location));
    }

    public void SendListenerOrientation(Vector3 _rotation)
    {
        oscLibrary.Send(appMessage.ListenerOrientation(_rotation));
    }
    public void SetListenerHRTF(string _listenerID, string _hrtfID)
    {
        oscLibrary.Send(appMessage.ListenerSetHRTF(_listenerID, _hrtfID));
    }

    public void SendListenerEnableSpatialization(string _listenerID, bool _enabled)
    {
        oscLibrary.Send(appMessage.ListenerEnableSpatialization(_listenerID, _enabled));

    }
    public void SendListenerEnableInterpolation(string _listenerID, bool _enabled)
    {
        oscLibrary.Send(appMessage.ListenerSetEnableInterpolation(_listenerID, _enabled));
    }
    public void SetListenerNFCFilters(string _listenerID, string _NFCFilterID)
    {
        oscLibrary.Send(appMessage.ListenerSetNFCFilterMessage(_listenerID, _NFCFilterID));
    }

    public void SendListenerEnableNearFieldEffect(string _listenerID, bool _enabled)
    {
        oscLibrary.Send(appMessage.ListenerSetEnableNearFieldEffect(_listenerID, _enabled));
    }
    public void SendListenerEnableBilateralAmbisonics(string _listenerID, bool _enabled)
    {
        oscLibrary.Send(appMessage.ListenerEnableBilateralAmbisonics(_listenerID, _enabled));
    }

    public void SendListenerAmbisonicOrder(string _listenerID, int _order)
    {
        oscLibrary.Send(appMessage.ListenerSetAmbisonicOrder(_listenerID, _order));
    }

    public void SendListenerAmbisonicNormalization(string _listenerID, string _normalization)
    {
        oscLibrary.Send(appMessage.ListenerSetAmbisonicNormalization(_listenerID, _normalization));
    } 


    public string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                //hintText.text = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
}

public class OSCMessageBeRTA
{
    // Control
    public OscMessage ControlConnect(string _ip, int _port)
    {
        OscMessage message = new OscMessage();
        message.address = "/control/connect";
        message.values.Add(_ip);
        message.values.Add(_port);        
        return message;
    }
    public OscMessage ControlDisconnect()
    {
        OscMessage message = new OscMessage();
        message.address = "/control/disconnect";        
        return message;
    }
    public OscMessage ControlPing()
    {
        OscMessage message = new OscMessage();
        message.address = "/control/ping";
        return message;
    }
    public OscMessage ControlVersion()
    {
        OscMessage message = new OscMessage();
        message.address = "/control/version";
        return message;
    }
    public OscMessage ControlSampleRate()
    {
        OscMessage message = new OscMessage();
        message.address = "/control/sampleRate";
        return message;
    }
    public OscMessage ControlFrameSize()
    {
        OscMessage message = new OscMessage();
        message.address = "/control/frameSize";
        return message;
    }

    // PLAYBACK Control

    public OscMessage Start()
    {
        OscMessage message = new OscMessage();
        message.address = "/start";
        return message;
    }
    public OscMessage Pause()
    {
        OscMessage message = new OscMessage();
        message.address = "/pause";
        return message;
    }
    public OscMessage Stop()
    {
        OscMessage message = new OscMessage();
        message.address = "/stop";
        return message;
    }
    public OscMessage RemoveAllSources()
    {
        OscMessage message = new OscMessage();
        message.address = "/removeAllSources";
        return message;
    }


    // SOURCE
    public OscMessage LoadSoundSource(string soundSourceID, string wavfile)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/loadSource";
        message.values.Add(soundSourceID);
        message.values.Add(wavfile);        
        return message;
    }
    public OscMessage RemoveSoundSource(string soundSourceID)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/removeSource";
        message.values.Add(soundSourceID);
        return message;
    }
    public OscMessage PlaySoundSource(string soundSourceID)
    {
        OscMessage message = new OscMessage();        
        message.address = "/source/play";
        message.values.Add(soundSourceID);
        return message;
    }
    public OscMessage PlayAndRecordSoundSource(string soundSourceID, string filename)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/playAndRecord";
        message.values.Add(soundSourceID);
        message.values.Add(filename);
        message.values.Add("mat");
        message.values.Add(-1);
        return message;
    }
    public OscMessage PauseSoundSource(string soundSourceID)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/pause";
        message.values.Add(soundSourceID);
        return message;
    }
    public OscMessage StopSoundSource(string soundSourceID)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/stop";
        message.values.Add(soundSourceID);
        return message;
    }

    public OscMessage LoopSoundSource(string soundSourceID, bool _enable)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/loop";
        message.values.Add(soundSourceID);
        if (!_enable) { message.values.Add(0); }
        else { message.values.Add(1); }
        return message;
    }

    public OscMessage MuteSoundSource(string soundSourceID)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/mute";
        message.values.Add(soundSourceID);        
        return message;
    }
    public OscMessage UnmuteSoundSource(string soundSourceID)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/unmute";
        message.values.Add(soundSourceID);
        return message;
    }

    public OscMessage SoundSourceLocation(string soundSourceID, Vector3 _location)
    {
        Vector3 _newLocation = CalculateLocationToBeRTAConvention(_location);
        OscMessage message = new OscMessage();

        message.address = "/source/location";
        message.values.Add(soundSourceID);
        message.values.Add(_newLocation.x);
        message.values.Add(_newLocation.y);
        message.values.Add(_newLocation.z);
        return message;
    }

    public OscMessage SoundSourceOrientation(string soundSourceID, Vector3 _orientation)
    {
        Vector3 _newLocation = CalculateLocationToBeRTAConvention(_orientation);
        OscMessage message = new OscMessage();

        message.address = "/source/orientation";
        message.values.Add(soundSourceID);
        message.values.Add(_newLocation.x);
        message.values.Add(_newLocation.y);
        message.values.Add(_newLocation.z);
        return message;
    }
    public OscMessage SoundSourceGain(string soundSourceID, float _gain)
    {
        OscMessage message = new OscMessage();
        message.address = "/source/gain";        
        message.values.Add(soundSourceID);
        message.values.Add(_gain);
        return message;
    }
    
    //// Resources
    public OscMessage LoadHRTF(string _hrtfID, string _hrtfFilePath, float _samplingStep)
    {
        OscMessage message = new OscMessage();
        message.address = "/loadHRTF";
        message.values.Add(_hrtfID);
        message.values.Add(_hrtfFilePath);
        message.values.Add(_samplingStep);
        return message;
    }
    public OscMessage LoadDirectivity(string _ID, string _filePath, float _samplingStep)
    {
        OscMessage message = new OscMessage();
        message.address = "/loadDirectivityTF";
        message.values.Add(_ID);
        message.values.Add(_filePath);
        message.values.Add(_samplingStep);
        return message;
    }
    public OscMessage LoadNFCFilters(string _ID, string _filePath)
    {
        OscMessage message = new OscMessage();
        message.address = "/loadNFCFilters";
        message.values.Add(_ID);
        message.values.Add(_filePath);
        return message;
    }

    /////// Listener
    public OscMessage ListenerSetHRTF(string _listenerID, string _hrtfID)
    {
        OscMessage message = new OscMessage();
        message.address = "/listener/setHRTF";
        message.values.Add(_listenerID);
        message.values.Add(_hrtfID);        
        return message;
    }

    public OscMessage ListenerOrientation(Vector3 _rotation)
    {
        OscMessage message = new OscMessage();
        message.address = "/listener/orientation";
        message.values.Add("DefaultListener");
        message.values.Add(_rotation.y);    //yaw
        message.values.Add(-_rotation.x);   //pitch         
        message.values.Add(-_rotation.z);    //rolll                 
        return message;
    }

    public OscMessage ListenerLocation(Vector3 _location)
    {
        Vector3 _bitaLocation = CalculateLocationToBeRTAConvention(_location);
        OscMessage message;
        message = new OscMessage();
        message.address = "/listener/location";
        message.values.Add("DefaultListener");
        message.values.Add(_bitaLocation.x);
        message.values.Add(_bitaLocation.y);
        message.values.Add(_bitaLocation.z);
        return message;
    }

    public OscMessage ListenerEnableSpatialization(string _listenerID, bool _enabled) {
        OscMessage message = new OscMessage();
        message.address = "/listener/enableSpatialization";
        message.values.Add(_listenerID);
        if (_enabled) { message.values.Add(1); }
        else { message.values.Add(0); }
        return message;
    }

    public OscMessage ListenerSetEnableInterpolation(string _listenerID, bool _enabled)
    {
        OscMessage message = new OscMessage();
        message.address = "/listener/enableInterpolation";
        message.values.Add(_listenerID);
        if (_enabled) { message.values.Add(1); }
        else { message.values.Add(0); }
        return message;
    }
    public OscMessage ListenerSetNFCFilterMessage(string _listenerID, string _NFCFilterfID) {
        OscMessage message = new OscMessage();
        message.address = "/listener/setNFCFilters";
        message.values.Add(_listenerID);
        message.values.Add(_NFCFilterfID);
        return message;
    }
    public OscMessage ListenerSetEnableNearFieldEffect(string _listenerID, bool _enabled)
    {
        OscMessage message = new OscMessage();
        message.address = "/listener/enableNearFieldEffect";
        message.values.Add(_listenerID);
        if (_enabled) { message.values.Add(1); }
        else { message.values.Add(0); }
        return message;
    }

    public OscMessage ListenerEnableBilateralAmbisonics(string _listenerID, bool _enabled)
    {
        OscMessage message = new OscMessage();
        message.address = "/listener/enableBilateralAmbisonics";
        message.values.Add(_listenerID);
        if (_enabled) { message.values.Add(1); }
        else { message.values.Add(0); }
        return message;
    }
    public OscMessage ListenerSetAmbisonicOrder(string _listenerID, int _order)
    {
        OscMessage message = new OscMessage();
        message.address = "/listener/setAmbisonicsOrder";
        message.values.Add(_listenerID);       
        message.values.Add(_order); 
        return message;
    }

    public OscMessage ListenerSetAmbisonicNormalization(string _listenerID, string _normalization)
    {
        OscMessage message = new OscMessage();
        message.address = "/listener/setAmbisonicsOrder";
        message.values.Add(_listenerID);
        message.values.Add(_normalization);
        return message;
    }

    //
    private Vector3 CalculateLocationToBeRTAConvention(Vector3 _location)
    {
        Vector3 _bertaLocation;
        _bertaLocation.x = _location.z;
        _bertaLocation.y = -_location.x;
        _bertaLocation.z = _location.y;

        return _bertaLocation;
    }


}
