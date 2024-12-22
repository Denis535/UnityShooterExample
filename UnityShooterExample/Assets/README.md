# Overview
This is the **Modular Third-Person Shooter Example** project.
In this example, I implemented my best ideas for organizing the structure and architecture of the game project.
And now I want to share my experience with you!

# Reference
The project has the following structure:
- Project
  * Project - the folder that contains definitions of all modules.
  * Project.00 - the folder that contains source codes of main Project module.
  * Project.01.UI - the folder that contains source codes of Project.UI module.
  * Project.05.App - the folder that contains source codes of Project.App module.
  * Project.06.Game - the folder that contains source codes of Project.Game module.
  * Project.07.Infrastructure - the folder that contains source codes of Project.Infrastructure module.
- Assets
  * Assets.Project - the folder that contains primary built-in assets.
  * Assets.Project.00 - the folder that contains addressable assets of main Project module.
  * Assets.Project.01.UI - the folder that contains addressable assets of Project.UI module.
  * Assets.Project.05.App - the folder that contains addressable assets of Project.App module.
  * Assets.Project.06.Game - the folder that contains addressable assets of Project.Game module.
  * Assets.Project.07.Infrastructure - the folder that contains common addressable assets.
- Plugins - the folder that contains various plugins, libraries, frameworks and packages.

The project has the following architecture:
- Project - the root module that contains the entry point.
- Project.UI - the presentation module that contains UI.
- Project.App - the application module that contains all entities, services and objects.
- Project.Game - the domain module that contains all entities of subject area.
- Project.Game.Actors - the domain module that contains all entities of subject area.
- Project.Game.Things - the domain module that contains all entities of subject area.
- Project.Game.Worlds - the domain module that contains all entities of subject area.
- Project.Infrastructure - the infrastructure module that contains everything common and any low-level stuff.

The project contains the following scenes:
- Assets.Project
  * Launcher
- Assets.Project.00
  * Main
  * MainScene
  * GameScene
- Assets.Project.06.Game.Worlds
  * World_01
  * World_02
  * World_03

The project contains the following source codes:
- Project
  * Editor/ProjectMenuBar.cs
  * Editor/ProjectWindow.cs
  * Launcher.cs
  * Program.cs
  * DebugScreen.cs
- Project.UI
  * Theme.cs
  * Screen.cs
  * Router.cs
- Project.UI.MainScreen
  * Internal/MainWidgetView.cs
  * Internal/MainMenuWidgetView.cs
  * MainWidget.cs
  * MainMenuWidget.cs
- Project.UI.GameScreen
  * Internal/GameWidgetView.cs
  * Internal/PlayerWidgetView.cs
  * Internal/GameTotalsWidgetView.cs
  * Internal/GameMenuWidgetView.cs
  * GameWidget.cs
  * PlayerWidget.cs
  * GameTotalsWidget.cs
  * GameMenuWidget.cs
- Project.UI.Common
  * Internal/DialogWidgetView.cs
  * Internal/LoadingWidgetView.cs
  * Internal/UnloadingWidgetView.cs
  * Internal/SettingsWidgetView.cs
  * Internal/ProfileSettingsWidgetView.cs
  * Internal/VideoSettingsWidgetView.cs
  * Internal/AudioSettingsWidgetView.cs
  * DialogWidget.cs
  * LoadingWidget.cs
  * UnloadingWidget.cs
  * SettingsWidget.cs
  * ProfileSettingsWidget.cs
  * VideoSettingsWidget.cs
  * AudioSettingsWidget.cs
- Project.App
  * Application2.cs
  * Storage.cs
  * Storage.ProfileSettings.cs
  * Storage.VideoSettings.cs
  * Storage.AudioSettings.cs
- Project.Game
  * Internal/CharacterInputProvider.cs
  * Internal/CameraInputProvider.cs
  * Game2.cs
  * Player2.cs
- Project.Game.Actors
  * PlayerCharacter.cs
  * PlayerCamera.cs
  * EnemyCharacter.cs
- Project.Game.Things
  * Gun.cs
  * Bullet.cs
- Project.Game.Worlds
  * World.cs
- Project.Infrastructure
  * Project.UI/VisualElementFactory.cs
  * Project.UI/VisualElementExtensions.cs
  * Project.Game/ICharacterInputProvider.cs
  * Project.Game/ICameraInputProvider.cs
  * Project.Game.Actors/CharacterBase.cs
  * Project.Game.Actors/PlayableCharacterBase.cs
  * Project.Game.Actors/PlayableCameraBase.cs
  * Project.Game.Actors/NonPlayableCharacterBase.cs
  * Project.Game.Actors/MoveableBody.cs
  * Project.Game.Things/WeaponBase.cs
  * System/Lock.cs
  * UnityEngine/Utils.cs
  * UnityEngine/GameObjectExtensions.cs
  * UnityEngine/Points/Point.cs
  * UnityEngine/Points/PlayerPoint.cs
  * UnityEngine/Points/EnemyPoint.cs
  * UnityEngine/Points/ThingPoint.cs
  * UnityEngine/Points/FirePoint.cs
  * UnityEngine/Sockets/Socket.cs
  * UnityEngine/Sockets/WeaponSocket.cs
  * UnityEngine.InputSystem/UIInputProvider.cs
  * UnityEngine.InputSystem/CharacterInputProvider.cs
  * UnityEngine.InputSystem/CameraInputProvider.cs
  * UnityEngine.AddressableAssets/R.cs
  * UnityEngine.AddressableAssets/L.cs

The project has the following dependencies:
- Architecture Game Framework - the framework that helps you develop your project using a modular clean architecture.
  * https://denis535.github.io/#clean-architecture-game-framework-unity
- Addressables Extensions           - the addressables additions that gives you the ability to use your assets in more convenient way.
  * https://denis535.github.io/#addressables-extensions-unity
- Addressables Source Generator     - the addressables additions that gives you the ability to reference your assets and labels in more convenient way with compile time checking support.
  * https://denis535.github.io/#addressables-source-generator-unity
- Colorful Project Window           - the editor extensions that provides you with the more convenient project window.
  * https://denis535.github.io/#colorful-project-window-unity
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
    "com.denis535.clean-architecture-game-framework": "1.5.0",
    "com.denis535.colorful-project-window": "1.1.1",
    "com.unity.2d.sprite": "1.0.0",
    "com.unity.addressables": "2.2.2",
    "com.unity.ide.visualstudio": "2.0.22",
    "com.unity.inputsystem": "1.11.2",
    "com.unity.nuget.mono-cecil": "1.11.4",
    "com.unity.profiling.core": "1.0.2",
    "com.unity.render-pipelines.universal": "17.0.3",
    "com.unity.services.qos": "1.3.2",
    "com.unity.terrain-tools": "5.1.2",
    "com.unity.test-framework": "1.4.5",
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
  * ```Denis535.CleanArchitectureGameFramework.Additions```
  * ```Denis535.CleanArchitectureGameFramework.Internal```
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
