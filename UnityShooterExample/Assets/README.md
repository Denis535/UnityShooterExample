# Overview
This is a third-person shooter example that uses a modular architecture.
In this example, I implemented my best ideas for organizing the architecture and structure of the game project.
And now I want to share my experience with you!

# Reference
The project has the following structure:
- Project - the folder that contains all definitions of main modules.
- Project.Content - the folder that contains all sources and assets of main modules.
- Project.Infrastructure - the folder that contains everything common and all low-level staff.
- Plugins - the folder that contains various libraries, frameworks and packages.

The project contains the following modules:
- Project                - the root module that contains the entry point.
- Project.UI             - the presentation module that contains UI.
- Project.UI.Internal    - the presentation module that contains low-level UI.
- Project.App            - the application module that contains all entities, services and objects.
- Project.Game           - the domain module that contains all entities of subject area.
- Project.Game.Actors    - the domain module that contains all entities of subject area.
- Project.Game.Things    - the domain module that contains all entities of subject area.
- Project.Game.Worlds    - the domain module that contains all entities of subject area.
- Project.Infrastructure - the infrastructure module that contains everything common and any low-level stuff.

The project contains the following source codes:
- Project
  * Assets/Project/Project.00/Launcher.cs
  * Assets/Project/Project.00/Program.cs
  * Assets/Project/Project.00/DebugScreen.cs
  * Assets/Project/Project.00/Editor/ProjectBar.cs
  * Assets/Project/Project.00/Editor/ProjectWindow.cs
- Project.UI
  * Assets/Project/Project.01.UI/UITheme.cs
  * Assets/Project/Project.01.UI/UIScreen.cs
  * Assets/Project/Project.01.UI/UIRouter.cs
  * Assets/Project/Project.01.UI.MainScreen/MainWidget.cs
  * Assets/Project/Project.01.UI.MainScreen/MainMenuWidget.cs
  * Assets/Project/Project.01.UI.GameScreen/GameWidget.cs
  * Assets/Project/Project.01.UI.GameScreen/PlayerWidget.cs
  * Assets/Project/Project.01.UI.GameScreen/GameTotalsWidget.cs
  * Assets/Project/Project.01.UI.GameScreen/GameMenuWidget.cs
  * Assets/Project/Project.01.UI.Common/LoadingWidget.cs
  * Assets/Project/Project.01.UI.Common/UnloadingWidget.cs
  * Assets/Project/Project.01.UI.Common/SettingsWidget.cs
  * Assets/Project/Project.01.UI.Common/ProfileSettingsWidget.cs
  * Assets/Project/Project.01.UI.Common/VideoSettingsWidget.cs
  * Assets/Project/Project.01.UI.Common/AudioSettingsWidget.cs
  * Assets/Project/Project.01.UI.Common/DialogWidget.cs
- Project.UI.Internal
  * Assets/Project/Project.02.UI.MainScreen/MainWidgetView.cs
  * Assets/Project/Project.02.UI.MainScreen/MainMenuWidgetView.cs
  * Assets/Project/Project.02.UI.GameScreen/GameWidgetView.cs
  * Assets/Project/Project.02.UI.GameScreen/PlayerWidgetView.cs
  * Assets/Project/Project.02.UI.GameScreen/GameTotalsWidgetView.cs
  * Assets/Project/Project.02.UI.GameScreen/GameMenuWidgetView.cs
  * Assets/Project/Project.02.UI.Common/LoadingWidgetView.cs
  * Assets/Project/Project.02.UI.Common/UnloadingWidgetView.cs
  * Assets/Project/Project.02.UI.Common/SettingsWidgetView.cs
  * Assets/Project/Project.02.UI.Common/ProfileSettingsWidgetView.cs
  * Assets/Project/Project.02.UI.Common/VideoSettingsWidgetView.cs
  * Assets/Project/Project.02.UI.Common/AudioSettingsWidgetView.cs
  * Assets/Project/Project.02.UI.Common/DialogWidgetView.cs
- Project.App
  * Assets/Project/Project.05.App/Application2.cs
  * Assets/Project/Project.05.App/Storage.cs
  * Assets/Project/Project.05.App/Storage.ProfileSettings.cs
  * Assets/Project/Project.05.App/Storage.VideoSettings.cs
  * Assets/Project/Project.05.App/Storage.AudioSettings.cs
- Project.Game
  * Assets/Project/Project.06.Game/Game2.cs
  * Assets/Project/Project.06.Game/Player2.cs
  * Assets/Project/Project.06.Game.Actors/Camera2.cs
  * Assets/Project/Project.06.Game.Actors/PlayerCharacter.cs
  * Assets/Project/Project.06.Game.Actors/EnemyCharacter.cs
  * Assets/Project/Project.06.Game.Things/Gun.cs
  * Assets/Project/Project.06.Game.Things/Bullet.cs
  * Assets/Project/Project.06.Game.Worlds/World.cs
- Project.Infrastructure
  * Assets/Project.Infrastructure/System/Lock.cs
  * Assets/Project.Infrastructure/UnityEngine/GameObjectExtensions.cs
  * Assets/Project.Infrastructure/UnityEngine/Utils.cs
  * Assets/Project.Infrastructure/UnityEngine/Points/Point.cs
  * Assets/Project.Infrastructure/UnityEngine/Points/PlayerPoint.cs
  * Assets/Project.Infrastructure/UnityEngine/Points/EnemyPoint.cs
  * Assets/Project.Infrastructure/UnityEngine/Points/ThingPoint.cs
  * Assets/Project.Infrastructure/UnityEngine/Points/FirePoint.cs
  * Assets/Project.Infrastructure/UnityEngine/Sockets/Socket.cs
  * Assets/Project.Infrastructure/UnityEngine/Sockets/WeaponSocket.cs
  * Assets/Project.Infrastructure/Project.01.UI/VisualElementFactory.cs
  * Assets/Project.Infrastructure/Project.01.UI/VisualElementExtensions.cs
  * Assets/Project.Infrastructure/Project.06.Game.Actors/CharacterBase.cs
  * Assets/Project.Infrastructure/Project.06.Game.Actors/PlayableCharacterBase.cs
  * Assets/Project.Infrastructure/Project.06.Game.Actors/NonPlayableCharacterBase.cs
  * Assets/Project.Infrastructure/Project.06.Game.Actors/MoveableBody.cs
  * Assets/Project.Infrastructure/Project.06.Game.Things/WeaponBase.cs

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

# Setup
- You must fill the ```manifest.json``` with the following content (unfortunately Asset Store Tools does not export ```scopedRegistries``` section):
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
- Then you must delete assemblies in ```Plugins``` folder:
    * ```Denis535.Addressables.Extensions```
    * ```Denis535.Addressables.SourceGenerator```
    * ```Denis535.CleanArchitectureGameFramework```
    * ```Denis535.CleanArchitectureGameFramework.Internal```
    * ```Denis535.CleanArchitectureGameFramework.Additions```
    * ```Denis535.CleanArchitectureGameFramework.Editor```
    * ```Denis535.ColorfulProjectWindow```
- You must link project with Unity Gaming Services.
- You must install the ```Node.js``` and ```Stylus``` package (if you are going to compile stylus files).

# How to build it
- Prepare your project for build (MenuBar/Project/Pre Build).
- Build your project (MenuBar/Project/Build).

# Links
- https://denis535.github.io/#unity-shooter-example
- https://www.nuget.org/profiles/Denis535
- https://openupm.com/packages/?sort=downloads&q=denis535
- https://www.fab.com/sellers/Denis535
- https://assetstore.unity.com/publishers/90787
- https://www.youtube.com/channel/UCLFdZl0pFkCkHpDWmodBUFg
- https://www.udemy.com/user/denis-84102

# If you want to support me
If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos.
