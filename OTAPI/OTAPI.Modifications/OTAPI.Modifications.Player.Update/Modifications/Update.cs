﻿using OTAPI.Patcher.Engine.Extensions;
using OTAPI.Patcher.Engine.Modification;

namespace OTAPI.Patcher.Engine.Modifications.Hooks.Player
{
	public class Update : ModificationBase
	{
		public override System.Collections.Generic.IEnumerable<string> AssemblyTargets => new[]
		{
			"TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null",
			"Terraria, Version=1.3.4.4, Culture=neutral, PublicKeyToken=null",
			"Terraria, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null","TerrariaServer, Version=1.3.5.1, Culture=neutral, PublicKeyToken=null" // TML
		};
		public override string Description => "Hooking Player.Update";
		public override void Run()
		{
			//var vanilla = this.Method(() => (new Terraria.Player()).Update(0));
			var vanilla = this.SourceDefinition.Type(typeof(Terraria.Player).FullName).Method("Update");

			int tmp = 0;
			var cbkBegin = this.SourceDefinition.MainModule.Import(
				this.Method(() => OTAPI.Callbacks.Terraria.Player.UpdateBegin(null, ref tmp))
			);

			var cbkEnd = this.SourceDefinition.MainModule.Import(
				this.Method(() => OTAPI.Callbacks.Terraria.Player.UpdateEnd(null,   tmp))
			);

			vanilla.Wrap
			(
				beginCallback: cbkBegin,
				endCallback: cbkEnd,
				beginIsCancellable: true,
				noEndHandling: false,
				allowCallbackInstance: true
			);
		}
	}
}
