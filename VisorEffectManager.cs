using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;
using VisorEffectManager.Patches;

namespace VisorEffectManager
{
    [BepInPlugin("com.jero.visoreffectmanager", "VisorEffectManager", "1.1.0")]
    public class VisorEffectManager : BaseUnityPlugin
    {
        internal static ManualLogSource LogSource;

        private const string MainSectionName = "Face Shield Settings";
        private const string KeyboardSectionName = "Keyboard Shortcuts";

        // Configurações para cada textura
        internal static ConfigEntry<bool> RemoveGlassDamage;
        internal static ConfigEntry<bool> RemoveScratches;
        internal static ConfigEntry<bool> RemoveBlur;
        internal static ConfigEntry<bool> RemoveDistortion;

        // Configurações de teclas de atalho
        internal static ConfigEntry<KeyboardShortcut> HotkeyGlassDamage;
        internal static ConfigEntry<KeyboardShortcut> HotkeyScratches;
        internal static ConfigEntry<KeyboardShortcut> HotkeyBlur;
        internal static ConfigEntry<KeyboardShortcut> HotkeyDistortion;

        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("VisorEffectManager: Initializing...");

            InitConfiguration();
            new FaceShieldMaskPatch().Enable();
            new FaceShieldPatch().Enable();

            LogSource.LogInfo("VisorEffectManager: Initialization complete");
            LogKeyboardShortcuts();
        }

        private void Update()
        {
            if (HotkeyGlassDamage.Value.IsDown())
            {
                ToggleSetting(RemoveGlassDamage, "Glass Damage");
            }
            else if (HotkeyScratches.Value.IsDown())
            {
                ToggleSetting(RemoveScratches, "Scratches");
            }
            else if (HotkeyBlur.Value.IsDown())
            {
                ToggleSetting(RemoveBlur, "Blur");
            }
            else if (HotkeyDistortion.Value.IsDown())
            {
                ToggleSetting(RemoveDistortion, "Distortion");
            }
        }

        private void LogKeyboardShortcuts()
        {
            LogSource.LogInfo("VisorEffectManager: Keyboard shortcuts configured:");
            LogSource.LogInfo($"  Glass Damage: {HotkeyGlassDamage.Value}");
            LogSource.LogInfo($"  Scratches: {HotkeyScratches.Value}");
            LogSource.LogInfo($"  Blur: {HotkeyBlur.Value}");
            LogSource.LogInfo($"  Distortion: {HotkeyDistortion.Value}");
        }

        private void ToggleSetting(ConfigEntry<bool> setting, string settingName)
        {
            if (setting == null)
            {
                return;
            }

            try
            {
                bool newValue = !setting.Value;
                setting.Value = newValue;

                LogSource.LogInfo($"VisorEffectManager: {settingName} {(newValue ? "enabled" : "disabled")} via keyboard shortcut");
            }
            catch (Exception ex)
            {
                LogSource.LogError($"VisorEffectManager: Error toggling {settingName} - {ex.Message}");
            }
        }

        private void InitConfiguration()
        {
            // Configurações de texturas
            RemoveGlassDamage = Config.Bind(
                MainSectionName,
                "1. Remove Glass Damage",
                true,
                "Remove damage texture from visors.");

            RemoveScratches = Config.Bind(
                MainSectionName,
                "2. Remove Scratches",
                true,
                "Remove scratches texture from visors.");

            RemoveBlur = Config.Bind(
                MainSectionName,
                "3. Remove Blur",
                true,
                "Remove blur effect from visors.");

            RemoveDistortion = Config.Bind(
                MainSectionName,
                "4. Remove Distortion",
                true,
                "Remove distortion effect from visors.");

            // Configurações de teclas de atalho
            HotkeyGlassDamage = Config.Bind(
                KeyboardSectionName,
                "1. Hotkey - Glass Damage",
                new KeyboardShortcut(KeyCode.Alpha1, KeyCode.RightControl),
                "Keyboard shortcut to toggle Glass Damage removal.");

            HotkeyScratches = Config.Bind(
                KeyboardSectionName,
                "2. Hotkey - Scratches",
                new KeyboardShortcut(KeyCode.Alpha2, KeyCode.RightControl),
                "Keyboard shortcut to toggle Scratches removal.");

            HotkeyBlur = Config.Bind(
                KeyboardSectionName,
                "3. Hotkey - Blur",
                new KeyboardShortcut(KeyCode.Alpha3, KeyCode.RightControl),
                "Keyboard shortcut to toggle Blur removal.");

            HotkeyDistortion = Config.Bind(
                KeyboardSectionName,
                "4. Hotkey - Distortion",
                new KeyboardShortcut(KeyCode.Alpha4, KeyCode.RightControl),
                "Keyboard shortcut to toggle Distortion removal.");

            LogSource.LogInfo("VisorEffectManager: Configuration initialized with default values (all enabled)");
        }
    }
}
