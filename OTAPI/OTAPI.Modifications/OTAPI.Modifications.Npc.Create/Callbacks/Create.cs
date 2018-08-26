﻿namespace OTAPI.Callbacks.Terraria
{
    internal static partial class Npc
    {
        internal static global::Terraria.NPC Create
        (
            ref int index,
            ref int x,
            ref int y,
            ref int type,
            ref int start,
            ref float ai0,
            ref float ai1,
            ref float ai2,
            ref float ai3,
            ref int target
        )
        {
            var npc = Hooks.Npc.Create?.Invoke
            (
                ref index,
                ref x,
                ref y,
                ref type,
                ref start,
                ref ai0,
                ref ai1,
                ref ai2,
                ref ai3,
                ref target
            );

            //This hook requires a return value or it will crash.
            if (npc == null)
            {
                System.Diagnostics.Debug.WriteLine($"Index: {index}");
                npc = new global::Terraria.NPC();

                global::Terraria.Main.npc[index] = npc;
                global::Terraria.Main.npc[index].SetDefaults(type, -1f);
            }

            return npc;
        }
    }
}
