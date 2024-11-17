# Overview
This project is an example of a third person shooter game with high-quality modular architecture.

# Reference
The project contains the following modules:
- Project                - The root module.
- Project.UI             - The presentation module.
- Project.UI.Internal    - The presentation module.
- Project.App            - The application module that contains all entities, services and objects.
- Project.Game           - The domain module that contains all entities of subject area.
- Project.Game.Actors    - The domain module that contains all entities of subject area.
- Project.Game.Things    - The domain module that contains all entities of subject area.
- Project.Game.Worlds    - The domain module that contains all entities of subject area.
- Project.Infrastructure - The infrastructure module that contains everything common and any low-level stuff.

The project contains the following source codes:
- Project
  - Assets/Project/Project.00/Launcher.cs
  - Assets/Project/Project.00/Program.cs
  - Assets/Project/Project.00/DebugScreen.cs
  - Assets/Project/Project.00/Editor/ProjectBar.cs
  - Assets/Project/Project.00/Editor/ProjectWindow.cs
- Project.UI
  - Assets/Project/Project.01.UI/UITheme.cs
  - Assets/Project/Project.01.UI/UIScreen.cs
  - Assets/Project/Project.01.UI/UIRouter.cs
  - Assets/Project/Project.01.UI.MainScreen/MainWidget.cs
  - Assets/Project/Project.01.UI.MainScreen/MainMenuWidget.cs
  - Assets/Project/Project.01.UI.GameScreen/GameWidget.cs
  - Assets/Project/Project.01.UI.GameScreen/PlayerWidget.cs
  - Assets/Project/Project.01.UI.GameScreen/GameTotalsWidget.cs
  - Assets/Project/Project.01.UI.GameScreen/GameMenuWidget.cs
  - Assets/Project/Project.01.UI.Common/LoadingWidget.cs
  - Assets/Project/Project.01.UI.Common/UnloadingWidget.cs
  - Assets/Project/Project.01.UI.Common/SettingsWidget.cs
  - Assets/Project/Project.01.UI.Common/ProfileSettingsWidget.cs
  - Assets/Project/Project.01.UI.Common/VideoSettingsWidget.cs
  - Assets/Project/Project.01.UI.Common/AudioSettingsWidget.cs
  - Assets/Project/Project.01.UI.Common/DialogWidget.cs
- Project.UI.Internal
  - Assets/Project/Project.02.UI.MainScreen/MainWidgetView.cs
  - Assets/Project/Project.02.UI.MainScreen/MainMenuWidgetView.cs
  - Assets/Project/Project.02.UI.GameScreen/GameWidgetView.cs
  - Assets/Project/Project.02.UI.GameScreen/PlayerWidgetView.cs
  - Assets/Project/Project.02.UI.GameScreen/GameTotalsWidgetView.cs
  - Assets/Project/Project.02.UI.GameScreen/GameMenuWidgetView.cs
  - Assets/Project/Project.02.UI.Common/LoadingWidgetView.cs
  - Assets/Project/Project.02.UI.Common/UnloadingWidgetView.cs
  - Assets/Project/Project.02.UI.Common/SettingsWidgetView.cs
  - Assets/Project/Project.02.UI.Common/ProfileSettingsWidgetView.cs
  - Assets/Project/Project.02.UI.Common/VideoSettingsWidgetView.cs
  - Assets/Project/Project.02.UI.Common/AudioSettingsWidgetView.cs
  - Assets/Project/Project.02.UI.Common/DialogWidgetView.cs
- Project.App
  - Assets/Project/Project.05.App/Application2.cs
  - Assets/Project/Project.05.App/Storage.cs
  - Assets/Project/Project.05.App/Storage.ProfileSettings.cs
  - Assets/Project/Project.05.App/Storage.VideoSettings.cs
  - Assets/Project/Project.05.App/Storage.AudioSettings.cs
- Project.Game
  - Assets/Project/Project.06.Game/Game2.cs
  - Assets/Project/Project.06.Game/Player2.cs
  - Assets/Project/Project.06.Game.Actors/Camera2.cs
  - Assets/Project/Project.06.Game.Actors/PlayerCharacter.cs
  - Assets/Project/Project.06.Game.Actors/EnemyCharacter.cs
  - Assets/Project/Project.06.Game.Things/Gun.cs
  - Assets/Project/Project.06.Game.Things/Bullet.cs
  - Assets/Project/Project.06.Game.Worlds/World.cs

The project has the following dependencies:
- Clean Architecture Game Framework - The framework that helps you develop your project following best practices.
- Addressables Extensions           - The additions that gives you the ability to manage your assets in more convenient way.
- Addressables Source Generator     - The additions that gives you the ability to reference your assets in more convenient way with compile time checking support.
- Colorful Project Window           - The more convenient project window.
- UIToolkit Theme Style Sheet       - The UIToolkit theme style sheet and some additions and tools.
- FC Game Audio Pack 1 [Lite]       - The musical themes.

# Setup
- Fill the ```manifest.json``` with the following content:
```
{
  "dependencies": {
    "com.denis535.addressables-extensions": "1.0.19",
    "com.denis535.addressables-source-generator": "1.0.43",
    "com.denis535.clean-architecture-game-framework": "1.4.8",
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
Or you can install the ```Embedded dependencies``` package.
- Link project with ```Unity Gaming Services```.
- Install the ```Node.js``` and ```Stylus``` package (not necessarily).

# Build
- Prepare your project for build (MenuBar/Project/Pre Build).
- Build your project (MenuBar/Project/Build).

# Media
- https://youtu.be/sfkF_tLaoOk
- https://youtu.be/WmLJHRg0EI4

# Tutorial
- https://www.udemy.com/user/denis-84102

# Links
- https://denis535.github.io
- https://www.nuget.org/profiles/Denis535
- https://openupm.com/packages/?sort=downloads&q=denis535
- https://www.fab.com/sellers/Denis535
- https://assetstore.unity.com/publishers/90787
- https://www.youtube.com/channel/UCLFdZl0pFkCkHpDWmodBUFg
- https://www.udemy.com/user/denis-84102

# If you want to support me
If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos.
