# Maravilloso, Maravilloso! 
The 000_LocTestBeRTA.maxpat allows to control BeRTA via OSC messages. More specifically, it allows to: 
1. Drag and drop audiofiles and HRTFs on MaxMSP, which are then automatically loaded on BeRTA.
2. Control sound sources positions and current HRTF.
3. Load a txt file (TestList.txt is just an example) with a list of trials, each specifying a source name, source position and HRTF.Res_HRTF.maxpat and Res_Source.maxpat are encapsulations that are used in the poly~ objects within the main patch.