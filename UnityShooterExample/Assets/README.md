# Overview
This is the third-person shooter example.
In this example, I implemented my best ideas for organizing the structure and architecture of the game project.
And now I want to share my experience with you!

# Reference
The project has the following structure:
- Project                - the folder that contains all definitions of main modules.
- Project.Content        - the folder that contains all sources and assets of main modules.
- Project.Infrastructure - the folder that contains everything common and all low-level staff.
- Plugins                - the folder that contains various libraries, frameworks and packages.

The project has the following architecture:
- Project                - the root module that contains the entry point.
- Project.UI             - the presentation module that contains UI.
- Project.UI.Internal    - the presentation module that contains low-level UI.
- Project.App            - the application module that contains all entities, services and objects.
- Project.Game           - the domain module that contains all entities of subject area.
- Project.Game.Actors    - the domain module that contains all entities of subject area.
- Project.Game.Things    - the domain module that contains all entities of subject area.
- Project.Game.Worlds    - the domain module that contains all entities of subject area.
- Project.Infrastructure - the infrastructure module that contains everything common and any low-level stuff.

The project contains the following scenes:
- Launcher
- Project
  * Main
  * MainScene
  * GameScene
- Project.Game.Worlds
  * World_01
  * World_02
  * World_03

The project contains the following source codes:
- Project
  * Launcher.cs
  * Program.cs
  * DebugScreen.cs
  * Editor/ProjectBar.cs
  * Editor/ProjectWindow.cs
- Project.UI
  * UITheme.cs
  * UIScreen.cs
  * UIRouter.cs
  * MainScreen/MainWidget.cs
  * MainScreen/MainMenuWidget.cs
  * GameScreen/GameWidget.cs
  * GameScreen/PlayerWidget.cs
  * GameScreen/GameTotalsWidget.cs
  * GameScreen/GameMenuWidget.cs
  * Common/LoadingWidget.cs
  * Common/UnloadingWidget.cs
  * Common/SettingsWidget.cs
  * Common/ProfileSettingsWidget.cs
  * Common/VideoSettingsWidget.cs
  * Common/AudioSettingsWidget.cs
  * Common/DialogWidget.cs
- Project.UI.Internal
  * MainScreen/MainWidgetView.cs
  * MainScreen/MainMenuWidgetView.cs
  * GameScreen/GameWidgetView.cs
  * GameScreen/PlayerWidgetView.cs
  * GameScreen/GameTotalsWidgetView.cs
  * GameScreen/GameMenuWidgetView.cs
  * Common/LoadingWidgetView.cs
  * Common/UnloadingWidgetView.cs
  * Common/SettingsWidgetView.cs
  * Common/ProfileSettingsWidgetView.cs
  * Common/VideoSettingsWidgetView.cs
  * Common/AudioSettingsWidgetView.cs
  * Common/DialogWidgetView.cs
- Project.App
  * Application2.cs
  * Storage.cs
  * Storage.ProfileSettings.cs
  * Storage.VideoSettings.cs
  * Storage.AudioSettings.cs
- Project.Game
  * Game2.cs
  * Player2.cs
  * Internal/CharacterInputProvider.cs
  * Internal/CameraInputProvider.cs
- Project.Game.Actors
  * Camera2.cs
  * PlayerCharacter.cs
  * EnemyCharacter.cs
- Project.Game.Things
  * Gun.cs
  * Bullet.cs
- Project.Game.Worlds
  * World.cs
- Project.Infrastructure
  * System/Lock.cs
  * UnityEngine/GameObjectExtensions.cs
  * UnityEngine/Utils.cs
  * UnityEngine/Points/Point.cs
  * UnityEngine/Points/PlayerPoint.cs
  * UnityEngine/Points/EnemyPoint.cs
  * UnityEngine/Points/ThingPoint.cs
  * UnityEngine/Points/FirePoint.cs
  * UnityEngine/Sockets/Socket.cs
  * UnityEngine/Sockets/WeaponSocket.cs
  * UnityEngine.InputSystem/InputActions_UI.cs
  * UnityEngine.InputSystem/InputActions_Character.cs
  * UnityEngine.InputSystem/InputActions_Camera.cs
  * UnityEngine.AddressableAssets/R.cs
  * UnityEngine.AddressableAssets/L.cs
  * Project.01.UI/VisualElementFactory.cs
  * Project.01.UI/VisualElementExtensions.cs
  * Project.06.Game.Actors/CharacterBase.cs
  * Project.06.Game.Actors/PlayableCharacterBase.cs
  * Project.06.Game.Actors/NonPlayableCharacterBase.cs
  * Project.06.Game.Actors/MoveableBody.cs
  * Project.06.Game.Things/WeaponBase.cs

The project has the following dependencies:
- Clean Architecture Game Framework - the framework that helps you develop your project using a modular clean architecture.
  * https://denis535.github.io/#clean-architecture-game-framework-unity
- Addressables Extensions           - the addressables additions that gives you the ability to use your assets in more convenient way.
  * https://denis535.github.io/#addressables-extensions-unity
- Addressables Source Generator     - the addressables additions that gives you the ability to reference your assets and labels in more convenient way with compile time checking support.
  * https://denis535.github.io/#addressables-source-generator-unity
- Colorful Project Window           - the editor extensions that provides you with the more convenient project window.
  * https://denis535.github.io/#colorful-project-window-unity
- UIToolkit Theme Style Sheet       - the UIToolkit styles and some additions.
  * https://denis535.github.io/#uitoolkit-theme-style-sheet-unity
- FC Game Audio Pack 1 [Lite]       - the audio themes.
  * https://assetstore.unity.com/packages/audio/music/fc-game-audio-pack-1-lite-182311

# How it works
First of all, Unity Engine loads the built-in ```Launcher``` scene.
The ```Launcher``` scene just contains simple script that loads the ```Main``` scene from the asset bundles.
The ```Main``` scene is loaded throughout the entire lifecycle of the application.
All other scenes are loaded and unloaded depending on the state of the application.

Secondly, the ```Main``` scene contains the ```Program``` entity:
- ```Program``` - the most main entity of application. This entity contains an enty point, initializes and runs the application and stores all other entities, services and objects:
  * ```UITheme``` - the audio theme entity. This entity plays various playlists.
  * ```UIScreen``` - the visual screen entity. This entity shows various widgets.
  * ```UIRouter``` - the application manager service. This services allows you to change the state of application.
  * ```Application``` - the application entity. This entity contains Game entity and all other entities, services and objects:
    * ```Game``` - the game entity that contains game's informations, rules, states and other entities:
      * ```Player``` - the entity representing a real player and that contains player's informations, states and input providers.
      * ```PlayerCharacter``` - the player's avatar character entity.
      * ```EnemyCharacter``` - the enemy character entity.
      * ```Camera``` - the player's camera entity.
      * ```Gun``` - the gun entity.
      * ```World``` - the world entity.

# How to setup it
- Fill the ```manifest.json``` with the following content (unfortunately Asset Store Tools does not export ```scopedRegistries``` section):
    ```
    {
      "dependencies": {
        "com.denis535.addressables-extensions": "1.0.19",
        "com.denis535.addressables-source-generator": "1.0.43",
        "com.denis535.clean-architecture-game-framework": "1.4.11",
        "com.denis535.colorful-project-window": "1.0.46",
        "com.unity.2d.sprite": "1.0.0",
        "com.unity.addressables": "1.21.21",
        "com.unity.ide.visualstudio": "2.0.22",
        "com.unity.inputsystem": "1.7.0",
        "com.unity.nuget.mono-cecil": "1.11.4",
        "com.unity.profiling.core": "1.0.2",
        "com.unity.render-pipelines.universal": "16.0.6",
        "com.unity.services.qos": "1.3.2",
        "com.unity.terrain-tools": "5.1.2",
        "com.unity.test-framework": "1.3.9",
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
            "com.denis535.clean-architecture-game-framework",
            "com.denis535.colorful-project-window"
          ]
        }
      ]
    }
    ```
- Then delete modules in ```Plugins``` folder:
    * ```Denis535.Addressables.Extensions```
    * ```Denis535.Addressables.SourceGenerator```
    * ```Denis535.CleanArchitectureGameFramework```
    * ```Denis535.CleanArchitectureGameFramework.Internal```
    * ```Denis535.CleanArchitectureGameFramework.Additions```
    * ```Denis535.CleanArchitectureGameFramework.Editor```
    * ```Denis535.ColorfulProjectWindow```
- Link project with Unity Gaming Services.
- Install the ```Node.js``` and ```Stylus``` package (if you are going to compile stylus files).

# How to build it
- Prepare your project for build (MenuBar/Project/Pre Build).
- Build your project (MenuBar/Project/Build).

# Links
- https://denis535.github.io/#unity-shooter-example

# If you want to support me
If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos.
