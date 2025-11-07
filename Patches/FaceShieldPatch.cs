using System.Reflection;
using SPT.Reflection.Patching;
using UnityEngine;
using VisorEffectManager;

namespace VisorEffectManager.Patches
{
    internal class FaceShieldPatch : ModulePatch
    {

        protected override MethodBase GetTargetMethod()
        {
            return typeof(VisorEffect).GetMethod("method_2", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        }

        [PatchPostfix]
        static void Postfix(VisorEffect __instance)
        {
            Material material = __instance.method_4();

            // Remove texturas baseado nas configurações
            if (Plugin.RemoveGlassDamage.Value)
            {
                material.SetTexture("_GlassDamageTex", null);
            }

            if (Plugin.RemoveScratches.Value)
            {
                material.SetTexture("_ScratchesTex", null);
            }

            if (Plugin.RemoveBlur.Value)
            {
                material.SetTexture("_BlurMask", null);
            }

            if (Plugin.RemoveDistortion.Value)
            {
                material.SetTexture("_DistortMask", null);
            }

            if (Plugin.RemoveMask.Value)
            {
                material.SetTexture("_Mask", null);
            }
        }

    }
}
