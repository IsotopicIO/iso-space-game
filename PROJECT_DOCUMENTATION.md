# Project Documentation

## Project Files
- **Assets**
- **Materials**
  - **Prefabs**
  - **Presets**
  - **Scenes**
  - **Scripts**
    - **Environment:** Scripts that control independent environment models, animations etc.
    - **Management:** Scripts that manage the game and its systems 
    - **Player:** Scripts regarding the player controls, movement and interaction
  - **Settings**
  
 ## Main Scripts
 
 **Management/GameManagement.cs**
 Singleton which controls the map, scrolling speed, and soon scoring system and other main features
 
 **Player/ShipController.cs**
 MonoBehaviour on the player's ship, handles input and movement of the ship
 
 **Player/CameraController.cs**
 MonoBehaviour on the camera, handles camera movement
 
 **Environment/MapSegment.cs**
 Holds information about a map segment, and its functionalities
