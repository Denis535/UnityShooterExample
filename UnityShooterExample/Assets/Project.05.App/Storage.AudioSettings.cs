#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Audio;
    using UnityEngine.Framework;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public partial class Storage {
        public class AudioSettings : StorageBase {

            private float masterVolume;
            private float musicVolume;
            private float sfxVolume;
            private float gameVolume;

            private AudioMixer AudioMixer { get; }
            public float MasterVolume {
                get => masterVolume;
                set {
                    masterVolume = value;
                    SetVolume( this.AudioMixer, "MasterVolume", value );
                }
            }
            public float MusicVolume {
                get => musicVolume;
                set {
                    musicVolume = value;
                    SetVolume( this.AudioMixer, "MusicVolume", value );
                }
            }
            public float SfxVolume {
                get => sfxVolume;
                set {
                    sfxVolume = value;
                    SetVolume( this.AudioMixer, "SfxVolume", value );
                }
            }
            public float GameVolume {
                get => gameVolume;
                set {
                    gameVolume = value;
                    SetVolume( this.AudioMixer, "GameVolume", value );
                }
            }

            internal AudioSettings() {
                this.AudioMixer = Addressables.LoadAssetAsync<AudioMixer>( R.UnityEngine.Audio.Value_AudioMixer ).GetResult();
                this.Load();
            }
            public override void Dispose() {
                Addressables.Release( this.AudioMixer );
                base.Dispose();
            }

            public void Load() {
                this.MasterVolume = PlayerPrefs.GetFloat( "AudioSettings.MasterVolume", 0.5f );
                this.MusicVolume = PlayerPrefs.GetFloat( "AudioSettings.MusicVolume", 0.5f );
                this.SfxVolume = PlayerPrefs.GetFloat( "AudioSettings.SfxVolume", 0.5f );
                this.GameVolume = PlayerPrefs.GetFloat( "AudioSettings.GameVolume", 0.5f );
            }
            public void Save() {
                PlayerPrefs.SetFloat( "AudioSettings.MasterVolume", this.MasterVolume );
                PlayerPrefs.SetFloat( "AudioSettings.MusicVolume", this.MusicVolume );
                PlayerPrefs.SetFloat( "AudioSettings.SfxVolume", this.SfxVolume );
                PlayerPrefs.SetFloat( "AudioSettings.GameVolume", this.GameVolume );
            }

            // Helpers
            private static void SetVolume(AudioMixer mixer, string name, float value) {
                Assert.Argument.Message( $"Volume {value} must be in [0-1] range" ).InRange( value is >= 0 and <= 1 );
                var isSucceeded = mixer.SetFloat( name, ToDecibels( value ) );
                if (!isSucceeded) throw Exceptions.Internal.Exception( $"Volume {name} was not set" );
            }
            private static float ToDecibels(float value) {
                value = Mathf.Clamp( value, 0.0001f, 1 );
                return Mathf.Log10( value ) * 20f;
            }

        }
    }
}
