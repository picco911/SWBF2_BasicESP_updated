using SharpDX;
using System.Runtime.InteropServices;

namespace BasicESP
{
    public class Frostbite
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct GameRenderer
        {
            [FieldOffset(1296)]
            public ulong m_GameRenderSettings0; //0x0510

            [FieldOffset(1304)]
            public ulong m_GameRenderSettings1; //0x0518

            [FieldOffset(1312)]
            public ulong m_pCompositionSettings; //0x0520

            [FieldOffset(1336)]
            public ulong m_RenderView; //0x0538
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RenderView
        {
            [FieldOffset(0x0000)]
            public Matrix m_CameraTransform; //0x0000

            [FieldOffset(0x0100)]
            public Matrix m_CameraTransformCopy; //0x0100

            [FieldOffset(0x0270)]
            public Matrix m_ViewMatrix; //0x0270

            [FieldOffset(0x02B0)]
            public Matrix m_ViewMatrixTranspose; //0x02B0

            [FieldOffset(0x02F0)]
            public Matrix m_ViewMatrixInverse; //0x02F0

            [FieldOffset(0x0330)]
            public Matrix m_ProjectionMatrix; //0x0330

            [FieldOffset(0x0370)]
            public Matrix m_ViewMatrixAtOrigin; //0x0370

            [FieldOffset(0x03B0)]
            public Matrix m_ProjectionTranspose; //0x03B0

            [FieldOffset(0x03F0)]
            public Matrix m_ProjectionInverse; //0x03F0

            [FieldOffset(0x0430)]
            public Matrix m_ViewProjection; //0x0430

            [FieldOffset(0x0470)]
            public Matrix m_ViewProjectionTranspose; //0x0470

            [FieldOffset(0x04B0)]
            public Matrix m_ViewProjectionInverse; //0x04B0
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct ClientGameContext
        {
            [FieldOffset(48)]
            public ulong m_GameTime; //0x0030

            [FieldOffset(56)]
            public ulong m_Level; //0x0038

            [FieldOffset(88)]
            public ulong m_ClientPlayerManager; //0x0058

            [FieldOffset(96)]
            public ulong m_OnlineManager; //0x0060

            [FieldOffset(104)]
            public ulong m_ClientGameView; //0x0068
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct ClientPlayerManager
        {
            [FieldOffset(8)]
            public ulong m_PlayerData; //0x0008

            [FieldOffset(240)]
            public ulong m_PlayerList; //0x00F0

            [FieldOffset(248)]
            public ulong m_PlayerListNoLocalPlayer; //0x00F8

            [FieldOffset(1384)]
            public ulong m_LocalPlayer; //0x0568
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct ClientPlayer
        {
            [FieldOffset(8)]
            public ulong m_PlayerData; //0x0008

            [FieldOffset(24)]
            public ulong m_Name; //0x0018

            [FieldOffset(60)]
            public uint x003C; //0x003C can be used as an assertion for valid player as 511

            [FieldOffset(88)]
            public uint m_TeamId; //0x0058 fb::TeamId

            [FieldOffset(384)]
            public ulong m_ThisClientPlayer; //0x0180

            [FieldOffset(512)]
            public ulong m_AttachedControllable; //0x0200

            [FieldOffset(528)]
            public ulong m_ControlledControllable; //0x0210

            [FieldOffset(552)]
            public ulong m_SomethingCamera; //0x0228

            [FieldOffset(584)]
            public uint m_PlayerIndex; //0x0248

            [FieldOffset(608)]
            public ulong m_ClientPlayerManager; //0x0260

            [FieldOffset(720)]
            public ulong m_DiceShooterClientPlayerExtent; //0x02D0
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct WSClientVehicleEntity
        {
            [FieldOffset(48)]
            public ulong m_VehicleEntityData; //0x0030

            [FieldOffset(56)]
            public ulong m_EntityComponents; //0x0038

            [FieldOffset(104)]
            public ulong m_PrevEntity; //0x0068

            [FieldOffset(112)]
            public ulong m_NextEntity; //0x0070

            [FieldOffset(160)]
            public ulong m_PhysicsComponent; //0x00A0

            [FieldOffset(332)]
            public uint m_TeamID; //0x014C

            [FieldOffset(616)]
            public ulong m_HealthComponent; //0x0268

            [FieldOffset(992)]
            public Vector4 m_MinAABB; //0x03E0

            [FieldOffset(1008)]
            public Vector4 m_MaxAABB; //0x03F0

            [FieldOffset(1056)]
            public Vector4 m_Velocity; //0x0420      // needs testing but included anyway
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct WSClientSoldierEntity
        {
            [FieldOffset(24)]
            public uint m_RenderFlags; //0x0018

            [FieldOffset(40)]
            public ulong m_EntityBus; //0x0028

            [FieldOffset(48)]
            public ulong m_SoldierEntityData; //0x0030

            [FieldOffset(56)]
            public ulong m_EntityComponents; //0x0038

            [FieldOffset(104)]
            public ulong m_PrevEntity; //0x0068 Prev of same entity type in list

            [FieldOffset(112)]
            public ulong m_NextEntity; //0x0070 Next "" "" 

            [FieldOffset(160)]
            public ulong m_PhysicsComponent; //0x00A0

            [FieldOffset(332)]
            public uint m_TeamID; //0x014C

            [FieldOffset(360)]
            public ulong m_ClientCharacterSpawnEntity; //0x0168

            [FieldOffset(440)]
            public ulong m_ThisSoldier; //0x01B8

            [FieldOffset(488)]
            public ulong m_ppClientPlayerEntryComponent; //0x01E8

            [FieldOffset(520)]
            public ulong m_ThisPlayer; //0x0208

            [FieldOffset(712)]
            public ulong m_HealthComponent; //0x0268 //new 2c8

            [FieldOffset(728)]
            public ulong m_SoldierBlueprint; //0x02D8

            [FieldOffset(760)]
            public ulong m_ClientAntAnimatableEntity1; //0x02F8

            [FieldOffset(808)]
            public ulong m_ToAnimationControl; //0x0328

            [FieldOffset(944)]
            public ulong m_ClientAntAnimatableEntity1b; //0x03B0

            [FieldOffset(952)]
            public ulong m_ClientAntAnimatableEntity2; //0x03B8

            [FieldOffset(1024)]
            public ulong m_ClientSoldierCameraComponent; //0x0400

            [FieldOffset(1136)]
            public float m_HeadboneOffsetZ; //0x0470

            [FieldOffset(1140)]
            public float m_HeadboneOffsetY; //0x0474 static offsets from m_Position

            [FieldOffset(1144)]
            public float m_HeadboneOffsetX; //0x0478

            [FieldOffset(1192)]
            public ulong m_SpottingTargetComponentData; //0x04A8

            [FieldOffset(1280)]
            public float unk_f0; //0x0500 rotate on z?

            [FieldOffset(1288)]
            public float unk_f1; //0x0508 change when char rotates Y

            [FieldOffset(1296)]
            public float unk_f2; //0x0510 corresponds to inverse matrix transaltion z

            [FieldOffset(1384)]
            public float m_StrafeDirection; //0x0568

            [FieldOffset(1408)]
            public Vector4 m_Velocity; //0x0580

            [FieldOffset(1440)]
            public ulong m_ClientBoneCollisionComponentPlus0x48; //0x05A0

            [FieldOffset(1776)]
            public ulong m_SomeOtherReplication; //0x06F0

            [FieldOffset(1784)]
            public ulong m_ClientSoldierReplication; //0x06F8

            [FieldOffset(1812)]
            public float m_YawRads; //0x0714 matches local aimer yaw

            [FieldOffset(2192)]
            public Vector4 m_Shootspace; //0x0890

            [FieldOffset(2336)]
            public float m_Pitch__; //0x0920

            [FieldOffset(2340)]
            public float m_Yaw__; //0x0924 almost yaw/pitch from aimer but a little off

            [FieldOffset(2372)]
            public float m_PitchRads; //0x0944 matches local aimer pitch

            [FieldOffset(2376)]
            public uint m_Pose; //0x0948

            [FieldOffset(2380)]
            public byte m_LocalRenderFlags; //0x094C 

            [FieldOffset(2464)]
            public ulong m_ClientSoldierWeaponsComponent; //0x09A0

            [FieldOffset(2472)]
            public ulong m_ClientSoldierBodyComponent; //0x09A8

            [FieldOffset(2488)]
            public ulong m_ClientVehicleEntryListenerComponent; //0x09B8

            [FieldOffset(2496)]
            public ulong m_ClientAimEntity; //0x09C0

            [FieldOffset(2504)]
            public ulong m_ClientSoldierWeapon; //0x09C8

            [FieldOffset(2536)]
            public byte m_IsSprinting; //0x09E8

            [FieldOffset(2552)]
            public byte m_IsOccluded; //0x09F8

            [FieldOffset(2792)]
            public ulong m_WSSoldierCustomizationGameplayAsset; //0x0AE8

            [FieldOffset(2800)]
            public ulong m_WSSoldierCustomizationKitAsset; //0x0AF0

            [FieldOffset(1880)]
            public ulong m_SoldierPrediction; //0x758

            [FieldOffset(1236)]
            public float m_HeightOffset; //0x04D4
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct HealthComponent
        {
            [FieldOffset(32)]
            public float m_Health; //0x0020

            [FieldOffset(36)]
            public float m_MaxHealth; //0x0024
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct ClientSoldierReplication
        {
            [FieldOffset(32)]
            public Vector4 m_Position; //0x0020

            [FieldOffset(48)]
            public Vector4 m_Velocity; //0x0030
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct AxisAlignedBox
        {
            [FieldOffset(0)]
            public Vector4 Min;  //0x0000

            [FieldOffset(16)]
            public Vector4 Max;  //0x0010
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct TransformAABBStruct
        {
            [FieldOffset(0)]
            public Matrix Matrix;

            [FieldOffset(0x0040)]
            public AxisAlignedBox AABB;
        }

        public enum PoseType
        {
            Standing = 0,
            Crouching = 1,
            Prone = 2
        }
    }
}
