    internal class AndroidGamePad
    {
        public InputDevice _device;
        public int _deviceId;
        public string _descriptor;
        public bool _isConnected;
        public bool DPadButtons;

        public Buttons _buttons;
        public float _leftTrigger, _rightTrigger;
        public Vector2 _leftStick, _rightStick;

        public readonly GamePadCapabilities _capabilities;

        public AndroidGamePad(InputDevice device)
        {
            _device = device;
            _deviceId = device.Id;
            _descriptor = device.Descriptor;
            _isConnected = true;

            _capabilities = CapabilitiesOfDevice(device);
        }

        private static GamePadCapabilities CapabilitiesOfDevice(InputDevice device)
        {
            var capabilities = new GamePadCapabilities();
            capabilities.IsConnected = true;
            capabilities.GamePadType = GamePadType.GamePad;
            capabilities.HasLeftVibrationMotor = capabilities.HasRightVibrationMotor = device.Vibrator.HasVibrator;

            // build out supported inputs from what the gamepad exposes
            int[] keyMap = new int[16];
            keyMap[0] = (int)Keycode.ButtonA;
            keyMap[1] = (int)Keycode.ButtonB;
            keyMap[2] = (int)Keycode.ButtonX;
            keyMap[3] = (int)Keycode.ButtonY;

            keyMap[4] = (int)Keycode.ButtonThumbl;
            keyMap[5] = (int)Keycode.ButtonThumbr;

            keyMap[6] = (int)Keycode.ButtonL1;
            keyMap[7] = (int)Keycode.ButtonR1;
            keyMap[8] = (int)Keycode.ButtonL2;
            keyMap[9] = (int)Keycode.ButtonR2;

            keyMap[10] = (int)Keycode.DpadDown;
            keyMap[11] = (int)Keycode.DpadLeft;
            keyMap[12] = (int)Keycode.DpadRight;
            keyMap[13] = (int)Keycode.DpadUp;

            keyMap[14] = (int)Keycode.ButtonStart;
            keyMap[15] = (int)Keycode.Back;

            // get a bool[] with indices matching the keyMap
            bool[] hasMap = new bool[16];
            // HasKeys() was defined in Kitkat / API19 / Android 4.4
            if (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Kitkat)
            {
                var keyMap2 = new Keycode[keyMap.Length];
                for(int i=0; i<keyMap.Length;i++)
                    keyMap2[i] = (Keycode)keyMap[i];
                hasMap = KeyCharacterMap.DeviceHasKeys(keyMap2);
            }
            else
            {
                hasMap = device.HasKeys(keyMap);
            }

            capabilities.HasAButton = hasMap[0];
            capabilities.HasBButton = hasMap[1];
            capabilities.HasXButton = hasMap[2];
            capabilities.HasYButton = hasMap[3];

            // we only check for the thumb button to see if we have 2 thumbsticks
            // if ever a controller doesn't support buttons on the thumbsticks,
            // this will need fixing
            capabilities.HasLeftXThumbStick = hasMap[4];
            capabilities.HasLeftYThumbStick = hasMap[4];
            capabilities.HasRightXThumbStick = hasMap[5];
            capabilities.HasRightYThumbStick = hasMap[5];

            capabilities.HasLeftShoulderButton = hasMap[6];
            capabilities.HasRightShoulderButton = hasMap[7];
            capabilities.HasLeftTrigger = hasMap[8];
            capabilities.HasRightTrigger = hasMap[9];

            capabilities.HasDPadDownButton = hasMap[10];
            capabilities.HasDPadLeftButton = hasMap[11];
            capabilities.HasDPadRightButton = hasMap[12];
            capabilities.HasDPadUpButton = hasMap[13];

            capabilities.HasStartButton = hasMap[14];
            capabilities.HasBackButton = hasMap[15];

            return capabilities;
        }
    }