# VisorEffectManager

BepInEx plugin for SPT (Single Player Tarkov) that allows managing and customizing the visual effects of face protection visors.

## Description

This plugin allows you to individually control the visual effects of visors in Escape from Tarkov, including damage, scratches, blur, distortion, and mask. All settings can be adjusted in real-time during gameplay.

## Requirements

- SPT-AKI (Single Player Tarkov)
- BepInEx installed and configured
- .NET Standard 2.1

## Installation

1. Compile the project or download the compiled version
2. Copy the `VisorEffectManager.dll` file to `BepInEx/plugins/`
3. Start the SPT server

## Features

- **Individual Control**: Enable or disable each effect separately
- **Real-Time Updates**: Changes are applied immediately without needing to discover/cover the visor
- **Persistent Settings**: All settings are saved and loaded automatically

### Controllable Effects

- Visor damage (`_GlassDamageTex`)
- Scratches (`_ScratchesTex`)
- Blur (`_BlurMask`)
- Distortion (`_DistortMask`)
- Mask (`_Mask`)

## Development

### Project Structure

```
VisorEffectManager/
├── Plugin.cs              # Main plugin class
├── Patches/
│   └── FaceShieldPatch.cs # Harmony patch to modify visors
└── VisorEffectManager.csproj
```

### Build

Make sure the `TarkovDir` variable in the `.csproj` file points to the correct directory of your SPT installation.

### Technologies

- **BepInEx**: Modding framework
- **Harmony**: Code patching library
- **SPT Reflection**: SPT reflection utilities

## License

MIT

## Author

silviohmartins
