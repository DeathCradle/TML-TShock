﻿using OTAPI.Patcher.Engine.Extensions;
using OTAPI.Patcher.Engine.Modification;

namespace OTAPI.Patcher.Engine.Modifications.Hooks.Player
{
	public class Save : ModificationBase
	{
		public override System.Collections.Generic.IEnumerable<string> AssemblyTargets => new[]
		{
			"Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null"
		};
		public override string Description => "Hooking Player.SavePlayer";
		public override void Run()
		{
			var vanilla = this.Method(() => Terraria.Player.SavePlayer(null, false));

			bool tmp = false;
			var cbkBegin = this.SourceDefinition.MainModule.Import(
				this.Method(() => OTAPI.Callbacks.Terraria.Player.SavePlayerBegin(null, ref tmp))
			);

			var cbkEnd = this.SourceDefinition.MainModule.Import(
				this.Method(() => OTAPI.Callbacks.Terraria.Player.SavePlayerEnd(null, tmp))
			);

			vanilla.Wrap
			(
				beginCallback: cbkBegin,
				endCallback: cbkEnd,
				beginIsCancellable: true,
				noEndHandling: false,
				allowCallbackInstance: false
			);
		}
	}
}
