# VisorEffectManager

A BepInEx plugin for SPT-AKI that allows you to customize and remove visual effects from face protection visors in Escape from Tarkov.

## Features

- **Individual Control**: Enable or disable each visor effect separately
- **Real-Time Updates**: Changes apply immediately without needing to discover/cover the visor
- **Persistent Settings**: All configurations are saved automatically

## Controllable Effects

You can individually remove the following visor effects:

1. **Glass Damage** - Removes damage texture from visors
2. **Scratches** - Removes scratches texture from visors
3. **Blur** - Removes blur effect from visors
4. **Distortion** - Removes distortion effect from visors
5. **Mask** - Removes mask texture from visors

## Installation

1. Download the latest release
2. Extract `VisorEffectManager.dll` to your `BepInEx/plugins/` folder
3. Start your SPT server

## In-Game Configuration

The plugin creates a configuration file automatically at:
```
BepInEx/config/com.jero.VisorEffectManager.cfg
```

You can edit this file directly or use the BepInEx Configuration Manager (if installed) to adjust settings in-game:

1. Press `F12` in-game to open the BepInEx Configuration Manager
2. Navigate to **VisorEffectManager** → **Face Shield Settings**
3. Toggle each effect on/off as desired:
   - `true` = Effect removed (visor is cleaner)
   - `false` = Effect enabled (default game behavior)

Changes are applied immediately - no restart required!

## Requirements

- SPT-AKI (Single Player Tarkov)
- BepInEx 5.x
- .NET Standard 2.1

## Notes

- All settings default to `true` (effects removed) for a cleaner visor experience
- Settings persist between game sessions
- Works with all face shields in the game

