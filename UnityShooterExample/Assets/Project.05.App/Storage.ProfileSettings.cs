#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;

    public partial class Storage {
        public class ProfileSettings : StorageBase {

            private string name = default!;

            public string Name {
                get => name;
                set {
                    Assert.Argument.Message( $"Argument 'value' ({value}) is invalid" ).Valid( this.IsNameValid( value ) );
                    name = value;
                }
            }

            internal ProfileSettings() {
                this.Load();
            }
            public override void Dispose() {
                base.Dispose();
            }

            public void Load() {
                this.Name = PlayerPrefs.GetString( "ProfileSettings.Name", "Anonymous" );
            }
            public void Save() {
                PlayerPrefs.SetString( "ProfileSettings.Name", this.Name );
            }

            public bool IsNameValid(string? value) {
                return value != null &&
                    value.Length >= 3 &&
                    char.IsLetterOrDigit( value.First() ) &&
                    char.IsLetterOrDigit( value.Last() ) &&
                    value.All( i => char.IsLetterOrDigit( i ) || (i is ' ' or '_' or '-') );
            }

        }
    }
}
