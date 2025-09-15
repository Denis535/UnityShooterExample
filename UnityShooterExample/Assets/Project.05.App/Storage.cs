#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;

    // https://docs.unity3d.com/Manual/CommandLineArguments.html
    // https://docs.unity3d.com/Manual/EditorCommandLineArguments.html
    // https://docs.unity3d.com/Manual/PlayerCommandLineArguments.html
    public partial class Storage : StorageBase {

        public string? Profile { get; }

        internal Storage() {
            //foreach (var (key, values) in CLI.GetKeyValues( Environment.GetCommandLineArgs() )) {
            //    if (key != null) {
            //        Debug.Log( key + ": " + string.Join( ", ", values ) );
            //    } else {
            //        Debug.Log( string.Join( ", ", values ) );
            //    }
            //}
            this.Profile = CommandLineArguments.GetValues( Environment.GetCommandLineArgs(), "--profile" )?.First();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
