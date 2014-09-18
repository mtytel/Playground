﻿/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Localization : MonoBehaviour {

#if UNITY_EDITOR

  private bool IsTraditionalChinese() {
    return false;
  }

#elif UNITY_STANDALONE_WIN

  [DllImport("KERNEL32.DLL")]
  private static extern System.UInt16 GetSystemDefaultUILanguage();

  private bool IsTraditionalChinese() {
    System.UInt16 identifier = GetSystemDefaultUILanguage();
    return identifier == 0x0c04 || // Hong Kong
           identifier == 0x1404 || // Macao
           identifier == 0x0404 || identifier == 1028;   // Taiwan
  }

#elif UNITY_STANDALONE_OSX

  [DllImport("CheckChineseTraditional")]
  private static extern bool IsTraditionalChinese();

#else

  private bool IsTraditionalChinese() {
    return false;
  }

#endif

  public string english;
  public string chineseSimplified;
  public string chineseTraditional;
  public string french;
  public string german;
  public string italian;
  public string japanese;
  public string korean;
  public string portuguese;
  public string spanish;

  public void Start() {
    GUIText text = GetComponent<GUIText>();

    switch (Application.systemLanguage) {
      case SystemLanguage.English:
        text.text = english;
        break;
      case SystemLanguage.Chinese:
        if (IsTraditionalChinese())
          text.text = chineseTraditional;
        else
          text.text = chineseSimplified;
        break;
      case SystemLanguage.French:
        text.text = french;
        break;
      case SystemLanguage.German:
        text.text = german;
        break;
      case SystemLanguage.Italian:
        text.text = italian;
        break;
      case SystemLanguage.Japanese:
        text.text = japanese;
        break;
      case SystemLanguage.Korean:
        text.text = korean;
        break;
      case SystemLanguage.Portuguese:
        text.text = portuguese;
        break;
      case SystemLanguage.Spanish:
        text.text = spanish;
        break;
      default:
        text.text = english;
        break;
    }
  }
}

