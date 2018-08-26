﻿namespace OTAPI.Callbacks.Terraria
{
    internal partial class Console
    {
        internal static void Write(string message)
        {
            if (Hooks.Console.Write?.Invoke(message) == HookResult.Cancel)
                return;

            System.Console.Write(message);
        }
    }
}
