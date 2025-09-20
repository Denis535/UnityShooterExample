# Overview
This is the **Modular Third-Person Shooter Example** project.
In this example, I implemented my best ideas for organizing the structure and architecture of the game project.
And now I want to share my experience with you!

# Reference
The project has the following modules:
- Project - the root module that contains the entry point.
- Project.UI - the presentation module that contains UI.
- Project.App - the application module that contains all entities, services and objects.
- Project.Game - the domain module that contains all entities of subject area.
- Project.Game.Entities
- Project.Game.Worlds
- Project.Infrastructure - the infrastructure module that contains everything common and any low-level stuff.

The project has the following assets:
- Assets - the folder that contains primary built-in assets.
- Assets.Project - the folder that contains addressable assets of main Project module.
- Assets.Project.UI - the folder that contains addressable assets of Project.UI module.
- Assets.Project.App - the folder that contains addressable assets of Project.App module.
- Assets.Project.Game - the folder that contains addressable assets of Project.Game module.
- Assets.Project.Infrastructure - the folder that contains common addressable assets.

The project has the following scenes:
- Assets
  * Launcher
- Assets.Project
  * Main
  * MainScene
  * GameScene
- Assets.Project.Game.Worlds
  * World_01
  * World_02
  * World_03

The project has the following dependencies:
- Addressables Extensions           - the addressables additions that gives you the ability to use your assets in more convenient way.
  * https://denis535.github.io/#addressables-extensions-unity
- Addressables Source Generator     - the addressables additions that gives you the ability to reference your assets and labels in more convenient way with compile time checking support.
  * https://denis535.github.io/#addressables-source-generator-unity
- Colorful Project Window           - the editor extensions that provides you with the more convenient project window.
  * https://denis535.github.io/#colorful-project-window-unity
- Game Framework Pro - the framework that helps you develop your project using a modular architecture.
  * https://denis535.github.io/#game-framework-pro-unity
- UIToolkit Theme Style Sheet       - the UIToolkit styles and some additions.
  * https://denis535.github.io/#uitoolkit-theme-style-sheet-unity

# How it works
First of all, Unity Engine loads the built-in ```Launcher``` scene.
The ```Launcher``` scene just contains simple script that loads the ```Main``` scene from the asset bundles.
The ```Main``` scene is loaded throughout the entire lifecycle of the application.
All other scenes are loaded and unloaded depending on the state of the application.

Secondly, the ```Main``` scene contains the ```Program``` entity:
- ```Program``` - the most main entity of application. This entity contains an enty point, initializes and runs the application and stores all other entities, services and objects:
  * ```Theme``` - the audio theme entity. This entity plays various playlists.
  * ```Screen``` - the visual screen entity. This entity shows various widgets.
  * ```Router``` - the application manager service. This services allows you to change the state of application.
  * ```Application``` - the application entity. This entity contains Game entity and all other entities, services and objects:
    * ```Game``` - the game entity that contains game's informations, rules, states and other entities:
      * ```Player``` - the entity representing a real player and that contains player's informations, states and input providers.
      * ```PlayerCharacter``` - the player's avatar character entity.
      * ```PlayerCamera``` - the player's camera entity.
      * ```EnemyCharacter``` - the enemy character entity.
      * ```Gun``` - the gun entity.
      * ```World``` - the world entity.

# How to setup it
- Fill the ```manifest.json``` with the following content (unfortunately Asset Store Tools does not export ```scopedRegistries``` section):
```
{
  "dependencies": {
    "com.denis535.addressables-extensions": "1.0.19",
    "com.denis535.addressables-source-generator": "1.0.43",
    "com.denis535.colorful-project-window": "1.1.1",
    "com.denis535.game-framework-pro": "1.2.2",
    "com.unity.2d.sprite": "1.0.0",
    "com.unity.addressables": "2.7.3",
    "com.unity.ide.visualstudio": "2.0.23",
    "com.unity.inputsystem": "1.14.2",
    "com.unity.nuget.mono-cecil": "1.11.5",
    "com.unity.profiling.core": "1.0.2",
    "com.unity.recorder": "5.1.2",
    "com.unity.render-pipelines.universal": "17.1.0",
    "com.unity.services.qos": "1.3.2",
    "com.unity.terrain-tools": "5.3.0",
    "com.unity.test-framework": "1.5.1",
    "com.unity.modules.animation": "1.0.0",
    "com.unity.modules.assetbundle": "1.0.0",
    "com.unity.modules.audio": "1.0.0",
    "com.unity.modules.imageconversion": "1.0.0",
    "com.unity.modules.jsonserialize": "1.0.0",
    "com.unity.modules.screencapture": "1.0.0",
    "com.unity.modules.terrain": "1.0.0",
    "com.unity.modules.terrainphysics": "1.0.0",
    "com.unity.modules.uielements": "1.0.0",
    "com.unity.modules.umbra": "1.0.0",
    "com.unity.modules.unityanalytics": "1.0.0",
    "com.unity.modules.unitywebrequest": "1.0.0",
    "com.unity.modules.unitywebrequestwww": "1.0.0"
  },
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.denis535.addressables-extensions",
        "com.denis535.addressables-source-generator",
        "com.denis535.colorful-project-window",
        "com.denis535.game-framework-pro"
      ]
    }
  ]
}
```
- Link project with Unity Gaming Services.
- Install the ```Node.js``` and ```Stylus``` package (if you are going to compile stylus files).

# How to build it
- Prepare your project for build (MenuBar/Project/Pre Build).
- Build your project (MenuBar/Project/Build).

# Links
- https://denis535.github.io/#unity-shooter-example

# If you want to support me
If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos.
