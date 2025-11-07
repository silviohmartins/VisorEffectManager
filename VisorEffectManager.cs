using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;
using VisorEffectManager.Patches;

namespace VisorEffectManager
{
    [BepInPlugin("com.jero.VisorEffectManager", "VisorEffectManager", "1.0.1")]
    public class VisorEffectManager : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;

        private const string MainSectionName = "Face Shield Settings";

        // Configurações para cada textura
        public static ConfigEntry<bool> RemoveGlassDamage;
        public static ConfigEntry<bool> RemoveScratches;
        public static ConfigEntry<bool> RemoveBlur;
        public static ConfigEntry<bool> RemoveDistortion;
        public static ConfigEntry<bool> RemoveMask;

        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("VisorEffectManager: Initializing...");

            InitConfiguration();
            new FaceShieldPatch().Enable();

            LogSource.LogInfo("VisorEffectManager: Initialization complete");
        }

        private void InitConfiguration()
        {
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

            RemoveMask = Config.Bind(
                MainSectionName,
                "5. Remove Mask",
                true,
                "Remove mask texture from visors.");

            // Adiciona eventos para atualizar visores quando configuração mudar
            RemoveGlassDamage.SettingChanged += OnSettingChanged;
            RemoveScratches.SettingChanged += OnSettingChanged;
            RemoveBlur.SettingChanged += OnSettingChanged;
            RemoveDistortion.SettingChanged += OnSettingChanged;
            RemoveMask.SettingChanged += OnSettingChanged;

            LogSource.LogInfo("VisorEffectManager: Configuration initialized with default values (all enabled)");
        }

        private void OnSettingChanged(object sender, EventArgs e)
        {
            LogSource.LogInfo("VisorEffectManager: Configuration changed, updating all visor effects");
            // Atualiza todos os visores ativos quando qualquer configuração mudar
            UpdateAllVisorEffects();
        }

        private void UpdateAllVisorEffects()
        {
            try
            {
                // Busca todas as instâncias ativas de VisorEffect na cena
                VisorEffect[] visorEffects = UnityEngine.Object.FindObjectsOfType<VisorEffect>();

                if (visorEffects == null || visorEffects.Length == 0)
                {
                    return;
                }

                // Obtém o método method_2 via reflection (cache uma vez)
                MethodInfo method2 = typeof(VisorEffect).GetMethod("method_2", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

                if (method2 == null)
                {
                    LogSource.LogWarning("UpdateAllVisorEffects: Could not find method_2 in VisorEffect class.");
                    return;
                }

                int updatedCount = 0;
                // Chama method_2() em cada instância para forçar atualização
                foreach (VisorEffect visorEffect in visorEffects)
                {
                    if (visorEffect != null && visorEffect.enabled)
                    {
                        try
                        {
                            method2.Invoke(visorEffect, null);
                            updatedCount++;
                        }
                        catch (Exception ex)
                        {
                            LogSource.LogWarning($"UpdateAllVisorEffects: Error updating visor effect instance - {ex.Message}");
                        }
                    }
                }

                if (updatedCount > 0)
                {
                    LogSource.LogInfo($"UpdateAllVisorEffects: Updated {updatedCount} visor effect(s)");
                }
            }
            catch (Exception ex)
            {
                LogSource.LogError($"UpdateAllVisorEffects: Error updating visor effects - {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
