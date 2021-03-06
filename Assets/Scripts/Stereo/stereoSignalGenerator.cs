// Copyright 2017 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class stereoSignalGenerator : signalGenerator {

  public signalGenerator incoming;
  public bool active = true;
  public float amp = 0f;

  float fader = 0f;

  public bool isLeft = true;
  
  // how to avoid double calculation of the channels? store buffer and check dsptime?

  public override void processBuffer(float[] buffer, double dspTime, int channels) {

    if (incoming != null) incoming.processBuffer(buffer, dspTime, channels);

    for(int n = 0; n < buffer.Length; n+=2) 
    {
      //fader = Mathf.Clamp01(fader + ( (!active || incoming == null) ? -0.005f : 0.005f)); // fade out or in
      if(isLeft){
        buffer[n] = buffer[n];
        buffer[n+1] = buffer[n];
      } else {
        buffer[n] = buffer[n + 1];
        buffer[n + 1] = buffer[n + 1];
      }
    }
  }
}
