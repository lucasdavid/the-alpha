using UnityEngine;

public abstract class Keymap {

    public abstract class select {
        public static KeyCode Alpha = KeyCode.Alpha1;
        public static KeyCode All   = KeyCode.Alpha2;
        public static KeyCode Type0 = KeyCode.Alpha3;
        public static KeyCode Type1 = KeyCode.Alpha4;
        public static KeyCode Type2 = KeyCode.Alpha5;
        public static KeyCode Type3 = KeyCode.Alpha6;
        public static KeyCode AllOfAKind = KeyCode.LeftControl;
        public static KeyCode Alternative = KeyCode.LeftShift;
    }

    public abstract class action {
        public static KeyCode Unhold = KeyCode.U;
        public static KeyCode Hold   = KeyCode.H;
        public static KeyCode Resume = KeyCode.M;
        public static KeyCode Stop   = KeyCode.S;
    }

    public abstract class spawn {
        public static KeyCode Basic   = KeyCode.Alpha1;
        public static KeyCode Scout   = KeyCode.Alpha2;
        public static KeyCode Soldier = KeyCode.Alpha3;
        public static KeyCode Tank    = KeyCode.Alpha4;
    }

    public abstract class camera {
        public static KeyCode Alternative = KeyCode.LeftControl;
    }

    public abstract class shop {
        public static KeyCode Interact = KeyCode.O;
    }

}
