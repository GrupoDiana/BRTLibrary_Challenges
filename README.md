# BRTLibrary_Challenges
We celebrated a first BRT Challenge in April 2024 to motivate use of the BRT Library. We hope there will be more!

## Málaga Challenge April 2024: HRTF Comparison
The challenge consisted in creating an app that, either by directly using the BRT C++ API, or via OSC communication with BeRTA, was capable of:
* Loading a mono audio sample and creating a BRT sound source
* Loading two or more HRTF sofa files and create a BRT listener. 
* Move at least one spatial DoF of listener or sound source. For example, source azimuth.
* Swap change HRTF during rendering for real time HRTF comparison.  

We divided in three groups: 

1. _Grupo_ 1 used the BRT C++ Api and JUCE to create and app that solves the challenge:
   - This is the link to an example created by [Luis Molina-Tanco](https://github.com/lmtanco) which [Rapolas Daugintis](https://github.com/rapolasd) debugged during the workshop - [Link to submodule](). 
   - This is a work-in-progress VST3 plugin using the JUCE framework and BRT in a CMake project (the BRT library has a CMake branch), created by [Nils Marggraff-Turley](https://github.com/Nils-MaTu) - [Link to submodule]().
3. _Grupo_ 2 used UNITY to create a Virtual Reality app that connected to BeRTA via its OSC interface. The Oculus HMD (they tried Rift, Quest 2 and Quest 3) tracked listener moves and rendered the visual scene, while BeRTA rendered the spatial audio.
4. _Grupo_ 3 used MAX MSP and Python to create apps that solve the challenge - [Download zip]().
   - This is the MAX MSP application _Maravilloso, Maravilloso_ created by [Lorenzo Picinali]() - [Download zip]().
   - This is the Python application created by [Katarina Poole](https://github.com/Katarina-Poole) - [Link to submodule]().
     
