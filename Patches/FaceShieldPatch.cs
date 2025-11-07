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
            if (VisorEffectManager.RemoveGlassDamage.Value)
            {
                material.SetTexture("_GlassDamageTex", null);
            }

            if (VisorEffectManager.RemoveScratches.Value)
            {
                material.SetTexture("_ScratchesTex", null);
            }

            if (VisorEffectManager.RemoveBlur.Value)
            {
                material.SetTexture("_BlurMask", null);
            }

            if (VisorEffectManager.RemoveDistortion.Value)
            {
                material.SetTexture("_DistortMask", null);
            }

            if (VisorEffectManager.RemoveMask.Value)
            {
                material.SetTexture("_Mask", null);
            }
        }

    }
}
