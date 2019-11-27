// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""15888a4d-f094-42df-8def-2af21f2cf0a8"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""4dbaa254-f1f4-400a-8387-7a647342cac4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""70454bc4-e8e1-4eba-89f7-c9916880165e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Button"",
                    ""id"": ""14ee231d-961d-4823-ae8b-71902cac7462"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""f93f044f-7a16-4a2f-b0e1-e1d442196602"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWeaponLeft"",
                    ""type"": ""Button"",
                    ""id"": ""c88bcc99-42ba-4370-ac2e-b311156a430c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWeaponRight"",
                    ""type"": ""Button"",
                    ""id"": ""86eaeeec-9afa-4f5b-a7b8-9d0ca3238c58"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToPistol"",
                    ""type"": ""Button"",
                    ""id"": ""afab87e6-91d6-44c2-b319-550de8dc6607"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToRifle"",
                    ""type"": ""Button"",
                    ""id"": ""3f7766fb-cdd7-48e4-af2e-1640854c359e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToShotgun"",
                    ""type"": ""Button"",
                    ""id"": ""e10d9433-6f9a-4519-96e1-b1ccc96cfe6f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5f841341-73b2-444b-b92f-31a3b2f3e5a0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc2ecc74-86b1-4d7b-9975-c43bd91d5e84"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""e1354d14-a94d-4a70-b20f-9224622e3eb0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""742865b4-18f6-40d6-af27-1019d7cfb5af"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a7cdbb07-1096-49ba-b54f-a09b7a3306dd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c7c0c40d-4315-4d58-afea-3a25887b61cd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7656f054-09f6-46fc-bf98-c2caa55258f8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""98569d8b-9528-473a-8f12-40c9a89e1c2d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f6cbc780-b69d-4da1-9e45-28b4b2cd8ec0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""498bd2f6-9507-498d-8daf-dac5bcab893e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""78173c5f-5e76-4214-8fe5-3d747fd6c48b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b4b34852-e0c0-4c0b-a427-bd14bcfdfc2a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""66920c19-210a-4aec-a6cc-53512358c566"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95bc5213-8d3c-4ed6-90fd-ff2eee8f3b0f"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44e31f00-5566-4540-9c6d-a34ca7c6864f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40af566a-648c-4bb1-b655-bec68ca2306a"",
                    ""path"": ""<Keyboard>/leftBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""SwitchWeaponLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a40f207-9a30-4208-9c73-2f8fd5f9fde1"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwitchWeaponLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9760105c-1987-4cd7-a109-07b09a1c3b17"",
                    ""path"": ""<Keyboard>/rightBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""SwitchWeaponRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""112cc71a-71a1-45a2-99d7-f72f3f491d81"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwitchWeaponRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdb999c8-64a1-4a0d-92cb-ee6644e90d26"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f81cd31a-d22a-4b97-8016-d240bcbadff6"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6ac6dc4-e93b-4fc7-b876-7a2f115fc1da"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""035da033-da69-4493-8f86-49569fe7fa7b"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ToPistol"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76a68ba5-74a6-4d7e-8291-50d55e58757d"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ToRifle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f712ad50-25b5-4d42-960c-bed9280dbe1d"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ToShotgun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gameplay"",
            ""id"": ""15d84786-a41c-4081-b32e-30e126e17c68"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""4333a60d-b3ee-4766-b7c7-de113e059dcc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Resume"",
                    ""type"": ""Button"",
                    ""id"": ""760beac7-0c64-44a6-8727-f24a5d71e699"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""f9ca8976-968f-46b7-afbe-8d4c9317b836"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""74cee5f0-8ba1-415d-8513-4009f8ba050d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ead34a0-bcef-4cd1-b4c7-a71b8bbaddfd"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""981c5a62-9cf8-4c7c-ac07-2c979575900e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Resume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ac9b2ea-37ce-453a-bf21-c932bf1ee47e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""CloseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14a9a045-0308-4bd5-bb4f-122fb9174c68"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CloseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""570105e9-79f8-46fe-8297-3a05298f944c"",
            ""actions"": [
                {
                    ""name"": ""SpeedUpCredits"",
                    ""type"": ""Button"",
                    ""id"": ""928ffd74-c52c-43aa-aaff-98738a6b84b9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9dae2e6f-a6c4-48fd-8022-262301e2bac7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""SpeedUpCredits"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d5be8ae-59e7-4fd4-b17d-2bbd0bc501b6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SpeedUpCredits"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Sound"",
            ""id"": ""d9d6105c-48a3-4a99-b972-a86c8b7261c2"",
            ""actions"": [
                {
                    ""name"": ""IncreaseSound"",
                    ""type"": ""Button"",
                    ""id"": ""736021ca-5c54-4019-8c52-f9946f2d0362"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LowerSound"",
                    ""type"": ""Button"",
                    ""id"": ""1d4eac08-0f9c-46c1-b56e-a7c5b639d80e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""IncreaseMusic"",
                    ""type"": ""Button"",
                    ""id"": ""c1173d41-41b3-4de7-8cf7-5f128d0604cd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LowerMusic"",
                    ""type"": ""Button"",
                    ""id"": ""0484fab1-eeab-4841-ba3a-1c612d54d69b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""38e773bf-a80e-4df9-bc30-6ec56da174e3"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""IncreaseSound"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b492918a-e4ab-4e71-8558-dee0a1335635"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LowerSound"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efdd81f4-a9bd-406d-a7ca-ca76b03e3a1f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""IncreaseMusic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a464f16f-3252-434f-ad72-951512954846"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LowerMusic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Turn = m_Player.FindAction("Turn", throwIfNotFound: true);
        m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
        m_Player_SwitchWeaponLeft = m_Player.FindAction("SwitchWeaponLeft", throwIfNotFound: true);
        m_Player_SwitchWeaponRight = m_Player.FindAction("SwitchWeaponRight", throwIfNotFound: true);
        m_Player_ToPistol = m_Player.FindAction("ToPistol", throwIfNotFound: true);
        m_Player_ToRifle = m_Player.FindAction("ToRifle", throwIfNotFound: true);
        m_Player_ToShotgun = m_Player.FindAction("ToShotgun", throwIfNotFound: true);
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_Resume = m_Gameplay.FindAction("Resume", throwIfNotFound: true);
        m_Gameplay_CloseMenu = m_Gameplay.FindAction("CloseMenu", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_SpeedUpCredits = m_Menu.FindAction("SpeedUpCredits", throwIfNotFound: true);
        // Sound
        m_Sound = asset.FindActionMap("Sound", throwIfNotFound: true);
        m_Sound_IncreaseSound = m_Sound.FindAction("IncreaseSound", throwIfNotFound: true);
        m_Sound_LowerSound = m_Sound.FindAction("LowerSound", throwIfNotFound: true);
        m_Sound_IncreaseMusic = m_Sound.FindAction("IncreaseMusic", throwIfNotFound: true);
        m_Sound_LowerMusic = m_Sound.FindAction("LowerMusic", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Turn;
    private readonly InputAction m_Player_Fire;
    private readonly InputAction m_Player_SwitchWeaponLeft;
    private readonly InputAction m_Player_SwitchWeaponRight;
    private readonly InputAction m_Player_ToPistol;
    private readonly InputAction m_Player_ToRifle;
    private readonly InputAction m_Player_ToShotgun;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Turn => m_Wrapper.m_Player_Turn;
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputAction @SwitchWeaponLeft => m_Wrapper.m_Player_SwitchWeaponLeft;
        public InputAction @SwitchWeaponRight => m_Wrapper.m_Player_SwitchWeaponRight;
        public InputAction @ToPistol => m_Wrapper.m_Player_ToPistol;
        public InputAction @ToRifle => m_Wrapper.m_Player_ToRifle;
        public InputAction @ToShotgun => m_Wrapper.m_Player_ToShotgun;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Turn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurn;
                @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @SwitchWeaponLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLeft;
                @SwitchWeaponLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLeft;
                @SwitchWeaponLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponLeft;
                @SwitchWeaponRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponRight;
                @SwitchWeaponRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponRight;
                @SwitchWeaponRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchWeaponRight;
                @ToPistol.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToPistol;
                @ToPistol.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToPistol;
                @ToPistol.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToPistol;
                @ToRifle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToRifle;
                @ToRifle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToRifle;
                @ToRifle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToRifle;
                @ToShotgun.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToShotgun;
                @ToShotgun.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToShotgun;
                @ToShotgun.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToShotgun;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @SwitchWeaponLeft.started += instance.OnSwitchWeaponLeft;
                @SwitchWeaponLeft.performed += instance.OnSwitchWeaponLeft;
                @SwitchWeaponLeft.canceled += instance.OnSwitchWeaponLeft;
                @SwitchWeaponRight.started += instance.OnSwitchWeaponRight;
                @SwitchWeaponRight.performed += instance.OnSwitchWeaponRight;
                @SwitchWeaponRight.canceled += instance.OnSwitchWeaponRight;
                @ToPistol.started += instance.OnToPistol;
                @ToPistol.performed += instance.OnToPistol;
                @ToPistol.canceled += instance.OnToPistol;
                @ToRifle.started += instance.OnToRifle;
                @ToRifle.performed += instance.OnToRifle;
                @ToRifle.canceled += instance.OnToRifle;
                @ToShotgun.started += instance.OnToShotgun;
                @ToShotgun.performed += instance.OnToShotgun;
                @ToShotgun.canceled += instance.OnToShotgun;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_Resume;
    private readonly InputAction m_Gameplay_CloseMenu;
    public struct GameplayActions
    {
        private @Controls m_Wrapper;
        public GameplayActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @Resume => m_Wrapper.m_Gameplay_Resume;
        public InputAction @CloseMenu => m_Wrapper.m_Gameplay_CloseMenu;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Resume.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResume;
                @Resume.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResume;
                @Resume.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResume;
                @CloseMenu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCloseMenu;
                @CloseMenu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCloseMenu;
                @CloseMenu.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCloseMenu;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Resume.started += instance.OnResume;
                @Resume.performed += instance.OnResume;
                @Resume.canceled += instance.OnResume;
                @CloseMenu.started += instance.OnCloseMenu;
                @CloseMenu.performed += instance.OnCloseMenu;
                @CloseMenu.canceled += instance.OnCloseMenu;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_SpeedUpCredits;
    public struct MenuActions
    {
        private @Controls m_Wrapper;
        public MenuActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SpeedUpCredits => m_Wrapper.m_Menu_SpeedUpCredits;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @SpeedUpCredits.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSpeedUpCredits;
                @SpeedUpCredits.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSpeedUpCredits;
                @SpeedUpCredits.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSpeedUpCredits;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SpeedUpCredits.started += instance.OnSpeedUpCredits;
                @SpeedUpCredits.performed += instance.OnSpeedUpCredits;
                @SpeedUpCredits.canceled += instance.OnSpeedUpCredits;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // Sound
    private readonly InputActionMap m_Sound;
    private ISoundActions m_SoundActionsCallbackInterface;
    private readonly InputAction m_Sound_IncreaseSound;
    private readonly InputAction m_Sound_LowerSound;
    private readonly InputAction m_Sound_IncreaseMusic;
    private readonly InputAction m_Sound_LowerMusic;
    public struct SoundActions
    {
        private @Controls m_Wrapper;
        public SoundActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @IncreaseSound => m_Wrapper.m_Sound_IncreaseSound;
        public InputAction @LowerSound => m_Wrapper.m_Sound_LowerSound;
        public InputAction @IncreaseMusic => m_Wrapper.m_Sound_IncreaseMusic;
        public InputAction @LowerMusic => m_Wrapper.m_Sound_LowerMusic;
        public InputActionMap Get() { return m_Wrapper.m_Sound; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SoundActions set) { return set.Get(); }
        public void SetCallbacks(ISoundActions instance)
        {
            if (m_Wrapper.m_SoundActionsCallbackInterface != null)
            {
                @IncreaseSound.started -= m_Wrapper.m_SoundActionsCallbackInterface.OnIncreaseSound;
                @IncreaseSound.performed -= m_Wrapper.m_SoundActionsCallbackInterface.OnIncreaseSound;
                @IncreaseSound.canceled -= m_Wrapper.m_SoundActionsCallbackInterface.OnIncreaseSound;
                @LowerSound.started -= m_Wrapper.m_SoundActionsCallbackInterface.OnLowerSound;
                @LowerSound.performed -= m_Wrapper.m_SoundActionsCallbackInterface.OnLowerSound;
                @LowerSound.canceled -= m_Wrapper.m_SoundActionsCallbackInterface.OnLowerSound;
                @IncreaseMusic.started -= m_Wrapper.m_SoundActionsCallbackInterface.OnIncreaseMusic;
                @IncreaseMusic.performed -= m_Wrapper.m_SoundActionsCallbackInterface.OnIncreaseMusic;
                @IncreaseMusic.canceled -= m_Wrapper.m_SoundActionsCallbackInterface.OnIncreaseMusic;
                @LowerMusic.started -= m_Wrapper.m_SoundActionsCallbackInterface.OnLowerMusic;
                @LowerMusic.performed -= m_Wrapper.m_SoundActionsCallbackInterface.OnLowerMusic;
                @LowerMusic.canceled -= m_Wrapper.m_SoundActionsCallbackInterface.OnLowerMusic;
            }
            m_Wrapper.m_SoundActionsCallbackInterface = instance;
            if (instance != null)
            {
                @IncreaseSound.started += instance.OnIncreaseSound;
                @IncreaseSound.performed += instance.OnIncreaseSound;
                @IncreaseSound.canceled += instance.OnIncreaseSound;
                @LowerSound.started += instance.OnLowerSound;
                @LowerSound.performed += instance.OnLowerSound;
                @LowerSound.canceled += instance.OnLowerSound;
                @IncreaseMusic.started += instance.OnIncreaseMusic;
                @IncreaseMusic.performed += instance.OnIncreaseMusic;
                @IncreaseMusic.canceled += instance.OnIncreaseMusic;
                @LowerMusic.started += instance.OnLowerMusic;
                @LowerMusic.performed += instance.OnLowerMusic;
                @LowerMusic.canceled += instance.OnLowerMusic;
            }
        }
    }
    public SoundActions @Sound => new SoundActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnSwitchWeaponLeft(InputAction.CallbackContext context);
        void OnSwitchWeaponRight(InputAction.CallbackContext context);
        void OnToPistol(InputAction.CallbackContext context);
        void OnToRifle(InputAction.CallbackContext context);
        void OnToShotgun(InputAction.CallbackContext context);
    }
    public interface IGameplayActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnResume(InputAction.CallbackContext context);
        void OnCloseMenu(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnSpeedUpCredits(InputAction.CallbackContext context);
    }
    public interface ISoundActions
    {
        void OnIncreaseSound(InputAction.CallbackContext context);
        void OnLowerSound(InputAction.CallbackContext context);
        void OnIncreaseMusic(InputAction.CallbackContext context);
        void OnLowerMusic(InputAction.CallbackContext context);
    }
}
