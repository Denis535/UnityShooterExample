# Overview
This project is example of a shooter game with high-quality modular architecture.

# Reference
The project contains the following modules:
- Project                     - The root module.
- Project.UI                  - The presentation module.
- Project.UI.Internal         - The presentation module.
- Project.App                 - The application module containing all entities and services of application.
- Project.Entities            - The domain module containing all entities of subject area.
- Project.Entities.Actors     - The domain module containing all entities of subject area.
- Project.Entities.Things     - The domain module containing all entities of subject area.
- Project.Entities.Worlds     - The domain module containing all entities of subject area.
- Project.Infrastructure      - The infrastructure module containing everything common and low-level.

The project contains the following source codes:
- Project
  - Assets/Project/Project.00/Launcher.cs
  - Assets/Project/Project.00/Program.cs
  - Assets/Project/Project.00/DebugScreen.cs
  - Assets/Project/Project.00/Tools/ProjectBar.cs
  - Assets/Project/Project.00/Tools/ProjectWindow.cs
- Project.UI
  - Assets/Project/Project.01.UI/UITheme.cs
  - Assets/Project/Project.01.UI/UIScreen.cs
  - Assets/Project/Project.01.UI/UIRouter.cs
  - Assets/Project/Project.01.UI/MainScreen/MainWidget.cs
  - Assets/Project/Project.01.UI/MainScreen/MenuWidget.cs
  - Assets/Project/Project.01.UI/GameScreen/GameWidget.cs
  - Assets/Project/Project.01.UI/GameScreen/TotalsWidget.cs
  - Assets/Project/Project.01.UI/GameScreen/MenuWidget.cs
  - Assets/Project/Project.01.UI/Common/DialogWidget.cs
  - Assets/Project/Project.01.UI/Common/LoadingWidget.cs
  - Assets/Project/Project.01.UI/Common/UnloadingWidget.cs
  - Assets/Project/Project.01.UI/Common/SettingsWidget.cs
  - Assets/Project/Project.01.UI/Common/ProfileSettingsWidget.cs
  - Assets/Project/Project.01.UI/Common/VideoSettingsWidget.cs
  - Assets/Project/Project.01.UI/Common/AudioSettingsWidget.cs
- Project.UI.Internal
  - Assets/Project/Project.01.UI.Internal/MainScreen/MainWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/MainScreen/MenuWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/GameScreen/GameWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/GameScreen/TotalsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/GameScreen/MenuWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/DialogWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/LoadingWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/UnloadingWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/SettingsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/ProfileSettingsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/VideoSettingsWidgetView.cs
  - Assets/Project/Project.01.UI.Internal/Common/AudioSettingsWidgetView.cs
- Project.App
  - Assets/Project/Project.02.App/Application2.cs
  - Assets/Project/Project.02.App/Storage.cs
  - Assets/Project/Project.02.App/Storage.ProfileSettings.cs
  - Assets/Project/Project.02.App/Storage.VideoSettings.cs
  - Assets/Project/Project.02.App/Storage.AudioSettings.cs
- Project.Entities
  - Assets/Project/Project.03.Entities/GameBase.cs
  - Assets/Project/Project.03.Entities/Game.cs
  - Assets/Project/Project.03.Entities/PlayerBase.cs
  - Assets/Project/Project.03.Entities/Player.cs
  - Assets/Project/Project.03.Entities/Camera2.cs
- Project.Entities.Actors
  - Assets/Project/Project.03.Entities.Characters/CharacterBase.cs
  - Assets/Project/Project.03.Entities.Characters/PlayableCharacterBase.cs
  - Assets/Project/Project.03.Entities.Characters/NonPlayableCharacterBase.cs
  - Assets/Project/Project.03.Entities.Characters/PlayerCharacter.cs
  - Assets/Project/Project.03.Entities.Characters/EnemyCharacter.cs
- Project.Entities.Things
  - Assets/Project/Project.03.Entities.Things/WeaponBase.cs
  - Assets/Project/Project.03.Entities.Things/Gun.cs
  - Assets/Project/Project.03.Entities.Things/Bullet.cs
- Project.Entities.Worlds
  - Assets/Project/Project.03.Entities.Worlds/World.cs

The project has the following dependencies:
- Clean Architecture Game Framework - The framework that helps you develop your project following best practices.
- Addressables Extensions           - The additions that gives you the ability to manage your assets in more convenient way.
- Addressables Source Generator     - The additions that gives you the ability to reference your assets in more convenient way with compile time checking support.
- Colorful Project Window           - The more convenient project window.
- UIToolkit Theme Style Sheet       - The UIToolkit theme style sheets and some additions and tools.
- FC Game Audio Pack 1 [Lite]       - The music.

# Setup
- Install the "UIToolkit Theme Style Sheet" package (https://denis535.github.io/#uitoolkit-theme-style-sheet).
- Embed "UIToolkit Theme Style Sheet" package (press MenuBar/Tools/UIToolkit Theme Style Sheet/Embed Package).
- Link project with Unity Gaming Services.

# Build
- Prepare your project for build (MenuBar/Project/Pre Build).
- Build your project (MenuBar/Project/Build).

# FAQ
- Why can not I compile the stylus files?
    - You need to install the node.js and stylus.
- Why is the "ThemeStyleSheet.styl" compiled with errors?
    - The "UIToolkit Theme Style Sheet" package must be embedded.
- Why is the UI broken?
    - Sometimes you need to reimport the "UIToolkit Theme Style Sheet" package.

# Media
- https://youtu.be/WmLJHRg0EI4

# Links
- https://denis535.github.io
- https://www.nuget.org/profiles/Denis535
- https://openupm.com/packages/?sort=downloads&q=denis535
- https://assetstore.unity.com/publishers/90787
- https://denis535.itch.io/

# If you want to support me
If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos.
