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

public class glideSignalGenerator : signalGenerator {

  public signalGenerator incoming;
  public bool active = true;
  public float time = 1f;
  private float current = 0f;

  [Range(0.01f, 10)]
  public float power = 0.05f;
  [Range(1, 1000)]
  public float div = 150f;

  public override void processBuffer(float[] buffer, double dspTime, int channels) {
    
    if (incoming == null) // DC Gen
    {
      for (int n = 0; n < buffer.Length; n += 1)
      {
        buffer[n] = time * 2 - 1; // normalize to [-1,1]
      }
    } else { // Glide

      incoming.processBuffer(buffer, dspTime, channels);

      for (int n = 0; n < buffer.Length; n += 2)
      {
        current += (buffer[n] - current) * (1.001f - Mathf.Pow(time, power) ) / div;
        buffer[n] = buffer[n + 1] = current;
      }
    }


  }
}
