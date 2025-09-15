﻿#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;

    public partial class Storage {
        public class VideoSettings : StorageBase {

            private bool isVSync;

            public bool IsFullScreen {
                get => Screen.fullScreen;
                set {
                    Screen.fullScreen = value;
                }
            }
            public Resolution ScreenResolution {
                get => Screen.currentResolution;
                set {
                    Screen.SetResolution( value.width, value.height, Screen.fullScreenMode, value.refreshRateRatio );
                }
            }
            public Resolution[] ScreenResolutions {
                //get => Screen.resolutions.SkipWhile( i => i.width < 1000 ).Reverse().ToArray();
                get => Screen.resolutions.Reverse().ToArray();
            }
            public bool IsVSync {
                get => isVSync;
                set {
                    isVSync = value;
                    QualitySettings.vSyncCount = value == true ? 1 : 0;
                }
            }

            internal VideoSettings() {
                this.Load();
            }
            public override void Dispose() {
                base.Dispose();
            }

            public void Load() {
                this.IsVSync = PlayerPrefs2.GetBool( "VideoSettings.IsVSync", true );
            }
            public void Save() {
                PlayerPrefs2.SetBool( "VideoSettings.IsVSync", this.IsVSync );
            }

        }
    }
}
