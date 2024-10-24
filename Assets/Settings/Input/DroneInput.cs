//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Settings/Input/DroneInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @DroneInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DroneInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DroneInput"",
    ""maps"": [
        {
            ""name"": ""Drone"",
            ""id"": ""60e5e5b5-26b2-465e-9d34-2d1ed3069cee"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""ec7267c2-3f7d-4eb5-b42a-3d6b3d4b4907"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Throttle"",
                    ""type"": ""Value"",
                    ""id"": ""13b77d2d-ea02-4851-970a-caa396e325c6"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""745d1d44-92aa-4e60-84e4-19636e9e4810"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""0200cc7b-6a5e-4c7d-8499-3d6c3bfbed63"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e820d153-f18a-493c-8cab-6fc1162a39f0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f0de6fac-a74d-401c-b6f9-6f5aad310e9a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4f800b61-bf0d-4988-87a6-48ec69c9adb8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""60d8b90a-6fe4-4c3e-ba92-6fd2d9e4d607"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d4ec7f9-93b8-48f4-82f0-ee00763c7ca1"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63218f84-cd89-46fd-be8f-3fba5bc239c4"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ed32bec-82ed-424e-b808-c95482bdfef7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""60e1511c-0505-41ef-8d2f-b1cc8e60cc14"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""ae37f80e-ee3e-448f-a70d-f86b99e6aa01"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8d60ce4a-aefd-4fbc-9aee-971a915c8056"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""aec9bfa8-0fc3-4100-bce5-3a65c61274c3"",
            ""actions"": [
                {
                    ""name"": ""Options"",
                    ""type"": ""Button"",
                    ""id"": ""67b2e679-60d5-4b4e-89af-726d98a1630f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SettingMenuMove"",
                    ""type"": ""Value"",
                    ""id"": ""65cffcdb-9f20-4217-b623-d1d16df4eb3d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""VerticalMove"",
                    ""type"": ""Value"",
                    ""id"": ""4ca7aaf3-a1cc-4121-aa2a-b09825e50dcb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Close"",
                    ""type"": ""Button"",
                    ""id"": ""58aa42d8-5908-4661-afaf-cfb05862eb83"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Apply"",
                    ""type"": ""Button"",
                    ""id"": ""4a4aec9b-0e10-441d-9194-db2cc3ea3298"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""e6d2fb7e-1913-43d7-a43e-88965c76d341"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""20d3ce17-4ba5-4a07-88f5-a7a2d110b000"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Options"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""899f94cd-7733-4db2-bc37-0857f59197d7"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Options"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40a87539-8bc7-46a7-bdad-646d49ccf240"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SettingMenuMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53e1ae3c-9313-40f0-b74a-42559147569d"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""SettingMenuMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b789fc60-790c-41eb-93d9-5ae0c6c45777"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16f4fdb1-0994-4528-a145-25209f813d5e"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3faf0460-219f-4f43-a628-046b18da1431"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cb91665-4fab-4152-a578-fdb153bff59c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbbbb74e-d520-4703-a98f-d97d450d1e40"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Apply"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2a399e6-3798-474e-b707-0df257db6e19"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Apply"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17b8ce72-fece-409b-9414-f0ae1cb31c1c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea3c227e-214e-4844-95b0-f90664b82459"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Drone
        m_Drone = asset.FindActionMap("Drone", throwIfNotFound: true);
        m_Drone_Rotate = m_Drone.FindAction("Rotate", throwIfNotFound: true);
        m_Drone_Throttle = m_Drone.FindAction("Throttle", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Rotate = m_Camera.FindAction("Rotate", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Options = m_UI.FindAction("Options", throwIfNotFound: true);
        m_UI_SettingMenuMove = m_UI.FindAction("SettingMenuMove", throwIfNotFound: true);
        m_UI_VerticalMove = m_UI.FindAction("VerticalMove", throwIfNotFound: true);
        m_UI_Close = m_UI.FindAction("Close", throwIfNotFound: true);
        m_UI_Apply = m_UI.FindAction("Apply", throwIfNotFound: true);
        m_UI_Quit = m_UI.FindAction("Quit", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Drone
    private readonly InputActionMap m_Drone;
    private List<IDroneActions> m_DroneActionsCallbackInterfaces = new List<IDroneActions>();
    private readonly InputAction m_Drone_Rotate;
    private readonly InputAction m_Drone_Throttle;
    public struct DroneActions
    {
        private @DroneInput m_Wrapper;
        public DroneActions(@DroneInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Drone_Rotate;
        public InputAction @Throttle => m_Wrapper.m_Drone_Throttle;
        public InputActionMap Get() { return m_Wrapper.m_Drone; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DroneActions set) { return set.Get(); }
        public void AddCallbacks(IDroneActions instance)
        {
            if (instance == null || m_Wrapper.m_DroneActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DroneActionsCallbackInterfaces.Add(instance);
            @Rotate.started += instance.OnRotate;
            @Rotate.performed += instance.OnRotate;
            @Rotate.canceled += instance.OnRotate;
            @Throttle.started += instance.OnThrottle;
            @Throttle.performed += instance.OnThrottle;
            @Throttle.canceled += instance.OnThrottle;
        }

        private void UnregisterCallbacks(IDroneActions instance)
        {
            @Rotate.started -= instance.OnRotate;
            @Rotate.performed -= instance.OnRotate;
            @Rotate.canceled -= instance.OnRotate;
            @Throttle.started -= instance.OnThrottle;
            @Throttle.performed -= instance.OnThrottle;
            @Throttle.canceled -= instance.OnThrottle;
        }

        public void RemoveCallbacks(IDroneActions instance)
        {
            if (m_Wrapper.m_DroneActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDroneActions instance)
        {
            foreach (var item in m_Wrapper.m_DroneActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DroneActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DroneActions @Drone => new DroneActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private List<ICameraActions> m_CameraActionsCallbackInterfaces = new List<ICameraActions>();
    private readonly InputAction m_Camera_Rotate;
    public struct CameraActions
    {
        private @DroneInput m_Wrapper;
        public CameraActions(@DroneInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Camera_Rotate;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void AddCallbacks(ICameraActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraActionsCallbackInterfaces.Add(instance);
            @Rotate.started += instance.OnRotate;
            @Rotate.performed += instance.OnRotate;
            @Rotate.canceled += instance.OnRotate;
        }

        private void UnregisterCallbacks(ICameraActions instance)
        {
            @Rotate.started -= instance.OnRotate;
            @Rotate.performed -= instance.OnRotate;
            @Rotate.canceled -= instance.OnRotate;
        }

        public void RemoveCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Options;
    private readonly InputAction m_UI_SettingMenuMove;
    private readonly InputAction m_UI_VerticalMove;
    private readonly InputAction m_UI_Close;
    private readonly InputAction m_UI_Apply;
    private readonly InputAction m_UI_Quit;
    public struct UIActions
    {
        private @DroneInput m_Wrapper;
        public UIActions(@DroneInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Options => m_Wrapper.m_UI_Options;
        public InputAction @SettingMenuMove => m_Wrapper.m_UI_SettingMenuMove;
        public InputAction @VerticalMove => m_Wrapper.m_UI_VerticalMove;
        public InputAction @Close => m_Wrapper.m_UI_Close;
        public InputAction @Apply => m_Wrapper.m_UI_Apply;
        public InputAction @Quit => m_Wrapper.m_UI_Quit;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Options.started += instance.OnOptions;
            @Options.performed += instance.OnOptions;
            @Options.canceled += instance.OnOptions;
            @SettingMenuMove.started += instance.OnSettingMenuMove;
            @SettingMenuMove.performed += instance.OnSettingMenuMove;
            @SettingMenuMove.canceled += instance.OnSettingMenuMove;
            @VerticalMove.started += instance.OnVerticalMove;
            @VerticalMove.performed += instance.OnVerticalMove;
            @VerticalMove.canceled += instance.OnVerticalMove;
            @Close.started += instance.OnClose;
            @Close.performed += instance.OnClose;
            @Close.canceled += instance.OnClose;
            @Apply.started += instance.OnApply;
            @Apply.performed += instance.OnApply;
            @Apply.canceled += instance.OnApply;
            @Quit.started += instance.OnQuit;
            @Quit.performed += instance.OnQuit;
            @Quit.canceled += instance.OnQuit;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Options.started -= instance.OnOptions;
            @Options.performed -= instance.OnOptions;
            @Options.canceled -= instance.OnOptions;
            @SettingMenuMove.started -= instance.OnSettingMenuMove;
            @SettingMenuMove.performed -= instance.OnSettingMenuMove;
            @SettingMenuMove.canceled -= instance.OnSettingMenuMove;
            @VerticalMove.started -= instance.OnVerticalMove;
            @VerticalMove.performed -= instance.OnVerticalMove;
            @VerticalMove.canceled -= instance.OnVerticalMove;
            @Close.started -= instance.OnClose;
            @Close.performed -= instance.OnClose;
            @Close.canceled -= instance.OnClose;
            @Apply.started -= instance.OnApply;
            @Apply.performed -= instance.OnApply;
            @Apply.canceled -= instance.OnApply;
            @Quit.started -= instance.OnQuit;
            @Quit.performed -= instance.OnQuit;
            @Quit.canceled -= instance.OnQuit;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IDroneActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnThrottle(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnRotate(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnOptions(InputAction.CallbackContext context);
        void OnSettingMenuMove(InputAction.CallbackContext context);
        void OnVerticalMove(InputAction.CallbackContext context);
        void OnClose(InputAction.CallbackContext context);
        void OnApply(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
    }
}
