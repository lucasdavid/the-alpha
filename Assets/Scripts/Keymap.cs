using UnityEngine;
using System.Collections;

public abstract class Keymap {

    public abstract class Select {
        public static KeyCode Alpha = KeyCode.Alpha1;
        public static KeyCode Type0 = KeyCode.Alpha2;
        public static KeyCode Type1 = KeyCode.Alpha3;
        public static KeyCode Type2 = KeyCode.Alpha4;
        public static KeyCode Type3 = KeyCode.Alpha5;
        public static KeyCode All   = KeyCode.Alpha6;
    }

    public abstract class Action {
        public static KeyCode Unhold = KeyCode.U;
        public static KeyCode Hold = KeyCode.H;
        public static KeyCode Resume = KeyCode.M;
        public static KeyCode Stop = KeyCode.S;
    }

}
