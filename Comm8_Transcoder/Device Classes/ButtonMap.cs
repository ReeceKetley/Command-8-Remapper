using Midi.Devices;

namespace ReCommand8.Device_Classes
{
    public static class ButtonMap
    {
        public enum ButtonCode
        {
            UNDEFINED,
            RTZ,
            REW,
            FWD,
            STOP,
            PLAY,
            REC,
            PLUGIN,
            MIX,
            EDIT,
            LOOP_PLAY,
            LOOP_REC,
            QUICK_PUNCH,
            MEM_LOCK,
            MASTER_FADERS,
            DOWN,
            FOCUS,
            FLIP,
            LEFT,
            RIGHT,
            FADER_MUTE,
            UP,
            BANK,
            NUDGE,
            ZOOM,
            ENTER,
            UNDO,
            CHANNEL_RECORD,
            PAN_MASTER,
            CTRL_CLUTCH,
            WIN_KEY,
            SHIFT_ADD,
            ALT,
            DEFAULT,
            MON,
            E,
            INSERT,
            D,
            SEND,
            C,
            PAN,
            B,
            CONSOLE_VIEW,
            DISPLAY_MODE,
            MUTE_1,
            MUTE_2,
            MUTE_3,
            MUTE_4,
            MUTE_5,
            MUTE_6,
            MUTE_7,
            MUTE_8,
            SOLO_1,
            SOLO_2,
            SOLO_3,
            SOLO_4,
            SOLO_5,
            SOLO_6,
            SOLO_7,
            SOLO_8,
            SELECT_1,
            SELECT_2,
            SELECT_3,
            SELECT_4,
            SELECT_5,
            SELECT_6,
            SELECT_7,
            SELECT_8,
            EQ,
            DYNAMICS,
            MIX_INSERT,
            PAN_SEND_PRE,
            PAGE_L,
            PAGE_R,
            MASTER_BYPASS,
            ESC
        }

        public static ButtonCode ToButton(IOutputDevice device, string note, int velocity) /// TODO: these values will also activate the LEDs for the button
        {
            switch (note)
            {
                case "FSharpNeg1" when velocity == 78:
                    return ButtonCode.RTZ;
                case "GNeg1" when velocity == 78:
                    return ButtonCode.REW;
                case "GSharpNeg1" when velocity == 78:
                    return ButtonCode.FWD;
                case "ANeg1" when velocity == 78:
                    return ButtonCode.STOP;
                case "ASharpNeg1" when velocity == 78:
                    return ButtonCode.PLAY;
                case "BNeg1" when velocity == 78:
                    return ButtonCode.REC;
                case "CNeg1" when velocity == 78:
                    return ButtonCode.PLUGIN;
                case "CSharpNeg1" when velocity == 78:
                    return ButtonCode.MIX;
                case "DNeg1" when velocity == 78:
                    return ButtonCode.EDIT;
                case "DSharpNeg1" when velocity == 78:
                    return ButtonCode.LOOP_PLAY;
                case "ENeg1" when velocity == 78:
                    return ButtonCode.LOOP_REC;
                case "FNeg1" when velocity == 78:
                    return ButtonCode.QUICK_PUNCH;
                case "C0" when velocity == 78:
                    return ButtonCode.MEM_LOCK;
                case "CSharpNeg1" when velocity == 77:
                    return ButtonCode.MASTER_FADERS;
                case "GSharpNeg1" when velocity == 77:
                    return ButtonCode.DOWN;
                case "CSharp0" when velocity == 77:
                    return ButtonCode.FOCUS;
                case "CNeg1" when velocity == 77:
                    return ButtonCode.FLIP;
                case "FNeg1" when velocity == 77:
                    return ButtonCode.LEFT;
                case "FSharpNeg1" when velocity == 77:
                    return ButtonCode.RIGHT;
                case "C0" when velocity == 77:
                    return ButtonCode.FADER_MUTE;
                case "GNeg1" when velocity == 77:
                    return ButtonCode.UP;
                case "DNeg1" when velocity == 77:
                    return ButtonCode.BANK;
                case "DSharpNeg1" when velocity == 77:
                    return ButtonCode.NUDGE;
                case "ENeg1" when velocity == 77:
                    return ButtonCode.ZOOM;
                case "CNeg1" when velocity == 76:
                    return ButtonCode.ENTER;
                case "CSharpNeg1" when velocity == 76:
                    return ButtonCode.UNDO;
                case "DSharpNeg1" when velocity == 76:
                    return ButtonCode.CHANNEL_RECORD;
                case "ENeg1" when velocity == 76:
                    return ButtonCode.PAN_MASTER;
                case "ASharpNeg1" when velocity == 74:
                    return ButtonCode.CTRL_CLUTCH;
                case "BNeg1" when velocity == 74:
                    return ButtonCode.WIN_KEY;
                case "GSharpNeg1" when velocity == 74:
                    return ButtonCode.SHIFT_ADD;
                case "ANeg1" when velocity == 74:
                    return ButtonCode.ALT;
                case "C0" when velocity == 74:
                    return ButtonCode.DEFAULT;
                case "CSharp0" when velocity == 74:
                    return ButtonCode.MON;
                case "GNeg1" when velocity == 74:
                    return ButtonCode.E;
                case "DNeg1" when velocity == 74:
                    return ButtonCode.INSERT;
                case "FSharpNeg1" when velocity == 74:
                    return ButtonCode.D;
                case "CSharpNeg1" when velocity == 74:
                    return ButtonCode.SEND;
                case "FNeg1" when velocity == 74:
                    return ButtonCode.C;
                case "CNeg1" when velocity == 74:
                    return ButtonCode.PAN;
                case "ENeg1" when velocity == 74:
                    return ButtonCode.B;
                case "DSharpNeg1" when velocity == 74:
                    return ButtonCode.CONSOLE_VIEW;
                case "GSharpNeg1" when velocity == 75:
                    return ButtonCode.DISPLAY_MODE;
                case "DSharpNeg1" when velocity == 64:
                    return ButtonCode.MUTE_1;
                case "DSharpNeg1" when velocity == 65:
                    return ButtonCode.MUTE_2;
                case "DSharpNeg1" when velocity == 66:
                    return ButtonCode.MUTE_3;
                case "DSharpNeg1" when velocity == 67:
                    return ButtonCode.MUTE_4;
                case "DSharpNeg1" when velocity == 68:
                    return ButtonCode.MUTE_5;
                case "DSharpNeg1" when velocity == 69:
                    return ButtonCode.MUTE_6;
                case "DSharpNeg1" when velocity == 70:
                    return ButtonCode.MUTE_7;
                case "DSharpNeg1" when velocity == 71:
                    return ButtonCode.MUTE_8;
                case "DNeg1" when velocity == 64:
                    return ButtonCode.SOLO_1;
                case "DNeg1" when velocity == 65:
                    return ButtonCode.SOLO_2;
                case "DNeg1" when velocity == 66:
                    return ButtonCode.SOLO_3;
                case "DNeg1" when velocity == 67:
                    return ButtonCode.SOLO_4;
                case "DNeg1" when velocity == 68:
                    return ButtonCode.SOLO_5;
                case "DNeg1" when velocity == 69:
                    return ButtonCode.SOLO_6;
                case "DNeg1" when velocity == 70:
                    return ButtonCode.SOLO_7;
                case "DNeg1" when velocity == 71:
                    return ButtonCode.SOLO_8;
                case "CNeg1" when velocity == 64:
                    return ButtonCode.SELECT_1;
                case "CNeg1" when velocity == 65:
                    return ButtonCode.SELECT_2;
                case "CNeg1" when velocity == 66:
                    return ButtonCode.SELECT_3;
                case "CNeg1" when velocity == 67:
                    return ButtonCode.SELECT_4;
                case "CNeg1" when velocity == 68:
                    return ButtonCode.SELECT_5;
                case "CNeg1" when velocity == 69:
                    return ButtonCode.SELECT_6;
                case "CNeg1" when velocity == 70:
                    return ButtonCode.SELECT_7;
                case "CNeg1" when velocity == 71:
                    return ButtonCode.SELECT_8;
                case "CNeg1" when velocity == 75:
                    return ButtonCode.EQ;
                case "CSharpNeg1" when velocity == 75:
                    return ButtonCode.DYNAMICS;
                case "DNeg1" when velocity == 75:
                    return ButtonCode.MIX_INSERT;
                case "DSharpNeg1" when velocity == 75:
                    return ButtonCode.PAN_SEND_PRE;
                case "ENeg1" when velocity == 75:
                    return ButtonCode.PAGE_L;
                case "FNeg1" when velocity == 75:
                    return ButtonCode.PAGE_R;
                case "FSharpNeg1" when velocity == 75:
                    return ButtonCode.MASTER_BYPASS;
                case "GNeg1" when velocity == 75:
                    return ButtonCode.ESC;
            }

            return ButtonCode.UNDEFINED;
        }
    }
}
