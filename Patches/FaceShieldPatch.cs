using System.Reflection;
using System.Runtime.CompilerServices;
using SPT.Reflection.Patching;
using UnityEngine;

namespace VisorEffectManager.Patches
{
    /// <summary>
    /// Records the mask set on each VisorEffect. With the Narrow mask the game clears every
    /// texture on purpose, so it must not be restored when a removal setting is disabled.
    /// </summary>
    internal class FaceShieldMaskPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(VisorEffect).GetMethod(nameof(VisorEffect.SetMask), BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        }

        [PatchPostfix]
        static void Postfix(VisorEffect __instance, VisorEffect.EMask mask)
        {
            VisorTextures.SetMask(__instance, mask);
        }
    }

    /// <summary>
    /// Applies the settings right before the visor is rendered. SetMask only runs when the helmet
    /// slot changes, so applying here also covers swapping the visor and toggling via hotkeys.
    /// </summary>
    internal class FaceShieldPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(VisorEffect).GetMethod(nameof(VisorEffect.OnRenderImageImpl), BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        }

        [PatchPrefix]
        static void Prefix(VisorEffect __instance)
        {
            // Same threshold the game uses to skip the effect entirely
            if (__instance.Intensity < 0.01f)
            {
                return;
            }

            VisorTextures.Apply(__instance);
        }
    }

    internal static class VisorTextures
    {
        private static readonly object Marker = new object();
        private static readonly ConditionalWeakTable<VisorEffect, object> NarrowMask = new ConditionalWeakTable<VisorEffect, object>();
        private static bool _errorLogged;

        internal static void SetMask(VisorEffect visor, VisorEffect.EMask mask)
        {
            NarrowMask.Remove(visor);
            if (mask == VisorEffect.EMask.Narrow)
            {
                NarrowMask.Add(visor, Marker);
            }
        }

        internal static void Apply(VisorEffect visor)
        {
            try
            {
                if (VisorEffectManager.RemoveGlassDamage == null ||
                    VisorEffectManager.RemoveScratches == null ||
                    VisorEffectManager.RemoveBlur == null ||
                    VisorEffectManager.RemoveDistortion == null)
                {
                    return;
                }

                Material material = visor.GetMaterial();
                if (material == null)
                {
                    return;
                }

                bool narrow = NarrowMask.TryGetValue(visor, out _);

                // Removed effects get null; otherwise restore the game's default texture (see VisorEffect.SetDefault)
                material.SetTexture("_GlassDamageTex", VisorEffectManager.RemoveGlassDamage.Value || narrow ? null : visor.GetOrCreateGlassRenderTexture());
                material.SetTexture("_ScratchesTex", VisorEffectManager.RemoveScratches.Value || narrow ? null : visor.Scratches);
                material.SetTexture("_BlurMask", VisorEffectManager.RemoveBlur.Value || narrow ? null : visor.BlurMask);
                material.SetTexture("_DistortMask", VisorEffectManager.RemoveDistortion.Value || narrow ? null : visor.DistortMask);
            }
            catch (System.Exception ex)
            {
                // Runs every frame, so only log the first failure
                if (!_errorLogged)
                {
                    _errorLogged = true;
                    VisorEffectManager.LogSource?.LogError($"FaceShieldPatch: Error applying visor settings - {ex.Message}\n{ex.StackTrace}");
                }
            }
        }
    }
}
