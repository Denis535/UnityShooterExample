#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class Storage {
        public class Preferences : DisposableBase {

            internal Preferences() {
                this.Load();
            }
            public override void Dispose() {
                base.Dispose();
            }

            public void Load() {
            }
            public void Save() {
            }

        }
    }
}
