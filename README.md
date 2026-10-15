# VisorEffectManager

BepInEx client plugin for SPT (Single Player Tarkov) that lets you manage the visual effects of face shield visors.

## Description

This plugin lets you individually remove the visual effects of visors in Escape from Tarkov: glass damage, scratches, blur and distortion. Each effect can be toggled in the BepInEx configuration or with a keyboard shortcut.

## Requirements

- SPT 4.1.x
- BepInEx (included with SPT)

## Installation

1. Build the project or download the compiled version
2. Copy `VisorEffectManager.dll` to `BepInEx/plugins/` in your SPT folder
3. Start SPT

## Features

- **Individual control**: enable or disable the removal of each effect separately
- **Keyboard shortcuts**: toggle each effect in-game
- **Persistent settings**: all settings are saved and loaded automatically

Changes are applied immediately, including when the visor is swapped or toggled.

### Controllable Effects

| Effect       | Shader property   | Default hotkey      |
|--------------|-------------------|---------------------|
| Glass damage | `_GlassDamageTex` | `Right Ctrl + 1`    |
| Scratches    | `_ScratchesTex`   | `Right Ctrl + 2`    |
| Blur         | `_BlurMask`       | `Right Ctrl + 3`    |
| Distortion   | `_DistortMask`    | `Right Ctrl + 4`    |

All effects are removed by default. Settings and hotkeys can be changed with the BepInEx Configuration Manager (F12) or in `BepInEx/config/com.jero.visoreffectmanager.cfg`.

## Development

### Project Structure

```text
VisorEffectManager/
├── VisorEffectManager.cs  # Main plugin class (configuration and hotkeys)
├── Patches/
│   └── FaceShieldPatch.cs # Applies the settings before VisorEffect renders (OnRenderImageImpl)
└── VisorEffectManager.csproj
```

### Build

The project references assemblies from an SPT installation. By default, `TarkovDir` points to `C:\games\SPT-4.1\`. Change it in the `.csproj` or override it on the command line:

```powershell
dotnet build -c Release -p:TarkovDir=D:\path\to\SPT\
```

The output is `bin/Release/VisorEffectManager.dll`.

### Technologies

- **BepInEx**: modding framework
- **Harmony**: code patching library
- **SPT Reflection**: SPT patching utilities (`ModulePatch`)

## License

MIT

## Author

silviohmartins
